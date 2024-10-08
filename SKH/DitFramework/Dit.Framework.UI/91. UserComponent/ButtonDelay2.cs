using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Dit.Framework.UI.UserComponent
{
    /// <summary>
    /// Lamp Alive 시간 기능 추가
    /// DelayClick시 Click이벤트 발생안하게 함.
    /// date 170712
    /// </summary>
    [System.ComponentModel.ToolboxItem(true)]
    public partial class ButtonDelay2 : Button
    {
        public event EventHandler DelayClick;
        public string Text2 { get; set; }
        private Font Font2 { get; set; }
        private DateTime _mouseDownTime = DateTime.MaxValue;
        private Timer _tmr = new Timer();
        private Timer _tmrFlicker = new Timer();

        private Color OnColor { get; set; }
        private Color OffColor { get; set; }

        private bool _isDelayClicked { get; set; }
        /* Lamp 색, 딜레이 추가
         * date : 170630
         * since : 170622
         */
        /// <summary>
        /// 정사각형 크기.
        /// </summary>
        public int LampSize { get; set; }
        public bool VisibleLeftLamp { get; set; }
        public bool VisibleRightLamp { get; set; }
        public bool IsLeftLampOn { get; set; }
        public bool IsRightLampOn { get; set; }
        /// <summary>
        /// ms
        /// </summary>        
        public int LampAliveTime { get; set; }
        public Color LeftLampColor { get; set; }
        public Color RightLampColor { get; set; }
        private bool _leftLampAlive = false;
        private bool _rightLampAlive = false;
        private Stopwatch _leftLampOnCheck = new Stopwatch();
        private Stopwatch _rightLampOnCheck = new Stopwatch();
        ///

        private bool _onOff { get; set; }
        public int Delay { get; set; }
        private int _minDelay = 200; // 최소딜레이
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
                if (_tmrFlicker.Enabled == value)
                    return;

                _tmrFlicker.Enabled = value;
                if (value == false)
                    BackColor = OnOff ? OnColor : OffColor;
                else
                    BackColor = OnColor;
            }
        }

        // 생성자
        public ButtonDelay2()
        {
            LampSize = 3;
            VisibleLeftLamp = false;
            VisibleRightLamp = false;
            LampAliveTime = 500;
            LeftLampColor = Color.Red;
            RightLampColor = Color.DarkGreen;
            Text2 = string.Empty;
            Font2 = new Font(this.Font.FontFamily, this.Font.Size - 2f);
            OnColor = Color.FromArgb(255,100,100);
            BackColor = OffColor = Color.Transparent;
            Delay = 100;

            InitializeComponent();

            _tmr.Interval = 50;
            _tmr.Tick += new EventHandler(Tmr_Tick);
            _tmrFlicker.Interval = 1000;
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

                if (Delay > _minDelay && 
                    DelayClick != null)
                {
                    _isDelayClicked = true;
                    DelayClick(this, new EventArgs());                    
                }
            }
            else
            {
                Text2 = string.Format("{0} 초", Math.Round((_mouseDownTime - DateTime.Now).TotalMilliseconds / 1000, 1));
            }
            this.Refresh();
        }

        private DateTime _lastFilckerTime = DateTime.Now;
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            DrawOutlineTextWithImage(pe.Graphics, this.ForeColor, Color.Gray);
            DrawLamp(pe);            
        }

        private void DrawLamp(PaintEventArgs pe)
        {
            if (VisibleLeftLamp)
            {
                if (IsLeftLampOn)
                {
                    _leftLampOnCheck.Restart();
                    _leftLampAlive = true;
                }
                else if (_leftLampAlive == true && _leftLampOnCheck.ElapsedMilliseconds > LampAliveTime)
                {
                    _leftLampAlive = false;
                }

                pe.Graphics.FillRectangle(new SolidBrush(_leftLampAlive ? LeftLampColor : Color.White), new Rectangle(this.Size.Width - (LampSize * 3) - 2, LampSize, LampSize, LampSize));
            }
            if (VisibleRightLamp)
            {
                if (IsRightLampOn)
                {
                    _rightLampOnCheck.Restart();
                    _rightLampAlive = true;
                }
                else if (_rightLampAlive == true && _rightLampOnCheck.ElapsedMilliseconds > LampAliveTime)
                {
                    _rightLampAlive = false;
                }

                pe.Graphics.FillRectangle(new SolidBrush(_rightLampAlive ? RightLampColor : Color.White), new Rectangle(this.Size.Width - (LampSize * 2), LampSize, LampSize, LampSize));
            }
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
            _isDelayClicked = false;
            base.OnMouseDown(mevent);
            _mouseDownTime = DateTime.Now.AddMilliseconds(Delay);
            _tmr.Start();
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (_isDelayClicked == false)
                base.OnMouseUp(mevent);
            else
                base.OnMouseUp(new MouseEventArgs(MouseButtons.Left, -1, -1, 0, 0));
            
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
