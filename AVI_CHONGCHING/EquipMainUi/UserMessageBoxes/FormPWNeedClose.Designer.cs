namespace EquipMainUi.Setting
{
    partial class FormPWNeedClose
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btndInputPW = new Dit.Framework.UI.UserComponent.ButtonDelay2();
            this.SuspendLayout();
            // 
            // btndInputPW
            // 
            this.btndInputPW.BackColor = System.Drawing.Color.Transparent;
            this.btndInputPW.BackgroundImage = global::EquipMainUi.Properties.Resources.innerworking_ch;
            this.btndInputPW.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btndInputPW.Delay = 500;
            this.btndInputPW.Flicker = false;
            this.btndInputPW.IsLeftLampOn = false;
            this.btndInputPW.IsRightLampOn = false;
            this.btndInputPW.LampAliveTime = 500;
            this.btndInputPW.LampSize = 3;
            this.btndInputPW.LeftLampColor = System.Drawing.Color.Red;
            this.btndInputPW.Location = new System.Drawing.Point(0, 0);
            this.btndInputPW.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btndInputPW.Name = "btndInputPW";
            this.btndInputPW.OnOff = false;
            this.btndInputPW.RightLampColor = System.Drawing.Color.DarkGreen;
            this.btndInputPW.Size = new System.Drawing.Size(1920, 1080);
            this.btndInputPW.TabIndex = 0;
            this.btndInputPW.Text2 = "";
            this.btndInputPW.UseVisualStyleBackColor = false;
            this.btndInputPW.VisibleLeftLamp = false;
            this.btndInputPW.VisibleRightLamp = false;
            this.btndInputPW.DelayClick += new System.EventHandler(this.btndPWInput_Click);
            // 
            // FormPWNeedClose
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.ControlBox = false;
            this.Controls.Add(this.btndInputPW);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormPWNeedClose";
            this.Text = "InnerWorkMessageBox";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CloseCheck_Event);
            this.ResumeLayout(false);

        }

        #endregion

        private Dit.Framework.UI.UserComponent.ButtonDelay2 btndInputPW;
    }
}