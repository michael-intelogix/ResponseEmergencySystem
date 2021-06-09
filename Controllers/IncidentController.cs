using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResponseEmergencySystem.Models;
using ResponseEmergencySystem.Views;
using ResponseEmergencySystem.Services;
using ResponseEmergencySystem.Samsara_Models;
using System.Data;
using ResponseEmergencySystem.Code;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using ResponseEmergencySystem.Forms.Modals;
using ResponseEmergencySystem.Reports;
using System.IO;
using ResponseEmergencySystem.Properties;
using Settings = ResponseEmergencySystem.Properties.Settings;

namespace ResponseEmergencySystem.Controllers
{
    public class IncidentController
    {
        IShowIncidentDetails _view;
        private string ID_Incident;
        private string Folio;
        private string ReportPath;
        Incident _selectedIncident;
        DataTable dt_InjuredPersons = new DataTable();

        private List<PersonsInvolved> _PersonsInvolved;
        private List<MailDirectory> _MailDIrectory;

        public double latitude;
        public double longitude;

        public IncidentController(IShowIncidentDetails view, string incidentId, string folio)
        {
            ID_Incident = incidentId;
            Folio = folio;
            ReportPath = Settings.Default.AppFolder;

            _view = view;
            view.SetController(this);
        }

        public Incident Incident
        {
            get { return _selectedIncident; }
        }

        //public void SendMail()
        //{
        //    var namefile = Utils.GetRowID(gv_Incidents, "Folio");
        //    string ReportPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"{namefile}.pdf");
        //    splashScreenManager1.ShowWaitForm();
        //    bool emailResponse = Utils.email_send(ReportPath, false);
        //    splashScreenManager1.CloseWaitForm();
        //    if (emailResponse)
        //    {
        //        MessageBox.Show("Mail Sent");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Mail Error");
        //    }
        //}

        public void LoadIncident()
        {
            _selectedIncident = IncidentService.GetIncident(ID_Incident)[0];
            _PersonsInvolved = IncidentService.list_PersonsInvolved(ID_Incident);
            _view.MailDirectoryCategoriesDataSource = MailDirectoryService.GetCategories();

            _view.FullName = _selectedIncident.Name;
            _view.PhoneNumber = _selectedIncident.PhoneNumber;
            _view.License = _selectedIncident.driver.License;
            _view.ExpirationDate = Convert.ToDateTime(_selectedIncident.driver.ExpirationDate).Date;
            _view.LicenseState = _selectedIncident.driver.ID_StateOfExpedition;
            _view.TruckNumber = _selectedIncident.truck.truckNumber;
            _view.TrailerNumber = _selectedIncident.trailer.TrailerNumber;
            _view.TruckDamages = _selectedIncident.TruckDamage;
            _view.TruckCanMove = _selectedIncident.TruckCanMove;
            _view.TruckNeedCrane = _selectedIncident.TruckNeedCrane;
            _view.TrailerDamages = _selectedIncident.TrailerDamage;
            _view.TrailerCanMove = _selectedIncident.TrailerCanMove;
            _view.TrailerNeedCrane = _selectedIncident.TrailerNeedCrane;
            _view.CargoSpill = _selectedIncident.trailer.CargoSpill;
            _view.ManifestNumber = _selectedIncident.ManifestNumber;
            _view.CargoType = _selectedIncident.trailer.Commodity;

            _view.ID_State = _selectedIncident.ID_State;
            _view.ID_City = _selectedIncident.ID_City;

            _view.Broker = _selectedIncident.broker.Name;
            _view.Comments = _selectedIncident.Comments;

            #region Accident Details
            _view.IncidentDate = _selectedIncident.IncidentDate.ToString("MM/dd/yyyy");
            _view.IncidentTime = _selectedIncident.IncidentDate.ToString("hh:mm:ss tt");
            _view.PoliceReport = _selectedIncident.PoliceReport;
            _view.CitationReportNumber = _selectedIncident.CitationReportNumber;
            _view.Latitude = _selectedIncident.IncidentLatitude;
            _view.Longitude = _selectedIncident.IncidentLongitude;
            _view.LocationReferences = _selectedIncident.LocationReferences;
            #endregion

            latitude = Convert.ToDouble(_selectedIncident.IncidentLatitude);
            longitude = Convert.ToDouble(_selectedIncident.IncidentLongitude);

            _view.LoadStates(Functions.getStates());
            if (_PersonsInvolved.Count > 0)
                _view.InvolvedPersonsDataSorurce = _PersonsInvolved;

        }

        public void SendEmail()
        {
            //var namefile = Utils.GetRowID(gv_Incidents, "Folio");

            if (!File.Exists(ReportPath + $"{Folio}.pdf"))
            {
                PDF();
            }

            if (!_view.SendToAllRecipientsInTheCategory)
            {
                Utils.email_send(ReportPath + $"\\{Folio}.pdf", false, mailAddress: _view.SelectedMail);
            }
            
            if (_view.SendToAllRecipientsInTheCategory)
            {
                Utils.email_send(ReportPath + $"\\{Folio}.pdf", false, categoryID: _view.MailDirectoryCategory);
            }

            //using (var ofd = new OpenFileDialog())
            //{
            //    ofd.Filter = "PDF Files (*.PDF)|*.PDF";
            //    ofd.ShowDialog();

            //    string ext = Path.GetExtension(ofd.FileName).ToUpper();
            //    try
            //    {
            //        if (ext == ".PDF")
            //        {
            //            ReportPath = ofd.FileName;
            //            PDF = true;
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}

            //if (PDF)
            //{
            //    //string ReportPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"{Folio}.pdf");
            //    return Utils.email_send(ReportPath, false);
            //}

            //return false;

        }

        public void PDF()
        {
            IncidentReport report1 = new IncidentReport(_selectedIncident);
            DevExpress.XtraPrinting.PdfExportOptions MyPdfOptions = new DevExpress.XtraPrinting.PdfExportOptions();
            try
            {
                report1.ExportToPdf(ReportPath + $"\\{Folio}.pdf");
                //MessageBox.Show("Report " + $"{Folio}.pdf");
                Utils.ShowMessage("Report " + $"{Folio}.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        public void GetCitiesByState()
        {
            var cities = GeneralService.list_Cities(_view.ID_State);
            _view.LueCitiesDataSource = cities;
        }

        public void GetMailsByCategory()
        {
            _MailDIrectory = MailDirectoryService.GetMailDirectory(_view.MailDirectoryCategory);
            _view.MailDirectoryDataSource = _MailDIrectory;
        }

    }
}
