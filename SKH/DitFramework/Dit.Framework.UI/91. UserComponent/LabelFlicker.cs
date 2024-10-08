using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Dit.Framework.UI.UserComponent
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class LabelFlicker : Label
    {
        public string Text2 { get; set; }
        private Font Font2 { get; set; }
        private Timer _tmr = new Timer();
        private Timer _tmrFlicker = new Timer();

        public Color OnColor { get; set; }
        public Color OffColor { get; set; }

        private bool _onOff { get; set; }
        public int Delay { get; set; }
       
        // 생성자
        public LabelFlicker()
        {
            Text2 = string.Empty;
            Font2 = new Font(this.Font.FontFamily, this.Font.Size - 2f);
            OnColor = Color.FromArgb(255,100,100);
            BackColor = OffColor = Color.White;
            Delay = 2;

            InitializeComponent();

            _tmrFlicker.Interval = 1000;
            _tmrFlicker.Tick += new EventHandler(_tmrFlicker_Tick);
        }
        public bool OnOff
        {
            get { return _onOff; }
            set
            {
                if (_onOff != value)
                {
                    BackColor = value ? OnColor : OffColor;
                    Refresh();
                }
                _onOff = value;
            }
        }
        public bool Flicker
        {
            get { return _tmrFlicker.Enabled; }
            set
            {
                _tmrFlicker.Enabled = value;
                if (value == false)
                    BackColor = OnOff ? OnColor : OffColor;
            }
        }
        private void _tmrFlicker_Tick(object sender, EventArgs e)
        {
            if (Flicker)
            {
                BackColor = (BackColor == OnColor) ? OffColor : OnColor;
            }
        }        

        private DateTime _lastFilckerTime = DateTime.Now;
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            DrawOutlineTextWithImage(pe.Graphics, this.ForeColor, Color.Gray);
        }
        private void DrawOutlineTextWithImage(Graphics graphics, Color color, Color color_2)
        {
            Rectangle rectText = new Rectangle(this.Width / 10, this.Height / 10, this.Width - this.Width / 10 * 2, this.Height - this.Height / 10 * 2);
            ContentAlignment alignText = ContentAlignment.BottomRight;
            TextFormatFlags flags = TranslateTextFormatFlags(alignText);
            TextRenderer.DrawText(graphics, Text2, this.Font2, rectText, this.ForeColor, Color.Transparent, flags);
        }
        
        // 메서드 [ 내부 ]
        private static TextFormatFlags TranslateTextFormatFlags(ContentAlignment contentAlign)
        {
            TextFormatFlags verti;
            {
                ContentAlignment anyBottom = ContentAlignment.BottomCenter | ContentAlignment.BottomLeft | ContentAlignment.BottomRight;
                ContentAlignment anyMiddle = ContentAlignment.MiddleCenter | ContentAlignment.MiddleLeft | ContentAlignment.MiddleRight;

                if ((contentAlign & anyBottom) != ((ContentAlignment)0))
                {
                    verti = TextFormatFlags.Bottom;
                }
                else if ((contentAlign & anyMiddle) != ((ContentAlignment)0))
                {
                    verti = TextFormatFlags.VerticalCenter;
                }
                else
                {
                    verti = TextFormatFlags.Top;
                }
            }

            TextFormatFlags horiz;
            {
                ContentAlignment anyRight = ContentAlignment.BottomRight | ContentAlignment.MiddleRight | ContentAlignment.TopRight;
                ContentAlignment anyCenter = ContentAlignment.BottomCenter | ContentAlignment.MiddleCenter | ContentAlignment.TopCenter;

                if ((contentAlign & anyRight) != ((ContentAlignment)0))
                {
                    horiz = TextFormatFlags.Right;
                }
                else if ((contentAlign & anyCenter) != ((ContentAlignment)0))
                {
                    horiz = TextFormatFlags.HorizontalCenter;
                }
                else
                {
                    horiz = TextFormatFlags.Left;
                }
            }

            TextFormatFlags @default = TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak | TextFormatFlags.NoPadding;
            return verti | horiz | @default;
        }
    }
}
