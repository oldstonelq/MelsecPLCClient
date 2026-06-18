using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PLCClient.Model
{
    public class McProtocolTcp : McProtocol
    {
        private TcpClient Client { get; set; }
        private NetworkStream Stream { get; set; }

        // 改进 Connected 判断，使用底层 Socket 的 Poll/Available 来检测远端是否已关闭
        public bool Connected
        {
            get
            {
                try
                {
                    if (this.Client == null)
                        return false;

                    var socket = this.Client.Client;
                    if (socket == null)
                        return false;

                    // 如果 socket 已经标记为可读且没有可用字节，通常表示远端已关闭连接
                    bool closed = socket.Poll(1000, SelectMode.SelectRead) && socket.Available == 0;
                    return socket.Connected && !closed;
                }
                catch
                {
                    return false;
                }
            }
        }

        public McProtocolTcp() : this("", 0) { }
        public McProtocolTcp(string iHostName, int iPortNumber) : base(iHostName, iPortNumber)
        {
            this.Client = new TcpClient();
        }

        protected override void Connect()
        {
            this.Client = new TcpClient();
            TcpClient c = this.Client;
            if (!c.Connected)
            {
                var ka = new List<byte>(sizeof(uint) * 3);
                ka.AddRange(BitConverter.GetBytes(1u));
                ka.AddRange(BitConverter.GetBytes(45000u));
                ka.AddRange(BitConverter.GetBytes(5000u));
                c.Client.IOControl(IOControlCode.KeepAliveValues, ka.ToArray(), null);
                c.Connect(this.HostName, this.PortNumber);
                this.Stream = c.GetStream();
                try
                {
                    // 设置超时，避免在网络异常时长时间阻塞读取
                    this.Stream.ReadTimeout = 5000;
                    this.Stream.WriteTimeout = 5000;
                    c.NoDelay = true;
                }
                catch { }
            }
        }

        protected override void Disconnect()
        {
            try
            {
                this.Stream?.Close();
            }
            catch { }
            finally
            {
                this.Stream = null;
            }

            try
            {
                if (this.Client != null)
                {
                    try { this.Client.Close(); } catch { }
                }
            }
            catch { }
            finally
            {
                this.Client = null;
            }
        }

        protected override byte[] Execute(byte[] iCommand)
        {
            NetworkStream networkStream = this.Stream;
            networkStream.Write(iCommand, 0, iCommand.Length);
            networkStream.Flush();

            using (var memoryStream = new MemoryStream())
            {
                var buff = new byte[256];
                do
                {
                    int count = networkStream.Read(buff, 0, buff.Length);
                    if (count == 0)
                    {
                        throw new Exception("PLC被断开");
                    }
                    memoryStream.Write(buff, 0, count);
                } while (networkStream.DataAvailable);
                return memoryStream.ToArray();
            }
        }

    }
}
