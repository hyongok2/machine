using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Dit.Framework.UI.UserComponent
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class PanelRound : Panel
    {
        // 생성자
        public PanelRound()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw, true);
        }
        public PanelRound(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        // 오버라이드
        protected override void OnPaint(PaintEventArgs e)
        {
            var state = e.Graphics.Save();
            e.Graphics.InterpolationMode = InterpolationMode.Bicubic;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;


            e.Graphics.Clear(this.BackColor);

            var outer = Rectangle.FromLTRB(
                this.Padding.Left,
                this.Padding.Top,
                (this.Width - 1) - this.Padding.Right,
                (this.Height - 1) - this.Padding.Bottom);

            using (var pen = new Pen(this.ForeColor, 2.5f))
            using (var path = GetRoundRectPath(outer, 20))
                e.Graphics.DrawPath(pen, path);

            e.Graphics.Restore(state);
        }

        // 메서드
        private static GraphicsPath GetRoundRectPath(Rectangle rect, int radius)
        {
            int x = rect.X;
            int y = rect.Y;
            int width = rect.Width;
            int height = rect.Height;

            GraphicsPath gp = new GraphicsPath();

            gp.AddArc(x + width - radius, y, radius, radius, 270, 90);
            gp.AddArc(x + width - radius, y + height - radius, radius, radius, 0, 90);
            gp.AddArc(x, y + height - radius, radius, radius, 90, 90);
            gp.AddArc(x, y, radius, radius, 180, 90);
            gp.CloseFigure();

            return gp;
        }

    }
}
