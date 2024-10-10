using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;
using Dit.Framework.PLC;

namespace Dit.Framework.PMAC
{
    public class VirtualUMacAsync : IVirtualMem
    {
        private object _criticalSection = new object();

        private int ONE_BYTE_SIZE = 4;
        private const int MEM_SIZE = 102400;

        private uint _uDeviceID = 0;
        private string _ip = string.Empty;
        private int _port = 0;
        private bool _running = false;
        private BackgroundWorker _memoryCopyWorker = new BackgroundWorker();
        private Stopwatch _stopwatch = new Stopwatch();

        public byte[] BYTE_P = new byte[MEM_SIZE];
        public byte[] BYTE_U2P = new byte[MEM_SIZE];

        //프로퍼티
        public int Interval { get; set; }

        public DateTime UpdateTime { get; set; }
        public long WorkCurrTime { get; set; }
        public long WorkMaxTime { get; set; }
        public long WorkMinTime { get; set; }
        public long WorkAvgTime { get; set; }
        public List<PlcAddr> LstReadAddr { get; set; }
        public List<PlcAddr> LstWriteAddr { get; set; }

        public VirtualUMacAsync(string name, string ip, int port)
        {
            _ip = ip;
            _port = port;

            LstReadAddr = new List<PlcAddr>();
            LstWriteAddr = new List<PlcAddr>();

            _memoryCopyWorker.DoWork += new DoWorkEventHandler(_memoryCopyWorker_DoWork);
        }

        //메소드 연결
        public override int Open()
        {
            uint ipadress = PowerPmac.ToInt(_ip);
            _uDeviceID = PowerPmac.PPmacDprOpen(ipadress, _port);
            PowerPmac.PPmacDprConnect(_uDeviceID);
            _running = true;
            _memoryCopyWorker.RunWorkerAsync();

            return 0;
        }
        public uint Close()
        {
            _running = false;
            return PowerPmac.PPmacDprClose(_uDeviceID);
        }

        private void _memoryCopyWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (_running)
            {
                _stopwatch.Restart();
                Thread.Sleep(Interval);

                foreach (PlcAddr readAddr in LstReadAddr)
                {
                    lock (_criticalSection)
                    {
                        ReadFromUmac(readAddr);
                    }
                }
                foreach (PlcAddr writeAddr in LstWriteAddr)
                {
                    lock (_criticalSection)
                    {
                        WriteToUmac(writeAddr);
                    }
                }
                _stopwatch.Stop();

                WorkCurrTime = _stopwatch.ElapsedMilliseconds;

                WorkMinTime = Math.Min(WorkMinTime, WorkCurrTime);
                WorkMaxTime = Math.Max(WorkMaxTime, WorkCurrTime);
                WorkAvgTime = (WorkAvgTime + WorkCurrTime) / 2;
                UpdateTime = DateTime.Now;

            }
        }

        //메소드 동기화
        public override int ReadFromPLC(PlcAddr addr, int wordSize)
        {
            uint byteStart = (uint)(addr.Addr * ONE_BYTE_SIZE);
            uint byteSize = (uint)(wordSize * ONE_BYTE_SIZE);

            lock (_criticalSection)
                Array.Copy(BYTE_U2P, byteStart, BYTE_P, byteStart, byteSize);

            int result = 0;
            return result;

        }
        public override int WriteToPLC(PlcAddr addr, int wordSize)
        {
            uint byteStart = (uint)(addr.Addr * ONE_BYTE_SIZE);
            uint byteSize = (uint)(wordSize * ONE_BYTE_SIZE);

            lock (_criticalSection)
                Array.Copy(BYTE_P, byteStart, BYTE_U2P, byteStart, byteSize);

            return 0;
        }

        //메소드 동기화
        private int ReadFromUmac(PlcAddr addr)
        {
            uint byteStart = (uint)(addr.Addr * ONE_BYTE_SIZE);
            uint byteSize = (uint)(addr.Length * ONE_BYTE_SIZE);

            IntPtr ptr1 = System.Runtime.InteropServices.Marshal.AllocHGlobal((int)byteSize);
            PowerPmac.PPmacDprGetDPRMem(_uDeviceID, byteStart, byteSize, ptr1);

            lock (_criticalSection)
                System.Runtime.InteropServices.Marshal.Copy(ptr1, BYTE_U2P, (int)byteStart, (int)byteSize);

            System.Runtime.InteropServices.Marshal.FreeHGlobal(ptr1);

            int result = 0;
            return result;

        }
        private int WriteToUmac(PlcAddr addr)
        {
            uint byteStart = (uint)(addr.Addr * ONE_BYTE_SIZE);
            uint byteSize = (uint)(addr.Length * ONE_BYTE_SIZE);

            IntPtr ptr1 = System.Runtime.InteropServices.Marshal.AllocHGlobal((int)byteSize);

            lock (_criticalSection)
                System.Runtime.InteropServices.Marshal.Copy(BYTE_U2P, (int)byteStart, ptr1, (int)byteSize);

            PowerPmac.PPmacDprSetDPRMem(_uDeviceID, byteStart, byteSize, ptr1);
            System.Runtime.InteropServices.Marshal.FreeHGlobal(ptr1);

            return 0;
        }


        //메소드 비트
        public override bool GetBit(PlcAddr addr)
        {
            throw new Exception("미 구현");
        }
        public override void SetBit(PlcAddr addr)
        {
            throw new Exception("미 구현");
        }
        public override void ClearBit(PlcAddr addr)
        {
            throw new Exception("미 구현");
        }
        public override void SetBit(PlcAddr addr, bool value)
        {
            throw new Exception("미 구현");
        }
        public override void Toggle(PlcAddr addr)
        {
            throw new Exception("미 구현");
        }
        public bool[] GetBists(PlcAddr addr, int wordSize, out int result)
        {
            short[] getWords = new short[wordSize];
            bool[] getBool = new bool[wordSize * 16];

            throw new Exception("미 구현");
            //return getBool;
        }
        //메소드 - STRING
        public override int SetAscii(PlcAddr addr, string text)
        {
            //throw new Exception("미 구현");
            return 0;
        }
        //메소드 - SHORT
        public override short GetShort(PlcAddr addr)
        {
            //throw new Exception("미 구현");
            return 0;
        }
        public override void SetShort(PlcAddr addr, short value)
        {
            throw new Exception("미 구현");
        }
        //메소드 - SHORT        
        public override short[] GetShorts(PlcAddr addr, int wordSize, out int result)
        {
            throw new Exception("미 구현");
        }
        public override void SetShorts(PlcAddr addr, short[] values, out int result)
        {
            throw new Exception("미 구현");
        }

        //메소드 - INT32
        public override int GetInt32(PlcAddr addr)
        {
            throw new Exception("미 구현");
        }
        public override void SetInt32(PlcAddr addr, int value)
        {
            throw new Exception("미 구현");
        }

        //읽어온 메모리에서 읽어오는 함수.
        public override bool VirGetBit(PlcAddr addr)
        {
            int intValue = VirGetInt32(addr);

            return (intValue != 0);
        }
        public override void VirSetBit(PlcAddr addr, bool value)
        {
            VirSetInt32(addr, value ? 1 : 0);
        }

        public override short VirGetShort(PlcAddr addr)
        {
            throw new Exception("미구현 메모리");
        }
        public override void VirSetShort(PlcAddr addr, short value)
        {
            throw new Exception("미구현 메모리");
        }
        public override string VirGetAscii(PlcAddr addr)
        {
            throw new Exception("미구현 메모리");
        }
        public override void VirSetAscii(PlcAddr addr, string value)
        {
            throw new Exception("미구현 메모리");
        }
        public override int VirGetInt32(PlcAddr addr)
        {
            byte[] intbyte = new byte[4];
            Array.Copy(BYTE_P, addr.Addr * 4, intbyte, 0, 4);

            intbyte = Swap4Byte(intbyte);
            return BitConverter.ToInt32(intbyte, 0);
        }
        public override void VirSetInt32(PlcAddr addr, int value)
        {
            byte[] intbyte = BitConverter.GetBytes(value);
            intbyte = Swap4Byte(intbyte);

            Array.Copy(intbyte, 0, BYTE_P, addr.Addr * 4, 4);
        }

        public override bool[] VirGetBits(PlcAddr addr, int wordSize)
        {
            throw new Exception("미구현 메모리");
        }
        public override short[] VirGetShorts(PlcAddr addr)
        {
            throw new Exception("미구현 메모리");
        }
        public override float VirGetFloat(PlcAddr addr)
        {
            byte[] intbyte = new byte[4];
            Array.Copy(BYTE_P, addr.Addr * 4, intbyte, 0, 4);

            intbyte = Swap4Byte(intbyte);
            return BitConverter.ToSingle(intbyte, 0);
        }
        public override void VirSetFloat(PlcAddr addr, float value)
        {
            byte[] intbyte = BitConverter.GetBytes(value);
            intbyte = Swap4Byte(intbyte);
            Array.Copy(intbyte, 0, BYTE_P, addr.Addr * 4, 4);
        }

        public byte[] Swap4Byte(byte[] bytes)
        {
            byte[] swapBytes = new byte[4];

            swapBytes[0] = bytes[3];
            swapBytes[3] = bytes[0];

            swapBytes[1] = bytes[2];
            swapBytes[2] = bytes[1];

            return swapBytes;
        }
    }
}
