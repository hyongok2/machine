using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using Dit.Framework.PLC;
using Dit.Framework.Comm;

namespace Dit.Framework.PLC
{
    public class VirtualCCLinkEx : IVirtualMem
    {
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdOpen(short Chan, short Mode, ref int Path);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdClose(int Path);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdSend(int Path, short Stno, short Devtyp, short devno, ref short size, ref short buf);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdReceive(int Path, short Stno, short Devtyp, short devno, ref short size, ref short buf);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdDevSet(int Path, short Stno, short Devtyp, short devno);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdDevRst(int Path, short Stno, short Devtyp, short devno);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdRandW(int Path, short Stno, ref short dev, ref short buf, short bufsiz);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdRandR(int Path, short Stno, ref short dev, ref short buf, short bufsiz);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdControl(int Path, short Stno, short buf);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdTypeRead(int Path, short Stno, ref short buf);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdBdLedRead(int Path, ref short buf);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdBdModRead(int Path, ref short Mode);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdBdModSet(int Path, short Mode);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdBdRst(int Path);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdBdSwRead(int Path, ref short buf);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdBdVerRead(int Path, ref short buf);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdInit(int Path);
        [DllImport("MDFUNC32.DLL")]
        private static extern short mdWaitBdEvent(int Path, ref short eventno, int timeout, ref short signaledno, ref short details);
        [DllImport("MDFUNC32.DLL")]
        private static extern int mdSendEx(int Path, int Netno, int Stno, int Devtyp, int devno, ref int size, ref short buf);
        [DllImport("MDFUNC32.DLL")]
        private static extern int mdReceiveEx(int Path, int Netno, int Stno, int Devtyp, int devno, ref int size, ref short buf);
        [DllImport("MDFUNC32.DLL")]
        private static extern int mdDevSetEx(int Path, int Netno, int Stno, int Devtyp, int devno);
        [DllImport("MDFUNC32.DLL")]
        private static extern int mdDevRstEx(int Path, int Netno, int Stno, int Devtyp, int devno);
        [DllImport("MDFUNC32.DLL")]
        private static extern int mdRandWEx(int Path, int Netno, int Stno, ref int dev, ref short buf, int bufsiz);
        [DllImport("MDFUNC32.DLL")]
        private static extern int mdRandREx(int Path, int Netno, int Stno, ref int dev, ref short buf, int bufsiz);

        private const int PLC_MEM_SIZE = 102400;

        public bool[] WORD_X = new bool[PLC_MEM_SIZE];
        public bool[] WORD_Y = new bool[PLC_MEM_SIZE];
        public bool[] WORD_B = new bool[PLC_MEM_SIZE];
        public bool[] WORD_SB = new bool[PLC_MEM_SIZE];
        public short[] WORD_R = new short[PLC_MEM_SIZE];
        public short[] WORD_D = new short[PLC_MEM_SIZE];
        public short[] WORD_W = new short[PLC_MEM_SIZE];
        public short[] WORD_ZR = new short[PLC_MEM_SIZE];
        public short[] WORD_Ww = new short[PLC_MEM_SIZE];
        public short[] WORD_Wr = new short[PLC_MEM_SIZE];

        private int _channelPath = 0;
        public int NetworkNo = 0;
        public int CclinkPath
        {
            get { return _channelPath; }
            set { _channelPath = value; }
        }
        public short StationNo { get; set; }
        public short Channel { get; set; }
        public short Mode { get; set; }

        public bool IsDirect { get; set; }

        public VirtualCCLinkEx(short channel, short stationNo, short md, string name)
        {
            StationNo = stationNo;
            Channel = channel;
            Mode = md;
            Name = name;
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
            //short mMD = -1;
            //short mMD = 0;
            short result = mdOpen(Channel, Mode, ref _channelPath);
            return result;
        }

        public override int Close()
        {
            return mdClose(_channelPath);
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
            if (addr.Type == PlcMemType.SB)
            {
                bool[] currWords = GetBists(addr, wordSize, out result);
                Array.Copy(currWords, 0, WORD_SB, addr.Addr, wordSize * 16);
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
            int byteSize = wordSize * 2;
            if (addr.Type == PlcMemType.B)
            {
                string strAddr = addr.GetPlcAddressWordString();
                short[] shorts = WORD_B.GetShorts(addr.Addr, wordSize);
                result = mdSendEx(_channelPath, NetworkNo, StationNo, (int)addr.Type, addr.Addr, ref byteSize, ref shorts[0]);
                return result;
            }
            else if (addr.Type == PlcMemType.X)
            {
                short[] shorts = WORD_X.GetShorts(addr.Addr, wordSize);
                result = mdSendEx(_channelPath, NetworkNo, StationNo, (int)addr.Type, addr.Addr, ref byteSize, ref shorts[0]);
                return result;
            }
            else if (addr.Type == PlcMemType.Y)
            {
                short[] shorts = WORD_Y.GetShorts(addr.Addr, wordSize);
                result = mdSendEx(_channelPath, NetworkNo, StationNo, (int)addr.Type, addr.Addr, ref byteSize, ref shorts[0]);
                return result;
            }
            else if (addr.Type == PlcMemType.R)
            {
                result = mdSendEx(_channelPath, NetworkNo, StationNo, (int)addr.Type, addr.Addr, ref byteSize, ref  WORD_R[addr.Addr]);
                return result;
            }
            else if (addr.Type == PlcMemType.D)
            {
                result = mdSendEx(_channelPath, NetworkNo, StationNo, (int)addr.Type, addr.Addr, ref byteSize, ref  WORD_D[addr.Addr]);
                return result;
            }
            else if (addr.Type == PlcMemType.ZR)
            {
                result = mdSendEx(_channelPath, NetworkNo, StationNo, (int)addr.Type, addr.Addr, ref byteSize, ref  WORD_ZR[addr.Addr]);
                return result;
            }
            else if (addr.Type == PlcMemType.W)
            {
                result = mdSendEx(_channelPath, NetworkNo, StationNo, (int)addr.Type, addr.Addr, ref byteSize, ref  WORD_W[addr.Addr]);
                return result;
            }
            else if (addr.Type == PlcMemType.Wr)
            {
                result = mdSendEx(_channelPath, NetworkNo, StationNo, (int)addr.Type, addr.Addr, ref byteSize, ref  WORD_Wr[addr.Addr]);
                return result;
            }
            else if (addr.Type == PlcMemType.Ww)
            {
                result = mdSendEx(_channelPath, NetworkNo, StationNo, (int)addr.Type, addr.Addr, ref byteSize, ref  WORD_Ww[addr.Addr]);
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
            short[] readBuff = new short[1];
            int wordSize = 2;
            if (addr.Type == PlcMemType.X || addr.Type == PlcMemType.Y || addr.Type == PlcMemType.B || addr.Type == PlcMemType.SB)
            {
                int address = (int)Math.Truncate(addr.Addr / 8f) * 8;
                int bit = addr.Addr % 8;

                int result = mdReceiveEx(_channelPath, NetworkNo, StationNo, (int)addr.Type, address, ref wordSize, ref readBuff[0]);
                return readBuff[0].GetBit(bit);
            }
            else
            {
                int result = mdReceiveEx(_channelPath, NetworkNo, StationNo, (int)addr.Type, addr.Addr, ref wordSize, ref readBuff[0]);
                return readBuff[0].GetBit(addr.Bit);
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
            short[] writeBuff = new short[1];
            short address = (short)addr.Addr;
            short byteSize = 2;

            writeBuff[0] = value ? (short)1 : (short)0;

            if (addr.Type == PlcMemType.B || addr.Type == PlcMemType.Y || addr.Type == PlcMemType.B)
            {
                //int result = mdSend(_channelPath, StationNo, (short)addr.Type, (short)addr.Addr, ref byteSize, ref writeBuff[0]);

                int result = 0;
                if (value)
                    result = mdDevSetEx(_channelPath, 0, StationNo, (int)addr.Type, addr.Addr);
                else
                    result = mdDevRstEx(_channelPath, 0, StationNo, (int)addr.Type, addr.Addr);

            }
            else
            {
                short shortValue = GetShort(addr);
                writeBuff[0] = shortValue.SetBit(addr.Bit, true);
                SetShort(addr, writeBuff[0]);
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
            short byteSize = (short)(wordSize * 2);
            short[] getWords = new short[wordSize];
            bool[] getBool = new bool[wordSize * 16];

            result = mdReceive(_channelPath, StationNo, (short)addr.Type, (short)addr.Addr, ref byteSize, ref getWords[0]);
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
            string str = text.PadRight(addr.Length * 2, '\0');
            byte[] bytes = Encoding.ASCII.GetBytes(str);

            short[] writeBuff = new short[bytes.Length / 2];
            int byteSize = writeBuff.Length * 2;
            for (int i = 0; i < writeBuff.Length; i++)
            {
                writeBuff[i] = (short)(bytes[i * 2 + 1] * 0x100 + bytes[i * 2]);
            }

            int result = mdSendEx(_channelPath, NetworkNo, StationNo, (int)addr.Type, addr.Addr, ref byteSize, ref writeBuff[0]);
            return result;
        }
        public override string GetAscii(PlcAddr addr)
        {
            int result = 0;
            short[] readShort = GetShorts(addr, addr.Length, out result);
            return readShort.GetPlcAscii(0, addr.Length);
        }


        //메소드 - SHORT
        public override short GetShort(PlcAddr addr)
        {
            int result = 0;
            short[] readShort = GetShorts(addr, 1, out result);
            return readShort[0];
        }
        public override void SetShort(PlcAddr addr, short value)
        {
            short[] writeBuff = new short[1];
            int byteSize = 2;
            writeBuff[0] = value;

            int result = mdSendEx(_channelPath, NetworkNo, StationNo, (int)addr.Type, addr.Addr, ref byteSize, ref writeBuff[0]);
        }

        //메소드 - SHORT        
        public override short[] GetShorts(PlcAddr addr, int wordSize, out int result)
        {
            short[] getwords = new short[wordSize];
            int byteSize = wordSize * 2;
            result = mdReceiveEx(_channelPath, NetworkNo, StationNo, (int)addr.Type, addr.Addr, ref byteSize, ref getwords[0]);
            return getwords;
        }
        public override void SetShorts(PlcAddr addr, short[] values, out int result)
        {
            short[] writeBuff = new short[addr.Length];
            int wordSize = values.Length > addr.Length ? addr.Length : values.Length;
            int byteSize = wordSize * 2;
            Array.Copy(values, writeBuff, wordSize);
            result = mdSendEx(_channelPath, NetworkNo, StationNo, (int)addr.Type, addr.Addr, ref byteSize, ref writeBuff[0]);
        }

        //메소드 - INT32
        public override int GetInt32(PlcAddr addr)
        {
            int result = 0;
            string strAddr = addr.GetPlcAddressBitString();
            short[] readShort = GetShorts(addr, 2, out result);

            return readShort.GetInt32(0);
        }
        public override void SetInt32(PlcAddr addr, int value)
        {
            short[] setwords = new short[2];
            setwords.SetInt32(0, value);

            int byteSize = 4;
            int result = mdSendEx(_channelPath, NetworkNo, StationNo, (int)addr.Type, addr.Addr, ref byteSize, ref setwords[0]);
        }


        //읽어온 메모리에서 읽어오는 함수.
        public override bool VirGetBit(PlcAddr addr)
        {
            if (IsDirect)
            {
                return GetBit(addr);
            }

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
            if (IsDirect)
            {
                SetBit(addr, value);
                return;
            }

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
                WORD_D[addr.Addr] = WORD_D[addr.Addr].SetBit(nBit, value);
            }
            else if (addr.Type == PlcMemType.ZR)
            {
                short word = WORD_ZR[addr.Addr];
                short nBit = (short)(1 << addr.Bit);
                WORD_ZR[addr.Addr] = WORD_ZR[addr.Addr].SetBit(nBit, value);
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }

        public override short VirGetShort(PlcAddr addr)
        {
            if (IsDirect)
            {
                return GetShort(addr);
            }

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
            if (IsDirect)
            {
                SetShort(addr, value);
                return;
            }

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
            if (IsDirect)
            {
                return GetAscii(addr);
            }

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
            if (IsDirect)
            {
                SetAscii(addr, value);
                return;
            }

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

        public override int VirGetInt32(PlcAddr addr)
        {
            if (IsDirect)
            {
                return GetInt32(addr);
            }

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
            if (IsDirect)
            {
                SetInt32(addr, value);
                return;
            }

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
            if (IsDirect)
            {
                throw new Exception("미구현");
            }


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
            if (IsDirect)
            {
                int result = 0;
                return GetShorts(addr, addr.Length, out result);
            }

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
            if (IsDirect)
            {
                throw new Exception("미구현");
            }

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
            else if (addr.Type == PlcMemType.Ww)
            {
                return WORD_Ww[addr.Addr];
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }
        public override void VirSetFloat(PlcAddr addr, float value)
        {
            if (IsDirect)
            {
                throw new Exception("미구현");
            }

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
            else if (addr.Type == PlcMemType.Wr)
            {
                WORD_Wr[addr.Addr] = (short)value;
                //WORD_Ww[addr.Addr + 1] = write[1];
            }
            else
            {
                throw new Exception("미지정 메모리");
            }
        }
    }
}
