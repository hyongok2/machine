namespace EquipMainUi.UserMessageBoxes
{
    partial class FrmPGrid
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
            this.propertyGridEx1 = new EquipMainUi.Common.Convenience.PropertyGridEx();
            this.SuspendLayout();
            // 
            // propertyGridEx1
            // 
            this.propertyGridEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridEx1.HelpVisible = false;
            this.propertyGridEx1.Location = new System.Drawing.Point(0, 0);
            this.propertyGridEx1.Name = "propertyGridEx1";
            this.propertyGridEx1.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGridEx1.Size = new System.Drawing.Size(800, 450);
            this.propertyGridEx1.TabIndex = 0;
            this.propertyGridEx1.ToolbarVisible = false;
            // 
            // FrmPGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.propertyGridEx1);
            this.Name = "FrmPGrid";
            this.Text = "FrmPGrid";
            this.ResumeLayout(false);

        }

        #endregion

        private Common.Convenience.PropertyGridEx propertyGridEx1;
    }
}