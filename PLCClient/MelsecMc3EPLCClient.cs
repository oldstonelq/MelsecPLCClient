using PLCTest.Interface;
using PLCTest.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static PLCTest.Models.Enums;

namespace PLCTest.Device
{
    /// <summary>
    /// 三菱PLC-MC协议客户端（3E帧数据结构）
    /// </summary>
    public class MelsecMc3EPLCClient : IPLCDevice
    {
        /// <summary>
        /// 通讯方式接口
        /// </summary>
        protected ICommunication _comm;
        /// <summary>
        /// 连接状态:
        /// </summary>
        public bool IsConnected => _comm==null ?false: _comm.IsConnected;
        /// <summary>
        /// 取消令牌
        /// </summary>
        private CancellationTokenSource _cts;
        /// <summary>
        /// 心跳线程
        /// </summary
        private Task _connectTask;
        /// <summary>
        /// 关闭标志 True表示已关闭，False表示未关闭
        /// </summary>
        private bool _disposed = false;
        public MelsecMc3EPLCClient(ICommunication comm)
        {
            _comm = comm;
        }

        #region 公有方法
        /// <summary>
        /// 连接方法
        /// </summary>
        public void Connect()
        {
            // 1. 先判断：心跳任务正在运行，直接返回，禁止重复启动
            if (_connectTask != null && !_connectTask.IsCompleted)
            {
                return;
            }
            // 2. 清理上一次残留的令牌源，释放资源
            if (_cts != null)
            {
                _cts.Dispose();
                _cts = null;
            }
            // 3. 新建令牌，启动心跳循环
            _cts = new CancellationTokenSource();
            //4. 启动心跳循环任务
            _connectTask = Task.Run(() => HeartBeatLoop(_cts.Token), _cts.Token);
        }
        /// <summary>
        /// 断开连接方法
        /// </summary>
        public void Disconnect()
        {
            if (_disposed) return;
            _disposed = true;

            try
            {
                //1.发送心跳线程取消请求
                if (_cts != null && !_cts.IsCancellationRequested)
                {
                    _cts.Cancel();
                }

                try
                {
                    // 2.等待连接任务退出（短超时以防阻塞）
                    _connectTask?.Wait(2000);
                }
                catch (AggregateException) { }
                catch (Exception) { }

                // 3.尝试关闭 master 连接
                try
                {
                    if (_comm != null)
                    {
                        _comm.DisConnectServer();
                    }
                }
                catch { }
            }
            finally
            {
                //4.释放资源
                _cts?.Dispose();
                _cts = null;
                _connectTask = null;
                _comm = null;
            }
        }
        /// <summary>
        /// 读取位数据
        /// </summary>
        /// <param name="area"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Result<bool> ReadBit(MemoryArea area, int address)
        {
            Result<bool> result = new Result<bool>();
            //1.校验软元件类型
            if (area == MemoryArea.D)
            {
                result.ErrorMsg = "软元件类型错误";
                return result;
            }
            //2.发送读取位数据指令并接收返回数据
            byte[] mReByte = _comm.SendAndRecevieData(CompletePack(BuildReadData((byte)area, address, 1, true)));
            //3.校验返回数据长度
            if (mReByte.Length > 11)
            {
                //4.校验结束码，不为0则为错误
                if (BitConverter.ToUInt16(mReByte, 9) != 0)
                {
                    result.ErrorMsg = "结束代码错误";
                }
                else
                {
                    //5.解析返回数据，返回bool值
                    bool RealData = (Convert.ToByte(mReByte[11]) & 0x10) == 0x10 ? true : false;
                    result.IsSuccess = true;
                    result.Data = RealData;
                }
            }
            else
            {
                result.ErrorMsg = "返回数据长度错误";
            }
            return result;
        }
        /// <summary>
        /// 读取位数组数据
        /// </summary>
        /// <param name="area"></param>
        /// <param name="address"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Result<bool[]> ReadBitArray(MemoryArea area, int address, int size)
        {
            Result<bool[]> result = new Result<bool[]>();
            //1.校验软元件类型
            if (area == MemoryArea.D)
            {
                result.ErrorMsg = "软元件类型错误";
                return result;
            }
            //2.发送读取位数据指令并接收返回数据
            byte[] mReByte = _comm.SendAndRecevieData(CompletePack(BuildReadData((byte)area, address, size, true)));
            //3.校验返回数据长度
            if (mReByte.Length > 11)
            {
                //4.校验结束码，不为0则为错误
                if (BitConverter.ToUInt16(mReByte, 9) != 0)
                {
                    result.ErrorMsg = "结束代码错误";
                }
                else
                {
                    //5.解析返回数据，返回bool数组
                    bool[] RealData = new bool[size];
                    for (int i = 0; i < size / 2; i++)
                    {
                        RealData[i * 2] = (Convert.ToByte(mReByte[11 + i]) & 0x10) == 0x10 ? true : false;
                        RealData[i * 2 + 1] = (Convert.ToByte(mReByte[11 + i]) & 0x01) == 0x01 ? true : false;
                    }
                    result.IsSuccess = true;
                    result.Data = RealData;
                }
            }
            else
            {
                result.ErrorMsg = "返回数据长度错误";
            }
            return result;
        }
        /// <summary>
        /// 写入位数据
        /// </summary>
        /// <param name="area"></param>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Result<bool> WriteBit(MemoryArea area, int address, bool value)
        {
            Result<bool> result = new Result<bool>();
            //1.校验软元件类型
            if (area == MemoryArea.D)
            {
                result.ErrorMsg = "软元件类型错误";
                return result;
            }
            //2.计算数据
            byte[] mData = new byte[1];
            // 第 2*i 位 → 高4位 0x10
            if (value)
            {
                mData[0] += 0x10;
            }
            //3.发送写入位数据指令并接收返回数据
            byte[] mReByte = _comm.SendAndRecevieData(CompletePack(BuildWriteData((byte)area, address, 1, true, mData)));
            //4.校验返回数据长度
            if (mReByte.Length > 11)
            {
                //5.校验结束码，不为0则为错误
                if (BitConverter.ToUInt16(mReByte, 9) != 0)
                {
                    result.ErrorMsg = "结束代码错误";
                }
                else
                {
                    //写入只要结束码为0就表示成功
                    result.IsSuccess = true;
                    result .Data = true;
                }
            }
            else
            {
                result.ErrorMsg = "返回数据长度错误";
            }
            return result;
        }
        /// <summary>
        /// 写入位数组数据
        /// </summary>
        /// <param name="area"></param>
        /// <param name="address"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Result<bool> WriteBitArray(MemoryArea area, int address, bool[] values)
        {
            Result<bool> result = new Result<bool>();
            //1.检查写入数据是否为空， 
            if (values == null || values.Length == 0)
            {
                result.ErrorMsg ="写入数据为空";
                return result;
            }
            //2.校验软元件类型
            if (area == MemoryArea.D)
            {
                result.ErrorMsg = "软元件类型错误";
                return result;
            }
            //3.计算打包后字节长度：每2个bit封装1个byte，向上取整
            int mLength = (values.Length + 1) / 2;
            byte[] mData = new byte[mLength];

            for (int i = 0; i < mLength; i++)
            {
                // 第 2*i 位 → 高4位 0x10
                if (values[i * 2])
                {
                    mData[i] += 0x10;
                }
                // 判断是否存在下一组bit
                int secondBitIndex = i * 2 + 1;
                if (secondBitIndex < values.Length)
                {
                    // 第 2*i+1 位 → 低4位 0x01
                    if (values[secondBitIndex])
                    {
                        mData[i] += 0x01;
                    }
                }
            }
            //4.发送读取位数据指令并接收返回数据
            byte[] mReByte = _comm. SendAndRecevieData(CompletePack(BuildWriteData((byte)area, address, values.Length, true, mData)));
            //5.校验返回数据长度
            if (mReByte.Length > 11)
            {
                //6.校验结束码，不为0则为错误
                if (BitConverter.ToUInt16(mReByte, 9) != 0)
                {
                    result.ErrorMsg = "结束代码错误";
                }
                else
                {
                    //写入只要结束码为0就表示成功
                    result.IsSuccess = true;
                    result.Data = true;
                }
            }
            else
            {
                result.ErrorMsg = "返回数据长度错误";
            }
            return result;
        }
        /// <summary>
        /// 读取字数据
        /// </summary>
        /// <param name="area"></param>
        /// <param name="address"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Result<short> ReadWord(MemoryArea area, int address)
        {
            Result<short> result = new Result<short>();
            //1.校验软元件类型
            if (area != MemoryArea.D)
            {
                result.ErrorMsg = "软元件类型错误";
                return result;
            }
            //2.发送读取字数据指令并接收返回数据
            byte[] mReByte = _comm.SendAndRecevieData(CompletePack(BuildReadData((byte)area, address, 1, false)));
            //3.校验返回数据长度
            if (mReByte.Length > 11)
            {
                //4.校验结束码，不为0则为错误
                if (BitConverter.ToUInt16(mReByte, 9) != 0)
                {
                    result.ErrorMsg = "结束代码错误";
                }
                else
                {
                    //5.解析返回数据，返回short值
                    byte[] mDataByte = new byte[mReByte.Length - 11];
                    Array.Copy(mReByte, 11, mDataByte, 0, mDataByte.Length);
                    short RealData = BitConverter.ToInt16(mDataByte,2);
                    result.IsSuccess = true;
                    result.Data = RealData;
                }
            }
            else
            {
                result.ErrorMsg = "返回数据长度错误";
            }
            return result;
        }
        /// <summary>
        /// 读取字数组数据
        /// </summary>
        /// <param name="area"></param>
        /// <param name="address"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Result<short[]> ReadWordArray(MemoryArea area, int address, int size)
        {
            Result<short[]> result = new Result<short[]>();
            //1.校验软元件类型
            if (area != MemoryArea.D)
            {
                result .ErrorMsg = "软元件类型错误";
                return result;
            }
            //2.发送读取字数据指令并接收返回数据
            byte[] mReByte = _comm.SendAndRecevieData(CompletePack(BuildReadData((byte)area, address, size, false)));
            //3.校验返回数据长度
            if (mReByte.Length > 11)
            {
                //4.校验结束码，不为0则为错误
                if (BitConverter.ToUInt16(mReByte, 9) != 0)
                {
                    result.ErrorMsg = "结束代码错误";
                }
                else
                {
                    //5.解析返回数据，返回short数组
                    short[] RealData = new short[size];
                    byte[] mDataByte = new byte[mReByte.Length - 11];
                    Array.Copy(mReByte, 11, mDataByte, 0, mDataByte.Length);
                    for (int i = 0; i < RealData.Length; i++)
                    {
                        RealData[i] = BitConverter.ToInt16(mDataByte, i * 2);
                    }
                    result.IsSuccess = true;
                    result .Data = RealData;
                }
            }
            else
            {
                result.ErrorMsg = "返回数据长度错误";
            }
            return result;
        }
        /// <summary>
        /// 写入字数据
        /// </summary>
        /// <param name="area"></param>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Result<bool> WriteWord(MemoryArea area, int address, short value)
        {
            Result<bool> result = new Result<bool>();
            //1.校验软元件类型
            if (area != MemoryArea.D)
            {
                result.ErrorMsg = "软元件类型错误";
                return result;
            }
            //2.计算打包后字节长度：每个short占2个字节
            byte[] mData = new byte[2];
            for (int i = 0; i < 1; i++)
            {
                byte[] mbyte = BitConverter.GetBytes(value);
                mbyte.CopyTo(mData, i * 2);
            }
            //3.发送写入子数组数据指令并接收返回数据
            byte[] mReByte = _comm.SendAndRecevieData(CompletePack(BuildWriteData((byte)area, address, 1, false, mData)));
            //4.校验返回数据长度
            if (mReByte.Length > 11)
            {
                //5.结束代码不为0说明出错了
                if (BitConverter.ToUInt16(mReByte, 9) != 0)
                {
                    result.ErrorMsg = "结束代码错误";
                }
                else
                {
                    //写入只要结束码为0就表示成功
                    result.IsSuccess = true;
                    result.Data = true;
                }
            }
            else
            {
                result.ErrorMsg = "返回数据长度错误";
            }
            return result;
        }
        /// <summary>
        /// 写入字数组数据
        /// </summary>
        /// <param name="area"></param>
        /// <param name="address"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Result<bool> WriteWordArray(MemoryArea area, int address, short[] values)
        {
            Result<bool> result = new Result<bool>();
            //1.检查写入数据是否为空， 
            if (values == null || values.Length == 0)
            {
                result.ErrorMsg = "写入数据为空";
                return result;
            }
            //2.校验软元件类型
            if (area != MemoryArea.D)
            {
                result.ErrorMsg = "软元件类型错误";
                return result;
            }
            //3.计算打包后字节长度：每个short占2个字节
            byte[] mData = new byte[values.Length * 2];
            for (int i = 0; i < values.Length; i++)
            {
                byte[] mbyte = BitConverter.GetBytes(values[i]);
                mbyte.CopyTo(mData, i * 2);
            }
            //4.发送写入子数组数据指令并接收返回数据
            byte[] mReByte = _comm.SendAndRecevieData(CompletePack(BuildWriteData((byte)area, address, values.Length, false, mData)));
            //5.校验返回数据长度
            if (mReByte.Length > 11)
            {
                //6.结束代码不为0说明出错了
                if (BitConverter.ToUInt16(mReByte, 9) != 0)
                {
                    result.ErrorMsg = "结束代码错误";
                }
                else
                {
                    //写入只要结束码为0就表示成功
                    result.IsSuccess = true;
                    result.Data = true;
                }
            }
            else
            {
                result.ErrorMsg = "返回数据长度错误";
            }
             return result;
        }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            //_comm.DisConnectServer();
            //_comm = null;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 私有心跳循环方法，用于保持与PLC的连接状态
        /// </summary>
        /// <param name="token"></param>
        private void HeartBeatLoop(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        if (IsConnected)
                        {
                            // 已连接，等待一段时间再检查，避免忙循环
                            Thread.Sleep(1000);
                            continue;
                        }
                        else
                        {
                            //未连接，尝试重新连接
                            for (int i = 0; i < 3 && !IsConnected && !token.IsCancellationRequested; i++)
                            {
                               _comm.ConnectServer();
                            }
                        }
                        Thread.Sleep(1000);
                    }
                    catch (Exception)
                    {
                        // 忽略连接内异常，继续重试，除非被取消
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // 取消时安全退出
            }
        }
        /// <summary>
        /// 构建读数据指令数据区数组
        /// </summary>
        /// <param name="memory">软元件类型</param>
        /// <param name="adress">起始地址</param>
        /// <param name="length">地址长度</param>
        /// <param name="isBit">是否为位操作</param>
        /// <returns>数据区数组</returns>
        private byte[] BuildReadData(byte memory, int adress, int length, bool isBit)
        {
            byte[] Data = new byte[10];
            //读:0401
            Data[0] = 0x01;
            Data[1] = 0x04;
            //以点为单位:0x01,字为单位0x00
            Data[2] = isBit ? (byte)0x01 : (byte)0x00;
            Data[3] = 0x00;
            //起始地址
            Data[4] = BitConverter.GetBytes(adress)[0];
            Data[5] = BitConverter.GetBytes(adress)[1];
            Data[6] = BitConverter.GetBytes(adress)[2];
            //软元件类型
            Data[7] = memory;
            //软元件点数
            Data[8] = (byte)(length % 256);
            Data[9] = (byte)(length / 256);
            return Data;
        }

        /// <summary>
        /// 构建写数据指令数据区数组
        /// </summary>
        /// <param name="memory">软元件类型</param>
        /// <param name="adress">起始地址</param>
        /// <param name="length">地址长度</param>
        /// <param name="isBit">是否为位操作</param>
        /// <param name="data">写入的数据</param>
        /// <returns>数据区数组</returns>
        private byte[] BuildWriteData(byte memory, int adress, int length, bool isBit, byte[] data)
        {
            byte[] Data = new byte[10 + data.Length];
            //读:1401
            Data[0] = 0x01;
            Data[1] = 0x14;
            //以点为单位:0x01,字为单位0x00
            Data[2] = isBit ? (byte)0x01 : (byte)0x00;
            Data[3] = 0x00;
            //起始地址
            Data[4] = BitConverter.GetBytes(adress)[0];
            Data[5] = BitConverter.GetBytes(adress)[1];
            Data[6] = BitConverter.GetBytes(adress)[2];
            //软元件类型
            Data[7] = memory;
            //软元件点数
            Data[8] = (byte)(length % 256);
            Data[9] = (byte)(length / 256);
            data.CopyTo(Data, 10);
            return Data;
        }

        /// <summary>
        /// 完整的包
        /// </summary>
        /// <param name="_data">数据区数组</param>
        /// <returns>完整的数据包</returns>
        private byte[] CompletePack(byte[] _data)
        {
            byte[] Command = new byte[11 + _data.Length];
            //副标题
            Command[0] = 0x50;
            Command[1] = 0x00;
            //网络号
            Command[2] = 0x00;
            //PLC编号
            Command[3] = 0xFF;
            //目标模块IO编号
            Command[4] = 0xFF;
            Command[5] = 0x03;
            //目标模块站号
            Command[6] = 0x00;
            //请求数据长度(剩余数据长度，CPU+data长度)
            Command[7] = (byte)((Command.Length - 9) % 256);
            Command[8] = (byte)((Command.Length - 9) / 256);
            //CPU监视定时器
            Command[9] = 0x0A;
            Command[10] = 0x00;
            _data.CopyTo(Command, 11);
            return Command;
        }
        #endregion
    }
}
