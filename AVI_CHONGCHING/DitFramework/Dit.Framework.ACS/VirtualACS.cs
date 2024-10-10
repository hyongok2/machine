using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Comm;
using SPIIPLUSCOM660Lib;
using Dit.Framework.PLC;

namespace Dit.Framework.ACS
{
    public class VirtualACS : IVirtualMem
    {
        private int ONE_BYTE_SIZE = 4;
        private const int MEM_SIZE = 102400;

        public int[] ACS_INT = new int[MEM_SIZE];
        public double[] ACS_REAR = new double[MEM_SIZE];

        public int[] CTRL_INT = new int[MEM_SIZE];
        public double[] CTRL_REAR = new double[MEM_SIZE];

        private string _ip = string.Empty;
        private int _port = 0;

        // ACS 연결 및 명령어 처리를 위한 인터페이스 클래스 생성
        private Channel _channel = new Channel();

        public VirtualACS(string name, string ip, int port)
        {
            _ip = ip;
            _port = port;
        }
        //메소드 연결
        public override int Open()
        {
            try
            {
                _channel.OpenCommEthernetTCP(_ip.ToString(), _port);
                return TRUE;
            }
            catch
            {
                return FALSE;
            }
        }
        public override int Close()
        {
            try
            {
                _channel.CloseComm();
                return TRUE;
            }
            catch
            {
                return FALSE;
            }
        }

        //메소드 동기화
        public override int ReadFromPLC(PlcAddr addr, int wordSize)
        {
            try
            {
                if (addr.Type != PlcMemType.AI && addr.Type != PlcMemType.AR && addr.Type != PlcMemType.CI && addr.Type != PlcMemType.CR) return FALSE;

                object objValue = _channel.ReadVariable(addr.Type.ToString(), -1, addr.Addr, addr.Addr + addr.Length);
                Array arrValue = objValue as Array;

                if (addr.Type == PlcMemType.AI)
                    Array.Copy(arrValue, 0, ACS_INT, addr.Addr, addr.Length);
                else if (addr.Type == PlcMemType.AR)
                    Array.Copy(arrValue, 0, ACS_REAR, addr.Addr, addr.Length);
                else if (addr.Type == PlcMemType.CI)
                    Array.Copy(arrValue, 0, CTRL_INT, addr.Addr, addr.Length);
                else if (addr.Type == PlcMemType.CR)
                    Array.Copy(arrValue, 0, CTRL_REAR, addr.Addr, addr.Length);


                return 0;
            }
            catch
            {
                return -1;
            }
        }
        public override int WriteToPLC(PlcAddr addr, int wordSize)
        {
            try
            {
                if (addr.Type == PlcMemType.AI)
                {
                    int[] value = new int[addr.Length];
                    Array.Copy(ACS_INT, addr.Addr, value, 0, addr.Length);

                    _channel.WriteVariable(value, addr.Type.ToString(), -1, addr.Addr, addr.Addr + addr.Length - 1, _channel.ACSC_NONE, _channel.ACSC_NONE);
                    return 0;
                }
                else if (addr.Type == PlcMemType.AR)
                {
                    double[] value = new double[addr.Length];
                    Array.Copy(ACS_INT, addr.Addr, value, 0, addr.Length);

                    _channel.WriteVariable(value, addr.Type.ToString(), -1, addr.Addr, addr.Addr + addr.Length - 1, _channel.ACSC_NONE, _channel.ACSC_NONE);
                    return 0;
                }
                else if (addr.Type == PlcMemType.CI)
                {
                    int[] value = new int[addr.Length];
                    Array.Copy(CTRL_INT, addr.Addr, value, 0, addr.Length);
                    
                    //Console.WriteLine(string.Format("{0} - {1}", CTRL_INT[4], CTRL_INT[5]));

                    _channel.WriteVariable(value, addr.Type.ToString(), -1, addr.Addr, addr.Addr + addr.Length - 1, _channel.ACSC_NONE, _channel.ACSC_NONE);
                    return 0;
                }
                else if (addr.Type == PlcMemType.CR)
                {
                    double[] value = new double[addr.Length];
                    Array.Copy(CTRL_REAR, addr.Addr, value, 0, addr.Length);

                    _channel.WriteVariable(value, addr.Type.ToString(), -1, addr.Addr, addr.Addr + addr.Length - 1, _channel.ACSC_NONE, _channel.ACSC_NONE);
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                return -1;
            }

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
        {            //jys:: addr 무조건 int변환. 필요시, 변환 자료형 확인 필요            
            return VirGetInt32(addr).GetBit(addr.Bit);
        }
        public override void VirSetBit(PlcAddr addr, bool value)
        {
            int vv = VirGetInt32(addr).SetBit(addr.Bit, value);
            VirSetInt32(addr, vv);
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
            if (addr.Type == PlcMemType.AI)
            {
                return ACS_INT[addr.Addr];
            }
            else if (addr.Type == PlcMemType.CI)
            {
                return CTRL_INT[addr.Addr];
            }
            else
                throw new Exception("ADDR TYPE ERROR");
        }
        public override void VirSetInt32(PlcAddr addr, int value)
        {
            if (addr.Type == PlcMemType.AI)
            {
                ACS_INT[addr.Addr] = value;
            }
            else if (addr.Type == PlcMemType.CI)
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
            if (addr.Type == PlcMemType.AR)
            {
                return (float)ACS_REAR[addr.Addr];
            }
            else if (addr.Type == PlcMemType.CR)
            {
                return (float)CTRL_REAR[addr.Addr];
            }
            else
                throw new Exception("ADDR TYPE ERROR");
        }
        public override void VirSetFloat(PlcAddr addr, float value)
        {
            if (addr.Type == PlcMemType.AR)
            {
                ACS_REAR[addr.Addr] = value;
            }
            else if (addr.Type == PlcMemType.CR)
            {
                CTRL_REAR[addr.Addr] = value;
            }
            else
                throw new Exception("ADDR TYPE ERROR");
        }
        public override void BufferCompile(int bufIdx)
        {
            _channel.CompileBuffer(bufIdx);
        }
        public override void BufferCompile(int masterBufIdx, int slaveBufIdx)
        {
            _channel.CompileBuffer(masterBufIdx);
            _channel.CompileBuffer(slaveBufIdx);
        }
        public override bool IsBufferCompileComplete(int bufIdx)
        {
            return _channel.GetProgramState(bufIdx) == 1 && _channel.ACSC_PST_COMPILED == 1; 
        }
        public override bool IsBufferCompileComplete(int masterBufIdx, int slaveBufIdx)
        {
            return _channel.GetProgramState(masterBufIdx) == 1 && _channel.GetProgramState(slaveBufIdx) == 1 && _channel.ACSC_PST_COMPILED == 1;
        }
        public override void RunBuffer(int bufIdx)
        {
            _channel.RunBuffer(bufIdx, null);
        }
        public override void RunBuffer(int masterBufIdx, int slaveBufIdx)
        {
            _channel.RunBuffer(masterBufIdx, null);
            _channel.RunBuffer(slaveBufIdx, null);
        }
    }
}
