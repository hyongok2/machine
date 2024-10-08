using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.Ini;
using System.IO;
using System.Windows.Forms;
using Dit.Framework.PLC;

namespace EquipMainUi.Struct.Detail
{
    public class ThetaServo : ServoMotorPmac
    {
        public static string PATH_SETTING = Path.Combine(GG.StartupPath, "Setting", "ThetaServo.ini");

        #region Teaching Point
        //변경 시 ECID관련 필수 변경할 것
        //Equipment.WriteEcidFile(), Equipment.ReadHostEcid()

        public static int LoadingPos = 1;
        public static int AlignPos = 2;
        #endregion

        public double VariableLoadingPos = -1;
        public static double UnldOffset = 180;
        public static double AllowUnldRange = 3;

        public ThetaServo(int innerAxisNo, int outerAxisNo, string name, int positionCount) :
            base(innerAxisNo, outerAxisNo, name, EmMotorType.DD, positionCount)
        {
            InposOffset = 0.02f; // jys:: 모터 분해능 0.001 //kkt:: 21-02-02 변경 기존 0.008 -> 0.02(모터에서 0.02로 사용하고 있음)
            SoftMinusLimit = -121;
            SoftPlusLimit = 216;
            SoftSpeedLimit = 90;
            SoftJogSpeedLimit = 30;
            EnableGripJogSpeedLimit = 1;
            SoftAccelPlusLimit = 1000;
            SoftAccelMinusLimit = 200;
        }
        public override bool CheckStartMoveInterLock(Equipment equip, MoveCommand cmd)
        {
            return base.CheckStartMoveInterLock(equip, cmd);
        }

        public override bool CheckMovingInterLock(Equipment equip, MoveCommand cmd, ref int stepMove)
        {
            return base.CheckMovingInterLock(equip, cmd, ref stepMove);
        }

        public virtual bool GoHomeOrPositionOne(Equipment equip)
        {
            if (this.IsHomeComplete() == true && GG.TestMode == false)
            {
                return MovePosition(equip, 0, 5, 300);
            }
            else
            {
                return GoHome(equip);
            }
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
            double tryPos = this.Setting.LstServoPosiInfo[ThetaServo.LoadingPos].Position + posOffset;
            if (IsCanLoadingPos(posOffset) == false)
            {
                AlarmMgr.Instance.Happen(equip, EM_AL_LST.AL_0531_THETA_ABNORMAL_LOADING_POS);
                InterLockMgr.AddInterLock(GG.boChinaLanguage ? "因Loading Position，所以是不可使用位置" : "로딩위치로 사용불가능한 위치 입니다", GG.boChinaLanguage ? string.Format("基准 Loading 位置:{0}, Offset:{1}, 允许范围(Ld,Ld+90,Ld+180,Ld+270)+-{1}", tryPos, posOffset, ThetaServo.AllowUnldRange) : string.Format("기준로딩위치:{0}, Offset:{1}, 허용범위(Ld,Ld+90,Ld+180,Ld+270)+-{1}", tryPos, posOffset, ThetaServo.AllowUnldRange));
                equip.IsInterlock = true;
                return false;
            }

            VariableLoadingPos = tryPos;
            return MovePosition(equip,
                VariableLoadingPos,
                this.Setting.LstServoPosiInfo[ThetaServo.LoadingPos].Speed,
                this.Setting.LstServoPosiInfo[ThetaServo.LoadingPos].Accel,
                ThetaServo.LoadingPos
                );
        }
        /// <summary>
        /// 이동불가영역 고려하여 이동, 로딩위치로 지정함
        /// </summary>
        /// <param name="equip"></param>
        /// <param name="ldOffset"></param>
        /// <returns></returns>
        public bool MoveCalLoadingPos(Equipment equip, double ldOffset)
        {
            double ldPos = this.Setting.LstServoPosiInfo[ThetaServo.LoadingPos].Position;            
            if (ldPos + ldOffset > SoftPlusLimit)
                ldOffset += -360;
            else if (ldPos + ldOffset < SoftMinusLimit)
                ldOffset += 360;

            return MoveLoadingEx(equip, (float)ldOffset);
        }
        public bool IsCanLoadingPos(double targetPos)
        {
            return Math.Abs(0 - targetPos) < ThetaServo.AllowUnldRange
                || Math.Abs(90 - targetPos) < ThetaServo.AllowUnldRange
                || Math.Abs(180 - targetPos) < ThetaServo.AllowUnldRange
                || Math.Abs(270 - targetPos) < ThetaServo.AllowUnldRange
                || Math.Abs(-90 - targetPos) < ThetaServo.AllowUnldRange
                || Math.Abs(-180 - targetPos) < ThetaServo.AllowUnldRange
                || Math.Abs(-270 - targetPos) < ThetaServo.AllowUnldRange
                ;
        }
        public override bool MovePosition(Equipment equip, double pos, double spd, double acc, int posiNo = -1)
        {
            if (posiNo == ThetaServo.LoadingPos
                && pos != VariableLoadingPos)
                throw new Exception("Theta 로딩위치이동 시 MoveLoadingEx함수만 사용");

            return base.MovePosition(equip, pos, spd, acc, posiNo);
        }
        public override bool IsMoveOnPosition(int posiNo)
        {
            if (posiNo == ThetaServo.LoadingPos)
                return base.IsMoveOnPosition(VariableLoadingPos);
            else
                return base.IsMoveOnPosition(posiNo);
        }
        /// <summary>
        /// 로딩위치 + pos 하여 이동할 수 있는 위치인지 확인
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool IsCantMoveLdOffsetPos(double pos)
        {
            double m = Setting.LstServoPosiInfo[ThetaServo.LoadingPos].Position + pos * /*-*/1;
            return (SoftMinusLimit > m && m > -360 + SoftPlusLimit)
                || (SoftPlusLimit < m && m < 360 + SoftMinusLimit)
                ;
        }
    }
}