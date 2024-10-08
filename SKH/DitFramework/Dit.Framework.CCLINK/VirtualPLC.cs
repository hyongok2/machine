using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.MemoryMappedFiles;
using Dit.Framework.Comm;

namespace Dit.Framework.PLC
{
    public class VirtualPlc : IVirtualMem
    {
        private const int PLC_MEM_SIZE = 102400;

        public string Name { get; set; }

        public bool[] WORD_X = new bool[PLC_MEM_SIZE];
        public bool[] WORD_Y = new bool[PLC_MEM_SIZE];
        public bool[] WORD_B = new bool[PLC_MEM_SIZE];
        public short[] WORD_R = new short[PLC_MEM_SIZE];
        public short[] WORD_D = new short[PLC_MEM_SIZE];
        public short[] WORD_W = new short[PLC_MEM_SIZE];
        public short[] WORD_ZR = new short[PLC_MEM_SIZE];
        public short[] WORD_Ww = new short[PLC_MEM_SIZE];
        public short[] WORD_Wr = new short[PLC_MEM_SIZE];

        public VirtualPlc(AxActEasyIF axActEasyIF, string name)
        {
            Name = name;
            _axActEasyIF = axActEasyIF;
        }
        public bool this[PlcAddr addr]
        {
            get
            {
                return VirGetBit(addr);
            }
        }

        //메소드 연결
        public override int Open()
        {
            return _axActEasyIF.Open();
        }
        public override int Close()
        {
            return _axActEasyIF.Close();
        }

        //메소드 동기화
        public override int ReadFromPLC(PlcAddr addr, int wordSize)
        {
            int result = 0;
            if (addr.Type == PlcMemType.B)
            {
                bool[] currWords = GetBists(addr, wordSize, out result);
                Array.Copy(currWords, 0, WORD_B, addr.Addr, wordSize * 16);

                return result;
            }
            else if (addr.Type == PlcMemType.X)
            {
                bool[] currWords = GetBists(addr, wordSize, out result);
                Array.Copy(currWords, 0, WORD_X, addr.Addr, wordSize * 16);
                return result;
            }
            else if (addr.Type == PlcMemType.Y)
            {
                bool[] currWords = GetBists(addr, wordSize, out result);
                Array.Copy(currWords, 0, WORD_Y, addr.Addr, wordSize * 16);
                return result;
            }
            else if (addr.Type == PlcMemType.R)
            {
                short[] currWords = GetShorts(addr, wordSize, out result);
                Array.Copy(currWords, 0, WORD_R, addr.Addr, wordSize);
                return result;
            }
            else if (addr.Type == PlcMemType.D)
            {
                short[] currWords = GetShorts(addr, wordSize, out result);
                Array.Copy(currWords, 0, WORD_D, addr.Addr, wordSize);
                return result;
            }
            else if (addr.Type == PlcMemType.ZR)
            {
                short[] currWords = GetShorts(addr, wordSize, out result);
                Array.Copy(currWords, 0, WORD_ZR, addr.Addr, wordSize);
                return result;
            }
            else if (addr.Type == PlcMemType.W)
            {
                short[] currWords = GetShorts(addr, wordSize, out result);
                Array.Copy(currWords, 0, WORD_W, addr.Addr, wordSize);
                return result;
            }
            else if (addr.Type == PlcMemType.Wr)
            {
                short[] currWords = GetShorts(addr, wordSize, out result);
                Array.Copy(currWords, 0, WORD_Wr, addr.Addr, wordSize);
                return result;
            }
            else if (addr.Type == PlcMemType.Ww)
            {
                short[] currWords = GetShorts(addr, wordSize, out result);
                Array.Copy(currWords, 0, WORD_Ww, addr.Addr, wordSize);
                return result;
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }
        public override int WriteToPLC(PlcAddr addr, int wordSize)
        {
            int result = 0;
            if (addr.Type == PlcMemType.B)
            {
                string strAddr = addr.GetPlcAddressWordString();
                short[] shorts = WORD_B.GetShorts(addr.Addr, wordSize);
                result = _axActEasyIF.WriteDeviceBlock2(strAddr, wordSize, ref shorts[0]);

                return result;
            }
            else if (addr.Type == PlcMemType.X)
            {
                string strAddr = addr.GetPlcAddressWordString();
                short[] shorts = WORD_X.GetShorts(addr.Addr, wordSize);
                result = _axActEasyIF.WriteDeviceBlock2(strAddr, wordSize, ref shorts[0]);
                return result;
            }
            else if (addr.Type == PlcMemType.Y)
            {
                string strAddr = addr.GetPlcAddressWordString();
                short[] shorts = WORD_Y.GetShorts(addr.Addr, wordSize);
                result = _axActEasyIF.WriteDeviceBlock2(strAddr, wordSize, ref shorts[0]);

                return result;
            }
            else if (addr.Type == PlcMemType.R)
            {
                string strAddr = addr.GetPlcAddressWordString();
                result = _axActEasyIF.WriteDeviceBlock2(strAddr, wordSize, ref WORD_R[addr.Addr]);
                return result;
            }
            else if (addr.Type == PlcMemType.D)
            {
                string strAddr = addr.GetPlcAddressWordString();
                result = _axActEasyIF.WriteDeviceBlock2(strAddr, wordSize, ref WORD_D[addr.Addr]);
                return result;
            }
            else if (addr.Type == PlcMemType.ZR)
            {
                string strAddr = addr.GetPlcAddressWordString();
                result = _axActEasyIF.WriteDeviceBlock2(strAddr, wordSize, ref WORD_ZR[addr.Addr]);
                return result;
            }
            else if (addr.Type == PlcMemType.W)
            {
                string strAddr = addr.GetPlcAddressWordString();
                result = _axActEasyIF.WriteDeviceBlock2(strAddr, wordSize, ref WORD_W[addr.Addr]);
                return result;
            }
            else if (addr.Type == PlcMemType.Wr)
            {
                string strAddr = addr.GetPlcAddressWordString();
                result = _axActEasyIF.WriteDeviceBlock2(strAddr, wordSize, ref WORD_W[addr.Addr]);
                return result;
            }
            else if (addr.Type == PlcMemType.Ww)
            {
                string strAddr = addr.GetPlcAddressWordString();
                result = _axActEasyIF.WriteDeviceBlock2(strAddr, wordSize, ref WORD_Ww[addr.Addr]);
                return result;
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }

        //메소드 비트
        public override bool GetBit(PlcAddr addr)
        {
            string strAddr = addr.GetPlcAddressBitString();
            short getbit = 0;
            int result = _axActEasyIF.GetDevice2(strAddr, out getbit);
            return (getbit == 1);
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
            string strAddr = addr.GetPlcAddressBitString();
            short vv = value ? (short)1 : (short)0;
            int result = _axActEasyIF.SetDevice2(strAddr, vv);
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
            short[] getWords = new short[wordSize];
            bool[] getBool = new bool[wordSize * 16];

            string strAddr = addr.GetPlcAddressBitString();
            result = _axActEasyIF.ReadDeviceBlock2(strAddr, wordSize, out  getWords[0]);

            for (int iPos = 0; iPos < wordSize; iPos++)
            {
                short[] bits = getWords[iPos].GetBits();
                for (int jPos = 0; jPos < 16; jPos++)
                    getBool[iPos * 16 + jPos] = bits[jPos] == 1;
            }
            return getBool;
        }

        //메소드 - STRING
        public override int SetAscii(PlcAddr addr, string text)
        {
            if (string.IsNullOrEmpty(text))
                return 0;

            byte[] bytes = Encoding.ASCII.GetBytes(text + "\0");
            short[] data = new short[bytes.Length / 2];
            int sendSize = data.Length;

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (short)(bytes[i * 2 + 1] * 0x100 + bytes[i * 2]);
            }

            string strAddr = addr.GetPlcAddressWordString();
            int result = _axActEasyIF.WriteDeviceBlock2(strAddr, sendSize, ref data[0]);
            return result;
        }

        //메소드 - SHORT
        public override short GetShort(PlcAddr addr)
        {
            int result = 0;
            string strAddr = addr.GetPlcAddressBitString();
            short[] readShort = GetShorts(addr, 1, out result);
            return readShort[0];
        }
        public override void SetShort(PlcAddr addr, short value)
        {
            string strAddr = addr.GetPlcAddressWordString();
            int result = _axActEasyIF.SetDevice2(strAddr, value);
        }

        //메소드 - SHORT        
        public override short[] GetShorts(PlcAddr addr, int wordSize, out int result)
        {
            short[] getwords = new short[wordSize];
            string strAddr = addr.GetPlcAddressWordString();
            result = _axActEasyIF.ReadDeviceBlock2(strAddr, wordSize, out  getwords[0]);
            return getwords;
        }
        public override void SetShorts(PlcAddr addr, short[] values, out int result)
        {
            short[] writeValues = new short[addr.Length];
            int size = values.Length > addr.Length ? addr.Length : values.Length;
            Array.Copy(values, writeValues, size);

            string strAddr = addr.GetPlcAddressWordString();
            result = _axActEasyIF.WriteDeviceBlock2(strAddr, addr.Length, ref writeValues[0]);
        }

        //메소드 - INT32
        public override int GetInt32(PlcAddr addr)
        {
            return (int)GetShort(addr);
        }
        public override void SetInt32(PlcAddr addr, int value)
        {
            SetShort(addr, (short)value);
        }


        //읽어온 메모리에서 읽어오는 함수.
        public override bool VirGetBit(PlcAddr addr)
        {
            if (addr.Type == PlcMemType.B)
            {
                return WORD_B[addr.Addr];
            }
            else if (addr.Type == PlcMemType.X)
            {
                return WORD_X[addr.Addr];
            }
            else if (addr.Type == PlcMemType.Y)
            {
                return WORD_Y[addr.Addr];
            }
            else if (addr.Type == PlcMemType.R)
            {
                short word = WORD_R[addr.Addr];
                return word.GetBit(addr.Bit);
            }
            else if (addr.Type == PlcMemType.D)
            {
                short word = WORD_D[addr.Addr];
                return word.GetBit(addr.Bit);
            }
            else if (addr.Type == PlcMemType.ZR)
            {
                short word = WORD_ZR[addr.Addr];
                return word.GetBit(addr.Bit);
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }
        public override void VirSetBit(PlcAddr addr, bool value)
        {
            if (addr.Type == PlcMemType.B)
            {
                WORD_B[addr.Addr] = value;
            }
            else if (addr.Type == PlcMemType.X)
            {
                WORD_X[addr.Addr] = value;
            }
            else if (addr.Type == PlcMemType.Y)
            {
                WORD_Y[addr.Addr] = value;
            }
            else if (addr.Type == PlcMemType.R)
            {
                short word = WORD_R[addr.Addr];
                short nBit = (short)(1 << addr.Bit);
                WORD_R[addr.Addr] = WORD_R[addr.Addr].SetBit(nBit, value);
            }
            else if (addr.Type == PlcMemType.D)
            {
                short word = WORD_D[addr.Addr];
                short nBit = (short)(1 << addr.Bit);
                WORD_R[addr.Addr] = WORD_R[addr.Addr].SetBit(nBit, value);
            }
            else if (addr.Type == PlcMemType.ZR)
            {
                short word = WORD_ZR[addr.Addr];
                short nBit = (short)(1 << addr.Bit);
                WORD_R[addr.Addr] = WORD_R[addr.Addr].SetBit(nBit, value);
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }

        public override short VirGetShort(PlcAddr addr)
        {
            if (addr.Type == PlcMemType.R)
            {
                return WORD_R[addr.Addr];
            }
            else if (addr.Type == PlcMemType.D)
            {
                return WORD_D[addr.Addr];
            }
            else if (addr.Type == PlcMemType.ZR)
            {
                return WORD_ZR[addr.Addr];
            }
            else if (addr.Type == PlcMemType.W)
            {
                return WORD_W[addr.Addr];
            }
            else if (addr.Type == PlcMemType.Wr)
            {
                return WORD_Wr[addr.Addr];
            }
            else if (addr.Type == PlcMemType.Ww)
            {
                return WORD_Ww[addr.Addr];
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }
        public override void VirSetShort(PlcAddr addr, short value)
        {
            if (addr.Type == PlcMemType.R)
            {
                WORD_R[addr.Addr] = value;
            }
            else if (addr.Type == PlcMemType.D)
            {
                WORD_D[addr.Addr] = value;
            }
            else if (addr.Type == PlcMemType.ZR)
            {
                WORD_ZR[addr.Addr] = value;
            }
            else if (addr.Type == PlcMemType.W)
            {
                WORD_W[addr.Addr] = value;
            }
            else if (addr.Type == PlcMemType.Ww)
            {
                WORD_Ww[addr.Addr] = value;
            }
            else if (addr.Type == PlcMemType.Wr)
            {
                WORD_Wr[addr.Addr] = value;
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }

        public override string VirGetAscii(PlcAddr addr)
        {
            if (addr.Type == PlcMemType.R)
            {
                return WORD_R.GetPlcAscii(addr.Addr, addr.Length);
            }
            else if (addr.Type == PlcMemType.D)
            {
                return WORD_D.GetPlcAscii(addr.Addr, addr.Length);
            }
            else if (addr.Type == PlcMemType.ZR)
            {
                return WORD_ZR.GetPlcAscii(addr.Addr, addr.Length);
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }
        public override void VirSetAscii(PlcAddr addr, string value)
        {
            if (addr.Type == PlcMemType.R)
            {
                WORD_R.SetPlcAscii(addr.Addr, addr.Length, value);
            }
            else if (addr.Type == PlcMemType.D)
            {
                WORD_D.SetPlcAscii(addr.Addr, addr.Length, value);
            }
            else if (addr.Type == PlcMemType.ZR)
            {
                WORD_ZR.SetPlcAscii(addr.Addr, addr.Length, value);
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }
        public override string VirGetAsciiTrim(PlcAddr addr)
        {
            return VirGetAscii(addr);
        }

        public override int VirGetInt32(PlcAddr addr)
        {
            //throw new Exception(" 검증 필요");
            if (addr.Type == PlcMemType.R)
            {
                return WORD_R.GetInt32(addr.Addr);
            }
            else if (addr.Type == PlcMemType.D)
            {
                return WORD_D.GetInt32(addr.Addr);
            }
            else if (addr.Type == PlcMemType.ZR)
            {
                return WORD_ZR.GetInt32(addr.Addr);
            }
            else if (addr.Type == PlcMemType.W)
            {
                return WORD_W.GetInt32(addr.Addr);
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }
        public override void VirSetInt32(PlcAddr addr, int value)
        {
            //throw new Exception(" 검증 필요");
            if (addr.Type == PlcMemType.R)
            {
                WORD_R.SetInt32(addr.Addr, value);
            }
            else if (addr.Type == PlcMemType.D)
            {
                WORD_D.SetInt32(addr.Addr, value);
            }
            else if (addr.Type == PlcMemType.ZR)
            {
                WORD_ZR.SetInt32(addr.Addr, value);
            }
            else if (addr.Type == PlcMemType.W)
            {
                WORD_W.SetInt32(addr.Addr, value);
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }

        public override bool[] VirGetBits(PlcAddr addr, int wordSize)
        {
            bool[] resultBits = new bool[wordSize * 16];
            if (addr.Type == PlcMemType.R)
            {
                for (int iPos = 0; iPos < wordSize; iPos++)
                {
                    short[] bits = WORD_R[addr.Addr + iPos].GetBits();
                    for (int jPos = 0; jPos < 16; jPos++)
                        resultBits[iPos * 16 + jPos] = bits[jPos] == 1;
                }
            }
            else if (addr.Type == PlcMemType.D)
            {
                for (int iPos = 0; iPos < wordSize; iPos++)
                {
                    short[] bits = WORD_D[addr.Addr + iPos].GetBits();
                    for (int jPos = 0; jPos < 16; jPos++)
                        resultBits[iPos * 16 + jPos] = bits[jPos] == 1;
                }
            }
            else if (addr.Type == PlcMemType.ZR)
            {
                for (int iPos = 0; iPos < wordSize; iPos++)
                {
                    short[] bits = WORD_ZR[addr.Addr + iPos].GetBits();
                    for (int jPos = 0; jPos < 16; jPos++)
                        resultBits[iPos * 16 + jPos] = bits[jPos] == 1;
                }
            }
            else if (addr.Type == PlcMemType.W)
            {
                for (int iPos = 0; iPos < wordSize; iPos++)
                {
                    short[] bits = WORD_W[addr.Addr + iPos].GetBits();
                    for (int jPos = 0; jPos < 16; jPos++)
                        resultBits[iPos * 16 + jPos] = bits[jPos] == 1;
                }
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
            return resultBits;
        }
        public override short[] VirGetShorts(PlcAddr addr)
        {
            short[] values = new short[addr.Length];

            if (addr.Type == PlcMemType.R)
            {
                for (int iPos = 0; iPos < addr.Length; iPos++)
                {
                    values[iPos] = WORD_R[addr.Addr + iPos];
                }
            }
            else if (addr.Type == PlcMemType.D)
            {
                for (int iPos = 0; iPos < addr.Length; iPos++)
                {
                    values[iPos] = WORD_D[addr.Addr + iPos];
                }
            }
            else if (addr.Type == PlcMemType.ZR)
            {
                for (int iPos = 0; iPos < addr.Length; iPos++)
                {
                    values[iPos] = WORD_ZR[addr.Addr + iPos];
                }
            }
            else if (addr.Type == PlcMemType.W)
            {
                for (int iPos = 0; iPos < addr.Length; iPos++)
                {
                    values[iPos] = WORD_W[addr.Addr + iPos];
                }
            }
            else if (addr.Type == PlcMemType.Wr)
            {
                for (int iPos = 0; iPos < addr.Length; iPos++)
                {
                    values[iPos] = WORD_Wr[addr.Addr + iPos];
                }
            }
            else if (addr.Type == PlcMemType.Ww)
            {
                for (int iPos = 0; iPos < addr.Length; iPos++)
                {
                    values[iPos] = WORD_Ww[addr.Addr + iPos];
                }
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
            if (addr.Type == PlcMemType.R)
            {
                byte[] bytes = new byte[4];

                bytes[0] = WORD_R[addr.Addr + 0].GetByte(0);
                bytes[1] = WORD_R[addr.Addr + 0].GetByte(1);
                bytes[2] = WORD_R[addr.Addr + 1].GetByte(0);
                bytes[3] = WORD_R[addr.Addr + 1].GetByte(1);

                return BitConverter.ToSingle(bytes, 0);
            }
            else if (addr.Type == PlcMemType.D)
            {
                byte[] bytes = new byte[4];

                bytes[0] = WORD_D[addr.Addr + 0].GetByte(0);
                bytes[1] = WORD_D[addr.Addr + 0].GetByte(1);
                bytes[2] = WORD_D[addr.Addr + 1].GetByte(0);
                bytes[3] = WORD_D[addr.Addr + 1].GetByte(1);

                return BitConverter.ToSingle(bytes, 0);
            }
            else if (addr.Type == PlcMemType.ZR)
            {
                byte[] bytes = new byte[4];

                bytes[0] = WORD_ZR[addr.Addr + 0].GetByte(0);
                bytes[1] = WORD_ZR[addr.Addr + 0].GetByte(1);
                bytes[2] = WORD_ZR[addr.Addr + 1].GetByte(0);
                bytes[3] = WORD_ZR[addr.Addr + 1].GetByte(1);

                return BitConverter.ToSingle(bytes, 0);
            }
            else if (addr.Type == PlcMemType.W)
            {
                byte[] bytes = new byte[4];

                bytes[0] = WORD_W[addr.Addr + 0].GetByte(0);
                bytes[1] = WORD_W[addr.Addr + 0].GetByte(1);
                bytes[2] = WORD_W[addr.Addr + 1].GetByte(0);
                bytes[3] = WORD_W[addr.Addr + 1].GetByte(1);

                return BitConverter.ToSingle(bytes, 0);
            }
            else if (addr.Type == PlcMemType.Wr)
            {
                return WORD_Wr[addr.Addr];
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

            if (addr.Type == PlcMemType.R)
            {
                WORD_R[addr.Addr + 0] = write[0];
                WORD_R[addr.Addr + 1] = write[1];
            }
            else if (addr.Type == PlcMemType.D)
            {
                WORD_D[addr.Addr + 0] = write[0];
                WORD_D[addr.Addr + 1] = write[1];
            }
            else if (addr.Type == PlcMemType.ZR)
            {
                WORD_ZR[addr.Addr + 0] = write[0];
                WORD_ZR[addr.Addr + 1] = write[1];
            }
            else if (addr.Type == PlcMemType.W)
            {
                WORD_W[addr.Addr + 0] = write[0];
                WORD_W[addr.Addr + 1] = write[1];
            }
            else if (addr.Type == PlcMemType.Ww)
            {
                WORD_Ww[addr.Addr] = (short)value;
                //WORD_Ww[addr.Addr + 1] = write[1];
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }
    }
}
