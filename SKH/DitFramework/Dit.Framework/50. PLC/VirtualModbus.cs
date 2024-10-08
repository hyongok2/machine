using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Collections;
using System.IO.Ports;
using System.Threading;
using Dit.Framework.PLC;
using Dit.Framework.Comm;
using Modbus.Device;

namespace Dit.Framework.PLC
{
    public class VirtualModbus : IVirtualMem
    {
        public enum EmModbusType
        {
            TCP, 
            //UDP,
            //ASCII,
            RTU
        }

        private int PLC_MEM_SIZE = 102400;

        public bool[][] DiscreteInputs;//=  new bool[PLC_MEM_SIZE];
        public bool[][] Coil;//= new bool[PLC_MEM_SIZE];
        public short[][] InputRegisters;//= new short[PLC_MEM_SIZE];
        public short[][] HoldingRegisters;// = new short[PLC_MEM_SIZE];

        private ModbusMaster _modbusMst = null;
        private EmModbusType _modbusType = EmModbusType.TCP;
        private string _name = string.Empty;
        private int _deviceCount = 0;

        private Thread _syncWorker = null;
        private bool _useAnsyc = true;
        private int _syncTime = 5;
        private bool _running = false;

        //공용
        public int SyncTime { get; set; }
        public List<PlcAddr> SyncBlocks = new List<PlcAddr>();

        public byte DeviceID { get; set; }

        //TCP 설정 파일         
        public string Ip { get; set; }
        public int Port { get; set; }
        private TcpClient _tcp = null;

        //SERAL 설정 파일 
        private SerialPort _serialPort = null;

        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public Parity ParityBit { get; set; }
        public int DataBits { get; set; }
        public StopBits StopBit { get; set; }

        public VirtualModbus(string name, int size, EmModbusType type, bool useAnsyc = true, int syncTime = 5, int deviceCount = 1)
        {
            _modbusType = type;
            _useAnsyc = useAnsyc;
            _syncTime = syncTime;
            _name = name;
            _deviceCount = deviceCount + 1;

            DiscreteInputs = new bool[_deviceCount][];
            Coil = new bool[_deviceCount][];
            InputRegisters = new short[_deviceCount][];
            HoldingRegisters = new short[_deviceCount][];

            PLC_MEM_SIZE = size;

            for (int iPos = 0; iPos < _deviceCount; iPos++)
            {
                DiscreteInputs[iPos] = new bool[PLC_MEM_SIZE];
                Coil[iPos] = new bool[PLC_MEM_SIZE];
                InputRegisters[iPos] = new short[PLC_MEM_SIZE];
                HoldingRegisters[iPos] = new short[PLC_MEM_SIZE];
            }

        }

        //메소드 연결
        public override int Open()
        {
            int result = 0;
            try
            {
                if (_modbusType == EmModbusType.TCP)
                    result = OpenTcp("127.0.0.1", 503);
                else
                    result = OpenSerail(PortName, BaudRate, ParityBit, DataBits, StopBit);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("{0}", ex.Message));
            }

            if (result == 0)
            {
                _running = true;
                _syncWorker = new Thread(new ThreadStart(Working));
                _syncWorker.Start();
            }

            return result;
        }

        public int OpenTcp(string ip, int port)
        {
            _tcp = new System.Net.Sockets.TcpClient();
            _tcp.Connect(ip, port);
            _modbusMst = ModbusIpMaster.CreateIp(_tcp);

            IsConnected = _serialPort.IsOpen;
            return _tcp.Connected ? 0 : -1;
        }
        public int OpenSerail(string portName, int baudRate, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.None)
        {
            _serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            _modbusMst = ModbusSerialMaster.CreateRtu(_serialPort);
            _modbusMst.Transport.ReadTimeout = 2000;
            try
            {
                _serialPort.Open();
                Console.WriteLine(string.Format("{0}(PORT NUMBER : {1}) : Serial Port Open Success", _name, PortName));
                //Logger.Log.AppendLine(LogLevel.Error, string.Format("{0}(PORT NUMBER : {1}) : Serial Port Open Success", _name, PortName));
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(string.Format("{0}", ex.Message));
                //Logger.Log.AppendLine(LogLevel.Error, string.Format("{0}(PORT NUMBER : {1}) : Serial Port Open Error", _name, PortName));
            }

            IsConnected = _serialPort.IsOpen;
            return _serialPort.IsOpen ? 0 : -1;
        }
        public override int Close()
        {
            _running = false;
            if (_modbusType == EmModbusType.TCP)
            {
                _tcp.Close();
            }
            else
            {
                try
                {
                    if (_serialPort.IsOpen)
                        _serialPort.Close();
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(string.Format("{0}", ex.Message));
                }
            }
            if (_syncWorker != null)
                _syncWorker.Join();

            if (_modbusMst != null)
                _modbusMst.Dispose();

            return 0;
        }


        //메소드 - 작업
        public void Working()
        {
            DateTime workingTime = DateTime.Now;
            while (_running)
            {

                foreach (PlcAddr addr in SyncBlocks)
                    ReadFromPLC(addr, addr.Length);

                while (Math.Abs((DateTime.Now - workingTime).TotalSeconds) < _syncTime)
                    Thread.Sleep(500);
            }
        }

        //메소드 동기화
        public override int ReadFromPLC(PlcAddr addr, int wordSize)
        {
            int result = 0;
            try
            {
                if (addr.Type == PlcMemType.MD)
                {
                    bool[] currWords = GetBists(addr, wordSize, out result);
                    Array.Copy(currWords, 0, DiscreteInputs[addr.DeviceID], addr.Addr, wordSize * 16);
                    return result;
                }
                else if (addr.Type == PlcMemType.MC)
                {
                    bool[] currWords = GetBists(addr, wordSize, out result);
                    Array.Copy(currWords, 0, Coil[addr.DeviceID], addr.Addr, wordSize * 16);
                    return result;
                }
                else if (addr.Type == PlcMemType.MI)
                {
                    short[] currWords = GetShorts(addr, wordSize, out result);
                    Array.Copy(currWords, 0, InputRegisters[addr.DeviceID], addr.Addr, wordSize);
                    return result;
                }
                else if (addr.Type == PlcMemType.MH)
                {
                    short[] currWords = GetShorts(addr, wordSize, out result);
                    Array.Copy(currWords, 0, HoldingRegisters[addr.DeviceID], addr.Addr, wordSize);
                    return result;
                }
                else
                {
                    throw new Exception("미지정 메모리");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("{0}", ex.Message));
                return result;
            }
        }
        public override int WriteToPLC(PlcAddr addr, int wordSize)
        {
            try
            {
                if (addr.Type == PlcMemType.MC)
                {
                    bool[] values = new bool[wordSize * 16];
                    Array.Copy(Coil, values, wordSize * 16);
                    _modbusMst.WriteMultipleCoils(DeviceID, (ushort)addr.Addr, values);

                    return 0;
                }
                else if (addr.Type == PlcMemType.MH)
                {
                    short[] values = new short[wordSize];
                    Array.Copy(HoldingRegisters, values, wordSize);
                    ushort[] vv = values.Select(f => (ushort)f).ToArray();

                    _modbusMst.WriteMultipleRegisters(DeviceID, (ushort)addr.Length, vv);
                    return 0;
                }
                else
                {
                    throw new Exception("미지정 메모리");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("{0}", ex.Message));
                return -1;
            }
        }


        ////메소드 비트
        public override bool GetBit(PlcAddr addr)
        {
            if (addr.Type == PlcMemType.MC)
            {
                bool[] value = _modbusMst.ReadCoils(DeviceID, (ushort)addr.Addr, (ushort)1);
                return value[0] == true;
            }
            else if (addr.Type == PlcMemType.MD)
            {
                bool[] value = _modbusMst.ReadInputs(DeviceID, (ushort)addr.Addr, (ushort)1);
                return value[0] == true;
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }
        public override void SetBit(PlcAddr addr)
        {
            SetBit(addr, true);
        }
        public override void ClearBit(PlcAddr addr)
        {
            SetBit(addr, false);
        }
        public override void SetBit(PlcAddr addr, bool value)
        {
            if (addr.Type == PlcMemType.MC)
            {
                _modbusMst.WriteSingleCoil(DeviceID, (ushort)addr.Addr, value);
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }
        public override void Toggle(PlcAddr addr)
        {
            if (GetBit(addr) == true)
                ClearBit(addr);
            else
                SetBit(addr);
        }
        public override bool[] GetBists(PlcAddr addr, int wordSize, out int result)
        {
            result = 0;
            if (addr.Type == PlcMemType.MC)
            {
                bool[] value = _modbusMst.ReadCoils(DeviceID, (ushort)addr.Addr, (ushort)(wordSize * 16));
                return value;
            }
            else if (addr.Type == PlcMemType.MD)
            {
                bool[] value = _modbusMst.ReadInputs(DeviceID, (ushort)addr.Addr, (ushort)(wordSize * 16));
                return value;
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }

        ////메소드 - STRING
        public override int SetAscii(PlcAddr addr, string text)
        {
            throw new Exception("미지정 메모리");
        }

        ////메소드 - SHORT
        public override short GetShort(PlcAddr addr)
        {
            int result = 0;
            short[] readShort = GetShorts(addr, 1, out result);
            return readShort[0];
        }
        public override void SetShort(PlcAddr addr, short value)
        {
            if (addr.Type == PlcMemType.MH)
            {
                _modbusMst.WriteSingleRegister(DeviceID, (ushort)addr.Addr, (ushort)value);
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }

        ////메소드 - SHORT        
        public override short[] GetShorts(PlcAddr addr, int wordSize, out int result)
        {
            result = 0;
            if (addr.Type == PlcMemType.MI)
            {
                ushort[] value = _modbusMst.ReadInputRegisters(DeviceID, (ushort)addr.Addr, (ushort)wordSize);
                short[] vv = value.Select(f => (short)f).ToArray();
                return vv;
            }
            else if (addr.Type == PlcMemType.MH)
            {
                ushort[] value = _modbusMst.ReadHoldingRegisters(DeviceID, (ushort)addr.Addr, (ushort)wordSize);
                short[] vv = value.Select(f => (short)f).ToArray();
                return vv;
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }
        public override void SetShorts(PlcAddr addr, short[] values, out int result)
        {
            result = 0;
            if (addr.Type == PlcMemType.MH)
            {

                ushort[] value = values.Cast<ushort>().ToArray();
                _modbusMst.WriteMultipleRegisters(DeviceID, (ushort)addr.Addr, value);
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }

        //메소드 - INT32
        public override int GetInt32(PlcAddr addr)
        {
            throw new NotImplementedException();
            //return (int)GetShort(addr);
        }
        public override void SetInt32(PlcAddr addr, int value)
        {
            throw new NotImplementedException();
            //SetShort(addr, (short)value);
        }


        ////읽어온 메모리에서 읽어오는 함수.
        public override bool VirGetBit(PlcAddr addr)
        {
            if (addr.Type == PlcMemType.MD)
            {
                return DiscreteInputs[addr.DeviceID][addr.Addr];
            }
            else if (addr.Type == PlcMemType.MC)
            {
                return Coil[addr.DeviceID][addr.Addr];
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }
        public override void VirSetBit(PlcAddr addr, bool value)
        {
            if (addr.Type == PlcMemType.MC)
            {
                Coil[addr.DeviceID][addr.Addr] = value;
            }
            else if (addr.Type == PlcMemType.MD)
            {
                DiscreteInputs[addr.DeviceID][addr.Addr] = value;
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }

        public override short VirGetShort(PlcAddr addr)
        {
            if (addr.Type == PlcMemType.MI)
            {
                return InputRegisters[addr.DeviceID][addr.Addr];
            }
            else if (addr.Type == PlcMemType.MH)
            {
                return HoldingRegisters[addr.DeviceID][addr.Addr];
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }
        public override void VirSetShort(PlcAddr addr, short value)
        {

            if (addr.Type == PlcMemType.MH)
            {
                HoldingRegisters[addr.DeviceID][addr.Addr] = value;
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }

        public override string VirGetAscii(PlcAddr addr)
        {
            if (addr.Type == PlcMemType.MI)
            {
                return InputRegisters[addr.DeviceID].GetPlcAscii(addr.Addr, addr.Length);
            }
            else if (addr.Type == PlcMemType.MH)
            {
                return HoldingRegisters[addr.DeviceID].GetPlcAscii(addr.Addr, addr.Length);
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }
        public override void VirSetAscii(PlcAddr addr, string value)
        {
            if (addr.Type == PlcMemType.MH)
            {
                HoldingRegisters[addr.DeviceID].SetPlcAscii(addr.Addr, addr.Length, value);
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }

        public override int VirGetInt32(PlcAddr addr)
        {
            //throw new Exception(" 검증 필요");
            if (addr.Type == PlcMemType.MI)
            {
                return InputRegisters[addr.DeviceID].GetInt32(addr.Addr);
            }
            else if (addr.Type == PlcMemType.MH)
            {
                return HoldingRegisters[addr.DeviceID].GetInt32(addr.Addr);
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }
        public override void VirSetInt32(PlcAddr addr, int value)
        {
            //throw new Exception(" 검증 필요");
            if (addr.Type == PlcMemType.MH)
            {
                HoldingRegisters[addr.DeviceID].SetInt32(addr.Addr, value);
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }
        public override bool[] VirGetBits(PlcAddr addr, int wordSize)
        {
            bool[] values = new bool[wordSize * 16];
            if (addr.Type == PlcMemType.MD)
            {
                Array.Copy(DiscreteInputs, addr.Addr, values, 0, values.Length);
            }
            else if (addr.Type == PlcMemType.MC)
            {
                Array.Copy(Coil, addr.Addr, values, 0, values.Length);
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
            return values;
        }
        public override short[] VirGetShorts(PlcAddr addr)
        {
            short[] values = new short[addr.Length];

            if (addr.Type == PlcMemType.MI)
            {
                Array.Copy(InputRegisters[addr.DeviceID], addr.Addr, values, 0, values.Length);
            }
            else if (addr.Type == PlcMemType.MH)
            {
                Array.Copy(HoldingRegisters[addr.DeviceID], addr.Addr, values, 0, values.Length);
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
            return values;
        }

        public override float VirGetFloat(PlcAddr addr)
        {
            //throw new Exception(" 검증 필요");
            if (addr.Type == PlcMemType.MI)
            {
                byte[] bytes = new byte[4];

                bytes[0] = InputRegisters[addr.DeviceID][addr.Addr + 0].GetByte(0);
                bytes[1] = InputRegisters[addr.DeviceID][addr.Addr + 0].GetByte(1);
                bytes[2] = InputRegisters[addr.DeviceID][addr.Addr + 1].GetByte(0);
                bytes[3] = InputRegisters[addr.DeviceID][addr.Addr + 1].GetByte(1);

                return BitConverter.ToSingle(bytes, 0);
            }
            else if (addr.Type == PlcMemType.MH)
            {
                byte[] bytes = new byte[4];

                bytes[0] = HoldingRegisters[addr.DeviceID][addr.Addr + 0].GetByte(0);
                bytes[1] = HoldingRegisters[addr.DeviceID][addr.Addr + 0].GetByte(1);
                bytes[2] = HoldingRegisters[addr.DeviceID][addr.Addr + 1].GetByte(0);
                bytes[3] = HoldingRegisters[addr.DeviceID][addr.Addr + 1].GetByte(1);

                return BitConverter.ToSingle(bytes, 0);
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }
        public override void VirSetFloat(PlcAddr addr, float value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            short[] write = new short[2];

            write[0] = (short)(bytes[0] + bytes[1] * 0x100);
            write[1] = (short)(bytes[2] + bytes[3] * 0x100);

            if (addr.Type == PlcMemType.MH)
            {
                HoldingRegisters[addr.DeviceID][addr.Addr + 0] = write[0];
                HoldingRegisters[addr.DeviceID][addr.Addr + 1] = write[1];
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }
        //public void ReadDiscreteInputs()
        //{
        //    //bool[] ss = _modbusMst.ReadCoils(0 ,1,  10);

        //    _modbusMst.WriteSingleCoil(1, 5, true);
        //    _modbusMst.WriteSingleCoil(1, 6, true);

        //    bool[] resultCoil = _modbusMst.ReadCoils(1, 0, 10);
        //    bool[] resultInputs = _modbusMst.ReadInputs(1, 0, 10);

        //    ushort[] resultRead = _modbusMst.ReadInputRegisters(1, 0, 10);
        //    ushort[] resultHold = _modbusMst.ReadHoldingRegisters(1, 0, 10);

        //    ushort[] resultHoldww = new ushort[] { 1, 2, 3 };
        //    _modbusMst.WriteMultipleRegisters(1, 10, resultHoldww);

        //}
    }
}

