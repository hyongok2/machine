using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EquipMainUi.UserMessageBoxes
{
    public partial class FrmRadioSelect : Form
    {
        public int SelectedItemIndex { get; set; }
        public string MainMessage { set { lblMainMsg.Text = value; } }

        private List<RadioButton> _rdList = new List<RadioButton>();

        public FrmRadioSelect()
        {
            InitializeComponent();
        }

        public void InitSelectItem(string[] items)
        {
            _rdList.Clear();
            int tagIdx = 0;
            foreach (string name in items)
            {
                RadioButton rd = new RadioButton();
                rd.Name = "rd" + name;
                rd.Text = name;
                rd.AutoSize = true;
                rd.Tag = tagIdx++;
                rd.Parent = flowLayoutPanel1;
                rd.Click += SomeRadioButton_Click;
                _rdList.Add(rd);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (SelectedItemIndex == -1)
                MessageBox.Show(GG.boChinaLanguage ? "选择后可以确认" : "선택 후 확인 가능");
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SelectedItemIndex = -1;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SomeRadioButton_Click(object sender, EventArgs e)
        {
            SelectedItemIndex = int.Parse((sender as RadioButton).Tag.ToString());
            lblSelectedIndex.Text = SelectedItemIndex.ToString();
        }
    }
}
