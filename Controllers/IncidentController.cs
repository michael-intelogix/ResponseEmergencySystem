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
using ResponseEmergencySystem.Forms;

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
            _view.Broker2 = _selectedIncident.TrailerBroker.Name;
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

            _view.Documents = CaptureService.ListDocumentsCapture(_selectedIncident.ID_Incident);
            _view.LoadIncident();

        }

        public void SendEmail()
        {
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

        }

        public void PDF()
        {
            IncidentReport report1 = new IncidentReport(_selectedIncident);
            try
            {
                report1.ExportToPdf(ReportPath + $"\\{Folio}.pdf");
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

        public double[] GetTruckSamsara()
        {
            double latitude = 0;
            double longitude = 0;
            const string url = "https://api.samsara.com/fleet/vehicles/locations";
            string number = _view.TruckNumber;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);

                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "samsara_api_XwURzQhn0F9rijd0vqXwDgWir2zLWc");

                    // List data response.
                    HttpResponseMessage response = client.GetAsync(url).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.}

                    var data = JArray.Parse(
                        JObject.Parse(
                            response.Content.ReadAsStringAsync().Result
                        )["data"].ToString()
                    );

                    List<Vehicle> locs = data.Select(p => new Vehicle
                    {
                        name = p["name"].ToString().Trim(),
                        time = (DateTime)p["location"]["time"],
                        latitude = (float)p["location"]["latitude"],
                        longitude = (float)p["location"]["longitude"],
                        heading = (int)p["location"]["heading"],
                        speed = (int)p["location"]["speed"],
                        formattedLocation = (string)p["location"]["reverseGeo"]["formattedLocation"]
                    }).ToList();

                    var filtered = locs.Where(x => x.name == number);

                    foreach (var item in filtered)
                    {
                        latitude = (double)item.latitude;
                        longitude = (double)item.longitude;
                        _view.Latitude = item.latitude.ToString();
                        _view.Longitude = item.longitude.ToString();

                        //Testing samsara = new Testing(item.name, item.time, item.latitude, item.longitude, item.heading, item.speed, item.formattedLocation);
                        //samsara.Show();

                    }


                    //Dispose once all HttpClient calls are complete.This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
                    client.Dispose();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return new double[] { latitude, longitude };
        }


        #region documents
        public string EditImageView(string imgPath, string fileType, bool firebase = true)
        {
            if (fileType == "img")
            {
                //Utils.ShowMessage(imgPath);
                frm_Image imageView = new frm_Image("", "", imgPath, firebase);
                ImageController appConfigCtrl = new ImageController(imageView);
                appConfigCtrl.DisableImageLoad();
                if (imageView.ShowDialog() == DialogResult.OK)
                {
                    Utils.ShowMessage("Image has been updated");
                    return imageView.filepath;
                }
            }

            if (fileType == "pdf")
            {
                frm_PdfViewer pdfViewer = new frm_PdfViewer(imgPath, firebase);
                pdfViewer.ShowDialog();
            }

            return "";

        }
        #endregion

    }
}
