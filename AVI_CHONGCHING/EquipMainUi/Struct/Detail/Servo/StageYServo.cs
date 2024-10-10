using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace EquipMainUi.Struct.Detail
{
    public class StageYServo : ServoMotorPmac
    {
        public static string PATH_SETTING = Path.Combine(GG.StartupPath, "Setting", "InspYServo.ini");

        #region Teaching Point
        //변경 시 ECID관련 필수 변경할 것
        //Equipment.WriteEcidFile(), Equipment.ReadHostEcid()

        public static int LoadingPos = 1;
        public static int AlignPos = 2;
        public static int InspReadyPos = 3;
        public static int ReviewReadyPos = 4;
        #endregion

        public double VariableLoadingPos = -1;
        public static double AllowUnldRange = 2.5;

        public StageYServo(int innerAxisNo, int outerAxisNo, string name, int positionCount) :
            base(innerAxisNo, outerAxisNo, name, EmMotorType.Linear, positionCount)
        {
            SoftMinusLimit = -8;
            SoftPlusLimit = 600;
            SoftSpeedLimit = 300;
            SoftJogSpeedLimit = 200;
            EnableGripJogSpeedLimit = 30;            
        }
        public override bool CheckStartMoveInterLock(Equipment equip, MoveCommand cmd)
        {
            return base.CheckStartMoveInterLock(equip, cmd);
        }
        public override bool CheckMovingInterLock(Equipment equip, MoveCommand cmd, ref int stepMove)
        {
            return false;
        }
        /// <summary>
        /// 로딩위치 기준으로 offset이동
        /// </summary>
        /// <param name="equip"></param>
        /// <param name="posiNo"></param>
        /// <param name="posOffset">로딩위치 OFFSET</param>
        /// <returns></returns>
        public bool MoveLoadingEx(Equipment equip, float posOffset = 0)
        {
            double tryPos = this.Setting.LstServoPosiInfo[StageYServo.LoadingPos].Position + posOffset;
            if (IsCanLoadingPos(tryPos) == false)
            {
                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0516_STAGE_Y_ABNORMAL_LOADING_POS);
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "因Loading Position，所以是不可使用位置" : "로딩위치로 사용불가능한 위치 입니다", GG.boChinaLanguage ? string.Format("试图 位置:{0}, 允许范围(Ld,Ld+90,Ld+180,Ld+270)+-{1}", tryPos, StageYServo.AllowUnldRange) : string.Format("시도위치:{0}, 허용범위(Ld,Ld+90,Ld+180,Ld+270)+-{1}", tryPos, StageYServo.AllowUnldRange));
                equip.IsInterlock = true;
                return false;
            }

            VariableLoadingPos = tryPos;
            return MovePosition(equip,
                VariableLoadingPos,
                this.Setting.LstServoPosiInfo[StageYServo.LoadingPos].Speed,
                this.Setting.LstServoPosiInfo[StageYServo.LoadingPos].Accel,
                StageYServo.LoadingPos
                );
        }
        private bool IsCanLoadingPos(double tryPos)
        {
            double ldPos = this.Setting.LstServoPosiInfo[StageYServo.LoadingPos].Position;

            return Math.Abs(ldPos - tryPos) < StageYServo.AllowUnldRange;
        }
        public override bool MovePosition(Equipment equip, double pos, double spd, double acc, int posiNo = -1)
        {
            if (posiNo == StageYServo.LoadingPos
                && pos != VariableLoadingPos)
                throw new Exception("StageY 로딩위치이동 시 MoveLoadingEx함수만 사용");

            return base.MovePosition(equip, pos, spd, acc, posiNo);
        }
        public override bool IsMoveOnPosition(int posiNo)
        {
            if (posiNo == StageYServo.LoadingPos)
                return base.IsMoveOnPosition(VariableLoadingPos);
            else
                return base.IsMoveOnPosition(posiNo);
        }
    }
}
