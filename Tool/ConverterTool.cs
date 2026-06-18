using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCClient.Tool
{
    public class ConverterTool
    {
        public static short[] BytesToShorts(byte[] bytes, bool expectedLittleEndian = true)
        {
            if (bytes == null || bytes.Length < 2) return new short[0];
            int count = bytes.Length / 2;
            short[] result = new short[count];

            bool needSwap = BitConverter.IsLittleEndian != expectedLittleEndian;
            if (!needSwap)
            {
                for (int i = 0; i < count; i++)
                    result[i] = BitConverter.ToInt16(bytes, i * 2);
            }
            else
            {
                // 按两字节翻转后再转换
                byte[] tmp = new byte[2];
                for (int i = 0; i < count; i++)
                {
                    tmp[0] = bytes[i * 2 + 1];
                    tmp[1] = bytes[i * 2];
                    result[i] = BitConverter.ToInt16(tmp, 0);
                }
            }

            return result;
        }

        public static string ShortToAscii(short value)
        {
            // 将 short 解释为两个字节（小端序），尝试转换为可打印 ASCII 字符
            var bytes = BitConverter.GetBytes(value); // little-endian: bytes[0] = 低字节
            var sb = new StringBuilder(2);
            foreach (var b in bytes)
            {
                if (b == 0) continue;                // 忽略 NUL 字符
                if (b >= 32 && b <= 126)            // 可打印 ASCII 范围
                    sb.Append((char)b);
                else
                    sb.Append('.');                 // 非打印字符用 '.' 占位（可选）
            }

            var result = sb.ToString();
            return string.IsNullOrEmpty(result) ? value.ToString() : result;
        }

        /// <summary>
        /// 将读取到的字节流拆分为高字节数组和低字节数组。
        /// 假定每两个字节为一组，默认顺序为 高字节 在前、低字节 在后（highByteFirst = true）。
        /// 如果字节数量为奇数，会把最后一个剩余字节作为高字节（或低字节），另一端补0。
        /// </summary>
       public static void StoreReadBytesAsHighLow(byte[] readBytes,ref byte[] byteHight, ref byte[] byteLow, bool highByteFirst = true)
        {
            if (readBytes == null || readBytes.Length == 0)
            {
               byteHight = new byte[0];
               byteLow = new byte[0];
                return;
            }

            int pairs = readBytes.Length / 2;
            bool hasRemainder = (readBytes.Length % 2) != 0;

            byteHight = new byte[pairs + (hasRemainder ? 1 : 0)];
            byteLow = new byte[pairs + (hasRemainder ? 1 : 0)];

            for (int i = 0; i < pairs; i++)
            {
                int idx = i * 2;
                if (highByteFirst)
                {
                    byteHight[i] = readBytes[idx];
                    byteLow[i] = readBytes[idx + 1];
                }
                else
                {
                    byteLow[i] = readBytes[idx];
                    byteHight[i] = readBytes[idx + 1];
                }
            }

            if (hasRemainder)
            {
                int lastIdx = pairs * 2;
                if (highByteFirst)
                {
                    byteHight[pairs] = readBytes[lastIdx];
                    byteLow[pairs] = 0;
                }
                else
                {
                    byteLow[pairs] = readBytes[lastIdx];
                    byteHight[pairs] = 0;
                }
            }
        }

        /// <summary>
        /// 将 short 转为二进制位数组（长度默认 16）。
        /// 返回的数组按从最低位到最高位（LSB-first）排列：result[0] 为 bit0, result[15] 为 bit15。
        /// </summary>
        public static bool[] ShortToBoolArray(short value, int bitCount = 16)
        {
            if (bitCount <= 0) throw new System.ArgumentOutOfRangeException(nameof(bitCount));
            if (bitCount > 16) bitCount = 16; // short 只有 16 位
            bool[] bits = new bool[bitCount];
            ushort u = (ushort)value; // 使用无符号以正确处理负数的二进制位（补码）
            for (int i = 0; i < bitCount; i++)
            {
                bits[i] = ((u >> i) & 1) == 1;
            }
            return bits;
        }

        /// <summary>
        /// 将给定值 currentValue 的指定 bitPosition 位置设为 1 并返回新值。
        /// bitPosition 约定：0 表示最低位 (bit0)，15 表示最高位 (bit15)。
        /// 使用示例：currentValue = SetBitToOne(currentValue, bitPosition);
        /// 或者：this.CurrentValue = SetBitToOne(this.CurrentValue, bitPosition);
        /// </summary>
        public static int SetBitToOne(int currentValue, int bitPosition)
        {
            if (bitPosition < 0 || bitPosition > 15)
                throw new System.ArgumentOutOfRangeException(nameof(bitPosition), "bitPosition 必须在 0 到 15 之间。");

            // 切换指定位置的位：如果为1则变为0，为0则变为1
            return currentValue ^ (1 << bitPosition);
        }

    }
}
