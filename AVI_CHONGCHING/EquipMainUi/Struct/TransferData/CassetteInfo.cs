using MongoDB.Bson;
using System;
using System.ComponentModel;

namespace EquipMainUi.Struct.TransferData
{

    public class CassetteInfoKey
    {
        public string ID { get; set; }

        public void Clear()
        {
            ID = string.Empty;
        }
    }
    [Serializable]
    public class CassetteInfo
    {
        [Browsable(false)]
        public ObjectId Id { get; set; }
        [ReadOnly(false)]
        public string CstID { get; set; }
        public string LotID { get; set; }
        public int SlotCount { get; set; }

        public int LoadPortNo { get; set; }

        public string RecipeName { get; set; }

        private DateTime _inputDate;
        public DateTime InputDate { get { return _inputDate.ToLocalTime(); } set { _inputDate = value; } }
        private DateTime _outputDate;
        public DateTime OutputDate { get { return _outputDate.ToLocalTime(); } set { _outputDate = value; } }
        public string HWaferIDList { get; set; }

        public int NextWaferIdx { get; set; }
        public bool IsProgressing { get; set; }
        public bool IsLotStartOK { get; set; }
        public CassetteInfo() { }

        public CassetteInfo(string id, int loadPortNo)
        {
            CstID = id;
            SlotCount = 0;
            LoadPortNo = loadPortNo;
            InputDate = DateTime.Now;

            OutputDate = DateTime.MinValue;
            NextWaferIdx = 1;

            IsProgressing = false;
            IsLotStartOK = false;
        }

        public static CassetteInfo CreateSampleCassetteInfo(string cstID, int loadPortNo)
        {
            CassetteInfo instance = new CassetteInfo(cstID, loadPortNo);
            instance.SlotCount = 13;

            return instance;
        }
        public CassetteInfoKey ToKey()
        {
            return new CassetteInfoKey() { ID = this.CstID };
        }

        public bool Update()
        {
            return TransferDataMgr.UpdateCst(this);
        }
    }
}
