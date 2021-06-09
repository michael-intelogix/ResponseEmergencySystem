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
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using ResponseEmergencySystem.Forms.Modals;
using DevExpress.XtraEditors.Controls;
using System.Diagnostics;
using System.IO;
using ResponseEmergencySystem.Forms;
using Firebase.Storage;

namespace ResponseEmergencySystem.Controllers.Incidents
{
    public class EditIncidentController
    {
        IEditIncidentView _view;
        private string ID_Incident;
        Incident _selectedIncident;
        DataTable dt_InjuredPersons = new DataTable();

        private List<Truck> _trucks = new List<Truck>();
        private List<Driver> _DriversLocal = new List<Driver>();

        private Int32 _selectedPerson = 0;

        private List<PersonsInvolved> _PersonsInvolved;

        public double latitude;
        public double longitude;

        private string ID_Driver;
        private string ID_Broker;
        private string ID_Truck;
        private string ID_Trailer;
        private string comments = "";

        private bool _errors = false;
        private bool _validation;

        private bool _DriverUpdateRequired = false;

        private string _ID_Samsara;
        private string _DriverName;

        public EditIncidentController(IEditIncidentView view, string incidentId)
        {
            ID_Incident = incidentId;
            _DriversLocal = DriverService.GetDriver("");
            _trucks = GeneralService.list_Trucks();
            _view = view;
            view.SetController(this);
        }

        public Incident Incident
        {
            get { return _selectedIncident; }
        }

        public void LoadTrucks()
        {
            _view.TrucksDataSource = _trucks;
        }

        public void LoadDrivers()
        {
            _view.DriversDataSource = _DriversLocal;
        }

        public void LoadIncident()
        {
            _selectedIncident = IncidentService.GetIncident(ID_Incident)[0];
            _PersonsInvolved = IncidentService.list_PersonsInvolved(ID_Incident);

            ID_Driver = _selectedIncident.driver.ID_Driver.ToString(); ;
            ID_Broker = _selectedIncident.broker.ID_Broker;
            ID_Truck = _selectedIncident.truck.ID_Truck.ToString();
            ID_Trailer = _selectedIncident.trailer.ID_Trailer.ToString();

            _DriverName = _selectedIncident.driver.Name;

            _view.FullName = _selectedIncident.Name;
            _view.PhoneNumber = _selectedIncident.PhoneNumber;
            _view.License = _selectedIncident.driver.License;
            _view.ExpirationDate = Convert.ToDateTime(_selectedIncident.driver.ExpirationDate).Date;
            _view.LicenseState = _selectedIncident.driver.ID_StateOfExpedition;
            _view.TruckNumber = _selectedIncident.truck.ID_Samsara;
            _view.TruckDamages = _selectedIncident.TruckDamage;
            _view.TruckCanMove = _selectedIncident.TruckCanMove;
            _view.TruckNeedCrane = _selectedIncident.TruckNeedCrane;
            _view.TrailerNumber = _selectedIncident.trailer.TrailerNumber;
            _view.TrailerDamages = _selectedIncident.TrailerDamage;
            _view.TrailerCanMove = _selectedIncident.TrailerCanMove;
            _view.TrailerNeedCrane = _selectedIncident.TrailerNeedCrane;
            _view.CargoSpill = _selectedIncident.trailer.CargoSpill;
            _view.CargoType = _selectedIncident.trailer.Commodity;
            _view.ManifestNumber = _selectedIncident.ManifestNumber;

            _view.IncidentDate = _selectedIncident.IncidentDate;
            _view.ID_State = _selectedIncident.ID_State;
            _view.ID_City = _selectedIncident.ID_City;

            _view.Broker = _selectedIncident.broker.Name;
            _view.Comments = _selectedIncident.Comments;

            //_view.ID_StatusDetail = _selectedIncident.ID_StatusDetail;

            #region Accident Details
            //_view.IncidentDate = _selectedIncident.IncidentDate.Date;
            //_view.IncidentDate = _selectedIncident.IncidentDate.ToString("hh:mm:ss tt");
            _view.PoliceReport = _selectedIncident.PoliceReport;
            _view.CitationReportNumber = _selectedIncident.CitationReportNumber;
            _view.Latitude = _selectedIncident.IncidentLatitude;
            _view.Longitude = _selectedIncident.IncidentLongitude;
            _view.LocationReferences = _selectedIncident.LocationReferences;
            #endregion

            latitude = Convert.ToDouble(_selectedIncident.IncidentLatitude);
            longitude = Convert.ToDouble(_selectedIncident.IncidentLongitude);

            _view.LoadIncident(_selectedIncident);
            _view.LoadStates(Functions.getStates());
            //_view.LueStatusDetailDataSource = StatusDetailService.list_StatusDetail();
            if (_PersonsInvolved.Count > 0)
                _view.InvolvedPersonsDataSource = _PersonsInvolved;

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

        public void GetDriver(string ID)
        {
            Driver selectedDriver = _DriversLocal.Where(d => d.ID_Samsara == ID).First();

            ID_Driver = selectedDriver.ID_Driver.ToString();
            _ID_Samsara = selectedDriver.ID_Samsara.ToString();
            _DriverName = selectedDriver.Name + " " + selectedDriver.LastName1;
            _view.FullName = _DriverName;
            _view.PhoneNumber = selectedDriver.PhoneNumber;
            _view.License = selectedDriver.License;

            if (selectedDriver.ExpirationDate != null)
                _view.ExpirationDate = (DateTime)selectedDriver.ExpirationDate;
            else
            {
                _view.ExpirationDate = DateTime.Now;
                _DriverUpdateRequired = true;
            }

            _view.LicenseState = selectedDriver.ID_StateOfExpedition;
        }

        public void SetBroker(string ID_Broker)
        {
            this.ID_Broker = ID_Broker;
        }

        public void SetTruck(string ID_Truck)
        {
            this.ID_Truck = ID_Truck;
        }

        public void SetTrailer(string ID_Trailer)
        {
            this.ID_Trailer = ID_Trailer;
        }

        public void CheckNumber(string edtName)
        {
            DataTable dt_Response = new DataTable();

            switch (edtName)
            {
                case "edt_TruckNumber":
                    dt_Response = Functions.Get_Truck(_view.TruckNumber);
                    DataRow truckResponse = dt_Response.Select().First();
                    SetTruck(truckResponse.ItemArray[0].ToString());
                    _view.LblTruckExistsVisibility = truckResponse.ItemArray[0].ToString() == "0";
                    break;
                case "edt_TrailerNumber":
                    dt_Response = Functions.Get_Trailer(_view.TrailerNumber);
                    DataRow trailerResponse = dt_Response.Select().First();
                    SetTrailer(trailerResponse.ItemArray[0].ToString());
                    if (trailerResponse.ItemArray[0].ToString() != "0")
                    {
                        _view.LblTrailerExistsVisibility = false;
                        _view.CargoType = trailerResponse.ItemArray[3].ToString();
                    }
                    else
                    {
                        _view.LblTrailerExistsVisibility = true;
                    }

                    break;
            }
        }

        public void GetCitiesByState()
        {
            var cities = GeneralService.list_Cities(_view.ID_State);
            _view.LueCitiesDataSource = cities;
        }

        public void AddPersonInvolved()
        {
            validate("validate");
            if (_validation)
            {
                _view.LblEmptyFieldsVisibility = false;
                _PersonsInvolved.Add(new PersonsInvolved(_view.IPFullName, _view.IPLastName1, _view.IPPhoneNumber, _view.IPAge, _view.IPDriver, _view.IPDriverLicense, _view.IPPrivate, _view.IPInjured, Guid.Empty.ToString()));
                _view.InvolvedPersonsDataSource = _PersonsInvolved;

                CleanPersonInvolvedCapture();
            }
            else
            {
                _view.LblEmptyFieldsVisibility = true;
            }



        }

        public void UpdatePersonInvolved()
        {
            int errors = 0;

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
                _PersonsInvolved[_selectedPerson].Driver = _view.IPDriver; 
                _PersonsInvolved[_selectedPerson].DriverLicense = _view.IPDriverLicense;
                _PersonsInvolved[_selectedPerson].PrivatePerson = _view.IPPrivate;
                _PersonsInvolved[_selectedPerson].Injured = _view.IPInjured;

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

            _view.BtnAddInvolvedPersonVisibility = false;
            _view.BtnEditInvolvedPersonVisibility = true;

            if (_view.BtnEditInvolvedPersonLocation.X == 8)
                _view.BtnEditInvolvedPersonLocation = new System.Drawing.Point(1177, 48);
        }

        public void RemoveInvolvedPersonByRow(int idx)
        {
            _PersonsInvolved.RemoveAt(idx);
            _view.InvolvedPersonsDataSource = _PersonsInvolved;
        }

        public void Update()
        {
            //check location refreces
            try
            {
                var t = new Task<Response>(() => IncidentService.UpdateIncident(
                    ID_Incident,
                    ID_Driver == Guid.Empty.ToString() ? _ID_Samsara : ID_Driver,
                    _DriverName,
                    _view.ID_State,
                    _view.ID_City,
                    ID_Broker.ToUpper(),
                    ID_Truck,
                    _view.TruckNumber,
                    _view.TrailerNumber,
                    _view.CargoType,
                    //_view.ID_StatusDetail,
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
                    constants.userIDTest.ToString(),
                    _view.Comments,
                    ID_Driver == Guid.Empty.ToString()
                ));

                t.Start();
                t.Wait();

                foreach(var person in _PersonsInvolved)
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

                foreach(var documentCapture in _view.Documents)
                {
                    if (t.Result.validation)
                    {
                        Debug.WriteLine($"Type of capture: {documentCapture.ID_CaptureType}");
                        SaveAsync(documentCapture.ID_CaptureType, t.Result.ID, documentCapture.documents);
                    }
                    else
                    {
                        Debug.Fail(t.Result.Message);
                    }
                    
                }
            } 
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void CheckEditChanged(string ckedtName, bool ckedtValue)
        {
            switch (ckedtName)
            {
                case "ckedt_Spill":
                    //_view.PnlBolVisibility = ckedtValue;
                    break;
                case "ckedt_PoliceReport":
                    //_view.PnlPoliceReportVisibility = ckedtValue;
                    break;
                case "ckedt_IPPassenger":
                    if (_view.IPDriver && ckedtValue)
                    {
                        _view.IPDriver = false;
                        _view.PnlDriverInvolvedVisibility = false;
                        _view.IPDriverLicense = "";
                    }

                    if (_view.IPPassenger)
                    {
                        _view.CkedtPassengerBorder = BorderStyles.Default;
                        _view.CkedtDriverBorder = BorderStyles.Default;
                        _validation = true;
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
                    _view.PnlDriverInvolvedVisibility = ckedtValue;
                    break;
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
        }

        public (bool, string, string) CheckDocument()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files(*.PNG;*.JPG;*.GIF;*.BMP)|*.PNG;*.JPG;*.GIF;*.BMP|PDF Files (*.PDF)|*.PDF|All Files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    (bool, string, string) d;

                    string ext = Path.GetExtension(ofd.FileName).ToUpper();
                    try
                    {

                        if (ext == ".GIF" || ext == ".JPG" || ext == ".PNG" || ext == ".BMP")
                        {
                            d.Item1 = true;
                            d.Item2 = ofd.FileName;
                            d.Item3 = "img";

                            return d;
                        }
                        else if (ext == ".PDF")
                        {
                            d.Item1 = true;
                            d.Item2 = ofd.FileName;
                            d.Item3 = "pdf";

                            return d;
                        }
                        else
                        {
                            Utils.ShowMessage("The file submitted is not an Image", title: "Image upload error", type: "Warning");
                            return (false, "", "");
                        }


                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Utils.ShowMessage(ex.Message, title: "Image upload error", type: "Error");
                        return (false, "", "");
                    }
                }
                else
                {
                    return (false, "", "");
                }



            }
        }

        public string EditImageView(string imgPath, string fileType)
        {
            if (fileType == "img")
            {
                frm_Image imageView = new frm_Image("", "", imgPath);
                ImageController appConfigCtrl = new ImageController(imageView);
                if (imageView.ShowDialog() == DialogResult.OK)
                {
                    Utils.ShowMessage("Image has been updated");
                    return imageView.filepath;
                }
            }

            if (fileType == "pdf")
            {
                frm_PdfViewer pdfViewer = new frm_PdfViewer(imgPath);
                pdfViewer.ShowDialog();
            }

            return "";

        }

        #region documents
        public async void SaveAsync(string ID_CaptureType, string ID_Incindent, List<Models.Documents.Document> documents)
        {
            string ID_Capture = "";
            bool success = false;

            var t = new Task(() => (ID_Capture, success) = SaveCapture(ID_CaptureType, ID_Incindent));
            t.Start();
            t.Wait();

            if (success)
            {
                //List<DocumentCapture> docsLoaded = _documents.Where(d => d.Path != null).ToList();
                for (var i = 0; i < documents.Count(); i++)
                {
                    var document = documents[i];
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
                    document.ID_Document = imgResponse.ID;
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
                //Get any Stream — it can be FileStream, MemoryStream or any other type of Stream
                var stream = File.Open(filepath, FileMode.Open);

                //Construct FirebaseStorage with path to where you want to upload the file and put it there
                var task = new FirebaseStorage("dcmanagement-3d402.appspot.com")
                .Child("SIREM")
                .Child(ID_Capture)
                .Child(name)
                .PutAsync(stream);

                // Await the task to wait until upload is completed and get the download url
                return task;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private (string ID, bool success) SaveCapture(string ID_CaptureType, string ID_Incident)
        {
            var response = CaptureService.AddCapture(ID_CaptureType, ID_Incident, "testing", "");
            if (response.validation)
                return (response.ID, true);
            else
            {
                Debug.Fail(response.Message);
                return (response.ID, false);
            }

        }

        #endregion
    }
}
