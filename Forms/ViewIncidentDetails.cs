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
            edt_FullName.EditValue = incident.Name;
            edt_PhoneNumber.EditValue = incident.PhoneNumber;
            edt_License.EditValue = incident.driver.License;
            dte_ExpirationDate.EditValue = incident.driver.ExpirationDate;
            lue_DriverLicenceState.EditValue = incident.driver.ID_StateOfExpedition;
            edt_TruckNumber.EditValue = incident.truck.truckNumber;
            edt_TrailerNumber.EditValue = incident.trailer.TrailerNumber;
            ckedt_truckDamages.Checked = incident.TruckDamage;
            ckedt_TruckCanMove.Checked = incident.TruckCanMove;
            ckedt_TruckNeedCrane.Checked = incident.TruckNeedCrane;
            ckedt_TrailerDamages.Checked = incident.TruckDamage;
            ckedt_TrailerCanMove.Checked = incident.TruckCanMove;
            ckedt_TrailerNeedCrane.Checked = incident.TrailerNeedCrane;
            ckedt_Spill.Checked = incident.trailer.CargoSpill;
            edt_Cargo.EditValue = incident.trailer.Commodity;

            #region Accident Details
            dte_IncidentDate.EditValue = incident.IncidentDate.Date;
            tme_Incident.EditValue = incident.IncidentDate.ToString("hh:mm:ss tt");
            ckedt_PoliceReport.EditValue = incident.PoliceReport;
            edt_PoliceReport.EditValue = incident.CitationReportNumber;
            edt_Latitude.EditValue = incident.IncidentLatitude;
            edt_Longitude.EditValue = incident.IncidentLongitude;
            edt_Highway.EditValue = incident.LocationReferences;
            #endregion
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
            gc_InjuredPersons.DataSource = dt_InjuredPersons;
        }

        public string FullName
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        public string PhoneNumber
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        public string License
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        public string ExpirationDate
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        public string LicenseState
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        public string LocationReferences
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        public string TruckNumber
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        public bool TruckDamages
        {
            get { return (bool)ckedt_PoliceReport.EditValue; }
            set { ckedt_PoliceReport.EditValue = value; }
        }

        public bool TruckCanMove
        {
            get { return (bool)ckedt_PoliceReport.EditValue; }
            set { ckedt_PoliceReport.EditValue = value; }
        }

        public bool TruckNeedCrane 
        {
            get { return (bool)ckedt_PoliceReport.EditValue; }
            set { ckedt_PoliceReport.EditValue = value; }
        }

        public string TrailerNumber 
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        public bool TrailerDamages 
        {
            get { return (bool)ckedt_PoliceReport.EditValue; }
            set { ckedt_PoliceReport.EditValue = value; }
        }

        public bool TrailerCanMove 
        {
            get { return (bool)ckedt_PoliceReport.EditValue; }
            set { ckedt_PoliceReport.EditValue = value; }
        }

        public bool TrailerNeedCrane 
        {
            get { return (bool)ckedt_PoliceReport.EditValue; }
            set { ckedt_PoliceReport.EditValue = value; }
        }

        public string CargoType 
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        public bool CargoSpill 
        {
            get { return (bool)ckedt_PoliceReport.EditValue; }
            set { ckedt_PoliceReport.EditValue = value; }
        }

        public string ManifestNumber 
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        public string Broker 
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        public DateTime IncidentDate { get; set; }

        public bool PoliceReport 
        {
            get { return (bool)ckedt_PoliceReport.EditValue; }
            set { ckedt_PoliceReport.EditValue = value; }
        }

        public string CitationReportNumber 
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        public string Latitude 
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        public string Longitude 
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        public string ID_State 
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        public string ID_City 
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        private void ViewIncidentDetails_Load(object sender, EventArgs e)
        {
            lue_DriverLicenceState.Properties.DataSource = Functions.getStates();
        }
    }

}