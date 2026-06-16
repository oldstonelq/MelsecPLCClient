using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCClient.Model
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

        X = 0x9C,

        Y = 0x9D,

        M = 0x90,

        D = 0xA8,

        L = 0x92,

        F = 0x93,

        V = 0x94,

        B = 0xA0,

        S = 0x98,

        W = 0xB4,

        TS = 0xC1,

        TC = 0xC0,

        TN = 0xC2,

        CS = 0xC4,

        CC = 0xC3,

        CN = 0xC5,

        SM = 0x91,

        R = 0xAF,


    }
}
