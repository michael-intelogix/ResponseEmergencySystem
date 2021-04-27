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
    public partial class AddMoreCaptures : DevExpress.XtraEditors.XtraForm
    {
        public AddMoreCaptures()
        {
            InitializeComponent();
        }

        private void AddMoreCaptures_Load(object sender, EventArgs e)
        {

        }

        private void btn_Cancel2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}