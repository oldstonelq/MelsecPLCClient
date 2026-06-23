using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PLCTest.Utils
{
    public  class MelsecMcPLCSever
    {
        public MelsecMcPLCSever(string Ip, int Port)
        {
            this.ipAndPoint = new IPEndPoint(IPAddress.Parse(Ip), Port);
            this.mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public DataExchange tmp = new DataExchange();
        public delegate void DelDataArrived(DataExchange tmp);
        public event DelDataArrived OnDataArrivedEvent;  //收到数据事件
        public event DelDataArrived OnDiscoveredDeviceEvent;  //发现设备事件
        private readonly List<Socket> sockets = new List<Socket>();
        private readonly object socketsLock = new object();
        // 简单的模拟PLC内存（线程安全访问）
        private readonly object memoryLock = new object();
        // D 寄存器（字设备）
        private readonly Dictionary<int, ushort> dRegisters = new Dictionary<int, ushort>();
        // M 线圈（位设备）
        private readonly Dictionary<int, bool> mBits = new Dictionary<int, bool>();
        private CancellationTokenSource _cleanupCts;
        public bool IsWorking { get; set; } = false;
        /// <summary>
        /// IP地址和Port口
        /// </summary>
        private IPEndPoint _IpAndPoint;
        public IPEndPoint ipAndPoint
        {
            get { return _IpAndPoint; }
            set { _IpAndPoint = value; }
        }

        /// <summary>
        /// 负责监听的Socket对象
        /// </summary>
        private Socket _MySocket;
        public Socket mySocket
        {
            get { return _MySocket; }
            set { _MySocket = value; }
        }

        /// <summary>
        /// 负责收发数据的Socket对象
        /// </summary>
        private Socket _ConnectSocket;
        public Socket ConnectSocket
        {
            get { return _ConnectSocket; }
            set { _ConnectSocket = value; }
        }
        /// <summary>
        /// 服务器开始监听
        /// </summary>
        public void StartListen()
        {
            IsWorking = true;
            mySocket.Bind(ipAndPoint);
            mySocket.Listen(10);
            StartSocketCleanup();
            // 开始首次接受
            BeginAcceptNext();
        }
        private void BeginAcceptNext()
        {
            try
            {
                mySocket.BeginAccept(new AsyncCallback((iResult) =>
                {
                    Socket client = null;
                    try
                    {
                        // EndAccept 可能抛出 ObjectDisposedException，当 socket 已关闭时忽略
                        client = mySocket.EndAccept(iResult);
                    }
                    catch (ObjectDisposedException)
                    {
                        // 如果正在关闭，静默返回；否则记录日志
                        return;
                    }
                    catch (SocketException)
                    {
                        // 视情况处理或记录
                        return;
                    }

                    if (client != null && client.Connected)
                    {
                        if (OnDiscoveredDeviceEvent != null)
                        {
                            tmp.ip = client.RemoteEndPoint;
                            OnDiscoveredDeviceEvent(tmp);
                        }
                        Task task = ReceiveDataFromClient(client);
                        AddSocket(client);
                    }

                    // 如果仍在工作，继续接受下一个连接
                    if (IsWorking)
                    {
                        BeginAcceptNext();
                    }
                }), null);
            }
            catch (ObjectDisposedException)
            {
                // 监听 socket 已被关闭
            }
        }
        public void StopListen()
        {
            IsWorking = false;
            StopSocketCleanup();
            if (this.ConnectSocket != null && this.ConnectSocket.Connected)
            {
                this.ConnectSocket.Shutdown(SocketShutdown.Both);
                this.ConnectSocket.Close();
            }
            if (this.mySocket != null)
            {
                this.mySocket.Close();
            }
            foreach (var socket in sockets)
            {
                if (socket != null && socket.Connected)
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
            }
        }
        public Task ReceiveDataFromClient(Socket rcvSocket)
        {
            return Task.Run(() =>
            {
                using (rcvSocket)
                {
                    while (IsWorking)
                    {
                        byte[] byt = new byte[1024];
                        int len = rcvSocket.Receive(byt, 0, byt.Length, SocketFlags.None);
                        if (len <= 0)
                        {
                            continue;
                        }

                        tmp.data = Encoding.Default.GetString(byt, 0, len);

                        // 简单解析 Mitsubishi MC Protocol (3E frame) 请求并构建基础响应。
                        // 说明：本实现做一个保守、安全的解析与响应（只处理基本帧结构并返回成功结束码 0x0000）。
                        // - 请求帧子头 (subheader) 0x50 0x00 表示 MC 3E 请求帧（常见）
                        // - 响应帧使用子头 0xD0 0x00（MC 协议中响应用不同子头，这里采用常见值）
                        // - 帧结构（简化/常见偏移）:
                        //   [0-1] subheader (0x50 0x00 request)
                        //   [2]   network number
                        //   [3]   pc number
                        //   [4-5] request destination module I/O number
                        //   [6]   request destination module station number
                        //   [7-8] request data length (little-endian) -> n (表示从命令开始到数据结尾的字节数)
                        //   [9-10] cpu/timer (通常为 0x00 0x00)
                        //   [11...] command(2) + subcommand(2) + request data...
                        try
                        {
                            if (len >= 11 && byt[0] == 0x50 && byt[1] == 0x00)
                            {
                                // 读取请求中声明的数据长度（从命令开始到数据末尾的字节数）
                                ushort reqDataLen = 0;
                                if (len >= 9 + 1) // 确保能读取到长度字段
                                {
                                    reqDataLen = BitConverter.ToUInt16(byt, 7); // little-endian
                                }

                                // command/subcommand 在偏移 11 开始（按 MC 3E frame）
                                int cmdOffset = 11;
                                ushort command = 0;
                                ushort subcommand = 0;
                                if (len >= cmdOffset + 4)
                                {
                                    command = BitConverter.ToUInt16(byt, cmdOffset);
                                    subcommand = BitConverter.ToUInt16(byt, cmdOffset + 2);
                                }

                                // 构建响应头（11 字节），采用子头 0xD0 0x00
                                byte[] respHeader = new byte[11];
                                respHeader[0] = 0xD0;
                                respHeader[1] = 0x00;
                                Array.Copy(byt, 2, respHeader, 2, 5);

                                // 默认成功结束码
                                byte[] endCode = new byte[2] { 0x00, 0x00 };
                                byte[] responseData = endCode;

                                // 请求数据区偏移与长度（reqDataLen 包含 command+subcommand+requestData）
                                int requestDataOffset = cmdOffset + 4;
                                int requestDataLen = Math.Max(0, reqDataLen - 4);

                                try
                                {
                                    // 处理常见命令：0x0401 = 批量读，0x1401 = 批量写
                                    if (command == 0x0401)
                                    {
                                        // 读取：起始地址(3 bytes little-endian) + device code(1) + points(2)
                                        if (requestDataLen >= 6 && len >= requestDataOffset + 6)
                                        {
                                            int addr = requestDataOffset;
                                            int start = byt[addr] | (byt[addr + 1] << 8) | (byt[addr + 2] << 16);
                                            byte deviceCode = byt[addr + 3];
                                            ushort points = BitConverter.ToUInt16(byt, addr + 4);

                                            var dataBuf = new List<byte>();
                                            lock (memoryLock)
                                            {
                                                if (deviceCode == 0xA8) // D 寄存器，按字返回（2 bytes per point）
                                                {
                                                    for (int i = 0; i < points; i++)
                                                    {
                                                        ushort val = 0;
                                                        dRegisters.TryGetValue(start + i, out val);
                                                        dataBuf.AddRange(BitConverter.GetBytes(val));
                                                    }
                                                }
                                                else if (deviceCode == 0x90) // M 线圈，按点返回 1 byte/point (0x00/0x01)
                                                {
                                                    for (int i = 0; i < points; i++)
                                                    {
                                                        bool v = false;
                                                        mBits.TryGetValue(start + i, out v);
                                                        dataBuf.Add((byte)(v ? 0x01 : 0x00));
                                                    }
                                                }
                                                else
                                                {
                                                    // 未支持的设备码，返回错误结束码
                                                    endCode = new byte[2] { 0x01, 0x00 };
                                                    responseData = endCode;
                                                }
                                            }

                                            if (dataBuf.Count > 0)
                                            {
                                                //响应 = 结束码 + 数据
                                                //responseData = new byte[2 + dataBuf.Count];
                                                //endCode.CopyTo(responseData, 0);
                                                //dataBuf.ToArray().CopyTo(responseData, 2);
                                                responseData = dataBuf.ToArray ();
                                            }
                                        }
                                    }
                                    else if (command == 0x1401)
                                    {
                                        // 写入：起始地址(3) + device code(1) + points(2) + data ...
                                        if (requestDataLen >= 6 && len >= requestDataOffset + 6)
                                        {
                                            int addr = requestDataOffset;
                                            int start = byt[addr] | (byt[addr + 1] << 8) | (byt[addr + 2] << 16);
                                            byte deviceCode = byt[addr + 3];
                                            ushort points = BitConverter.ToUInt16(byt, addr + 4);
                                            int dataStart = addr + 6;

                                            lock (memoryLock)
                                            {
                                                if (deviceCode == 0xA8)
                                                {
                                                    // 预期每点 2 bytes
                                                    for (int i = 0; i < points; i++)
                                                    {
                                                        int off = dataStart + i * 2;
                                                        if (len >= off + 2)
                                                        {
                                                            ushort val = BitConverter.ToUInt16(byt, off);
                                                            dRegisters[start + i] = val;
                                                        }
                                                    }
                                                }
                                                else if (deviceCode == 0x90)
                                                {
                                                    // 预期每点 1 byte (0x00/0x01)
                                                    for (int i = 0; i < points; i++)
                                                    {
                                                        int off = dataStart + i;
                                                        if (len > off)
                                                        {
                                                            byte b = byt[off];
                                                            mBits[start + i] = (b != 0x00);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    endCode = new byte[2] { 0x01, 0x00 };
                                                }
                                            }

                                            responseData = endCode;
                                        }
                                    }
                                }
                                catch
                                {
                                    // 解析或处理失败，返回错误结束码
                                    responseData = new byte[2] { 0x01, 0x00 };
                                }

                                // 填写响应的长度字段（responseData 的字节数），小端
                                ushort respDataLenField = (ushort)responseData.Length;
                                BitConverter.GetBytes(respDataLenField).CopyTo(respHeader, 7);

                                respHeader[7] = (byte)(responseData.Length+2);
                                // 保持 CPU timer 字段为 0x0000
                                respHeader[9] = 0x00;
                                respHeader[10] = 0x00;

                                // 合并响应头与响应数据并发送
                                byte[] resp = new byte[respHeader.Length + responseData.Length];
                                Array.Copy(respHeader, 0, resp, 0, respHeader.Length);
                                Array.Copy(responseData, 0, resp, respHeader.Length, responseData.Length);
                                try { rcvSocket.Send(resp); } catch { }
                            }
                        }
                        catch
                        {
                            // 忽略解析异常，避免阻塞接收循环（可根据需要记录日志）
                        }

                        if (OnDataArrivedEvent != null)
                        {
                            OnDataArrivedEvent(tmp);
                        }
                    }
                }
            });
        }
        /// <summary>
        /// 发送数据给客户端
        /// </summary>
        /// <param name="Msg"></param>
        public void Send(string Msg)
        {
            try
            {
                byte[] bytStr = Encoding.Default.GetBytes(Msg);
                this.ConnectSocket.BeginSend(bytStr, 0, bytStr.Length, SocketFlags.None, new AsyncCallback((iar) =>
                {
                    Socket Skt = (Socket)iar.AsyncState;
                    int length = this.ConnectSocket.EndSend(iar);
                }), this.ConnectSocket);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// 将新连接加入列表（线程安全）
        /// </summary>
        private void AddSocket(Socket client)
        {
            if (client == null) return;
            lock (socketsLock)
            {
                sockets.Add(client);
            }
        }

        /// <summary>
        /// 从列表中移除并优雅释放 socket
        /// </summary>
        private void RemoveSocket(Socket s)
        {
            if (s == null) return;
            lock (socketsLock)
            {
                if (sockets.Contains(s))
                    sockets.Remove(s);
            }
            try { s.Shutdown(System.Net.Sockets.SocketShutdown.Both); } catch { }
            try { s.Close(); } catch { }
            try { s.Dispose(); } catch { }
        }

        /// <summary>
        /// 启动后台清理任务（在服务器启动时调用）
        /// </summary>
        private void StartSocketCleanup()
        {
            if (_cleanupCts != null) return;
            _cleanupCts = new CancellationTokenSource();
            var ct = _cleanupCts.Token;
            Task.Run(() => CleanupLoop(ct), ct);
        }

        /// <summary>
        /// 停止后台清理任务（在服务器关闭时调用）
        /// </summary>
        private void StopSocketCleanup()
        {
            if (_cleanupCts == null) return;
            _cleanupCts.Cancel();
            _cleanupCts.Dispose();
            _cleanupCts = null;
        }

        /// <summary>
        /// 周期性扫描 sockets 列表，移除已断开的或异常的 socket
        /// </summary>
        private async Task CleanupLoop(CancellationToken ct)
        {
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    Socket[] listCopy;
                    lock (socketsLock)
                    {
                        listCopy = sockets.ToArray();
                    }

                    foreach (var s in listCopy)
                    {
                        bool remove = false;
                        try
                        {
                            // 常用断连检测：如果未连接或可读但可用字节为0，说明远端已关闭连接
                            if (!s.Connected)
                            {
                                remove = true;
                            }
                            else if (s.Poll(1000,SelectMode.SelectRead) && s.Available == 0)
                            {
                                remove = true;
                            }
                        }
                        catch
                        {
                            remove = true;
                        }

                        if (remove)
                            RemoveSocket(s);
                    }

                    await Task.Delay(2000, ct).ConfigureAwait(false);
                }
            }
            catch (OperationCanceledException) { }
        }
    }
    /// <summary>
    /// 数据交换类，用于传递数据
    /// </summary>
    public class DataExchange : EventArgs
    {
        private EndPoint _IP;
        public EndPoint ip
        {
            get { return _IP; }
            set { _IP = value; }
        }

        private Socket _TmpSkt;
        public Socket tmpSkt
        {
            get { return _TmpSkt; }
            set { _TmpSkt = value; }
        }

        private string _Data;
        public string data
        {
            get { return _Data; }
            set { _Data = value; }
        }
    }
}
