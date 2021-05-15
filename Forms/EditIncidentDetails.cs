using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Models;
using ResponseEmergencySystem.Views;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace ResponseEmergencySystem.Forms
{
    public partial class EditIncidentDetails : DevExpress.XtraEditors.XtraForm, IEditIncidentView
    {
        public EditIncidentDetails()
        {
            InitializeComponent();
        }

        Controllers.Incidents.EditIncidentController _controller;

        public void OnStateEditValueChanged(object sender, EventArgs e)
        {
            _controller.GetCitiesByState();
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
                case "ckedt_IPDriver":
                    pnl_DriverInvolved.Visible = ckedtValue;
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

        public void SetController(Controllers.Incidents.EditIncidentController controller)
        {
            _controller = controller;
        }

        private void FindTruckSamsara_Click(object sender, EventArgs e)
        {
            if (Utils.GetEdtValue(edt_TruckNumber) == "" || lbl_TruckExists.Visible)
            {
                gMapControl1.Position = new GMap.NET.PointLatLng(36.05948, -102.51325);
                Utils.ShowMessage("There is no truck to find, Please check\n the information again", "Samsara Error");
            }
            else
            {
                splashScreenManager1.ShowWaitForm();
                var res = _controller.GetTruckSamsara();
                gMapControl1.Position = new GMap.NET.PointLatLng(res[0], res[1]);
                splashScreenManager1.CloseWaitForm();
            }
        }

        public void LoadIncident(Incident incident)
        {

        }

        public void LoadStates(DataTable dt_States)
        {
            lue_states.Properties.DataSource = dt_States;
            lue_DriverLicenseState.Properties.DataSource = dt_States;
        }

        public void LoadInjuredPersons(DataTable dt_InjuredPersons)
        {
            gc_InvolvedPersons.DataSource = dt_InjuredPersons;
        }

        #region view fields
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
            set
            {
                dte_IncidentDate.DateTime = value;
                tme_IncidentTime.Time = value;
            }
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

        public string Comments
        {
            get { return memoEdit1.EditValue == null ? "" : memoEdit1.EditValue.ToString(); }
            set { memoEdit1.EditValue = value; }
        }
        

    #endregion

        #region view involved persons

        public string IPFullName 
        {
            get { return Utils.GetEdtValue(edt_IPFullName); } 
            set { edt_IPFullName.EditValue = value; }
        }
        public string IPLastName1 
        { 
            get { return Utils.GetEdtValue(edt_IPLastName1); }
            set { edt_IPLastName1.EditValue = value; }
        }
        public string IPPhoneNumber 
        {
            get { return Utils.GetEdtValue(edt_IPPhoneNumber); } 
            set { edt_IPPhoneNumber.EditValue = value; }
        }
        public string IPAge         
        {
            get { return Utils.GetEdtValue(edt_IPAge); }
            set { edt_IPAge.EditValue = value; }
        }
        public bool IPPrivate 
        {
            get { return (bool)ckedt_IPPrivate.EditValue; }
            set { ckedt_IPPrivate.EditValue = value; }
        }
        public bool IPInjured 
        {
            get { return (bool)ckedt_IPInjured.EditValue; }
            set { ckedt_IPInjured.EditValue = value; }
        }
        public bool IPPassenger 
        {
            get { return (bool)ckedt_IPPassenger.EditValue; }
            set { ckedt_IPPassenger.EditValue = value; }
        }
        public bool IPDriver 
        {
            get { return (bool)ckedt_IPDriver.EditValue; }
            set { ckedt_IPDriver.EditValue = value; }
        }
        public string IPLicense 
        {
            get { return Utils.GetEdtValue(edt_IPLicense); }
            set { edt_IPLicense.EditValue = value; }
        }

        #endregion

        #region view properties
        public bool LblTruckExistsVisibility 
        { 
            set { lbl_TruckExists.Visible = value; } 
        }

        public bool LblTrailerExistsVisibility { 
            set { lbl_TrailerExists.Visible = value; }
        }

        public object LueCitiesDataSource
        {
            set { lue_Cities.Properties.DataSource = value; }
        }

        public object InvolvedPersonsDataSorurce
        {
            set { gc_InvolvedPersons.DataSource = value; }
        }

        public bool PnlDriverInvolvedVisibility
        {
            set { pnl_DriverInvolved.Visible = value; }
        }

        public string BtnAddInvolvedPersonText 
        {
            set { simpleButton5.Text = value; } 
        }

        public Point BtnAddInvolvedPersonLocation 
        {
            set { simpleButton5.Location = value; }
        }

        public Size BtnAddInvolvedPersonSize 
        { 
            set { simpleButton5.Size = value; } 
        }

        public bool BtnAddInvolvedPersonVisibility
        {
            set { simpleButton5.Visible = value; }
        }

        public Point BtnEditInvolvedPersonLocation
        {
            set { simpleButton6.Location = value; }
            get { return simpleButton6.Location; }
        }

        public bool BtnEditInvolvedPersonVisibility
        {
            set { simpleButton6.Visible = value; }
        }

        public bool LblEmptyFieldsVisibility
        {
            set { lbl_EmptyFields.Visible = value; }
        }

        public BorderStyles EdtFullNameBorder
        {
            get { return edt_IPFullName.BorderStyle; }
            set { edt_IPFullName.BorderStyle = value; }
        }

        public BorderStyles EdtLastNameBorder
        {
            get { return edt_IPLastName1.BorderStyle; }
            set { edt_IPLastName1.BorderStyle = value; }
        }

        public BorderStyles EdtPhoneNumberBorder
        {
            get { return edt_IPPhoneNumber.BorderStyle; }
            set { edt_IPPhoneNumber.BorderStyle = value; }
        }

        public BorderStyles EdtAgeBorder
        {
            get { return edt_IPAge.BorderStyle; }
            set { edt_IPAge.BorderStyle = value; }
        }
        #endregion

        #region events needed
        public void checkNumber_OnEdtKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                TextEdit edt_Number = (TextEdit)sender;
                _controller.CheckNumber(edt_Number.Name);
            }
        }

        public void checkNumber_OnEdtLeave(object sender, EventArgs e)
        {
            TextEdit edt_Number = (TextEdit)sender;
            _controller.CheckNumber(edt_Number.Name);
        }
        #endregion

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

        private void EditIncidentDetails_Load(object sender, EventArgs e)
        {
            try
            {
                gMapControl1.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
                GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
                gMapControl1.Position = new GMap.NET.PointLatLng(_controller.latitude, _controller.longitude);
                gMapControl1.ShowCenter = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btn_EditPersonOnClick(object sender, EventArgs e)
        {
            _controller.EditInvolvedPersonByRow(gv_InvolvedPersons.FocusedRowHandle);
        }

        private void btn_RemovePersonOnClick(object sender, EventArgs e)
        {
            _controller.RemoveInvolvedPersonByRow(gv_InvolvedPersons.FocusedRowHandle);
            gv_InvolvedPersons.BestFitColumns();
        }

        private void ckedt_IPPrivate_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            _controller.AddPersonInvolved();
            gv_InvolvedPersons.BestFitColumns();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            _controller.UpdatePersonInvolved();
            gv_InvolvedPersons.BestFitColumns();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_UpdateIncident_Click(object sender, EventArgs e)
        {
            if (dxValidationProvider1.Validate())
            {
                splashScreenManager1.ShowWaitForm();
                _controller.Update();
                splashScreenManager1.CloseWaitForm();

                this.DialogResult = DialogResult.OK;
            }
            else
                Utils.ShowMessage("Please Check the information again", "Validation Error");
        }
    }
}