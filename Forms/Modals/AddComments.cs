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
    public partial class AddComments : DevExpress.XtraEditors.XtraForm
    {
        public string comments { get; set; }
        public AddComments()
        {
            InitializeComponent();
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            this.comments = memoEdit1.EditValue.ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}