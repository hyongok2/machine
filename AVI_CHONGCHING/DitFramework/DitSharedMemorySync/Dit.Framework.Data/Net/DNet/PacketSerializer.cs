using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Dit.FrameworkSingle.Net.ServerClinet
{
    public class PacketSerializer
    {
        public static byte[] SerializeBin<T>(T it)
        {
            BinaryFormatter binFmt = new BinaryFormatter();
            using (MemoryStream fs = new MemoryStream())
            {
                binFmt.Serialize(fs, it);
                return fs.ToArray();
            }
        }

        public static T DeserializeBin<T>(byte[] bin)
        {
            T p;
            BinaryFormatter binFmt = new BinaryFormatter(); ;
            using (MemoryStream rdr = new MemoryStream(bin, false))
            {
                p = (T)binFmt.Deserialize(rdr);
            }
            return p;
        }
    }
}
