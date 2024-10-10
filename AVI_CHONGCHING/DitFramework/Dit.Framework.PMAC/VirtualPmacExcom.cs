using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;

namespace Dit.Framework.PMAC
{
    /// <summary>
    /// brief   Pmac Excom 용 Virtual
    /// since   19.04.05
    /// </summary>
    public class VirtualPmacExcom : VirtualUMac
    {
        PmacExcom _excom;
        public VirtualPmacExcom(string name, string ip, int port)
            : base(name, ip, port)
        {
            _excom = new PmacExcom(ip, port);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>0 = OK</returns>
        public override int Open()
        {
            return _excom.Open() ? 0 : 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>0 = OK</returns>
        public override int Close()
        {
            return _excom.Close() ? 0 : 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="wordSize"></param>
        /// <returns>0 = OK</returns>
        public override int ReadFromPLC(PlcAddr addr, int wordSize)
        {
            int byteStart = addr.Addr * ONE_BYTE_SIZE;
            int byteSize = wordSize * ONE_BYTE_SIZE;
            byte[] readBuffer = new byte[byteSize];

            if (_excom.GetMemory(byteStart, byteSize, out readBuffer) == false)
                return 1;

            Array.Copy(readBuffer, 0, BYTE_W, byteStart, byteSize);

            return 0;
        }

        public override int WriteToPLC(PlcAddr addr, int wordSize)
        {
            int byteStart = addr.Addr * ONE_BYTE_SIZE;
            int byteSize = wordSize * ONE_BYTE_SIZE;
            byte[] writeBuffer = new byte[byteSize];

            Array.Copy(BYTE_W, byteStart, writeBuffer, 0, byteSize);

            if (_excom.SetMemory(byteStart, byteSize, writeBuffer) == false)
                return 1;

            return 0;
        }

        public override float VirGetFloat(PlcAddr addr)
        {
            byte[] intbyte = new byte[4];
            Array.Copy(BYTE_W, addr.Addr * 4, intbyte, 0, 4);
            //jys:: 스왑하면 안됨, intbyte = Swap4Byte(intbyte);
            return BitConverter.ToSingle(intbyte, 0);
        }
        public override void VirSetFloat(PlcAddr addr, float value)
        {
            byte[] intbyte = BitConverter.GetBytes(value);
            //jys:: 스왑하면 안됨, intbyte = Swap4Byte(intbyte);
            Array.Copy(intbyte, 0, BYTE_W, addr.Addr * 4, 4);
        }
        public override int VirGetInt32(PlcAddr addr)
        {
            byte[] intbyte = new byte[4];
            Array.Copy(BYTE_W, addr.Addr * 4, intbyte, 0, 4);
            //jys:: 스왑하면 안됨, intbyte = Swap4Byte(intbyte);
            return BitConverter.ToInt32(intbyte, 0);
        }
        public override void VirSetInt32(PlcAddr addr, int value)
        {
            byte[] intbyte = BitConverter.GetBytes(value);
            //jys:: 스왑하면 안됨, intbyte = Swap4Byte(intbyte);
            Array.Copy(intbyte, 0, BYTE_W, addr.Addr * 4, 4);
        }
    }
}
