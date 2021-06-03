
namespace ResponseEmergencySystem.Forms
{
    partial class AddComments
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
            this.simpleButton9 = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Cancel2 = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton9
            // 
            this.simpleButton9.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(40)))), ((int)(((byte)(94)))));
            this.simpleButton9.Appearance.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton9.Appearance.ForeColor = System.Drawing.Color.White;
            this.simpleButton9.Appearance.Options.UseBackColor = true;
            this.simpleButton9.Appearance.Options.UseFont = true;
            this.simpleButton9.Appearance.Options.UseForeColor = true;
            this.simpleButton9.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.simpleButton9.ImageOptions.SvgImage = global::ResponseEmergencySystem.Properties.Resources.saveWhite;
            this.simpleButton9.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.simpleButton9.Location = new System.Drawing.Point(241, 272);
            this.simpleButton9.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.simpleButton9.LookAndFeel.UseDefaultLookAndFeel = false;
            this.simpleButton9.Name = "simpleButton9";
            this.simpleButton9.Size = new System.Drawing.Size(102, 42);
            this.simpleButton9.TabIndex = 1;
            this.simpleButton9.Text = "Save";
            this.simpleButton9.Click += new System.EventHandler(this.simpleButton9_Click);
            // 
            // btn_Cancel2
            // 
            this.btn_Cancel2.Appearance.BackColor = System.Drawing.Color.Maroon;
            this.btn_Cancel2.Appearance.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel2.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Cancel2.Appearance.Options.UseBackColor = true;
            this.btn_Cancel2.Appearance.Options.UseFont = true;
            this.btn_Cancel2.Appearance.Options.UseForeColor = true;
            this.btn_Cancel2.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Cancel2.ImageOptions.SvgImage = global::ResponseEmergencySystem.Properties.Resources.closeWhite;
            this.btn_Cancel2.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btn_Cancel2.Location = new System.Drawing.Point(133, 272);
            this.btn_Cancel2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btn_Cancel2.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn_Cancel2.Name = "btn_Cancel2";
            this.btn_Cancel2.Size = new System.Drawing.Size(102, 42);
            this.btn_Cancel2.TabIndex = 2;
            this.btn_Cancel2.Text = "Cancel";
            this.btn_Cancel2.Click += new System.EventHandler(this.btn_Cancel2_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(23, 47);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(63, 17);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Comments";
            // 
            // memoEdit1
            // 
            this.memoEdit1.Location = new System.Drawing.Point(102, 46);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new System.Drawing.Size(321, 211);
            this.memoEdit1.TabIndex = 5;
            // 
            // AddComments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 331);
            this.Controls.Add(this.memoEdit1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.btn_Cancel2);
            this.Controls.Add(this.simpleButton9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddComments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Capture Comments";
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton9;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.MemoEdit memoEdit1;
    }
}