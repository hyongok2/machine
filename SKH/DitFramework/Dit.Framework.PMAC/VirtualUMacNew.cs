using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Comm;
using Dit.Framework.PLC;

namespace Dit.Framework.PMAC
{
    public enum Em_Mt_Er_New
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
    public class VirtualUMacNew : IVirtualMem
    {
        private int ONE_BYTE_SIZE = 4;
        private const int MEM_SIZE = 102400;
        public byte[] BYTE_W = new byte[MEM_SIZE];

        public int INT_VALUE = 0;
        public float FLOAT_VALUE = 0;
        
        private uint _uDeviceID = 0;
        private string _ip = string.Empty;
        private uint _port = 0;
        public VirtualUMacNew(string name, string ip, int port)
        {
            _ip = ip;
            _port = (uint)port;
        }
        //메소드 연결
        public override int Open()
        {
            uint ipadress = PowerPmacNew.ToInt(_ip);
            _uDeviceID = PowerPmacNew.DTKPowerPmacOpen(ipadress, _port);
            uint uRet = PowerPmacNew.DTKConnect(_uDeviceID);
            return 0;
        }
        public uint Close()
        {
            PowerPmacNew.DTKDisconnect(_uDeviceID);
            return PowerPmacNew.DTKPowerPmacClose(_uDeviceID);
        }

        //메소드 동기화
        public override int ReadFromPLC(PlcAddr addr, int wordSize)
        {
            uint byteStart = (uint)(addr.Addr * ONE_BYTE_SIZE);
            int byteSize = (int)(wordSize * ONE_BYTE_SIZE);

            SByte[] pV = new SByte[byteSize];

            uint uRet = PowerPmacNew.DTKGetUserMem(_uDeviceID, byteStart, byteSize, pV);
            
            byte[] BYTE_NEW = new byte[MEM_SIZE];
            BYTE_NEW = Array.ConvertAll<SByte, byte>(pV, f => (byte)f);

            Array.Copy(BYTE_NEW, 0, BYTE_W, byteStart, byteSize);

            return (int)uRet;
        }
        public override int WriteToPLC(PlcAddr addr, int wordSize)
        {
            //uint byteStart = (uint)(addr.Addr * ONE_BYTE_SIZE);
            //int byteSize = (int)(wordSize * ONE_BYTE_SIZE);

            //SByte[] pV = new SByte[MEM_SIZE];

            //pV = Array.ConvertAll<byte, SByte>(BYTE_W, f => (SByte)f);

            //uint uRet = PowerPmacNew.DTKSetUserMem(_uDeviceID, byteStart, byteSize, pV);
            //PowerPmacNew.DTKSetUserMemFloat(_uDeviceID, 20404, a);

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
            Array.Copy(BYTE_W, addr.Addr * 4, intbyte, 0, 4);

            //intbyte = Swap4Byte(intbyte);
            return BitConverter.ToInt32(intbyte, 0);
        }
        public override void VirSetInt32(PlcAddr addr, int value)
        {
            byte[] intbyte = BitConverter.GetBytes(value);
            uint byteStart = (uint)(addr.Addr * ONE_BYTE_SIZE);
            
            if (VirGetInt32(addr) != value)
            {
                Array.Copy(intbyte, 0, BYTE_W, addr.Addr * 4, 4);
                PowerPmacNew.DTKSetUserMemInteger(_uDeviceID, byteStart, value);
            }
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

            //intbyte = Swap4Byte(intbyte);
            return BitConverter.ToSingle(intbyte, 0);
        }
        public override void VirSetFloat(PlcAddr addr, float value)
        {
            byte[] intbyte = BitConverter.GetBytes(value);
            uint byteStart = (uint)(addr.Addr * ONE_BYTE_SIZE);

            if (VirGetFloat(addr) != value)
            {
                Array.Copy(intbyte, 0, BYTE_W, addr.Addr * 4, 4);
                PowerPmacNew.DTKSetUserMemFloat(_uDeviceID, byteStart, value);
            }
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
