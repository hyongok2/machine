namespace Dit.Framework.EIP
{
    partial class FrmCtrl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCtrl));
            this.variableCompolet1 = new OMRON.Compolet.CIPCompolet64.Variable.VariableCompolet(this.components);
            this.SuspendLayout();
            // 
            // variableCompolet1
            // 
            this.variableCompolet1.Active = false;
            this.variableCompolet1.PlcEncoding = ((System.Text.Encoding)(resources.GetObject("variableCompolet1.PlcEncoding")));
// TODO: '기본 형식이 잘못되었습니다. System.IntPtr. CodeObjectCreateExpression을 사용하십시오.' 예외 때문에 ''의 코드를 생성하지 못했습니다.
            // 
            // FrmCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "FrmCtrl";
            this.Text = "FrmTest";
            this.ResumeLayout(false);

        }

        #endregion

        public OMRON.Compolet.CIPCompolet64.Variable.VariableCompolet variableCompolet1;

        
    }
}