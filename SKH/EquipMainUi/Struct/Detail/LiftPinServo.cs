using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace EquipMainUi.Struct.Detail
{
    public class LiftPinServo : ServoMotorUmac
    {
        public static string PATH_SETTING = Path.Combine(Application.StartupPath, "Setting", "LiftPinServo.ini");
        public static int LiftPinUpHiSpeed { get; set; }
        public static int LiftPinMiddleLowSpeed { get; set; }
        public static int LiftPinDownHiSpeed { get; set; }
        public static int LiftPinHomeLowSpeed { get; set; }
        public static int LiftPinMiddleHiSpeed { get; set; }
        public static int LiftPinDownLowSpeed { get; set; }
        public static int LiftPinUpLowSpeed { get; set; }

        public LiftPinServo(string name) :
            base(name)
        {
            SoftMinusLimit = 0;
            SoftPlusLimit = 100;
            SoftSpeedLimit = 50;
            SoftJogSpeedLimit = 5;

            LiftPinHomeLowSpeed = 0;
            LiftPinDownHiSpeed = 1;
            LiftPinMiddleLowSpeed = 2;
            LiftPinUpHiSpeed = 3;

            LiftPinMiddleHiSpeed = 4;
            LiftPinDownLowSpeed = 5;
            LiftPinUpLowSpeed = 6;

            base.MoveActionName[0] = "원점 위치(모든→원점 저속)";  //저속
            base.MoveActionName[1] = "하강 위치(원점→하강 고속)";  //고속
            base.MoveActionName[2] = "중간 위치(하강→중간 저속)";  //저속
            base.MoveActionName[3] = "상승 위치(중간→상승 고속)";  //고속

            base.MoveActionName[4] = "중간 위치(상승→중간 고속)";  //고속
            base.MoveActionName[5] = "하강 위치(중간→하강 저속)";  //저속            
            base.MoveActionName[6] = "상승 위치(중간→상승 저속)";  //저속

        }
        protected override bool CheckStartMoveInterLock(Equipment equip, MoveCommand cmd)
        {

            if (equip.IsUseInterLockOff) return false;
            if (equip.IsHomePositioning == false)
            {
                if (equip.InspX.IsMoveOnPosition(InspXServo.InspXLoadingPosiNo) == false &&
                    (int)cmd != LiftPinHomeLowSpeed &&
                    (int)cmd != LiftPinDownLowSpeed && (int)cmd != LiftPinDownHiSpeed &&
                    cmd != MoveCommand.HOME_POSI)
                {
                    InterLockMgr.AddInterLock("INSP X위치가 로딩 위치가 아님, LIFT PIN 이동 금지");
                    Logger.Log.AppendLine(LogLevel.Warning, "INSP X위치가 로딩 위치가 아님, LIFT PIN 이동 금지");
                    equip.IsInterLock = true;
                    return true;

                }
                if (equip.InspY.IsMoveOnPosition(InspYServo.InspYLoadingPosiNo) == false &&
                    (int)cmd != LiftPinHomeLowSpeed &&
                    (int)cmd != LiftPinDownLowSpeed && (int)cmd != LiftPinDownHiSpeed &&
                    cmd != MoveCommand.HOME_POSI)
                {
                    InterLockMgr.AddInterLock("INSP Y위치가 로딩 위치가 아님, LIFT PIN 이동 금지");
                    Logger.Log.AppendLine(LogLevel.Warning, "INSP Y위치가 로딩 위치가 아님, LIFT PIN 이동 금지");
                    equip.IsInterLock = true;
                    return true;
                }

                if (equip.ReviY.IsMoveOnPosition(ReviYServo.RevYLoadingPosiNo) == false &&
                    (int)cmd != LiftPinHomeLowSpeed &&
                    (int)cmd != LiftPinDownLowSpeed && (int)cmd != LiftPinDownHiSpeed &&
                    cmd != MoveCommand.HOME_POSI )
                {
                    InterLockMgr.AddInterLock("REV Y위치가 로딩 위치가 아님, LIFT PIN 이동 금지");
                    Logger.Log.AppendLine(LogLevel.Warning, "REV Y위치가 로딩 위치가 아님, LIFT PIN 이동 금지");
                    equip.IsInterLock = true;
                    return true;
                }

                if (equip.ReviY_Under.IsMoveOnPosition(ReviY2Servo.RevY2LoadingPosiNo) == false &&
                   (int)cmd != LiftPinHomeLowSpeed &&
                   (int)cmd != LiftPinDownLowSpeed && (int)cmd != LiftPinDownHiSpeed &&
                   cmd != MoveCommand.HOME_POSI)
                {
                    InterLockMgr.AddInterLock("REV 하부 Y위치가 로딩 위치가 아님, LIFT PIN 이동 금지");
                    Logger.Log.AppendLine(LogLevel.Warning, "REV Y위치가 로딩 위치가 아님, LIFT PIN 이동 금지");
                    equip.IsInterLock = true;
                    return true;
                }
                if (equip.Centering.IsCenteringBackward(equip) == false)
                {
                    InterLockMgr.AddInterLock("센터링 전전 중, LIFT PIN 이동 금지");
                    Logger.Log.AppendLine(LogLevel.Warning, "센터링 전전 중, LIFT PIN 이동 금지");
                    equip.IsInterLock = true;
                    return true;
                }

                if (equip.IsNoGlassMode == false)
                {
                    if (equip.Vacuum.IsVacuumOn == true && (equip.IsGlassCheckOk == EmGlassSense.ALL || equip.IsGlassCheckOk == EmGlassSense.SOME) 
                        //&&
                        //(int)cmd != LiftPinHomeLowSpeed &&
                        //(int)cmd != LiftPinDownLowSpeed && (int)cmd != LiftPinDownHiSpeed &&
                        //cmd != MoveCommand.HOME_POSI
                        )
                    {
                        InterLockMgr.AddInterLock("VACCUM ON 상태에서 LIFT PIN 이동 금지");
                        Logger.Log.AppendLine(LogLevel.Warning, "VACCUM ON 상태에서 LIFT PIN 이동 금지");
                        equip.IsInterLock = true;
                        return true;
                    }
                }
            }

            return false;
        }
        protected override bool CheckMovingInterLock(Equipment equip, MoveCommand cmd, ref int stepMove)
        {
            return false;
        }

        //테스트 코드~ 지워야 할 것!
        /*  private MoveCommand _crrentPosi = MoveCommand.HOME_POSI;
          private DateTime _moveStartTime = DateTime.Now;
          private bool _moveComplet = false;
          private bool _moveing = false;
        
          public override bool MovePosition(Equipment equip, int posiNo)
          {
              if (CheckUMacPositionSetting(equip, (MoveCommand)posiNo, MoveActionName[posiNo]) == false) return false;
              if (IsStartedStep(equip, (MoveCommand)posiNo, MoveActionName[posiNo]) == true) return false;

              if (CheckStartMoveInterLock(equip, (MoveCommand)posiNo))
              {
                  //IsInterLockError = true;
                  return false;
              }
              else
              {
                  _crrentPosi = (MoveCommand)posiNo;
                  _moveStartTime = DateTime.Now;
                  _moveComplet = false;
                  _moveing = true;
                  return true;
              }


          }
          public override bool GoHomeOrPositionOne(Equipment equip)
          {
              if (true)
              {
                  return MovePosition(equip, 0);
              }
              else
              {
                  return GoHome(equip);
              }
          }
          public override bool GoHome(Equipment equip)
          {
              if (CheckUMacPositionSetting(equip, MoveCommand.HOME_POSI, "HOME") == false) return false;
              if (IsStartedStep(equip, MoveCommand.HOME_POSI, "HOME") == true) return false;

              if (CheckStartMoveInterLock(equip, MoveCommand.HOME_POSI))
              {
                  //IsInterLockError = true;
                  return false;
              }
              else
              {

                  _crrentPosi = MoveCommand.HOME_POSI;

                  _moveStartTime = DateTime.Now;
                  _moveComplet = false;
                  return true;
              }
          }
          public override bool IsMoving
          {
              get { return _moveing; }
          }

          public override bool IsMoveOnPosition(int posiNo)
          {
              return (_crrentPosi == (MoveCommand)posiNo) && _moveComplet;
          }

          public override void LogicWorking(Equipment equip)
          {
              if ((DateTime.Now - _moveStartTime).TotalSeconds > 5 && _moveComplet == false)
              {
                  _moveing = false;
                  _moveComplet = true;
              }

              base.SettingWorking(equip);
          }*/

    }
}
