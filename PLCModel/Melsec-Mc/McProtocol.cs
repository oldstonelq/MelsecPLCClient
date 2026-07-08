using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCTest.Model
{
    public abstract class McProtocol
    {
        /// <summary>
        /// 块大小
        /// </summary>
        private const int BlockSize = 0x0010;

        /// <summary>
        /// 命令帧
        /// </summary>
        public McFrameType CommandFrame { get; set; }

        /// <summary>
        /// 主机名
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        public int PortNumber { get; set; }

        /// <summary>
        /// MC命令
        /// </summary>
        private McCommand Command { get; set; }

        /// <summary>
        /// 连接
        /// </summary>
        protected abstract void Connect();

        /// <summary>
        /// 断开
        /// </summary>
        protected abstract void Disconnect();

        /// <summary>
        /// 发送命令
        /// </summary>
        /// <param name="iCommand"></param>
        /// <returns></returns>
        protected abstract byte[] Execute(byte[] iCommand);


        protected McProtocol(string iHostName, int iPortNumber)
        {
            this.CommandFrame = McFrameType.MC3E;
            this.HostName = iHostName;
            this.PortNumber = iPortNumber;
        }

        /// <summary>
        /// 打开PLC
        /// </summary>
        /// <returns></returns>
        public int Open()
        {
            Connect();
            this.Command = new McCommand(this.CommandFrame);
            return 0;
        }

        /// <summary>
        /// 关闭PLC
        /// </summary>
        /// <returns></returns>
        public int Close()
        {
            Disconnect();
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        private byte[] TryExecution(byte[] command)
        {
            byte[] response;
            int count = 10;
            do
            {
                response = Execute(command);
                --count;
                if (count < 0)
                {
                    throw new Exception("无法从PLC获取正确的值.");
                }
            } while (this.Command.IsIncorrectResponse(response));
            return response;
        }

        /// <summary>
        /// 写位软元件(装置）
        /// </summary>
        /// <param name="mcRegisterType">寄存器类型</param>
        /// <param name="address">起始地址</param>
        /// <param name="size">大小</param>
        /// <param name="data">数据</param>
        /// <returns>结束代码：0时操作成功</returns>
        public int WriteBitDevice(McRegisterType mcRegisterType, int address, int size, int[] data)
        {
            var buffer = new List<byte>(6)
            {
                  (byte) address
                , (byte) (address >> 8)
                , (byte) (address >> 16)
                , (byte) mcRegisterType
                , (byte) size
                , (byte) (size >> 8)
            };

            var d = (byte)data[0];
            var i = 0;
            while (i < data.Length)
            {
                if (i % 2 == 0)
                {
                    d = (byte)data[i];
                    d <<= 4;
                }
                else
                {
                    d |= (byte)(data[i] & 0x01);
                    buffer.Add(d);
                }
                ++i;
            }
            if (i % 2 != 0)
            {
                buffer.Add(d);
            }
            byte[] sendCommand = this.Command.SetCommand(0x1401, 0x0001, buffer.ToArray());
            byte[] reviceResponse = TryExecution(sendCommand);
            int rtCode = this.Command.SetResponse(reviceResponse);
            return rtCode;
        }

        /// <summary>
        /// 写软元件
        /// </summary>
        /// <param name="mcRegisterType">寄存器类型</param>
        /// <param name="address">起始地址</param>
        /// <param name="size">大小</param>
        /// <param name="data">数据</param>
        /// <returns>结束代码：0时操作成功</returns>
        public int WriteDevice(McRegisterType mcRegisterType, int address, int size, int[] data)
        {
            var buffer = new List<byte>(6)
            {
                  (byte) address
                , (byte) (address >> 8)
                , (byte) (address >> 16)
                , (byte) mcRegisterType
                , (byte) size
                , (byte) (size >> 8)
            };

            foreach (int d in data)
            {
                buffer.Add((byte)d);
                buffer.Add((byte)(d >> 8));
            }

            byte[] sdCommand = this.Command.SetCommand(0x1401, 0x0000, buffer.ToArray());
            byte[] rtResponse = TryExecution(sdCommand);
            int rtCode = this.Command.SetResponse(rtResponse);

            return rtCode;

        }

        /// <summary>
        /// 写软元件
        /// </summary>
        /// <param name="mcRegisterType">寄存器类型</param>
        /// <param name="address">起始地址</param>
        /// <param name="size">大小</param>
        /// <param name="data">数据</param>
        /// <returns>结束代码：0时操作成功</returns>
        public int WriteDevice(McRegisterType mcRegisterType, int address, byte[] data)
        {
            int size = data.Length % 2 == 0 ? data.Length / 2 : (data.Length / 2) + 1;
            var buffer = new List<byte>(6)
            {
                  (byte) address
                , (byte) (address >> 8)
                , (byte) (address >> 16)
                , (byte) mcRegisterType
                , (byte) size
                , (byte) (size >> 8)
            };
            buffer.AddRange(data);

            if (data.Length % 2 != 0)
                buffer.Add(0);

            byte[] sdCommand = this.Command.SetCommand(0x1401, 0x0000, buffer.ToArray());
            byte[] rtResponse = TryExecution(sdCommand);
            int rtCode = this.Command.SetResponse(rtResponse);
            return rtCode;

        }

        /// <summary>
        /// 读位软元件(装置）
        /// </summary>
        /// <param name="mcRegisterType">寄存器类型</param>
        /// <param name="address">起始地址</param>
        /// <param name="size">大小</param>
        /// <param name="outData">输出数据</param>
        /// <returns>结束代码：0时操作成功</returns>
        public int ReadBitDevice(McRegisterType mcRegisterType, int address, int size, out int[] outData)
        {
            outData = new int[size];
            var buffer = new List<byte>(6)
            {
                  (byte) address
                , (byte) (address >> 8)
                , (byte) (address >> 16)
                , (byte) mcRegisterType
                , (byte) size
                , (byte) (size >> 8)
            };

            byte[] sdCommand = this.Command.SetCommand(0x0401, 0x0001, buffer.ToArray());
            byte[] rtResponse = TryExecution(sdCommand);
            int rtCode = this.Command.SetResponse(rtResponse);
            byte[] rtData = this.Command.Response;

            for (int i = 0; i < size; ++i)
            {
                if (i % 2 == 0)
                {
                    outData[i] = (rtCode == 0) ? ((rtData[i / 2] >> 4) & 0x01) : 0;
                }
                else
                {
                    outData[i] = (rtCode == 0) ? (rtData[i / 2] & 0x01) : 0;
                }
            }
            return rtCode;
        }

        /// <summary>
        /// 读软元件
        /// </summary>
        /// <param name="mcRegisterType">寄存器类型</param>
        /// <param name="address">起始地址</param>
        /// <param name="size">大小</param>
        /// <param name="outData">输出数据</param>
        /// <returns>结束代码：0时操作成功</returns>
        public int ReadDevice(McRegisterType mcRegisterType, int address, int size, out byte[] outData)
        {
            var buffer = new List<byte>(6)
            {
                  (byte) address
                , (byte) (address >> 8)
                , (byte) (address >> 16)
                , (byte) mcRegisterType
                , (byte) size
                , (byte) (size >> 8)
            };

            byte[] sdCommand = this.Command.SetCommand(0x0401, 0x0000, buffer.ToArray());
            byte[] rtResponse = TryExecution(sdCommand);
            int rtCode = this.Command.SetResponse(rtResponse);
            outData = this.Command.Response;
            return rtCode;
        }

    }
}
