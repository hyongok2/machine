﻿using System;
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
            [IniAttribute("InspServerSetting", "SignalTimeout", 0), DescriptionAttribute(GG.boChinaLanguage ? "(ms) 控制 -> 检查，信号超时" : "(ms) 제어 to 검사 신호 오버타임")]
            public int SignalTimeout { get; set; }
            [IniAttribute("InspServerSetting", "EventTimeout", 0), DescriptionAttribute(GG.boChinaLanguage ? "(ms) 检查 -> 控制 信号超时" : "(ms) 검사 to 제어 신호 오버타임")]
            public int EventTimeout { get; set; }
            [IniAttribute("InspServerSetting", "Insp Complete OverTime", 10000), DescriptionAttribute("(ms)")]
            public int InspectionCompleteEventTimeout { get; set; }
            [IniAttribute("InspServerSetting", "AlignOvertime", 0), DescriptionAttribute("(ms)")]
            public int AlignOvertime { get; set; }
            [IniAttribute("InspServerSetting", "AlignTryCount", 1), DescriptionAttribute(GG.boChinaLanguage ? "Align 重试次数(超过重试次数时报警)" : "Align 재시도 횟수 (초과시 알람)")]
            public int AlignTryCount { get; set; }
            [IniAttribute("InspServerSetting", "ScanTimeover", 0), DescriptionAttribute(GG.boChinaLanguage ? "(ms) Scan 信号超时" : "(ms) 검사 스캔 오버타임")]
            public int ScanOvertime { get; set; }
            [IniAttribute("InspServerSetting", "JudgeCompleteTimeOver", 0), DescriptionAttribute("(ms)")]
            public int JudgeCompleteTimeOver { get; set; }
            [IniAttribute("InspServerSetting", "ReviewStartDelay", 0), DescriptionAttribute(GG.boChinaLanguage ? "(ms) Scan 完毕后, Review 开始前为止, Delay" : "(ms) Scan 완료 후 리뷰 시작 전 까지 딜레이")]
            public int ReviewStartDelay { get; set; }            
            [IniAttribute("InspServerSetting", "ReviewOvertime", 0), DescriptionAttribute("(ms)")]
            public int ReviewOvertime { get; set; }
            [IniAttribute("InspServerSetting", "TTTMMeasureCycle", 1), DescriptionAttribute(GG.boChinaLanguage ? "每n张Wafer 进行测量 TTTM" : "웨이퍼 n장 마다 TTTM 측정 진행 (장)")]
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

            [IniAttribute("CtrlSetting", "CenteringForwardWait", 0), DescriptionAttribute(GG.boChinaLanguage ? "(ms) 前进后，待机时间" : "(ms) 전진 후 대기 시간")]
            public int CenteringForwardWait { get; set; }

            [IniAttribute("CtrlSetting", "BlowerTime", 0), DescriptionAttribute(GG.boChinaLanguage ? "(ms) Vacuum Off Step 时, Blow 时间" : "(ms) Vacuum Off Step 시 Blow 시간")]
            public int BlowerTime { get; set; }

            [IniAttribute("CtrlSetting", "VacuumOnWaitTime", 0), DescriptionAttribute(GG.boChinaLanguage ? "(ms) Vacuum On 待机时间" : "(ms) Vacuum On 대기 시간")]
            public int VacuumOnWaitTime { get; set; }

            [IniAttribute("CtrlSetting", "AutoStepTimeout", 0), DescriptionAttribute(GG.boChinaLanguage ? "(ms) 自动动作中，在一个Step中停止了设定的时间为止时报警 (Scan, Review 除外)" : "(ms) 자동 동작 중 한 스텝에서 설정 시간만큼 멈춰있을 경우 알람 (Scan,Review 제외)")]
            public int AutoStepTimeout { get; set; }

            [IniAttribute("CtrlSetting", "RFReadTryTimes", 0), DescriptionAttribute(GG.boChinaLanguage ? "LPM RF Reader 尝试读取次数" : "LPM RF Reader 읽기 시도 횟수")]
            public int RFReadTryTimes { get; set; }

            [IniAttribute("CtrlSetting", "OCRReadTryTimes", 0), DescriptionAttribute(GG.boChinaLanguage ? "PreAligner OCR 尝试读取次数" : "PreAligner OCR 읽기 시도 횟수")]
            public int OCRReadTryTimes { get; set; }

            [IniAttribute("CtrlSetting", "BCRReadTryTimes", 0), DescriptionAttribute(GG.boChinaLanguage ? "PreAligner BCR 尝试读取次数" : "PreAligner BCR 읽기 시도 횟수")]
            public int BCRReadTryTimes { get; set; }

            [IniAttribute("CtrlSetting", "PreAlignTryTimes", 0), DescriptionAttribute(GG.boChinaLanguage ? "PreAligner 尝试Align次数" : "PreAligner Align 시도 횟수")]
            public int PreAlignTryTimes { get; set; }
            [IniAttribute("CtrlSetting", "PreAlignerXLimit", 0), DescriptionAttribute(GG.boChinaLanguage ? "(mm) Pre-Aligner 补正值 X Limit" : "(mm) Pre-Aligner 보정값 X Limit")]
            public int PreAlignerXLimit { get; set; }
            [IniAttribute("CtrlSetting", "PreAlignerYLimit", 0), DescriptionAttribute(GG.boChinaLanguage ? "(mm) Pre-Aligner 补正值 Y Limit" : "(mm) Pre-Aligner 보정값 Y Limit")]
            public int PreAlignerYLimit { get; set; }
            [IniAttribute("CtrlSetting", "PreAlignerTLimit", 0), DescriptionAttribute(GG.boChinaLanguage ? "(mm) Pre-Aligner 补正值 T Limit" : "(mm) Pre-Aligner 보정값 T Limit")]
            public int PreAlignerTLimit { get; set; }
            [IniAttribute("CtrlSetting", "Ionizer Decay Time", 0), DescriptionAttribute(GG.boChinaLanguage ? "(ms) 设定的时间里，未能调整到设定的报警基准时间内时，发生报警" : "(ms) 설정 Decay Time 시간 안에 알람 기준 설정값 내(±)로 조정되지 않을 때 알람 발생")]
            public int IonizerDecayTime { get; set; }
            [IniAttribute("CtrlSetting", "Ionizer Electronic Standard Value", 0), DescriptionAttribute(GG.boChinaLanguage ? "(V) 设定的时间里，未能调整到设定的报警基准时间以下时，发生报警" : "(V) 설정 Decay Time 시간 안에 알람 기준 설정값 이하로 조정되지 않을 때 알람 발생")]
            public int IonizerStaticElecAlarmStd { get; set; }
            [IniAttribute("CtrlSetting", "EFEM Reconnect Cycle Time", 10), DescriptionAttribute(GG.boChinaLanguage ? "(sec) EFEM 通讯断开时, 每设置的时间（秒）尝试重新连接" : "(sec) EFEM TCP 통신이 끊겼을 때 설정한 시간(초)마다 재연결 시도")]
            public int EFEMReconnectCycleTime { get; set; }

            // Joo 경로: D:\DitCtrl\Exec\Ctrl\Setting // PcCtrlSetting.ini 파일
            [IniAttribute("CtrlSetting", "DeepLearningDelayTime", 60000), DescriptionAttribute(GG.boChinaLanguage ? "(ms) Deep Learning Review 等待决定的时间" : "(ms) Deep Learning Review 판정 시 대기 시간")]
            public int DeepLearningDelayTime { get; set; }
        }

        public class AnalogSetting : BaseSetting
        {
            [Browsable(true)]
            [DisplayNameAttribute("Cp Box Temperture Limit"), IniAttribute("CtrlSetting", "CpBoxAlarmTemp", 40), DescriptionAttribute(GG.boChinaLanguage ? "(℃) 设定温度感应异常时，发生Alarm" : "(℃) 설정한 온도 이상 감지되면 알람 발생")]
            public int CpBoxAlarmTemp { get; set; }

            [Browsable(true)]
            [DisplayNameAttribute("PC Rack Box Temperture Limit"), IniAttribute("CtrlSetting", "PcRackBoxAlarmTemp", 40), DescriptionAttribute(GG.boChinaLanguage ? "(℃) 设定温度感应异常时，发生Alarm" : "(℃) 설정한 온도 이상 감지되면 알람 발생")]
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

            [IniAttribute("HsmsSetting", "Cassette Load Confirm Timeover", 30), DescriptionAttribute(GG.boChinaLanguage ? "(sec) 设置的时间超时时，发生超时 Alarm" : "(sec) 설정한 시간 초과시 타임오버 알람 발생")]
            public int CstLoadConfirmTimeover { get; set; }
            [IniAttribute("HsmsSetting", "PP Select Timeover", 30), DescriptionAttribute(GG.boChinaLanguage ? "(sec) 设置的时间超时时，发生超时 Alarm" : "(sec) 설정한 시간 초과시 타임오버 알람 발생")]
            public int PPSelectTimeover { get; set; }
            [IniAttribute("HsmsSetting", "Cassette Load Start Cmd Timeover", 30), DescriptionAttribute(GG.boChinaLanguage ? "(sec) 设置的时间超时时，发生超时 Alarm" : "(sec) 설정한 시간 초과시 타임오버 알람 발생")]
            public int CstLoadStartCmdTimeover { get; set; }
            [IniAttribute("HsmsSetting", "Cassette Lot Start Confirm Timeover", 30), DescriptionAttribute(GG.boChinaLanguage ? "(sec) 设置的时间超时时，发生超时 Alarm" : "(sec) 설정한 시간 초과시 타임오버 알람 발생")]
            public int CstLotStartConfirmTimeover { get; set; }
            [IniAttribute("HsmsSetting", "WaferLoadConfirmtWait", 30), Description(GG.boChinaLanguage ? "(sec) 设置的时间超时时，发生超时 Alarm" : "(sec) 설정한 시간 초과시 타임오버 알람 발생")]
            public int WaferLoadConfirmWait { get; set; }
            [IniAttribute("HsmsSetting", "WaferMapRequestWait", 30), Description(GG.boChinaLanguage ? "(sec) 设置的时间超时时，发生超时 Alarm" : "(sec) 설정한 시간 초과시 타임오버 알람 발생")]
            public int WaferMapRequestWait { get; set; }
            [IniAttribute("HsmsSetting", "OHT Load Request Interval", 30), Description(GG.boChinaLanguage ? "(sec) OHT Load Request 报告的间距" : "(sec) OHT Load Request 보고 간격")]
            public int OHTLoadRequestInterval { get; set; }
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
        [IniAttribute("Setting", "AirKnifeUse", false)]
        public bool AirKnifeUse { get; set; }

        // Joo 경로: D:\DitCtrl\Exec\Ctrl\Setting // PcCtrlSetting.ini 파일
        [IniAttribute("Setting", "ReviewJudgeMode", false)]
        public bool ReviewJudgeMode { get; set; }
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