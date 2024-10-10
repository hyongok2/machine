using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.FrameworkSingle.Net.ServerClinet;

namespace DitSharedMemoryPacket
{
    [Serializable]
    public class PkSyncTest : Packet
    {
        public DateTime ReqTime { get; set; }
        public string Name { get; set; }       
    }
}
