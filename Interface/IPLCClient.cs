using PLCTest.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PLCTest.Models.Enums;

namespace PLCTest.Interface
{
    /// <summary>
    /// 基础PLC设备接口
    /// </summary>
    public interface IPLCClient
    {
        /// <summary>
        /// 连接状态
        /// </summary>
        bool IsConnected { get; }
        /// <summary>
        /// 连接
        /// </summary>
        void Connect();
        /// <summary>
        /// 关闭连接
        /// </summary>
        void Disconnect();
        /// <summary>
        /// 读取位数据
        /// </summary>
        /// <param name="area">区域</param>
        /// <param name="address">地址</param>
        /// <returns></returns>
        Result<bool> ReadBit(MemoryArea area, int address);
        /// <summary>
        /// 读取位数组
        /// </summary>
        /// <param name="area">区域</param>
        /// <param name="address">地址</param>
        /// <param name="size">长度</param>
        /// <returns></returns>
        Result<bool[]> ReadBitArray(MemoryArea area, int address, int size);
        /// <summary>
        /// 写入位数据
        /// </summary>
        /// <param name="area">区域</param>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        Result<bool> WriteBit(MemoryArea area, int address, bool value);
        /// <summary>
        /// 写入位数组
        /// </summary>
        /// <param name="area">区域</param>
        /// <param name="address">地址</param>
        /// <param name="values">数组</param>
        /// <returns></returns>
        Result<bool> WriteBitArray(MemoryArea area, int address, bool[] values);
        /// <summary>
        /// 读取字数据
        /// </summary>
        /// <param name="area">区域</param>
        /// <param name="address">地址</param>
        /// <returns></returns>
        Result<short> ReadWord(MemoryArea area, int address);
        /// <summary>
        /// 读取字数组
        /// </summary>
        /// <param name="area">区域</param>
        /// <param name="address">地址</param>
        /// <param name="size">长度</param>
        /// <returns></returns>
        Result<short[]> ReadWordArray(MemoryArea area, int address, int size);
        /// <summary>
        /// 写入字数据
        /// </summary>
        /// <param name="area">区域</param>
        /// <param name="address">地址</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        Result<bool> WriteWord(MemoryArea area, int address, short value);
        /// <summary>
        /// 写入字数组
        /// </summary>
        /// <param name="area">区域</param>
        /// <param name="address">地址</param>
        /// <param name="values">值数组</param>
        /// <returns></returns>
        Result<bool> WriteWordArray(MemoryArea area, int address, short[] values);
    }
}
