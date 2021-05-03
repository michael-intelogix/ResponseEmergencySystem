using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using ResponseEmergencySystem.Models;

namespace ResponseEmergencySystem.Reports
{
    public partial class IncidentReport : DevExpress.XtraReports.UI.XtraReport
    {
        public IncidentReport(Incident incident)
        {
            InitializeComponent();
            LoadData(incident);
        }

        private void LoadData(Incident incident)
        {
            lbl_Name.Text = incident.Name;
            lbl_License.Text = incident.driver.License;
            lbl_PhoneNumber.Text = incident.PhoneNumber;
            lbl_IncidentDate.Text = incident.IncidentDate.ToString("MM/dd/yyyy");
            lbl_IncidentTime.Text = incident.IncidentDate.ToString("hh:mm tt");
            lbl_LocationReferences.Text = incident.LocationReferences;
            cklbl_PoliceReport1.Checked = incident.PoliceReport == true ? true : false;
            cklbl_PoliceReport0.Checked = incident.PoliceReport == false ? true : false;
            lbl_PoliceReportNo.Text = incident.CitationReportNumber;
            lbl_TruckNo.Text = incident.truck.truckNumber;
            cklbl_TruckDamages1.Checked = incident.TruckDamage == true ? true : false;
            cklbl_TruckDamages0.Checked = incident.TruckDamage == false ? true : false;
            lbl_TrailerNo.Text = incident.trailer.TrailerNumber;
            cklbl_TrailerDamages1.Checked = incident.TrailerDamage == true ? true : false;
            cklbl_TrailerDamages0.Checked = incident.TrailerDamage == false ? true : false;
            lbl_Cargo.Text = incident.trailer.Commodity;
            cklbl_Spill1.Checked = incident.trailer.CargoSpill == true ? true : false;
            cklbl_Spill0.Checked = incident.trailer.CargoSpill == false ? true : false;
            lbl_BOL.Text = incident.ManifestNumber;
        }

    }
}
