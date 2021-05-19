
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lue_StatusDetail = new DevExpress.XtraEditors.LookUpEdit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lue_StatusDetail.Properties)).BeginInit();
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
            this.btn_Cancel2.Appearance.BackColor = System.Drawing.Color.Maroon;
            this.btn_Cancel2.Appearance.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel2.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Cancel2.Appearance.Options.UseBackColor = true;
            this.btn_Cancel2.Appearance.Options.UseFont = true;
            this.btn_Cancel2.Appearance.Options.UseForeColor = true;
            this.btn_Cancel2.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.btn_Cancel2.ImageOptions.SvgImage = global::ResponseEmergencySystem.Properties.Resources.closeWhite;
            this.btn_Cancel2.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.btn_Cancel2.Location = new System.Drawing.Point(125, 265);
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
            this.simpleButton9.Appearance.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton9.Appearance.ForeColor = System.Drawing.Color.White;
            this.simpleButton9.Appearance.Options.UseBackColor = true;
            this.simpleButton9.Appearance.Options.UseFont = true;
            this.simpleButton9.Appearance.Options.UseForeColor = true;
            this.simpleButton9.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.simpleButton9.ImageOptions.SvgImage = global::ResponseEmergencySystem.Properties.Resources.saveWhite;
            this.simpleButton9.ImageOptions.SvgImageSize = new System.Drawing.Size(25, 25);
            this.simpleButton9.Location = new System.Drawing.Point(233, 265);
            this.simpleButton9.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.simpleButton9.LookAndFeel.UseDefaultLookAndFeel = false;
            this.simpleButton9.Name = "simpleButton9";
            this.simpleButton9.Size = new System.Drawing.Size(102, 42);
            this.simpleButton9.TabIndex = 6;
            this.simpleButton9.Text = "Save";
            this.simpleButton9.Click += new System.EventHandler(this.simpleButton9_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(24, 37);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 17);
            this.labelControl1.TabIndex = 11;
            this.labelControl1.Text = "Status";
            // 
            // lue_StatusDetail
            // 
            this.lue_StatusDetail.Location = new System.Drawing.Point(103, 34);
            this.lue_StatusDetail.Name = "lue_StatusDetail";
            this.lue_StatusDetail.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lue_StatusDetail.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_StatusDetail", "ID_StatusDetail", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Description", "Description")});
            this.lue_StatusDetail.Properties.DisplayMember = "Description";
            this.lue_StatusDetail.Properties.NullText = "";
            this.lue_StatusDetail.Properties.ShowHeader = false;
            this.lue_StatusDetail.Properties.ValueMember = "ID_StatusDetail";
            this.lue_StatusDetail.Size = new System.Drawing.Size(134, 24);
            this.lue_StatusDetail.TabIndex = 12;
            // 
            // EditComments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 325);
            this.Controls.Add(this.lue_StatusDetail);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.memoEdit1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.btn_Cancel2);
            this.Controls.Add(this.simpleButton9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "EditComments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit";
            this.Load += new System.EventHandler(this.EditComments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.memoEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lue_StatusDetail.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit memoEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel2;
        private DevExpress.XtraEditors.SimpleButton simpleButton9;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit lue_StatusDetail;
    }
}