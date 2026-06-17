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

        public bool Connected => this.Client.Connected;

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
            }
        }

        protected override void Disconnect()
        {
            TcpClient c = this.Client;
            if (c.Connected)
            {
                c.Close();
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
