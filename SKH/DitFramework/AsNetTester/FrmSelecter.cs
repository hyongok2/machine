using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsNetTester
{
    public partial class FrmSelecter : Form
    {
        public FrmSelecter()
        {
            InitializeComponent();
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            FrmServer ff = new FrmServer();
            ff.Show();
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            FrmClinet ff = new FrmClinet();
            ff.Show();
        }
    }
}
