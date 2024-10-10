using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.MemoryMappedFiles;
using System.Windows.Forms;
using System.IO;
using DitSharedMemorySvr.Log;
using Dit.Framework.Log;
using DitSharedMemoryPacket;

namespace DitSharedMemorySvr
{
   
    public class ShardMemMgr
    {
        public SortedList<string, ShardMem> LstShardMemory { get; set; }

        public ShardMemMgr()
        {
            LstShardMemory = new SortedList<string, ShardMem>();
        }
        public void Open()
        {
            Logger.FileLogger.AppendLine(LogLevel.Info, "Shard Memory Manager Open");
            foreach (string name in LstShardMemory.Keys)
            {
                Logger.FileLogger.AppendLine(LogLevel.Info, "Shard Memory Open {0}, {1}", LstShardMemory[name].Name, LstShardMemory[name].Size);
                LstShardMemory[name].Open();
            }
        }
        public void Close()
        {
            foreach (string name in LstShardMemory.Keys)
            {
                LstShardMemory[name].Close();
                Logger.FileLogger.AppendLine(LogLevel.Info, "Shard Memory Close {0}, {1}", LstShardMemory[name].Name, LstShardMemory[name].Size);
            }

            Logger.FileLogger.AppendLine(LogLevel.Info, "Shard Memory Manager Closed");
        }
    }
}
