
namespace ResponseEmergencySystem.Forms
{
    partial class frm_Image
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
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.pnl_Zoom = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.pnl_ImgControls = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.pnl_Uploading = new DevExpress.XtraEditors.PanelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.btn_SaveImage = new DevExpress.XtraEditors.SimpleButton();
            this.btn_Cancel = new DevExpress.XtraEditors.SimpleButton();
            this.img_Test = new DevExpress.XtraEditors.PictureEdit();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::ResponseEmergencySystem.Forms.WaitForm1), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Zoom)).BeginInit();
            this.pnl_Zoom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_ImgControls)).BeginInit();
            this.pnl_ImgControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Uploading)).BeginInit();
            this.pnl_Uploading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img_Test.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.simpleButton1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(40)))), ((int)(((byte)(94)))));
            this.simpleButton1.Appearance.ForeColor = System.Drawing.Color.White;
            this.simpleButton1.Appearance.Options.UseBackColor = true;
            this.simpleButton1.Appearance.Options.UseForeColor = true;
            this.simpleButton1.ImageOptions.SvgImage = global::ResponseEmergencySystem.Properties.Resources.uploadWhite;
            this.simpleButton1.ImageOptions.SvgImageSize = new System.Drawing.Size(30, 30);
            this.simpleButton1.Location = new System.Drawing.Point(369, 684);
            this.simpleButton1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.simpleButton1.LookAndFeel.UseDefaultLookAndFeel = false;
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(129, 38);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "Load Image";
            this.simpleButton1.Click += new System.EventHandler(this.onClickLoadImage);
            // 
            // pnl_Zoom
            // 
            this.pnl_Zoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnl_Zoom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Zoom.Controls.Add(this.simpleButton5);
            this.pnl_Zoom.Controls.Add(this.simpleButton4);
            this.pnl_Zoom.Location = new System.Drawing.Point(250, 684);
            this.pnl_Zoom.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnl_Zoom.Name = "pnl_Zoom";
            this.pnl_Zoom.Size = new System.Drawing.Size(72, 41);
            this.pnl_Zoom.TabIndex = 7;
            this.pnl_Zoom.Visible = false;
            // 
            // simpleButton5
            // 
            this.simpleButton5.ImageOptions.Image = global::ResponseEmergencySystem.Properties.Resources.zoomin_16x161;
            this.simpleButton5.Location = new System.Drawing.Point(3, 9);
            this.simpleButton5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(28, 29);
            this.simpleButton5.TabIndex = 1;
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // simpleButton4
            // 
            this.simpleButton4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton4.ImageOptions.Image = global::ResponseEmergencySystem.Properties.Resources.zoomout_16x16;
            this.simpleButton4.Location = new System.Drawing.Point(37, 9);
            this.simpleButton4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(28, 29);
            this.simpleButton4.TabIndex = 0;
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // pnl_ImgControls
            // 
            this.pnl_ImgControls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_ImgControls.Appearance.BackColor = System.Drawing.SystemColors.Highlight;
            this.pnl_ImgControls.Appearance.Options.UseBackColor = true;
            this.pnl_ImgControls.Controls.Add(this.labelControl1);
            this.pnl_ImgControls.Controls.Add(this.labelControl2);
            this.pnl_ImgControls.Controls.Add(this.labelControl3);
            this.pnl_ImgControls.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnl_ImgControls.Location = new System.Drawing.Point(734, 16);
            this.pnl_ImgControls.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnl_ImgControls.Name = "pnl_ImgControls";
            this.pnl_ImgControls.Size = new System.Drawing.Size(154, 43);
            this.pnl_ImgControls.TabIndex = 8;
            this.pnl_ImgControls.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl1.ImageOptions.SvgImage = global::ResponseEmergencySystem.Properties.Resources.refreshBlue;
            this.labelControl1.ImageOptions.SvgImageSize = new System.Drawing.Size(45, 45);
            this.labelControl1.Location = new System.Drawing.Point(109, -2);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(45, 45);
            this.labelControl1.TabIndex = 12;
            this.labelControl1.ToolTip = "Reset Image";
            this.labelControl1.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.labelControl1.Click += new System.EventHandler(this.labelControl1_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl2.ImageOptions.SvgImage = global::ResponseEmergencySystem.Properties.Resources.zoomOutBlue;
            this.labelControl2.ImageOptions.SvgImageSize = new System.Drawing.Size(40, 40);
            this.labelControl2.Location = new System.Drawing.Point(63, 3);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(40, 40);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.ToolTip = "Zoom In (Right Click)";
            this.labelControl2.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.labelControl2.Click += new System.EventHandler(this.labelControl2_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl3.ImageOptions.SvgImage = global::ResponseEmergencySystem.Properties.Resources.zoomInBlue;
            this.labelControl3.ImageOptions.SvgImageSize = new System.Drawing.Size(40, 40);
            this.labelControl3.Location = new System.Drawing.Point(17, 0);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(40, 40);
            this.labelControl3.TabIndex = 11;
            this.labelControl3.ToolTip = "Zoom Out (Left Click) ";
            this.labelControl3.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.labelControl3.Click += new System.EventHandler(this.labelControl3_Click);
            // 
            // pnl_Uploading
            // 
            this.pnl_Uploading.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Uploading.Appearance.Options.UseBackColor = true;
            this.pnl_Uploading.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Uploading.Controls.Add(this.labelControl4);
            this.pnl_Uploading.Controls.Add(this.progressBarControl1);
            this.pnl_Uploading.Location = new System.Drawing.Point(286, 319);
            this.pnl_Uploading.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnl_Uploading.Name = "pnl_Uploading";
            this.pnl_Uploading.Size = new System.Drawing.Size(320, 41);
            this.pnl_Uploading.TabIndex = 14;
            this.pnl_Uploading.Visible = false;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(6, 8);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(109, 17);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "Uploading  Image";
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarControl1.Location = new System.Drawing.Point(117, 7);
            this.progressBarControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Size = new System.Drawing.Size(197, 24);
            this.progressBarControl1.TabIndex = 3;
            // 
            // btn_SaveImage
            // 
            this.btn_SaveImage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_SaveImage.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(40)))), ((int)(((byte)(94)))));
            this.btn_SaveImage.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_SaveImage.Appearance.Options.UseBackColor = true;
            this.btn_SaveImage.Appearance.Options.UseForeColor = true;
            this.btn_SaveImage.ImageOptions.SvgImage = global::ResponseEmergencySystem.Properties.Resources.saveWhite;
            this.btn_SaveImage.ImageOptions.SvgImageSize = new System.Drawing.Size(30, 30);
            this.btn_SaveImage.Location = new System.Drawing.Point(736, 684);
            this.btn_SaveImage.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btn_SaveImage.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn_SaveImage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_SaveImage.Name = "btn_SaveImage";
            this.btn_SaveImage.Size = new System.Drawing.Size(129, 38);
            this.btn_SaveImage.TabIndex = 15;
            this.btn_SaveImage.Text = "Save";
            this.btn_SaveImage.Click += new System.EventHandler(this.btn_SaveImage_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Cancel.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(27)))), ((int)(((byte)(27)))));
            this.btn_Cancel.Appearance.ForeColor = System.Drawing.Color.White;
            this.btn_Cancel.Appearance.Options.UseBackColor = true;
            this.btn_Cancel.Appearance.Options.UseForeColor = true;
            this.btn_Cancel.ImageOptions.SvgImage = global::ResponseEmergencySystem.Properties.Resources.closeWhite;
            this.btn_Cancel.ImageOptions.SvgImageSize = new System.Drawing.Size(30, 30);
            this.btn_Cancel.Location = new System.Drawing.Point(14, 684);
            this.btn_Cancel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
            this.btn_Cancel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(129, 38);
            this.btn_Cancel.TabIndex = 16;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // img_Test
            // 
            this.img_Test.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.img_Test.Location = new System.Drawing.Point(14, 16);
            this.img_Test.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.img_Test.Name = "img_Test";
            this.img_Test.Properties.AllowScrollViaMouseDrag = true;
            this.img_Test.Properties.AllowZoom = DevExpress.Utils.DefaultBoolean.True;
            this.img_Test.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.img_Test.Properties.ShowMenu = false;
            this.img_Test.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.False;
            this.img_Test.Size = new System.Drawing.Size(874, 660);
            this.img_Test.TabIndex = 0;
            this.img_Test.ToolTip = "Click Ctrl and Use the mouse wheel";
            this.img_Test.EditValueChanged += new System.EventHandler(this.img_Test_EditValueChanged);
            this.img_Test.MouseDown += new System.Windows.Forms.MouseEventHandler(this.img_Test_Click);
            // 
            // splashScreenManager1
            // 
            this.splashScreenManager1.ClosingDelay = 500;
            // 
            // frm_Image
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 730);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_SaveImage);
            this.Controls.Add(this.pnl_Uploading);
            this.Controls.Add(this.pnl_ImgControls);
            this.Controls.Add(this.pnl_Zoom);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.img_Test);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frm_Image";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image";
            this.Shown += new System.EventHandler(this.frm_Image_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Zoom)).EndInit();
            this.pnl_Zoom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_ImgControls)).EndInit();
            this.pnl_ImgControls.ResumeLayout(false);
            this.pnl_ImgControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Uploading)).EndInit();
            this.pnl_Uploading.ResumeLayout(false);
            this.pnl_Uploading.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img_Test.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit img_Test;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.PanelControl pnl_Zoom;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.PanelControl pnl_ImgControls;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl pnl_Uploading;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
        private DevExpress.XtraEditors.SimpleButton btn_SaveImage;
        private DevExpress.XtraEditors.SimpleButton btn_Cancel;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}