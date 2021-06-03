using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.IO;
using ResponseEmergencySystem.Models;
using DevExpress.XtraEditors.Controls;

using System.Data.SQLite;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace ResponseEmergencySystem.Forms
{
    
    public partial class AddIncidentDetails : XtraForm, IAddIncidentView
    {
        // driver E2E7FBBB-6BF8-414A-B160-1A4EE294DC97
        // driver2 C7B06EF3-869B-4212-A1EC-7820B2D17CA4
        
        private DataTable dt_InjuredPersons;

        public AddIncidentDetails()
        {
            InitializeComponent();
        }

        Controllers.Incidents.AddIncidentController _controller;

        private void btn_AddRowsClick(object sender, EventArgs e)
        {
            //Int16 currentRow = 0;
            //while (currentRow < Convert.ToInt16(edt_NumberOfInjured.EditValue))
            //{
            //    _controller.addEmptyRow();
            //    currentRow++;
            //}

        }

        private void btn_DeleteRowClick(object sender, EventArgs e)
        {
            //Int32 index = gv_InjuredPersons.FocusedRowHandle;

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

        private void IncidentCapture_Load(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gMapControl1.Position = new GMap.NET.PointLatLng(36.05948, -102.51325);
            gMapControl1.ShowCenter = false;

            //toolTipController1.ShowHint("HIIII");
        }

        private void btn_AddIncident_Click(object sender, EventArgs e)
        {

            if (dxValidationProvider1.Validate())
            {
                splashScreenManager1.ShowWaitForm();
                _controller.AddIncident();
                splashScreenManager1.CloseWaitForm();

                this.DialogResult = DialogResult.OK;
            }
            else
                Utils.ShowMessage("Please Check the information again", "Validation Error", type: "Error");

        }
       
        private void AddIncidentDetails_Shown(object sender, EventArgs e)
        { 
        }
        
        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            _controller.SetBroker();
        }


        #region view interface methods
        public void SetController(Controllers.Incidents.AddIncidentController controller)
        {
            _controller = controller;
        }

        public void LoadIncident(Incident incident)
        {
        }

        public void LoadStates(DataTable dt_States)
        {
            lue_StateExp.Properties.DataSource = dt_States;
            lue_DriverLicenseState.Properties.DataSource = dt_States;
        }


        public void LoadCities(DataTable dt_Cities)
        {
            lue_Cities.Properties.DataSource = dt_Cities;
        }

        public void LoadInjuredPersons(DataTable dt_InjuredPersons)
        {
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

        public void FindTruckSamsara_Click(object sender, EventArgs e)
        {
            if(Utils.GetEdtValue(edt_TruckNumber) == "" || lbl_TruckExists.Visible)
            {
                gMapControl1.Position = new GMap.NET.PointLatLng(36.05948, -102.51325);
                Utils.ShowMessage("There is no truck to find, Please check\n the information again", "Samsara Error", type: "Error");
            }
            else
            {
                splashScreenManager1.ShowWaitForm();
                var res = _controller.GetTruckSamsara();
                gMapControl1.Position = new GMap.NET.PointLatLng(res[0], res[1]);

                GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
                GMap.NET.WindowsForms.GMapMarker marker = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
                                                                new GMap.NET.PointLatLng(res[0], res[1]),
                GMap.NET.WindowsForms.Markers.GMarkerGoogleType.red_dot);
                gMapControl1.Overlays.Clear();
                markers.Markers.Add(marker);
                gMapControl1.Overlays.Add(markers);
                splashScreenManager1.CloseWaitForm();
            }
            
        }

        public void Ckedt_OnValueChanged(object sender, EventArgs e)
        {
            CheckEdit cb = (CheckEdit)sender;

            _controller.CheckEditChanged(cb.Name, (bool)cb.EditValue);
        }

        public void OnStateEditValueChanged(object sender, EventArgs e)
        {
            _controller.GetCitiesByState();
        }

        #endregion

        #region form inputs
        public string DriverInfoSearch
        {
            get { return Utils.GetEdtValue(edt_SearchDriver); }
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
            set 
            {
                if (value == DateTime.Now)
                    dte_ExpirationDate.EditValue = null;
                else
                    dte_ExpirationDate.EditValue = value; 
            }
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

        public DateTime IncidentDate {
            get 
            {
                DateTime date = Utils.GetDateTime(dte_IncidentDate.DateTime.Date, tme_IncidentTime.Time.Hour, tme_IncidentTime.Time.Minute);
                return date;
            } 
        }

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
            get { return lue_StateExp.EditValue.ToString(); }
            set { lue_StateExp.EditValue = value; }
        }

        public string ID_City
        {
            get { return lue_Cities.EditValue.ToString(); }
            set { lue_Cities.EditValue = value; }
        }

        public string Comments
        {
            get { return memoEdit1.EditValue == null ? "" : memoEdit1.EditValue.ToString(); }
        }
        #endregion

        #region involved persons 
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
        //not in use
        public string IPLastName2 
        {
            get { return Utils.GetEdtValue(edt_IPLastName1); }
        }
        public string IPAge 
        { 
            get { return Utils.GetEdtValue(edt_IPAge); }
            set { edt_IPAge.EditValue = value; }
        }
        public string IPPhoneNumber 
        { 
            get { return Utils.GetEdtValue(edt_IPPhoneNumber); }
            set { edt_IPPhoneNumber.EditValue = value; }
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
        public string IPDriverLicense { 
            get { return Utils.GetEdtValue(edt_IPLicense); }
            set { edt_IPLicense.EditValue = value; }
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
        #endregion

        #region Form properties
        public bool PnlBolVisibility
        {
            set { pnl_BOL.Visible = value; }
        }

        public bool PnlPoliceReportVisibility
        {
            set { pnl_PoliceReport.Visible = value; }
        }

        public bool LblTruckExistsVisibility
        {
            set { lbl_TruckExists.Visible = value; }
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

        public bool BtnAddInvolvedPersonVisibility
        {
            set { simpleButton5.Visible = value; }
        }

        public Point BtnAddInvolvedPersonLocation
        {
            set { simpleButton5.Location = value; }
        }

        public Size BtnAddInvolvedPersonSize
        {
            set { simpleButton5.Size = value; }
        }

        public bool BtnEditInvolvedPersonVisibility
        {
            set { simpleButton6.Visible = value; }
        }

        public Point BtnEditInvolvedPersonLocation
        {
            set { simpleButton6.Location = value; }
            get { return simpleButton6.Location; }
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

        public BorderStyles EdtLicenseBorder
        {
            get { return edt_IPLicense.BorderStyle; }
            set { edt_IPLicense.BorderStyle = value; }
        }

        public bool EdtFullNameShowWarningIcon
        {
            set { pic_FullNameWarning.Visible = value; }
            get { return pic_FullNameWarning.Visible; }
        }

        public bool EdtLastName1ShowWarningIcon
        {
            set { pic_LastName1Warning.Visible = value; }
            get { return pic_LastName1Warning.Visible; }
        }

        public bool EdtPhoneNumberShowWarningIcon
        {
            set { pic_PhoneNumberWarning.Visible = value; }
        }

        public bool EdtAgeShowWarningIcon
        {
            set { pic_AgeWarning.Visible = value; }
        }


        public bool EdtLicenseShowWarningIcon
        {
            set { pic_LicenseWarning.Visible = value; }
        }

        #endregion

        #region truck exists
        public bool LblTrailerExistsVisibility
        {
            set { lbl_TrailerExists.Visible = value; }
        }

        public bool BtnAddTrailerVisibility
        {
            set { btn_AddTrailer.Visible = value; }
        }

        public bool EdtCommodityReadOnly
        {
            set { edt_Cargo.ReadOnly = value; }
        }
        #endregion


        private void ViewIncidentDetails_Load(object sender, EventArgs e)
        {
            lue_StateExp.Properties.DataSource = Functions.getStates();
        }

        private void btn_AddComments_Click(object sender, EventArgs e)
        {
            _controller.SetComments();
        }

        private void edt_Broker_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btn_FindDriver_Click(object sender, EventArgs e)
        {
            _controller.GetDriver();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            _controller.SetBroker();
        }

        private void edt_SearchDriver_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                _controller.GetDriver();
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            _controller.AddPersonInvolved();
            gv_InvolvedPersons.BestFitColumns();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //Utils.ShowMessage("Are you sure you want to close?", "Incident");
            this.Close();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            _controller.UpdatePersonInvolved();
            gv_InvolvedPersons.BestFitColumns();
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

        private void simpleButton7_Click(object sender, EventArgs e)
        {

        }

        private void edt_Leave(object sender, EventArgs e)
        {
            TextEdit edt = (TextEdit)sender;

            _controller.validate(edt.Name);
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            //_controller.FindDriverInSamsara();
            SamsaraService.UpdateSamsaraDrivers();
        }
    }

}