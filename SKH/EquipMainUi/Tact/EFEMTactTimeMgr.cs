using EquipMainUi.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Monitor
{
    public enum EFEM_TT_LST
    {
        T000_LPM_OPEN_START,
        T000_LPM_OPEN_END,

        T005_LPM_START_WAIT_START,
        T005_LPM_START_WAIT_END,
        //============================================================//
        T010_ROBOT_PICK_WAFER_FROM_LPM_START,
        T010_ROBOT_PICK_WAFER_FROM_LPM_END,

        T020_ROBOT_PLACE_WAFER_TO_ALIGNER_START,
        T020_ROBOT_PLACE_WAFER_TO_ALIGNER_END,

        T030_ALIGNER_PRE_ALIGN_START,
        T030_ALIGNER_PRE_ALIGN_END,

        T040_ROBOT_PICK_WAFER_FROM_ALIGNER_START,
        T040_ROBOT_PICK_WAFER_FROM_ALIGNER_END,

        T050_PREVIOUS_WAFER_COMPLETE_WAIT_START,        //단독 동작일 때는 0초 여야함
        T050_PREVIOUS_WAFER_COMPLETE_WAIT_END,          //단동 동작일 떄는 0초 여야함

        T060_ROBOT_PICK_PREVIOUS_WAFER_FROM_AVI_START,  //단동 동작일 떄는 0초 여야함
        T060_ROBOT_PICK_PREVIOUS_WAFER_FROM_AVI_END,    //단동 동작일 떄는 0초 여야함

        T070_ROBOT_PLACE_CURRENT_WAFER_TO_AVI_START,
        T070_ROBOT_PLACE_CURRENT_WAFER_TO_AVI_END,

        T080_AVI_INSPECTOR_START,
        T080_AVI_INSPECTOR_END,

        T090_ROBOT_PICK_CURRENT_WAFER_FROM_AVI_START,
        T090_ROBOT_PICK_CURRENT_WAFER_FROM_AVI_END,

        T100_ROBOT_PLACE_NEXT_WAFER_TO_AVI_START,       //다음장 글라스가 없을 떄는 0초여야 함
        T100_ROBOT_PLACE_NEXT_WAFER_TO_AVI_END,         //다음장 글라스가 없을 떄는 0초여야 함

        T110_ROBOT_PLACE_WAFER_TO_LPM_START,
        T110_ROBOT_PLACE_WAFER_TO_LPM_END,
        //============================================================//
        T120_LPM_CLOSE_START,
        T120_LPM_CLOSE_END,

    }
    public class EFEMTactTimeSpan
    {
        private string v;

        public EFEMTactTimeSpan()
        { }
        public EFEMTactTimeSpan(string name, EFEM_TT_LST start, EFEM_TT_LST end)
        {
            this.Name = name;
            this.Start = start;
            this.End = end;
        }

        public EFEMTactTimeSpan(string v)
        {
            this.v = v;
        }

        public string Name { get; set; }
        public EFEM_TT_LST Start { get; set; }
        public EFEM_TT_LST End { get; set; }
    }
    public class EFEMTactTimeMgr
    {
        private const int HIST_COUNT = 10;
        public Dictionary<string, Dictionary<EFEM_TT_LST, DateTime>> TacktimeHist = new Dictionary<string, Dictionary<EFEM_TT_LST, DateTime>>();

        public List<string> ListWaferID = new List<string>();
        private static EFEMTactTimeMgr _selfInstance = null;

        public List<EFEMTactTimeSpan> LstEFEMTactTimeSpan = new List<EFEMTactTimeSpan>()
        {
            new EFEMTactTimeSpan("EFEM Total Tact",                     /**/EFEM_TT_LST.T000_LPM_OPEN_START,                            /**/EFEM_TT_LST.T120_LPM_CLOSE_END),
            //========================================================================================================================================================//
            new EFEMTactTimeSpan("LPM Open",                            /**/EFEM_TT_LST.T000_LPM_OPEN_START,                            /**/EFEM_TT_LST.T000_LPM_OPEN_END),
            new EFEMTactTimeSpan("LPM START WAIT",                      /**/EFEM_TT_LST.T005_LPM_START_WAIT_START,                      /**/EFEM_TT_LST.T005_LPM_START_WAIT_END),

            new EFEMTactTimeSpan("ROBOT PICK WAFER FROM LPM",           /**/EFEM_TT_LST.T010_ROBOT_PICK_WAFER_FROM_LPM_START,           /**/EFEM_TT_LST.T010_ROBOT_PICK_WAFER_FROM_LPM_END),

            new EFEMTactTimeSpan("ROBOT PLACE WAFER TO ALIGNER",        /**/EFEM_TT_LST.T020_ROBOT_PLACE_WAFER_TO_ALIGNER_START,        /**/EFEM_TT_LST.T020_ROBOT_PLACE_WAFER_TO_ALIGNER_END),
            new EFEMTactTimeSpan("ALIGNER ALIGN",                       /**/EFEM_TT_LST.T030_ALIGNER_PRE_ALIGN_START,                   /**/EFEM_TT_LST.T030_ALIGNER_PRE_ALIGN_END),
            new EFEMTactTimeSpan("ROBOT PICK WAFER FROM ALIGNER",       /**/EFEM_TT_LST.T040_ROBOT_PICK_WAFER_FROM_ALIGNER_START,       /**/EFEM_TT_LST.T040_ROBOT_PICK_WAFER_FROM_ALIGNER_END),
            
            new EFEMTactTimeSpan("PREVIOUS_WAFER_COMPLETE_WAIT",         /**/EFEM_TT_LST.T050_PREVIOUS_WAFER_COMPLETE_WAIT_START,       /**/EFEM_TT_LST.T050_PREVIOUS_WAFER_COMPLETE_WAIT_END),
            new EFEMTactTimeSpan("ROBOT PICK PREVIOUS WAFER FROM AVI",  /**/EFEM_TT_LST.T060_ROBOT_PICK_PREVIOUS_WAFER_FROM_AVI_START,  /**/EFEM_TT_LST.T060_ROBOT_PICK_PREVIOUS_WAFER_FROM_AVI_END),
            new EFEMTactTimeSpan("ROBOT PLACE CURRENT WAFER TO AVI",    /**/EFEM_TT_LST.T070_ROBOT_PLACE_CURRENT_WAFER_TO_AVI_START,    /**/EFEM_TT_LST.T070_ROBOT_PLACE_CURRENT_WAFER_TO_AVI_END),
            new EFEMTactTimeSpan("AVI INSPECTOR COMPLETE WAIT",         /**/EFEM_TT_LST.T080_AVI_INSPECTOR_START,                       /**/EFEM_TT_LST.T080_AVI_INSPECTOR_END),
            new EFEMTactTimeSpan("ROBOT PICK CURRENT WAFER FROM AVI",   /**/EFEM_TT_LST.T090_ROBOT_PICK_CURRENT_WAFER_FROM_AVI_START,   /**/EFEM_TT_LST.T090_ROBOT_PICK_CURRENT_WAFER_FROM_AVI_END),

            new EFEMTactTimeSpan("ROBOT PLACE NEXT WAFER TO AVI",       /**/EFEM_TT_LST.T100_ROBOT_PLACE_NEXT_WAFER_TO_AVI_START,       /**/EFEM_TT_LST.T100_ROBOT_PLACE_NEXT_WAFER_TO_AVI_END),
            new EFEMTactTimeSpan("ROBOT PLACE WAFER TO LPM",            /**/EFEM_TT_LST.T110_ROBOT_PLACE_WAFER_TO_LPM_START,            /**/EFEM_TT_LST.T110_ROBOT_PLACE_WAFER_TO_LPM_END),

            new EFEMTactTimeSpan("LPM CLOSE",                           /**/EFEM_TT_LST.T120_LPM_CLOSE_START,                           /**/EFEM_TT_LST.T120_LPM_CLOSE_END),
        };

        private EFEMTactTimeMgr()
        {
            InitializeTackMgr();
        }
        private void InitializeTackMgr()
        {
            for (int iPos = 0; iPos < HIST_COUNT; iPos++)
                AddToHist("CHECKING WAFER ID" + iPos.ToString());
        }
        public static EFEMTactTimeMgr Instance
        {
            get
            {
                if (_selfInstance == null)
                    _selfInstance = new EFEMTactTimeMgr();

                return _selfInstance;
            }
        }
        private DateTime _lastLogged = DateTime.Now;

        public void LogTactTime()
        {
            _lastLogged = DateTime.Now;

            string[] tacts = new string[LstEFEMTactTimeSpan.Count];
            tacts.Initialize();

            for (int iPos = 0; iPos < LstEFEMTactTimeSpan.Count; iPos++)
            {
                string tackStr = EFEMTactTimeMgr.Instance.GetTackStr(
                    LstEFEMTactTimeSpan[iPos].Start,
                    LstEFEMTactTimeSpan[iPos].End, 0);
                double tackMSecond = EFEMTactTimeMgr.Instance.GetTactMSecond(
                    LstEFEMTactTimeSpan[iPos].Start,
                    LstEFEMTactTimeSpan[iPos].End, 0);

                string formatted = string.Format("{0,-50}", LstEFEMTactTimeSpan[iPos].Name);
                int byteLen = Encoding.Default.GetBytes(formatted).Length;
                if (byteLen > 50)
                    formatted = formatted.Remove(formatted.Length - byteLen + formatted.Length);

                tacts[iPos] = string.Format("{0}|\t{1,-15}\t{2}", formatted, tackStr, tackMSecond);
            }
            //Logger.TacttimeLog.AppendLine(LogLevel.NoLog, "Glass ID : {0}", ListWaferID[0]);
            //Logger.TacttimeLog.AppendLine(LogLevel.NoLog, "--------------------TactTime▼--------------------");
            //for (int i = 0; i < LstEFEMTactTimeSpan.Count; ++i)
            //    Logger.TacttimeLog.AppendLine(LogLevel.NoLog, "{0}", tacts[i]);
            //Logger.TacttimeLog.AppendLine(LogLevel.NoLog, "--------------------TactTime▲----------------------");
        }
        public void Set(EFEM_TT_LST id, string waferID)
        {
            bool tacttimeSetLog = false;
            //if (tacttimeSetLog)
            //    Logger.Log.AppendLine(LogLevel.NoLog, "Tacttime Set : [{0}]", id.ToString());
            if (TacktimeHist.ContainsKey(waferID) == false)
            {
                AddToHist(waferID);
            }
            if (id == EFEM_TT_LST.T110_ROBOT_PLACE_WAFER_TO_LPM_END)
            {
                TacktimeHist[waferID][EFEM_TT_LST.T120_LPM_CLOSE_START] = TacktimeHist[waferID][EFEM_TT_LST.T120_LPM_CLOSE_END] = DateTime.Now;
            }
            if (TacktimeHist[waferID].ContainsKey(id) == false) return;
            TacktimeHist[waferID][id] = DateTime.Now;
        }
        public void Set(EFEM_TT_LST end_id, EFEM_TT_LST start_id, string waferID)
        {
            bool tacttimeSetLog = true;
            //if (tacttimeSetLog)
            //    Logger.Log.AppendLine(LogLevel.NoLog, "Tacttime Set : END[{0}], START[{1}]", end_id.ToString(), start_id.ToString());
            if (TacktimeHist.ContainsKey(waferID) == false)
            {
                AddToHist(waferID);
                TacktimeHist[waferID][EFEM_TT_LST.T000_LPM_OPEN_START] = TacktimeHist[waferID][EFEM_TT_LST.T000_LPM_OPEN_END] = DateTime.Now;
                TacktimeHist[waferID][start_id] = DateTime.Now;
            }
            else
            {
                if (TacktimeHist[waferID].ContainsKey(end_id) == false || TacktimeHist[waferID].ContainsKey(start_id) == false) return;
                TacktimeHist[waferID][end_id] = TacktimeHist[waferID][start_id] = DateTime.Now;
            }
        }
        public void AddToHist(string waferID)
        {
            lock (TacktimeHist)
            {
                Dictionary<EFEM_TT_LST, DateTime> dicTact = new Dictionary<EFEM_TT_LST, DateTime>();
                foreach (EFEM_TT_LST id in Enum.GetValues(typeof(EFEM_TT_LST)))
                    dicTact.Add(id, DateTime.MinValue);

                //TacktimeHist.Insert(waferID, dicTact);
                TacktimeHist.Add(waferID, dicTact);

                if (TacktimeHist.Count > HIST_COUNT)
                    TacktimeHist.Remove(ListWaferID[HIST_COUNT - 1]);
                //TacktimeHist.RemoveAt(HIST_COUNT);
            }
            lock (ListWaferID)
            {
                ListWaferID.Insert(0, waferID);

                if (ListWaferID.Count > HIST_COUNT)
                    ListWaferID.RemoveAt(HIST_COUNT);
            }
        }
        public TimeSpan GetTackInterval(EFEM_TT_LST start, EFEM_TT_LST end, int index)
        {
            try
            {
                if (index >= TacktimeHist.Count) new TimeSpan();
                if (index < 0) return new TimeSpan();
                if (TacktimeHist[ListWaferID[index]].ContainsKey(start) == false) return new TimeSpan();
                if (TacktimeHist[ListWaferID[index]].ContainsKey(end) == false) return new TimeSpan();

                if (TacktimeHist[ListWaferID[index]][start] != DateTime.MinValue && TacktimeHist[ListWaferID[index]][end] == DateTime.MinValue)
                    //if (index != 0)
                    //    return new TimeSpan();
                    //else
                    return DateTime.Now - TacktimeHist[ListWaferID[index]][start];
                else
                    return TacktimeHist[ListWaferID[index]][end] - TacktimeHist[ListWaferID[index]][start];
            }
            catch (Exception)
            {
                return new TimeSpan();
            }
        }
        public string GetTackStr(EFEM_TT_LST start, EFEM_TT_LST end, int index)
        {
            lock (TacktimeHist)
            {
                TimeSpan ts = GetTackInterval(start, end, index);
                return (ts.TotalMilliseconds < 0) ? "00.00.000" : ts.ToString(@"mm\:ss\.fff", System.Globalization.CultureInfo.InvariantCulture);
            }
        }
        public double GetTactMSecond(EFEM_TT_LST start, EFEM_TT_LST end, int index)
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
                TacktimeHist.Remove("0");

                Dictionary<EFEM_TT_LST, DateTime> dicTact = new Dictionary<EFEM_TT_LST, DateTime>();
                foreach (EFEM_TT_LST id in Enum.GetValues(typeof(EFEM_TT_LST)))
                    dicTact.Add(id, DateTime.MinValue);

                TacktimeHist.Add("9", dicTact);
            }
            lock (ListWaferID)
            {
                ListWaferID.RemoveAt(0);
                ListWaferID.Insert(9, " ");
            }
        }
        public void SetWaferID(string waferId)
        {
            ListWaferID[0] = waferId;
        }
    }
}
