using EquipMainUi.Struct.Detail.EFEM.Step;
using EquipMainUi.Struct.TransferData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EquipMainUi.Struct.Detail.EFEM
{

    public class EFEMStatus
    {
        public bool IsInitComplete { get; set; }
        public bool IsError { get; set; }
        public bool IsDoorClose { get; set; }
        public bool IsEMO { get; set; }
        public bool IsLPMCDAError { get; set; }
        public bool IsLPMVacError { get; set; }
        public bool IsMainIonizerError { get; set; }
        public bool IsRobotVaccumError { get; set; }
        public EmEfemLampBuzzerState TowerLampRed { get; set; }
        public EmEfemLampBuzzerState TowerLampYellow { get; set; }
        public EmEfemLampBuzzerState TowerLampGreen { get; set; }
        public EmEfemLampBuzzerState TowerLampBlue { get; set; }
        public bool IsAutoMode { get; set; }
        public bool IsModeSwitchAuto { get; set; }
        public EmEfemSafetyPLCState SafetyPlcState { get; set; }
        public EFEMStatus()
        {
            IsDoorClose = true;
        }
        public bool CopyTo(ref EFEMStatus dest)
        {
            dest.IsInitComplete         = this.IsInitComplete;
            dest.IsError                = this.IsError;
            dest.IsDoorClose            = this.IsDoorClose;
            dest.IsEMO                  = this.IsEMO;
            dest.IsLPMCDAError          = this.IsLPMCDAError;
            dest.IsLPMVacError          = this.IsLPMVacError;
            dest.IsMainIonizerError     = this.IsMainIonizerError;
            dest.IsRobotVaccumError     = this.IsRobotVaccumError;
            dest.TowerLampRed       = this.TowerLampRed;
            dest.TowerLampYellow    = this.TowerLampYellow;
            dest.TowerLampGreen     = this.TowerLampGreen;
            dest.TowerLampBlue      = this.TowerLampBlue;
            dest.IsAutoMode             = this.IsAutoMode;
            dest.IsModeSwitchAuto       = this.IsModeSwitchAuto;
            dest.SafetyPlcState         = this.SafetyPlcState;
            return true;
        }

        public void Set(string statusData)
        {
            if (statusData.Length != 15)
                throw new Exception("EFEM STATUS DATA LENGTH ERROR");

            int i = 0;
            IsInitComplete        = statusData[i++] == '1' ? true : false;
            IsError               = statusData[i++] == '1' ? true : false;
            IsDoorClose           = statusData[i++] == '1' ? true : false;
            IsEMO                 = statusData[i++] == '0' ? true : false;
            IsLPMCDAError         = statusData[i++] == '1' ? true : false;
            IsLPMVacError         = statusData[i++] == '1' ? true : false;
            IsMainIonizerError    = statusData[i++] == '1' ? true : false;
            IsRobotVaccumError    = statusData[i++] == '1' ? true : false;
            TowerLampRed      = (EmEfemLampBuzzerState)int.Parse(statusData[i++].ToString());
            TowerLampYellow   = (EmEfemLampBuzzerState)int.Parse(statusData[i++].ToString());
            TowerLampGreen    = (EmEfemLampBuzzerState)int.Parse(statusData[i++].ToString());
            TowerLampBlue     = (EmEfemLampBuzzerState)int.Parse(statusData[i++].ToString());
            IsAutoMode            = statusData[i++] == '1' ? true : false;
            IsModeSwitchAuto      = statusData[i++] == '1' ? true : false;
            SafetyPlcState        = (EmEfemSafetyPLCState)int.Parse(statusData[i++].ToString());
        }
    }

    public class EFEMRobotStatus
    {
        public bool IsMoving { get; set; }
        public bool IsAlarm { get; set; }
        public bool IsServoOn { get; set; }
        public bool IsAutoMode { get; set; }
        public bool IsProgramRunning { get; set; }
        public bool IsArmUnFold { get; set; }
        public bool IsLowerArmVacOn { get; set; }
        public bool IsUpperArmVacOn { get; set; }

        public bool CopyTo(ref EFEMRobotStatus dest)
        {
            dest.IsMoving         = this.IsMoving;
            dest.IsAlarm          = this.IsAlarm;
            dest.IsServoOn        = this.IsServoOn;
            dest.IsAutoMode       = this.IsAutoMode;
            dest.IsProgramRunning = this.IsProgramRunning;
            dest.IsArmUnFold      = this.IsArmUnFold;
            if (GG.EfemNoWafer == false)
            {
                dest.IsLowerArmVacOn = this.IsLowerArmVacOn;
                dest.IsUpperArmVacOn = this.IsUpperArmVacOn;
            }
            return true;
        }

        public void Set(string statusData)
        {
            if (statusData.Length != 8)
                throw new Exception("EFEM ROBOT DATA LENGTH ERROR");

            int i = 0;
            IsMoving         = statusData[i++] == '1' ? true : false;
            IsAlarm          = statusData[i++] == '1' ? true : false;
            IsServoOn        = statusData[i++] == '1' ? true : false;
            IsAutoMode       = statusData[i++] == '1' ? true : false;
            IsProgramRunning = statusData[i++] == '1' ? true : false;
            IsArmUnFold      = statusData[i++] == '1' ? true : false;
            if (GG.EfemNoWafer == false)
            {
                IsLowerArmVacOn = statusData[i++] == '1' ? true : false;
                IsUpperArmVacOn = statusData[i++] == '1' ? true : false;
            }
        }
    }

    public class EFEMLoadPortStatus
    {
        //Mapping Data
        public EmEfemMappingInfo[] MappingData { get; set; }

        //Event
        public bool IsFoupDetected { get; set; }
        public bool IsFoupRemoved { get; set; }
        public bool IsLoadButtonPushed { get; set; }
        public bool IsUnloadButtonPushed { get; set; }

        //by STAT~
        public bool IsError            { get; set; }
        public bool IsBusy             { get; set; }
        public bool IsAuto             { get; set; }
        public bool IsHomeComplete     { get; set; }
        public bool IsWaferStickOut    { get; set; }
        public bool IsMappingSensorUse { get; set; }
        public bool IsDoorOpen         { get; set; }
        public bool IsDoorClose        { get; set; }
        public bool IsFoupUse          { get; set; } // EFEM 내부 테스트용
        public bool IsFoupExist        { get; set; }
        public bool IsPinForward       { get; set; }
        public bool IsPinHome          { get; set; }
        public bool IsUnitForward      { get; set; } // 웨이퍼 정렬해주는거에 딸린거?
        public bool IsUnitHome         { get; set; }
        public bool IsAlignForward     { get; set; } // 웨이퍼 정렬해주는거
        public bool IsAlignHome        { get; set; }
        public bool IsMapArmForward    { get; set; }
        public bool IsMapArmHome       { get; set; }
        public bool IsAutoLampOn       { get; set; }
        public bool IsManualLampOn     { get; set; }
        public bool IsPlaceLampOn      { get; set; }
        public bool IsPresentLampOn    { get; set; }
        public bool IsLoadLampOn       { get; set; }
        public bool IsUnloadLampOn     { get; set; }
        public bool IsReserveLampOn    { get; set; }
        public bool IsAlarmLampOn      { get; set; }

        public bool MappingCopyTo(ref EFEMLoadPortStatus dest)
        {
            dest.MappingData = new EmEfemMappingInfo[this.MappingData.Length];
            for (int i = 0; i < dest.MappingData.Length; ++i)
                dest.MappingData[i] = this.MappingData[i];

            if (GG.TestMode)
            {
                #region test only simul
                //Presence Absence
                //최초 매핑 시 들어갈 웨이퍼
                dest.MappingData[12] = EmEfemMappingInfo.Presence;
                dest.MappingData[11] = EmEfemMappingInfo.Presence;
                dest.MappingData[10] = EmEfemMappingInfo.Presence; 
                dest.MappingData[09] = EmEfemMappingInfo.Presence; 
                dest.MappingData[08] = EmEfemMappingInfo.Presence;
                dest.MappingData[07] = EmEfemMappingInfo.Presence; 
                dest.MappingData[06] = EmEfemMappingInfo.Presence; 
                dest.MappingData[05] = EmEfemMappingInfo.Presence;
                dest.MappingData[04] = EmEfemMappingInfo.Presence; 
                dest.MappingData[03] = EmEfemMappingInfo.Presence;
                dest.MappingData[02] = EmEfemMappingInfo.Presence;
                dest.MappingData[01] = EmEfemMappingInfo.Presence;
                dest.MappingData[01] = EmEfemMappingInfo.Presence;
                dest.MappingData[00] = EmEfemMappingInfo.Presence;

                //중간에 재시작 시 빠진 웨이퍼 처리
                List<int> tempList = new List<int>();
                tempList.Add(GG.Equip.Efem.Robot.Status.IsLowerArmVacOn ? GG.Equip.Efem.Robot.LowerWaferKey.SlotNo - 1 : -1);
                tempList.Add(GG.Equip.Efem.Robot.Status.IsUpperArmVacOn ? GG.Equip.Efem.Robot.UpperWaferKey.SlotNo - 1 : -1);
                tempList.Add(GG.Equip.Efem.Aligner.Status.IsWaferExist ? GG.Equip.Efem.Aligner.LowerWaferKey.SlotNo - 1 : -1);
                tempList.Add(GG.Equip.IsWaferDetect == EmGlassDetect.ALL ? GG.Equip.TransferUnit.LowerWaferKey.SlotNo - 1 : -1);

                foreach (var item in tempList)
                {
                    if(item != -1)
                        dest.MappingData[item] = EmEfemMappingInfo.Absence;
                }

                #endregion test only
            }

            return true;
        }
        public bool CopyTo(ref EFEMLoadPortStatus dest)
        {
            dest.IsError            = this.IsError;
            dest.IsBusy             = this.IsBusy;
            dest.IsAuto             = this.IsAuto;
            dest.IsHomeComplete     = this.IsHomeComplete;
            dest.IsWaferStickOut    = this.IsWaferStickOut;
            dest.IsMappingSensorUse = this.IsMappingSensorUse;
            dest.IsDoorOpen         = this.IsDoorOpen;
            dest.IsDoorClose        = this.IsDoorClose;
            dest.IsFoupUse          = this.IsFoupUse;
            dest.IsFoupExist        = this.IsFoupExist;
            dest.IsPinForward       = this.IsPinForward;
            dest.IsPinHome          = this.IsPinHome;
            dest.IsUnitForward      = this.IsUnitForward;
            dest.IsUnitHome         = this.IsUnitHome;
            dest.IsAlignForward     = this.IsAlignForward;
            dest.IsAlignHome        = this.IsAlignHome;
            dest.IsMapArmForward    = this.IsMapArmForward;
            dest.IsMapArmHome       = this.IsMapArmHome;
            dest.IsAutoLampOn       = this.IsAutoLampOn;
            dest.IsManualLampOn     = this.IsManualLampOn;
            dest.IsPlaceLampOn      = this.IsPlaceLampOn;
            dest.IsPresentLampOn    = this.IsPresentLampOn;
            dest.IsLoadLampOn       = this.IsLoadLampOn;
            dest.IsUnloadLampOn     = this.IsUnloadLampOn;
            dest.IsReserveLampOn    = this.IsReserveLampOn;
            dest.IsAlarmLampOn      = this.IsAlarmLampOn;
            return true;
        }

        

        public void CopyEventDataTo(ref EFEMLoadPortStatus dest)
        {
            dest.IsFoupDetected = this.IsFoupDetected;
            dest.IsFoupRemoved = this.IsFoupRemoved;
            dest.IsLoadButtonPushed = this.IsLoadButtonPushed;
            dest.IsUnloadButtonPushed = this.IsUnloadButtonPushed;
            this.IsFoupDetected = false;
            this.IsFoupRemoved = false;
            this.IsLoadButtonPushed = false;
            this.IsUnloadButtonPushed = false;
        }

        public void Set(string statusData)
        {
            if (statusData.Length != 26)
                throw new Exception("EFEM ALIGNER DATA LENGTH ERROR");

            int i = 0;
            IsError                    = statusData[i++] == '1' ? true : false;
            IsBusy                     = statusData[i++] == '1' ? true : false;
            IsAuto                     = statusData[i++] == '1' ? true : false;
            IsHomeComplete             = statusData[i++] == '1' ? true : false;
            IsWaferStickOut            = statusData[i++] == '1' ? true : false;
            IsMappingSensorUse         = statusData[i++] == '1' ? true : false;
            IsDoorClose                = statusData[i++] == '1' ? true : false;
            IsDoorOpen                 = statusData[i++] == '1' ? true : false;
            IsFoupUse                  = statusData[i++] == '1' ? true : false;
            IsFoupExist                = statusData[i++] == '1' ? true : false;
            IsPinForward               = statusData[i++] == '1' ? true : false;
            IsPinHome                  = statusData[i++] == '1' ? true : false;            
            IsUnitForward              = statusData[i++] == '1' ? true : false;
            IsUnitHome                 = statusData[i++] == '1' ? true : false;
            IsAlignForward             = statusData[i++] == '1' ? true : false;
            IsAlignHome                = statusData[i++] == '1' ? true : false;
            IsMapArmForward            = statusData[i++] == '1' ? true : false;
            IsMapArmHome               = statusData[i++] == '1' ? true : false;            
            IsAutoLampOn               = statusData[i++] == '1' ? true : false;
            IsManualLampOn             = statusData[i++] == '1' ? true : false;
            IsPlaceLampOn              = statusData[i++] == '1' ? true : false;
            IsPresentLampOn            = statusData[i++] == '1' ? true : false;
            IsLoadLampOn               = statusData[i++] == '1' ? true : false;
            IsUnloadLampOn             = statusData[i++] == '1' ? true : false;
            IsReserveLampOn            = statusData[i++] == '1' ? true : false;
            IsAlarmLampOn              = statusData[i++] == '1' ? true : false;
        }

        //public void SetLPMSG(string data)
        //{
        //    List<string> msgs = new List<string>
        //    {
        //        "CASSETTE DETECTED",
        //        "CASSETTE REMOVED",
        //        "LOAD SWITCH PUSH",
        //        "UNLOAD SWITCH PUSH"
        //    };            
        //    switch(msgs.IndexOf(data))
        //    {
        //        case 0: IsFoupDetected = true; break;
        //        case 1: IsFoupRemoved = true; break;
        //        case 2:
        //            if(GG.Equip.Efem.LoadPort1.SeqStepNum == EmEFEMLPMSeqStep.S110_WAIT_LOAD_BUTTON_PUSH)
        //                IsLoadButtonPushed = true;
        //            break;
        //        case 3: IsUnloadButtonPushed = true; break;
        //    }
        //}
        public void SetLPMSG(string data, EmEfemPort target)
        {
            List<string> msgs = new List<string>
            {
                "CASSETTE DETECTED",
                "CASSETTE REMOVED",
                "LOAD SWITCH PUSH",
                "UNLOAD SWITCH PUSH",
                "SWITCH PUSH" // FOUP없을 때 LOAD BUTTON 누르면 발생함.
            };
            switch (msgs.IndexOf(data))
            {
                case 0: IsFoupExist = true; break;
                case 1: IsFoupRemoved = true; break;
                case 2: IsLoadButtonPushed = true; break;
                case 3: IsUnloadButtonPushed = true; break;
                case 4: IsLoadButtonPushed = true; break;
            }
        }

        public EFEMLoadPortStatus()
        {
            MappingData = new EmEfemMappingInfo[13];
        }

        public bool IsLampOn(EmEfemLampType type)
        {
            switch (type)
            {
                case EmEfemLampType.LOAD_LAMP: return IsLoadLampOn;
                case EmEfemLampType.UNLOAD_LAMP: return IsUnloadLampOn;
                case EmEfemLampType.RESERVE_LAMP: return IsReserveLampOn;
                default:
                    return false;
            }
        }
    }

    public class EFEMAlignerStatus
    {
        //Aligner Module
        public EmEfemAlignerMode Mode { get; set; }
        public EmEfemAlignerStatus IsStatus { get; set; }
        private bool _isWaferExist;
        public bool IsWaferExist
        {
            get
            {
                return _isWaferExist;
            }
            set
            {
                _isWaferExist = value;
            }
        }
        #region PreAligner Data
        public double WaferNotchSettingX             { get; set; } // PreAligner 레시피에 센터 기준
        public double WaferNotchSettingY             { get; set; } // PreAligner 레시피에 센터 기준
        public double WaferNotchSettingT             { get; set; } // PreAligner 레시피에 노치 기준
        public double OffsetX                        { get; set; } // Wafer notch Setting X,Y,T 를 0으로 기준하여
        public double OffsetY                        { get; set; } // Wafer notch Setting X,Y,T 를 0으로 기준하여
        public double OffsetT                        { get; set; } // Wafer notch Setting X,Y,T 를 0으로 기준하여
        public double WaferNotchPosX                 { get; set; } // 첫 확인 시 Wafer Notch 보정해야되는 값
        public double WaferNotchPosY                 { get; set; } // 첫 확인 시 Wafer Notch 보정해야되는 값
        public double WaferNotchPosT                 { get; set; } // 첫 확인 시 Wafer Notch 보정해야되는 값
        public double MovedX                          { get; set; } // Pre-Aligner에서 이동한 값
        public double MovedY                          { get; set; } // Pre-Aligner에서 이동한 값
        public double MovedT                          { get; set; } // Pre-Aligner에서 이동한 값

        public double EllipseX         { get; set; }
        public double EllipseY         { get; set; }
        public double MajorLength      { get; set; }
        public double MinorLength      { get; set; }
        public double EllipseT         { get; set; }
        public double EllipseMajorX1   { get; set; } //참고 : Major - 장변, Minor - 단변
        public double EllipseMajorY1   { get; set; }
        public double EllipseMajorX2   { get; set; }
        public double EllipseMajorY2   { get; set; }
        public double EllipseMinorX1   { get; set; }
        public double EllipseMinorY1   { get; set; }
        public double EllipseMinorX2   { get; set; }
        public double EllipseMinorY2   { get; set; }
        #endregion PreAligner Data

        public bool CopyTo(ref EFEMAlignerStatus dest)
        {
            dest.Mode = this.Mode;
            dest.IsStatus = this.IsStatus;
            if (GG.EfemNoWafer == false)
            {
                dest.IsWaferExist = this.IsWaferExist;
            }
            return true;
        }

        public void Set(string statusData)
        {
            if (statusData.Length != 3)
                throw new Exception("EFEM ALIGNER DATA LENGTH ERROR");

            int i = 0;
            Mode = (EmEfemAlignerMode)int.Parse(statusData[i++].ToString());
            IsStatus            = (EmEfemAlignerStatus)int.Parse(statusData[i++].ToString());
            if (GG.EfemNoWafer == false)
            {
                IsWaferExist = statusData[i++] == '1' ? true : false;
            }
        }
        public void ClearAlignData()
        {
            WaferNotchSettingX = 0;
            WaferNotchSettingY = 0;
            WaferNotchSettingT = 0;
            OffsetX = 0;
            OffsetY = 0;
            OffsetT = 0;
            WaferNotchPosX = 0;
            WaferNotchPosY = 0;
            WaferNotchPosT = 0;
            MovedX = 0;
            MovedY = 0;
            MovedT = 0;
            EllipseX = 0;
            EllipseY         = 0;
            MajorLength      = 0;
            MinorLength      = 0;
            EllipseT         = 0;
            EllipseMajorX1   = 0;
            EllipseMajorY1   = 0;
            EllipseMajorX2   = 0;
            EllipseMajorY2   = 0;
            EllipseMinorX1   = 0;
            EllipseMinorY1   = 0;
            EllipseMinorX2   = 0;
            EllipseMinorY2   = 0;
        }
        public void AlignDataCopyTo(ref EFEMAlignerStatus alignerStat)
        {
            alignerStat.WaferNotchSettingX = this.WaferNotchSettingX;
            alignerStat.WaferNotchSettingY = this.WaferNotchSettingY;
            alignerStat.WaferNotchSettingT = this.WaferNotchSettingT;
            alignerStat.OffsetX            = this.OffsetX;
            alignerStat.OffsetY            = this.OffsetY;
            alignerStat.OffsetT            = this.OffsetT;
            alignerStat.WaferNotchPosX     = this.WaferNotchPosX;
            alignerStat.WaferNotchPosY     = this.WaferNotchPosY;
            alignerStat.WaferNotchPosT     = this.WaferNotchPosT;
            alignerStat.MovedX             = this.MovedX;
            alignerStat.MovedY             = this.MovedY;
            alignerStat.MovedT             = this.MovedT;
            alignerStat.EllipseX           = this.EllipseX;
            alignerStat.EllipseY           = this.EllipseY;
            alignerStat.MajorLength        = this.MajorLength;
            alignerStat.MinorLength        = this.MinorLength;
            alignerStat.EllipseT           = this.EllipseT;
            alignerStat.EllipseMajorX1     = this.EllipseMajorX1;
            alignerStat.EllipseMajorY1     = this.EllipseMajorY1;
            alignerStat.EllipseMajorX2     = this.EllipseMajorX2;
            alignerStat.EllipseMajorY2     = this.EllipseMajorY2;
            alignerStat.EllipseMinorX1     = this.EllipseMinorX1;
            alignerStat.EllipseMinorY1     = this.EllipseMinorY1;
            alignerStat.EllipseMinorX2     = this.EllipseMinorX2;
            alignerStat.EllipseMinorY2     = this.EllipseMinorY2;
        }
        public void AlignDataCopyTo(ref WaferInfo wafer)
        {
            wafer.SettingX = this.WaferNotchSettingX;
            wafer.SettingY = this.WaferNotchSettingY;
            wafer.SettingT = this.WaferNotchSettingT;
            wafer.OffsetX = this.OffsetX;
            wafer.OffsetY = this.OffsetY;
            wafer.OffsetT = this.OffsetT;
            wafer.WaferNotchPosX = this.WaferNotchPosX;
            wafer.WaferNotchPosY = this.WaferNotchPosY;
            wafer.WaferNotchPosT = this.WaferNotchPosT;
            wafer.MovedX = this.MovedX;
            wafer.MovedY = this.MovedY;
            wafer.MovedT = this.MovedT;

            wafer.EllipseX = this.EllipseX;
            wafer.EllipseY = this.EllipseY;
            wafer.MajorLength = this.MajorLength;
            wafer.MinorLength = this.MinorLength;
            wafer.EllipseT = this.EllipseT;
            wafer.EllipseMajorX1 = this.EllipseMajorX1;
            wafer.EllipseMajorY1 = this.EllipseMajorY1;
            wafer.EllipseMajorX2 = this.EllipseMajorX2;
            wafer.EllipseMajorY2 = this.EllipseMajorY2;
            wafer.EllipseMinorX1 = this.EllipseMinorX1;
            wafer.EllipseMinorY1 = this.EllipseMinorY1;
            wafer.EllipseMinorX2 = this.EllipseMinorX2;
            wafer.EllipseMinorY2 = this.EllipseMinorY2;
        }
        public bool Parsing(string row)
        {
            try
            {
                ClearAlignData();

                string setting = row.Split('/')[0];
                string notchPos = row.Split('/')[1];
                string offset = row.Split('/')[2];
                string move = row.Split('/')[3];
                string ellipse = row.Split('/')[4];
                
                this.WaferNotchSettingX = (int)double.Parse(setting.Split(',')[0]); WaferNotchSettingX = double.Parse((WaferNotchSettingX * 1 / 1000f).ToString("F3"));
                this.WaferNotchSettingY = (int)double.Parse(setting.Split(',')[1]); WaferNotchSettingY = double.Parse((WaferNotchSettingY * 1 / 1000f).ToString("F3"));
                this.WaferNotchSettingT = double.Parse(setting.Split(',')[2]);
                this.WaferNotchPosX = (int)double.Parse(notchPos.Split(',')[0]); WaferNotchPosX = double.Parse((WaferNotchPosX * 1 / 1000f).ToString("F3"));
                this.WaferNotchPosY = (int)double.Parse(notchPos.Split(',')[1]); WaferNotchPosY = double.Parse((WaferNotchPosY * 1 / 1000f).ToString("F3"));
                this.WaferNotchPosT = double.Parse(notchPos.Split(',')[2]);
                this.OffsetX = (int)double.Parse(offset.Split(',')[0]); OffsetX = double.Parse((OffsetX * 1 / 1000f).ToString("F3"));
                this.OffsetY = (int)double.Parse(offset.Split(',')[1]); OffsetY = double.Parse((OffsetY * 1 / 1000f).ToString("F3"));
                this.OffsetT = double.Parse(offset.Split(',')[2]);
                this.MovedX = (int)double.Parse(move.Split(',')[0]); MovedX = double.Parse((MovedX * 1 / 1000f).ToString("F3"));
                this.MovedY = (int)double.Parse(move.Split(',')[1]); MovedY = double.Parse((MovedY * 1 / 1000f).ToString("F3"));
                this.MovedT = double.Parse(move.Split(',')[2]);

                int i = 0;
                this.EllipseX = (int)double.Parse(ellipse.Split(',')[i++]); EllipseX = double.Parse((EllipseX * 1 / 1000f).ToString("F3"));
                this.EllipseY = (int)double.Parse(ellipse.Split(',')[i++]); EllipseY = double.Parse((EllipseY * 1 / 1000f).ToString("F3"));
                this.EllipseT = double.Parse(ellipse.Split(',')[i++]);
                this.MajorLength = (int)double.Parse(ellipse.Split(',')[i++]); MajorLength = double.Parse((MajorLength * 1 / 1000f).ToString("F3"));
                this.MinorLength = (int)double.Parse(ellipse.Split(',')[i++]); MinorLength = double.Parse((MinorLength * 1 / 1000f).ToString("F3"));
                this.EllipseMajorX1 = (int)double.Parse(ellipse.Split(',')[i++]); EllipseMajorX1 = double.Parse((EllipseMajorX1 * 1 / 1000f).ToString("F3"));
                this.EllipseMajorY1 = (int)double.Parse(ellipse.Split(',')[i++]); EllipseMajorY1 = double.Parse((EllipseMajorY1 * 1 / 1000f).ToString("F3"));
                this.EllipseMajorX2 = (int)double.Parse(ellipse.Split(',')[i++]); EllipseMajorX2 = double.Parse((EllipseMajorX2 * 1 / 1000f).ToString("F3"));
                this.EllipseMajorY2 = (int)double.Parse(ellipse.Split(',')[i++]); EllipseMajorY2 = double.Parse((EllipseMajorY2 * 1 / 1000f).ToString("F3"));
                this.EllipseMinorX1 = (int)double.Parse(ellipse.Split(',')[i++]); EllipseMinorX1 = double.Parse((EllipseMinorX1 * 1 / 1000f).ToString("F3"));
                this.EllipseMinorY1 = (int)double.Parse(ellipse.Split(',')[i++]); EllipseMinorY1 = double.Parse((EllipseMinorY1 * 1 / 1000f).ToString("F3"));
                this.EllipseMinorX2 = (int)double.Parse(ellipse.Split(',')[i++]); EllipseMinorX2 = double.Parse((EllipseMinorX2 * 1 / 1000f).ToString("F3"));
                this.EllipseMinorY2 = (int)double.Parse(ellipse.Split(',')[i++]); EllipseMinorY2 = double.Parse((EllipseMinorY2 * 1 / 1000f).ToString("F3"));

                return true;
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog.AppendLine(LogLevel.NoLog, "OnCompleteAlign Exception ! : Recv Msg ({0})", row);
                return false;
            }
        }
        public void SetDitAlignResult(PreAligner.CWaferPreAlignerResult result)
        {
            if (PixelSize_um <= 0) PixelSize_um = 116.4; // jys:: 200918
            this.WaferNotchSettingX = result.m_dRefCenterX * MicroToMilimeter(PixelSize_um);
            this.WaferNotchSettingY = result.m_dRefCenterY * MicroToMilimeter(PixelSize_um);
            this.WaferNotchSettingT = result.m_dRefNotchDegree;
            this.OffsetX            = result.m_dDx * MicroToMilimeter(PixelSize_um);
            this.OffsetY            = -result.m_dDy * MicroToMilimeter(PixelSize_um);
            this.OffsetT            = result.m_dDTheta;
            this.WaferNotchPosX     = result.m_dNotchX * MicroToMilimeter(PixelSize_um);
            this.WaferNotchPosY     = result.m_dNotchY * MicroToMilimeter(PixelSize_um);
            this.WaferNotchPosT     = result.m_dNotchDegree;
            this.MovedX             = 0; //result.MovedX;
            this.MovedY             = 0; //result.MovedY;
            this.MovedT             = 0; //result.MovedT;
            this.EllipseX           = result.m_dCenterX * MicroToMilimeter(PixelSize_um);
            this.EllipseY           = result.m_dCenterY * MicroToMilimeter(PixelSize_um);
            this.MajorLength        = result.m_dMajorLength * MicroToMilimeter(PixelSize_um);
            this.MinorLength        = result.m_dMinorLength * MicroToMilimeter(PixelSize_um);
            this.EllipseT           = result.m_dMajorDegree;
            this.EllipseMajorX1     = result.m_dMajorStartX * MicroToMilimeter(PixelSize_um);
            this.EllipseMajorY1     = result.m_dMajorStartY * MicroToMilimeter(PixelSize_um);
            this.EllipseMajorX2     = result.m_dMajorEndX * MicroToMilimeter(PixelSize_um);
            this.EllipseMajorY2     = result.m_dMajorEndY * MicroToMilimeter(PixelSize_um);
            this.EllipseMinorX1     = result.m_dMinorStartX * MicroToMilimeter(PixelSize_um);
            this.EllipseMinorY1     = result.m_dMinorStartY * MicroToMilimeter(PixelSize_um);
            this.EllipseMinorX2     = result.m_dMinorEndX * MicroToMilimeter(PixelSize_um);
            this.EllipseMinorY2     = result.m_dMinorEndY * MicroToMilimeter(PixelSize_um);
        }

        public double PixelSize_um { get; private set; }

        public double MicroToMilimeter(double micrometer)
        {
            return micrometer / 1000;
        }
    }
}
