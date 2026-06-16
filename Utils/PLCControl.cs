using PLCClient.Model;
using PLCClient.Tool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PLCClient.Utils
{
    public class PLCControl
    {
        /// <summary>
        /// PLC基类
        /// </summary>
        private McProtocolTcp _master;

        /// <summary>
        /// PLCIP
        /// </summary>
        private string _IPAddress;

        /// <summary>
        /// PLCPort
        /// </summary>
        private string _Port;

        /// <summary>
        /// PLC连接
        /// </summary>
        /// <param name="IPAddress"></param>
        /// <param name="Port"></param>
        public PLCControl(string IPAddress, string Port)
        {
            try
            {
                _IPAddress = IPAddress;
                _Port = Port;
                _master = new McProtocolTcp(IPAddress, int.Parse(Port));
                 Task .Run (() => 
                   Connect());
                  
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public void Connect()
        {
            try
            {
                while (true)
                {
                    if (_master.Connected)
                    {
                        continue;
                    }

                    if (_master.Connected == false)
                    {
                        for (int i = 0; i < 3 && _master.Connected == false; i++)
                        {
                            _master.Open();
                        }
                    }
                    Thread .Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        /// <summary>
        /// GetConnectedState
        /// </summary>
        /// <returns></returns>
        public bool GetConnected()
        {
            return _master.Connected;
        }

        /// <summary>
        /// WriteDevice
        /// </summary>
        /// <param name="Area"></param>
        /// <param name="address"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool WriteDevice(McRegisterType Area, int address, short data)
        {
            if (_master.Connected==false)
            {
                return false;
            }
            return _master.WriteDevice(Area, address, StructureHelp.StructToBytes(data)) == 0;
        }
        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="address"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool WriteDevice(McRegisterType Area,int address, short[] data)
        {
            byte[] bytes = new byte[data.Length * 2];
            StructureHelp.Int16ToByte(data, data.Length, ref bytes);
            return _master.WriteDevice(Area, address, bytes) == 0;
        }
        /// <summary>
        /// ReadDevice
        /// </summary>
        /// <param name="Area"></param>
        /// <param name="address"></param>
        /// <param name="size"></param>
        /// <param name="outData"></param>
        /// <returns></returns>
        public bool ReadDevice(McRegisterType Area, int address, int size, out byte[] outData)
        {
            if (_master.Connected == false)
            {
                outData=new byte[size];
                return false;
            }
            var ReadResult = _master.ReadDevice(Area, address, size, out outData);
            return ReadResult == 0 ? true : false;
        }
    }
}
