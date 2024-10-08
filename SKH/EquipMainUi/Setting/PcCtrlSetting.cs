using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Ini;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;

namespace EquipMainUi.Setting
{
    public class PcCtrlSetting : BaseSetting
    {
        public static string PATH_SETTING = Path.Combine(GG.StartupPath, "Setting", "PcCtrlSetting.ini");
        public MotorSetting Motor = new MotorSetting();
        public InspServerSetting Insp = new InspServerSetting();
        public CtrlSetting Ctrl = new CtrlSetting();
        public AnalogSetting AnalogSet = new AnalogSetting();
        public HsmsSetting Hsms = new HsmsSetting();
        public MongoDBSetting Mongo = new MongoDBSetting();

        public class MotorSetting : BaseSetting
        {
            [IniAttribute("MotorSetting", "StageX_HomeOvertime", 0), DescriptionAttribute("(ms)")]
            public int StageXHomeOvertime { get; set; }
            [IniAttribute("MotorSetting", "InspYHomeOvertime", 0), DescriptionAttribute("(ms)")]
            public int InspYHomeOvertime { get; set; }
            [IniAttribute("MotorSetting", "ThetaHomeOvertime", 0), DescriptionAttribute("(ms)")]
            public int ThetaHomeOvertime { get; set; }
        }
        public class MongoDBSetting : BaseSetting
        {
            #region MongDB
            [IniAttribute("MongoSetting", "LastCycleTime", "20010101")]
            public DateTime LastCycleTime { get; set; }
            [IniAttribute("MongoSetting", "MaxCstCount", 30)]
            public int MaxCstCount { get; set; }
            [IniAttribute("MongoSetting", "DeleteDays", 1)]
            public int DeleteDays { get; set; }
            #endregion
        }

        public class InspServerSetting : BaseSetting
        {
            [IniAttribute("InspServerSetting", "SignalTimeout", 0), DescriptionAttribute("(ms) 제어 to 검사 신호 오버타임")]
            public int SignalTimeout { get; set; }
            [IniAttribute("InspServerSetting", "EventTimeout", 0), DescriptionAttribute("(ms) 검사 to 제어 신호 오버타임")]
            public int EventTimeout { get; set; }
            [IniAttribute("InspServerSetting", "Insp Complete OverTime", 10000), DescriptionAttribute("(ms)")]
            public int InspectionCompleteEventTimeout { get; set; }
            [IniAttribute("InspServerSetting", "AlignOvertime", 0), DescriptionAttribute("(ms)")]
            public int AlignOvertime { get; set; }
            [IniAttribute("InspServerSetting", "AlignTryCount", 1), DescriptionAttribute("Align 재시도 횟수 (초과시 알람)")]
            public int AlignTryCount { get; set; }
            [IniAttribute("InspServerSetting", "ScanTimeover", 0), DescriptionAttribute("(ms) 검사 스캔 오버타임")]
            public int ScanOvertime { get; set; }
            [IniAttribute("InspServerSetting", "JudgeCompleteTimeOver", 0), DescriptionAttribute("(ms)")]
            public int JudgeCompleteTimeOver { get; set; }
            [IniAttribute("InspServerSetting", "ReviewStartDelay", 0), DescriptionAttribute("(ms) Scan 완료 후 리뷰 시작 전 까지 딜레이")]
            public int ReviewStartDelay { get; set; }            
            [IniAttribute("InspServerSetting", "ReviewOvertime", 0), DescriptionAttribute("(ms)")]
            public int ReviewOvertime { get; set; }
            [IniAttribute("InspServerSetting", "TTTMMeasureCycle", 1), DescriptionAttribute("웨이퍼 n장 마다 TTTM 측정 진행 (장)")]
            public int TTTMMeasureCycle { get; set; }
            [IniAttribute("InspServerSetting", "TTTMOverTime", 0), DescriptionAttribute("(ms)")]
            public int TTTMOvertime { get; set; }
        }
        public class CtrlSetting : BaseSetting
        {
            [IniAttribute("CtrlSetting", "CylinderFWDOvertime", 0), DescriptionAttribute("(ms)")]
            public int CylinderFWDOvertime { get; set; }

            [IniAttribute("CtrlSetting", "CylinderBWDOvertime", 0), DescriptionAttribute("(ms)")]
            public int CylinderBWDOvertime { get; set; }

            [IniAttribute("CtrlSetting", "CenteringForwardWait", 0), DescriptionAttribute("(ms) 전진 후 대기 시간")]
            public int CenteringForwardWait { get; set; }

            [IniAttribute("CtrlSetting", "BlowerTime", 0), DescriptionAttribute("(ms) Vacuum Off Step 시 Blow 시간")]
            public int BlowerTime { get; set; }

            [IniAttribute("CtrlSetting", "VacuumOnWaitTime", 0), DescriptionAttribute("(ms) Vacuum On 대기 시간")]
            public int VacuumOnWaitTime { get; set; }

            [IniAttribute("CtrlSetting", "AutoStepTimeout", 0), DescriptionAttribute("(ms) 자동 동작 중 한 스텝에서 설정 시간만큼 멈춰있을 경우 알람 (Scan,Review 제외)")]
            public int AutoStepTimeout { get; set; }

            [IniAttribute("CtrlSetting", "RFReadTryTimes", 0), DescriptionAttribute("LPM RF Reader 읽기 시도 횟수")]
            public int RFReadTryTimes { get; set; }

            [IniAttribute("CtrlSetting", "OCRReadTryTimes", 0), DescriptionAttribute("PreAligner OCR 읽기 시도 횟수")]
            public int OCRReadTryTimes { get; set; }

            [IniAttribute("CtrlSetting", "BCRReadTryTimes", 0), DescriptionAttribute("PreAligner BCR 읽기 시도 횟수")]
            public int BCRReadTryTimes { get; set; }

            [IniAttribute("CtrlSetting", "PreAlignTryTimes", 0), DescriptionAttribute("PreAligner Align 시도 횟수")]
            public int PreAlignTryTimes { get; set; }
            [IniAttribute("CtrlSetting", "PreAlignerXLimit", 0), DescriptionAttribute("(mm) Pre-Aligner 보정값 X Limit")]
            public int PreAlignerXLimit { get; set; }
            [IniAttribute("CtrlSetting", "PreAlignerYLimit", 0), DescriptionAttribute("(mm) Pre-Aligner 보정값 Y Limit")]
            public int PreAlignerYLimit { get; set; }
            [IniAttribute("CtrlSetting", "PreAlignerTLimit", 0), DescriptionAttribute("(mm) Pre-Aligner 보정값 T Limit")]
            public int PreAlignerTLimit { get; set; }
            [IniAttribute("CtrlSetting", "Ionizer Decay Time", 0), DescriptionAttribute("(ms) 설정 Decay Time 시간 안에 알람 기준 설정값 내(±)로 조정되지 않을 때 알람 발생")]
            public int IonizerDecayTime { get; set; }
            [IniAttribute("CtrlSetting", "Ionizer Electronic Standard Value", 0), DescriptionAttribute("(V) 설정 Decay Time 시간 안에 알람 기준 설정값 이하로 조정되지 않을 때 알람 발생")]
            public int IonizerStaticElecAlarmStd { get; set; }
            [IniAttribute("CtrlSetting", "EFEM Reconnect Cycle Time", 10), DescriptionAttribute("(sec) EFEM TCP 통신이 끊겼을 때 설정한 시간(초)마다 재연결 시도")]
            public int EFEMReconnectCycleTime { get; set; }
            // Joo 경로: D:\DitCtrl\Exec\Ctrl\Setting // PcCtrlSetting.ini 파일
            [IniAttribute("CtrlSetting", "DeepLearningDelayTime", 60000), DescriptionAttribute("(ms) Deep Learning Review 판정 시 대기 시간")]
            public int DeepLearningDelayTime { get; set; }
            [IniAttribute("CtrlSetting", "Rf Read Delay Time", 500), DescriptionAttribute("(ms) RF Read 대기 시간 - 재시도 시 현재 진행 LPM 파악 하기 위해")]
            public int RfReadDelayTime { get; set; }
        }

        public class AnalogSetting : BaseSetting
        {
            [Browsable(true)]
            [DisplayNameAttribute("Cp Box Temperture Limit"), IniAttribute("CtrlSetting", "CpBoxAlarmTemp", 40), DescriptionAttribute("(℃) 설정한 온도 이상 감지되면 알람 발생")]
            public int CpBoxAlarmTemp { get; set; }

            [Browsable(true)]
            [DisplayNameAttribute("PC Rack Box Temperture Limit"), IniAttribute("CtrlSetting", "PcRackBoxAlarmTemp", 40), DescriptionAttribute("(℃) 설정한 온도 이상 감지되면 알람 발생")]
            public int PcRackAlarmTemp { get; set; }
        }
        
        public class HsmsSetting : BaseSetting
        {
            [IniAttribute("HsmsSetting", "APCSavePath", "\\")]
            public string APCSavePath { get; set; }
            [IniAttribute("HsmsSetting", "ECIDSavePath", "\\")]
            public string ECIDSavePath { get; set; }
            [IniAttribute("HsmsSetting", "HostECIDSavePath", "\\")]
            public string HostECIDSavePath { get; set; }
            [IniAttribute("HsmsSetting", "TerminalMsgPath", "\\")]
            public string TerminalMsgPath { get; set; }
            [IniAttribute("HsmsSetting", "WaferMapPath", "\\")]
            public string WaferMapPath { get; set; }

            [IniAttribute("HsmsSetting", "Cassette Load Confirm Timeover", 30), DescriptionAttribute("(sec) 설정한 시간 초과시 타임오버 알람 발생")]
            public int CstLoadConfirmTimeover { get; set; }
            [IniAttribute("HsmsSetting", "PP Select Timeover", 30), DescriptionAttribute("(sec) 설정한 시간 초과시 타임오버 알람 발생")]
            public int PPSelectTimeover { get; set; }
            [IniAttribute("HsmsSetting", "Cassette Load Start Cmd Timeover", 30), DescriptionAttribute("(sec) 설정한 시간 초과시 타임오버 알람 발생")]
            public int CstLoadStartCmdTimeover { get; set; }
            [IniAttribute("HsmsSetting", "Cassette Lot Start Confirm Timeover", 30), DescriptionAttribute("(sec) 설정한 시간 초과시 타임오버 알람 발생")]
            public int CstLotStartConfirmTimeover { get; set; }
            [IniAttribute("HsmsSetting", "WaferLoadConfirmtWait", 30), Description("(sec) 설정한 시간 초과시 타임오버 알람 발생")]
            public int WaferLoadConfirmWait { get; set; }
            [IniAttribute("HsmsSetting", "WaferMapRequestWait", 30), Description("(sec) 설정한 시간 초과시 타임오버 알람 발생")]
            public int WaferMapRequestWait { get; set; }
        }


        #region STATUS
        [IniAttribute("Setting", "PPIDUseMode", 0)]
        public int AutoRecipeChange { get; set; }
        [IniAttribute("Setting", "IsRFKeyIn", true)]
        public bool IsRFKeyIn { get; set; }
        [IniAttribute("Setting", "UseOCR", true)]
        public bool UseOCR { get; set; }
        [IniAttribute("Setting", "UseOnlyReadOCR", true)]
        public bool UseOnlyReadOCR { get; set; }
        [IniAttribute("Setting", "UseBCR", true)]
        public bool UseBCR { get; set; }
        [IniAttribute("Setting", "LPM1LoadType", 0)]
        public int LPMLoadType { get; set; }
        [IniAttribute("Setting", "LPM1ProgressWay", 0)]
        public int LPMProgressWay { get; set; }
        [IniAttribute("Setting", "LPM2LoadType", 0)]
        public int SerialCheckCycle { get; set; }
        // Joo 경로: D:\DitCtrl\Exec\Ctrl\Setting // PcCtrlSetting.ini 파일
        [IniAttribute("Setting", "ReviewJudgeMode", false)]
        public bool ReviewJudgeMode { get; set; }

        [IniAttribute("Setting", "NextInspCount", 0)]
        public int NextInspCount { get; set; }

        // KYH 230913-01 : ReviewFailCount, AutoMoveOutCount는 포트별로 가져야 한다.
        //[IniAttribute("Setting", "ReviewFailCount", 0)]
        //public int ReviewFailCount { get; set; }
        //[IniAttribute("Setting", "AutoMoveOutCount", 0)]
        //public int AutoMoveOutCount { get; set; }

        [IniAttribute("Setting", "ReviewFailCount1", 0)]
        public int ReviewFailCount1 { get; set; }
        [IniAttribute("Setting", "AutoMoveOutCount1", 0)]
        public int AutoMoveOutCount1 { get; set; }
        [IniAttribute("Setting", "ReviewFailCount2", 0)]
        public int ReviewFailCount2 { get; set; }
        [IniAttribute("Setting", "AutoMoveOutCount2", 0)]
        public int AutoMoveOutCount2 { get; set; }


        [IniAttribute("Setting", "InspRepeatCount", 0)]
        public int InspRepeatCount { get; set; }
        [IniAttribute("Setting", "InspRepeatMode", false)]
        public bool InspRepeatMode { get; set; }

        public int slotProcessCount { get; set; }
        #endregion



        public PcCtrlSetting()
        {
            UseAutoBackup = true;
        }

        /**
        *@param : path - null : 기본 경로
        *            not null : 경로 변경
        *               */
        public override bool Save(string path = null)
        {
            if (null != path)
                PATH_SETTING = path;
            if (!Directory.Exists(Path.GetPathRoot(PATH_SETTING)))
                Directory.CreateDirectory(PATH_SETTING.Remove(PATH_SETTING.LastIndexOf('\\')));

            Ctrl.Save(PATH_SETTING);
            Insp.Save(PATH_SETTING);
            Motor.Save(PATH_SETTING);
            AnalogSet.Save(PATH_SETTING);
            Hsms.Save(PATH_SETTING);
            Mongo.Save(PATH_SETTING);

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

            Ctrl.Load(PATH_SETTING);
            Insp.Load(PATH_SETTING);
            Motor.Load(PATH_SETTING);
            AnalogSet.Load(PATH_SETTING);
            Hsms.Load(PATH_SETTING);
            Mongo.Save(PATH_SETTING);

            return base.Load(PATH_SETTING);
        }
        public void ToggleAutoRecipeChangeMode()
        {
            if (AutoRecipeChange == 1)
                AutoRecipeChange = 0;
            else
                AutoRecipeChange = 1;

            Save();
        }
    }
}