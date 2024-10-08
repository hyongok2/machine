using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Dit.FrameworkSingle.Net.DNet
{
    public class DNetClinetSessionManager
    {
        public SortedList<string, DNetClinetSession> EquipMonitors { get; set; }
        private static DNetClinetSessionManager _sefInstance = null;
        private Timer _tmrAliveEquip = new Timer(5000);


        private DNetClinetSessionManager()
        {
            EquipMonitors = new SortedList<string, DNetClinetSession>();
            _tmrAliveEquip.Elapsed += new ElapsedEventHandler(TmrAliveEquip_Elapsed);

        }
        public static DNetClinetSessionManager GetInstance()
        {
            if (_sefInstance == null)
                _sefInstance = new DNetClinetSessionManager();

            return _sefInstance;
        }
        public bool RegistEquipMonitor(string equip, DNetServerSession clientMonitor)
        {
            //lock (EquipMonitors)
            //{
            //    if (EquipMonitors.ContainsKey(equip))
            //    {
            //        if (EquipMonitors[equip].Observers.Count < EquipMonitors[equip].MaxClientCount)
            //        {
            //            EquipMonitors[equip].Observers.Add(clientMonitor);
            //        }
            //        else
            //        {
            //            //clientMonitor.SendOverMaxClientCount();
            //        }

            //        return true;
            //    }
            //    else
            //    {
            //        //clientMonitor.SendEqpIdNotFound();
            //        return false;
            //    }
            //}
            return false;
        }
        public bool UnregistEquipMonitor(string equip, DNetServerSession clientMonitor)
        {
            //lock (EquipMonitors)
            //{
            //    if (EquipMonitors.ContainsKey(equip))
            //    {
            //        EquipMonitors[equip].Observers.Remove(clientMonitor);
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
                    return false;
        }
        public void Start()
        {

            lock (EquipMonitors)
            {
                foreach (DNetClinetSession equipMonitor in EquipMonitors.Values)
                {
                    equipMonitor.Start();
                    System.Threading.Thread.Sleep(10);
                }
                _tmrAliveEquip.Interval = 100;
                _tmrAliveEquip.Start();
            }
        }
        public void Stop()
        {
            lock (EquipMonitors)
            {
                foreach (DNetClinetSession equipMonitor in EquipMonitors.Values)
                {
                    equipMonitor.Stop();
                } 
                _tmrAliveEquip.Stop();
            }
        }
        private void TmrAliveEquip_Elapsed(object sender, ElapsedEventArgs e)
        {
            //lock (EquipMonitors)
            //{
            //    EquipMonitors.Values.ToList().ForEach(f =>
            //    {
            //        //f.SendAliveEquip();
            //    });
            //}
        }
        public void SendCmdToDetectAgent(string eqpid, string cmd)
        {
            //lock (EquipMonitors)
            //{
            //    EquipMonitors.Values
            //        .Where(f => f.EqpID == eqpid)
            //        .ToList()
            //        .ForEach(f =>
            //    {
            //        //f.SendCmdToDetectAgent(cmd);
            //    });
            //}
        }
    }
}
