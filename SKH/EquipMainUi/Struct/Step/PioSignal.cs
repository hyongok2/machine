using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace EquipMainUi.Struct.Step
{
    public class PioSignalSend
    {
        private PioSignalRecv _lowerPioRecv { get; set; }
        [Browsable(false)]
        public string Name { get; set; }

        public bool YSendAble { get; set; }
        public bool YSendStart { get; set; }
        public bool YSendComplete { get; set; }

        public bool XRecvAble { get { return _lowerPioRecv.YRecvAble; } }
        public bool XRecvStart { get { return _lowerPioRecv.YRecvStart; } }
        public bool XRecvComplete { get { return _lowerPioRecv.YRecvComplete; } }
        [Browsable(false)]
        public bool IsRunning { get { return XRecvAble == true || YSendStart == true || YSendComplete == true; } }
        public PioSignalSend()
        {
            Name = string.Empty;
        }

        public void SetLower(PioSignalRecv lowerSender)
        {
            _lowerPioRecv = lowerSender;
        }

        public void Initailize()
        {
            YSendAble = false;
            YSendStart = false;
            YSendComplete = false;
        }
    }

    public class PioSignalRecv
    {
        private PioSignalSend _upperPioSend { get; set; }
        [Browsable(false)]
        public string Name { get; set; }

        public bool YRecvAble { get; set; }
        public bool YRecvStart { get; set; }
        public bool YRecvComplete { get; set; }


        public bool XSendAble { get { return _upperPioSend.YSendAble; } }
        public bool XSendStart { get { return _upperPioSend.YSendStart; } }
        public bool XSendComplete { get { return _upperPioSend.YSendComplete; } }
        [Browsable(false)]
        public bool IsRunning { get { return XSendAble == true || YRecvStart == true || YRecvComplete == true; } }

        public PioSignalRecv()
        {
            Name = string.Empty;
        }

        public void SetUpper(PioSignalSend upperSender)
        {
            _upperPioSend = upperSender;
        }

        public void Initailize()
        {
            YRecvAble = false;
            YRecvStart = false;
            YRecvComplete = false;
        }
    }
}
