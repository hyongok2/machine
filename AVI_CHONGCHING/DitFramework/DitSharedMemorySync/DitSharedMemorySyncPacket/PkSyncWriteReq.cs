using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.FrameworkSingle.Net.ServerClinet;

namespace DitSharedMemoryPacket
{
    [Serializable]
    public class PkSyncWriteReq : Packet
    {
        public DateTime ReqTime { get; set; }
        public string Name { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public byte[] WriteBytes { get; set; }
    }
}

