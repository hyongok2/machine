using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Dit.Framework.PLC;
using System.Text.RegularExpressions;

namespace Dit.Framework.Comm
{
    public class AddressMgr
    {
        public static List<PlcAddr> LstMasterPCPlcAddr = new List<PlcAddr>();
        public static List<PlcAddr> LstRsWorkerPlcAddr = new List<PlcAddr>();
        public static List<PlcAddr> LstSimulatorPCPlcAddr = new List<PlcAddr>();
        public static List<PlcAddr> LstTesterPlcAddr = new List<PlcAddr>();
        public static PlcAddr GetAddress(string name)
        {

            PlcAddr first = LstMasterPCPlcAddr.FirstOrDefault(f => f.Desc.Split('\t').Contains(name));
            if (first == null)
            {
                first = new PlcAddr(PlcMemType.X, 0, 0, 1, PlcValueType.BIT, "NONE", (short)81);
                Console.WriteLine(name);
            }
            return first;
        }
        public static PlcAddr GetAddress(string strAddr, short channel = 81)
        {

            PlcAddr findAddr = PlcAddr.Parsing(strAddr, channel);

            PlcAddr plcAddr = LstMasterPCPlcAddr.FirstOrDefault(f =>
                    f.Type == findAddr.Type && f.Addr == findAddr.Addr && f.Bit == findAddr.Bit && f.Length == findAddr.Length && f.Channel == findAddr.Channel);
            return plcAddr;
        }
        //Address 할당후 Bit 변경
        public static PlcAddr GetAddress(string name, int bit)
        {
            PlcAddr first = LstMasterPCPlcAddr.FirstOrDefault(f => f.Desc.Split('\t').Contains(name));
            if (first == null)
            {
                first = new PlcAddr(PlcMemType.X, 0, 0, 1, PlcValueType.BIT, "NONE");
                Console.WriteLine(name);
                return first;
            }
            PlcAddr clone = new PlcAddr(first.Type, first.Addr, bit, 1, PlcValueType.BIT, string.Format("{0}_{1}", name, bit));
            clone.PLC = first.PLC;
            return clone;
        }
        public static PlcAddr GetRsWorkerAddress(string name)
        {
            PlcAddr first = LstRsWorkerPlcAddr.FirstOrDefault(f => f.Desc.Split('\t').Contains(name));
            if (first == null)
            {
                first = new PlcAddr(PlcMemType.X, 0, 0, 1, PlcValueType.BIT, "NONE", (short)81);
                Console.WriteLine(name);
            }
            return first;
        }
        public static PlcAddr GetRsWorkerAddress(string name, int bit)
        {
            PlcAddr first = LstRsWorkerPlcAddr.FirstOrDefault(f => f.Desc.Split('\t').Contains(name));
            if (first == null)
            {
                first = new PlcAddr(PlcMemType.X, 0, 0, 1, PlcValueType.BIT, "NONE");
                Console.WriteLine(name);
                return first;
            }
            PlcAddr clone = new PlcAddr(first.Type, first.Addr, bit, 1, PlcValueType.BIT, string.Format("{0}_{1}", name, bit));
            clone.PLC = first.PLC;
            return clone;
        }
        public static void LoadRS(string addressLine, IVirtualMem plc)
        {
            int iPos = 0;
            string[] lines = addressLine.Split((char)10, (char)13);
            foreach (string line in lines)
            {
                iPos++;
                if (line.Length <= 5) continue;

                string[] items = line.Split('\t');

                if (items.Length != 4) continue;
                if (items[0].Length <= 1) continue;
                if (items[1].Length <= 1) continue;

                PlcAddr addr = PlcAddr.Parsing(items[0]);
                addr.Desc = items[3];
                addr.PLC = plc;
                LstRsWorkerPlcAddr.Add(addr);
            }
        }
        public static void Load(string addressLine, IVirtualMem plc, short channel = 81)
        {
            int iPos = 0;
            string[] lines = addressLine.Split((char)10, (char)13);
            foreach (string line in lines)
            {
                iPos++;
                if (line.Length <= 5) continue;

                string[] items = line.Split('\t');

                if (items.Length != 4) continue;
                if (items[0].Length <= 1) continue;
                if (items[1].Length <= 1) continue;

                PlcAddr addr = PlcAddr.Parsing(items[0]);
                addr.Desc = items[3];
                addr.PLC = plc;
                addr.Channel = channel;
                LstMasterPCPlcAddr.Add(addr);
            }
        }

        public static PlcAddr[] FindIO(string desc, int resultCount)
        {
            var s = (from f in LstMasterPCPlcAddr
                     where Regex.IsMatch(f.Desc, desc, RegexOptions.IgnoreCase)
                     select f).Take(resultCount);
            return s.ToArray();
        }
    }
}
