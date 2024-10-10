using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dit.Framework.PLC
{
    public enum SyncType
    {
        READ,
        WRITE
    }
    public class IVirtualMem
    {
        protected int TRUE = 1;
        protected int FALSE = 0;

        public IVirtualMem()
        {

        }
        public string Name { get; set; }

        //메소드 연결
        public virtual int Open()
        {
            return 0;
        }
        public virtual int Close()
        {
            return 0;
        }

        //메소드 동기화
        public virtual int ReadFromPLC(PlcAddr addr, int wordSize) { return 0; }
        public virtual int ReadFromMEM(PlcAddr addr, int wordSize) { return 0; }
        public virtual int WriteToPLC(PlcAddr addr, int wordSize) { return 0; }
        public virtual int WriteToMEM(PlcAddr addr, int wordSize) { return 0; }

        //메소드 비트
        public virtual bool GetBit(PlcAddr addr) { return false; }
        public virtual void SetBit(PlcAddr addr) { }
        public virtual void ClearBit(PlcAddr addr) { }
        public virtual void SetBit(PlcAddr addr, bool value) { }
        public virtual void Toggle(PlcAddr addr) { }
        public virtual bool[] GetBists(PlcAddr addr, int wordSize, out int result) { result = 0; return new bool[0]; }

        //메소드 - STRING
        public virtual int SetAscii(PlcAddr addr, string text) { return 0; }
        public virtual string GetAscii(PlcAddr addr) { return string.Empty; }

        //메소드 - SHORT
        public virtual short GetShort(PlcAddr addr) { return 0; }
        public virtual void SetShort(PlcAddr addr, short value) { }

        //메소드 - SHORT      
        public virtual void SetShorts(PlcAddr addr, short[] values, out int result) { result = 0; }
        public virtual short[] GetShorts(PlcAddr addr, int wordSize, out int result) { result = 0; return new short[0]; }

        //메소드 - INT32
        public virtual int GetInt32(PlcAddr addr) { return 0; }
        public virtual void SetInt32(PlcAddr addr, int value) { }

        //메소드 - INT32s
        public virtual void SetInt32s(PlcAddr addr, int[] values, out int result) { result = 0; }
        public virtual int[] GetInt32s(PlcAddr addr, int wordSize, out int result) { result = 0; return new int[0]; }

        //읽어온 메모리에서 읽어오는 함수.
        public virtual bool VirGetBit(PlcAddr addr) { return false; }
        public virtual void VirSetBit(PlcAddr addr, bool value) { }

        public virtual byte VirGetByte(PlcAddr addr) { return 0; }
        public virtual void VirSetByte(PlcAddr addr, byte value) { }

        public virtual short VirGetShort(PlcAddr addr) { return 0; }
        public virtual void VirSetShort(PlcAddr addr, short value) { }

        public virtual string VirGetAscii(PlcAddr addr) { return string.Empty; }
        public virtual void VirSetAscii(PlcAddr addr, string value) { }
        public virtual string VirGetAsciiTrim(PlcAddr addr)
        {
            throw new NotImplementedException();
        }
        public virtual int VirGetInt32(PlcAddr addr) { return 0; }
        public virtual void VirSetInt32(PlcAddr addr, int value) { }

        public virtual bool[] VirGetBits(PlcAddr addr, int wordSize) { return new bool[0]; }

        public virtual short[] VirGetShorts(PlcAddr addr) { return new short[0]; }
        public virtual void VirSetShorts(PlcAddr addr, short[] values) { }

        public virtual byte[] VirGetBytes(PlcAddr addr) { return new byte[1]; }
        public virtual void VirSetBytes(PlcAddr addr, byte[] bytes) { }        

        public virtual float VirGetFloat(PlcAddr addr) { return 0f; }
        public virtual void VirSetFloat(PlcAddr addr, float value) { }

        public void SetBytes(PlcAddr plcAddr, byte[] alarmbytes, out int result)
        {
            result = 0;
        }
        public virtual void SetBytes(PlcAddr addr, byte[] bytes)
        {
            //throw new NotImplementedException();
        }        
        //ACS
        public virtual void BufferCompile(int bufIdx) {}
        public virtual void BufferCompile(int masterBufIdx, int slaveBufIdx) { }
        public virtual bool IsBufferCompileComplete(int bufIdx) { return true; }
        public virtual bool IsBufferCompileComplete(int masterBufIdx, int slaveBufIdx) { return true; }
        public virtual void RunBuffer(int bufIdx) { }
        public virtual void RunBuffer(int masterBufIdx, int slaveBufIdx) { }

        //modbus
        public virtual bool IsConnected { get; set; }
    }
}
