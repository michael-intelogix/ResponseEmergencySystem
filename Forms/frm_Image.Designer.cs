
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
            this.pnl_ImgControls = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btn_SaveImage = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.img_Test = new DevExpress.XtraEditors.PictureEdit();
            this.pnl_Uploading = new DevExpress.XtraEditors.PanelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.progressBarControl1 = new DevExpress.XtraEditors.ProgressBarControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Zoom)).BeginInit();
            this.pnl_Zoom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_ImgControls)).BeginInit();
            this.pnl_ImgControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_Test.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Uploading)).BeginInit();
            this.pnl_Uploading.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.simpleButton1.Location = new System.Drawing.Point(346, 529);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
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
            this.pnl_Zoom.Location = new System.Drawing.Point(3, 523);
            this.pnl_Zoom.Name = "pnl_Zoom";
            this.pnl_Zoom.Size = new System.Drawing.Size(62, 31);
            this.pnl_Zoom.TabIndex = 7;
            this.pnl_Zoom.Visible = false;
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
            this.pnl_ImgControls.Location = new System.Drawing.Point(707, 382);
            this.pnl_ImgControls.Name = "pnl_ImgControls";
            this.pnl_ImgControls.Size = new System.Drawing.Size(44, 123);
            this.pnl_ImgControls.TabIndex = 8;
            this.pnl_ImgControls.Visible = false;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl1.ImageOptions.Image = global::ResponseEmergencySystem.Properties.Resources.refreshallpivottable_32x32;
            this.labelControl1.Location = new System.Drawing.Point(7, 86);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(32, 32);
            this.labelControl1.TabIndex = 12;
            this.labelControl1.ToolTip = "Reset Image";
            this.labelControl1.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.labelControl1.Click += new System.EventHandler(this.labelControl1_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl2.ImageOptions.Image = global::ResponseEmergencySystem.Properties.Resources.zoomout_32x32;
            this.labelControl2.Location = new System.Drawing.Point(6, 42);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(32, 32);
            this.labelControl2.TabIndex = 10;
            this.labelControl2.ToolTip = "Zoom In (Right Click)";
            this.labelControl2.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.labelControl2.Click += new System.EventHandler(this.labelControl2_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelControl3.ImageOptions.Image = global::ResponseEmergencySystem.Properties.Resources.zoomin_32x32;
            this.labelControl3.Location = new System.Drawing.Point(6, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(32, 32);
            this.labelControl3.TabIndex = 11;
            this.labelControl3.ToolTip = "Zoom Out (Left Click) ";
            this.labelControl3.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.labelControl3.Click += new System.EventHandler(this.labelControl3_Click);
            // 
            // btn_SaveImage
            // 
            this.btn_SaveImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_SaveImage.ImageOptions.Image = global::ResponseEmergencySystem.Properties.Resources.save_32x321;
            this.btn_SaveImage.Location = new System.Drawing.Point(711, 21);
            this.btn_SaveImage.Name = "btn_SaveImage";
            this.btn_SaveImage.Size = new System.Drawing.Size(40, 42);
            this.btn_SaveImage.TabIndex = 13;
            this.btn_SaveImage.Visible = false;
            this.btn_SaveImage.Click += new System.EventHandler(this.btn_SaveImage_Click);
            // 
            // simpleButton5
            // 
            this.simpleButton5.ImageOptions.Image = global::ResponseEmergencySystem.Properties.Resources.zoomin_16x161;
            this.simpleButton5.Location = new System.Drawing.Point(3, 7);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(24, 22);
            this.simpleButton5.TabIndex = 1;
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // simpleButton4
            // 
            this.simpleButton4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton4.ImageOptions.Image = global::ResponseEmergencySystem.Properties.Resources.zoomout_16x16;
            this.simpleButton4.Location = new System.Drawing.Point(34, 7);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(24, 22);
            this.simpleButton4.TabIndex = 0;
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // img_Test
            // 
            this.img_Test.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.img_Test.Location = new System.Drawing.Point(12, 12);
            this.img_Test.Name = "img_Test";
            this.img_Test.Properties.AllowScrollViaMouseDrag = true;
            this.img_Test.Properties.AllowZoom = DevExpress.Utils.DefaultBoolean.True;
            this.img_Test.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.img_Test.Properties.ShowMenu = false;
            this.img_Test.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.False;
            this.img_Test.Size = new System.Drawing.Size(749, 505);
            this.img_Test.TabIndex = 0;
            this.img_Test.ToolTip = "Click Ctrl and Use the mouse wheel";
            this.img_Test.EditValueChanged += new System.EventHandler(this.img_Test_EditValueChanged);
            this.img_Test.MouseDown += new System.Windows.Forms.MouseEventHandler(this.img_Test_Click);
            // 
            // pnl_Uploading
            // 
            this.pnl_Uploading.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pnl_Uploading.Appearance.Options.UseBackColor = true;
            this.pnl_Uploading.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnl_Uploading.Controls.Add(this.labelControl4);
            this.pnl_Uploading.Controls.Add(this.progressBarControl1);
            this.pnl_Uploading.Location = new System.Drawing.Point(245, 244);
            this.pnl_Uploading.Name = "pnl_Uploading";
            this.pnl_Uploading.Size = new System.Drawing.Size(274, 31);
            this.pnl_Uploading.TabIndex = 14;
            this.pnl_Uploading.Visible = false;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(5, 6);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(83, 13);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "Uploading  Image";
            // 
            // progressBarControl1
            // 
            this.progressBarControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarControl1.Location = new System.Drawing.Point(100, 5);
            this.progressBarControl1.Name = "progressBarControl1";
            this.progressBarControl1.Size = new System.Drawing.Size(169, 18);
            this.progressBarControl1.TabIndex = 3;
            // 
            // frm_Image
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 558);
            this.Controls.Add(this.pnl_Uploading);
            this.Controls.Add(this.btn_SaveImage);
            this.Controls.Add(this.pnl_ImgControls);
            this.Controls.Add(this.pnl_Zoom);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.img_Test);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frm_Image";
            this.Text = "frm_Image";
            this.Shown += new System.EventHandler(this.frm_Image_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Zoom)).EndInit();
            this.pnl_Zoom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnl_ImgControls)).EndInit();
            this.pnl_ImgControls.ResumeLayout(false);
            this.pnl_ImgControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.img_Test.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnl_Uploading)).EndInit();
            this.pnl_Uploading.ResumeLayout(false);
            this.pnl_Uploading.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBarControl1.Properties)).EndInit();
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
        private DevExpress.XtraEditors.SimpleButton btn_SaveImage;
        private DevExpress.XtraEditors.PanelControl pnl_Uploading;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.ProgressBarControl progressBarControl1;
    }
}