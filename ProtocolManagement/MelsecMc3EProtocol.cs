using PLCTest.Interface;
using PLCTest.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PLCTest.ProtocolManagement
{
    /// <summary>
    /// 三菱3E通讯协议
    /// </summary>
    public class MelsecMc3EProtocol : ICommunicationProtocol
    {
        /// <summary>
        /// 构建完整的报文数据包
        /// </summary>
        /// <param name="_data"></param>
        /// <returns></returns>
        public byte[] BuildCompletePack(byte[] _data)
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
        /// <summary>
        /// 构建读取数据的报文
        /// </summary>
        /// <param name="memory"></param>
        /// <param name="adress"></param>
        /// <param name="length"></param>
        /// <param name="isBit"></param>
        /// <returns></returns>
        public byte[] BuildReadData(byte memory, int adress, int length, bool isBit)
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
        /// 构建写入数据的报文
        /// </summary>
        /// <param name="memory"></param>
        /// <param name="adress"></param>
        /// <param name="length"></param>
        /// <param name="isBit"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] BuildWriteData(byte memory, int adress, int length, bool isBit, byte[] data)
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
        /// 解析读取位数据的返回报文
        /// </summary>
        /// <param name="_data"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Result<bool[]> ParseReadBitResponse(byte[] _data,int size)
        {
            Result<bool[]> result = new Result<bool[]>();
            if (_data.Length > 11)
            {
                //4.校验结束码，不为0则为错误
                if (BitConverter.ToUInt16(_data, 9) != 0)
                {
                    result.ErrorMsg = "结束代码错误";
                }
                else
                {
                    //5.解析返回数据，返回bool数组
                    bool[] RealData = new bool[size];
                    for (int i = 0; i < size / 2; i++)
                    {
                        RealData[i * 2] = (Convert.ToByte(_data[11 + i]) & 0x10) == 0x10 ? true : false;
                        RealData[i * 2 + 1] = (Convert.ToByte(_data[11 + i]) & 0x01) == 0x01 ? true : false;
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
        /// 解析读取字数据的返回报文
        /// </summary>
        /// <param name="_data"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public Result<short[]> ParseReadWordResponse(byte[] _data ,int size)
        {
            Result<short[]> result = new Result<short[]>();
            if (_data.Length > 11)
            {
                //4.校验结束码，不为0则为错误
                if (BitConverter.ToUInt16(_data, 9) != 0)
                {
                    result.ErrorMsg = "结束代码错误";
                }
                else
                {
                    //5.解析返回数据，返回short数组
                    short[] RealData = new short[size];
                    byte[] mDataByte = new byte[_data.Length - 11];
                    Array.Copy(_data, 11, mDataByte, 0, mDataByte.Length);
                    for (int i = 0; i < RealData.Length; i++)
                    {
                        RealData[i] = BitConverter.ToInt16(mDataByte, i * 2);
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
        /// 解析写入数据的返回报文
        /// </summary>
        /// <param name="_data"></param>
        /// <returns></returns>
        public Result<bool> ParseWriteResponse(byte[] _data)
        {
            Result<bool> result = new Result<bool>();
            if (_data.Length > 11)
            {
                //5.结束代码不为0说明出错了
                if (BitConverter.ToUInt16(_data, 9) != 0)
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
    }
}
