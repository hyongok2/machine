using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using EquipMainUi.Struct;

namespace EquipMainUi.Monitor
{
    public enum EM_TT_LST
    {
        // Loading
        T000_PIO_SEND_WAIT_START,
        T000_PIO_SEND_WAIT_END,
        T010_PIO_RECEIVE_ACTUAL_START,
        T010_PIO_RECEIVE_ACTUAL_END,

        T020_LIFT_PIN_DOWN_START,
        T020_LIFT_PIN_DOWN_END,
        
        T030_VACUUM_ON_START,
        T030_VACUUM_ON_END,
        
        T040_ALIGN_POS_MOVE_START,
        T040_ALIGN_POS_MOVE_END,
        T050_ALIGN_START,
        T050_ALIGN_END,

        T060_SCAN_START_POS_START,
        T060_SCAN_START_POS_END,

        T070_SCAN_RUN_START,
        T070_SCAN_RUN_END,
        
        T080_REVIEW_WAIT_POS_START,
        T080_REVIEW_WAIT_POS_END,

        T090_REVIEW_RUN_START,
        T090_REVIEW_RUN_END,

        T100_MANUAL_REVIEW_START,
        T100_MANUAL_REVIEW_END,

        T110_POS_UNLOADING_START,
        T110_POS_UNLOADING_END,
        T120_VACUUM_OFF_START,
        T120_VACUUM_OFF_END,

        T130_LIFT_PIN_UP_START,
        T130_LIFT_PIN_UP_END,

        T140_ALL_CENTERING_START,
        T140_ALL_CENTERING_END,
        T150_ALL_UNCENTERING_START,
        T150_ALL_UNCENTERING_END,

        T160_PIO_SEND_WAIT_START,
        T160_PIO_SEND_WAIT_END,

        T170_PIO_SEND_ACTUAL_START,
        T170_PIO_SEND_ACTUAL_END,
    }
    public class TactTimeSpan
    {
        public TactTimeSpan()
        { }
        public TactTimeSpan(string name, EM_TT_LST start, EM_TT_LST end)
        {
            this.Name = name;
            this.Start = start;
            this.End = end;
        }
        public string Name { get; set; }
        public EM_TT_LST Start { get; set; }
        public EM_TT_LST End { get; set; }
    }
    public class TactTimeMgr
    {
        private const int HIST_COUNT = 10;
        public List<Dictionary<EM_TT_LST, DateTime>> TacktimeHist = new List<Dictionary<EM_TT_LST, DateTime>>();
        public List<string> ListWaferID = new List<string>();
        private static TactTimeMgr _selfInstance = null;

        public List<TactTimeSpan> LstMainTactTimeSpan = new List<TactTimeSpan>()
        {
            new TactTimeSpan("PIO Receive Wait",		    /**/EM_TT_LST.T000_PIO_SEND_WAIT_START,	            /**/EM_TT_LST.T000_PIO_SEND_WAIT_END),
            new TactTimeSpan("PIO Receive Actual Time",     /**/EM_TT_LST.T010_PIO_RECEIVE_ACTUAL_START ,       /**/EM_TT_LST.T010_PIO_RECEIVE_ACTUAL_END),

            new TactTimeSpan("Lift Pin Down",		        /**/EM_TT_LST.T020_LIFT_PIN_DOWN_START,	            /**/EM_TT_LST.T020_LIFT_PIN_DOWN_END),         
            new TactTimeSpan("Pad Vacuum On",		        /**/EM_TT_LST.T030_VACUUM_ON_START,	                /**/EM_TT_LST.T030_VACUUM_ON_END),

            new TactTimeSpan("Align Position Move",		    /**/EM_TT_LST.T040_ALIGN_POS_MOVE_START,	        /**/EM_TT_LST.T040_ALIGN_POS_MOVE_END),
            new TactTimeSpan("Vision Align",		        /**/EM_TT_LST.T050_ALIGN_START,	                    /**/EM_TT_LST.T050_ALIGN_END),

            new TactTimeSpan("Scan Start Position Move",	/**/EM_TT_LST.T060_SCAN_START_POS_START,	        /**/EM_TT_LST.T060_SCAN_START_POS_END),
            new TactTimeSpan("Scan Run",		            /**/EM_TT_LST.T070_SCAN_RUN_START,	                /**/EM_TT_LST.T070_SCAN_RUN_END),  
            
            new TactTimeSpan("Review Wait Position Move",	/**/EM_TT_LST.T080_REVIEW_WAIT_POS_START,	        /**/EM_TT_LST.T080_REVIEW_WAIT_POS_END),
            new TactTimeSpan("Review Run",		            /**/EM_TT_LST.T090_REVIEW_RUN_START,	            /**/EM_TT_LST.T090_REVIEW_RUN_END),
            new TactTimeSpan("Manual Review",		        /**/EM_TT_LST.T100_MANUAL_REVIEW_START,	            /**/EM_TT_LST.T100_MANUAL_REVIEW_END),

            new TactTimeSpan("Unloading Pos Move",		    /**/EM_TT_LST.T110_POS_UNLOADING_START,	            /**/EM_TT_LST.T110_POS_UNLOADING_END),

            new TactTimeSpan("Pad Vacuum Off",		        /**/EM_TT_LST.T120_VACUUM_OFF_START,	            /**/EM_TT_LST.T120_VACUUM_OFF_END),
            new TactTimeSpan("Lift Pin UP",		            /**/EM_TT_LST.T130_LIFT_PIN_UP_START,	            /**/EM_TT_LST.T130_LIFT_PIN_UP_END),

            new TactTimeSpan("Centering All",		        /**/EM_TT_LST.T140_ALL_CENTERING_START,	            /**/EM_TT_LST.T140_ALL_CENTERING_END),
            new TactTimeSpan("Uncentering All",		        /**/EM_TT_LST.T150_ALL_UNCENTERING_START,	        /**/EM_TT_LST.T150_ALL_UNCENTERING_END),

            new TactTimeSpan("PIO Send Wait",               /**/EM_TT_LST.T160_PIO_SEND_WAIT_START,             /**/EM_TT_LST.T160_PIO_SEND_WAIT_END),
            new TactTimeSpan("PIO Send Actual Time",        /**/EM_TT_LST.T170_PIO_SEND_ACTUAL_START,           /**/EM_TT_LST.T170_PIO_SEND_ACTUAL_END),

            new TactTimeSpan("Total",		                /**/EM_TT_LST.T020_LIFT_PIN_DOWN_START,	            /**/EM_TT_LST.T150_ALL_UNCENTERING_END)
        };

        private TactTimeMgr()
        {
            InitializeTackMgr();
        }
        private void InitializeTackMgr()
        {
            for (int iPos = 0; iPos < HIST_COUNT; iPos++)
                AddToHist();
        }
        public static TactTimeMgr Instance
        {
            get
            {
                if (_selfInstance == null)
                    _selfInstance = new TactTimeMgr();

                return _selfInstance;
            }
        }
        private DateTime _lastLogged = DateTime.Now;

        public void LogTactTime(Equipment equip)
        {
            _lastLogged = DateTime.Now;

            string[] tacts = new string[LstMainTactTimeSpan.Count];
            tacts.Initialize();

            for (int iPos = 0; iPos < LstMainTactTimeSpan.Count; iPos++)
            {
                string tackStr = TactTimeMgr.Instance.GetTackStr(
                    LstMainTactTimeSpan[iPos].Start,
                    LstMainTactTimeSpan[iPos].End, 0);
                double tackMSecond = TactTimeMgr.Instance.GetTactMSecond(
                    LstMainTactTimeSpan[iPos].Start,
                    LstMainTactTimeSpan[iPos].End, 0);

                string formatted = string.Format("{0,-50}",LstMainTactTimeSpan[iPos].Name);                    
                int byteLen = Encoding.Default.GetBytes(formatted).Length;
                if (byteLen > 50)
                    formatted = formatted.Remove(formatted.Length - byteLen + formatted.Length);

                tacts[iPos] = string.Format("{0}|\t{1,-15}\t{2}", formatted, tackStr, tackMSecond);
            }
            Logger.TacttimeLog.AppendLine(LogLevel.NoLog, "Glass ID : {0}", ListWaferID[0]);
            Logger.TacttimeLog.AppendLine(LogLevel.NoLog, "--------------------TactTime▼--------------------");
            for (int i = 0; i < LstMainTactTimeSpan.Count; ++i)
                Logger.TacttimeLog.AppendLine(LogLevel.NoLog, "{0}", tacts[i]);
            Logger.TacttimeLog.AppendLine(LogLevel.NoLog, "--------------------TactTime▲----------------------");            
        }
        public void Set(EM_TT_LST id)
        {
            bool tacttimeSetLog = false;
            if (tacttimeSetLog)
                Logger.Log.AppendLine(LogLevel.NoLog, "Tacttime Set : [{0}]", id.ToString());
            if (TacktimeHist[0].ContainsKey(id) == false) return;
            TacktimeHist[0][id] = DateTime.Now;
        }
        public void Set(EM_TT_LST end_id, EM_TT_LST start_id)
        {
            bool tacttimeSetLog = true;
            if (tacttimeSetLog)
                Logger.Log.AppendLine(LogLevel.NoLog, "Tacttime Set : END[{0}], START[{1}]", end_id.ToString(), start_id.ToString());
            if (TacktimeHist[0].ContainsKey(end_id) == false || TacktimeHist[0].ContainsKey(start_id) == false) return;
            TacktimeHist[0][end_id] = TacktimeHist[0][start_id] = DateTime.Now;
        }
        public void AddToHist()
        {
            lock (TacktimeHist)
            {
                Dictionary<EM_TT_LST, DateTime> dicTact = new Dictionary<EM_TT_LST, DateTime>();
                foreach (EM_TT_LST id in Enum.GetValues(typeof(EM_TT_LST)))
                    dicTact.Add(id, DateTime.MinValue);

                TacktimeHist.Insert(0, dicTact);

                if (TacktimeHist.Count > HIST_COUNT)
                    TacktimeHist.RemoveAt(HIST_COUNT);
            }
            lock (ListWaferID)
            {
                ListWaferID.Insert(0, "");

                if (ListWaferID.Count > HIST_COUNT)
                    ListWaferID.RemoveAt(HIST_COUNT);
            }
        }
        public TimeSpan GetTackInterval(EM_TT_LST start, EM_TT_LST end, int index)
        {
            if (index >= TacktimeHist.Count) new TimeSpan();
            if (index < 0) return new TimeSpan();
            if (TacktimeHist[index].ContainsKey(start) == false) return new TimeSpan();
            if (TacktimeHist[index].ContainsKey(end) == false) return new TimeSpan();

            if (TacktimeHist[index][start] != DateTime.MinValue && TacktimeHist[index][end] == DateTime.MinValue)
                if (index != 0)
                    return new TimeSpan();
                else
                    return DateTime.Now - TacktimeHist[index][start];
            else
                return TacktimeHist[index][end] - TacktimeHist[index][start];
        }
        public string GetTackStr(EM_TT_LST start, EM_TT_LST end, int index)
        {
            lock (TacktimeHist)
            {
                TimeSpan ts = GetTackInterval(start, end, index);
                return (ts.TotalMilliseconds < 0) ? "00.00.000" : ts.ToString(@"mm\:ss\.fff", System.Globalization.CultureInfo.InvariantCulture);
            }
        }
        public double GetTactMSecond(EM_TT_LST start, EM_TT_LST end, int index)
        {
            lock (TacktimeHist)
            {
                TimeSpan ts = GetTackInterval(start, end, index);
                return (ts.TotalMilliseconds < 0) ? 0 : ts.TotalMilliseconds;
            }
        }
        public void RemoveCurrentTact()
        {
            lock (TacktimeHist)
            {
                TacktimeHist.RemoveAt(0);

                Dictionary<EM_TT_LST, DateTime> dicTact = new Dictionary<EM_TT_LST, DateTime>();
                foreach (EM_TT_LST id in Enum.GetValues(typeof(EM_TT_LST)))
                    dicTact.Add(id, DateTime.MinValue);

                TacktimeHist.Insert(9, dicTact);
            }
            lock (ListWaferID)
            {
                ListWaferID.RemoveAt(0);
                ListWaferID.Insert(9, " ");
            }
        }
        public void SetGlassInfo(string waferId)
        {
            if(waferId.Equals(""))
            {
                ListWaferID[0] = "No Wafer ID";
            }
            else
            {
                ListWaferID[0] = waferId;
            }
        }
    }
}
