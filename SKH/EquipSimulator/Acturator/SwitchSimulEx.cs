using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dit.Framework.PLC;
using System.Windows.Forms;
using System.Drawing;
using EquipSimulator.Log;
using Dit.Framework.Log;

namespace EquipSimulator.Acturator
{
    /// <summary>
    /// 접점 기능 추가
    /// date : 170724
    /// </summary>
    public class SwitchSimulEx
    {
        private int _step = 0;

        public string Name { get; set; }
        public DataGridView Acturator { get; set; }
        public int IdxInDgv { get; set; }
        public bool IsAContactSol { get; set; }
        public bool IsAContactSensor { get; set; }
        public PlcAddr YB_OnOff { get; set; }
        public PlcAddr XB_OnOff { get; set; }
        public bool IsSolOnOff { get { return !(IsAContactSol ^ YB_OnOff.vBit); } }
        public bool IsOnOff { get { return !(IsAContactSensor ^ XB_OnOff.vBit); } }
        public SwitchSimulEx()
        {
            IsAContactSol = true;
            IsAContactSensor = true;
        }
        public void LogicWorking()
        {
            if (_step == 0)
            {
                bool b1 = IsSolOnOff;
                bool b2 = XB_OnOff.vBit;
                if (IsSolOnOff != XB_OnOff.vBit)
                    _step = 10;                
                Acturator.Rows[IdxInDgv].DefaultCellStyle.BackColor = XB_OnOff.vBit ? Color.Red : Color.Gray;
            }
            else if (_step == 10)
            {
                //Acturator.Checked = YB_OnOff;
                //if (Acturator.Checked)
                //    Acturator.BackColor = Color.Red;
                //else
                //    Acturator.BackColor = Color.Gray;

                Logger.Log.AppendLine(LogLevel.Info, "{0} {1} {2}", Name, "센서", YB_OnOff ? "ON" : "OFF");

                XB_OnOff.vBit = IsAContactSensor ? IsSolOnOff : !IsSolOnOff;
                _step = 20;
            }
            else if (_step == 20)
            {
                _step = 0;
            }
        }
        public void Initialize()
        {
            Acturator.CellContentClick += delegate(object sender, DataGridViewCellEventArgs e)
            {
                if(e.RowIndex == IdxInDgv)
                    OnOff(!XB_OnOff.vBit);
            };
        }

        public void OnOff(bool value)
        {
            //Acturator.BackColor = Acturator.Checked ? Color.Red : Color.Gray;
            XB_OnOff.vBit = !(IsAContactSol ^ value); ;
            //Acturator.Checked = value;
        }
    }
}
