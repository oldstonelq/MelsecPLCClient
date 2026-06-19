using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
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
            if (this.ConnectSocket != null && this.ConnectSocket.Connected)
            {
                this.ConnectSocket.Shutdown(SocketShutdown.Both);
                this.ConnectSocket.Close();
            }
            if (this.mySocket != null)
            {
                this.mySocket.Close();
            }
            IsWorking = false;
        }
        public Task ReceiveDataFromClient(Socket rcvSocket)
        {
            return Task.Run(() =>
            {
                using (rcvSocket)
                {
                    while (true)
                    {
                        byte[] byt = new byte[1024];
                        int len = rcvSocket.Receive(byt, 0, byt.Length, SocketFlags.None);
                        tmp.data = Encoding.Default.GetString(byt, 0, len);
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
