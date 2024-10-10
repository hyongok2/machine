using EquipMainUi.Struct.Detail.EFEM;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace EquipMainUi.Struct.TransferData
{
    public class WaferInfoKey : ICloneable
    {
        [ReadOnlyAttribute(true), DisplayName("CST ID")]
        public string CstID { get; set; }
        [ReadOnlyAttribute(true), DisplayName("Slot No")]
        public int SlotNo { get; set; }

        public WaferInfoKey()
        {
            CstID = string.Empty;
            SlotNo = -1;
        }

        public object Clone()
        {
            return new WaferInfoKey()
            {
                CstID = this.CstID,
                SlotNo = this.SlotNo,
            };
        }

        public void Clear()
        {
            CstID = string.Empty;
            SlotNo = -1;
        }
        public static List<WaferInfoKey> GetListKey(List<WaferInfo> listWafers)
        {
            List<WaferInfoKey> list = new List<WaferInfoKey>();

            foreach (WaferInfo wafer in listWafers)
            {
                list.Add(new WaferInfoKey() { CstID = wafer.CstID, SlotNo = wafer.SlotNo });
            }

            return list;
        }

        public static WaferInfoKey GetKey(WaferInfo wafer)
        {
            return new WaferInfoKey() { CstID = wafer.CstID, SlotNo = wafer.SlotNo };
        }
    }

    public class WaferInfo
    {
        #region automation data
        public string CstID { get; private set; }
        public string WaferID { get; set; }
        public string LotID { get; set; }
        public string RecipeID { get; set; }
        public int InputCstNo { get; set; }
        public string ControlJob { get; set; }
        public string ProcessJob { get; set; }
        public EmWaferIDType IDType { get; set; }
        public EmWaferMapType MapType { get; set; }
        public string BinCode { get; set; }
        public string NullBinCode { get; set; }
        public string Device { get; set; }
        public int CellColCount { get; set; }
        public int CellRowCount { get; set; }
        public string CellMap { get; set; }
        public string FloatZone { get; set; }
        public EmWaferNotchDir Notch { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int NotchToDegreeStdAVI()
        {
            int ret = 0;
            switch(Notch)
            {
                case EmWaferNotchDir.Bottom: ret =  0; break;
                case EmWaferNotchDir.Right: ret = -90; break;
                case EmWaferNotchDir.Left: ret = 90;   break;
                case EmWaferNotchDir.Top: ret = 180;   break;
                default: ret = 0; break;
            }
            return ret;
        }
        #endregion
        [Browsable(false)]
        public ObjectId Id { get; set; }
        
        public int SlotNo { get; private set; }
        public EmEfemMappingInfo Status { get; set; }                
        public bool IsOut {get;set;}
        public bool IsAlignComplete { get; set; }
        public bool IsInspComplete { get; set; }
        public bool IsReviewComplete {get;set;}
        public bool IsComeBack {get;set;}
        public bool IsDeepLearningReviewComplete { get; set; }
        private DateTime _inputDate;
        public DateTime InputDate { get { return _inputDate.ToLocalTime(); } set { _inputDate = value; } }
        private DateTime _outputDate;
        public DateTime OutputDate { get { return _outputDate.ToLocalTime(); } set { _outputDate = value; } }                
        public string BCRID1 { get; set; }
        public string BCRID2 { get; set; }
        public string OCRID { get; set; }
        [Description("Aligner Setting X")]
        public double SettingX { get; set; }
        [Description("Aligner Setting Y")]
        public double SettingY { get; set; }
        [Description("Aligner Setting T")]
        public double SettingT { get; set; }
        [Description("Aligner Calibration X")]
        public double? OffsetX { get; set; }
        [Description("Aligner Calibration Y")]
        public double? OffsetY { get; set; }
        [Description("Aligner Calibration T")]
        public double? OffsetT { get; set; }
        [Description("Aligner Wafer Notch T")]
        public double WaferNotchPosX { get; internal set; }
        [Description("Aligner Wafer Notch T")]
        public double WaferNotchPosY { get; internal set; }
        [Description("Aligner Wafer Notch T")]
        public double WaferNotchPosT { get; internal set; }
        [Description("Aligner Moved T")]
        public double MovedX { get; internal set; }
        [Description("Aligner Moved T")]
        public double MovedY { get; internal set; }
        [Description("Aligner Moved T")]
        public double MovedT { get; internal set; }
        public double EllipseX { get; set; }
        public double EllipseY { get; set; }
        public double MajorLength { get; set; }
        public double MinorLength { get; set; }
        public double EllipseT { get; set; }
        public double EllipseMajorX1 { get; set; } //참고 : Major - 장변, Minor - 단변
        public double EllipseMajorY1 { get; set; }
        public double EllipseMajorX2 { get; set; }
        public double EllipseMajorY2 { get; set; }
        public double EllipseMinorX1 { get; set; }
        public double EllipseMinorY1 { get; set; }
        public double EllipseMinorX2 { get; set; }
        public double EllipseMinorY2 { get; set; }
        public double KerfDataCh1_1 { get; set; }
        public double KerfDataCh1_2 { get; set; }
        public double KerfDataCh1_3 { get; set; }
        public double KerfDataCh1_4 { get; set; }
        public double KerfDataCh1_5 { get; set; }
        public double KerfDataCh1_6 { get; set; }
        public double KerfDataCh1_7 { get; set; }
        public double KerfDataCh1_8 { get; set; }
        public double KerfDataCh1_9 { get; set; }
        public double KerfDataCh1_10 { get; set; }
        public double KerfDataCh1_11 { get; set; }
        public double KerfDataCh1_12 { get; set; }
        public double KerfDataCh1_13 { get; set; }
        public double KerfDataCh1_14 { get; set; }
        public double KerfDataCh1_15 { get; set; }
        public double KerfDataCh2_1 { get; set; }
        public double KerfDataCh2_2 { get; set; }
        public double KerfDataCh2_3 { get; set; }
        public double KerfDataCh2_4 { get; set; }
        public double KerfDataCh2_5 { get; set; }
        public double KerfDataCh2_6 { get; set; }
        public double KerfDataCh2_7 { get; set; }
        public double KerfDataCh2_8 { get; set; }
        public double KerfDataCh2_9 { get; set; }
        public double KerfDataCh2_10 { get; set; }
        public double KerfDataCh2_11 { get; set; }
        public double KerfDataCh2_12 { get; set; }
        public double KerfDataCh2_13 { get; set; }
        public double KerfDataCh2_14 { get; set; }
        public double KerfDataCh2_15 { get; set; }

        public WaferInfo() { }

        public WaferInfo(string cstID, int slotNo, EmEfemMappingInfo status, EmWaferNotchDir notch = EmWaferNotchDir.Bottom)
        {
            Clear(cstID, slotNo, status, notch);
        }

        public void Clear(string cstID, int slotNo, EmEfemMappingInfo status, EmWaferNotchDir notch = EmWaferNotchDir.Bottom)
        {
            CstID = cstID;
            SlotNo = slotNo;
            
            Status = status;
            Notch = notch;

            IsOut = false;
            IsAlignComplete = false;
            IsInspComplete = false;
            IsReviewComplete = false;
            IsComeBack = false;
            IsDeepLearningReviewComplete = false;

            OutputDate = DateTime.MinValue;
            InputDate = DateTime.MinValue;
            WaferID = null;
            OCRID = null;
            BCRID1 = null;

            OffsetX = null;
            OffsetY = null;
            OffsetT = null;
        }

        public void SetDataNoPresence()
        {
            IsOut = true;
            IsComeBack = true;
            IsDeepLearningReviewComplete = true;
        }

        public static WaferInfo CreateTestWaferInfo(string cstID, int slotNo)
        {
            WaferInfo instance = new WaferInfo(cstID, slotNo, EmEfemMappingInfo.Presence);

            return instance;
        }

        public static WaferInfo CreateTestInspWaferInfo()
        {
            WaferInfo info = new WaferInfo("CSTIDTEST", 1, EmEfemMappingInfo.Presence);

            info.WaferID = "WAFERIDTEST";

            return info;
        }

        public WaferInfoKey ToKey()
        {
            return new WaferInfoKey() { CstID = this.CstID, SlotNo = this.SlotNo };
        }

        public bool Update()
        {
            return TransferDataMgr.UpdateWaferInfo(this);
        }
    }
}
