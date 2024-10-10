using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Dit.FrameworkSingle.Net.ServerClinet
{
    public class TcpClinetSessionManager
    {
        public SortedList<string, TcpClinetSession> EquipMonitors { get; set; }
        private static TcpClinetSessionManager _sefInstance = null;
        private Timer _tmrAliveEquip = new Timer(5000);


        private TcpClinetSessionManager()
        {
            EquipMonitors = new SortedList<string, TcpClinetSession>();
            _tmrAliveEquip.Elapsed += new ElapsedEventHandler(TmrAliveEquip_Elapsed);

        }
        public static TcpClinetSessionManager GetInstance()
        {
            if (_sefInstance == null)
                _sefInstance = new TcpClinetSessionManager();

            return _sefInstance;
        }
        public bool RegistEquipMonitor(string equip, TcpServerSession clientMonitor)
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
        public bool UnregistEquipMonitor(string equip, TcpServerSession clientMonitor)
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
                foreach (TcpClinetSession equipMonitor in EquipMonitors.Values)
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
                foreach (TcpClinetSession equipMonitor in EquipMonitors.Values)
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
