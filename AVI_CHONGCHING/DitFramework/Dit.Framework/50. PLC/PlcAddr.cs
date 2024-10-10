using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Dit.Framework.PLC
{
    public enum PlcMemType
    {
        X = 1,
        Y = 2,
        M = 33333,
        B = 23,
        W = 24,
        D = 13,
        R = 22,
        ZR = 220,
        Ww = 36,
        Wr = 37,
        SB = 5,   //25
        SW = 14,  //28

        //PMAC
        P = 100,

        //ShardMememory
        S = 101,

        //Modbus
        MD = 1001, //Discrete Inputs
        MC = 1002, //Coils
        MI = 1003, //Input Registers
        MH = 1004, //Holding Registers        

        //ACS
        CR = 2000,
        AR = 2001,
        CI = 2002,
        AI = 2003,

		//Sysmac Gate Way 
        SG = 3000,

        //RS NMC
        RS_CI = 3000,
        RS_CR = 3001,
        RS_MI = 3002,
        RS_MR = 3003,
        
        RS_X = 3100, // digital in
        RS_Y = 3101, // digital out
        RS_AI = 3102, // analog in
        RS_AO = 3103, // analog out

        EM = 5000, //Ezi Step Motor
        ER = 5001, //Ezi Step Motor
    }
    public enum PlcValueType
    {
        SINT,
        SHORT,
        INT,
        INT32,
        ASCII,
        BIT,
        NONO,
        BCD,
        FLOAT
    }

    public class PlcAddr
    {
        public IVirtualMem PLC { get; set; }
        public PlcMemType Type { get; set; }
        public short Channel { get; set; }
        public PlcValueType ValueType { get; set; }
        public int DeviceID { get; set; }

        public int Addr { get; set; }
        public int Bit { get; set; }
        public int Length { get; set; }
        public string Desc { get; set; }

        public string MemName { get; set; }

        public PlcAddr(PlcMemType type, int addr)
            : this(type, addr, 0, 0)
        {
        }
        public PlcAddr(PlcMemType type, int addr, int bit)
            : this(type, addr, bit, 0)
        {
        }
        public PlcAddr(PlcMemType type, int addr, int bit, int length)
            : this(type, addr, bit, length, PlcValueType.BIT, (short)81)
        {
        }
        public PlcAddr(PlcMemType type, int addr, int bit, int length, PlcValueType valueType, short channel = 81)
        {
            this.Type = type;
            this.Bit = bit;
            this.Length = length;
            this.Addr = addr;
            this.ValueType = valueType;
            this.Channel = channel;
        }
        public PlcAddr(PlcMemType type, int addr, int bit, int length, PlcValueType valueType, string desc, short channel = 81)
        {
            this.Type = type;
            this.Bit = bit;
            this.Length = length;
            this.Addr = addr;
            this.ValueType = valueType;
            this.Channel = channel;
            this.Desc = desc;
        }
        public PlcAddr(string memName, PlcMemType type, int addr, int bit, int length, PlcValueType valueType, string desc)
        {
            this.Type = type;
            this.Bit = bit;
            this.Length = length;
            this.Addr = addr;
            this.ValueType = valueType;
            this.MemName = memName;
            this.Desc = desc;
        }
        public static PlcAddr operator +(PlcAddr plcAddr1, int addr)
        {
            return new PlcAddr(plcAddr1.Type, plcAddr1.Addr + addr, plcAddr1.Bit, plcAddr1.Length) { PLC = plcAddr1.PLC, MemName = plcAddr1.MemName };
        }

        public static PlcAddr operator +(PlcAddr plcAddr1, PlcAddr plcAddr2)
        {
            return new PlcAddr(plcAddr1.Type, plcAddr1.Addr + plcAddr2.Addr, plcAddr1.Bit + plcAddr2.Bit, plcAddr1.Length) { PLC = plcAddr1.PLC, MemName = plcAddr1.MemName };
        }
        public override string ToString()
        {
            if (Type == PlcMemType.B || Type == PlcMemType.W || Type == PlcMemType.P || Type == PlcMemType.EM)
            {
                return string.Format("{0}{1:X}.{2:X}-{3}", this.Type.ToString(), Addr, Bit, Length);
            }
            else if (Type == PlcMemType.D || Type == PlcMemType.R || Type == PlcMemType.ZR || Type == PlcMemType.S
                || Type == PlcMemType.CR || Type == PlcMemType.CI || Type == PlcMemType.AR || Type == PlcMemType.AI || Type == PlcMemType.ER
                )
            {
                return string.Format("{0}{1}.{2:X}-{3}", this.Type.ToString(), Addr, Bit, Length);
            }
            else
            {
                return string.Format("{0}{1:X}.{2:X}-{3}", this.Type.ToString(), Addr, Bit, Length);
            }
        }
        public string GetPlcAddressBitString()
        {
            string strAddr = string.Empty;
            if (Type == PlcMemType.RS_X || Type == PlcMemType.RS_Y)
            {
                strAddr = string.Format("{0}{1:D2}{2:D2}", Type.ToString(), Addr / 32, Addr % 32);
            }
            else if (Type == PlcMemType.B || Type == PlcMemType.X || Type == PlcMemType.Y )
            {
                strAddr = string.Format("{0}{1:X}", Type.ToString(), Addr);
            }
            else if (Type == PlcMemType.W)
            {
                strAddr = string.Format("{0}{1:X}.{2:X}", Type.ToString(), Addr);
            }
            else if (Type == PlcMemType.D || Type == PlcMemType.R || Type == PlcMemType.ZR || Type == PlcMemType.S)
            {
                strAddr = string.Format("{0}{1}.{2:X}", Type.ToString(), Addr, Bit);
            }
            else if (Type == PlcMemType.P || Type == PlcMemType.S || Type == PlcMemType.CR || Type == PlcMemType.CI || Type == PlcMemType.AR || Type == PlcMemType.AI
                || (PlcMemType.RS_CI <= Type && Type <=PlcMemType.RS_MR))
            {
                strAddr = string.Format("{0}{1}.{2:X}", Type.ToString(), Addr, Bit);
            }
            else if (Type == PlcMemType.EM || Type == PlcMemType.ER)
            {
                strAddr = string.Format("{0}{1}", Type.ToString(), Addr);
            }
            else
            {
                throw new Exception("미지정 메모리");
            }

            return strAddr;
        }
        public string GetPlcAddressWordString()
        {
            string strAddr = string.Empty;
            if (PlcMemType.RS_CI <= Type && Type <= PlcMemType.RS_MR)
            {
                strAddr = string.Format("{0}{1}", Type.ToString(), Addr);
            }
            else if (Type == PlcMemType.D || Type == PlcMemType.R || Type == PlcMemType.ZR || 
                Type == PlcMemType.CR || Type == PlcMemType.CI || Type == PlcMemType.AR || Type == PlcMemType.AI )
            {
                strAddr = string.Format("{0}{1}", Type.ToString(), Addr);
            }
            else if (Type == PlcMemType.W)
            {
                strAddr = string.Format("{0}{1:X}", Type.ToString(), Addr);
            }
            else if (Type == PlcMemType.B || Type == PlcMemType.X || Type == PlcMemType.Y
                 || Type == PlcMemType.RS_X || Type == PlcMemType.RS_Y
                                          || Type == PlcMemType.Wr || Type == PlcMemType.Ww || Type == PlcMemType.S)
            {
                strAddr = string.Format("{0}{1:X}", Type.ToString(), Addr);
            }
            else if (Type == PlcMemType.P || Type == PlcMemType.S)
            {
                strAddr = string.Format("{0}{1}", Type, Addr);
            }
            else if (Type == PlcMemType.EM || Type == PlcMemType.ER)
            {
                strAddr = string.Format("{0}{1}", Type, Addr);
            }
            else
            {
                throw new Exception("미지정 메모리");
            }

            return strAddr;
        }
        public static implicit operator bool(PlcAddr addr)
        {
            bool rt = addr.PLC.VirGetBit(addr);
            return rt;
        }

        private bool _useBitTest = false;
        public bool UseBitTest
        {
            get { return _useBitTest; }
            set
            {
                _testBit = vBit;
                _useBitTest = value;
            }
        }

        private bool _testBit = false;
        /// <summary>
        /// Test모드일 경우 testbit 변경 -> Set        
        /// </summary>
        /// <param name="bit"></param>
        public void SetTestBit(bool bit)
        {
            if (UseBitTest)
                _testBit = bit;
            else
                vBit = bit;
        }

        public bool vBit
        {
            get
            {
                if (UseBitTest)
                    return _testBit;
                else
                    return PLC.VirGetBit(this);
            }
            set
            {
                if (UseBitTest)
                    PLC.VirSetBit(this, _testBit);
                else
                    PLC.VirSetBit(this, value);
            }
        }

        public float vFloat
        {
            get { return PLC.VirGetFloat(this); }
            set
            {

                PLC.VirSetFloat(this, value);
            }
        }
        public int vInt
        {
            get { return PLC.VirGetInt32(this); }
            set { PLC.VirSetInt32(this, value); }
        }
        public byte vByte
        {
            get { return PLC.VirGetByte(this); }
            set { PLC.VirSetByte(this, value); }
        }
        public short vShort
        {
            get { return PLC.VirGetShort(this); }
            set { PLC.VirSetShort(this, value); }
        }
        public string vAscii
        {
            get { return PLC.VirGetAscii(this); }
            set { PLC.VirSetAscii(this, value); }
        }

        public static PlcAddr Parsing(string strAddr, short channel = 81)
        {
            PlcAddr addr = null;
            if (strAddr[0] == 'R' && strAddr[1] == 'S')
            {
                if (strAddr[3] == 'X')
                {
                    int moduleOrder = int.Parse(strAddr.Substring(4, 2));
                    int ioAddr = int.Parse(strAddr.Substring(6, 2));

                    //Console.WriteLine("X:{0}", moduleOrder * 32 + ioAddr);
                    addr = new PlcAddr(PlcMemType.RS_X, moduleOrder * 32 + ioAddr, 0, 1, PlcValueType.BIT, "", channel);
                    //addr = new PlcAddr(PlcMemType.X, int.Parse(strAddr.Substring(1), NumberStyles.HexNumber), 0, 1, PlcValueType.BIT, "", channel);
                }
                else if (strAddr[3] == 'Y')
                {
                    int moduleOrder = int.Parse(strAddr.Substring(4, 2));
                    int ioAddr = int.Parse(strAddr.Substring(6, 2));

                    //Console.WriteLine("Y:{0}", moduleOrder * 32 + ioAddr);
                    addr = new PlcAddr(PlcMemType.RS_Y, moduleOrder * 32 + ioAddr, 0, 1, PlcValueType.BIT, "", channel);
                    //addr = new PlcAddr(PlcMemType.Y, int.Parse(strAddr.Substring(1), NumberStyles.HexNumber), 0, 1, PlcValueType.BIT, "", channel);
                }
                else if (strAddr[3] == 'C' && strAddr[4] == 'R')
                {
                    addr = new PlcAddr(PlcMemType.RS_CR, int.Parse(strAddr.Substring(5), NumberStyles.Integer), 0, 1, PlcValueType.FLOAT, "", channel);
                }
                else if (strAddr[3] == 'C' && strAddr[4] == 'I')
                {
                    addr = new PlcAddr(PlcMemType.RS_CI, int.Parse(strAddr.Substring(5), NumberStyles.Integer), 0, 1, PlcValueType.INT32, "", channel);
                }
                else if (strAddr[3] == 'M' && strAddr[4] == 'R')
                {
                    addr = new PlcAddr(PlcMemType.RS_MR, int.Parse(strAddr.Substring(5), NumberStyles.Integer), 0, 1, PlcValueType.FLOAT, "", channel);
                }
                else if (strAddr[3] == 'M' && strAddr[4] == 'I')
                {
                    addr = new PlcAddr(PlcMemType.RS_MI, int.Parse(strAddr.Substring(5), NumberStyles.Integer), 0, 1, PlcValueType.INT32, "", channel);
                }
                else if (strAddr[3] == 'A' && strAddr[4] == 'I')
                {
                    addr = new PlcAddr(PlcMemType.RS_AI, int.Parse(strAddr.Substring(5), NumberStyles.HexNumber), 0, 1, PlcValueType.FLOAT, "", channel);
                }
                else if (strAddr[3] == 'A' && strAddr[4] == 'O')
                {
                    addr = new PlcAddr(PlcMemType.RS_AO, int.Parse(strAddr.Substring(5), NumberStyles.HexNumber), 0, 1, PlcValueType.FLOAT, "", channel);
                }
                else
                    throw new Exception(string.Format("{0}은 미지정 PLC MEM TYPE ", strAddr));
            }
            else if (strAddr[0] == 'X')
            {
                addr = new PlcAddr(PlcMemType.X, int.Parse(strAddr.Substring(1), NumberStyles.HexNumber), 0, 1, PlcValueType.BIT, "", channel);
            }
            else if (strAddr[0] == 'Y')
            {
                addr = new PlcAddr(PlcMemType.Y, int.Parse(strAddr.Substring(1), NumberStyles.HexNumber), 0, 1, PlcValueType.BIT, "", channel);
            }            
            else if (strAddr[0] == 'D')
            {
                addr = new PlcAddr(PlcMemType.D, int.Parse(strAddr.Substring(1), NumberStyles.HexNumber), 0, 1, PlcValueType.BIT, "", channel);
            }
            else if (strAddr[0] == 'R')
            {
                addr = new PlcAddr(PlcMemType.R, int.Parse(strAddr.Substring(1), NumberStyles.HexNumber), 0, 1, PlcValueType.BIT, "", channel);
            }
            else if (strAddr[0] == 'S')
            {
                string str = strAddr.Substring(1);
                int memAddr = int.Parse(str.IndexOf('.') < 0 ? str : str.Substring(0, str.IndexOf('.')));
                int memBit = int.Parse(str.IndexOf('.') < 0 ? "0" : str.Substring(str.IndexOf('.') + 1));

                addr = new PlcAddr(PlcMemType.S, memAddr, memBit, 1, PlcValueType.BIT, "", channel);
            }
            else if (strAddr[0] == 'P')
            {
                string str = strAddr.Substring(1);
                int memAddr = int.Parse(str.IndexOf('.') < 0 ? str : str.Substring(0, str.IndexOf('.')));
                int memBit = int.Parse(str.IndexOf('.') < 0 ? "0" : str.Substring(str.IndexOf('.') + 1));
                addr = new PlcAddr(PlcMemType.P, memAddr, memBit, 1, PlcValueType.BIT, "", channel);
            }
            else if (strAddr[0] == 'E' && strAddr[1] == 'M')
            {
                string str = strAddr.Substring(2);
                int memAddr = int.Parse(str.IndexOf('.') < 0 ? str : str.Substring(0, str.IndexOf('.')));
                int memBit = int.Parse(str.IndexOf('.') < 0 ? "0" : str.Substring(str.IndexOf('.') + 1));
                addr = new PlcAddr(PlcMemType.EM, memAddr, memBit, 1, PlcValueType.BIT, "", channel);
                
                //string[] split = strAddr.Split('.');
                //addr = new PlcAddr(PlcMemType.EM, int.Parse(split[0].Substring(2)), 0, 1, PlcValueType.BIT, "", channel);
            }
            else if (strAddr[0] == 'E' && strAddr[1] == 'R')
            {
                string str = strAddr.Substring(2);
                int memAddr = int.Parse(str.IndexOf('.') < 0 ? str : str.Substring(0, str.IndexOf('.')));
                int memBit = int.Parse(str.IndexOf('.') < 0 ? "0" : str.Substring(str.IndexOf('.') + 1));
                addr = new PlcAddr(PlcMemType.ER, memAddr, memBit, 1, PlcValueType.BIT, "", channel);

                //string[] split = strAddr.Split('.');
                //addr = new PlcAddr(PlcMemType.ER, int.Parse(split[0].Substring(2)), 0, 1, PlcValueType.FLOAT, "", channel);
            }
            else if (strAddr[0] == 'W' && strAddr[1] == 'r')
            {
                addr = new PlcAddr(PlcMemType.Wr, int.Parse(strAddr.Substring(2), NumberStyles.HexNumber), 0, 1, PlcValueType.BIT, "", channel);
            }
            else if (strAddr[0] == 'W' && strAddr[1] == 'w')
            {
                addr = new PlcAddr(PlcMemType.Ww, int.Parse(strAddr.Substring(2), NumberStyles.HexNumber), 0, 1, PlcValueType.BIT, "", channel);
            }
            else if (strAddr[0] == 'C' && strAddr[1] == 'R')
            {
                addr = new PlcAddr(PlcMemType.CR, int.Parse(strAddr.Substring(2), NumberStyles.Integer), 0, 1, PlcValueType.FLOAT, "", channel);
            }
            else if (strAddr[0] == 'C' && strAddr[1] == 'I')
            {
                addr = new PlcAddr(PlcMemType.CI, int.Parse(strAddr.Substring(2), NumberStyles.Integer), 0, 1, PlcValueType.INT32, "", channel);
            }
            else if (strAddr[0] == 'A' && strAddr[1] == 'R')
            {
                addr = new PlcAddr(PlcMemType.AR, int.Parse(strAddr.Substring(2), NumberStyles.Integer), 0, 1, PlcValueType.FLOAT, "", channel);
            }
            else if (strAddr[0] == 'A' && strAddr[1] == 'I')
            {
                addr = new PlcAddr(PlcMemType.AI, int.Parse(strAddr.Substring(2), NumberStyles.Integer), 0, 1, PlcValueType.INT32, "", channel);
            }

            else
                throw new Exception(string.Format("{0}은 미지정 PLC MEM TYPE ", strAddr[0]));

            return addr;
        }
        public PlcAddr Clone()
        {
            PlcAddr addr = new PlcAddr(this.Type, this.Addr, this.Bit, this.Length, this.ValueType, this.Desc, this.Channel)
            {
                PLC = this.PLC,
                MemName = this.MemName
            };
            return addr;
        }

        public PlcAddr AddBit(int bitSeq, int bitCount)
        {
            PlcAddr addr = this.Clone();
            int word = bitSeq / bitCount;
            int bit = bitSeq % bitCount;

            addr.Addr = addr.Addr + word;
            addr.Bit = addr.Bit + bit;

            if (addr.Bit >= bitCount)
            {
                addr.Addr = addr.Addr + 1;
                addr.Bit = addr.Bit - bitCount;
            }
            return addr;
        }

        public PlcAddr AddWord(int wordSeq)
        {
            PlcAddr addr = this.Clone();
            addr.Addr = addr.Addr + wordSeq;

            return addr;
        }
    }
}
