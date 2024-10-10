using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dit.FrameworkSingle.Net.ServerClinet
{
    [Serializable]
    public class Packet
    {
        public static readonly int OnePacketSize = 8192;
        public string PacketID { set; get; }

        public Packet()
        {
            PacketID = "ID00";
        }

        public byte[] Serialize()
        {
            Type tt = this.GetType();
            if (tt == typeof(Packet))
                return PacketSerializer.SerializeBin<Packet>(this);
            else
            {
                return PacketSerializer.SerializeBin<Packet>(this);
            }
        }
        public static Packet Deserialize(byte[] it)
        {
            return PacketSerializer.DeserializeBin<Packet>(it);
        }
    }
}
