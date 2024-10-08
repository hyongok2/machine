using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dit.Framework.Comm;
using Dit.Framework.PLC;
using System.IO;
using Dit.Framework.Log;

namespace EquipMainUi.Monitor
{
    public partial class UcrlSignalView : UserControl
    {
        public List<UcrlSignalItem> LstSignalItems = new List<UcrlSignalItem>();
        public List<SignalLog> LstSignalLog = new List<SignalLog>();

        private int _index = 0;

        public UcrlSignalView()
        {
            InitializeComponent();
            DeleteCopyFile();
            #region SIGNAL_ITEM
            LstSignalItems.Add(ucrlSignalItem1);
            LstSignalItems.Add(ucrlSignalItem2);
            LstSignalItems.Add(ucrlSignalItem3);
            LstSignalItems.Add(ucrlSignalItem4);
            LstSignalItems.Add(ucrlSignalItem5);
            LstSignalItems.Add(ucrlSignalItem6);
            LstSignalItems.Add(ucrlSignalItem7);
            LstSignalItems.Add(ucrlSignalItem8);
            LstSignalItems.Add(ucrlSignalItem9);
            LstSignalItems.Add(ucrlSignalItem10);
            LstSignalItems.Add(ucrlSignalItem11);
            LstSignalItems.Add(ucrlSignalItem12);
            LstSignalItems.Add(ucrlSignalItem13);
            LstSignalItems.Add(ucrlSignalItem14);
            LstSignalItems.Add(ucrlSignalItem15);
            LstSignalItems.Add(ucrlSignalItem16);
            LstSignalItems.Add(ucrlSignalItem17);
            LstSignalItems.Add(ucrlSignalItem18);
            LstSignalItems.Add(ucrlSignalItem19);
            LstSignalItems.Add(ucrlSignalItem20);
            LstSignalItems.Add(ucrlSignalItem21);
            LstSignalItems.Add(ucrlSignalItem22);
            LstSignalItems.Add(ucrlSignalItem23);
            LstSignalItems.Add(ucrlSignalItem24);
            LstSignalItems.Add(ucrlSignalItem25);
            LstSignalItems.Add(ucrlSignalItem26);
            LstSignalItems.Add(ucrlSignalItem27);
            LstSignalItems.Add(ucrlSignalItem28);
            LstSignalItems.Add(ucrlSignalItem29);
            LstSignalItems.Add(ucrlSignalItem30);
            LstSignalItems.Add(ucrlSignalItem31);
            LstSignalItems.Add(ucrlSignalItem32);
            LstSignalItems.Add(ucrlSignalItem33);
            LstSignalItems.Add(ucrlSignalItem34);
            LstSignalItems.Add(ucrlSignalItem35);
            LstSignalItems.Add(ucrlSignalItem36);
            LstSignalItems.Add(ucrlSignalItem37);
            LstSignalItems.Add(ucrlSignalItem38);
            LstSignalItems.Add(ucrlSignalItem39);
            LstSignalItems.Add(ucrlSignalItem40);
            LstSignalItems.Add(ucrlSignalItem41);
            LstSignalItems.Add(ucrlSignalItem42);
            LstSignalItems.Add(ucrlSignalItem43);
            LstSignalItems.Add(ucrlSignalItem44);
            LstSignalItems.Add(ucrlSignalItem45);
            LstSignalItems.Add(ucrlSignalItem46);
            LstSignalItems.Add(ucrlSignalItem47);
            LstSignalItems.Add(ucrlSignalItem48);
            LstSignalItems.Add(ucrlSignalItem49);
            LstSignalItems.Add(ucrlSignalItem50);
            LstSignalItems.Add(ucrlSignalItem51);
            LstSignalItems.Add(ucrlSignalItem52);
            LstSignalItems.Add(ucrlSignalItem53);
            LstSignalItems.Add(ucrlSignalItem54);
            LstSignalItems.Add(ucrlSignalItem55);
            LstSignalItems.Add(ucrlSignalItem56);
            LstSignalItems.Add(ucrlSignalItem57);
            LstSignalItems.Add(ucrlSignalItem58);
            LstSignalItems.Add(ucrlSignalItem59);
            LstSignalItems.Add(ucrlSignalItem60);
            LstSignalItems.Add(ucrlSignalItem61);
            LstSignalItems.Add(ucrlSignalItem62);
            LstSignalItems.Add(ucrlSignalItem63);
            LstSignalItems.Add(ucrlSignalItem64);
            LstSignalItems.Add(ucrlSignalItem65);
            LstSignalItems.Add(ucrlSignalItem66);
            LstSignalItems.Add(ucrlSignalItem67);
            LstSignalItems.Add(ucrlSignalItem68);
            LstSignalItems.Add(ucrlSignalItem69);
            LstSignalItems.Add(ucrlSignalItem70);
            LstSignalItems.Add(ucrlSignalItem71);
            LstSignalItems.Add(ucrlSignalItem72);
            LstSignalItems.Add(ucrlSignalItem73);
            LstSignalItems.Add(ucrlSignalItem74);
            LstSignalItems.Add(ucrlSignalItem75);
            LstSignalItems.Add(ucrlSignalItem76);
            LstSignalItems.Add(ucrlSignalItem77);
            LstSignalItems.Add(ucrlSignalItem78);
            LstSignalItems.Add(ucrlSignalItem79);
            LstSignalItems.Add(ucrlSignalItem80);
            #endregion
        }
        public void UpdateUI()
        {
            if (LstSignalLog.Count == 0)
            {
                btnBackward.Enabled = btnFastBackward.Enabled = btnHighFastBackward.Enabled = false;
                btnForward.Enabled = btnFastForward.Enabled = btnHighFastForward.Enabled = false;
            }
            else
            {
                for (int iPos = 0; iPos < 80; iPos++)
                {
                    LstSignalItems[iPos].UpdateUi(LstSignalLog[_index].IsOnOff[80 - iPos - 1]);
                }
                lblDate.Text = LstSignalLog[_index].Date;
                btnBackward.Enabled = btnFastBackward.Enabled = btnHighFastBackward.Enabled = _index != 0;
                btnForward.Enabled = btnFastForward.Enabled = btnHighFastForward.Enabled = _index != (LstSignalLog.Count - 1);
            }
        }
        public void SetText(int index, string addr, string desc)
        {
            if (index < 0 || index >= 80) return;
            LstSignalItems[index].SetText(addr, desc);
        }
        public void SetTitle(string leftTitle, string rightTitle)
        {
            lblLeftTitle.Text = leftTitle;
            lblRightTitle.Text = rightTitle;
        }
        private void btnFileLoad_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Title = "Open Signal Log File";
                openFile.InitialDirectory = @"D:/DitCtrl/Log/Signal/";
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    CopyFile(openFile.FileName, Path.GetFileName(openFile.FileName));

                    string path = Path.Combine(Application.StartupPath, "Tmp", Path.GetFileName(openFile.FileName));

                    string log = File.ReadAllText(path);
                    string[] lines = log.Split((char)10, (char)13);

                    foreach(string str in lines)
                    {
                        SignalLog signal = null;
                        if (SignalLog.TryParse(str, out signal))
                        {
                            LstSignalLog.Add(signal);
                        }
                    }

                    UpdateUI();
                }
            }
            catch (System.Exception ex)
            {
            	
            }            
        }
        private void CopyFile(string src, string fileName)
        {
            string dst = Path.Combine(Application.StartupPath, "Tmp");

            if (!Directory.Exists(dst))
            {
                Directory.CreateDirectory(dst);
            }

            if (Directory.Exists(src))
            {
                File.Delete(src);
            }
            File.Copy(src, Path.Combine(dst, fileName), true);
        }
        private void DeleteCopyFile()
        {
            string path = Path.Combine(Application.StartupPath, "Tmp");
            DirectoryInfo dir = new DirectoryInfo(path);

            if (Directory.Exists(path))
            {
                FileInfo[] files = dir.GetFiles("*.*", SearchOption.AllDirectories);

                foreach (System.IO.FileInfo file in files)
                    file.Attributes = FileAttributes.Normal;

                Directory.Delete(path, true);
            }           
        }
        private void btnHighFastBackward_Click(object sender, EventArgs e)
        {
            try
            {
                _index -= 100;
                if (_index < 0)
                    _index = 0;

                UpdateUI();
            }
            catch (System.Exception ex)
            {

            }
        }
        private void btnFastBackward_Click(object sender, EventArgs e)
        {
            try
            {
                _index -= 10;
                if (_index < 0)
                    _index = 0;

                UpdateUI();
            }
            catch (System.Exception ex)
            {
            	
            }
        }
        private void btnBackward_Click(object sender, EventArgs e)
        {
            try
            {
                _index -= 1;
                if (_index < 0)
                    _index = 0;

                UpdateUI();
            }
            catch (System.Exception ex)
            {

            }
        }
        private void btnForward_Click(object sender, EventArgs e)
        {
            try
            {
                _index += 1;
                if (_index >= LstSignalLog.Count)
                    _index = LstSignalLog.Count - 1;

                UpdateUI();
            }
            catch (System.Exception ex)
            {

            }
        }
        private void btnFastForward_Click(object sender, EventArgs e)
        {
            try
            {
                _index += 10;
                if (_index >= LstSignalLog.Count)
                    _index = LstSignalLog.Count - 1;

                UpdateUI();
            }
            catch (System.Exception ex)
            {

            }
        }    
        private void btnHighFastForward_Click(object sender, EventArgs e)
        {
            try
            {
                _index += 100;
                if (_index >= LstSignalLog.Count)
                    _index = LstSignalLog.Count - 1;

                UpdateUI();
            }
            catch (System.Exception ex)
            {

            }
        }

    }
}
