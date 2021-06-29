
namespace ResponseEmergencySystem.Forms
{
    partial class frm_Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Login));
            this.loginCtrl1 = new Login.LoginCtrl();
            this.SuspendLayout();
            // 
            // loginCtrl1
            // 
            this.loginCtrl1.Access = null;
            this.loginCtrl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.loginCtrl1.Appearance.Options.UseBackColor = true;
            this.loginCtrl1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("loginCtrl1.BackgroundImage")));
            this.loginCtrl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.loginCtrl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginCtrl1.Location = new System.Drawing.Point(0, 0);
            this.loginCtrl1.Name = "loginCtrl1";
            this.loginCtrl1.Size = new System.Drawing.Size(457, 721);
            this.loginCtrl1.TabIndex = 0;
            this.loginCtrl1.userid = null;
            this.loginCtrl1.userloged = null;
            // 
            // frm_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 721);
            this.ControlBox = false;
            this.Controls.Add(this.loginCtrl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IconOptions.SvgImage = global::ResponseEmergencySystem.Properties.Resources.intelogixWhite;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_Login";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_Login_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private Login.LoginCtrl loginCtrl1;
    }
}