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
    public partial class frm_DriverSearchList : DevExpress.XtraEditors.XtraForm
    {
        public Int32 dt_DriverRowSelected  { get; set; }
        private DataTable dt_Drivers = new DataTable();
        public frm_DriverSearchList(DataTable dt_Drivers)
        {
            this.dt_Drivers = dt_Drivers;
            InitializeComponent();
            loadDrivers();
        }

        private void loadDrivers()
        {
            gc_Drivers.DataSource = dt_Drivers;
        }

        private void btn_ApprovedDriver(object sender, EventArgs e)
        {
            Int32 index = gv_Drivers.FocusedRowHandle;

            this.dt_DriverRowSelected = index;
            this.DialogResult = DialogResult.OK;
            this.Close();

            //DialogResult result = MessageBox.Show(
            //    "Are you sure you want to delete this row?",
            //    "Delete injured person row",
            //    MessageBoxButtons.OKCancel,
            //    MessageBoxIcon.Information);
            //if (result.Equals(DialogResult.OK))
            //{
            //    gv_InjuredPersons.DeleteRow(index);
            //}
        }
        
    }
}