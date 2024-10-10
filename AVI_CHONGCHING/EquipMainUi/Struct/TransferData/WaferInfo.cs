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
            switch (Notch)
            {
                case EmWaferNotchDir.Bottom: ret = 0; break;
                case EmWaferNotchDir.Right: ret = -90; break;
                case EmWaferNotchDir.Left: ret = 90; break;
                case EmWaferNotchDir.Top: ret = 180; break;
                default: ret = 0; break;
            }
            return ret;
        }
        #endregion
        [Browsable(false)]
        public ObjectId Id { get; set; }

        public int SlotNo { get; private set; }
        public EmEfemMappingInfo Status { get; set; }
        public bool IsOut { get; set; }
        public bool IsAlignComplete { get; set; }
        public bool IsInspComplete { get; set; }
        public bool IsReviewComplete { get; set; }
        public bool IsComeBack { get; set; }
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

        #region Kerf Shift Data
        public double KerfData_Right_Col_1 { get; set; }
        public double KerfData_Right_Row_1 { get; set; }
        public double KerfData_Right_1 { get; set; }
        public double KerfData_Right_Col_2 { get; set; }
        public double KerfData_Right_Row_2 { get; set; }
        public double KerfData_Right_2 { get; set; }
        public double KerfData_Right_Col_3 { get; set; }
        public double KerfData_Right_Row_3 { get; set; }
        public double KerfData_Right_3 { get; set; }
        public double KerfData_Right_Col_4 { get; set; }
        public double KerfData_Right_Row_4 { get; set; }
        public double KerfData_Right_4 { get; set; }
        public double KerfData_Right_Col_5 { get; set; }
        public double KerfData_Right_Row_5 { get; set; }
        public double KerfData_Right_5 { get; set; }
        public double KerfData_Right_Col_6 { get; set; }
        public double KerfData_Right_Row_6 { get; set; }
        public double KerfData_Right_6 { get; set; }
        public double KerfData_Right_Col_7 { get; set; }
        public double KerfData_Right_Row_7 { get; set; }
        public double KerfData_Right_7 { get; set; }
        public double KerfData_Right_Col_8 { get; set; }
        public double KerfData_Right_Row_8 { get; set; }
        public double KerfData_Right_8 { get; set; }
        public double KerfData_Right_Col_9 { get; set; }
        public double KerfData_Right_Row_9 { get; set; }
        public double KerfData_Right_9 { get; set; }
        public double KerfData_Right_Col_10 { get; set; }
        public double KerfData_Right_Row_10 { get; set; }
        public double KerfData_Right_10 { get; set; }
        public double KerfData_Right_Col_11 { get; set; }
        public double KerfData_Right_Row_11 { get; set; }
        public double KerfData_Right_11 { get; set; }
        public double KerfData_Right_Col_12 { get; set; }
        public double KerfData_Right_Row_12 { get; set; }
        public double KerfData_Right_12 { get; set; }
        public double KerfData_Right_Col_13 { get; set; }
        public double KerfData_Right_Row_13 { get; set; }
        public double KerfData_Right_13 { get; set; }
        public double KerfData_Right_Col_14 { get; set; }
        public double KerfData_Right_Row_14 { get; set; }
        public double KerfData_Right_14 { get; set; }
        public double KerfData_Right_Col_15 { get; set; }
        public double KerfData_Right_Row_15 { get; set; }
        public double KerfData_Right_15 { get; set; }
        public double KerfData_Right_Col_16 { get; set; }
        public double KerfData_Right_Row_16 { get; set; }
        public double KerfData_Right_16 { get; set; }
        public double KerfData_Right_Col_17 { get; set; }
        public double KerfData_Right_Row_17 { get; set; }
        public double KerfData_Right_17 { get; set; }
        public double KerfData_Right_Col_18 { get; set; }
        public double KerfData_Right_Row_18 { get; set; }
        public double KerfData_Right_18 { get; set; }
        public double KerfData_Right_Col_19 { get; set; }
        public double KerfData_Right_Row_19 { get; set; }
        public double KerfData_Right_19 { get; set; }
        public double KerfData_Right_Col_20 { get; set; }
        public double KerfData_Right_Row_20 { get; set; }
        public double KerfData_Right_20 { get; set; }

        public double KerfData_Bottom_Col_1 { get; set; }
        public double KerfData_Bottom_Row_1 { get; set; }
        public double KerfData_Bottom_1 { get; set; }
        public double KerfData_Bottom_Col_2 { get; set; }
        public double KerfData_Bottom_Row_2 { get; set; }
        public double KerfData_Bottom_2 { get; set; }
        public double KerfData_Bottom_Col_3 { get; set; }
        public double KerfData_Bottom_Row_3 { get; set; }
        public double KerfData_Bottom_3 { get; set; }
        public double KerfData_Bottom_Col_4 { get; set; }
        public double KerfData_Bottom_Row_4 { get; set; }
        public double KerfData_Bottom_4 { get; set; }
        public double KerfData_Bottom_Col_5 { get; set; }
        public double KerfData_Bottom_Row_5 { get; set; }
        public double KerfData_Bottom_5 { get; set; }
        public double KerfData_Bottom_Col_6 { get; set; }
        public double KerfData_Bottom_Row_6 { get; set; }
        public double KerfData_Bottom_6 { get; set; }
        public double KerfData_Bottom_Col_7 { get; set; }
        public double KerfData_Bottom_Row_7 { get; set; }
        public double KerfData_Bottom_7 { get; set; }
        public double KerfData_Bottom_Col_8 { get; set; }
        public double KerfData_Bottom_Row_8 { get; set; }
        public double KerfData_Bottom_8 { get; set; }
        public double KerfData_Bottom_Col_9 { get; set; }
        public double KerfData_Bottom_Row_9 { get; set; }
        public double KerfData_Bottom_9 { get; set; }
        public double KerfData_Bottom_Col_10 { get; set; }
        public double KerfData_Bottom_Row_10 { get; set; }
        public double KerfData_Bottom_10 { get; set; }
        public double KerfData_Bottom_Col_11 { get; set; }
        public double KerfData_Bottom_Row_11 { get; set; }
        public double KerfData_Bottom_11 { get; set; }
        public double KerfData_Bottom_Col_12 { get; set; }
        public double KerfData_Bottom_Row_12 { get; set; }
        public double KerfData_Bottom_12 { get; set; }
        public double KerfData_Bottom_Col_13 { get; set; }
        public double KerfData_Bottom_Row_13 { get; set; }
        public double KerfData_Bottom_13 { get; set; }
        public double KerfData_Bottom_Col_14 { get; set; }
        public double KerfData_Bottom_Row_14 { get; set; }
        public double KerfData_Bottom_14 { get; set; }
        public double KerfData_Bottom_Col_15 { get; set; }
        public double KerfData_Bottom_Row_15 { get; set; }
        public double KerfData_Bottom_15 { get; set; }
        public double KerfData_Bottom_Col_16 { get; set; }
        public double KerfData_Bottom_Row_16 { get; set; }
        public double KerfData_Bottom_16 { get; set; }
        public double KerfData_Bottom_Col_17 { get; set; }
        public double KerfData_Bottom_Row_17 { get; set; }
        public double KerfData_Bottom_17 { get; set; }
        public double KerfData_Bottom_Col_18 { get; set; }
        public double KerfData_Bottom_Row_18 { get; set; }
        public double KerfData_Bottom_18 { get; set; }
        public double KerfData_Bottom_Col_19 { get; set; }
        public double KerfData_Bottom_Row_19 { get; set; }
        public double KerfData_Bottom_19 { get; set; }
        public double KerfData_Bottom_Col_20 { get; set; }
        public double KerfData_Bottom_Row_20 { get; set; }
        public double KerfData_Bottom_20 { get; set; }
        #endregion

        #region Kerf Width Data
        public double KerfData_01_DIE_COL { get; set; }
        public double KerfData_01_DIE_ROW { get; set; }
        public double KerfData_01_WIDTH_TOP { get; set; }
        public double KerfData_01_WIDTH_RIGHT { get; set; }
        public double KerfData_01_WIDTH_BOTTOM { get; set; }
        public double KerfData_01_WIDTH_LEFT { get; set; }
        public double KerfData_02_DIE_COL { get; set; }
        public double KerfData_02_DIE_ROW { get; set; }
        public double KerfData_02_WIDTH_TOP { get; set; }
        public double KerfData_02_WIDTH_RIGHT { get; set; }
        public double KerfData_02_WIDTH_BOTTOM { get; set; }
        public double KerfData_02_WIDTH_LEFT { get; set; }
        public double KerfData_03_DIE_COL { get; set; }
        public double KerfData_03_DIE_ROW { get; set; }
        public double KerfData_03_WIDTH_TOP { get; set; }
        public double KerfData_03_WIDTH_RIGHT { get; set; }
        public double KerfData_03_WIDTH_BOTTOM { get; set; }
        public double KerfData_03_WIDTH_LEFT { get; set; }
        public double KerfData_04_DIE_COL { get; set; }
        public double KerfData_04_DIE_ROW { get; set; }
        public double KerfData_04_WIDTH_TOP { get; set; }
        public double KerfData_04_WIDTH_RIGHT { get; set; }
        public double KerfData_04_WIDTH_BOTTOM { get; set; }
        public double KerfData_04_WIDTH_LEFT { get; set; }
        public double KerfData_05_DIE_COL { get; set; }
        public double KerfData_05_DIE_ROW { get; set; }
        public double KerfData_05_WIDTH_TOP { get; set; }
        public double KerfData_05_WIDTH_RIGHT { get; set; }
        public double KerfData_05_WIDTH_BOTTOM { get; set; }
        public double KerfData_05_WIDTH_LEFT { get; set; }
        public double KerfData_06_DIE_COL { get; set; }
        public double KerfData_06_DIE_ROW { get; set; }
        public double KerfData_06_WIDTH_TOP { get; set; }
        public double KerfData_06_WIDTH_RIGHT { get; set; }
        public double KerfData_06_WIDTH_BOTTOM { get; set; }
        public double KerfData_06_WIDTH_LEFT { get; set; }
        public double KerfData_07_DIE_COL { get; set; }
        public double KerfData_07_DIE_ROW { get; set; }
        public double KerfData_07_WIDTH_TOP { get; set; }
        public double KerfData_07_WIDTH_RIGHT { get; set; }
        public double KerfData_07_WIDTH_BOTTOM { get; set; }
        public double KerfData_07_WIDTH_LEFT { get; set; }
        public double KerfData_08_DIE_COL { get; set; }
        public double KerfData_08_DIE_ROW { get; set; }
        public double KerfData_08_WIDTH_TOP { get; set; }
        public double KerfData_08_WIDTH_RIGHT { get; set; }
        public double KerfData_08_WIDTH_BOTTOM { get; set; }
        public double KerfData_08_WIDTH_LEFT { get; set; }
        public double KerfData_09_DIE_COL { get; set; }
        public double KerfData_09_DIE_ROW { get; set; }
        public double KerfData_09_WIDTH_TOP { get; set; }
        public double KerfData_09_WIDTH_RIGHT { get; set; }
        public double KerfData_09_WIDTH_BOTTOM { get; set; }
        public double KerfData_09_WIDTH_LEFT { get; set; }
        public double KerfData_10_DIE_COL { get; set; }
        public double KerfData_10_DIE_ROW { get; set; }
        public double KerfData_10_WIDTH_TOP { get; set; }
        public double KerfData_10_WIDTH_RIGHT { get; set; }
        public double KerfData_10_WIDTH_BOTTOM { get; set; }
        public double KerfData_10_WIDTH_LEFT { get; set; }
        public double KerfData_11_DIE_COL { get; set; }
        public double KerfData_11_DIE_ROW { get; set; }
        public double KerfData_11_WIDTH_TOP { get; set; }
        public double KerfData_11_WIDTH_RIGHT { get; set; }
        public double KerfData_11_WIDTH_BOTTOM { get; set; }
        public double KerfData_11_WIDTH_LEFT { get; set; }
        public double KerfData_12_DIE_COL { get; set; }
        public double KerfData_12_DIE_ROW { get; set; }
        public double KerfData_12_WIDTH_TOP { get; set; }
        public double KerfData_12_WIDTH_RIGHT { get; set; }
        public double KerfData_12_WIDTH_BOTTOM { get; set; }
        public double KerfData_12_WIDTH_LEFT { get; set; }
        public double KerfData_13_DIE_COL { get; set; }
        public double KerfData_13_DIE_ROW { get; set; }
        public double KerfData_13_WIDTH_TOP { get; set; }
        public double KerfData_13_WIDTH_RIGHT { get; set; }
        public double KerfData_13_WIDTH_BOTTOM { get; set; }
        public double KerfData_13_WIDTH_LEFT { get; set; }
        public double KerfData_14_DIE_COL { get; set; }
        public double KerfData_14_DIE_ROW { get; set; }
        public double KerfData_14_WIDTH_TOP { get; set; }
        public double KerfData_14_WIDTH_RIGHT { get; set; }
        public double KerfData_14_WIDTH_BOTTOM { get; set; }
        public double KerfData_14_WIDTH_LEFT { get; set; }
        public double KerfData_15_DIE_COL { get; set; }
        public double KerfData_15_DIE_ROW { get; set; }
        public double KerfData_15_WIDTH_TOP { get; set; }
        public double KerfData_15_WIDTH_RIGHT { get; set; }
        public double KerfData_15_WIDTH_BOTTOM { get; set; }
        public double KerfData_15_WIDTH_LEFT { get; set; }
        public double KerfData_16_DIE_COL { get; set; }
        public double KerfData_16_DIE_ROW { get; set; }
        public double KerfData_16_WIDTH_TOP { get; set; }
        public double KerfData_16_WIDTH_RIGHT { get; set; }
        public double KerfData_16_WIDTH_BOTTOM { get; set; }
        public double KerfData_16_WIDTH_LEFT { get; set; }
        public double KerfData_17_DIE_COL { get; set; }
        public double KerfData_17_DIE_ROW { get; set; }
        public double KerfData_17_WIDTH_TOP { get; set; }
        public double KerfData_17_WIDTH_RIGHT { get; set; }
        public double KerfData_17_WIDTH_BOTTOM { get; set; }
        public double KerfData_17_WIDTH_LEFT { get; set; }
        public double KerfData_18_DIE_COL { get; set; }
        public double KerfData_18_DIE_ROW { get; set; }
        public double KerfData_18_WIDTH_TOP { get; set; }
        public double KerfData_18_WIDTH_RIGHT { get; set; }
        public double KerfData_18_WIDTH_BOTTOM { get; set; }
        public double KerfData_18_WIDTH_LEFT { get; set; }
        public double KerfData_19_DIE_COL { get; set; }
        public double KerfData_19_DIE_ROW { get; set; }
        public double KerfData_19_WIDTH_TOP { get; set; }
        public double KerfData_19_WIDTH_RIGHT { get; set; }
        public double KerfData_19_WIDTH_BOTTOM { get; set; }
        public double KerfData_19_WIDTH_LEFT { get; set; }
        public double KerfData_20_DIE_COL { get; set; }
        public double KerfData_20_DIE_ROW { get; set; }
        public double KerfData_20_WIDTH_TOP { get; set; }
        public double KerfData_20_WIDTH_RIGHT { get; set; }
        public double KerfData_20_WIDTH_BOTTOM { get; set; }
        public double KerfData_20_WIDTH_LEFT { get; set; }

        #endregion
        //public double KerfDataCh1_1 { get; set; }
        //public double KerfDataCh1_2 { get; set; }
        //public double KerfDataCh1_3 { get; set; }
        //public double KerfDataCh1_4 { get; set; }
        //public double KerfDataCh1_5 { get; set; }
        //public double KerfDataCh1_6 { get; set; }
        //public double KerfDataCh1_7 { get; set; }
        //public double KerfDataCh1_8 { get; set; }
        //public double KerfDataCh1_9 { get; set; }
        //public double KerfDataCh1_10 { get; set; }
        //public double KerfDataCh1_11 { get; set; }
        //public double KerfDataCh1_12 { get; set; }
        //public double KerfDataCh1_13 { get; set; }
        //public double KerfDataCh1_14 { get; set; }
        //public double KerfDataCh1_15 { get; set; }
        //public double KerfDataCh2_1 { get; set; }
        //public double KerfDataCh2_2 { get; set; }
        //public double KerfDataCh2_3 { get; set; }
        //public double KerfDataCh2_4 { get; set; }
        //public double KerfDataCh2_5 { get; set; }
        //public double KerfDataCh2_6 { get; set; }
        //public double KerfDataCh2_7 { get; set; }
        //public double KerfDataCh2_8 { get; set; }
        //public double KerfDataCh2_9 { get; set; }
        //public double KerfDataCh2_10 { get; set; }
        //public double KerfDataCh2_11 { get; set; }
        //public double KerfDataCh2_12 { get; set; }
        //public double KerfDataCh2_13 { get; set; }
        //public double KerfDataCh2_14 { get; set; }
        //public double KerfDataCh2_15 { get; set; }

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
