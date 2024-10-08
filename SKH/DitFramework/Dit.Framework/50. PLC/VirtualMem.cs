using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.MemoryMappedFiles;
using Dit.Framework.Comm;

namespace Dit.Framework.PLC
{
    public class VirtualMem : IVirtualMem
    {
        public int PLC_MEM_SIZE = 102400;

        private MemoryMappedFile _fileX = null;
        private MemoryMappedViewAccessor _accessorX = null;

        private MemoryMappedFile _fileY = null;
        private MemoryMappedViewAccessor _accessorY = null;

        private MemoryMappedFile _fileWr = null;
        private MemoryMappedViewAccessor _accessorWr = null;

        private MemoryMappedFile _fileWw = null;
        private MemoryMappedViewAccessor _accessorWw = null;

        private MemoryMappedFile _fileB = null;
        private MemoryMappedViewAccessor _accessorB = null;

        private MemoryMappedFile _fileR = null;
        private MemoryMappedViewAccessor _accessorR = null;

        private MemoryMappedFile _fileD = null;
        private MemoryMappedViewAccessor _accessorD = null;

        private MemoryMappedFile _fileW = null;
        private MemoryMappedViewAccessor _accessorW = null;

        private MemoryMappedFile _fileZR = null;
        private MemoryMappedViewAccessor _accessorZR = null;

        //공유 메모리용
        private MemoryMappedFile _fileS = null;
        private MemoryMappedViewAccessor _accessorS = null;

        //UMAC 공유 메모리용
        private MemoryMappedFile _fileP = null;
        private MemoryMappedViewAccessor _accessorP = null;

        //ACS 
        private MemoryMappedFile _fileCR = null;
        private MemoryMappedViewAccessor _accessorCR = null;

        private MemoryMappedFile _fileCI = null;
        private MemoryMappedViewAccessor _accessorCI = null;

        private MemoryMappedFile _fileAR = null;
        private MemoryMappedViewAccessor _accessorAR = null;

        private MemoryMappedFile _fileAI = null;
        private MemoryMappedViewAccessor _accessorAI = null;

        //RS NMC
        private MemoryMappedFile _fileRS_CR = null;
        private MemoryMappedViewAccessor _accessorRS_CR = null;

        private MemoryMappedFile _fileRS_CI = null;
        private MemoryMappedViewAccessor _accessorRS_CI = null;

        private MemoryMappedFile _fileRS_MR = null;
        private MemoryMappedViewAccessor _accessorRS_MR = null;

        private MemoryMappedFile _fileRS_MI = null;
        private MemoryMappedViewAccessor _accessorRS_MI = null;

        private MemoryMappedFile _fileEM = null;
        private MemoryMappedViewAccessor _accessorEM = null;

        private MemoryMappedFile _fileER = null;
        private MemoryMappedViewAccessor _accessorER = null;

        public string MemoryName;
        public VirtualMem(string name)
        {
            MemoryName = string.Format("DIT.PLC.{0}.", name);
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
            _fileX = MemoryMappedFile.CreateOrOpen(MemoryName + "X", PLC_MEM_SIZE);
            _accessorX = _fileX.CreateViewAccessor();

            _fileY = MemoryMappedFile.CreateOrOpen(MemoryName + "Y", PLC_MEM_SIZE);
            _accessorY = _fileY.CreateViewAccessor();

            _fileWw = MemoryMappedFile.CreateOrOpen(MemoryName + "Ww", PLC_MEM_SIZE);
            _accessorWw = _fileWw.CreateViewAccessor();

            _fileWr = MemoryMappedFile.CreateOrOpen(MemoryName + "Wr", PLC_MEM_SIZE);
            _accessorWr = _fileWr.CreateViewAccessor();

            _fileB = MemoryMappedFile.CreateOrOpen(MemoryName + "B", PLC_MEM_SIZE);
            _accessorB = _fileB.CreateViewAccessor();

            _fileR = MemoryMappedFile.CreateOrOpen(MemoryName + "R", PLC_MEM_SIZE);
            _accessorR = _fileR.CreateViewAccessor();

            _fileD = MemoryMappedFile.CreateOrOpen(MemoryName + "D", PLC_MEM_SIZE);
            _accessorD = _fileD.CreateViewAccessor();

            _fileW = MemoryMappedFile.CreateOrOpen(MemoryName + "W", PLC_MEM_SIZE);
            _accessorW = _fileW.CreateViewAccessor();

            _fileZR = MemoryMappedFile.CreateOrOpen(MemoryName + "ZR", PLC_MEM_SIZE);
            _accessorZR = _fileZR.CreateViewAccessor();

            _fileS = MemoryMappedFile.CreateOrOpen(MemoryName + "S", PLC_MEM_SIZE);
            _accessorS = _fileS.CreateViewAccessor();

            _fileP = MemoryMappedFile.CreateOrOpen(MemoryName + "P", PLC_MEM_SIZE);
            _accessorP = _fileP.CreateViewAccessor();


            _fileCR = MemoryMappedFile.CreateOrOpen(MemoryName + "CR", PLC_MEM_SIZE);
            _accessorCR = _fileCR.CreateViewAccessor();

            _fileCI = MemoryMappedFile.CreateOrOpen(MemoryName + "CI", PLC_MEM_SIZE);
            _accessorCI = _fileCI.CreateViewAccessor();
            
            _fileAR = MemoryMappedFile.CreateOrOpen(MemoryName + "AR", PLC_MEM_SIZE);
            _accessorAR = _fileAR.CreateViewAccessor();

            _fileAI = MemoryMappedFile.CreateOrOpen(MemoryName + "AI", PLC_MEM_SIZE);
            _accessorAI = _fileAI.CreateViewAccessor();

            //RS NMC
            _fileRS_CR = MemoryMappedFile.CreateOrOpen(MemoryName + "RS_CR", PLC_MEM_SIZE);
            _accessorRS_CR = _fileRS_CR.CreateViewAccessor();

            _fileRS_CI = MemoryMappedFile.CreateOrOpen(MemoryName + "RS_CI", PLC_MEM_SIZE);
            _accessorRS_CI = _fileRS_CI.CreateViewAccessor();

            _fileRS_MR = MemoryMappedFile.CreateOrOpen(MemoryName + "RS_MR", PLC_MEM_SIZE);
            _accessorRS_MR = _fileRS_MR.CreateViewAccessor();

            _fileRS_MI = MemoryMappedFile.CreateOrOpen(MemoryName + "RS_MI", PLC_MEM_SIZE);
            _accessorRS_MI = _fileRS_MI.CreateViewAccessor();

            _fileEM = MemoryMappedFile.CreateOrOpen(MemoryName + "EM", PLC_MEM_SIZE);
            _accessorEM = _fileEM.CreateViewAccessor();

            _fileER = MemoryMappedFile.CreateOrOpen(MemoryName + "ER", PLC_MEM_SIZE);
            _accessorER = _fileER.CreateViewAccessor();

            return 0;
        }

        private MemoryMappedViewAccessor GetAccessor(PlcAddr addr)
        {

            if (addr.Type == PlcMemType.X || addr.Type == PlcMemType.RS_X)
            {
                return _accessorX;
            }
            else if (addr.Type == PlcMemType.Y || addr.Type == PlcMemType.RS_Y)
            {
                return _accessorY;
            }
            else if (addr.Type == PlcMemType.Ww)
            {
                return _accessorWw;
            }
            else if (addr.Type == PlcMemType.Wr)
            {
                return _accessorWr;
            }
            else if (addr.Type == PlcMemType.B)
            {
                return _accessorB;
            }
            else if (addr.Type == PlcMemType.D)
            {
                return _accessorD;
            }
            else if (addr.Type == PlcMemType.ZR)
            {
                return _accessorZR;
            }
            else if (addr.Type == PlcMemType.S)
            {
                return _accessorS;
            }
            else if (addr.Type == PlcMemType.P)
            {
                return _accessorP;
            }
            else if (addr.Type == PlcMemType.R)
            {
                return _accessorR;
            }
            else if (addr.Type == PlcMemType.CR)
            {
                return _accessorCR;
            }
            else if (addr.Type == PlcMemType.CI)
            {
                return _accessorCI;
            }
            else if (addr.Type == PlcMemType.AR)
            {
                return _accessorAR;
            }
            else if (addr.Type == PlcMemType.AI)
            {
                return _accessorAI;
            }

            else if (addr.Type == PlcMemType.RS_CR)
            {
                return _accessorRS_CR;
            }
            else if (addr.Type == PlcMemType.RS_CI)
            {
                return _accessorRS_CI;
            }
            else if (addr.Type == PlcMemType.RS_MR)
            {
                return _accessorRS_MR;
            }
            else if (addr.Type == PlcMemType.RS_MI)
            {
                return _accessorRS_MI;
            }
            else if (addr.Type == PlcMemType.RS_AI)
                return _accessorAI; // jys:: simulation 시 analog input 이라 의미없음
            else if (addr.Type == PlcMemType.EM)
                return _accessorEM;
            else if (addr.Type == PlcMemType.ER)
                return _accessorER;
            else
                throw new Exception("미지정 메모리 타입");
        }
        private long ConvertVirAddr(PlcAddr addr, int start)
        {
            if (addr.Type == PlcMemType.P 
                || addr.Type == PlcMemType.CR
                || addr.Type == PlcMemType.AR
                || addr.Type == PlcMemType.CI 
                || addr.Type == PlcMemType.AI
                || addr.Type == PlcMemType.RS_CI
                || addr.Type == PlcMemType.RS_CR
                || addr.Type == PlcMemType.RS_MI
                || addr.Type == PlcMemType.RS_MR
                || addr.Type == PlcMemType.ER
                )
                return start * 4;
            else
                return start;
        }
        public override int Close()
        {
            _fileX.Dispose();
            _accessorX.Dispose();

            _fileY.Dispose();
            _accessorY.Dispose();

            _fileWw.Dispose();
            _accessorWw.Dispose();

            _fileWr.Dispose();
            _accessorWr.Dispose();

            _fileB.Dispose();
            _accessorB.Dispose();

            _fileR.Dispose();
            _accessorR.Dispose();

            _fileD.Dispose();
            _accessorD.Dispose();

            _fileW.Dispose();
            _accessorW.Dispose();

            _fileZR.Dispose();
            _accessorZR.Dispose();

            _fileS.Dispose();
            _accessorS.Dispose();

            _fileP.Dispose();
            _accessorP.Dispose();

            _fileEM.Dispose();
            _accessorEM.Dispose();

            _fileER.Dispose();
            _accessorER.Dispose();

            return 0;
        }
        public override int ReadFromPLC(PlcAddr addr, int size)
        {
            int result = 0;
            return result;
        }

        public override int WriteToPLC(PlcAddr addr, int size)
        {
            int result = 0;
            return result;
        }

        //메소드 비트
        public override bool GetBit(PlcAddr addr)
        {
            if (addr.Type == PlcMemType.S 
                || addr.Type == PlcMemType.AI 
                || addr.Type == PlcMemType.CI
                || addr.Type == PlcMemType.RS_MI
                || addr.Type == PlcMemType.RS_CI
                || addr.Type == PlcMemType.EM
                )
            {
                //return GetAccessor(addr).ReadByte(addr.Addr).GetBit(addr.Bit);
                return GetAccessor(addr).ReadInt32(addr.Addr * 4).GetBit(addr.Bit);
            }
            else
            {
                return GetAccessor(addr).ReadBoolean(ConvertVirAddr(addr, addr.Addr));
            }
        }
        public override void SetBit(PlcAddr addr, bool value)
        {
            if (addr.Type == PlcMemType.S 
                || addr.Type == PlcMemType.CI
                || addr.Type == PlcMemType.AI
                || addr.Type == PlcMemType.RS_MI
                || addr.Type == PlcMemType.RS_CI
                || addr.Type == PlcMemType.EM
                )
            {
                int wb = GetAccessor(addr).ReadInt32(addr.Addr * 4).SetBit(addr.Bit, value);
                GetAccessor(addr).Write(addr.Addr * 4, wb);
            }
            else
            {
                GetAccessor(addr).Write(ConvertVirAddr(addr, addr.Addr), value);
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
            if (string.IsNullOrEmpty(text))
                return 0;

            byte[] data = new byte[addr.Length];
            byte[] source = Encoding.ASCII.GetBytes(text);
            Array.Copy(source, data, source.Length < addr.Length ? source.Length : addr.Length);

            GetAccessor(addr).WriteArray(ConvertVirAddr(addr, addr.Addr), data, 0, data.Length);
            int result = 0;
            return result;
        }
        public override string GetAscii(PlcAddr addr)
        {
            byte[] data = new byte[addr.Length];
            GetAccessor(addr).ReadArray(ConvertVirAddr(addr, addr.Addr), data, 0, data.Length);
            //string str = data.GetPlcAscii(0, addr.Length);
            return Encoding.ASCII.GetString(data).Replace("\0", "");
        }

        //메소드 - SHORT      
        public override short GetShort(PlcAddr addr)
        {
            return GetAccessor(addr).ReadInt16(ConvertVirAddr(addr, addr.Addr));
        }
        public override void SetShort(PlcAddr addr, short value)
        {
            GetAccessor(addr).Write(ConvertVirAddr(addr, addr.Addr), value);
        }

        //메소드 - SHORT      
        public override void SetShorts(PlcAddr addr, short[] values, out int result)
        {
            //throw new Exception("미지정 함수");
            result = 0;
            return;
        }
        public override short[] GetShorts(PlcAddr addr, int wordSize, out int result)
        {
            short[] values = new short[wordSize];
            result = GetAccessor(addr).ReadArray<short>((long)addr.Addr, values, 0, (int)values.Length);
            return values;
        }

        //메소드 - INT32
        public override void SetInt32(PlcAddr addr, int value)
        {
            GetAccessor(addr).Write(ConvertVirAddr(addr, addr.Addr), value);
        }
        public override int GetInt32(PlcAddr addr)
        {
            return GetAccessor(addr).ReadInt32(ConvertVirAddr(addr, addr.Addr));
        }
        //메소드 - INT32s
        public virtual void SetInt32s(PlcAddr addr, int[] values, out int result)
        {
            //throw new Exception("미지정 함수");
            result = 0;
            return;
        }
        public override int[] GetInt32s(PlcAddr addr, int wordSize, out int result)
        {
            int[] values = new int[wordSize];
            result = GetAccessor(addr).ReadArray<int>((long)addr.Addr, values, 0, (int)values.Length);
            return values;
        }
        //메소드 - FLOAT
        private float GetFloat(PlcAddr addr)
        {
            return GetAccessor(addr).ReadSingle(ConvertVirAddr(addr, addr.Addr));
        }
        private void SetFloat(PlcAddr addr, float value)
        {
            GetAccessor(addr).Write(ConvertVirAddr(addr, addr.Addr), value);
        }
        //읽어온 메모리에서 읽어오는 함수.
        public override bool VirGetBit(PlcAddr addr)
        {
            return GetBit(addr);
        }
        public override void VirSetBit(PlcAddr addr, bool value)
        {
            SetBit(addr, value);
        }
        public override short VirGetShort(PlcAddr addr)
        {
            return GetShort(addr);
        }
        public override void VirSetShort(PlcAddr addr, short value)
        {
            SetShort(addr, value);
        }
        public override string VirGetAscii(PlcAddr addr)
        {
            return GetAscii(addr);
        }
        public override void VirSetAscii(PlcAddr addr, string value)
        {
            SetAscii(addr, value);
        }
        public override int VirGetInt32(PlcAddr addr)
        {
            return GetInt32(addr);
        }
        public override void VirSetInt32(PlcAddr addr, int value)
        {
            SetInt32(addr, value);
        }
        public override bool[] VirGetBits(PlcAddr addr, int wordSize)
        {
            int result = 0;
            return GetBists(addr, wordSize, out result);
        }
        public override short[] VirGetShorts(PlcAddr addr)
        {
            int result = 0;
            return GetShorts(addr, addr.Length, out result);
        }
        public override float VirGetFloat(PlcAddr addr)
        {
            return GetFloat(addr);
        }
        public override void VirSetFloat(PlcAddr addr, float value)
        {
            SetFloat(addr, value);
        }
    }
}
