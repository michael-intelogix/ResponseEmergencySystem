using DevExpress.XtraEditors;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Services;
using ResponseEmergencySystem.Models;
using ResponseEmergencySystem.Views;
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
using ResponseEmergencySystem.Controllers;
using GMap.NET.MapProviders;

namespace ResponseEmergencySystem.Forms
{
    // https://stackoverflow.com/questions/1774498/how-to-iterate-through-a-datatable
    public partial class ViewIncidentDetails : DevExpress.XtraEditors.XtraForm, IShowIncidentDetails
    {
        DataTable dt_InjuredPersons = new DataTable();

        public ViewIncidentDetails()
        {
            InitializeComponent();

        }

        IncidentController _controller;

        private void loadData(string incidentId)
        {

        }

        public void SetController(IncidentController controller)
        {
            _controller = controller;
        }
        public void LoadIncident(Incident incident)
        {

        }
        public void LoadStates(DataTable dt_States)
        {
            lue_DriverLicenceState.Properties.DataSource = dt_States;
        }
        public void LoadCities(DataTable dt_Cities)
        {

        }
        public void LoadInjuredPersons(DataTable dt_InjuredPersons)
        {
            //gc_InjuredPersons.DataSource = dt_InjuredPersons;
        }

        #region Form Properties
        public bool ShowMailButton { 
            set { simpleButton2.Visible = value;  }
        }

        public object LueCitiesDataSource
        {
            set { lue_LocationCities.Properties.DataSource = value; }
        }

        public object InvolvedPersonsDataSorurce
        {
            set { gc_InvolvedPersons.DataSource = value; }
        }

        public object MailDirectoryCategoriesDataSource
        {
            set { lue_MailDirectoryCategories.Properties.DataSource = value; }
        }

        public object MailDirectoryDataSource
        {
            set { lue_MailDirectory.Properties.DataSource = value; }
        }

        #endregion

        #region view inputs
        public string SelectedMail
        {
            get { return lue_MailDirectory.EditValue == null ? "" : lue_MailDirectory.EditValue.ToString(); }
        }

        public string MailDirectoryCategory
        {
            get { return lue_MailDirectoryCategories.EditValue == null ? "" : lue_MailDirectoryCategories.EditValue.ToString(); }
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

        public string ExpirationDate
        {
            get { return dte_ExpirationDate.DateTime.Date.ToString(); }
            set { dte_ExpirationDate.DateTime = Convert.ToDateTime(value).Date; }
        }

        public string LicenseState
        {
            get { return lue_DriverLicenceState.EditValue.ToString(); }
            set { lue_DriverLicenceState.EditValue = value; }
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
            get { return edt_TrailerNumber.EditValue.ToString(); }
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

        public string IncidentDate {
            get { return dte_IncidentDate.DateTime.Date.ToString(); }
            set { dte_IncidentDate.EditValue = value; } 
        }

        public string IncidentTime
        {
            get { return tme_Incident.Time.ToString(); }
            set { tme_Incident.EditValue = value; }
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
            get { return lue_LocationStates.EditValue.ToString(); }
            set { lue_LocationStates.EditValue = value; }
        }

        public string ID_City 
        {
            get { return lue_LocationCities.EditValue.ToString(); }
            set { lue_LocationCities.EditValue = value; }
        }

        public string Comments
        {
            set { memoEdit1.Text = value; }
        }
        #endregion

        private void ViewIncidentDetails_Load(object sender, EventArgs e)
        {
            lue_DriverLicenceState.Properties.DataSource = Functions.getStates();
            lue_LocationStates.Properties.DataSource = Functions.getStates();

            gMapControl1.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gMapControl1.Position = new GMap.NET.PointLatLng(_controller.latitude, _controller.longitude);
            gMapControl1.ShowCenter = false;
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            _controller.PDF();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            _controller.PDF();
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            _controller.SendEmail();
        }

        private void labelControl11_Click(object sender, EventArgs e)
        {

        }

        public void OnStateEditValueChanged(object sender, EventArgs e)
        {
            _controller.GetCitiesByState();
        }

        private void lue_MailDirectoryCategories_EditValueChanged(object sender, EventArgs e)
        {
            _controller.GetMailsByCategory();
        }
    }

}