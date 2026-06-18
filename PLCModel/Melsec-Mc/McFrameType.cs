using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCTest.Model
{
    /// <summary>
    /// MC帧格式
    /// </summary>
    public enum McFrameType
    {
        /// <summary>
        /// 3E 帧格式
        /// </summary>
        // ReSharper disable once InconsistentNaming
        MC3E,
        /// <summary>
        /// 4E 帧格式
        /// </summary>
        // ReSharper disable once InconsistentNaming
        MC4E
    }

    public enum McRegisterType
    {
        #region bit
        X = 0x9C,
        Y = 0x9D,
        M = 0x90,
        L = 0x92,
        B = 0xA0,
        S = 0x98,
        F = 0x93,
        #endregion

        #region Word（2个字节）
        D = 0xA8,
        R = 0xAF,
        TN = 0xC2,
        CN = 0xC5,
        #endregion

        #region 未知
        V = 0x94,
        W = 0xB4,
        TS = 0xC1,
        TC = 0xC0,
        CS = 0xC4,
        CC = 0xC3,
        SM = 0x91,
        #endregion
    }
}
