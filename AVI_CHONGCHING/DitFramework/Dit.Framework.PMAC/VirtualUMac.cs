using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Comm;
using Dit.Framework.PLC;

namespace Dit.Framework.PMAC
{
    public enum Em_Mt_Er
    {
        OK = 0,
        Exception = 1,
        TimeOut = 2,
        NotConnected = 3,
        Failed = 4,
        InvaildDevice = 11,
        LengthExceed = 21,
        RunningThread = 22,
    }
    public class VirtualUMac : IVirtualMem
    {
        protected int ONE_BYTE_SIZE = 4;
        protected const int MEM_SIZE = 102400;
        public byte[] BYTE_W = new byte[MEM_SIZE];
        private uint _uDeviceID = 0;
        protected string _ip = string.Empty;
        protected int _port = 0;
        public VirtualUMac(string name, string ip, int port)
        {
            _ip = ip;
            _port = port;
        }
        //메소드 연결
        public override int Open()
        {
            uint ipadress = PowerPmac.ToInt(_ip);
            _uDeviceID = PowerPmac.PPmacDprOpen(ipadress, _port);
            uint uRet = PowerPmac.PPmacDprConnect(_uDeviceID);
            return 0;
        }
        public override int Close()
        {
            PowerPmac.PPmacDprDisconnect(_uDeviceID);
            return (int)PowerPmac.PPmacDprClose(_uDeviceID);
        }

        //메소드 동기화
        public override int ReadFromPLC(PlcAddr addr, int wordSize)
        {
            uint byteStart = (uint)(addr.Addr * ONE_BYTE_SIZE);
            uint byteSize = (uint)(wordSize * ONE_BYTE_SIZE);

            IntPtr ptr1 = System.Runtime.InteropServices.Marshal.AllocHGlobal((int)byteSize);

            uint uRet = PowerPmac.PPmacDprGetDPRMem(_uDeviceID, byteStart, byteSize, ptr1);

            System.Runtime.InteropServices.Marshal.Copy(ptr1, BYTE_W, (int)byteStart, (int)byteSize);
            System.Runtime.InteropServices.Marshal.FreeHGlobal(ptr1);

            //bool b5000 = VirGetBit(new PlcAddr(PlcMemType.X, 5000, 0));
            //bool b5001 = VirGetBit(new PlcAddr(PlcMemType.X, 5001, 0));
            //bool b5002 = VirGetBit(new PlcAddr(PlcMemType.X, 5002, 0));
            //bool b5003 = VirGetBit(new PlcAddr(PlcMemType.X, 5003, 0));
            //float f6612 = VirGetFloat(new PlcAddr(PlcMemType.X, 6612, 0));

            return (int)uRet;
        }
        public override int WriteToPLC(PlcAddr addr, int wordSize)
        {
            uint byteStart = (uint)(addr.Addr * ONE_BYTE_SIZE);
            uint byteSize = (uint)(wordSize * ONE_BYTE_SIZE);

            IntPtr ptr1 = System.Runtime.InteropServices.Marshal.AllocHGlobal((int)byteSize);

            System.Runtime.InteropServices.Marshal.Copy(BYTE_W, (int)byteStart, ptr1, (int)byteSize);

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
            if (addr.Type == PlcMemType.P)
            {
                int intValue = VirGetInt32(addr);
                return (intValue != 0);
            }
            else
            {
                //jys:: addr 무조건 int변환. 필요시, 변환 자료형 확인 필요            
                return VirGetInt32(addr).GetBit(addr.Bit);
            }
        }
        public override void VirSetBit(PlcAddr addr, bool value)
        {
            if (addr.Type == PlcMemType.P)
            {
                VirSetInt32(addr, value ? 1 : 0);
            }
            else
            {
                int result = VirGetInt32(addr).SetBit(addr.Bit, value);
                VirSetInt32(addr, result);
            }
        }

        public override short VirGetShort(PlcAddr addr)
        {
            throw new Exception("미구현 메모리");
        }
        public override  void VirSetShort(PlcAddr addr, short value)
        {
            throw new Exception("미구현 메모리");
        }

        public override  string VirGetAscii(PlcAddr addr)
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
            Array.Copy(BYTE_W, addr.Addr * 4, intbyte, 0, 4);

            intbyte = Swap4Byte(intbyte);
            return BitConverter.ToInt32(intbyte, 0);
        }
        public override void VirSetInt32(PlcAddr addr, int value)
        {
            byte[] intbyte = BitConverter.GetBytes(value);
            intbyte = Swap4Byte(intbyte);

            Array.Copy(intbyte, 0, BYTE_W, addr.Addr * 4, 4);
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
            Array.Copy(BYTE_W, addr.Addr * 4, intbyte, 0, 4);

            intbyte = Swap4Byte(intbyte);
            return BitConverter.ToSingle(intbyte, 0);
        }
        public override  void VirSetFloat(PlcAddr addr, float value)
        {
            byte[] intbyte = BitConverter.GetBytes(value);
            intbyte = Swap4Byte(intbyte);
            Array.Copy(intbyte, 0, BYTE_W, addr.Addr * 4, 4);
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
