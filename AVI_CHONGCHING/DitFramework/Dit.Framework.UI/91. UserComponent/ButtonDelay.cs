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
    public partial class ButtonDelay : Button
    {
        public event EventHandler DelayClick;
        public string Text2 { get; set; }
        private Font Font2 { get; set; }
        private DateTime _mouseDownTime = DateTime.MaxValue;
        private Timer _tmr = new Timer();
        private Timer _tmrFlicker = new Timer();

        public Color OnColor { get; set; }
        public Color OffColor { get; set; }        

        /* Lamp 표시 기능 추가
         * date : 170622
         */
        /// <summary>
        /// 정사각형 크기.
        /// </summary>
        public int LampSize { get; set; }
        public Color LeftLampColor { get; set; }
        public Color RightLampColor { get; set; }
        public int LampOnWiatTime { get; set; }
        public bool VisibleLeftLamp { get; set; }
        public bool VisibleRightLamp { get; set; }

        private bool _isLeftLampOn = false;
        private bool _isRightLampOn = false;
        public bool IsLeftLampOn 
        { 
            get 
            { 
                return _isLeftLampOn; 
            }
            set
            {
                _isLeftLampOn = value;
            }
        }
        public bool IsRightLampOn 
        { 
            get 
            { 
                return _isRightLampOn; 
            }
            set
            {
                _isRightLampOn = value;
            }
        }
        ///

        private bool _onOff { get; set; }
        public int Delay { get; set; }
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
        public int _flickerInterval = 900;
        public int FlickerInterval
        {
            get { return _flickerInterval; }
            set { _flickerInterval = value; }
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
        DateTime _leftDelayOffStartTime = DateTime.Now;
        public bool LeftDelayOff
        {
            get { return _isLeftLampOn; }
            set
            {
                if (value == true)
                {
                    _leftDelayOffStartTime = DateTime.Now;
                    _isLeftLampOn = true;
                }

                if (Math.Abs((DateTime.Now - _leftDelayOffStartTime).TotalMilliseconds) > LampOnWiatTime && value == false)
                {
                    _isLeftLampOn = false;
                }
            }
        }
        DateTime _rightDelayOffStartTime = DateTime.Now;
        public bool RightDelayOff
        {
            get { return _isRightLampOn; }
            set
            {
                if (value == true)
                {
                    _rightDelayOffStartTime = DateTime.Now;
                    _isRightLampOn = true;
                }

                if (Math.Abs((DateTime.Now - _rightDelayOffStartTime).TotalMilliseconds) > LampOnWiatTime && value == false)
                {
                    _isRightLampOn = false;
                }
            }
        }

        // 생성자
        public ButtonDelay()
        {
            LampSize = 1;
            VisibleLeftLamp = false;
            VisibleRightLamp = false;
            Text2 = string.Empty;
            Font2 = new Font(this.Font.FontFamily, this.Font.Size - 2f);
            OnColor = Color.DodgerBlue;//Color.FromArgb(255,100,100);
            LeftLampColor = Color.Red;
            RightLampColor = Color.Green;
            BackColor = OffColor = Color.Transparent;
            Delay = 2;

            InitializeComponent();

            _tmr.Interval = 50;
            _tmr.Tick += new EventHandler(Tmr_Tick);
            _tmrFlicker.Interval = FlickerInterval;
            _tmrFlicker.Tick += new EventHandler(_tmrFlicker_Tick);
        }
        private void _tmrFlicker_Tick(object sender, EventArgs e)
        {
            if (Flicker)
            {
                BackColor = (BackColor == OnColor) ? OffColor : OnColor;
            }
        }
        private void Tmr_Tick(object sender, EventArgs e)
        {
            if (_mouseDownTime < DateTime.Now)
            {
                Text2 = string.Empty;
                _tmr.Stop();

                if (DelayClick != null)
                    DelayClick(this, new EventArgs());
            }
            else
            {
                Text2 = string.Format("{0} Sec", Math.Round((_mouseDownTime - DateTime.Now).TotalMilliseconds / 1000, 1));
            }
            this.Refresh();
        }

        private DateTime _lastFilckerTime = DateTime.Now;
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            DrawOutlineTextWithImage(pe.Graphics, this.ForeColor, Color.Gray);
            if (VisibleLeftLamp)
                pe.Graphics.FillRectangle(new SolidBrush(IsLeftLampOn ? LeftLampColor : Color.White), new Rectangle(this.Size.Width - (LampSize * 3) - 2, LampSize, LampSize, LampSize));
            if (VisibleRightLamp)
                pe.Graphics.FillRectangle(new SolidBrush(IsRightLampOn ? RightLampColor : Color.White), new Rectangle(this.Size.Width - (LampSize * 2), LampSize, LampSize, LampSize));
        }
        private void DrawOutlineTextWithImage(Graphics graphics, Color color, Color color_2)
        {
            Rectangle rectText = new Rectangle(this.Width / 10, this.Height / 10, this.Width - this.Width / 10 * 2, this.Height - this.Height / 10 * 2);
            ContentAlignment alignText = ContentAlignment.BottomRight;
            TextFormatFlags flags = TranslateTextFormatFlags(alignText);
            TextRenderer.DrawText(graphics, Text2, this.Font2, rectText, this.ForeColor, Color.Transparent, flags);
        }
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            _mouseDownTime = DateTime.Now.AddMilliseconds(Delay);
            _tmr.Start();
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            _mouseDownTime = DateTime.MaxValue;           
            _tmr.Stop();
            Text2 = string.Empty;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (_tmr.Enabled)
            {
                _mouseDownTime = DateTime.MaxValue;
                _tmr.Stop();
            }
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
