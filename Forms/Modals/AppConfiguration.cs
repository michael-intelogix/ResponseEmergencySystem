using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Services;
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
    public partial class AppConfiguration : DevExpress.XtraEditors.XtraForm, IAppConfigView
    {
        private string AppPath = "";

        public AppConfiguration()
        {
            InitializeComponent();
        }

        Controllers.AppConfigController _controller;

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (xtraFolderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                AppPath = xtraFolderBrowserDialog1.SelectedPath;
                textEdit1.Text = AppPath.Replace("\\", "/");
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AppFolder = AppPath;
            Properties.Settings.Default.Save();
        }

        private void AppConfiguration_Load(object sender, EventArgs e)
        {
            AppPath = Properties.Settings.Default.AppFolder;
            textEdit1.Text = AppPath.Replace("\\", "/");
            lue_Drivers.Properties.DataSource = DriverService.List_SamsaraDrivers();
        }

        private void ckedt_NewCategory_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit cb = (CheckEdit)sender;
            pnl_Category.Visible = (bool)cb.EditValue;
        }

        private void btn_AddCategory_Click(object sender, EventArgs e)
        {
            _controller.validate("category", true);
            _controller.AddCategory();   
        }

        #region view interface methods
        public void SetController(Controllers.AppConfigController controller)
        {
            _controller = controller;
        }
        #endregion

        #region view inputs
        public string NewCategory
        {
            get { return Utils.GetEdtValue(edt_Category); }

        }

        public string Category
        {
            get { return lue_Categories.EditValue == null ? "" : lue_Categories.EditValue.ToString(); }
            set { lue_Categories.EditValue = value; }
        }

        public string Mail
        {
            get { return Utils.GetEdtValue(edt_Mail); }
            set { edt_Mail.EditValue = value; }
        }

        public object MailDirectoryDataSource
        {
            set { gc_MailDirectory.DataSource = value;  }
        }

        public object CategoriesDataSource
        {
            set { lue_Categories.Properties.DataSource = value; }
        }

        public object Categories2DataSource
        {
            set { lue_Categories2.DataSource = value; }
        }
        #endregion

        #region validation properties
        public bool CategoryWarningIcon
        {
            set { pic_AddDeparmentWarning.Visible = value; }
        }

        public BorderStyles EdtCategoryBorder
        {
            set { edt_Category.BorderStyle = value; }
        }

        public bool LueMailCategoryWarningIcon
        {
            set { pic_MailCategoryWarningIcon.Visible = value; }
        }

        public BorderStyles LueMailCategoryBorder
        {
            set { lue_Categories.BorderStyle = value; }
        }

        public bool EdtMailWarningIcon
        {
            set { pic_MailWarningIcon.Visible = value; }
        }

        public BorderStyles EdtMailBorder
        {
            set { edt_Mail.BorderStyle = value; }
        }
        #endregion

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            _controller.validate("mailCategory");
            _controller.validate("mail");
            _controller.AddMailToDirectory();
        }

        private void lue_Categories_EditValueChanged(object sender, EventArgs e)
        {
            _controller.validate("mailCategory");
        }

        private void edt_Category_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            if (xtraFolderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                AppPath = xtraFolderBrowserDialog1.SelectedPath;
                textEdit1.Text = AppPath.Replace("\\", "/");
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AppFolder = AppPath;
            Properties.Settings.Default.Save();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            Int32 index = gv_MailDirectory.FocusedRowHandle;

            _controller.DeleteMailFromDirectory(index);
            gv_MailDirectory.BestFitColumns();
        }

        private void edt_Category_Leave(object sender, EventArgs e)
        {
            _controller.validate("category");
        }

        private void edt_Category_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                _controller.validate("category");
            }
        }

        private void edt_Mail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                _controller.validate("mail");
            }
        }

        private void edt_Mail_Leave(object sender, EventArgs e)
        {
            _controller.validate("mail");
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            SamsaraService.UpdateDriverSamsara(lue_Drivers.EditValue == null ? "" : lue_Drivers.EditValue.ToString());
        }
    }
}