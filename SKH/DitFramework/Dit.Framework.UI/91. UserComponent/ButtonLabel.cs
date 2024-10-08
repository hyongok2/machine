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
    public partial class ButtonLabel : Label
    {
        private int _alpha = 128;
        private bool _clicked = false;
        private Color _colorOnMouseEnter = Color.FromArgb(79, 129, 189);
        private Color _colorOnMouseLeave = Color.Transparent;
        //private Color _selectedColor = Color.Azure;
        private Color _selectedColor = Color.FromArgb(144, 200, 246);
        private Color _unSelectedColor = Color.Transparent;

        public Color ColorOnMouseLeave
        {
            get { return _colorOnMouseLeave; }
            set { _colorOnMouseLeave = value; }
        }

        public TextImageRelation TextImageRelation { get; set; }
        public int ImageTextPadding { get; set; }
        private bool _selected = false;
        public bool Selected
        {
            get
            {
                
                return _selected;             
            }
            set
            {
                _selected = value;
                
                if (Selected)
                    this.BackColor = _selectedColor;
                else
                    this.BackColor = _colorOnMouseLeave;

                this.Refresh();
            }
        }

        // 생성자
        public ButtonLabel()
        {
            InitializeComponent();

            this.Margin = new Padding(0);
            this.AutoSize = false;
            //this.Width = (int)size.Width + 25;
            this.Height = 22;
            //this.Font = font;
            //this.Text = text;
            this.TextAlign = ContentAlignment.MiddleLeft;
            this.ImageAlign = ContentAlignment.MiddleLeft;
            this.ImageTextPadding = 5;
            this.TextImageRelation = TextImageRelation.ImageBeforeText;

            TextImageRelation = TextImageRelation.Overlay;
            ImageTextPadding = 4;
            Selected = false;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Invalidate();
        }

        // 오버라이드 [ override ]
        protected override void OnPaint(PaintEventArgs pe)
        {
            DrawOutlineTextWithImage(pe.Graphics, this.ForeColor, Color.Gray);
        }

        // 오버라이드 2
        protected override void OnMouseEnter(EventArgs e)
        {
            if (this.Enabled == true)
                this.BackColor = Color.FromArgb(_alpha, _colorOnMouseEnter);

            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            if (this.Enabled == true)
                if (Selected)
                    this.BackColor = _selectedColor;
                else
                    this.BackColor = _colorOnMouseLeave;

            base.OnMouseLeave(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this.Enabled == true)
            {
                _clicked = true;
                this.Refresh();
            }

            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this.Enabled == true)
            {
                _clicked = false;
                this.Refresh();
            }

            base.OnMouseUp(e);
        }
        protected override void OnEnabledChanged(EventArgs e)
        {
            this.Refresh();

            base.OnEnabledChanged(e);
        }

        // 메서드
        protected void DrawOutlineTextWithImage(Graphics graphics, Color foreColor, Color forColorDisabled)
        {
            Image image = this.Image;
            Size offset = _clicked ? new Size(1, 1) : new Size(0, 0);

            Rectangle rectImage = new Rectangle(offset.Width, offset.Height, this.Width, this.Height);
            Rectangle rectText = new Rectangle(0, 0, this.Width, this.Height);

            ContentAlignment alignImage = ImageAlign;
            ContentAlignment alignText = TextAlign;

            if (image != null)
            {
                switch (TextImageRelation)
                {
                    case TextImageRelation.ImageAboveText:  /**/
                        {
                            switch (ImageAlign)
                            {
                                case ContentAlignment.TopLeft:      /**/ ImageAlign = ContentAlignment.TopLeft;   /**/ break;
                                case ContentAlignment.TopCenter:    /**/ ImageAlign = ContentAlignment.TopCenter; /**/ break;
                                case ContentAlignment.TopRight:     /**/ ImageAlign = ContentAlignment.TopRight;  /**/ break;
                                case ContentAlignment.MiddleLeft:   /**/ ImageAlign = ContentAlignment.TopLeft;   /**/ break;
                                case ContentAlignment.MiddleCenter: /**/ ImageAlign = ContentAlignment.TopCenter; /**/ break;
                                case ContentAlignment.MiddleRight:  /**/ ImageAlign = ContentAlignment.TopRight;  /**/ break;
                                case ContentAlignment.BottomLeft:   /**/ ImageAlign = ContentAlignment.TopLeft;   /**/ break;
                                case ContentAlignment.BottomCenter: /**/ ImageAlign = ContentAlignment.TopCenter; /**/ break;
                                case ContentAlignment.BottomRight:  /**/ ImageAlign = ContentAlignment.TopRight;  /**/ break;

                                default:                            /**/ break;
                            }

                            int delta = image.Height + ImageTextPadding;
                            rectText = new Rectangle(offset.Width, offset.Height + delta, this.Width, this.Height - delta);

                            break;
                        }

                    case TextImageRelation.ImageBeforeText:
                        {
                            switch (ImageAlign)
                            {
                                case ContentAlignment.TopLeft:      /**/ ImageAlign = ContentAlignment.TopLeft;    /**/ break;
                                case ContentAlignment.TopCenter:    /**/ ImageAlign = ContentAlignment.TopLeft;    /**/ break;
                                case ContentAlignment.TopRight:     /**/ ImageAlign = ContentAlignment.TopLeft;    /**/ break;
                                case ContentAlignment.MiddleLeft:   /**/ ImageAlign = ContentAlignment.MiddleLeft; /**/ break;
                                case ContentAlignment.MiddleCenter: /**/ ImageAlign = ContentAlignment.MiddleLeft; /**/ break;
                                case ContentAlignment.MiddleRight:  /**/ ImageAlign = ContentAlignment.MiddleLeft; /**/ break;
                                case ContentAlignment.BottomLeft:   /**/ ImageAlign = ContentAlignment.BottomLeft; /**/ break;
                                case ContentAlignment.BottomCenter: /**/ ImageAlign = ContentAlignment.BottomLeft; /**/ break;
                                case ContentAlignment.BottomRight:  /**/ ImageAlign = ContentAlignment.BottomLeft; /**/ break;

                                default:                            /**/ break;
                            }

                            int delta = image.Width + ImageTextPadding;
                            rectText = new Rectangle(offset.Width + delta, offset.Height, this.Width - delta, this.Height);
                            break;
                        }
                    case TextImageRelation.TextAboveImage:
                        {
                            switch (ImageAlign)
                            {
                                case ContentAlignment.TopLeft:      /**/ ImageAlign = ContentAlignment.BottomLeft;   /**/ break;
                                case ContentAlignment.TopCenter:    /**/ ImageAlign = ContentAlignment.BottomCenter; /**/ break;
                                case ContentAlignment.TopRight:     /**/ ImageAlign = ContentAlignment.BottomRight;  /**/ break;
                                case ContentAlignment.MiddleLeft:   /**/ ImageAlign = ContentAlignment.BottomLeft;   /**/ break;
                                case ContentAlignment.MiddleCenter: /**/ ImageAlign = ContentAlignment.BottomCenter; /**/ break;
                                case ContentAlignment.MiddleRight:  /**/ ImageAlign = ContentAlignment.BottomRight;  /**/ break;
                                case ContentAlignment.BottomLeft:   /**/ ImageAlign = ContentAlignment.BottomLeft;   /**/ break;
                                case ContentAlignment.BottomCenter: /**/ ImageAlign = ContentAlignment.BottomCenter; /**/ break;
                                case ContentAlignment.BottomRight:  /**/ ImageAlign = ContentAlignment.BottomRight;  /**/ break;

                                default:                            /**/ break;
                            }

                            int delta = image.Height + ImageTextPadding;
                            rectText = new Rectangle(offset.Width, offset.Height, this.Width, this.Height - delta);
                            break;
                        }
                    case TextImageRelation.TextBeforeImage:
                        {
                            switch (ImageAlign)
                            {
                                case ContentAlignment.TopLeft:      /**/ ImageAlign = ContentAlignment.TopRight;    /**/ break;
                                case ContentAlignment.TopCenter:    /**/ ImageAlign = ContentAlignment.TopRight;    /**/ break;
                                case ContentAlignment.TopRight:     /**/ ImageAlign = ContentAlignment.TopRight;    /**/ break;
                                case ContentAlignment.MiddleLeft:   /**/ ImageAlign = ContentAlignment.MiddleRight; /**/ break;
                                case ContentAlignment.MiddleCenter: /**/ ImageAlign = ContentAlignment.MiddleRight; /**/ break;
                                case ContentAlignment.MiddleRight:  /**/ ImageAlign = ContentAlignment.MiddleRight; /**/ break;
                                case ContentAlignment.BottomLeft:   /**/ ImageAlign = ContentAlignment.BottomRight; /**/ break;
                                case ContentAlignment.BottomCenter: /**/ ImageAlign = ContentAlignment.BottomRight; /**/ break;
                                case ContentAlignment.BottomRight:  /**/ ImageAlign = ContentAlignment.BottomRight; /**/ break;

                                default:                            /**/ break;
                            }

                            int delta = image.Width + ImageTextPadding;
                            rectText = new Rectangle(offset.Width, offset.Height, this.Width - delta, this.Height);
                            break;
                        }

                    default:
                    case TextImageRelation.Overlay:
                        break;
                }

                this.DrawImage(graphics, image, rectImage, alignImage);
            }

            TextFormatFlags flags = TranslateTextFormatFlags(alignText);

            // 원래 텍스트 다시 써줌...
            TextRenderer.DrawText(graphics, this.Text, this.Font, rectText, this.Enabled ? foreColor : forColorDisabled, Color.Transparent, flags);
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
