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
    public class PLCControl : IDisposable
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

        private CancellationTokenSource _cts;
        private Task _connectTask;
        private bool _disposed = false;

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

                _cts = new CancellationTokenSource();
                _connectTask = Task.Run(() => Connect(_cts.Token), _cts.Token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // 改为支持取消的 Connect
        public void Connect(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        if (_master.Connected)
                        {
                            // 已连接，等待一段时间再检查，避免忙循环
                            Thread.Sleep(1000);
                            continue;
                        }

                        if (!_master.Connected)
                        {
                            for (int i = 0; i < 3 && !_master.Connected && !token.IsCancellationRequested; i++)
                            {
                                _master.Open();
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
        /// GetConnectedState
        /// </summary>
        /// <returns></returns>
        public bool GetConnected()
        {
            return _master != null && _master.Connected;
        }

        /// <summary>
        /// WriteDevice
        /// </summary>
        public bool WriteDevice(McRegisterType Area, int address, short data)
        {
            if (_master == null || !_master.Connected)
            {
                return false;
            }
            return _master.WriteDevice(Area, address, StructureHelp.StructToBytes(data)) == 0;
        }

        public bool WriteDevice(McRegisterType Area, int address, short[] data)
        {
            if (_master == null) return false;
            byte[] bytes = new byte[data.Length * 2];
            StructureHelp.Int16ToByte(data, data.Length, ref bytes);
            return _master.WriteDevice(Area, address, bytes) == 0;
        }

        public bool WriteDevice(McRegisterType mcRegisterType, int address, int size, int[] data)
        {
            if (_master == null) return false;
            return _master.WriteBitDevice(mcRegisterType, address, size, data) == 0;
        }

        public bool ReadDevice(McRegisterType Area, int address, int size, out byte[] outData)
        {
            outData = new byte[size];
            if (_master == null || !_master.Connected)
            {
                return false;
            }
            var ReadResult = _master.ReadDevice(Area, address, size, out outData);
            return ReadResult == 0 ? true : false;
        }

        /// <summary>
        /// 优雅停止连接任务并关闭资源
        /// </summary>
        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            try
            {
                if (_cts != null && !_cts.IsCancellationRequested)
                {
                    _cts.Cancel();
                }

                try
                {
                    // 等待连接任务退出（短超时以防阻塞）
                    _connectTask?.Wait(2000);
                }
                catch (AggregateException) { }
                catch (Exception) { }

                // 尝试关闭 master 连接
                try
                {
                    if (_master != null)
                    {
                        _master.Close();
                    }
                }
                catch { }
            }
            finally
            {
                _cts?.Dispose();
                _cts = null;
                _connectTask = null;
                _master = null;
            }
        }

        ~PLCControl()
        {
            Dispose();
        }
    }
}