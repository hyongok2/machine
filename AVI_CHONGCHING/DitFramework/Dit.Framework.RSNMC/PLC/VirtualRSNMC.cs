using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using Dit.Framework.Comm;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;

namespace Dit.Framework.RSNMC.PLC
{
    /// <summary>
    /// RS Network Motor Contoller Memory처리
    /// date 180710
    /// since 180710
    /// </summary>
    public class VirtualRSNMC : IVirtualMem
    {
        private int ONE_BYTE_SIZE = 4;
        private const int MEM_SIZE = 102400;
        
        public int[] MC_INT = new int[MEM_SIZE];
        public double[] MC_REAL = new double[MEM_SIZE];
        public int[] CTRL_INT = new int[MEM_SIZE];
        public double[] CTRL_REAL = new double[MEM_SIZE];

        public bool[] WORD_X = new bool[MEM_SIZE];
        public bool[] WORD_Y = new bool[MEM_SIZE];
        public short[] WORD_AO = new short[MEM_SIZE];
        public short[] WORD_AI = new short[MEM_SIZE];

        private RSNMCWorker _worker;

        public VirtualRSNMC(string name, RSNMCWorker worker)
        {
            this.Name = name;
            _worker = worker;
        }
        //메소드 연결
        public override int Open()
        {
            return _worker.Open();
        }
        public override int Close()
        {
            return _worker.Close();
        }

        //메소드 동기화
        private bool[] _tempBools;
        private byte[] _tempBytes;
        private short[] _tempShorts;
        private double[] _tempdoubles;
        private int[] _tempInts;
        public override int ReadFromPLC(PlcAddr addr, int byteLen)
        {
            try
            {
                if (addr.Type < PlcMemType.RS_CI || PlcMemType.RS_AO < addr.Type) return FALSE;

                object[] dest;
                _worker.ReadMemory(addr, out dest);
                
                Array arrValue = dest as Array;

                if (addr.Type == PlcMemType.RS_MI)
                    Array.Copy(arrValue, 0, MC_INT, addr.Addr, addr.Length);
                else if (addr.Type == PlcMemType.RS_MR)
                    Array.Copy(arrValue, 0, MC_REAL, addr.Addr, addr.Length);
                else if (addr.Type == PlcMemType.RS_CI)
                    Array.Copy(arrValue, 0, CTRL_INT, addr.Addr, addr.Length);
                else if (addr.Type == PlcMemType.RS_CR)
                    Array.Copy(arrValue, 0, CTRL_REAL, addr.Addr, addr.Length);

                else if (addr.Type == PlcMemType.RS_X)
                {
                    _tempBytes = new byte[arrValue.Length];                    
                    Array.Copy(arrValue, 0, _tempBytes, 0, _tempBytes.Length);

                    _tempBools = new bool[arrValue.Length * 8];
                    (new BitArray(_tempBytes)).CopyTo(_tempBools, 0);
                    Array.Copy(_tempBools, 0, WORD_X, addr.Addr, byteLen * 8);                    
                }
                else if (addr.Type == PlcMemType.RS_Y)
                {
                    _tempBytes = new byte[arrValue.Length];
                    Array.Copy(arrValue, 0, _tempBytes, 0, _tempBytes.Length);

                    _tempBools = new bool[arrValue.Length * 8];
                    (new BitArray(_tempBytes)).CopyTo(_tempBools, 0);
                    Array.Copy(_tempBools, 0, WORD_Y, addr.Addr, byteLen * 8);
                }
                else if (addr.Type == PlcMemType.RS_AI)
                {
                    _tempBytes = new byte[arrValue.Length];
                    Array.Copy(arrValue, 0, _tempBytes, 0, _tempBytes.Length);

                    _tempShorts = new short[_tempBytes.Length / 2];
                    Buffer.BlockCopy(_tempBytes, 0, _tempShorts, 0, _tempBytes.Length);
                    Array.Copy(_tempShorts, 0, WORD_AI, addr.Addr, addr.Length);
                }
                else if (addr.Type == PlcMemType.RS_AO)
                {
                    _tempBytes = new byte[arrValue.Length];
                    Array.Copy(arrValue, 0, _tempBytes, 0, _tempBytes.Length);

                    _tempShorts = new short[_tempBytes.Length / 2];
                    Buffer.BlockCopy(_tempBytes, 0, _tempShorts, 0, _tempBytes.Length);
                    Array.Copy(_tempShorts, 0, WORD_AO, addr.Addr, addr.Length);
                }
                else
                    return FALSE;

                return TRUE;
            }
            catch (Exception ex)
            {
                return FALSE;
            }
        }
        public override int WriteToPLC(PlcAddr addr, int byteLen)
        {
            try
            {
                if (addr.Type == PlcMemType.RS_MI)
                {
                    _tempInts= new int[addr.Length];
                    Array.Copy(MC_INT, addr.Addr, _tempInts, 0, addr.Length);
                    _worker.WriteMemory(addr, _tempInts);   
                    return TRUE;
                }
                else if (addr.Type == PlcMemType.RS_MR)
                {
                    _tempdoubles = new double[addr.Length];
                    Array.Copy(MC_REAL, addr.Addr, _tempdoubles, 0, addr.Length);
                    _worker.WriteMemory(addr, _tempdoubles);
                    return TRUE;
                }
                else if (addr.Type == PlcMemType.RS_CI)
                {
                    _tempInts= new int[addr.Length];
                    Array.Copy(CTRL_INT, addr.Addr, _tempInts, 0, addr.Length);
                    _worker.WriteMemory(addr, _tempInts);
                    return TRUE;
                }
                else if (addr.Type == PlcMemType.RS_CR)
                {
                    _tempdoubles = new double[addr.Length];
                    Array.Copy(CTRL_REAL, addr.Addr, _tempdoubles, 0, addr.Length);
                    _worker.WriteMemory(addr, _tempdoubles);
                    return TRUE;
                }

                else if (addr.Type == PlcMemType.RS_X)
                {
                    _tempBytes= WORD_X.GetBytes(addr.Addr, byteLen);
                    _worker.WriteMemory(addr, _tempBytes);
                    return TRUE;
                }
                else if (addr.Type == PlcMemType.RS_Y)
                {
                    _tempBytes= WORD_Y.GetBytes(addr.Addr, byteLen);
                    _worker.WriteMemory(addr, _tempBytes);
                    return TRUE;
                }
                else if (addr.Type == PlcMemType.RS_AI)
                {                    
                    _tempShorts = new short[addr.Length];
                    Array.Copy(WORD_AI, addr.Addr, _tempShorts, 0, addr.Length);
                    _tempBytes = new byte[addr.Length * 2];
                    Buffer.BlockCopy(_tempShorts, 0, _tempBytes, 0, _tempBytes.Length);
                    _worker.WriteMemory(addr, _tempBytes);
                    return TRUE;
                }
                else if (addr.Type == PlcMemType.RS_AO)
                {
                    _tempShorts = new short[addr.Length];
                    Array.Copy(WORD_AO, addr.Addr, _tempShorts, 0, addr.Length);
                    _tempBytes = new byte[addr.Length * 2];
                    Buffer.BlockCopy(_tempShorts, 0, _tempBytes, 0, _tempBytes.Length);
                    _worker.WriteMemory(addr, _tempBytes);
                    return TRUE;
                }
                else
                {
                    return FALSE;
                }
            }
            catch
            {
                return FALSE;
            }

        }
        #region Access Memory        
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
            throw new Exception("미 구현");
            //return new bool[0];
        }
        //메소드 - STRING
        public override int SetAscii(PlcAddr addr, string text)
        {
            throw new Exception("미 구현");
            //return 0;
        }
        //메소드 - SHORT
        public override short GetShort(PlcAddr addr)
        {
            throw new Exception("미 구현");
            //return 0;
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
            if (addr.Type == PlcMemType.RS_X)
            {
                return WORD_X[addr.Addr];
            }
            else if (addr.Type == PlcMemType.RS_Y)
            {
                return WORD_Y[addr.Addr];
            }
            else if (addr.Type == PlcMemType.RS_AO)
                return VirGetInt32(addr).GetBit(addr.Bit);
            else if (addr.Type == PlcMemType.RS_AI)
                return VirGetInt32(addr).GetBit(addr.Bit);
            else if (addr.Type == PlcMemType.RS_CI
                || addr.Type == PlcMemType.RS_CR
                || addr.Type == PlcMemType.RS_MI
                || addr.Type == PlcMemType.RS_MR
                )
                return VirGetInt32(addr).GetBit(addr.Bit);
            else
            {
                throw new Exception("미지정 메모리");
            }

        }
        public override void VirSetBit(PlcAddr addr, bool value)
        {
            if (addr.Type == PlcMemType.RS_X)
            {
                WORD_X[addr.Addr] = value;
            }
            else if (addr.Type == PlcMemType.RS_Y)
            {
                WORD_Y[addr.Addr] = value;
            }
            else if (addr.Type == PlcMemType.RS_AO)
            {
                int vv = VirGetInt32(addr).SetBit(addr.Bit, value);
                VirSetInt32(addr, vv);
            }
            else if (addr.Type == PlcMemType.RS_AI)
            {
                int vv = VirGetInt32(addr).SetBit(addr.Bit, value);
                VirSetInt32(addr, vv);
            }
            else if (addr.Type == PlcMemType.RS_CI
                || addr.Type == PlcMemType.RS_CR
                || addr.Type == PlcMemType.RS_MI
                || addr.Type == PlcMemType.RS_MR
            )
            {
                int vv = VirGetInt32(addr).SetBit(addr.Bit, value);
                VirSetInt32(addr, vv);
            }
            else
            {
                throw new Exception("미지정 메모리");
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
            if (addr.Type == PlcMemType.RS_MI)
            {
                return MC_INT[addr.Addr];
            }
            else if (addr.Type == PlcMemType.RS_CI)
            {
                return CTRL_INT[addr.Addr];
            }
            else
                throw new Exception("ADDR TYPE ERROR");
        }
        public override void VirSetInt32(PlcAddr addr, int value)
        {
            if (addr.Type == PlcMemType.RS_MI)
            {
                MC_INT[addr.Addr] = value;
            }
            else if (addr.Type == PlcMemType.RS_CI)
            {
                CTRL_INT[addr.Addr] = value;
            }
            else
                throw new Exception("ADDR TYPE ERROR");
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
            if (addr.Type == PlcMemType.RS_MR)
            {
                return (float)MC_REAL[addr.Addr];
            }
            else if (addr.Type == PlcMemType.RS_CR)
            {
                return (float)CTRL_REAL[addr.Addr];
            }
            else if (addr.Type == PlcMemType.RS_AI)
            {
                return WORD_AI[addr.Addr];
            }
            else if (addr.Type == PlcMemType.RS_AO)
            {
                return WORD_AO[addr.Addr];
            }
            else
                throw new Exception("ADDR TYPE ERROR");
        }
        public override void VirSetFloat(PlcAddr addr, float value)
        {
            if (addr.Type == PlcMemType.RS_MR)
            {
                MC_REAL[addr.Addr] = value;
            }
            else if (addr.Type == PlcMemType.RS_CR)
            {
                CTRL_REAL[addr.Addr] = value;
            }
            else if (addr.Type == PlcMemType.RS_AI)
            {
                WORD_AI[addr.Addr] = (short)value;
            }
            else if (addr.Type == PlcMemType.RS_AO)
            {
                WORD_AO[addr.Addr] = (short)value;
            }
            else
                throw new Exception("ADDR TYPE ERROR");
        }
        #endregion
    }
}
