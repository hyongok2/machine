using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DitCim.Common
{
    public class CimConst
    {
        ////////////////////////////////////////////////////////////////////////////
        //// Index
        //Index / Body (HSMS) ModuleIDs
        public const string LD01 = "LD01";                    // Layer1 [Index Loader (LD01)]
        public const string BODY = "AOIU";                    // Layer1 [Body (AOIU)]

        public const string BP01 = "BP01"; // Layer2
        public const string BP02 = "BP02"; // Layer2
        public const string BP03 = "BP03"; // Layer2
        public const string BP04 = "BP04"; // Layer2

        public static List<string> HostPortList = new List<string>() { BP01, BP02, BP03, BP04 };
         
        public const string COURSE_STATE = "S1";
        public const string COURSE_STATE_ALIGN = "L1";
        public const string COURSE_ARM = "N0";
        public const string COURSE_EXCHANGE = "X?";

        public static List<string> RobotCmdPortIdList = new List<string>() { "P1", "P2", "P3", "P4" };
        
        public static int GetPortSeqFromHost(string hostPortID)
        {
            return HostPortList.IndexOf(hostPortID);
        }
        public static int GetPortSeqFromIndex(string indexPortID)
        {
            return RobotCmdPortIdList.IndexOf(indexPortID);
        }

        public const string PORT_TYPE_BT = "BT";
        public const string PORT_TYPE_IP = "IP";
        public const string PORT_TYPE_OP = "OP";
        public const string PORT_TYPE_BF = "BF";
        public const string PORT_TYPE_TC = "TC";
    }
}
