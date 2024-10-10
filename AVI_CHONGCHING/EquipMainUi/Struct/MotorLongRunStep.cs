using EquipMainUi.Struct.Detail;
using EquipMainUi.Struct.Step;
using System;

namespace EquipMainUi.Struct
{
    public enum EmLONGRUN_NO
    {
        S000_LONG_RUN_WAIT,

        S010_LONG_RUN_START,
        S020_STAGE_X_ALIGN_POS_WAIT,
        S030_STAGE_X_LOADING_POS_WAIT,

        S040_STAGE_Y_ALIGN_POS_WAIT,
        S050_STAGE_Y_LOADING_POS_WAIT,

        S060_THETA_ALIGN_POS_WAIT,
        S070_THETA_LOADING_POS_WAIT,
    }
    public class MotorLongRunStep : StepBase
    {
        public EmLONGRUN_NO step = EmLONGRUN_NO.S000_LONG_RUN_WAIT;
        public int count = 0;
        DateTime startTime;
        DateTime endTime;

        PlcTimerEx LongRunDelay = new PlcTimerEx("", false);
        public void StepStart()
        {
            count = 0;

            Logger.Log.AppendLine(LogLevel.Info, "Motor Long Run Start {0}", startTime.ToShortTimeString());
            startTime = DateTime.Now;
            step = EmLONGRUN_NO.S010_LONG_RUN_START;
        }
        public void StepStop()
        {
            endTime = DateTime.Now;
            step = EmLONGRUN_NO.S000_LONG_RUN_WAIT;

            Logger.Log.AppendLine(LogLevel.Info, "Motor Long Run End {0}, 횟수 : {1}", endTime.ToShortTimeString(), count.ToString());
        }
        public override void LogicWorking(Equipment equip)
        {
            if (LongRunDelay == false && LongRunDelay.IsStart == false)
            {
                LongRunDelay.Start(0, 200);
            }
            else
            {
                switch (step)
                {
                    case EmLONGRUN_NO.S000_LONG_RUN_WAIT:
                        break;
                    case EmLONGRUN_NO.S010_LONG_RUN_START:
                        equip.StageX.MovePosition(equip, StageXServo.AlignPos);
                        step = EmLONGRUN_NO.S020_STAGE_X_ALIGN_POS_WAIT;
                        break;
                    case EmLONGRUN_NO.S020_STAGE_X_ALIGN_POS_WAIT:
                        if (equip.StageX.IsMoveOnPosition(StageXServo.AlignPos))
                        {
                            equip.StageX.MoveLoadingEx(equip, StageXServo.LoadingPos);
                            step = EmLONGRUN_NO.S030_STAGE_X_LOADING_POS_WAIT;
                        }
                        break;
                    case EmLONGRUN_NO.S030_STAGE_X_LOADING_POS_WAIT:
                        if (equip.StageX.IsMoveOnPosition(StageXServo.LoadingPos))
                        {
                            equip.StageY.MovePosition(equip, StageYServo.AlignPos);
                            step = EmLONGRUN_NO.S040_STAGE_Y_ALIGN_POS_WAIT;
                        }
                        break;
                    case EmLONGRUN_NO.S040_STAGE_Y_ALIGN_POS_WAIT:
                        if (equip.StageY.IsMoveOnPosition(StageYServo.AlignPos))
                        {
                            equip.StageY.MoveLoadingEx(equip, StageYServo.LoadingPos);
                            step = EmLONGRUN_NO.S050_STAGE_Y_LOADING_POS_WAIT;
                        }
                        break;
                    case EmLONGRUN_NO.S050_STAGE_Y_LOADING_POS_WAIT:
                        if (equip.StageY.IsMoveOnPosition(StageYServo.LoadingPos))
                        {
                            equip.Theta.MovePosition(equip, ThetaServo.AlignPos);
                            step = EmLONGRUN_NO.S060_THETA_ALIGN_POS_WAIT;
                        }
                        break;
                    case EmLONGRUN_NO.S060_THETA_ALIGN_POS_WAIT:
                        if (equip.Theta.IsMoveOnPosition(ThetaServo.AlignPos))
                        {
                            equip.Theta.MoveLoadingEx(equip, ThetaServo.LoadingPos);
                            step = EmLONGRUN_NO.S070_THETA_LOADING_POS_WAIT;
                        }
                        break;
                    case EmLONGRUN_NO.S070_THETA_LOADING_POS_WAIT:
                        if (equip.Theta.IsMoveOnPosition(ThetaServo.LoadingPos))
                        {
                            count++;
                            step = EmLONGRUN_NO.S010_LONG_RUN_START;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}