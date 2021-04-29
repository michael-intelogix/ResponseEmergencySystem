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
using ResponseEmergencySystem.Views;
using ResponseEmergencySystem.Controllers;

namespace ResponseEmergencySystem.Forms.Modals
{
    public partial class frm_BrokerList : DevExpress.XtraEditors.XtraForm, IBrokerView
    {
        public Int32 dt_BrokerRowSelected { get; set; }
        private List<Broker> brokers = new List<Broker>();

        public frm_BrokerList()
        {
            InitializeComponent();
        }

        BrokerController _controller;

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

        #region interface
        public void SetController(BrokerController controller)
        {
            _controller = controller;
        }

        public void LoadBrokers(List<Broker> brokers)
        {
            gc_Brokers.DataSource = brokers;
        }
        #endregion
    }
}