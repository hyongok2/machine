using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.MemoryMappedFiles;
using Dit.Framework.Comm;
using System.Collections;

namespace Dit.Framework.PLC
{
    public class VirtualShare : IVirtualMem
    {
        //공유 메모리용
        public byte[] WORD_S = null;

        private MemoryMappedFile _fileS = null;
        private MemoryMappedViewAccessor _accessorS = null;

        public string MemoryName { get; set; }
        public int MemorySize { get; set; }
        public VirtualShare(string name, int byteSize)
        {
            this.MemoryName = name;
            this.MemorySize = byteSize;
        }
        public bool this[PlcAddr addr]
        {
            get
            {
                return GetBit(addr);
            }
        }
        public override int Open()
        {
            WORD_S = new byte[MemorySize];

            _fileS = MemoryMappedFile.CreateOrOpen(MemoryName, MemorySize);
            _accessorS = _fileS.CreateViewAccessor();
            return 0;
        }
        public override int Close()
        {
            _fileS.Dispose();
            _accessorS.Dispose();
            return 0;
        }
        public override int ReadFromPLC(PlcAddr addr, int size)
        {
            byte[] bytes = new byte[size];
            try
            {
                _accessorS.ReadArray<byte>(addr.Addr, bytes, 0, size);
                Array.Copy(bytes, 0, WORD_S, addr.Addr, size);
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        public override int WriteToPLC(PlcAddr addr, int size)
        {

            byte[] bytes = new byte[size];
            try
            {
                Array.Copy(WORD_S, addr.Addr, bytes, 0, size);
                _accessorS.WriteArray<byte>(addr.Addr, bytes, 0, size);
                return 0;
            }
            catch
            {
                return -1;
            }
        }
        //메소드 비트
        public override bool GetBit(PlcAddr addr)
        {
            return _accessorS.ReadByte(addr.Addr).GetBit(addr.Bit);
        }
        public override void SetBit(PlcAddr addr, bool value)
        {
            byte wb = _accessorS.ReadByte(addr.Addr).SetBit(addr.Bit, value);
            _accessorS.Write(addr.Addr, wb);
        }
        public override void SetBit(PlcAddr addr)
        {
            SetBit(addr, true);
        }
        public override void ClearBit(PlcAddr addr)
        {
            SetBit(addr, false);
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
            throw new Exception("미지정 함수");
        }
        //메소드 - STRING
        public override int SetAscii(PlcAddr addr, string text)
        {
            try
            {
                byte[] data = new byte[addr.Length];
                byte[] source = Encoding.ASCII.GetBytes(text);
                int length = source.Length < addr.Length ? source.Length : addr.Length;
                Array.Copy(source, data, length);
                _accessorS.WriteArray<byte>(addr.Addr, data, 0, data.Length);

                return 0;
            }
            catch
            {
                return -1;
            }

        }
        public override string GetAscii(PlcAddr addr)
        {
            try
            {
                byte[] data = new byte[addr.Length];
                _accessorS.ReadArray(addr.Addr, data, 0, addr.Length);
                return Encoding.ASCII.GetString(data).Replace("\0", "");
            }
            catch
            {
                return null;
            }
        }

        public byte GetByte(PlcAddr addr)
        {
            return _accessorS.ReadByte(addr.Addr);
        }
        public void SetByte(PlcAddr addr, byte value)
        {
            _accessorS.Write(addr.Addr, value);
        }

        //메소드 - SHORT      
        public override short GetShort(PlcAddr addr)
        {
            return _accessorS.ReadInt16(addr.Addr);
        }
        public override void SetShort(PlcAddr addr, short value)
        {
            _accessorS.Write(addr.Addr, value);
        }

        //메소드 - SHORT      
        public override void SetShorts(PlcAddr addr, short[] values, out int result)
        {
            try
            {
                int length = values.Length < addr.Length ? values.Length : addr.Length;
                _accessorS.WriteArray(addr.Addr, values, 0, length);

                result = 0;
            }
            catch
            {
                result = -1;
            }

        }
        public override short[] GetShorts(PlcAddr addr, int wordSize, out int result)
        {
            short[] values = new short[wordSize];
            try
            {
                _accessorS.ReadArray<short>((long)addr.Addr, values, 0, (int)values.Length);
                result = 0;
            }
            catch
            {
                result = -1;
            }
            return values;
        }

        //메소드 - INT32
        public override void SetInt32(PlcAddr addr, int value)
        {
            _accessorS.Write(addr.Addr, value);
        }
        public override int GetInt32(PlcAddr addr)
        {
            return _accessorS.ReadInt32(addr.Addr);
        }
        //메소드 - FLOAT
        private float GetFloat(PlcAddr addr)
        {
            return _accessorS.ReadSingle(addr.Addr);
        }
        private void SetFloat(PlcAddr addr, float value)
        {
            _accessorS.Write(addr.Addr, value);
        }

        //읽어온 메모리에서 읽어오는 함수.
        public override bool VirGetBit(PlcAddr addr)
        {
            return WORD_S[addr.Addr].GetBit(addr.Bit);
        }
        public override void VirSetBit(PlcAddr addr, bool value)
        {
            WORD_S[addr.Addr] = WORD_S[addr.Addr].SetBit(addr.Bit, value);
        }
        public override short VirGetShort(PlcAddr addr)
        {
            return BitConverter.ToInt16(WORD_S, addr.Addr);
        }
        public override void VirSetShort(PlcAddr addr, short value)
        {
            byte[] source = BitConverter.GetBytes(value);
            int length = source.Length < addr.Length ? source.Length : addr.Length;
            Array.Copy(source, 0, WORD_S, addr.Addr, length);   //64bit Application일대.. 오류 있음. 
        }
        public override string VirGetAscii(PlcAddr addr)
        {
            return Encoding.ASCII.GetString(WORD_S, addr.Addr, addr.Length);
        }

        public override string VirGetAsciiTrim(PlcAddr addr)
        {
            return Encoding.ASCII.GetString(WORD_S, addr.Addr, addr.Length).Trim(new char[] { '\0', '\r', '\n', '\t', ' ' });
        }

        public override void VirSetAscii(PlcAddr addr, string text)
        {
            try
            {
                byte[] data = new byte[addr.Length];
                byte[] source = Encoding.ASCII.GetBytes(text);
                int length = source.Length < addr.Length ? source.Length : addr.Length;
                Array.Copy(source, data, length);
                Array.Copy(data, 0, WORD_S, addr.Addr, data.Length);
            }
            catch
            {
            }
        }
        public override int VirGetInt32(PlcAddr addr)
        {
            return BitConverter.ToInt32(WORD_S, addr.Addr);
        }
        public override void VirSetInt32(PlcAddr addr, int value)
        {
            byte[] source = BitConverter.GetBytes(value);
            int length = source.Length < addr.Length ? source.Length : addr.Length;
            Array.Copy(source, 0, WORD_S, addr.Addr, length);
        }
        public override bool[] VirGetBits(PlcAddr addr, int wordSize)
        {
            short[] result = new short[addr.Length];
            Buffer.BlockCopy(WORD_S, addr.Addr, result, 0, addr.Length);

            bool[] resultBits = new bool[wordSize * 16];
            for (int iPos = 0; iPos < wordSize; iPos++)
            {
                short[] bits = result[iPos].GetBits();
                for (int jPos = 0; jPos < 16; jPos++)
                    resultBits[iPos * 16 + jPos] = bits[jPos] == 1;
            }

            return resultBits;
        }
        public override short[] VirGetShorts(PlcAddr addr)
        {
            short[] result = new short[addr.Length];
            Buffer.BlockCopy(WORD_S, addr.Addr, result, 0, addr.Length);
            return result;
        }

        public override void VirSetShorts(PlcAddr addr, short[] values)
        {
            Buffer.BlockCopy(values, 0, WORD_S, addr.Addr, addr.Length);
        }
        public override void VirSetBytes(PlcAddr addr, byte[] bytes)
        {
            Buffer.BlockCopy(bytes, 0, WORD_S, addr.Addr, addr.Length);
        }
        public override byte[] VirGetBytes(PlcAddr addr)
        {
            byte[] bytes = new byte[addr.Length];
            Buffer.BlockCopy(WORD_S, addr.Addr, bytes, 0, addr.Length);
            return bytes;
        }
        public override byte VirGetByte(PlcAddr addr)
        {
            return WORD_S[addr.Addr];
        }
        public override void VirSetByte(PlcAddr addr, byte value) 
        {
            WORD_S[addr.Addr] = value;
        }
        public override float VirGetFloat(PlcAddr addr)
        {
            return BitConverter.ToSingle(WORD_S, addr.Addr);
        }
        public override void VirSetFloat(PlcAddr addr, float value)
        {
            byte[] source = BitConverter.GetBytes(value);
            int length = source.Length < addr.Length ? source.Length : addr.Length;
            Array.Copy(source, 0, WORD_S, addr.Addr, length);
        }
    }
}
