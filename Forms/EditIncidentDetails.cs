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
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Properties;
using ResponseEmergencySystem.Controllers;
using ResponseEmergencySystem.Forms.Modals;
using ResponseEmergencySystem.Services;
using ResponseEmergencySystem.Views;
using ResponseEmergencySystem.Models;

namespace ResponseEmergencySystem.Forms
{
    public partial class EditIncidentDetails : DevExpress.XtraEditors.XtraForm, IEditIncidentView
    {
        public EditIncidentDetails()
        {
            InitializeComponent();
        }

        Controllers.Incidents.EditIncidentController _controller;

        private void lue_States_Properties_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void OnChangedCheckEdit(object sender, EventArgs e)
        {
            CheckEdit cb = (CheckEdit)sender;
            bool ckedtValue = (bool)cb.EditValue;

            switch (cb.Name)
            {
                case "ckedt_Spill":
                    pnl_BOL.Visible = ckedtValue;
                    break;
                case "ckedt_PoliceReport":
                    pnl_PoliceReport.Visible = ckedtValue;
                    break;
                //case "ckedt_Injured":
                //    panelControl3.Visible = ckedtValue;
                //    pnl_AddInjuredFields.Visible = ckedtValue;
                //    gc_InjuredPersons.Enabled = ckedtValue;

                //    if (dt_InjuredPersons.Rows.Count == 0)
                //        addEmptyRow();

                //    break;
            }

        }

        private void btn_SelectBroker_Click(object sender, EventArgs e)
        {
            _controller.GetBroker();
        }

        private void btn_AddIncident_Click(object sender, EventArgs e)
        {
            _controller.Update();
        }

        private void btn_LookUpLicence_Click(object sender, EventArgs e)
        {
            _controller.GetDriver();
        }

        private void btn_LookUpPhoneNumber_Click(object sender, EventArgs e)
        {
            _controller.GetDriver();
        }

        private void btn_LookUpName_Click(object sender, EventArgs e)
        {
            _controller.GetDriver();
        }

        public void SetController(Controllers.Incidents.EditIncidentController controller)
        {
            _controller = controller;
        }

        private void FindTruckSamsara_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            _controller.GetTruckSamsara();
            splashScreenManager1.CloseWaitForm();
        }

        public void LoadIncident(Incident incident)
        {

        }
        public void LoadStates(DataTable dt_States)
        {
            lue_states.Properties.DataSource = dt_States;
            lue_DriverLicenseState.Properties.DataSource = dt_States;
        }
        public void LoadCities(DataTable dt_Cities)
        {

        }
        public void LoadInjuredPersons(DataTable dt_InjuredPersons)
        {
            gc_InvolvedPersons.DataSource = dt_InjuredPersons;
        }

        public string DriverSearch
        {
            get { return Utils.GetEdtValue(edt_DriverInfoSearch); }
        }

        public string FullName
        {
            get { return Utils.GetEdtValue(edt_FullName); }
            set { edt_FullName.EditValue = value; }
        }

        public string PhoneNumber
        {
            get { return Utils.GetEdtValue(edt_PhoneNumber); }
            set { edt_PhoneNumber.EditValue = value; }
        }

        public string License
        {
            get { return Utils.GetEdtValue(edt_License); }
            set { edt_License.EditValue = value; }
        }

        public DateTime ExpirationDate
        {
            get { return dte_ExpirationDate.DateTime; }
            set { dte_ExpirationDate.EditValue = value; }
        }

        public string LicenseState
        {
            get { return lue_DriverLicenseState.EditValue.ToString(); }
            set { lue_DriverLicenseState.EditValue = value; }
        }

        public string LocationReferences
        {
            get { return Utils.GetEdtValue(edt_Highway); }
            set { edt_Highway.EditValue = value; }
        }

        public string TruckNumber
        {
            get { return Utils.GetEdtValue(edt_TruckNumber); }
            set { edt_TruckNumber.EditValue = value; }
        }

        public bool TruckDamages
        {
            get { return (bool)ckedt_truckDamages.EditValue; }
            set { ckedt_truckDamages.EditValue = value; }
        }

        public bool TruckCanMove
        {
            get { return (bool)ckedt_TruckCanMove.EditValue; }
            set { ckedt_TruckCanMove.EditValue = value; }
        }

        public bool TruckNeedCrane
        {
            get { return (bool)ckedt_TruckNeedCrane.EditValue; }
            set { ckedt_TruckNeedCrane.EditValue = value; }
        }

        public string TrailerNumber
        {
            get { return Utils.GetEdtValue(edt_TrailerNumber); }
            set { edt_TrailerNumber.EditValue = value; }
        }

        public bool TrailerDamages
        {
            get { return (bool)ckedt_TrailerDamages.EditValue; }
            set { ckedt_TrailerDamages.EditValue = value; }
        }

        public bool TrailerCanMove
        {
            get { return (bool)ckedt_TrailerCanMove.EditValue; }
            set { ckedt_TrailerCanMove.EditValue = value; }
        }

        public bool TrailerNeedCrane
        {
            get { return (bool)ckedt_TrailerNeedCrane.EditValue; }
            set { ckedt_TrailerNeedCrane.EditValue = value; }
        }

        public string CargoType
        {
            get { return Utils.GetEdtValue(edt_Cargo); }
            set { edt_Cargo.EditValue = value; }
        }

        public bool CargoSpill
        {
            get { return (bool)ckedt_Spill.EditValue; }
            set { ckedt_Spill.EditValue = value; }
        }

        public string ManifestNumber
        {
            get { return Utils.GetEdtValue(edt_manifest); }
            set { edt_manifest.EditValue = value; }
        }

        public string Broker
        {
            get { return Utils.GetEdtValue(edt_Broker); }
            set { edt_Broker.EditValue = value; }
        }

        public DateTime IncidentDate
        {
            get { return new DateTime(dte_IncidentDate.DateTime.Ticks); }
        }

        //public DateTime IncidentTime
        //{
        //    get { return new DateTime(tme_IncidentTime.Time.Ticks); }
        //    set { }
        //}

        public bool PoliceReport
        {
            get { return (bool)ckedt_PoliceReport.EditValue; }
            set { ckedt_PoliceReport.EditValue = value; }
        }

        public string CitationReportNumber
        {
            get { return Utils.GetEdtValue(edt_PoliceReport); }
            set { edt_PoliceReport.EditValue = value; }
        }

        public string Latitude
        {
            get { return Utils.GetEdtValue(edt_Latitude); }
            set { edt_Latitude.EditValue = value; }
        }

        public string Longitude
        {
            get { return Utils.GetEdtValue(edt_Longitude); }
            set { edt_Longitude.EditValue = value; }
        }

        public string ID_State
        {
            get { return lue_states.EditValue.ToString(); }
            set { lue_states.EditValue = value; }
        }

        public string ID_City
        {
            get { return lue_Cities.EditValue.ToString(); }
            set { lue_Cities.EditValue = value; }
        }


        private void ViewIncidentDetails_Load(object sender, EventArgs e)
        {
            lue_DriverLicenseState.Properties.DataSource = Functions.getStates();
        }

        private void textEdit3_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void lue_DriverLicenseState_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btn_FindDriver_Click(object sender, EventArgs e)
        {
            _controller.GetDriver();
        }

        private void edt_DriverInfoSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                _controller.GetDriver();
            }
        }

        private void ckedt_Spill_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panelControl4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelControl13_Click(object sender, EventArgs e)
        {

        }

        private void edt_TruckNumber_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl14_Click(object sender, EventArgs e)
        {

        }

        private void labelControl27_Click(object sender, EventArgs e)
        {

        }

        private void lbl_TruckExists_Click(object sender, EventArgs e)
        {

        }

        private void edt_SearchDriver_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void labelControl25_Click(object sender, EventArgs e)
        {

        }

        private void panelControl5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}