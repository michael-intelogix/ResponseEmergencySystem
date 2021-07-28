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

namespace ResponseEmergencySystem.Forms.Modals
{
    public partial class IncidentSelection : DevExpress.XtraEditors.XtraForm
    {
        public bool IsTruck = false;
        public bool IsCar = false;

        public IncidentSelection()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.IsTruck = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.IsTruck = false;
            this.IsCar = true;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}