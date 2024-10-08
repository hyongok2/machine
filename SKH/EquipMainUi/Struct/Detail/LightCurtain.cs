using Dit.Framework.PLC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMainUi.Struct.Detail
{
    public class LightCurtain : UnitBase
    {
        public Switch Muting1 = new Switch();
        public Switch Muting2 = new Switch();
        public Switch ResetOut = new Switch(); // 리셋
        public Sensor Detect = new Sensor(false); // B접, 감지시 OFF, 뮤팅 시 ON
        private Sensor _mutingOn = new Sensor(); // On 시 Flicker.
        private bool _isMutingOn;

        public bool IsMuting { get { return GG.TestMode == true ? true : _isMutingOn; } }

        public void SetAddress(PlcAddr mutingOn)
        {
            _mutingOn.XB_OnOff = mutingOn;
        }

        private bool _oldDetect;
        public override void LogicWorking(Equipment equip)
        {
            ResetLogic(equip);
            MutingLogic(equip);
            MutingOnCheckLogic();

            if (Detect.IsOn != _oldDetect)
            {
                Logger.Log.AppendLine(LogLevel.NoLog, "LIGHT CURTAIN {0}!", Detect.IsOn ? "감지" : "감지해제");
            }
            _oldDetect = Detect.IsOn;
        }

        private int _mutingOnCheckStep = 0;
        private Stopwatch _mutingOnTimer = new Stopwatch();
        private bool _mutingOnOld;
        private void MutingOnCheckLogic()
        {
            if (_mutingOnOld != _mutingOn.IsOn)
                _mutingOnTimer.Restart();

            if (_mutingOnTimer.ElapsedMilliseconds > 700 || _mutingOnTimer.IsRunning == false)
            {
                _isMutingOn = false;
                _mutingOnTimer.Stop();
            }
            else
                _isMutingOn = true;

            _mutingOnOld = _mutingOn.IsOn;
        }

        public void Reset()
        {
            _rstStep = 10;
        }
        /// <summary>
        /// 이하의 어느 한조건으로 뮤팅 에러는 해제됩니다.
        /// 1. 올바른 뮤팅 초기 조건*1이 0.1s 이상 계속되었을 때
        /// 2. 뮤팅입력 1, 2가 OFF인 상태에서 전원을 재투입했을 때
        /// 
        /// *1 올바른 뮤팅 초기 조건을 이하에나타냅니다.
        /// ･라이트 커텐의 제어 출력이 ON인 상태 - 보조출력은 반대임
        /// ･뮤팅 입력 1, 2가 OFF인 상태
        /// </summary>
        public void StopMuting()
        {
            _mutingSignal = false;            
            _muteStep = 10;
        }
        /// <summary>
        /// 뮤팅 기능을 사용하려면
        /// 뮤팅 입력 1, 2에 시간차를 설정하고 ON 시킴으로써 뮤팅 상태가 됩니다.
        /// 리스크 어세스먼트의 결과에 의해서 뮤팅 램프가 필요하게 되는 경우 보조 출력선에 뮤팅램프를 접속
        /// 해 주십시오.
        /// 
        /// <시작 조건>
        /// 다음 2가지 조건을 모두 만족하는 경우에 뮤팅 상태로 됩니다.
        /// 1. F3SJ-B 의 검출 영역에 차광 물체가 없고 제어 출력이 ON으로 되어 있다.
        /// 2. 뮤팅 입력 1을 ON(Vs-3V~Vs에 접속)으로 한 후, 뮤팅 입력 시간 제한치 T1min ~T1max(0.1~3s)
        /// 의 범위내에서 뮤팅 입력 2를 ON(Vs-3V~Vs에 접속)으로 한다.
        /// 2.의 조건이 성립한 후, 최대 0.15s 후에 뮤팅 상태로 됩니다. 1.의 조건은 만족하지만 2.의 시간 조건
        /// 을 만족하지 않은 경우, 뮤팅 에러로 되어 투광기측의 뮤팅 에러 표시등이 점등합니다. 단, 뮤팅 에러
        /// 상태에서도 F3SJ-B의 안전 기능은 작동하여 통상 작동을 계속합니다
        /// </summary>
        public void StartMuting()
        {
            _mutingSignal = true;
            _muteStep = 10;
        }
        private bool _mutingSignal;
        private int _muteStep = 0;
        private PlcTimerEx _onDelay = new PlcTimerEx("Muting On Delay", false);
        public void MutingLogic(Equipment equip)
        {
            switch(_muteStep)
            {
                case 0:
                    break;
                case 10:
                    ResetOut.OnOff(equip, false);
                    _muteStep = 15;
                    break;
                case 15:
                    Muting1.OnOff(equip, _mutingSignal);
                    _onDelay.Start(0, _mutingSignal ? 150 : 10);
                    _muteStep = 20;
                    break;
                case 20:
                    if (_onDelay)
                    {
                        _onDelay.Stop();
                        Muting2.OnOff(equip, _mutingSignal);
                        _muteStep = 30;
                    }
                    break;
                case 30:
                    _muteStep = 0;
                    break;
            }
        }

        /*
         * 전원 투입시나 차광시에 제어 출력을 OFF로 하고, 리셋 입력이 인가될 때까지 이 상태를
         * 유지합니다. 이 상태를 인터록이라고 합니다.
         * 인터록의 리셋 방법에는 오토 리셋 (차광물체가 없어진 시점에서 자동적으로 제어 출력이 ON) 과 매
         * 뉴얼 리셋 (차광물체가 없어져도 리셋 신호가 입력될 때까지 제어 출력을 OFF로 유지) 의 2종류가 있
         * 습니다.
         */
        private int _rstStep = 0;
        private PlcTimerEx _rstDelay = new PlcTimerEx("Muting Reset Delay", false);
        private void ResetLogic(Equipment equip)
        {
            switch(_rstStep)
            {
                case 0:
                    break;
                case 10:
                    ResetOut.OnOff(equip, true);
                    _rstDelay.Start(0, 500);
                    _rstStep = 20;
                    break;
                case 20:
                    if (_rstDelay)
                    {
                        _rstDelay.Stop();
                        ResetOut.OnOff(equip, false);
                        _rstStep = 0;
                    }
                    break;
            }
        }
    }
}
