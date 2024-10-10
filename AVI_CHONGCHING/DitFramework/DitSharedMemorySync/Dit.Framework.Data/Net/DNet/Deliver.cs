using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dit.FrameworkSingle.Net.ServerClinet
{
    public class Deliver
    {
        public int DeilverSize { get; set; }
        public string PacketID { get; set; }
        public byte[] PacketBuffer { get; set; }
        public byte[] AllBuffer { get; set; }

        public bool SetDeiver(string packetid, byte[] buff)
        {
            PacketID = packetid;
            PacketBuffer = new byte[buff.Length];
            Array.Copy(buff, PacketBuffer, buff.Length);
            DeilverSize = 1 + sizeof(Int32) + 4 + PacketBuffer.Length + 1;

            AllBuffer = new byte[DeilverSize];
            AllBuffer[0] = 0x02;
            AllBuffer[DeilverSize - 1] = 0x03;

            Array.Copy(BitConverter.GetBytes(DeilverSize), 0, AllBuffer, 1, 4);
            Array.Copy(Encoding.ASCII.GetBytes(PacketID), 0, AllBuffer, 5, 4);
            Array.Copy(PacketBuffer, 0, AllBuffer, 9, PacketBuffer.Length);

            return true;
        }

        public bool SetDeiver(byte[] deilverbuffer)
        {
            if (deilverbuffer.Length < 8)
                return false;

            AllBuffer = deilverbuffer;
            DeilverSize = BitConverter.ToInt32(deilverbuffer, 1);
            PacketID = Encoding.ASCII.GetString(deilverbuffer, 5, 4);

            PacketBuffer = new byte[DeilverSize - 10];
            Array.Copy(AllBuffer, 9, PacketBuffer, 0, PacketBuffer.Length);
            return true;
        }
    }
}
