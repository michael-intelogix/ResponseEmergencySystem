using DevExpress.XtraEditors;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Services;
using ResponseEmergencySystem.Models;
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

namespace ResponseEmergencySystem.Forms
{
    // https://stackoverflow.com/questions/1774498/how-to-iterate-through-a-datatable
    public partial class ViewIncidentDetails : DevExpress.XtraEditors.XtraForm, IShowIncidentDetails
    {
        DataTable dt_InjuredPersons = new DataTable();

        public ViewIncidentDetails(string incidentId)
        {
            InitializeComponent();

            addEmptyRow();
            loadData(incidentId);
            
        }

        private void loadData(string incidentId)
        {
       
            Incident result = IncidentService.list_Incidents("", "", "", "", "", incidentId: incidentId)[0];
            edt_FullName.EditValue = result.Name;
            edt_PhoneNumber.EditValue = result.PhoneNumber;
            edt_License.EditValue = result.driver.License;
            dte_ExpirationDate.EditValue = result.driver.ExpirationDate;
            lue_DriverLicenceState.EditValue = result.driver.ID_StateOfExpedition;
            edt_TruckNumber.EditValue = result.truck.truckNumber;
            edt_TrailerNumber.EditValue = result.trailer.TrailerNumber;
            ckedt_truckDamages.Checked = result.TruckDamage;
            ckedt_TruckCanMove.Checked = result.TruckCanMove;
            ckedt_TruckNeedCrane.Checked = result.TruckNeedCrane;
            ckedt_TrailerDamages.Checked = result.TruckDamage;
            ckedt_TrailerCanMove.Checked = result.TruckCanMove;
            ckedt_TrailerNeedCrane.Checked = result.TrailerNeedCrane;
            ckedt_Spill.Checked = result.trailer.CargoSpill;
            edt_Cargo.EditValue = result.trailer.Commodity;
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