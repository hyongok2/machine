using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipMainUi.Struct
{
    public class GlassInfo
    {
        public EMPanelState PanelState;

        public string CstID { get; set; }
        public string RFReadCstID { get; set; }
        public string WaferID { get; set; }
        public string BarcodeReadWaferID { get; set; }
        public string RecipeID { get; set; }

        #region Official
        public string HGlassID;
        public string EGlassID;
        public string LotID;
        public string BatchID;
        public string JobID;
        public string PortID;
        public string SlotID;

        public string ProductType;
        public string ProductKind;
        public string ProductID;
        public string RunSpecID;
        public string LayerID;
        public string StepID;
        public string PPID;
        public string FlowID;
        public int[] GlassSize;
        public int GlassThickness;
        public int GlassState; // EMPanelState
        public string GlassOrder;
        public string Comment;

        public string UseCount;
        public string Judgement;
        public string ReasonCode;
        public string InsFlag;
        public string EncFlag;
        public string PrerunFlag;
        public string TurnDir;
        public string FlipState;
        public string WorkState;
        public string MultiUse;

        public string PairGlassID;
        public string PairPPID;

        public string OptionName1;
        public string OptionValue1;
        public string OptionName2;
        public string OptionValue2;
        public string OptionName3;
        public string OptionValue3;
        public string OptionName4;
        public string OptionValue4;
        public string OptionName5;
        public string OptionValue5;
        #endregion

        public int LotFlag;
        public string Etc;

        public int CSIF;
        public int AS;
        public int APS;
        public int UniqueID;
        public int BitSignal;
        public int StagePnlAbort;

        //프로그램상에서 사용 
        public string MainLotId { get; set; }

        public int SlotSeq = 0;
        public int PortSeq = 0;
        
        public GlassInfo()
        {
            GlassSize = new int[2];

            CstID = string.Empty;
            RFReadCstID = string.Empty;
            WaferID = string.Empty;
            BarcodeReadWaferID = string.Empty;
            RecipeID = string.Empty;

            HGlassID = string.Empty;
            EGlassID = string.Empty;
            LotID = string.Empty;
            BatchID = string.Empty;
            JobID = string.Empty;
            PortID = string.Empty;
            SlotID = string.Empty;
            ProductType = string.Empty;
            ProductKind = string.Empty;
            ProductID = string.Empty;
            RunSpecID = string.Empty;
            LayerID = string.Empty;
            StepID = string.Empty;
            PPID = string.Empty;
            FlowID = string.Empty;
            GlassSize[0] = 0;
            GlassSize[1] = 0;
            GlassThickness = 0;
            GlassState = (short)EMPanelState.EMPTY;
            GlassOrder = string.Empty;
            Comment = string.Empty;

            UseCount = string.Empty;
            Judgement = string.Empty;
            ReasonCode = string.Empty;
            InsFlag = string.Empty;
            EncFlag = string.Empty;
            PrerunFlag = string.Empty;
            TurnDir = string.Empty;
            FlipState = string.Empty;
            WorkState = string.Empty;
            MultiUse = string.Empty;

            PairGlassID = string.Empty;
            PairPPID = string.Empty;

            OptionName1 = string.Empty;
            OptionValue1 = string.Empty;
            OptionName2 = string.Empty;
            OptionValue2 = string.Empty;
            OptionName3 = string.Empty;
            OptionValue3 = string.Empty;
            OptionName4 = string.Empty;
            OptionValue4 = string.Empty;
            OptionName5 = string.Empty;
            OptionValue5 = string.Empty;

            LotFlag = 0;
            Etc = string.Empty;
            MainLotId = string.Empty;

            StagePnlAbort = 0;

            CSIF = 0;
            AS = 0;
            APS = 0;
            UniqueID = 0;
            BitSignal = 0;
            CstID = string.Empty;

            MainLotId = string.Empty;
            CstID = string.Empty;
        }

        public void CopyData(GlassInfo targetGlassInfo)
        {
            this.CstID = targetGlassInfo.CstID;
            this.RFReadCstID = targetGlassInfo.RFReadCstID;
            this.WaferID = targetGlassInfo.WaferID;
            this.BarcodeReadWaferID = targetGlassInfo.BarcodeReadWaferID;
            this.RecipeID = targetGlassInfo.RecipeID;

            //this.HGlassID = targetGlassInfo.HGlassID;
            //this.EGlassID = targetGlassInfo.EGlassID;
            //this.LotID = targetGlassInfo.LotID;
            //this.BatchID = targetGlassInfo.BatchID;
            //this.JobID = targetGlassInfo.JobID;
            //this.PortID = targetGlassInfo.PortID;
            //this.SlotID = targetGlassInfo.SlotID;
            //this.ProductType = targetGlassInfo.ProductType;
            //this.ProductKind = targetGlassInfo.ProductKind;
            //this.ProductID = targetGlassInfo.ProductID;
            //this.RunSpecID = targetGlassInfo.RunSpecID;
            //this.LayerID = targetGlassInfo.LayerID;
            //this.StepID = targetGlassInfo.StepID;
            //this.PPID = targetGlassInfo.PPID;
            //this.FlowID = targetGlassInfo.FlowID;
            //this.GlassSize[0] = targetGlassInfo.GlassSize[0];
            //this.GlassSize[1] = targetGlassInfo.GlassSize[1];
            //this.GlassThickness = targetGlassInfo.GlassThickness;
            //this.GlassState = targetGlassInfo.GlassState;
            //this.GlassOrder = targetGlassInfo.GlassOrder;
            //this.Comment = targetGlassInfo.Comment;

            //this.UseCount = targetGlassInfo.UseCount;
            //this.Judgement = targetGlassInfo.Judgement;
            //this.ReasonCode = targetGlassInfo.ReasonCode;
            //this.InsFlag = targetGlassInfo.InsFlag;
            //this.EncFlag = targetGlassInfo.EncFlag;
            //this.PrerunFlag = targetGlassInfo.PrerunFlag;
            //this.TurnDir = targetGlassInfo.TurnDir;
            //this.FlipState = targetGlassInfo.FlipState;
            //this.WorkState = targetGlassInfo.WorkState;
            //this.MultiUse = targetGlassInfo.MultiUse;

            //this.PairGlassID = targetGlassInfo.PairGlassID;
            //this.PairPPID = targetGlassInfo.PairPPID;

            //this.OptionName1 = targetGlassInfo.OptionName1;
            //this.OptionValue1 = targetGlassInfo.OptionValue1;
            //this.OptionName2 = targetGlassInfo.OptionName2;
            //this.OptionValue2 = targetGlassInfo.OptionValue2;
            //this.OptionName3 = targetGlassInfo.OptionName3;
            //this.OptionValue3 = targetGlassInfo.OptionValue3;
            //this.OptionName4 = targetGlassInfo.OptionName4;
            //this.OptionValue4 = targetGlassInfo.OptionValue4;
            //this.OptionName5 = targetGlassInfo.OptionName5;
            //this.OptionValue5 = targetGlassInfo.OptionValue5;

            //this.LotFlag = targetGlassInfo.LotFlag;

            //this.StagePnlAbort = targetGlassInfo.StagePnlAbort;
            //this.OctaPnlPosi = targetGlassInfo.OctaPnlPosi;
            //this.FullPnlPosi = targetGlassInfo.FullPnlPosi;

            //this.Etc = targetGlassInfo.Etc;

            //this.CSIF = targetGlassInfo.CSIF;
            //this.AS = targetGlassInfo.AS;
            //this.APS = targetGlassInfo.APS;
            //this.UniqueID = targetGlassInfo.UniqueID;
            //this.BitSignal = targetGlassInfo.BitSignal;

            //생성시. 초기화 데이터
            //SlotSeq 
            //PortSeq 
        }
        
    }
}
