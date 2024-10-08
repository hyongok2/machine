using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EquipMainUi.PreAligner
{
    public class LightControllerProxy
    {
        LightController_EsmartK_704PPA_F Controller;
        public bool IsInitDone { get; private set; }

        public void Initialize(string port)
        {
            Controller = new LightController_EsmartK_704PPA_F(port);
            IsInitDone = true;
        }

        private DateTime _lastLogic;
        private int _scanTime = 100;

        private DateTime _errCheckLogic;
        private int _errScanTime = 1000;
        public void LogicWorking()
        {
            if (Controller == null) return;
            if (Controller.IsOpen() == false) return;

            if ((DateTime.Now - _lastLogic).TotalMilliseconds > _scanTime)
            {
                OnWorking();
                OffWorking();
                RemoteWorking();
                GetErrWorking();
                _lastLogic = DateTime.Now;
            }

            if ((DateTime.Now - _lastLogic).TotalMilliseconds > _errScanTime)
            {
                GetErrMsg();
            }
        }

        public void On(int bright)
        {
            IsOn = false;
            Bright = bright;
            _onStep = 10;
        }
        public void Off()
        {
            IsOn = true;
            _offStep = 10;
        }

        internal bool Reopen(string port)
        {
            return Controller.ReOpen(port);
        }

        public void Remote()
        {
            IsRemote = false;
            _remoteStep = 10;
        }
        public void GetErrMsg()
        {
            _getErrStep = 10;
        }
        public string ErrorMsg { get; private set; }
        public bool IsOn { get; private set; }
        public bool IsRemote { get; private set; }
        public bool IsOpen => Controller != null && Controller.IsOpen();
        public string Port => Controller.Port;

        internal void Release()
        {
            StopGetErrorStep();
            StopOffStep();
            StopOnStep();
            StopRemoteStep();
            Controller.Stop();
        }

        public void StopStep()
        {
            StopGetErrorStep();
            StopOffStep();
            StopOnStep();
            StopRemoteStep();
        }

        private void StopRemoteStep()
        {
            _remoteStep = 0;
        }

        private void StopOnStep()
        {
            _onStep = 0;
        }

        private void StopOffStep()
        {
            _offStep = 0;
        }
        public int Bright { get; private set; }
        private int _onStep = 0;
        private int _retry = 0;
        private void OnWorking()
        {
            switch (_onStep)
            {
                case 0:
                    break;
                case 10:
                    _retry = 0;
                    Controller.SetBright(0, Bright);
                    Bright = Controller.SetBright(1, Bright);
                    _onStep = 20;
                    break;
                case 20:
                    _retry++;
                    Controller.GetBright(0);
                    _onStep = 30;
                    break;
                case 30:
                    if (Controller.IsCmdReturned(EmCommand.GetBright) == true)
                    {
                        if (Controller.IsCmdSuccess(EmCommand.GetBright)
                            && Controller.Ch1.Bright == Bright)
                        {
                            _retry = 0;
                            _onStep = 40;
                        }
                        else
                        {
                            if (_retry < 3)
                                _onStep = 20;
                            else
                                _onStep = 10;
                        }
                    }
                    else
                    {
                        if (_retry < 3)
                            _onStep = 20;
                        else
                            _onStep = 10;
                    }
                    break;
                case 40:
                    _retry++;
                    Controller.GetBright(1);
                    _onStep = 50;
                    break;
                case 50:
                    if (Controller.IsCmdReturned(EmCommand.GetBright) == true)
                    {
                        if (Controller.IsCmdSuccess(EmCommand.GetBright)
                        && Controller.Ch2.Bright == Bright)
                        {
                            _retry = 0;
                            _onStep = 60;
                        }
                        else
                        {
                            if (_retry < 3)
                                _onStep = 40;
                            else
                                _onStep = 10;
                        }
                    }
                    else
                    {
                        if (_retry < 3)
                            _onStep = 40;
                        else
                            _onStep = 10;
                    }
                    break;
                case 60:
                    _retry++;
                    Controller.On();
                    _onStep = 70;
                    break;
                case 70:
                    Controller.GetOnOff();
                    _onStep = 80;
                    break;
                case 80:
                    if (Controller.IsCmdReturned(EmCommand.GetOnOff) == true)
                    {
                        if (Controller.IsCmdSuccess(EmCommand.GetOnOff)
                            && Controller.Ch1.IsOn == true
                            && Controller.Ch2.IsOn == true)
                        {
                            IsOn = true;
                            _onStep = 100;
                        }
                        else
                        {
                            if (_retry < 3)
                                _onStep = 60;
                            else
                                _onStep = 10;
                        }
                    }
                    else
                    {
                        if (_retry < 3)
                            _onStep = 60;
                        else
                            _onStep = 10;
                    }
                    break;
                case 100:
                    Console.WriteLine("ON DONE");
                    _onStep = 0;
                    break;
            }
        }

        private void StopGetErrorStep()
        {
            _getErrStep = 0;
        }

        private int _offStep = 0;
        private void OffWorking()
        {
            switch (_offStep)
            {
                case 0:
                    break;
                case 10:                    
                    _retry = 0;
                    Controller.Off();
                    _offStep = 20;
                    break;
                case 20:
                    _retry++;
                    Controller.GetOnOff();
                    _offStep = 30;
                    break;
                case 30:
                    if (Controller.IsCmdReturned(EmCommand.GetOnOff) == true)
                    {
                        if (Controller.IsCmdSuccess(EmCommand.GetOnOff)
                            && Controller.Ch1.IsOn == false
                            && Controller.Ch2.IsOn == false)
                        {
                            IsOn = false;
                            _offStep = 100;
                        }
                        else
                        {
                            if (_retry < 3)
                                _offStep = 20;
                            else
                                _offStep = 10;
                        }
                    }
                    else
                    {
                        if (_retry < 3)
                            _offStep = 20;
                        else
                            _offStep = 10;
                    }
                    break;
                case 100:
                    Console.WriteLine("OFF DONE");
                    _offStep = 0;
                    break;
            }
        }        

        private int _remoteStep = 0;
        private void RemoteWorking()
        {
            switch (_remoteStep)
            {
                case 0:
                    break;
                case 10:
                    _retry = 0;
                    Controller.SetRemoteMode();
                    _remoteStep = 20;
                    break;
                case 20:
                    _retry++;
                    Controller.GetRemoteMode();
                    _remoteStep = 30;
                    break;
                case 30:
                    if (Controller.IsCmdReturned(EmCommand.GetRemoteMode) == true)
                    {
                        if (Controller.IsCmdSuccess(EmCommand.GetRemoteMode)
                            && Controller.IsRemote == true)
                        {
                            IsRemote = true;
                            _remoteStep = 100;
                        }
                        else
                        {
                            if (_retry < 3)
                                _remoteStep = 20;
                            else
                                _remoteStep = 10;
                        }
                    }
                    else
                    {
                        if (_retry < 3)
                            _remoteStep = 20;
                        else
                            _remoteStep = 10;
                    }
                    break;
                case 100:
                    Console.WriteLine("REMOTE DONE");
                    _remoteStep = 0;
                    break;
            }
        }

        private int _getErrStep = 0;
        private bool _getErrorRunning => _getErrStep != 0;
        private void GetErrWorking()
        {
            switch (_getErrStep)
            {
                case 0:
                    break;
                case 10:
                    _retry = 0;
                    _getErrStep = 20;
                    break;
                case 20:
                    _retry++;
                    Controller.GetErrStatus();
                    _getErrStep = 30;
                    break;
                case 30:
                    if (Controller.IsCmdReturned(EmCommand.GetErrStatus) == true)
                    {
                        if (Controller.IsCmdSuccess(EmCommand.GetErrStatus))
                        {
                            ErrorMsg = Controller.ErrorMsg;
                            _getErrStep = 100;
                        }
                        else
                        {
                            if (_retry < 3)
                                _getErrStep = 20;
                            else
                                _getErrStep = 10;
                        }
                    }
                    else
                    {
                        if (_retry < 3)
                            _getErrStep = 20;
                        else
                            _getErrStep = 10;
                    }
                    break;
                case 100:
                    Console.WriteLine("GET ERROR DONE");
                    _getErrStep = 0;
                    break;
            }
        }
    }
}
