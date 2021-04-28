
namespace ResponseEmergencySystem.Forms.Modals
{
    partial class EditComments
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
            this.memoEdit1 = new DevExpress.XtraEditors.MemoEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btn_Cancel2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton9 = new DevExpress.XtraEditors.SimpleButton();
            this.textEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // memoEdit1
            // 
            this.memoEdit1.Location = new System.Drawing.Point(103, 83);
            this.memoEdit1.Name = "memoEdit1";
            this.memoEdit1.Size = new System.Drawing.Size(321, 161);
            this.memoEdit1.TabIndex = 9;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(24, 84);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(63, 17);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "Comments";
            // 
            // btn_Cancel2
            // 
            this.btn_Cancel2.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.btn_Cancel2.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Cancel2.Appearance.Options.UseBackColor = true;
            this.btn_Cancel2.Appearance.Options.UseForeColor = true;
            this.btn_Cancel2.ImageOptions.SvgImage = global::ResponseEmergencySystem.Properties.Resources.closeWhite;
            this.btn_Cancel2.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btn_Cancel2.Location = new System.Drawing.Point(24, 286);
            this.btn_Cancel2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btn_Cancel2.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn_Cancel2.Name = "btn_Cancel2";
            this.btn_Cancel2.Size = new System.Drawing.Size(102, 42);
            this.btn_Cancel2.TabIndex = 7;
            this.btn_Cancel2.Text = "Cancel";
            // 
            // simpleButton9
            // 
            this.simpleButton9.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(40)))), ((int)(((byte)(94)))));
            this.simpleButton9.Appearance.ForeColor = System.Drawing.Color.White;
            this.simpleButton9.Appearance.Options.UseBackColor = true;
            this.simpleButton9.Appearance.Options.UseForeColor = true;
            this.simpleButton9.ImageOptions.SvgImage = global::ResponseEmergencySystem.Properties.Resources.saveWhite;
            this.simpleButton9.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.simpleButton9.Location = new System.Drawing.Point(322, 286);
            this.simpleButton9.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.simpleButton9.LookAndFeel.UseDefaultLookAndFeel = false;
            this.simpleButton9.Name = "simpleButton9";
            this.simpleButton9.Size = new System.Drawing.Size(102, 42);
            this.simpleButton9.TabIndex = 6;
            this.simpleButton9.Text = "Save";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(103, 34);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.textEdit1.Size = new System.Drawing.Size(140, 24);
            this.textEdit1.TabIndex = 10;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(24, 37);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 17);
            this.labelControl1.TabIndex = 11;
            this.labelControl1.Text = "Status";
            // 
            // EditComments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 360);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.memoEdit1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.btn_Cancel2);
            this.Controls.Add(this.simpleButton9);
            this.Controls.Add(this.textEdit1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "EditComments";
            this.Text = "Edit";
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel2;
        private DevExpress.XtraEditors.SimpleButton simpleButton9;
        private DevExpress.XtraEditors.ComboBoxEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}