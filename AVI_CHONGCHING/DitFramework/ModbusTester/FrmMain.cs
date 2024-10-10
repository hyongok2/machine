using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dit.Framework.PLC;
using Dit.Framework.HWControl.DisplacementSensor;
using System.Net.Sockets;


namespace ModbusTester
{
    public partial class FrmMain : Form
    {

        private VirtualModbus vv = new VirtualModbus("", 10000, VirtualModbus.EmModbusType.RTU);

        private Equipment equip = new Equipment();

        public FrmMain()
        {
            InitializeComponent();
        }
        private void FrmMain_Load(object sender, EventArgs e)
        {
            vv.SyncBlocks.Add(new PlcAddr(PlcMemType.MD, 0, 0, 10));
            vv.SyncBlocks.Add(new PlcAddr(PlcMemType.MC, 0, 0, 10));
            vv.SyncBlocks.Add(new PlcAddr(PlcMemType.MI, 0, 0, 10));
            vv.SyncBlocks.Add(new PlcAddr(PlcMemType.MH, 0, 0, 10));

            equip.Sensor.FlowAddr = new PlcAddr(PlcMemType.MH, 0, 0, 2) { PLC = vv };
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
        }
        private void btnReadDiscreteInputs_Click(object sender, EventArgs e)
        {
            //vv.ReadDiscreteInputs();
        }
        private void btnReadWrite_Click(object sender, EventArgs e)
        {
            //vv.ReadFromPLC(new PlcAddr(PlcMemType.MD, 0, 0, 100), 100);
            //vv.ReadFromPLC(new PlcAddr(PlcMemType.MC, 0, 0, 100), 100);
            //vv.ReadFromPLC(new PlcAddr(PlcMemType.MI, 0, 0, 100), 100);
            //vv.ReadFromPLC(new PlcAddr(PlcMemType.MH, 0, 0, 100), 100);

            //bool di3 = vv.VirGetBit(new PlcAddr(PlcMemType.MD, 1, 0, 1));
            //vv.VirSetBit(new PlcAddr(PlcMemType.MC, 0, 1), false);

            //vv.WriteToPLC(new PlcAddr(PlcMemType.MC, 0, 0, 100), 100);
        }

        private void btnConnectTcp_Click(object sender, EventArgs e)
        {
            vv.Ip = "127.0.0.1";
            vv.Port = 500;
            vv.Open();

            tmWorker.Start();
        }
        private void btnConnectRTU_Click(object sender, EventArgs e)
        {
            vv.PortName = "COM2";
            vv.BaudRate = 9600;
            vv.DataBits = 8;
            vv.ParityBit = System.IO.Ports.Parity.Even;
            vv.StopBit = System.IO.Ports.StopBits.One;
            vv.Open();

            tmWorker.Start();
        }

        private void tmWorker_Tick(object sender, EventArgs e)
        {
            //equip.LogicWorking();


            textBox1.Text = equip.Sensor.Flow.ToString();

        }


        private ZW7000T DispSensor = null;
        private void btnZW7000TOpen_Click(object sender, EventArgs e)
        {
            if (DispSensor == null)
                DispSensor = new ZW7000T();
            DispSensor.Open("192.168.250.50", 9600);
        }

        private void btnZW7000TGetData_Click(object sender, EventArgs e)
        {
            DispSensor.StartGetData(Dit.Framework.HWControl.DisplacementSensor.ZW7000T.EmZW7000TCmd.GetTask1);

            while (true)
            {
                Application.DoEvents();
                if (DispSensor.IsReadComplete)
                {
                    txtZW7000TData.Text =(DispSensor.IsSuccess ? "OK" : "NG") + DispSensor.MeasurementValue.ToString();
                    return;
                }
            }
        }
    }
}
