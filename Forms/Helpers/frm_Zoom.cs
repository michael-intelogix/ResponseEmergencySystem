using DevExpress.XtraEditors;
using ResponseEmergencySystem.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponseEmergencySystem.Forms.Helpers
{
    public partial class frm_Zoom : DevExpress.XtraEditors.XtraForm
    {
        public frm_Zoom()
        {
            InitializeComponent();
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if ((bool)ckedt_ShowHelper.EditValue) { 
                Settings.Default.ZoomMsg = false;
                Settings.Default.Save();
            }
            this.Close();
        }
    }
}