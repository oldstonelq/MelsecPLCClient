using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCTest.Interface
{
    /// <summary>
    /// PLC服务端接口
    /// </summary>
    public interface IPLCServer : IDisposable
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
    }
}
