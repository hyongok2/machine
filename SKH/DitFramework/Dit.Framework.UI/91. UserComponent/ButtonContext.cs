using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Design;

namespace Dit.Framework.UI.UserComponent
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class ButtonContext : ButtonDelay
    {
        public event EventHandler ContextClick;
        public bool IsUsingOnOdd { get; set; }
        public Color MenuBackgroundColor
        {
            get
            {
                return contextMenuStrip.BackColor;
            }
            set
            {
                contextMenuStrip.BackColor = value;
            }
        }

        private bool _isOdd = true;
       
        // 생성자
        public ButtonContext()
            :base()
        {
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        // When Button Click, Context Menu Show
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);

            if (mevent.Button == MouseButtons.Left && ((IsUsingOnOdd == true && _isOdd == true) || IsUsingOnOdd == false))
            {
                Point ptLowerLeft = new Point(0, this.Height);
                ptLowerLeft = this.PointToScreen(ptLowerLeft);
                contextMenuStrip.Show(ptLowerLeft);
            }

            _isOdd = !_isOdd;
        }

        //Context Menu Item Click Event
        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ContextClick(this, e);
        }

        //Context Menu Item Add method
        public void AddItem(string item)
        {
            contextMenuStrip.Items.Add(item);
        }
        public void AddSeparator()
        {
            contextMenuStrip.Items.Add(new ToolStripSeparator());
        }
        public void Clear()
        {
            contextMenuStrip.Items.Clear();
        }
    }
}
