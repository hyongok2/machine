using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DitSharedMemoryClient
{
    public class SMSyncClientMgr
    {
        public List<SMSyncSession> LstSession { get; set; }
        public SMSyncClientMgr()
        {
            LstSession = new List<SMSyncSession>();
        }
        public void Start()
        {
            LstSession.ForEach(f => f.Start());
        }
        public void Stop()
        {
            LstSession.ForEach(f => f.Stop());
        }
    }
}
