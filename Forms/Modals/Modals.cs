using DevExpress.XtraEditors;
using ResponseEmergencySystem.Controllers;
using ResponseEmergencySystem.Properties;
using ResponseEmergencySystem.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponseEmergencySystem.Forms.Modals
{
    public partial class Modals : DevExpress.XtraEditors.XtraForm, IModalView
    {
        public Modals(String Message, string Title)
        {
            InitializeComponent();
            labelControl1.Text = Title;
            label1.Text = Message;
        }

        ModalController _controller;

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void stackPanel2_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region view interface methods
        public void SetController(ModalController controller)
        {
            _controller = controller;
        }
        #endregion

        #region view inputs
        public string Category
        {
            get { return ""; }
        }



        //public object MailDirectoryDataSource
        //{
        //    set { gc_MailDirectory.DataSource = value; }
        //}
        #endregion

        #region set status icons
        public void SetErrorIcon()
        {
            pictureEdit2.SvgImage = Resources.actions_deletecircled;
        }

        public void SetApprovedIcon()
        {
            pictureEdit2.SvgImage = Resources.actions_checkcircled;
        }

        public void SetWarningIcon()
        {
            pictureEdit2.SvgImage = Resources.warnigColors;
        }

        public void SetMailSentIcon()
        {
            pictureEdit2.SvgImage = Resources.emailSent;
        }
        #endregion
    }
}