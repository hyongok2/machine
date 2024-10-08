using EquipMainUi.Struct;
using EquipMainUi.Struct.Detail.EFEM;
using EquipMainUi.Struct.TransferData;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Tact
{
    public enum EFEM_TACT_VALUE
    {
        T000_LPM_OPEN_START,
        T000_LPM_OPEN_END,
        //
        T010_ROBOT_PICK_FROM_LPM_START,
        T010_ROBOT_PICK_FROM_LPM_END,

        T020_ROBOT_PLACE_TO_ALIGNER_START,
        T020_ROBOT_PLACE_TO_ALIGNER_END,
        T030_ALIGNER_ALIGNMENT_START,
        T030_ALIGNER_ALIGNMENT_END,
        T040_ROBOT_PICK_FROM_ALIGNER_START,
        T040_ROBOT_PICK_FROM_ALIGNER_END,

        T050_PREVIOUS_WAFER_WAIT_START,
        T050_PREVIOUS_WAFER_WAIT_END,

        T070_WAIT_PREVIOUS_WAFER_PIO_COMPLETE_START,
        T070_WAIT_PREVIOUS_WAFER_PIO_COMPLETE_END,
        T080_PLACE_TO_AVI_START,
        T080_PLACE_TO_AVI_END,

        T090_INSPECTOR_START,
        T090_INSPECTOR_END,

        T095_WAIT_ROBOT_PICK_START,
        T095_WAIT_ROBOT_PICK_END,

        T100_PICK_FROM_AVI_START,
        T100_PICK_FROM_AVI_END,
        T110_WAIT_NEXT_WAFER_PIO_COMPLETE_START,
        T110_WAIT_NEXT_WAFER_PIO_COMPLETE_END,

        T120_ROBOT_PLACE_TO_LPM_START,
        T120_ROBOT_PLACE_TO_LPM_END,
        //
        T130_LPM_CLOSE_START,
        T130_LPM_CLOSE_END,
    }

    public class EFEMTimeSpan
    {
        public string Name { get; set; }
        public TimeSpan Time { get; set; }
    }
    public class EFEMTactTimeSpan
    {
        private string v;

        public EFEMTactTimeSpan()
        { }
        public EFEMTactTimeSpan(string name, EFEM_TACT_VALUE start, EFEM_TACT_VALUE end)
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
        public EFEM_TACT_VALUE Start { get; set; }
        public EFEM_TACT_VALUE End { get; set; }
    }
    public class EFEMTactMgr
    {
        private const int HIST_COUNT = 10;
        public Dictionary<string, Dictionary<EFEM_TACT_VALUE, DateTime>> TacktimeHist = new Dictionary<string, Dictionary<EFEM_TACT_VALUE, DateTime>>();
        private Queue<WaferInfoKey> _waferIdHistory = new Queue<WaferInfoKey>();
        public List<WaferInfoKey> ListWaferID = new List<WaferInfoKey>();

        private static EFEMTactMgr _selfInstance = null;

        public List<EFEMTactTimeSpan> LstMainTactTimeSpan = new List<EFEMTactTimeSpan>()
        {
            new EFEMTactTimeSpan(GG.boChinaLanguage ? "LPM Open" : "LPM 오픈",                          /**/EFEM_TACT_VALUE.T000_LPM_OPEN_START,                           /**/EFEM_TACT_VALUE.T000_LPM_OPEN_END),

            new EFEMTactTimeSpan("LPM ▶ Robot",                 /**/EFEM_TACT_VALUE.T010_ROBOT_PICK_FROM_LPM_START ,                /**/EFEM_TACT_VALUE.T010_ROBOT_PICK_FROM_LPM_END),

            new EFEMTactTimeSpan("Robot ▶ Aligner",              /**/EFEM_TACT_VALUE.T020_ROBOT_PLACE_TO_ALIGNER_START,               /**/EFEM_TACT_VALUE.T020_ROBOT_PLACE_TO_ALIGNER_END),
            new EFEMTactTimeSpan(GG.boChinaLanguage ? "PreAligner" : "프리 얼라인",                  /**/EFEM_TACT_VALUE.T030_ALIGNER_ALIGNMENT_START,                   /**/EFEM_TACT_VALUE.T030_ALIGNER_ALIGNMENT_END),
            new EFEMTactTimeSpan("Aligner ▶ Robot",              /**/EFEM_TACT_VALUE.T040_ROBOT_PICK_FROM_ALIGNER_START,               /**/EFEM_TACT_VALUE.T040_ROBOT_PICK_FROM_ALIGNER_END),

            //new EFEMTactTimeSpan("이전 Wafer 완료 대기",                  /**/EFEM_TACT_VALUE.T050_PREVIOUS_WAFER_WAIT_START,                   /**/EFEM_TACT_VALUE.T050_PREVIOUS_WAFER_WAIT_END),

            //new EFEMTactTimeSpan("이전 Wafer Exchange 대기",   /**/EFEM_TACT_VALUE.T070_WAIT_PREVIOUS_WAFER_PIO_COMPLETE_START,   /**/EFEM_TACT_VALUE.T070_WAIT_PREVIOUS_WAFER_PIO_COMPLETE_END),
            //new EFEMTactTimeSpan("Robot ▶ AVI (AVI PIO 제외)",                        /**/EFEM_TACT_VALUE.T080_PLACE_TO_AVI_START,                          /**/EFEM_TACT_VALUE.T080_PLACE_TO_AVI_END),

            //new EFEMTactTimeSpan("검사기",                           /**/EFEM_TACT_VALUE.T090_INSPECTOR_START,                           /**/EFEM_TACT_VALUE.T090_INSPECTOR_END),

            //new EFEMTactTimeSpan("검사 후 배출 대기",                           /**/EFEM_TACT_VALUE.T095_WAIT_ROBOT_PICK_START,                           /**/EFEM_TACT_VALUE.T095_WAIT_ROBOT_PICK_END),

            //new EFEMTactTimeSpan("AVI ▶ Robot (AVI PIO 제외)",                  /**/EFEM_TACT_VALUE.T100_PICK_FROM_AVI_START,                       /**/EFEM_TACT_VALUE.T100_PICK_FROM_AVI_END),
            //new EFEMTactTimeSpan("다음 Wafer Exchange 대기",   /**/EFEM_TACT_VALUE.T110_WAIT_NEXT_WAFER_PIO_COMPLETE_START,       /**/EFEM_TACT_VALUE.T110_WAIT_NEXT_WAFER_PIO_COMPLETE_END),

            new EFEMTactTimeSpan(GG.boChinaLanguage ? "检查机 Total(PIO, 包括等待时间)" : "검사기 Total(PIO, 대기시간 포함)", /**/EFEM_TACT_VALUE.T050_PREVIOUS_WAFER_WAIT_START, /**/EFEM_TACT_VALUE.T110_WAIT_NEXT_WAFER_PIO_COMPLETE_END),

            new EFEMTactTimeSpan("Robot ▶ LPM",           /**/EFEM_TACT_VALUE.T120_ROBOT_PLACE_TO_LPM_START,                   /**/EFEM_TACT_VALUE.T120_ROBOT_PLACE_TO_LPM_END),

            new EFEMTactTimeSpan(GG.boChinaLanguage ? "LPM Close" : "LPM 클로즈",                          /**/EFEM_TACT_VALUE.T130_LPM_CLOSE_START,                           /**/EFEM_TACT_VALUE.T130_LPM_CLOSE_END),
        };
        private EFEMTactMgr()
        {
            InitializeTackMgr();
        }
        private void InitializeTackMgr()
        {
            for (int iPos = 0; iPos < HIST_COUNT; iPos++)
                AddToHist(new WaferInfoKey() { CstID = string.Empty, SlotNo = iPos });
        }
        public static EFEMTactMgr Instance
        {
            get
            {
                if (_selfInstance == null)
                    _selfInstance = new EFEMTactMgr();

                return _selfInstance;
            }
        }
        //private DateTime _lastLogged = DateTime.Now;
        //private List<string> lstTact;
        //private string tactListHeader = ", ";
        //public void LogTactTime(Equipment equip, WaferInfoKey _key)
        //{
        //    _lastLogged = DateTime.Now;
        //    lstTact = new List<string>();

        //    string[] tacts = new string[LstMainTactTimeSpan.Count];
        //    string[] tacts2 = new string[LstMainTactTimeSpan.Count];

        //    tacts.Initialize();
        //    tacts2.Initialize();

        //    for (int iPos = 0; iPos < LstMainTactTimeSpan.Count; iPos++)
        //    {
        //        string tackStr = EFEMTactMgr.Instance.GetTackStr(
        //            LstMainTactTimeSpan[iPos].Start,
        //            LstMainTactTimeSpan[iPos].End, string.Format("{0}_{1}", _key.CstID, _key.SlotNo.ToString()));

        //        string formatted = string.Format("{0,-50}", LstMainTactTimeSpan[iPos].Name);
        //        int byteLen = Encoding.Default.GetBytes(formatted).Length;
        //        if (byteLen > 50)
        //            formatted = formatted.Remove(formatted.Length - byteLen + formatted.Length);

        //        tacts[iPos] = string.Format("{0}|{1,-15}", formatted, tackStr);

        //        tactListHeader += ", " + formatted;
        //        tacts2[iPos] = string.Format("{0,-10}", tackStr);
        //    }
        //    Logger.TacttimeLog.AppendLine(LogLevel.Info, tactListHeader);

        //    for (int i = 0; i < LstMainTactTimeSpan.Count; ++i)
        //    {
        //        lstTact.Add(tacts2[i]);
        //    }
        //    Logger.TacttimeLog.AppendLine(glassID, string.Format("{0}", string.Join(", ", lstTact.ToArray())));
        //}
        public void Set(EFEM_TACT_VALUE id, WaferInfoKey _key)
        {
            if (_key.SlotNo == -1)
                return;
            WaferInfoKey waferkey = FindWaferKey(_key);
            if (waferkey == null)
                return;
            string key = waferkey.CstID + "_" + waferkey.SlotNo.ToString();
            if (TacktimeHist[key].ContainsKey(id) == false) return;
            TacktimeHist[key][id] = DateTime.Now;
        }
        public void Set(EFEM_TACT_VALUE end_id, EFEM_TACT_VALUE start_id, WaferInfoKey _key)
        {
            if (_key.SlotNo == -1)
                return;
            WaferInfoKey waferkey = FindWaferKey(_key);
            if (waferkey == null)
                return;
            string key = waferkey.CstID + "_" + waferkey.SlotNo.ToString();
            if (TacktimeHist[key].ContainsKey(end_id) == false || TacktimeHist[key].ContainsKey(start_id) == false) return;
            TacktimeHist[key][end_id] = TacktimeHist[key][start_id] = DateTime.Now;
        }
        List<WaferInfoKey> listWaferKey = new List<WaferInfoKey>();
        public WaferInfoKey FindWaferKey(WaferInfoKey key)
        {
            Queue<WaferInfoKey> instance = new Queue<WaferInfoKey>(_waferIdHistory);
            foreach (var value in instance)
            {
                if (value.CstID == key.CstID && value.SlotNo == key.SlotNo)
                    return value;
            }
            return null;
        }

        public void AddToHist(WaferInfoKey _key, Dictionary<EFEM_TACT_VALUE, DateTime> dicTact = null)
        {
            lock (TacktimeHist)
            {
                try
                {
                    if (dicTact == null)
                    {
                        dicTact = new Dictionary<EFEM_TACT_VALUE, DateTime>();
                        foreach (EFEM_TACT_VALUE id in Enum.GetValues(typeof(EFEM_TACT_VALUE)))
                            dicTact.Add(id, DateTime.MinValue);
                    }

                    string key = _key.CstID + "_" + _key.SlotNo.ToString();
                    if (TacktimeHist.ContainsKey(key) == false)
                    {
                        TacktimeHist.Add(key, dicTact);
                        _waferIdHistory.Enqueue(_key);
                    }

                    if (TacktimeHist.Count > HIST_COUNT)
                    {
                        WaferInfoKey lastWaferId = _waferIdHistory.Dequeue();
                        key = lastWaferId.CstID + "_" + lastWaferId.SlotNo.ToString();
                        TacktimeHist.Remove(key);
                    }
                }
                catch(Exception ex)
                {
                    
                }
            }
            lock (ListWaferID)
            {
                if(_key.CstID == string.Empty)
                {
                    ListWaferID.Insert(0, new WaferInfoKey() { CstID = string.Empty, SlotNo = 0 });
                }
                else
                {
                    ListWaferID.Insert(0, new WaferInfoKey() { CstID = _key.CstID, SlotNo = _key.SlotNo });
                }

                if (ListWaferID.Count > HIST_COUNT)
                    ListWaferID.RemoveAt(HIST_COUNT);
            }
        }
        public TimeSpan GetTackInterval(EFEM_TACT_VALUE start, EFEM_TACT_VALUE end, string key)
        {
            try
            {
                if (TacktimeHist[key].ContainsKey(start) == false) return new TimeSpan();
                if (TacktimeHist[key].ContainsKey(end) == false) return new TimeSpan();

                if (TacktimeHist[key][start] != DateTime.MinValue && TacktimeHist[key][end] == DateTime.MinValue)
                    return DateTime.Now - TacktimeHist[key][start];
                else
                    return TacktimeHist[key][end] - TacktimeHist[key][start];
            }
            catch (Exception)
            {
                return new TimeSpan();
            }
            
        }
        public string GetTackStr(EFEM_TACT_VALUE start, EFEM_TACT_VALUE end, int pos)
        {
            Queue <WaferInfoKey> queue = new Queue<WaferInfoKey>(_waferIdHistory);
            if (queue.Count == 0)
                return "ERROR";

            WaferInfoKey temp = null;
            try
            {
                for (int i = 0; i <= pos; i++)
                {
                    temp = queue.Dequeue();
                }
                if (temp == null)
                    return "00.00.000";
                string key = temp.CstID + "_" + temp.SlotNo;
                lock (TacktimeHist)
                {
                    TimeSpan ts = GetTackInterval(start, end, key);
                    return (ts.TotalMilliseconds < 0) ? "00.00.000" : ts.ToString(@"mm\:ss\.fff", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                return "00.00.000";
            }
        }
        public double GetTactMSecond(EFEM_TACT_VALUE start, EFEM_TACT_VALUE end, string key)
        {
            lock (TacktimeHist)
            {
                TimeSpan ts = GetTackInterval(start, end, key);
                return (ts.TotalMilliseconds < 0) ? 0 : ts.TotalMilliseconds;
            }
        }
        public void RemoveCurrentTact()
        {
            lock (TacktimeHist)
            {
                List<WaferInfoKey> keyList = new List<WaferInfoKey>();
                keyList.Add(GG.Equip.Efem.Robot.UpperWaferKey);
                keyList.Add(GG.Equip.Efem.Robot.LowerWaferKey);
                keyList.Add(GG.Equip.Efem.Aligner.LowerWaferKey);
                keyList.Add(GG.Equip.TransferUnit.LowerWaferKey);
                
                foreach (var item in keyList)
                {
                    if(item.CstID != string.Empty)
                    {
                        var key = string.Format("{0}_{1}", item.CstID, item.SlotNo.ToString());
                        TacktimeHist.Remove(key);
                        Dictionary<EFEM_TACT_VALUE, DateTime> dicTact = new Dictionary<EFEM_TACT_VALUE, DateTime>();
                        foreach (EFEM_TACT_VALUE id in Enum.GetValues(typeof(EFEM_TACT_VALUE)))
                            dicTact.Add(id, DateTime.MinValue);

                        TacktimeHist.Add(key, dicTact);

                        ListWaferID.RemoveAt(0);
                        ListWaferID.Insert(9, item);
                    }
                }
            }
        }
        public List<Dictionary<EFEM_TACT_VALUE, DateTime>> tempList1;
        public List<Dictionary<EFEM_TACT_VALUE, DateTime>> tempList2;
        public bool IsInsertWafer1 = false;
        public bool IsInsertWafer2 = false;
        public void InsertWafer(int portNum)
        {
            if(portNum == 1)
            {
                tempList1 = new List<Dictionary<EFEM_TACT_VALUE, DateTime>>();
                for (int i = 0; i < 13; i++)
                {
                    Dictionary<EFEM_TACT_VALUE, DateTime> dicTact = new Dictionary<EFEM_TACT_VALUE, DateTime>();
                    foreach (EFEM_TACT_VALUE id in Enum.GetValues(typeof(EFEM_TACT_VALUE)))
                    {
                        if (id == EFEM_TACT_VALUE.T000_LPM_OPEN_START)
                        {
                            dicTact.Add(id, DateTime.Now);
                        }
                        else
                        {
                            dicTact.Add(id, DateTime.MinValue);
                        }
                    }

                    tempList1.Add(dicTact);
                }
                IsInsertWafer1 = true;
            }
            else
            {
                tempList2 = new List<Dictionary<EFEM_TACT_VALUE, DateTime>>();
                for (int i = 0; i < 13; i++)
                {
                    Dictionary<EFEM_TACT_VALUE, DateTime> dicTact = new Dictionary<EFEM_TACT_VALUE, DateTime>();
                    foreach (EFEM_TACT_VALUE id in Enum.GetValues(typeof(EFEM_TACT_VALUE)))
                    {
                        if (id == EFEM_TACT_VALUE.T000_LPM_OPEN_START)
                        {
                            dicTact.Add(id, DateTime.Now);
                        }
                        else
                        {
                            dicTact.Add(id, DateTime.MinValue);
                        }
                    }

                    tempList2.Add(dicTact);
                }
                IsInsertWafer2 = true;
            }
        }
        public void CloseStart(string _cstID)
        {
            List<WaferInfoKey> waferkey = FindAllWaferByCstID(_cstID);
            if (waferkey == null)
                return;
            for(int i = 0; i < waferkey.Count;i++)
            {
                string key = waferkey[i].CstID + "_" + waferkey[i].SlotNo.ToString();
                if (TacktimeHist[key].ContainsKey(EFEM_TACT_VALUE.T130_LPM_CLOSE_START) == false) return;
                TacktimeHist[key][EFEM_TACT_VALUE.T130_LPM_CLOSE_START] = DateTime.Now;
            }
        }
        public void CloseEnd(string _cstID)
        {
            List<WaferInfoKey> waferkey = FindAllWaferByCstID(_cstID);
            if (waferkey == null)
                return;
            for (int i = 0; i < waferkey.Count; i++)
            {
                string key = waferkey[i].CstID + "_" + waferkey[i].SlotNo.ToString();
                if (TacktimeHist[key].ContainsKey(EFEM_TACT_VALUE.T130_LPM_CLOSE_END) == false) return;
                TacktimeHist[key][EFEM_TACT_VALUE.T130_LPM_CLOSE_END] = DateTime.Now;
            }
        }
        public List<WaferInfoKey> FindAllWaferByCstID(string _cstID)
        {
            List<WaferInfoKey> listWaferInfoKey = new List<WaferInfoKey>();

            Queue<WaferInfoKey> instance = new Queue<WaferInfoKey>(_waferIdHistory);
            foreach (var value in instance)
            {
                if (value.CstID == _cstID)
                    listWaferInfoKey.Add(value);
            }
            return listWaferInfoKey;
        }
        public void FilterData(EmEfemMappingInfo[] mapping, string _cstID, int portNum)
        {
            int count = 0;
            if (portNum == 1)
            {
                for (int i = 0; i < mapping.Count(); i++)
                {
                    if (mapping[i] == EmEfemMappingInfo.Absence)
                    {
                        tempList1.RemoveAt(count);
                    }
                    else
                    {
                        tempList1[count][EFEM_TACT_VALUE.T000_LPM_OPEN_END] = DateTime.Now;

                        AddToHist(new WaferInfoKey() { CstID = _cstID, SlotNo = i + 1 }, tempList1[count]);
                        count++;
                    }
                }
                IsInsertWafer1 = false;
            }
            else
            {
                for (int i = 0; i < mapping.Count(); i++)
                {
                    if (mapping[i] == EmEfemMappingInfo.Absence)
                    {
                        tempList2.RemoveAt(count);
                    }
                    else
                    {
                        tempList2[count][EFEM_TACT_VALUE.T000_LPM_OPEN_END] = DateTime.Now;

                        AddToHist(new WaferInfoKey() { CstID = _cstID, SlotNo = i + 1 }, tempList2[count]);
                        count++;
                    }
                }
                IsInsertWafer2 = false;
            }
        }
    }
}