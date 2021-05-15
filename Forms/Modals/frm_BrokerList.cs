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
using ResponseEmergencySystem.Code;

namespace ResponseEmergencySystem.Forms.Modals
{
    public partial class frm_BrokerList : DevExpress.XtraEditors.XtraForm, IBrokerView
    {
        public string broker { get; set; }
        public string ID { get; set; }

        private List<Broker> brokers = new List<Broker>();

        public frm_BrokerList()
        {
            InitializeComponent();
        }

        BrokerController _controller;

        private void btn_ApprovedBroker(object sender, EventArgs e)
        {
            //Int32 index = gridView1.FocusedRowHandle;

            //this.broker = _controller.GetBrokerByIndex(index).Name;
            //this.ID = _controller.GetBrokerByIndex(index).ID_Broker;
            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        private void btn_SaveOnClick(object sender, EventArgs e)
        {
            Int32 index = gridView1.FocusedRowHandle;

            this.broker = _controller.GetBrokerByIndex(index).Name;
            this.ID = _controller.GetBrokerByIndex(index).ID_Broker;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void lue_State_EditValueChanged(object sender, EventArgs e)
        {
            _controller.GetCities(lue_State.EditValue.ToString());
        }

        private void add_Click(object sender, EventArgs e)
        {
            gc_Brokers.DataSource = _controller.Save();
            //gc_Brokers.Update();
            gridView1.BestFitColumns();
        }

        private void CloseOnClick(object sender, EventArgs e)
        {
            this.Close();
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

        public string Broker
        {
            get { return Utils.GetEdtValue(edt_Broker); }
            set { edt_Broker.EditValue = value; }
        }

        public string State
        {
            get { return lue_State.EditValue.ToString(); }
            set { lue_State.EditValue = value; }
        }

        public string City
        {
            get { return lue_City.EditValue.ToString(); }
            set { lue_City.EditValue = value; }
        }

        public string Address
        {
            get { return Utils.GetEdtValue(edt_Address); }
            set { edt_Address.EditValue = value; }
        }

        public bool Private
        {
            get { return (bool)ckedt_Private.EditValue; }
            set { ckedt_Private.EditValue = value; }
        }

        public string StateName
        {
            get { return lue_State.Properties.DisplayMember; }
        }

        public string CityName
        {
            get { return lue_City.Properties.DisplayMember; }
        }
        #endregion


        #region form properties
        public object CitiesDataSource
        {
            set { lue_City.Properties.DataSource = value; }
        }

        public object StatesDataSource
        {
            set { lue_State.Properties.DataSource = value; }
        }
        #endregion
    }
}