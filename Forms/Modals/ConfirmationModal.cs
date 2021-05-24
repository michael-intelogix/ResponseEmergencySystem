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
    public partial class ConfirmationModal : DevExpress.XtraEditors.XtraForm, IConfirmationModalView
    {
        public ConfirmationModal(String Message, string Title)
        {
            InitializeComponent();
            labelControl1.Text = Title;
            label1.Text = Message;
        }

        ConfirmationModalController _controller;

        private void ConfirmationModal_Load(object sender, EventArgs e)
        {

        }


        #region view interface methods
        public void SetController(ConfirmationModalController controller)
        {
            _controller = controller;
        }
        #endregion

        #region view inputs
        public string Category
        {
            get { return ""; }
        }
        #endregion

        #region set status icons
        public void SetErrorIcon()
        {
            pictureEdit2.SvgImage = Resources.cancelRed;
        }

        public void SetApprovedIcon()
        {
            pictureEdit2.SvgImage = Resources.checkGreen;
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}