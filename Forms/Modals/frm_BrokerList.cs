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
using ResponseEmergencySystem.Models;

namespace ResponseEmergencySystem.Forms.Modals
{
    public partial class frm_BrokerList : DevExpress.XtraEditors.XtraForm
    {
        public Int32 dt_BrokerRowSelected { get; set; }
        private List<Broker> brokers = new List<Broker>();

        public frm_BrokerList(List<Broker> brokers)
        {
            this.brokers = brokers;
            InitializeComponent();
            loadBrokers();
        }

        private void loadBrokers()
        {
            gc_Brokers.DataSource = brokers;
        }

        private void btn_ApprovedDriver(object sender, EventArgs e)
        {
            Int32 index = gv_Brokers.FocusedRowHandle;

            this.dt_BrokerRowSelected = index;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frm_BrokerList_Load(object sender, EventArgs e)
        {

        }
    }
}