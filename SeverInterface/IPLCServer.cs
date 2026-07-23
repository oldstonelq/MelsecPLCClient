using PLCTest.Models;
using static PLCTest.Models.Enums;

namespace PLCTest.SeverInterface
{
    /// <summary>
    /// PLC服务端接口
    /// </summary>
    public interface IPLCServer
    {
        /// <summary>
        /// 服务是否开启 True 开启，False 关闭
        /// </summary>
        bool SeverIsOpen { get; }
        /// <summary>
        /// 连接客户端数量
        /// </summary>
        int ClientCount { get; }
        /// <summary>
        /// 开启服务端
        /// </summary>
        void OpenServer();
        /// <summary>
        /// 关闭服务端
        /// </summary>
        void CloseServer();

        #region 寄存器操作

        /// <summary>
        /// 读取位数据
        /// </summary>
        Result<bool> ReadBit(MemoryArea area, int address);
        /// <summary>
        /// 读取位数组
        /// </summary>
        Result<bool[]> ReadBitArray(MemoryArea area, int address, int size);
        /// <summary>
        /// 写入位数据
        /// </summary>
        Result<bool> WriteBit(MemoryArea area, int address, bool value);
        /// <summary>
        /// 写入位数组
        /// </summary>
        Result<bool> WriteBitArray(MemoryArea area, int address, bool[] values);
        /// <summary>
        /// 读取字数据
        /// </summary>
        Result<short> ReadWord(MemoryArea area, int address);
        /// <summary>
        /// 读取字数组
        /// </summary>
        Result<short[]> ReadWordArray(MemoryArea area, int address, int size);
        /// <summary>
        /// 写入字数据
        /// </summary>
        Result<bool> WriteWord(MemoryArea area, int address, short value);
        /// <summary>
        /// 写入字数组
        /// </summary>
        Result<bool> WriteWordArray(MemoryArea area, int address, short[] values);

        #endregion

        #region 辅助方法（供UI层使用）

        /// <summary>
        /// 获取指定区域的寄存器数量
        /// </summary>
        int GetRegisterCount(MemoryArea area);
        /// <summary>
        /// 检查指定地址是否存在寄存器
        /// </summary>
        bool ContainsRegister(MemoryArea area, int address);
        /// <summary>
        /// 尝试添加寄存器（仅在不存在时添加）
        /// </summary>
        bool TryAddRegister(MemoryArea area, int address);
        /// <summary>
        /// 清空指定区域的所有寄存器
        /// </summary>
        void ClearRegisters(MemoryArea area);
        /// <summary>
        /// 清空指定区域指定范围内的寄存器值（设为默认值，不清除Key）
        /// </summary>
        void ResetRegisterValues(MemoryArea area, int start, int length);
        /// <summary>
        /// 重置指定区域所有寄存器的值
        /// </summary>
        void ResetAllRegisterValues(MemoryArea area);

        #endregion
    }
}
