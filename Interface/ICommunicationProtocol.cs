using PLCTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCTest.Interface
{
    /// <summary>
    /// 协议接口
    /// </summary>
    public interface ICommunicationProtocol
    {
        /// <summary>
        /// 创建读取数据包
        /// </summary>
        /// <param name="memory"></param>
        /// <param name="adress"></param>
        /// <param name="length"></param>
        /// <param name="isBit"></param>
        /// <returns></returns>
        byte[] BuildReadData(byte memory, int adress, int length, bool isBit);
        /// <summary>
        /// 创建写入数据包
        /// </summary>
        /// <param name="memory"></param>
        /// <param name="adress"></param>
        /// <param name="length"></param>
        /// <param name="isBit"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        byte[] BuildWriteData(byte memory, int adress, int length, bool isBit, byte[] data);
        /// <summary>
        /// 拼接完整数据包
        /// </summary>
        /// <param name="_data"></param>
        /// <returns></returns>
        byte[] BuildCompletePack(byte[] _data);
        /// <summary>
        /// 解析写返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_data"></param>
        /// <returns></returns>
        Result<bool> ParseWriteResponse(byte[] _data);
        /// <summary>
        /// 解析读未数据返回
        /// </summary>
        /// <param name="_data"></param>
        /// <returns></returns>
        Result<bool[]> ParseReadBitResponse(byte[] _data,int size);
        /// <summary>
        /// 解析读字数据返回
        /// </summary>
        /// <param name="_data"></param>
        /// <returns></returns>
        Result<short[]> ParseReadWordResponse(byte[] _data,int size);
    }
}
