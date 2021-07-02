using DevExpress.XtraEditors.Controls;
using Firebase.Storage;
using Newtonsoft.Json.Linq;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Forms;
using ResponseEmergencySystem.Forms.Modals;
using ResponseEmergencySystem.Models;
using ResponseEmergencySystem.Properties;
using ResponseEmergencySystem.Reports;
using ResponseEmergencySystem.Services;
using ResponseEmergencySystem.Views.Incidents;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponseEmergencySystem.Controllers.Incidents
{
    public class DriverIncidentController
    {
        IIncidentView _view;
        Incident _selectedIncident;
        Driver _selectedDriver;

        List<PersonsInvolved> _PersonsInvolved;

        private List<Driver> _DriversLocal = new List<Driver>();
        private List<Truck> _trucks = new List<Truck>();

        string ID_Incident = ""; 
        string Folio;
        string ReportPath;

        private string _ID_Samsara;
        private string _DriverName;
        private string ID_Driver;
        private string ID_Capture;

        private string ID_Broker;
        private string ID_Broker2;
        private string ID_Truck;
        private string ID_Trailer;

        public double latitude = 36.05948;
        public double longitude = -102.51325;

        private bool _validation;

        private int _selectedPerson;
        private bool _errors = false;

        private List<MailDirectory> _MailDIrectory;

        public DriverIncidentController(IIncidentView view, string incidentId, string folio)
        {
            ID_Incident = incidentId;
            Folio = folio;
            ReportPath = Settings.Default.AppFolder;

            _DriversLocal = DriverService.GetDriver("");
            _trucks = GeneralService.list_Trucks();
            

            if (incidentId == constants.emptyId.ToString())
            {
                _PersonsInvolved = new List<PersonsInvolved>();
            }
            else
            {
                _PersonsInvolved = IncidentService.list_PersonsInvolved(ID_Incident);
            }

            _view = view;
            view.SetController(this);

            _view.LoadStates(Functions.getStates("99F9B034-75BE-4615-88C6-8D64BC3549DC"));
            _view.DriversDataSource = _DriversLocal;
            _view.TrucksDataSource = _trucks;
            _view.MailDirectoryCategoriesDataSource = MailDirectoryService.GetCategories();
        }

        public void LoadIncident()
        {
            _selectedIncident = IncidentService.GetIncident(ID_Incident)[0];
            //_PersonsInvolved = IncidentService.list_PersonsInvolved(ID_Incident);
            //_view.MailDirectoryCategoriesDataSource = MailDirectoryService.GetCategories();

            ID_Truck = _selectedIncident.truck.ID_Truck.ToString();
            

            Folio = _selectedIncident.Folio;
            _view.Folio = Folio;

            ID_Driver = _selectedIncident.driver.ID_Driver.ToString(); ;
            ID_Broker = _selectedIncident.broker.ID_Broker;
            ID_Broker2 = _selectedIncident.TrailerBroker.ID_Broker;
            ID_Truck = _selectedIncident.truck.ID_Truck.ToString();
            ID_Trailer = _selectedIncident.trailer.ID_Trailer.ToString();
            _ID_Samsara = _selectedIncident.driver.ID_Samsara;

            _view.InvolvedPersonsDataSource = _PersonsInvolved;

            _DriverName = _selectedIncident.Name;
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
            _view.IncidentDate = _selectedIncident.IncidentDate;
            _view.PoliceReport = _selectedIncident.PoliceReport;
            _view.CitationReportNumber = _selectedIncident.CitationReportNumber;
            _view.Latitude = _selectedIncident.IncidentLatitude;
            _view.Longitude = _selectedIncident.IncidentLongitude;
            _view.LocationReferences = _selectedIncident.LocationReferences;
            #endregion

            latitude = Convert.ToDouble(_selectedIncident.IncidentLatitude);
            longitude = Convert.ToDouble(_selectedIncident.IncidentLongitude);

            //GetDocuments(_selectedIncident.ID_Incident);

            _view.TruckId = _selectedIncident.truck.ID_Samsara;
            //if (_PersonsInvolved.Count > 0)
            //    _view.InvolvedPersonsDataSorurce = _PersonsInvolved;

            //_view.Documents = CaptureService.ListDocumentsCapture(_selectedIncident.ID_Incident);
            //_view.LoadIncident();



        }

        public void GetCitiesByState()
        {
            var cities = GeneralService.list_Cities(_view.ID_State);
            _view.LueCitiesDataSource = cities;
        }

        public void GetDriver(string ID)
        {
            _selectedDriver = _DriversLocal.Where(d => d.ID_Samsara == ID).First();

            ID_Driver = _selectedDriver.ID_Driver.ToString();
            _ID_Samsara = _selectedDriver.ID_Samsara.ToString();
            _DriverName = _selectedDriver.Name + " " + _selectedDriver.LastName1;
            _view.FullName = _DriverName;
            _view.PhoneNumber = _selectedDriver.PhoneNumber;
            _view.License = _selectedDriver.License;

            if (_selectedDriver.ExpirationDate != null)
                _view.ExpirationDate = (DateTime)_selectedDriver.ExpirationDate;
            else
            {
                _view.ExpirationDate = DateTime.Now;
            }

            _view.LicenseState = _selectedDriver.ID_StateOfExpedition;
        }

        public void GetBroker()
        {
            frm_BrokerList brokerView = new frm_BrokerList();
            BrokerController brokerCtrl = new BrokerController(brokerView, BrokerService.list_Brokers());
            brokerCtrl.LoadBrokers();
            if (brokerView.ShowDialog() == DialogResult.OK)
            {
                _view.Broker = brokerView.broker;
                ID_Broker = brokerView.ID;
            }
        }

        public void GetBroker2()
        {
            frm_BrokerList brokerView = new frm_BrokerList();
            BrokerController brokerCtrl = new BrokerController(brokerView, BrokerService.list_Brokers());
            brokerCtrl.LoadBrokers();
            if (brokerView.ShowDialog() == DialogResult.OK)
            {
                _view.Broker2 = brokerView.broker;
                ID_Broker2 = brokerView.ID;
            }
        }
        public void Update()
        {
            //_mainView.OpenSpinner();
            //check location refreces
            if (_view.SendAfterSave)
            {
                if (_view.SelectedMail == "" && !_view.SendToAllRecipientsInTheCategory)
                {
                    _view.MailValidationBorder = BorderStyles.Simple;
                    Utils.ShowMessage("Please select a mail before submit the changes", "Mail Error", type: "Warning");
                    return;
                }

                if (_view.MailDirectoryCategory == "" && _view.SendToAllRecipientsInTheCategory)
                {
                    _view.CategoryValidationBorder = BorderStyles.Simple;
                    Utils.ShowMessage("Please select a category before submit the changes", "Mail Error", type: "Warning");
                    return;
                }
            }

            try
            {
                var t = new Task<Response>(() => IncidentService.UpdateIncident(
                    ID_Incident,
                    ID_Driver == Guid.Empty.ToString() ? _ID_Samsara : ID_Driver,
                    _DriverName,
                    _view.ID_State,
                    _view.ID_City,
                    ID_Broker.ToUpper(),
                    ID_Broker2,
                    ID_Truck,
                    _view.TruckNumber == null ? "" : _view.TruckNumber.ToString(),
                    _view.TrailerNumber,
                    _view.CargoType,
                    _view.IncidentDate,
                    _view.PoliceReport,
                    _view.CitationReportNumber,
                    _view.CargoSpill,
                    _view.ManifestNumber,
                    _view.LocationReferences,
                    _view.Latitude,
                    _view.Longitude,
                    _view.TruckDamages,
                    _view.TruckCanMove,
                    _view.TruckNeedCrane,
                    _view.TrailerDamages,
                    _view.TrailerCanMove,
                    _view.TrailerNeedCrane,
                    constants.userID,
                    _view.Comments == null ? "" : _view.Comments.ToString(),
                    ID_Driver == Guid.Empty.ToString()
                ));

                t.Start();
                t.Wait();

                //_mainView.CloseSpinner();

                foreach (var person in _PersonsInvolved)
                {
                    if (t.Result.validation)
                    {
                        person.ID_Incident = t.Result.ID;
                        IncidentService.AddPersonInvolved(person);
                    }
                    else
                    {
                        Debug.WriteLine(t.Result.Message);
                    }
                }

                if (t.Result.validation)
                {
                    foreach (var documentCapture in _view.Documents)
                    {

                        if (documentCapture.Status == "created")
                        {
                            Task tCapture = new Task(() => SaveCapture(documentCapture.ID_Capture, documentCapture.ID_CaptureType, t.Result.ID));
                            tCapture.Start();
                            tCapture.Wait();

                            foreach (var doc in documentCapture.documents)
                            {
                                if (doc.Status == "empty" || doc.Status == "loaded")
                                    continue;

                                var tDocument = new Task(() => CaptureService.AddImage(Guid.NewGuid().ToString(), documentCapture.ID_Capture, doc.FirebaseUrl, doc.name, "", doc.Type));
                                tDocument.Start();
                                tDocument.Wait();
                            }
                        }
                        else if (documentCapture.Status == "updated")
                        {
                            foreach (var doc in documentCapture.documents)
                            {
                                if (doc.Status == "empty" || doc.Status == "loaded" || doc.Status == "disposed")
                                    continue;

                                if (doc.Status == "deleted")
                                {
                                    var tDelete = new Task(() => CaptureService.DeleteImageCapture(doc.ID_Document));
                                    tDelete.Start();
                                    tDelete.Wait();
                                    continue;
                                }

                                var ID = doc.Status == "created" ? Guid.NewGuid().ToString() : doc.ID_Document;
                                var tDocument = new Task(() => CaptureService.AddImage(ID, documentCapture.ID_Capture, doc.FirebaseUrl, doc.name, "", doc.Type));
                                tDocument.Start();
                                tDocument.Wait();
                            }
                        }
                        //var t1 = new Task(() => CaptureService.AddImage(Guid.NewGuid().ToString(), ID_Capture, document.FirebaseUrl, document.name, "", document.Type););
                        //t1.Start();
                        //t1.Wait();
                        //if (t.Result.validation)
                        //{

                            //    Debug.WriteLine($"Type of capture: {documentCapture.ID_CaptureType}");
                            //    if (documentCapture.Status == "created")
                            //    {
                            //        SaveAsync(documentCapture.ID_CaptureType, t.Result.ID, documentCapture.documents);
                            //        // Wait for all the tasks to finish.
                            //        //Task.WaitAll(SaveAsync(documentCapture.ID_CaptureType, t.Result.ID, documentCapture.documents).ToArray());

                            //        //// We should never get to this point
                            //        //Console.WriteLine("WaitAll() has not thrown exceptions. THIS WAS NOT EXPECTED.");
                            //    }
                            //    else if (documentCapture.Status == "updated")
                            //    {
                            //        UpdateAsync(t.Result.ID, documentCapture.documents, documentCapture.ID_Capture);
                            //        // Wait for all the tasks to finish.
                            //        //Task.WaitAll(UpdateAsync(t.Result.ID, documentCapture.documents, documentCapture.ID_Capture).ToArray());

                            //        //// We should never get to this point
                            //        //Console.WriteLine("WaitAll() has not thrown exceptions. THIS WAS NOT EXPECTED.");
                            //    }
                            //    else if (documentCapture.Status == "deleted")
                            //    {
                            //        Debug.WriteLine("is deleted");
                            //    }
                            //}
                            //else
                            //{
                            //    Debug.Fail(t.Result.Message);
                            //}

                    }
                }


                //foreach (var log in _logs)
                //{
                //    Debug.WriteLine(log.Change);
                //}

                if (_view.SendAfterSave && ID_Incident != "")
                {
                    SendEmail();
                    _view.ViewClose();
                }
                else
                {
                    _view.ViewClose();
                }


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void SaveCapture(string captureId, string captureType, string incident)
        {
            var response = CaptureService.AddCapture(captureId, captureType, incident, "testing", "");
            ID_Capture = response.ID;
        }

        public void AddIncident()
        {
            if (_view.SendAfterSave)
            {
                if (_view.SelectedMail == "" && !_view.SendToAllRecipientsInTheCategory)
                {
                    _view.MailValidationBorder = BorderStyles.Simple;
                    Utils.ShowMessage("Please select a mail before submit the changes", "Mail Error", type: "Warning");
                    return;
                }

                if (_view.MailDirectoryCategory == "" && _view.SendToAllRecipientsInTheCategory)
                {
                    _view.CategoryValidationBorder = BorderStyles.Simple;
                    Utils.ShowMessage("Please select a category before submit the changes", "Mail Error", type: "Warning");
                    return;
                }
            }

            DataRow folioReponse = Functions.Get_Folio().Select().First();
            Folio = folioReponse.ItemArray[2].ToString() + "-" + folioReponse.ItemArray[3].ToString();

            var t = new Task<Response>(() => IncidentService.AddIncident(
                ID_Driver == Guid.Empty.ToString() ? _ID_Samsara : ID_Driver,
                _view.FullName,
                _view.ID_State,
                _view.ID_City,
                ID_Broker.ToUpper(),
                ID_Broker2,
                ID_Truck,
                Folio,
                _view.TruckNumber == null ? "" : _view.TruckNumber.ToString(),
                _view.TrailerNumber,
                _view.CargoType,
                _view.IncidentDate,
                _view.PoliceReport,
                _view.CitationReportNumber,
                _view.CargoSpill,
                _view.ManifestNumber,
                _view.LocationReferences,
                _view.Latitude,
                _view.Longitude,
                _view.TruckDamages,
                _view.TruckCanMove,
                _view.TruckNeedCrane,
                _view.TrailerDamages,
                _view.TrailerCanMove,
                _view.TrailerNeedCrane,
                constants.userID,
                _view.Comments == null ? "" : _view.Comments.ToString(),
                ID_Driver == Guid.Empty.ToString()
            ));

            t.Start();
            t.Wait();

            if (t.Result.validation)
            {
                ID_Incident = t.Result.ID;
            }
            
            foreach (var person in _PersonsInvolved)
            {
                if (t.Result.validation)
                {
                    person.ID_Incident = t.Result.ID;
                    IncidentService.AddPersonInvolved(person);
                }
                else
                {
                    Debug.WriteLine(t.Result.Message);
                }

            }

            if (t.Result.validation)
            {
                foreach (var documentCapture in _view.Documents)
                {

                    if (documentCapture.Status == "created")
                    {
                        Task tCapture = new Task(() => SaveCapture(documentCapture.ID_Capture, documentCapture.ID_CaptureType, t.Result.ID));
                        tCapture.Start();
                        tCapture.Wait();

                        foreach (var doc in documentCapture.documents)
                        {
                            if (doc.Status == "empty" || doc.Status == "loaded")
                                continue;
                            var tDocument = new Task(() => CaptureService.AddImage(Guid.NewGuid().ToString(), documentCapture.ID_Capture, doc.FirebaseUrl, doc.name, "", doc.Type));
                            tDocument.Start();
                            tDocument.Wait();
                        }
                    }
                    else if (documentCapture.Status == "updated")
                    {
                        foreach (var doc in documentCapture.documents)
                        {
                            if (doc.Status == "empty" || doc.Status == "loaded")
                                continue;
                            var tDocument = new Task(() => CaptureService.AddImage(Guid.NewGuid().ToString(), documentCapture.ID_Capture, doc.FirebaseUrl, doc.name, "", doc.Type));
                            tDocument.Start();
                            tDocument.Wait();
                        }
                    }
                }

            }


            //        foreach (var documentCapture in _view.Documents)
            //{
            //    if (t.Result.validation)
            //    {
            //        Debug.WriteLine($"Type of capture: {documentCapture.ID_CaptureType}");
            //        if (documentCapture.Status == "created")
            //            SaveAsync(documentCapture.ID_CaptureType, t.Result.ID, documentCapture.documents);
            //        else if (documentCapture.Status == "updated")
            //        {
            //            SaveAsync(documentCapture.ID_CaptureType, t.Result.ID, documentCapture.documents);
            //            //UpdateAsync(t.Result.ID, documentCapture.documents, documentCapture.ID_Capture);
            //        }
            //    }
            //    else
            //    {
            //        Debug.Fail(t.Result.Message);
            //    }

            //}

            if (_view.SendAfterSave && ID_Incident != "")
            {
                SendEmail();
                _view.ViewClose();
            }
            else
            {
                _view.ViewClose();
            }
        }

        public double[] GetTruckSamsara()
        {
            double latitude = 0;
            double longitude = 0;
            string number = _view.TruckId;
            string url = "https://api.samsara.com/fleet/vehicles/locations/feed?vehicleIds=" + number;

            if (number != "")
            {
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

                        //List<Vehicle> locs = data.Select(p => new Vehicle
                        //{
                        //    name = p["name"].ToString().Trim(),
                        //    time = (DateTime)p["location"]["time"],
                        //    latitude = (float)p["location"]["latitude"],
                        //    longitude = (float)p["location"]["longitude"],
                        //    heading = (int)p["location"]["heading"],
                        //    speed = (int)p["location"]["speed"],
                        //    formattedLocation = (string)p["location"]["reverseGeo"]["formattedLocation"]
                        //}).ToList();

                        //var filtered = locs.Where(x => x.name == number);
                        if (data.Count > 0)
                        {
                            latitude = (double)data[0]["locations"][0]["latitude"];
                            longitude = (double)data[0]["locations"][0]["longitude"];
                            _view.Latitude = latitude.ToString();
                            _view.Longitude = longitude.ToString();
                            _view.LocationReferences = (string)data[0]["locations"][0]["reverseGeo"]["formattedLocation"];
                        }

                        //foreach (var item in filtered)
                        //{
                        //    latitude = (double)item.latitude;
                        //    longitude = (double)item.longitude;
                        //    _view.Latitude = item.latitude.ToString();
                        //    _view.Longitude = item.longitude.ToString();

                        //    //Testing samsara = new Testing(item.name, item.time, item.latitude, item.longitude, item.heading, item.speed, item.formattedLocation);
                        //    //samsara.Show();

                        //}


                        //Dispose once all HttpClient calls are complete.This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
                        client.Dispose();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            return new double[] { latitude, longitude };
        }

        #region Involved Persons
        public void AddPersonInvolved()
        {
            validate("validate");
            if (_validation)
            {
                _view.LblEmptyFieldsVisibility = false;
                _PersonsInvolved.Add(new PersonsInvolved(_view.IPFullName, _view.IPLastName1, _view.IPPhoneNumber, _view.IPAge, _view.IPDriver, _view.IPDriver ? _view.IPDriverLicense : "", _view.IPPrivate, _view.IPInjured, _view.IPHospital, _view.IPComments, Guid.Empty.ToString()));
                _view.InvolvedPersonsDataSource = _PersonsInvolved;

                CleanPersonInvolvedCapture();
            }
            else
            {
                _view.LblEmptyFieldsVisibility = true;
            }
        }

        public void validate(string controlName)
        {
            switch (controlName)
            {
                case "edt_IPFullName":
                    if (_view.IPFullName.Length == 0)
                    {
                        _view.EdtFullNameBorder = BorderStyles.Simple;
                        _view.EdtFullNameShowWarningIcon = true;
                        _validation = false;
                    }
                    else
                    {
                        _view.EdtFullNameBorder = BorderStyles.Default;
                        _view.EdtFullNameShowWarningIcon = false;
                        _validation = true;
                    }
                    break;
                case "edt_IPLastName1":
                    if (_view.IPLastName1.Length == 0)
                    {
                        _view.EdtLastNameBorder = BorderStyles.Simple;
                        _view.EdtLastName1ShowWarningIcon = true;
                        _validation = false;
                    }
                    else
                    {
                        _view.EdtLastNameBorder = BorderStyles.Default;
                        _view.EdtLastName1ShowWarningIcon = false;
                        _validation = true;
                    }
                    break;
                case "edt_IPPhoneNumber":
                    if (_view.IPPhoneNumber.Length == 0)
                    {
                        _view.EdtPhoneNumberBorder = BorderStyles.Simple;
                        _view.EdtPhoneNumberShowWarningIcon = true;
                        _validation = false;
                    }
                    else
                    {
                        _view.EdtPhoneNumberBorder = BorderStyles.Default;
                        _view.EdtPhoneNumberShowWarningIcon = false;
                        _validation = true;
                    }
                    break;
                case "edt_IPLicense":
                    if (_view.IPDriver)
                    {
                        if (_view.IPDriverLicense.Length == 0)
                        {
                            _view.EdtLicenseBorder = BorderStyles.Simple;
                            _view.EdtLicenseShowWarningIcon = true;
                            _validation = false;
                        }
                        else
                        {
                            _view.EdtLicenseBorder = BorderStyles.Default;
                            _view.EdtLicenseShowWarningIcon = false;
                            _validation = true;
                        }
                    }
                    break;
                case "selectedMail":
                    if (_view.SelectedMail != "")
                    {
                        _view.MailValidationBorder = BorderStyles.Default;
                    }
                    break;
                case "selectedCategory":
                    if (_view.MailDirectoryCategory != "")
                    {
                        _view.CategoryValidationBorder = BorderStyles.Default;
                    }
                    break;
                case "validate":
                    if (_view.IPFullName.Length == 0)
                    {
                        _view.EdtFullNameBorder = BorderStyles.Simple;
                        _view.EdtFullNameShowWarningIcon = true;
                        _validation = false;
                    }

                    if (_view.IPLastName1.Length == 0)
                    {
                        _view.EdtLastNameBorder = BorderStyles.Simple;
                        _view.EdtLastName1ShowWarningIcon = true;
                        _validation = false;
                    }

                    if (_view.IPPhoneNumber.Length == 0)
                    {
                        _view.EdtPhoneNumberBorder = BorderStyles.Simple;
                        _view.EdtPhoneNumberShowWarningIcon = true;
                        _validation = false;
                    }

                    if (!_view.IPPassenger && !_view.IPDriver)
                    {
                        _view.CkedtPassengerBorder = BorderStyles.Simple;
                        _view.CkedtDriverBorder = BorderStyles.Simple;
                        _validation = false;
                    }

                    if (_view.IPDriver)
                    {
                        if (_view.IPDriverLicense.Length == 0)
                        {
                            _view.EdtLicenseBorder = BorderStyles.Simple;
                            _view.EdtLicenseShowWarningIcon = true;
                            _validation = false;
                        }
                    }
                    else
                    {
                        _view.EdtLicenseBorder = BorderStyles.Default;
                        _view.EdtLicenseShowWarningIcon = false;
                    }
                    break;
            }

            if (_view.EdtFullNameShowWarningIcon || _view.EdtLastName1ShowWarningIcon)
                _view.LblEmptyFieldsVisibility = true;
            else
                _view.LblEmptyFieldsVisibility = false;
        }

        private void CleanPersonInvolvedCapture()
        {
            _view.IPFullName = "";
            _view.IPLastName1 = "";
            _view.IPPhoneNumber = "";
            _view.IPAge = "";
            _view.IPPrivate = false;
            _view.IPInjured = false;
            _view.IPPassenger = false;
            _view.IPDriver = false;
            _view.IPDriverLicense = "";
            _view.IPHospital = "";
            _view.IPComments = "";
        }

        public void EditInvolvedPersonByRow(Int32 index)
        {
            _selectedPerson = index;
            var person = _PersonsInvolved[index];
            _view.IPFullName = person.FullName;
            _view.IPLastName1 = person.LastName1;
            _view.IPPhoneNumber = person.PhoneNumber;
            _view.IPAge = person.Age;
            _view.IPPrivate = person.PrivatePerson;
            _view.IPInjured = person.Injured;
            _view.IPPassenger = !person.Driver;
            _view.IPDriver = person.Driver;
            _view.IPDriverLicense = person.DriverLicense;
            _view.IPHospital = person.Hospital;
            _view.IPComments = person.Comments;

            _view.BtnAddInvolvedPersonVisibility = false;
            _view.BtnEditInvolvedPersonVisibility = true;

            if (_view.BtnEditInvolvedPersonLocation.X == 8)
                _view.BtnEditInvolvedPersonLocation = new System.Drawing.Point(1483, 81);

        }

        public void UpdatePersonInvolved()
        {
            Int32 errors = 0;

            if (_view.IPDriver)
            {
                if (_view.IPDriverLicense.Length == 0)
                {
                    _view.EdtLicenseBorder = BorderStyles.Simple;
                    _view.EdtLicenseShowWarningIcon = true;
                    errors += 1;
                }
                else
                {
                    _view.EdtLicenseBorder = BorderStyles.Default;
                    _view.EdtLicenseShowWarningIcon = false;
                }
            }
            else
            {
                if (errors > 0) { errors -= 1; }
            }

            if (!_errors)
            {
                _view.LblEmptyFieldsVisibility = false;
                _PersonsInvolved[_selectedPerson].FullName = _view.IPFullName;
                _PersonsInvolved[_selectedPerson].LastName1 = _view.IPLastName1;
                _PersonsInvolved[_selectedPerson].PhoneNumber = _view.IPPhoneNumber;
                _PersonsInvolved[_selectedPerson].Age = _view.IPAge;
                _PersonsInvolved[_selectedPerson].SetDriver(_view.IPDriver);
                _PersonsInvolved[_selectedPerson].DriverLicense = _view.IPDriverLicense;
                _PersonsInvolved[_selectedPerson].SetPrivate(_view.IPPrivate);
                _PersonsInvolved[_selectedPerson].SetInjured(_view.IPInjured);
                _PersonsInvolved[_selectedPerson].Hospital = _view.IPHospital;
                _PersonsInvolved[_selectedPerson].Comments = _view.IPComments;

                _view.InvolvedPersonsDataSource = _PersonsInvolved;

                CleanPersonInvolvedCapture();

                _view.BtnAddInvolvedPersonVisibility = true;
                _view.BtnEditInvolvedPersonVisibility = false;
            }
            else
            {
                _view.LblEmptyFieldsVisibility = true;
            }

        }

        public void RemoveInvolvedPersonByRow(int idx)
        {
            _PersonsInvolved.RemoveAt(idx);
            _view.InvolvedPersonsDataSource = _PersonsInvolved;
            CleanPersonInvolvedCapture();
            _view.BtnAddInvolvedPersonVisibility = true;
            _view.BtnEditInvolvedPersonVisibility = false;
        }
        #endregion

        #region utils for this view
        public void CheckEditChanged(string ckedtName, bool ckedtValue)
        {
            switch (ckedtName)
            {
                case "ckedt_Spill":
                    _view.edtManifestReadOnly = !ckedtValue;
                    _view.ManifestNumber = "";
                    break;
                case "ckedt_PoliceReport":
                    _view.edtPoliceReportReadOnly = !ckedtValue;
                    _view.CitationReportNumber = "";
                    break;
                case "ckedt_IPPassenger":
                    if (_view.IPDriver && ckedtValue)
                    {
                        _view.IPDriver = false;
                        //_view.PnlDriverInvolvedVisibility = false;
                        _view.IPDriverLicense = "";
                    }

                    if (_view.IPPassenger)
                    {
                        _view.CkedtPassengerBorder = BorderStyles.Default;
                        _view.CkedtDriverBorder = BorderStyles.Default;
                        _validation = true;

                        _view.EdtLicenseBorder = BorderStyles.Default;
                        _view.EdtLicenseShowWarningIcon = false;
                    }
                    break;
                case "ckedt_IPDriver":
                    if (_view.IPPassenger && ckedtValue)
                        _view.IPPassenger = false;

                    if (_view.IPDriver)
                    {
                        _view.CkedtPassengerBorder = BorderStyles.Default;
                        _view.CkedtDriverBorder = BorderStyles.Default;
                        _validation = true;
                    }
                    //_view.PnlDriverInvolvedVisibility = ckedtValue;
                    break;
            }
        }
        #endregion

        #region captures documents
        public async void SaveAsync(string ID_CaptureType, string ID_Incindent, List<Models.Documents.Document> documents)
        {
            string ID_Capture = "";
            bool success = false;
            List<Task<Response>> filesUploaded = new List<Task<Response>>();

            //var t = new Task(() => (ID_Capture, success) = SaveCapture(ID_CaptureType, ID_Incindent));
            //t.Start();
            //t.Wait();

            if (success)
            {
                //List<DocumentCapture> docsLoaded = _documents.Where(d => d.Path != null).ToList();
                for (var i = 0; i < documents.Count(); i++)
                {
                    var document = documents[i];

                    if (document.Path == "")
                        continue;

                    var task = UploadImgFirebaseAsync(document.Path, document.name, ID_Capture);

                    //ProgressBarControl pbr = _view.GetPbrControl(document.containerName, $"pbrDocument{document.ID}");

                    //_view.SetControlProperties(document.containerName, $"lblStatus{document.ID}", visibility: false);
                    //pbr.Visible = true;

                    //Track progress of the upload
                    task.Progress.ProgressChanged += (s, ev) =>
                    {
                        //pbr.EditValue = ev.Percentage;
                        //pbr.CreateGraphics().DrawString(ev.Percentage.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(pbr.Width / 2 - 10, pbr.Height / 2 - 7));
                        Console.WriteLine($"Progress: {ev.Percentage} %");
                    };

                    
                    document.FirebaseUrl = await task;

                    //pbr.Visible = false;
                    //_view.SetControlProperties(document.containerName, $"lblStatus{document.ID}", "Uploaded", true);

                    Response imgResponse = CaptureService.AddImage(Guid.NewGuid().ToString(), ID_Capture, document.FirebaseUrl, document.name, "", document.Type);

                }


            }
            else
            {
                Debug.Fail("Problem in upload capture");
            }

        }

        public async void UpdateAsync(string ID_Incindent, List<Models.Documents.Document> documents, string ID_Capture)
        {
            List<Task<Response>> filesUploaded = new List<Task<Response>>();
            bool success = true;

            if (success)
            {
                //var documentsDeleted = documents.Where(d => d.Status == "deleted").ToList();
                //if (documentsDeleted.Count > 0)
                //{
                //    for (var i = 0; i < documentsDeleted.Count(); i++)
                //    {

                //    }
                //}

                //List<DocumentCapture> docsLoaded = _documents.Where(d => d.Path != null).ToList();
                var documentsUpdated = documents.Where(d => d.Status == "updated" || d.Status == "created").ToList();
                for (var i = 0; i < documentsUpdated.Count(); i++)
                {
                    var document = documentsUpdated[i];
                    var task = UploadImgFirebaseAsync(document.Path, document.name, ID_Capture);

                    //ProgressBarControl pbr = _view.GetPbrControl(document.containerName, $"pbrDocument{document.ID}");

                    //_view.SetControlProperties(document.containerName, $"lblStatus{document.ID}", visibility: false);
                    //pbr.Visible = true;

                    //Track progress of the upload
                    task.Progress.ProgressChanged += (s, ev) =>
                    {
                        //pbr.EditValue = ev.Percentage;
                        //pbr.CreateGraphics().DrawString(ev.Percentage.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(pbr.Width / 2 - 10, pbr.Height / 2 - 7));
                        Console.WriteLine($"Progress: {ev.Percentage} %");
                    };

                    System.Action a = delegate () {
                        CaptureService.AddImage(Guid.NewGuid().ToString(), ID_Capture, "", "", "", "");
                    };

                    //Response r = (Response)a;

                    task.GetAwaiter().OnCompleted(a);
                    //pbr.Visible = false;
                    //_view.SetControlProperties(document.containerName, $"lblStatus{document.ID}", "Uploaded", true);

                    var ID = document.Status == "created" ? Guid.NewGuid().ToString() : document.ID_Document;

                    document.FirebaseUrl = await task;


                    var imgResponse = a;
                    //Response imgResponse = CaptureService.UpdateImage(ID, ID_Capture, document.FirebaseUrl, document.name, "", document.Type);

                }

            }
            else
            {
                Debug.Fail("Problem in upload capture");
            }

        }

        private FirebaseStorageTask UploadImgFirebaseAsync(string filepath, string name, string ID_Capture)
        {
            try
            {
                FileStream stream;
                //Get any Stream — it can be FileStream, MemoryStream or any other type of Stream
                stream = File.Open(filepath, FileMode.Open);

                //Construct FirebaseStorage with path to where you want to upload the file and put it there
                var task = new FirebaseStorage("dcmanagement-3d402.appspot.com")
               .Child("SIREM")
               .Child(ID_Capture)
               .Child(name)
               .PutAsync(stream);

                System.Action a = delegate () {
                    CaptureService.AddImage(Guid.NewGuid().ToString(), ID_Capture, "", "", "", "");
                };

                // Await the task to wait until upload is completed and get the download url
                return task;
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.Message);
                return null;
            }
        }

        private void SaveDocumentsAsync(string filepath, string name, string ID_Capture) {

            try
            {
                FileStream stream;
                //Get any Stream — it can be FileStream, MemoryStream or any other type of Stream
                stream = File.Open(filepath, FileMode.Open);

                //Construct FirebaseStorage with path to where you want to upload the file and put it there
                var task = new FirebaseStorage("dcmanagement-3d402.appspot.com")
               .Child("SIREM")
               .Child(ID_Capture)
               .Child(name)
               .PutAsync(stream);

                // Await the task to wait until upload is completed and get the download url
                System.Action a = delegate () { 
                    CaptureService.AddImage(Guid.NewGuid().ToString(), ID_Capture, "", "", "", ""); 
                };
                task.GetAwaiter().OnCompleted(a);
                //a.Method.
               
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.Message);
                
            }
        }

        public void GetDocuments()
        {
            if (ID_Incident != "")
            {
                var documentCaptures = CaptureService.ListDocumentsCapture(Guid.Parse(ID_Incident));
                _view.Documents = documentCaptures;
            }
            
        }

        //private (string ID, bool success) SaveCapture(string ID_CaptureType, string ID_Incident)
        //{
        //    var response = CaptureService.AddCapture(ID_CaptureType, ID_Incident, "testing", "");
        //    if (response.validation)
        //        return (response.ID, true);
        //    else
        //    {
        //        Debug.Fail(response.Message);
        //        return (response.ID, false);
        //    }

        //}

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

        #region mailing
        public void SendEmail()
        {
            bool sended = false;

            if (!File.Exists(ReportPath + $"{Folio}.pdf"))
            {
                PDF(true);
            }

            if (!_view.SendToAllRecipientsInTheCategory)
            {
                sended = Utils.email_send(ReportPath + $"\\{Folio}.pdf", false, mailAddress: _view.SelectedMail);
            }

            if (_view.SendToAllRecipientsInTheCategory)
            {
                sended = Utils.email_send(ReportPath + $"\\{Folio}.pdf", false, categoryID: _view.MailDirectoryCategory);
            }

            if (sended)
                Utils.ShowMessage("Mail Sent", type: "MailSent");
        }

        public void PDF(bool generate = false, bool openAfterSave = false)
        {
            if (generate)
            {
                
                var t = new Task(() =>
                {
                    _selectedIncident = IncidentService.GetIncident(ID_Incident).FirstOrDefault();
                });
                t.Start();
                t.Wait();

            }

            IncidentReport report1 = new IncidentReport(_selectedIncident);
            try
            {
                var reportFilename = ReportPath + $"\\{Folio}.pdf";
                report1.ExportToPdf(reportFilename);
                if (!generate)
                    Utils.ShowMessage("Report " + $"{Folio}.pdf");
                if (openAfterSave)
                    Process.Start(reportFilename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        public void GetMailsByCategory()
        {
            _MailDIrectory = MailDirectoryService.GetMailDirectory(_view.MailDirectoryCategory);
            _view.MailDirectoryDataSource = _MailDIrectory;
        }
        #endregion
    }
}
