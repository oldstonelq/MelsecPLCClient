using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCClient.Model
{
    public class McCommand
    {
        /// <summary>
        /// 帧类型
        /// </summary>
        private McFrameType FrameType { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        private uint SerialNumber { get; set; }

        /// <summary>
        /// 网络编事情
        /// </summary>
        private uint NetwrokNumber { get; set; }

        /// <summary>
        /// PC编号
        /// </summary>
        private uint PcNumber { get; set; }

        /// <summary>
        /// IO编号
        /// </summary>
        private uint IoNumber { get; set; }

        /// <summary>
        /// 通道编号
        /// </summary>
        private uint ChannelNumber { get; set; }

        /// <summary>
        /// 保留
        /// </summary>
        private uint CpuTimer { get; set; }

        /// <summary>
        /// 结果代码
        /// </summary> 
        private int ResultCode { get; set; }

        /// <summary>
        /// 反回值
        /// </summary> 
        public byte[] Response { get; private set; }

        public McCommand(McFrameType mcFrameType)
        {
            this.FrameType = mcFrameType;
            this.SerialNumber = 0x0001u;

            this.NetwrokNumber = 0x0000u;
            this.PcNumber = 0x00FFu;
            this.IoNumber = 0x03FFu;
            this.ChannelNumber = 0x0000u;
            this.CpuTimer = 0x0010u;
        }


        /// <summary>
        /// 构造MC命令
        /// </summary>
        /// <param name="iMainCommand">指令</param>
        /// <param name="iSubCommand">子指令</param>
        /// <param name="iData">数据</param>
        /// <returns></returns>
        public byte[] SetCommand(uint iMainCommand, uint iSubCommand, byte[] iData)
        {
            // 01.计算数据长度
            var dataLength = (uint)(iData.Length + 6);  // 保留(2字节）+ 指令(2字节） +  子指令(2字节）

            var ret = new List<byte>(iData.Length + 20);

            uint frame = (this.FrameType == McFrameType.MC3E) ? 0x0050u : (this.FrameType == McFrameType.MC4E) ? 0x0054u : 0x0000u;

            ret.Add((byte)frame);                           // 副帧头(2字节)
            ret.Add((byte)(frame >> 8));
            if (this.FrameType == McFrameType.MC4E)         // 当为4E帧时添加系号(2字节)
            {
                ret.Add((byte)this.SerialNumber);
                ret.Add((byte)(this.SerialNumber >> 8));
                ret.Add(0x00);
                ret.Add(0x00);
            }

            ret.Add((byte)this.NetwrokNumber);              // 网络编号(1字节）
            ret.Add((byte)this.PcNumber);                   // PC编号(1字节）

            ret.Add((byte)this.IoNumber);                   // IO编号(2字节）
            ret.Add((byte)(this.IoNumber >> 8));

            ret.Add((byte)this.ChannelNumber);              // 通道编号(1字节）

            ret.Add((byte)dataLength);                      // 数据长度(2字节）
            ret.Add((byte)(dataLength >> 8));

            ret.Add((byte)this.CpuTimer);                   // 保留(2字节）
            ret.Add((byte)(this.CpuTimer >> 8));

            ret.Add((byte)iMainCommand);                    // 指令(2字节）
            ret.Add((byte)(iMainCommand >> 8));

            ret.Add((byte)iSubCommand);                     // 子指令(2字节）
            ret.Add((byte)(iSubCommand >> 8));

            ret.AddRange(iData);

            return ret.ToArray();
        }

        // ================================================================================
        public int SetResponse(byte[] iResponse)
        {
            int min = (this.FrameType == McFrameType.MC3E) ? 11 : 15;
            if (min <= iResponse.Length)
            {
                var btCount = new[] { iResponse[min - 4], iResponse[min - 3] };           // 获取数据长度
                var btCode = new[] { iResponse[min - 2], iResponse[min - 1] };           // 获取结束代码

                int rsCount = BitConverter.ToUInt16(btCount, 0);                       // 数据长度
                this.ResultCode = BitConverter.ToUInt16(btCode, 0);                       // 结束代码

                this.Response = new byte[rsCount - 2];                                    // 数据长度 - 结束代码(2字节) = 实际数据长度
                Buffer.BlockCopy(iResponse, min, this.Response, 0, this.Response.Length); // 复制数据内容
            }
            return this.ResultCode;
        }


        // ================================================================================
        public bool IsIncorrectResponse(byte[] iResponse)
        {
            var min = (this.FrameType == McFrameType.MC3E) ? 11 : 15;
            var btCount = new[] { iResponse[min - 4], iResponse[min - 3] };
            var btCode = new[] { iResponse[min - 2], iResponse[min - 1] };
            var rsCount = BitConverter.ToUInt16(btCount, 0) - 2;
            var rsCode = BitConverter.ToUInt16(btCode, 0);
            return (rsCode == 0 && rsCount != (iResponse.Length - min));
        }
    }
}
