using DevExpress.XtraEditors;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Controllers;
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
    public partial class EditComments : DevExpress.XtraEditors.XtraForm, IEditImageData
    {
        public EditComments()
        {
            InitializeComponent();
        }

        EditImageDataController _controller;

        #region interface methods
        public void SetController(EditImageDataController controller)
        {
            _controller = controller;
        }

        public void CloseForm()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region interface properties
        public string Comments
        {
            get { return memoEdit1.EditValue == null ? "" : memoEdit1.EditValue.ToString(); }
        }

        public object StatusDetail
        {
            get { return lue_StatusDetail.EditValue; }
        }

        public object StatusDetailDataSource
        {
            set { lue_StatusDetail.Properties.DataSource = value; }
        }
        #endregion

        private void EditComments_Load(object sender, EventArgs e)
        {
            memoEdit1.EditValue = _controller.comments;
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            _controller.UpdateData();
        }

        private void btn_Cancel2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}