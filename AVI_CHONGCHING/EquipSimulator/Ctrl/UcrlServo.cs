using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EquipSimulator.Acturator;

namespace EquipSimulator.Ctrl
{
    public partial class UcrlServo : UserControl
    {
        public ServoSimulUmac Servo; 
        
        public UcrlServo()
        {
            InitializeComponent();
        }

        public new void Update()
        {
            lblIXPosition.Text = Servo.XF_CurrMotorPosition.vFloat.ToString();
            chkInHome.BackColor = Servo.YB_HomeCmd ? Color.Red : Color.Gray;
            chkOutHomeEnd.BackColor = Servo.XB_StatusHomeInPosition ? Color.Red : Color.Gray;
            chkOutMoveGo.BackColor = Servo.XB_StatusMotorMoving ? Color.Red : Color.Gray;

            CheckBox[] checks = new CheckBox[]
            {
                chkP1,chkP2,chkP3,
                chkP4,chkP5,chkP6,
                chkP7,chkP8,chkP9,
                chkP10,chkP11,chkP12            
            };
            CheckBox[] checkEnds = new CheckBox[]
            {
                chkEndP1,chkEndP2,chkEndP3,
                chkEndP4,chkEndP5,chkEndP6,
                chkEndP7,chkEndP8,chkEndP9,
                chkEndP10,chkEndP11,chkEndP12
            };

            int checkBoxCountMax = 1;
            for (int iter = 0; iter < checkBoxCountMax; ++iter)
            {
                checks[iter].BackColor = Servo.YB_TargetMoveCmd[iter].vBit ? Color.Red : Color.Gray;
                checkEnds[iter].BackColor = Servo.XB_TargetMoveComplete[iter].vBit ? Color.Red : Color.Gray;
            }            

            Label[] labels = new Label[]
            {
                lblInspX1,lblInspX2,lblInspX3,
                lblInspX4,lblInspX5,lblInspX6,
                lblInspX7
            };
          
            for (int iter = 0; iter < 1; ++iter )
                labels[iter].Text = Servo.YF_TargetPosition[iter].vFloat.ToString();
        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }

        public string Tiltel
        {
            set
            { lblTitle.Text = value; }
        }

        private void chkP7_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
