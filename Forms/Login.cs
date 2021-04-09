using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponseEmergencySystem.Forms
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {

        object user;
        public DataTable myData = new DataTable();

        public Login()
        {
            InitializeComponent();
        }

        private void frm_login_Load(object sender, EventArgs e)
        {
            user = this.loginCtrl1.userloged;
        }

        private void frm_login_FormClosing(object sender, FormClosingEventArgs e)
        {
            myData = loginCtrl1.Access;

            DialogResult = DialogResult.OK;
        }
    }
}