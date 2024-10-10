using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EquipMainUi
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class UserCtrl : UserControl
    {
        public UserCtrl()
        {   
            InitializeComponent();


              base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor |
                               ControlStyles.UserPaint, true);//중요합니다.
        base.SetStyle(ControlStyles.Selectable | ControlStyles.FixedHeight, false);
        base.SetStyle(ControlStyles.ResizeRedraw, true);
        this.TabStop = false;
        this.BackColor = Color.Transparent;//중요합니다.
       
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
        }

        private void UserCtrl_Load(object sender, EventArgs e)
        {

        }
    }
}
