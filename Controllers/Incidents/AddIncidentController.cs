﻿using Newtonsoft.Json.Linq;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Forms.Modals;
using ResponseEmergencySystem.Models;
using ResponseEmergencySystem.Samsara_Models;
using ResponseEmergencySystem.Services;
using ResponseEmergencySystem.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using ResponseEmergencySystem.Forms;
using System.IO;
using Firebase.Storage;

namespace ResponseEmergencySystem.Controllers.Incidents
{
    public class AddIncidentController
    {
        IAddIncidentView _view;
        public DataTable dt_InjuredPersons = new DataTable();

        private List<PersonsInvolved> _PersonsInvolved = new List<PersonsInvolved>();
        private List<State> _States = new List<State>();
        private List<Models.Samsara.Driver> _Drivers = new List<Models.Samsara.Driver>();
        private List<Truck> _trucks = new List<Truck>(); 
        private List<Driver> _DriversLocal = new List<Driver>();

        private Int32 _selectedPerson = 0;

        private string ID_Driver;
        private string ID_Broker;
        private string ID_Truck;
        private string ID_Trailer;
        private string comments = "";
        private int _errors = 0;

        private bool _validation = false;

        private bool _DriverUpdateRequired = false;

        public AddIncidentController(IAddIncidentView view)
        {
            _view = view;
            _Drivers = GetDriversSamsara();
            _DriversLocal = DriverService.GetDriver("");
            _States = GeneralService.list_States();
            _trucks = GeneralService.list_Trucks();
            view.SetController(this);
        }

        public void LoadTrucks()
        {
            _view.TrucksDataSource = _trucks;
        }

        public void LoadDrivers()
        {
            _view.DriversDataSource = _DriversLocal;
        }

        public void LoadStates()
        {
           
            _view.LoadStates(Functions.getStates());
        }

        public void GetCitiesByState()
        {
            var cities = Functions.getCities(Guid.Parse(_view.ID_State));
            _view.LueCitiesDataSource = cities;
        }

        public List<Models.Samsara.Driver> GetDriversSamsara()
        {
            const string url = "https://api.samsara.com/fleet/drivers";

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

                    List<Models.Samsara.Driver> drivers = data.Select(p => new Models.Samsara.Driver
                    {
                        ID = (string)p["id"],
                        Name = (string)p["name"],
                        Phone = (string)p["phone"],
                        LicenseNumber = (string)p["licenseNumber"],
                        LicenseState = (string)p["licenseState"]
                    }).ToList();

                    //Dispose once all HttpClient calls are complete.This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
                    client.Dispose();

                    return drivers;
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Models.Samsara.Driver>();
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
                        latitude = (double) item.latitude;
                        longitude = (double) item.longitude;
                        _view.Latitude = item.latitude.ToString();
                        _view.Longitude = item.longitude.ToString();
                        _view.LocationReferences = item.formattedLocation;

                        //Testing samsara = new Testing(item.name, item.time, item.latitude, item.longitude, item.heading, item.speed, item.formattedLocation);
                        //samsara.Show();

                    }


                    //Dispose once all HttpClient calls are complete.This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
                    client.Dispose();
                }

                return new double[] { latitude, longitude };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new double[] { 36.05948, -102.51325 };
            }

            

        }

        public void GetDriver(string ID)
        {
            Driver selectedDriver = _DriversLocal.Where(d => d.ID_Driver == Guid.Parse(ID)).First();

            ID_Driver = selectedDriver.ID_Driver.ToString();
            _view.FullName = selectedDriver.Name + " " + selectedDriver.LastName1;
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

            //var Driver_Response = DriverService.GetDriver(_view.DriverInfoSearch);

            //if (Driver_Response == null)
            //{
            //    Utils.ShowMessage("There is no driver with that search information", title: "Driver Not Found", type: "Warning");
            //}
            //else
            //{
            //    ID_Driver = Driver_Response.ID_Driver.ToString();
            //    _view.FullName = Driver_Response.Name + " " + Driver_Response.LastName1;
            //    _view.PhoneNumber = Driver_Response.PhoneNumber;
            //    _view.License = Driver_Response.License;

            //    if (Driver_Response.ExpirationDate != null)
            //        _view.ExpirationDate = (DateTime)Driver_Response.ExpirationDate;
            //    else
            //    {
            //        _view.ExpirationDate = DateTime.Now;
            //        _DriverUpdateRequired = true;
            //    }

            //    _view.LicenseState = Driver_Response.ID_StateOfExpedition;
            //}

        }

        public void SetBroker()
        {
            frm_BrokerList brokerView = new frm_BrokerList();
            BrokerController brokerCtrl = new BrokerController(brokerView, BrokerService.list_Brokers());
            brokerCtrl.LoadBrokers();
            if (brokerView.ShowDialog() == DialogResult.OK)
            {
                this.ID_Broker = brokerView.ID;
                _view.Broker = brokerView.broker;
            }
            
        }

        public void SetTruck(string ID_Truck)
        {
            var truck = _trucks.Where(t => t.truckNumber == _view.TruckNumber).First();
            this.ID_Truck = truck.ID_Truck.ToString();
        }

        public void SetTrailer(string ID_Trailer)
        {
            this.ID_Trailer = ID_Trailer;
        }

        public void SetComments()
        {
            Forms.AddComments commentsView = new Forms.AddComments();
            if (commentsView.ShowDialog() == DialogResult.OK)
            {
                comments = commentsView.comments; 
            }
        }

        public void AddIncident()
        {
            DataRow folioReponse = Functions.Get_Folio().Select().First();
            string folio = folioReponse.ItemArray[2].ToString() + "-" + folioReponse.ItemArray[3].ToString();

            string ID_Incident = "";
            //check location refreces

            if (_DriverUpdateRequired)
            {
                //var t2 = new Task<Response>(() =>);
            }
            

            var t = new Task<Response>(() => IncidentService.AddIncident(
                ID_Driver.ToUpper(),
                _view.FullName,
                _view.ID_State,
                _view.ID_City,
                ID_Broker.ToUpper(),
                ID_Truck,
                folio,
                _view.TruckNumber,
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
                _view.Comments,
                ID_Driver == Guid.Empty.ToString()
            ));

            t.Start();
            t.Wait();

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

            foreach (var documentCapture in _view.Documents)
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

        public void CreateInjuredPersonsTable()
        {
            dt_InjuredPersons.Columns.Add("FullName");
            dt_InjuredPersons.Columns.Add("LastName1");
            dt_InjuredPersons.Columns.Add("LastName2");
            dt_InjuredPersons.Columns.Add("Phone");
            _view.LoadInjuredPersons(dt_InjuredPersons);
        }

        public void addEmptyRow()
        {
            DataRow _data = dt_InjuredPersons.NewRow();
            _data["FullName"] = "";
            _data["LastName1"] = "";
            _data["LastName2"] = "";
            _data["Phone"] = "";
            dt_InjuredPersons.Rows.Add(_data);
            _view.LoadInjuredPersons(dt_InjuredPersons);
        }

        private float Truncate(float value, int digits)
        {
            double mult = Math.Pow(10.0, digits);
            double result = Math.Truncate(mult * value) / mult;
            return (float)result;
        }

        public void CheckEditChanged(string ckedtName, bool ckedtValue)
        {
            switch (ckedtName)
            {
                case "ckedt_Spill":
                    _view.PnlBolVisibility = ckedtValue;
                    break;
                case "ckedt_PoliceReport":
                    _view.PnlPoliceReportVisibility = ckedtValue;
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
            switch(controlName)
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
                        _view.BtnAddTrailerVisibility = false;
                        _view.EdtCommodityReadOnly = true;
                        _view.CargoType = trailerResponse.ItemArray[3].ToString();
                    }
                    else
                    {
                        _view.LblTrailerExistsVisibility = true;
                        _view.BtnAddTrailerVisibility = true;
                        _view.EdtCommodityReadOnly = false;
                        _view.CargoType = "";
                    }

                    break;
            }
        }

        public void AddPersonInvolved()
        {
            //int errors = 0;

            //if (_view.IPDriver)
            //{
            //    if (_view.IPDriverLicense.Length == 0)
            //    {
            //        _view.EdtLicenseBorder = BorderStyles.Simple;
            //        _view.EdtLicenseShowWarningIcon = true;
            //        errors += 1;
            //    }
            //    else
            //    {
            //        _view.EdtLicenseBorder = BorderStyles.Default;
            //        _view.EdtLicenseShowWarningIcon = false;
            //    }
            //}
            //else
            //{
            //    if (errors > 5) { errors -= 1; }
            //}
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
                if (errors > 5) { errors -= 1; }
            }

            if (errors == 0)
            {
                _view.LblEmptyFieldsVisibility = false;
                _PersonsInvolved[_selectedPerson] = new PersonsInvolved(_view.IPFullName, _view.IPLastName1, _view.IPPhoneNumber, _view.IPAge, _view.IPDriver, _view.IPDriverLicense, _view.IPPrivate, _view.IPInjured, Guid.Empty.ToString());
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

        public void FindDriverInSamsara()
        {
            Models.Samsara.Driver driver = new Models.Samsara.Driver { Name = _view.FullName, Phone = _view.PhoneNumber};
            SamsaraService.FindDriver(driver);
            //var Driver_Response = DriverService.GetDriver(_view.FullName);

            //if (Driver_Response == null)
            //{
            //    Utils.ShowMessage("There is no driver with that search information", title: "Driver Error", type: "Warning");
            //}
            //else
            //{
            //    ID_Driver = Driver_Response.ID_Driver.ToString();
            //    _view.FullName = Driver_Response.Name + " " + Driver_Response.LastName1;
            //    _view.PhoneNumber = Driver_Response.PhoneNumber;
            //    _view.License = Driver_Response.License;

            //    if (Driver_Response.ExpirationDate != null)
            //        _view.ExpirationDate = (DateTime)Driver_Response.ExpirationDate;
            //    else
            //    {
            //        _view.ExpirationDate = DateTime.Now;
            //        _DriverUpdateRequired = true;
            //    }

            //    _view.LicenseState = Driver_Response.ID_StateOfExpedition;
            //}

        }

        private int validate(bool validation, ref BorderStyles border)
        {
            
            return validation ? 1 : 0;
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
                            return (false, "","");
                        }


                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Utils.ShowMessage(ex.Message, title: "Image upload error", type: "Error");
                        return (false,"", "");
                    }
                }
                else
                {
                    return (false,"", "");
                }



            }
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
