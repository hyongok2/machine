using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Ini;
using System.Windows.Forms;
using System.IO;
using EquipMainUi.Struct;

namespace EquipMainUi.Setting
{
    public class GlassInfoBackup : BaseSetting
    {
        public static string PATH_SETTING = Path.Combine(Application.StartupPath, "GlassInfo", "GlassBackup.bak");

        public string SavePath { get; set; }

        [IniAttribute("GlassInfo", "CSTID", 0)]
        public string CSTID { get; set; }

        [IniAttribute("GlassInfo", "RFREADCSTID", 0)]
        public string RFREADCSTID { get; set; }

        [IniAttribute("GlassInfo", "WAFERID", 0)]
        public string WAFERID { get; set; }

        [IniAttribute("GlassInfo", "BARCODEREADWAFERID", 0)]
        public string BARCODEREADWAFERID { get; set; }

        [IniAttribute("GlassInfo", "RECIPEID", 0)]
        public string RECIPEID { get; set; }

        public GlassInfoBackup(string path)
        {
            SavePath = Path.Combine(Application.StartupPath, "Setting", path); ;
        }

        /**
        *@param : path - null : 기본 경로
        *            not null : 경로 변경
        *               */
        public override bool Save(string path = null)
        {
            if (null != path)
                PATH_SETTING = path;
            if (!Directory.Exists(PATH_SETTING))
                Directory.CreateDirectory(PATH_SETTING.Remove(PATH_SETTING.LastIndexOf('\\')));

            return base.Save(PATH_SETTING);
        }

        /**
         *@param : path - null : 기본 경로
         *            not null : 경로 변경
         *               */
        public override bool Load(string path = null)
        {
            if (null != path)
                PATH_SETTING = path;
            return base.Load(PATH_SETTING);
        }
        public void Backup(GlassInfo gls)
        {
            this.CSTID = gls.CstID;
            this.RFREADCSTID = gls.RFReadCstID;
            this.WAFERID = gls.WaferID;
            this.BARCODEREADWAFERID = gls.BarcodeReadWaferID;
            this.RECIPEID = gls.RecipeID;

            //this.HGLASSID = gls.HGlassID;
            //this.EGLASSID = gls.EGlassID;
            //this.LOTID = gls.LotID;
            //this.BATCHID = gls.BatchID;
            //this.JOBID = gls.JobID;
            //this.PORTID = gls.PortID;
            //this.SLOTID = gls.SlotID;
            //this.PRODUCTTYPE = gls.ProductType;
            //this.PRODUCTKIND = gls.ProductKind;
            //this.PRODUCTID = gls.ProductID;
            //this.RUNSPECID = gls.RunSpecID;
            //this.LAYERID = gls.LayerID;
            //this.STEPID = gls.StepID;
            //this.PPID = gls.PPID;
            //this.FLOWID = gls.FlowID;
            //this.GLASSSIZE0 = gls.GlassSize[0];
            //this.GLASSSIZE1 = gls.GlassSize[1];
            //this.GLASSTHICKNESS = gls.GlassThickness;
            //this.GLASSSTATE = gls.GlassState;
            //this.GLASSORDER = gls.GlassOrder;
            //this.COMMENT = gls.Comment;

            //this.USECOUNT = gls.UseCount;
            //this.JUDGEMENT = gls.Judgement;
            //this.REASONCODE = gls.ReasonCode;
            //this.INSFLAG = gls.InsFlag;
            //this.ENCFLAG = gls.EncFlag;
            //this.PRERUNFLAG = gls.PrerunFlag;
            //this.TUNDIR = gls.TurnDir;
            //this.FLIPSTATE = gls.FlipState;
            //this.WORKSTATE = gls.WorkState;
            //this.MULTIUSE = gls.MultiUse;

            //this.PAIRGLASSID = gls.PairGlassID;
            //this.PAIRPPID = gls.PairPPID;

            //this.OPTIONNAME1 = gls.OptionName1;
            //this.OPTIONVALUE1 = gls.OptionValue1;
            //this.OPTIONNAME2 = gls.OptionName2;
            //this.OPTIONVALUE2 = gls.OptionValue2;
            //this.OPTIONNAME3 = gls.OptionName3;
            //this.OPTIONVALUE3 = gls.OptionValue3;
            //this.OPTIONNAME4 = gls.OptionName4;
            //this.OPTIONVALUE4 = gls.OptionValue4;
            //this.OPTIONNAME5 = gls.OptionName5;
            //this.OPTIONVALUE5 = gls.OptionValue5;

            //this.LOTFLAG = gls.LotFlag;

            //this.CSIF = gls.CSIF;
            //this.AS = gls.AS;
            //this.APS = gls.APS;
            //this.UNIQUEID = gls.UniqueID;
            //this.BITSIGNAL = gls.BitSignal;

            //this.MAINLOTID = gls.MainLotId;
            //this.CSTID = gls.CstID; ;

            //this.STAGEPANELABORT = gls.StagePnlAbort;
            //this.FULLPNLPOSI = gls.FullPnlPosi;
            //this.OCTAPNLPOSI = gls.OctaPnlPosi;

            Save(SavePath);
        }
        public GlassInfo GetBackupData()
        {
            GlassInfo gls = new GlassInfo();

            gls.CstID = this.CSTID;
            gls.RFReadCstID = this.RFREADCSTID;
            gls.WaferID = this.WAFERID;
            gls.BarcodeReadWaferID = this.BARCODEREADWAFERID;
            gls.RecipeID = this.RECIPEID;
            //gls.HGlassID = this.HGLASSID;
            //gls.EGlassID = this.EGLASSID;
            //gls.LotID = this.LOTID;
            //gls.BatchID = this.BATCHID;
            //gls.JobID = this.JOBID;
            //gls.PortID = this.PORTID;
            //gls.SlotID = this.SLOTID;
            //gls.ProductType = this.PRODUCTTYPE;
            //gls.ProductKind = this.PRODUCTKIND;
            //gls.ProductID = this.PRODUCTID;
            //gls.RunSpecID = this.RUNSPECID;
            //gls.LayerID = this.LAYERID;
            //gls.StepID = this.STEPID;
            //gls.PPID = this.PPID;
            //gls.FlowID = this.FLOWID;
            //gls.GlassSize[0] = this.GLASSSIZE0;
            //gls.GlassSize[1] = this.GLASSSIZE1;
            //gls.GlassThickness = this.GLASSTHICKNESS;
            //gls.GlassState = this.GLASSSTATE;
            //gls.GlassOrder = this.GLASSORDER;
            //gls.Comment = this.COMMENT;

            //gls.UseCount = this.USECOUNT;
            //gls.Judgement = this.JUDGEMENT;
            //gls.ReasonCode = this.REASONCODE;
            //gls.InsFlag = this.INSFLAG;
            //gls.EncFlag = this.ENCFLAG;
            //gls.PrerunFlag = this.PRERUNFLAG;
            //gls.TurnDir = this.TUNDIR;
            //gls.FlipState = this.FLIPSTATE;
            //gls.WorkState = this.WORKSTATE;
            //gls.MultiUse = this.MULTIUSE;

            //gls.PairGlassID = this.PAIRGLASSID;
            //gls.PairPPID = this.PAIRPPID;

            //gls.OptionName1 = this.OPTIONNAME1;
            //gls.OptionValue1 = this.OPTIONVALUE1;
            //gls.OptionName2 = this.OPTIONNAME2;
            //gls.OptionValue2 = this.OPTIONVALUE2;
            //gls.OptionName3 = this.OPTIONNAME3;
            //gls.OptionValue3 = this.OPTIONVALUE3;
            //gls.OptionName4 = this.OPTIONNAME4;
            //gls.OptionValue4 = this.OPTIONVALUE4;
            //gls.OptionName5 = this.OPTIONNAME5;
            //gls.OptionValue5 = this.OPTIONVALUE5;

            //gls.LotFlag = this.LOTFLAG;

            //gls.CSIF = this.CSIF;
            //gls.AS = this.AS;
            //gls.APS = this.APS;
            //gls.UniqueID = this.UNIQUEID;
            //gls.BitSignal = this.BITSIGNAL;

            //gls.MainLotId = this.MAINLOTID;
            //gls.CstID = this.CSTID;

            //gls.StagePnlAbort = this.STAGEPANELABORT;
            //gls.FullPnlPosi = this.FULLPNLPOSI;
            //gls.OctaPnlPosi = this.OCTAPNLPOSI;

            return gls;
        }
    }
}