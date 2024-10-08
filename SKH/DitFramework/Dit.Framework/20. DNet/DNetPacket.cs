using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Dit.Framework.Comm;

namespace Dit.FrameworkSingle.Net.DNet
{ 
    public class DNetPacket
    {
        public static int PACKET_HEADERSIZE = (sizeof(int) * 3 + sizeof(short) * 4);
        public static int PACKET_MAXSIZE = 8192 * 10;
        public static int MAX_BUFFER_SIZE = 8192 * 100;
        private byte[] buff;

        public DNetPacket()
        {
            ShortValues = new List<short>();
            IntValues = new List<int>();
            DoubleValues = new List<double>();
            StringValues = new List<string>();

        }
        public DNetPacket(byte[] buff)
            : this()
        {
            this.buff = buff;

            int pkHeaderSize = 0;
            this.ParsingHeaderBuffer(buff, ref pkHeaderSize);
            this.ParsingBodyBuffer(buff, pkHeaderSize);


        }

        public int PacketSize { get; set; }
        public short NetworkCode { get; set; }
        public short Version { get; set; }
        public int PacketCode { get; set; }
        public short PacketIndex { get; set; }
        public short Result { get; set; }
        public int ModuleNo { get; set; }

        public List<short> ShortValues { get; set; }
        public List<int> IntValues { get; set; }
        public List<Double> DoubleValues { get; set; }
        public List<string> StringValues { get; set; }

        public byte[] GetBuffer()
        {
            int writePos = 0;
            byte[] buff = new byte[MAX_BUFFER_SIZE];

            //Packet Size
            Array.Copy(BitConverter.GetBytes(0), 0, buff, writePos, sizeof(int));
            writePos += sizeof(int);

            //Network Code
            Array.Copy(BitConverter.GetBytes(NetworkCode), 0, buff, writePos, sizeof(short));
            writePos += sizeof(short);

            //Version
            Array.Copy(BitConverter.GetBytes(Version), 0, buff, writePos, sizeof(short));
            writePos += sizeof(short);

            //Network Code
            Array.Copy(BitConverter.GetBytes(PacketCode), 0, buff, writePos, sizeof(int));
            writePos += sizeof(int);

            //PacketIndex 
            Array.Copy(BitConverter.GetBytes(PacketIndex), 0, buff, writePos, sizeof(short));
            writePos += sizeof(short);

            //Result Code
            Array.Copy(BitConverter.GetBytes(Result), 0, buff, writePos, sizeof(short));
            writePos += sizeof(short);

            //ModuleNo Code
            Array.Copy(BitConverter.GetBytes(ModuleNo), 0, buff, writePos, sizeof(int));
            writePos += sizeof(int);

            //SHORT
            Array.Copy(BitConverter.GetBytes(ShortValues.Count), 0, buff, writePos, sizeof(int));
            writePos += sizeof(int);
            for (int iPos = 0; iPos < ShortValues.Count; iPos++)
            {
                Array.Copy(BitConverter.GetBytes(ShortValues[iPos]), 0, buff, writePos, sizeof(short));
                writePos += sizeof(short);
            }

            //INT
            Array.Copy(BitConverter.GetBytes(IntValues.Count), 0, buff, writePos, sizeof(int));
            writePos += sizeof(int);
            for (int iPos = 0; iPos < IntValues.Count; iPos++)
            {
                Array.Copy(BitConverter.GetBytes(IntValues[iPos]), 0, buff, writePos, sizeof(int));
                writePos += sizeof(int);
            }

            //DOUBLE
            Array.Copy(BitConverter.GetBytes(DoubleValues.Count), 0, buff, writePos, sizeof(int));
            writePos += sizeof(int);
            for (int iPos = 0; iPos < DoubleValues.Count; iPos++)
            {
                Array.Copy(BitConverter.GetBytes(DoubleValues[iPos]), 0, buff, writePos, sizeof(double));
                writePos += sizeof(double);
            }

            //STRING
            Array.Copy(BitConverter.GetBytes(StringValues.Count), 0, buff, writePos, sizeof(int));
            writePos += sizeof(int);
            for (int iPos = 0; iPos < StringValues.Count; iPos++)
            {
                byte[] bytes = Encoding.ASCII.GetBytes(StringValues[iPos]);
                Array.Copy(BitConverter.GetBytes(bytes.Length), 0, buff, writePos, sizeof(int));
                writePos += sizeof(int);

                Array.Copy(bytes, 0, buff, writePos, bytes.Length);
                writePos += bytes.Length;
            }


            Array.Copy(BitConverter.GetBytes(PacketSize), 0, buff, writePos, sizeof(int));
            writePos += sizeof(int);


            //패킷 사이즈 작성
            Array.Copy(BitConverter.GetBytes(writePos), 0, buff, 0, sizeof(int));

            if (PacketSize > 0)
            {
                //Array.Copy(BitConverter.GetBytes(PacketSize), 0, buff, writePos, sizeof(int));                
            }


            byte[] result = new byte[writePos];
            Array.Copy(buff, result, writePos);

            //PK SIZE 
            return result;
        }

        public ComResult ParsingHeaderBuffer(byte[] buff, ref int headerSize)
        {
            int readSize = 0;

            this.PacketSize = BitConverter.ToInt32(buff, readSize);
            readSize += sizeof(int);

            this.NetworkCode = BitConverter.ToInt16(buff, readSize);
            readSize += sizeof(short);

            this.Version = BitConverter.ToInt16(buff, readSize);
            readSize += sizeof(short);

            this.PacketCode = BitConverter.ToInt32(buff, readSize);
            readSize += sizeof(int);

            this.PacketIndex = BitConverter.ToInt16(buff, readSize);
            readSize += sizeof(short);

            this.Result = BitConverter.ToInt16(buff, readSize);
            readSize += sizeof(short);

            this.ModuleNo = BitConverter.ToInt32(buff, readSize);
            readSize += sizeof(int);

            headerSize = readSize;
            return Dit.Framework.Comm.ComResult.Success;
        }
        private ComResult ParsingBodyBuffer(byte[] buff, int pkHeaderSize)
        {
            int readSize = pkHeaderSize;
            int vCount = 0;
            //SHORT
            vCount = BitConverter.ToInt32(buff, readSize);
            readSize += sizeof(int);
            for (int iPos = 0; iPos < vCount; iPos++)
            {
                ShortValues.Add(BitConverter.ToInt16(buff, readSize));
                readSize += sizeof(short);
            }

            //INT
            vCount = BitConverter.ToInt32(buff, readSize);
            readSize += sizeof(int);
            for (int iPos = 0; iPos < vCount; iPos++)
            {
                IntValues.Add(BitConverter.ToInt32(buff, readSize));
                readSize += sizeof(int);
            }

            //DOUBLE
            vCount = BitConverter.ToInt32(buff, readSize);
            readSize += sizeof(int);
            for (int iPos = 0; iPos < vCount; iPos++)
            {
                DoubleValues.Add(BitConverter.ToDouble(buff, readSize));
                readSize += sizeof(double);
            }

            //STRING
            vCount = BitConverter.ToInt32(buff, readSize);
            readSize += sizeof(int);

            for (int iPos = 0; iPos < vCount; iPos++)
            {
                int readStringSize = BitConverter.ToInt32(buff, readSize);
                readSize += sizeof(int);
                string str = Encoding.ASCII.GetString( buff, readSize, readStringSize);

                StringValues.Add(str);
                readSize += readStringSize;
            }
            
            //Array.Copy(BitConverter.GetBytes(PacketSize), 0, buff, writePos, sizeof(int));
            //writePos += sizeof(int);
            return ComResult.Success;
        }
    }
}
