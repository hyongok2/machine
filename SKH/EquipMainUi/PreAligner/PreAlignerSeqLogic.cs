using EquipMainUi.PreAligner.Recipe;
using EquipMainUi.Struct;
using EquipMainUi.Struct.Detail.EFEM;
using EquipMainUi.Struct.Detail.EziStep;
using EquipMainUi.Struct.Step;
using EquipMainUi.UserMessageBoxes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquipMainUi.PreAligner
{
    public enum EmPreAlignINIT
    {
        H000_WAIT,
        H005_INIT_RETRY_WAIT,
        H010_CHECK_CAMERA_READY,
        H020_INIT_START,
        H030_CHECK_VACUUM_ON,
        H040_CHECK_OCR_UP,
        H050_STEP_MOTOR_MOVE_HOME_WAIT,
        H060_STEP_MOTOR_MOVE_LD_POSI_WAIT,
        H070_CHECK_VACUUM_OFF,

        H100_INIT_COMPLETE,
    }
    public enum EmPreAlignAlign
    {
        S000_WAIT,
        S010_CHECK_BEFORE_ALIGN,
        S020_ALIGN_START,
        S030_ALIGN_COMPLETE_WAIT,
        S040_VACUUM_OFF_WAIT,

        S050_ALIGN_COMPLETE,
    }

    public class PreAlignerSeqLogic : StepBase
    {
        private FrmManualClose _frmInit;
        private EmPreAlignINIT homeStep;
        public bool IsInitComplete{ get; private set; }

        private EmPreAlignAlign alignStep;
        public bool IsAlignComplete { get; private set; }
        public PlcTimerEx HomeDelay = new PlcTimerEx("Step Motor Home Delay", false);
        public PlcTimerEx InitDelay = new PlcTimerEx("Cam Init Delay", false);

        private Timer _popupTimer = new Timer();

        public PreAlignerSeqLogic()
        {
            Name = "PreAligner";            
                   
        }
        public void InitUserInterface(Equipment equip)
        {
            _frmInit = new FrmManualClose();

            _popupTimer.Tick += _popupTimer_Tick;
            _popupTimer.Interval = 1000;
            _popupTimer.Start();
        }

        private bool _initTimerPopup = false;
        private void _popupTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_initTimerPopup)
                {
                    _initTimerPopup = false;
                    _frmInit.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;                    
                    _frmInit.Show();
                }
            }
            catch (Exception ex)
            {
                if (AlarmMgr.Instance.IsHappened(GG.Equip, EM_AL_LST.AL_0946_UI_EXCEPTION) == false)
                {
                    Logger.ExceptionLog.AppendLine(LogLevel.Error, string.Format("UI 갱신 예외 발생 : {0}", ex.Message));
                    Logger.ExceptionLog.AppendLine(LogLevel.Error, Log.EquipStatusDump.CallStackLog());
                    AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0946_UI_EXCEPTION);
                }
            }
        }

        public override void LogicWorking(Equipment equip)
        {
            SeqLogging(equip);
            InitLogic(equip);
            AlignLogic(equip);
        }
        private int _initRetryCount = 1;
        private DateTime _initStartTime = DateTime.Now;
        private void InitLogic(Equipment equip)
        {
            switch (homeStep)
            {
                case EmPreAlignINIT.H000_WAIT:
                    break;
                case EmPreAlignINIT.H005_INIT_RETRY_WAIT:
                    if (InitDelay)
                    {
                        _initRetryCount++;
                        InitDelay.Stop();
                        homeStep = EmPreAlignINIT.H010_CHECK_CAMERA_READY;
                    }
                    break;
                case EmPreAlignINIT.H010_CHECK_CAMERA_READY:
                    bool cam = false, light = false, retry = false;
                    IsInitComplete = false;
                    try
                    {
                        if (equip.PreAligner.IsCamReady == false)
                            cam = equip.PreAligner.OpenCamera();
                        else
                            cam = true;
                        if (equip.PreAligner.IsLightReady == false)
                            light = equip.PreAligner.OpenLightController(GG.Equip.InitSetting.LightControllerPort);
                        else
                            light = true;
                        
                        if (cam == false || light == false)
                            retry = true;
                    }
                    catch (Exception ex)
                    {                        
                        Logger.Log.AppendLine(LogLevel.NoLog, "PreAligner 카메라 초기화 중 : {0}, 조명 연결 중 : {1} ({2})", cam ? "성공" : "실패", light ? "성공" : "실패", ex.Message);
                    }

                    if (retry && GG.TestMode == false)
                    {
                        _frmInit.SetStartTime(_initStartTime);
                        _frmInit.SetText("초기화중", string.Format("PreAligner\n(재시도 횟수 : {2})\n\n카메라 초기화 중 : {0}\n조명 연결 중 : {1}\n(약 5분 소요)",
                            cam ? "성공" : "실패", light ? "성공" : "실패", _initRetryCount));
                        _initTimerPopup = true;
                        InitDelay.Start(0, 20000);
                        homeStep = EmPreAlignINIT.H005_INIT_RETRY_WAIT;
                    }
                    else
                    {
                        _frmInit.Close();
                        homeStep = EmPreAlignINIT.H020_INIT_START;
                    }
                    break;
                case EmPreAlignINIT.H020_INIT_START:


                    if (equip.AlignerWaferDetect.IsOn)
                    {
                        equip.AlignerVac.VacuumOn();
                        equip.AlignerOcrCylinder.Up();

                        homeStep = EmPreAlignINIT.H030_CHECK_VACUUM_ON;
                    }
                    else
                    {
                        equip.AlignerOcrCylinder.Up();
                        homeStep = EmPreAlignINIT.H040_CHECK_OCR_UP;
                    }
                    break;
                case EmPreAlignINIT.H030_CHECK_VACUUM_ON:
                    if (equip.AlignerVac.IsVacuumOn)
                    {
                        homeStep = EmPreAlignINIT.H040_CHECK_OCR_UP;
                    }
                    break;
                case EmPreAlignINIT.H040_CHECK_OCR_UP:
                    if (equip.AlignerOcrCylinder.IsUp)
                    {
                        equip.AlignerX.MoveHome();
                        equip.AlignerY.MoveHome();
                        equip.AlignerT.MoveHome();

                        HomeDelay.Start(0, 3000);

                        homeStep = EmPreAlignINIT.H050_STEP_MOTOR_MOVE_HOME_WAIT;
                    }
                    break;
                case EmPreAlignINIT.H050_STEP_MOTOR_MOVE_HOME_WAIT:
                    if (equip.AlignerX.IsHomeComplete && equip.AlignerY.IsHomeComplete && equip.AlignerT.IsHomeComplete
                        && equip.AlignerX.IsMovingStep == false && equip.AlignerY.IsMovingStep == false && equip.AlignerT.IsMovingStep == false 
                        && HomeDelay)
                    {
                        HomeDelay.Stop();
                        equip.AlignerX.MovePosition(AlignerXEzi.LoadingPos);
                        equip.AlignerY.MovePosition(AlignerYEzi.LoadingPos);
                        equip.AlignerT.MovePosition(AlignerThetaEzi.LoadingPos);

                        homeStep = EmPreAlignINIT.H060_STEP_MOTOR_MOVE_LD_POSI_WAIT;
                    }
                    break;
                case EmPreAlignINIT.H060_STEP_MOTOR_MOVE_LD_POSI_WAIT:
                    if (equip.AlignerX.IsMoveOnPosition(AlignerXEzi.LoadingPos) &&
                        equip.AlignerY.IsMoveOnPosition(AlignerYEzi.LoadingPos) &&
                        equip.AlignerT.IsMoveOnPosition(AlignerThetaEzi.LoadingPos))
                    {
                        if (equip.AlignerWaferDetect.IsOn || equip.AlignerVac.Vacuum.IsSolOnOff == true)
                        {
                            equip.AlignerVac.StartOffStep();
                        }
                        homeStep = EmPreAlignINIT.H070_CHECK_VACUUM_OFF;
                    }
                    break;
                case EmPreAlignINIT.H070_CHECK_VACUUM_OFF:
                    if (equip.AlignerVac.IsVacuumOff)
                    {
                        homeStep = EmPreAlignINIT.H100_INIT_COMPLETE;
                    }
                    break;
                case EmPreAlignINIT.H100_INIT_COMPLETE:
                    IsInitComplete = true;
                    homeStep = EmPreAlignINIT.H000_WAIT;
                    break;
            }
        }
        private void AlignLogic(Equipment equip)
        {
            switch (alignStep)
            {
                case EmPreAlignAlign.S000_WAIT:
                    break;
                case EmPreAlignAlign.S010_CHECK_BEFORE_ALIGN:
                    IsAlignComplete = false;

                    EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorModule = EmEfemErrorModule.E3;
                    EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorCode = 0;

                    if(GG.TestMode)
                    {
                        equip.PreAligner.ImageProcessingUpdate();
                        alignStep = EmPreAlignAlign.S050_ALIGN_COMPLETE;
                        break;
                    }

                    if (equip.PreAligner.IsCamReady == false)
                        AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0913_ALIGNER_CAMERA_NOT_CONNECTED); //EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorCode = 83;
                    if (equip.PreAligner.IsLightReady == false)
                        AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0911_ALIGNER_LIGHT_NOT_CONNECTED); //EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorCode = 81;

                    if (EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorCode > 0)
                    {
                        alignStep = EmPreAlignAlign.S050_ALIGN_COMPLETE;
                        break;
                    }
                    alignStep = EmPreAlignAlign.S020_ALIGN_START;
                    break;
                case EmPreAlignAlign.S020_ALIGN_START:
                    EFEMAlignDataSet data = cmd.Tag as EFEMAlignDataSet;
                    PreAlignerRecipe recp = null;
                    
                    recp = PreAlignerRecipeDataMgr.GetRecipe(data.WaferID);

                    if (recp == null || recp.Name == string.Empty)
                    {
                        Logger.Log.AppendLine(LogLevel.Info, "등록된 레시피가 없어 디폴트로 진행");
                        recp = PreAlignerRecipeDataMgr.GetRecipe(equip.FixedDitAlignerRecipeName);
                    }

                    string waferID = data.WaferID;
                    if (GG.Equip.Efem.Aligner.IsRunMode)
                    {
                        try
                        {
                            var wafer = Struct.TransferData.TransferDataMgr.GetWafer(GG.Equip.Efem.Aligner.LowerWaferKey);
                            waferID = wafer.WaferID;
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    equip.PreAligner.SetCurRecipe(recp);
                    equip.PreAligner.Start(waferID);

                    alignStep = EmPreAlignAlign.S030_ALIGN_COMPLETE_WAIT;
                    break;
                case EmPreAlignAlign.S030_ALIGN_COMPLETE_WAIT:
                    EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorModule = EmEfemErrorModule.E3;
                    EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].IsSuccess = false;

                    if (EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorCode > 0)
                    {
                        Logger.Log.AppendLine(LogLevel.Error, "PreAligner 알람발생 : {0}",
                            EFEMError.GetErrorDesc(EmEfemErrorModule.E3, EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorCode));
                        alignStep = EmPreAlignAlign.S050_ALIGN_COMPLETE;
                        break;
                    }
                    else
                    {
                        if (equip.PreAligner.IsProcessingDone)
                        {
                            if (equip.PreAligner.IsProcessingOvertime)
                            {
                                if (equip.PreAligner.IsLightControllDone == false)
                                    AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0914_ALIGNER_LIGHT_CONNECTION_ERROR); //EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorCode = 84;
                                else
                                    AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0913_ALIGNER_CAMERA_NOT_CONNECTED); //EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorCode = 83;
                                Logger.Log.AppendLine(LogLevel.Error, "PreAligner 알람발생 : {0}",
                                    EFEMError.GetErrorDesc(EmEfemErrorModule.E3, EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorCode));
                                alignStep = EmPreAlignAlign.S050_ALIGN_COMPLETE;
                                break;
                            }
                            else
                            {
                                if (equip.PreAligner.ProcessingResult == WaferPreAlignerResultCode.WaferPreAlignerResult_Success)
                                {
                                    equip.Efem.Aligner.Status.SetDitAlignResult(equip.PreAligner.AlignParamResult);
                                    EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].IsSuccess = true;
                                    Logger.Log.AppendLine(LogLevel.Info, "PreAligner 처리 성공 (미가공Data), {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}",
                                        "DX", "DY", "DTheta", "CenterX", "CenterY", "MajorLength", "MinorLength", "NotchX", "NotchY", "NotchDegree");
                                    Logger.Log.AppendLine(LogLevel.Info, "PreAligner 처리 성공 (미가공Data), {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}",
                                        equip.PreAligner.AlignParamResult.DX,
                                        equip.PreAligner.AlignParamResult.DY,
                                        equip.PreAligner.AlignParamResult.DTheta,
                                        equip.PreAligner.AlignParamResult.CenterX,
                                        equip.PreAligner.AlignParamResult.CenterY,
                                        equip.PreAligner.AlignParamResult.MajorLength,
                                        equip.PreAligner.AlignParamResult.MinorLength,
                                        equip.PreAligner.AlignParamResult.NotchX,
                                        equip.PreAligner.AlignParamResult.NotchY,
                                        equip.PreAligner.AlignParamResult.NotchDegree
                                        );
                                    Logger.Log.AppendLine(LogLevel.Info, "PreAligner 처리 성공 (가공Data), {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}",
                                        "OffsetX", "OffsetY", "OffsetT", "EllipseX", "EllipseY", "MajorLength", "MinorLength",
                                        "WaferNotchPosX", "WaferNotchPosY", "WaferNotchPosT");
                                    Logger.Log.AppendLine(LogLevel.Info, "PreAligner 처리 성공 (가공Data), {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}",
                                        equip.Efem.Aligner.Status.OffsetX,
                                        equip.Efem.Aligner.Status.OffsetY,
                                        equip.Efem.Aligner.Status.OffsetT,
                                        equip.Efem.Aligner.Status.EllipseX,
                                        equip.Efem.Aligner.Status.EllipseY,
                                        equip.Efem.Aligner.Status.MajorLength,
                                        equip.Efem.Aligner.Status.MinorLength,
                                        equip.Efem.Aligner.Status.WaferNotchPosX,
                                        equip.Efem.Aligner.Status.WaferNotchPosY,
                                        equip.Efem.Aligner.Status.WaferNotchPosT
                                        );
                                }
                                else
                                {
                                    AlarmMgr.Instance.Happen(GG.Equip, EM_AL_LST.AL_0904_PRE_ALIGNER_ALIGN_FAIL);
                                    //EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorCode = 208 + (int)equip.PreAligner.ProcessingResult;
                                    Logger.Log.AppendLine(LogLevel.Error, "PreAligner 처리 실패 : {0}",
                                        EFEMError.GetErrorDesc(EmEfemErrorModule.E3, EFEMTcp.HS[EmEfemPort.ALIGNER][EmEfemCommand.ALIGN].ErrorCode));
                                }

                                if (equip.PreAligner.CurRecipe.SaveResult)
                                {
                                    equip.PreAligner.SaveResultImage(
                                        equip.PreAligner.ProcessingResult == WaferPreAlignerResultCode.WaferPreAlignerResult_Success,
                                        equip.PreAligner.WaferID);
                                }

                                equip.AlignerVac.StartOffStep();
                                alignStep = EmPreAlignAlign.S040_VACUUM_OFF_WAIT;
                                break;
                            }
                        }
                        break;
                    }
                case EmPreAlignAlign.S040_VACUUM_OFF_WAIT:
                    if(equip.AlignerVac.IsVacuumOff)
                    {
                        alignStep = EmPreAlignAlign.S050_ALIGN_COMPLETE;
                    }
                    break;
                case EmPreAlignAlign.S050_ALIGN_COMPLETE:
                    IsAlignComplete = true;
                    break;
            }
        }
        private EmPreAlignINIT _oldHomeStep;
        private EmPreAlignAlign _oldAlignStep;
        private void SeqLogging(Equipment equip)
        {
            SeqStepStr = alignStep.ToString();
            HomeStepStr = homeStep.ToString();

            if (alignStep != _oldAlignStep)
                _oldAlignStep = alignStep;

            if (homeStep != _oldHomeStep)
                _oldHomeStep = homeStep;

            base.LogicWorking(equip);
        }
        private bool IsSafeToInit()
        {
            if(homeStep != EmPreAlignINIT.H000_WAIT)
            {
                InterLockMgr.AddInterLock("인터락<Pre Aligner 홈 동작>\n프리얼라이너 홈 동작이 이미 진행 중>");
                return false;
            }
            if(GG.IsDitPreAligner == false)
            {
                InterLockMgr.AddInterLock("인터락<Pre Aligner 홈 동작>\n프리얼라이너를 직접 제어하지 않은 설비에서는 진행할 수 없습니다>");
                return false;
            }
            return true;
        }

        public void InitStart(Equipment equip)
        {
            if(IsSafeToInit())
            {
                _initRetryCount = 1;
                _initStartTime = DateTime.Now;
                Logger.Log.AppendLine(LogLevel.Error, "프리 얼라이너 홈 동작 시작");
                homeStep = EmPreAlignINIT.H010_CHECK_CAMERA_READY;
            }
        }
        private EFEMPcCmd cmd;
        public void AlignStart(Equipment equip, EFEMPcCmd _cmd)
        {
            //인터락 필요하면?? 넣기
            cmd = _cmd;
            alignStep = EmPreAlignAlign.S010_CHECK_BEFORE_ALIGN;
        }
        private void InitStop()
        {
            Logger.Log.AppendLine(LogLevel.Error, "프리 얼라이너 Init 동작 중 정지");
            homeStep = EmPreAlignINIT.H000_WAIT;
        }
        private void AlignStop()
        {
            Logger.Log.AppendLine(LogLevel.Error, "프리 얼라이너 Align 동작 중 정지");
            alignStep = EmPreAlignAlign.S000_WAIT;
        }
    }
}
