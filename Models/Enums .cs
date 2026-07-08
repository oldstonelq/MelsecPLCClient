using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCTest.Models
{
    /// <summary>
    /// 枚举类
    /// </summary>
    public class Enums
    {
        /// <summary>
        /// PLC内存区域枚举
        /// </summary>
        public enum MemoryArea
        {
            X = 0x9C,
            Y = 0x9D,
            M = 0x90,
            D = 0xA8,
           
        }
    }
}
