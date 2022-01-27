﻿using DevExpress.XtraEditors.Controls;
using Firebase.Storage;
using Newtonsoft.Json.Linq;
using ResponseEmergencySystem.Builders;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Forms;
using ResponseEmergencySystem.Forms.Modals;
using ResponseEmergencySystem.Models;
using ResponseEmergencySystem.Properties;
using ResponseEmergencySystem.Reports;
using ResponseEmergencySystem.Services;
using ResponseEmergencySystem.Views.Incidents;
using ResponseEmergencySystem.Views.Incidents.Containers;
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
        protected IIncidentView _view;
        Builders.Incident _selectedIncident;
        Employee _selectedDriver;
        Vehicle _selectedTruck;
        Vehicle _selectedTrailer;

        private frm_BrokerList _brokerView = new frm_BrokerList();
        private BrokerController _brokerCtrl = null;

        List<PersonsInvolved> _PersonsInvolved;

        private List<Employee> _DriversLocal = new List<Employee>();
        private List<Vehicle> _trucks = new List<Vehicle>();
        private List<Vehicle> _trailers = new List<Vehicle>();
        private List<Vehicle> _vehicles = new List<Vehicle>();
        private List<Broker> _brokerList = null;

        private List<Models.Logs.Log> _logs = new List<Models.Logs.Log>();

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
            _trucks = VehicleService.list_Trucks();
            _trailers = VehicleService.list_Trailers();
            _vehicles = VehicleService.list_Vehicles();

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

            _view.LoadStates(GeneralService.list_States());
            _view.DriversDataSource = _DriversLocal;
            //var samsaraDrivers = _DriversLocal.Where(x => x.ID == Guid.Empty).ToList();
            _view.TrucksDataSource = _trucks;
            _view.MailDirectoryCategoriesDataSource = MailDirectoryService.GetCategories();

            _brokerList = BrokerService.list_Brokers();
            _brokerCtrl = new BrokerController(_brokerView, _brokerList);
            _brokerCtrl.LoadBrokers();
        }

        public bool IsNewDriver()
        {
            //_selectedIncident.isNew
            return true;
        }

        public void LoadIncident()
        {
            
            _selectedIncident = IncidentService.GetIncident(ID_Incident);

            Folio = _selectedIncident.Folio;
            _view.Folio = Folio;
            _view.ClaimNumber = _selectedIncident.ClaimNumber;

            #region driver information
            _selectedDriver = _DriversLocal.Where(d => d.PhoneNumber == _selectedIncident.Driver.PhoneNumber).FirstOrDefault();
            _view.DriverID = _selectedDriver.ID.ToString();
            #endregion region

            #region trucks information
            ID_Truck = _selectedIncident.Truck.ID.ToString();
            _selectedTruck = _trucks.Where(t => t.Name == _selectedIncident.Truck.Name).FirstOrDefault();
            _truckTrailerView.ID_Truck = _selectedTruck.ID.ToString();
            _truckTrailerView.TruckDamage = _selectedIncident.Truck.vehicleStatus.Damage;
            _truckTrailerView.TruckNeedCrane = _selectedIncident.Truck.vehicleStatus.NeedCrane;
            _truckTrailerView.TruckCanMove = _selectedIncident.Truck.vehicleStatus.CanMove;
            _truckTrailerView.TruckBroker = _selectedIncident.Truck.vehicleStatus.Broker;
            #endregion

            #region trailers information
            ID_Trailer = _selectedIncident.Trailer.ID.ToString();
            var trailer = _trailers.Where(t => t.Name == _selectedIncident.Trailer.Name).FirstOrDefault();
            _truckTrailerView.ID_Trailer = trailer.ID.ToString();
            _truckTrailerView.TrailerDamage = _selectedIncident.Trailer.vehicleStatus.Damage;
            _truckTrailerView.TrailerNeedCrane = _selectedIncident.Trailer.vehicleStatus.NeedCrane;
            _truckTrailerView.TrailerCanMove = _selectedIncident.Trailer.vehicleStatus.CanMove;
            _truckTrailerView.TrailerBroker = _selectedIncident.Trailer.vehicleStatus.Broker;
            _truckTrailerView.TrailerCargoSpill = _selectedIncident.Trailer.vehicleStatus.CargoSpill;
            _truckTrailerView.TrailerBOL = _selectedIncident.Trailer.vehicleStatus.BOL;
            #endregion

            #region Location Information
            _view.Latitude = _selectedIncident.Location.Latitude;
            _view.Longitude = _selectedIncident.Location.Longitude;
            _view.LocationReferences = _selectedIncident.Location.Description;
            _view.ID_State = _selectedIncident.Location.ID_State;
            _view.ID_City = _selectedIncident.Location.ID_City;
            #endregion

            #region Accident Details
            _view.IncidentDate = _selectedIncident.IncidentDate;
            _view.PoliceReport = _selectedIncident.PoliceReport;
            _view.CitationReportNumber = _selectedIncident.CitationReportNumber;
            #endregion

            #region Involved Persons
            if (_PersonsInvolved.Count > 0)
                _view.InvolvedPersonsDataSource = _PersonsInvolved;
            #endregion
        }

        public void GetCitiesByState()
        {
            var cities = GeneralService.list_Cities(_view.ID_State);
            _view.LueCitiesDataSource = cities;
        }

        public void GetDriver(string ID, bool disableAction = false)
        {
            if (ID == "" || disableAction)
                return;
            
            _selectedDriver = _DriversLocal.Where(d => d.ID == Guid.Parse(ID)).FirstOrDefault();

            ID_Driver = _selectedDriver.ID.ToString();
            _ID_Samsara = _selectedDriver.ID_Samsara.ToString();
            _DriverName = _selectedDriver.Name + " " + _selectedDriver.LastName1;
            _view.FullName = _DriverName;
            _view.PhoneNumber = _selectedDriver.PhoneNumber;
            _view.License = _selectedDriver.License;

            _view.ExpirationDate = DateTime.Now;
            //if (_selectedDriver.ExpirationDate != null)
            //    _view.ExpirationDate = (DateTime)_selectedDriver.ExpirationDate;
            //else
            //{
            //    _view.ExpirationDate = DateTime.Now;
            //}

            //_view.LicenseState = _selectedDriver.ID_StateOfExpedition;
        }

        public void GetBroker()
        {
            if (_brokerView.ShowDialog() == DialogResult.OK)
            {
                _truckTrailerView.TruckBroker = _brokerView.broker;
                _selectedTruck = _trucks.Where(t => t.ID == Guid.Parse(_truckTrailerView.ID_Truck)).FirstOrDefault();
                _selectedTruck.SetBroker(_brokerView.ID);
            }
        }

        public void GetBroker2()
        {
            if (_brokerView.ShowDialog() == DialogResult.OK)
            {
                _truckTrailerView.TrailerBroker = _brokerView.broker;
                _selectedTrailer = _trailers.Where(t => t.ID == Guid.Parse(_truckTrailerView.ID_Trailer)).FirstOrDefault();
                _selectedTrailer.SetBroker(_brokerView.ID);
            }
        }
        public async Task UpdateAsync()
        {
            try
            {
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

                // Add Employee if not in samsara 
                var driver = _DriversLocal.Where(dl => dl.ID == Guid.Parse(ID_Driver)).First();

                #region vehicles services
            
                var truck = _trucks.Where(v => v.ID == Guid.Parse(_truckTrailerView.ID_Truck)).First();
                truck.SetVehicleStatus(_truckTrailerView.TruckDamage, _truckTrailerView.TruckCanMove, _truckTrailerView.TruckNeedCrane);

                var trailer = _trailers.Where(v => v.ID == Guid.Parse(_truckTrailerView.ID_Trailer)).First();
                trailer.SetVehicleStatus(_truckTrailerView.TrailerDamage, _truckTrailerView.TrailerCanMove, _truckTrailerView.TrailerNeedCrane);
                trailer.SetCargoStatus(_truckTrailerView.TrailerCargoSpill, _truckTrailerView.TrailerBOL);
                #endregion

                // prototype
                Builders.Incident incident = _selectedIncident.ShallowCopy();

                incident.ID_Incident = Guid.Parse(ID_Incident);
                incident.Folio = Folio;
                incident.ClaimNumber = _view.ClaimNumber;
                incident.PoliceReport = _view.PoliceReport;
                incident.CitationReportNumber = _view.CitationReportNumber;
                incident.ManifestNumber = _view.ManifestNumber;
                incident.IncidentDate =_view.IncidentDate;
                incident.Location = new Builders.Location(_view.ID_State, _view.ID_City, _view.Latitude, _view.Longitude, _view.LocationReferences);
                incident.Truck = _trucks.Where(t => t.ID == Guid.Parse(_truckTrailerView.ID_Truck)).FirstOrDefault();
                incident.Trailer = _trailers.Where(t => t.ID == Guid.Parse(_truckTrailerView.ID_Trailer)).FirstOrDefault();
                incident.Driver = _selectedDriver;

                var incidentRes = await IncidentService.update_TruckTrailerIncident(incident, trailer, truck, driver, _PersonsInvolved, _view.Documents, true);

                //CheckDiscrepancies(t.Result.ID);



                //foreach(var log in _logs)
                //{

                //}

                //if (_view.SendAfterSave && ID_Incident != "")
                //{
                //    SendEmail();
                //    _view.ViewClose();
                //}
                //else
                //{
                //    _view.ViewClose();
                //}

                _view.ViewClose();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return;
        }

        public void CheckDiscrepancies(string ID)
        {
   

                    //ID_Incident,
                    //_view.NewDriver? t2.Result.ID.ToString() : ID_Driver,
                    //_DriverName,
                    //_view.ID_State,
                    //_view.ID_City,
                    //ID_Broker.ToUpper(),
                    //ID_Broker2,
                    //ID_Truck,
                    //_view.TruckNumber == null ? "" : _view.TruckNumber.ToString(),
                    //_view.TrailerNumber,
                    //_view.CargoType,
                    //_view.IncidentDate,
                    //_view.PoliceReport,
                    //_view.CitationReportNumber,
                    //_view.CargoSpill,
                    //_view.ManifestNumber,
                    //_view.LocationReferences,
                    //_view.Latitude,
                    //_view.Longitude,
                    //_view.TruckDamages,
                    //_view.TruckCanMove,
                    //_view.TruckNeedCrane,
                    //_view.TrailerDamages,
                    //_view.TrailerCanMove,
                    //_view.TrailerNeedCrane,
                    //constants.userID,
                    //_view.Comments == null ? "" : _view.Comments.ToString(),
                    //ID_Driver == Guid.Empty.ToString()
        }

        private void SaveCapture(string captureId, string captureType, string incident)
        {
            var response = CaptureService.AddCapture(captureId, captureType, incident, "testing", "");
            ID_Capture = response.ID;
        }

        public async Task AddIncidentAsync()
        {

            try
            {
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

                // Add Employee if not in samsara 
                var driver = _DriversLocal.Where(dl => dl.ID == Guid.Parse(ID_Driver)).First();

                #region vehicles services

                var truck = _trucks.Where(v => v.ID == Guid.Parse(_truckTrailerView.ID_Truck)).First();
                truck.SetVehicleStatus(_truckTrailerView.TruckDamage, _truckTrailerView.TruckCanMove, _truckTrailerView.TruckNeedCrane);

                var trailer = _trailers.Where(v => v.ID == Guid.Parse(_truckTrailerView.ID_Trailer)).First();
                trailer.SetVehicleStatus(_truckTrailerView.TrailerDamage, _truckTrailerView.TrailerCanMove, _truckTrailerView.TrailerNeedCrane);
                trailer.SetCargoStatus(_truckTrailerView.TrailerCargoSpill, _truckTrailerView.TrailerBOL);
                #endregion

                DataRow folioReponse = Functions.Get_Folio().Select().First();
                Folio = folioReponse.ItemArray[2].ToString() + "-" + folioReponse.ItemArray[3].ToString();

                Builders.Incident incident = new IncidentBuilder()
                                                    .SetID(Guid.Parse(ID_Incident))
                                                    .SetFolio(Folio)
                                                    .SetClaimNumber(_view.ClaimNumber)
                                                    .HasPoliceReport(_view.PoliceReport, _view.CitationReportNumber)
                                                    .SetOpenDate(_view.IncidentDate)
                                                    .SetLocation(
                                                        new Builders.Location(
                                                            _view.ID_State, 
                                                            _view.ID_City, 
                                                            _view.Latitude, 
                                                            _view.Longitude,
                                                            _view.LocationReferences
                                                        )
                                                    )
                                                    .SetComments(_view.Comments == null ? "" : _view.Comments.ToString())
                                                    .Build();

                var incidentRes = await IncidentService.update_TruckTrailerIncident(incident, trailer, truck, driver, _PersonsInvolved, _view.Documents);

                //CheckDiscrepancies(t.Result.ID);



                //foreach(var log in _logs)
                //{

                //}

                //if (_view.SendAfterSave && ID_Incident != "")
                //{
                //    SendEmail();
                //    _view.ViewClose();
                //}
                //else
                //{
                //    _view.ViewClose();
                //}
                _view.ViewClose();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return;

            //if (_view.SendAfterSave)
            //{
            //    if (_view.SelectedMail == "" && !_view.SendToAllRecipientsInTheCategory)
            //    {
            //        _view.MailValidationBorder = BorderStyles.Simple;
            //        Utils.ShowMessage("Please select a mail before submit the changes", "Mail Error", type: "Warning");
            //        return;
            //    }

            //    if (_view.MailDirectoryCategory == "" && _view.SendToAllRecipientsInTheCategory)
            //    {
            //        _view.CategoryValidationBorder = BorderStyles.Simple;
            //        Utils.ShowMessage("Please select a category before submit the changes", "Mail Error", type: "Warning");
            //        return;
            //    }
            //}
            

            //if (_view.SendAfterSave && ID_Incident != "")
            //{
            //    SendEmail();
            //    _view.ViewClose();
            //}
            //else
            //{
            //    _view.ViewClose();
            //}
        }

        public void GetTruckSamsara()
        {
            if (_truckTrailerView.ID_Truck == "")
                return;

            double latitude = 0;
            double longitude = 0;
            string samsaraId = _trucks.Where(t => t.ID == Guid.Parse(_truckTrailerView.ID_Truck)).FirstOrDefault().ID_Samsara;
            string url = "https://api.samsara.com/fleet/vehicles/locations/feed?vehicleIds=" + samsaraId;

            if (samsaraId != "")
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

                        //var filtered = locs.Where(x => x.name == number);
                        if (data.Count > 0)
                        {
                            latitude = (double)data[0]["locations"][0]["latitude"];
                            longitude = (double)data[0]["locations"][0]["longitude"];
                            _view.Latitude = latitude.ToString();
                            _view.Longitude = longitude.ToString();
                            _view.LocationReferences = (string)data[0]["locations"][0]["reverseGeo"]["formattedLocation"];
                            _view.SetMap(new double[] { latitude, longitude });
                        }

                        //Dispose once all HttpClient calls are complete.This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
                        client.Dispose();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
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
                case "ckedt_NewDriver":
                    _view.DriverInformationReadOnly(ckedtValue);
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

            Builders.Incident incident = null;
            if (generate)
            {
                
                var t = new Task(() =>
                {
                    incident = IncidentService.GetIncident(ID_Incident);
                });
                t.Start();
                t.Wait();

            }

            IncidentReport report1 = new IncidentReport(_selectedIncident);
            //IncidentReport report1 = new IncidentReport(null);
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

        #region Add Employee system
        public bool AddEmployee()
        {
            //var emp = _DriversLocal.Where(dl => dl.ID == Guid.Parse(ID_Driver)).First();

            var emp = new EmployeeBuilder()
                    .Called(_view.FullName)
                    .PhoneNumber(_view.PhoneNumber)
                    .LicenseNumber(_view.License)
                    .NewEmployee(true)
                    .Build();

            _DriversLocal.Add(emp);
                
            _view.DriversDataSource = _DriversLocal;
            _view.DriverID = emp.ID.ToString();
            ID_Driver = emp.ID.ToString();
            return true;
            //Utils.ShowMessage($"Status: {emp.Status}, Value: {emp.PhoneNumber}");
        }

        public void UpdateEmployeeInfo(string newData, string controlName, bool isUpdate = false)
        {
            if (isUpdate)
                return;

            var emp = _DriversLocal.Where(dl => dl.ID == Guid.Parse(ID_Driver)).First();
            switch (controlName)
            {
                case "edt_PhoneNumber":
                    if (!emp.ValidatePhoneNumber(newData))
                        _view.PhoneNumber = emp.PhoneNumber;
                    break;
                case "edt_License":
                    if (!emp.ValidateLicense(newData))
                        _view.License = emp.License;
                    break;
            }

            
        }
        #endregion

        #region trucktrailers view
        protected ITrucksTrailersView _truckTrailerView;
        public void SetTruckTrailerView(ITrucksTrailersView view)
        {
            _truckTrailerView = view;
        }
        #endregion

        #region trucks
        public void LoadTrucks()
        {
            _truckTrailerView.TrucksDataSource = _trucks;
        }

        public void SetTruckInfo()
        {
            var ID = _truckTrailerView.ID_Truck;
            if (ID == "")
                return;

            var truck = _trucks.Where(t => t.ID == Guid.Parse(ID)).First();
            _truckTrailerView.TruckName = truck.Name;
            _truckTrailerView.TruckVinNumber = truck.VinNumber;
            _truckTrailerView.TruckSerialNumber = truck.SerialNumber;
            _truckTrailerView.TruckMake = truck.Make;
            _truckTrailerView.TruckModel = truck.Model;
            _truckTrailerView.TruckYear = truck.Year.ToString();
            _truckTrailerView.TruckLicensePlate = truck.LicensePlate;
        }

        public void UpdateTruckInfo()
        {
            var truck = _trucks.Where(t => t.ID == Guid.Parse(_truckTrailerView.ID_Truck)).First();
            truck.ValidateVinNumber(_truckTrailerView.TruckVinNumber);
            truck.ValidateSerialNumber(_truckTrailerView.TruckSerialNumber);
            truck.ValidateMake(_truckTrailerView.TruckMake);
            truck.ValidateModel(_truckTrailerView.TruckModel);
            truck.ValidateYear(_truckTrailerView.TruckYear);
            truck.ValidateLicensePlate(_truckTrailerView.TruckLicensePlate);

            _truckTrailerView.TrucksDataSource = _trucks;
        }
        
        public void Newtruck()
        {

            _truckTrailerView.TruckName = "";
            _truckTrailerView.TruckVinNumber = "";
            _truckTrailerView.TruckSerialNumber = "";
            _truckTrailerView.TruckMake = "";
            _truckTrailerView.TruckModel = "";
            _truckTrailerView.TruckYear = "";
            _truckTrailerView.TruckLicensePlate = "";

        }

        public void AddTruck()
        {
            var newTruck = new VehicleBuilder()
                .SetName(_truckTrailerView.TruckName)
                .SetVinNumber(_truckTrailerView.TruckVinNumber)
                .SetSerialNumber(_truckTrailerView.TruckSerialNumber)
                .SetMake(_truckTrailerView.TruckMake)
                .SetModel(_truckTrailerView.TruckModel)
                .SetYear(_truckTrailerView.TruckYear)
                .SetLicensePlate(_truckTrailerView.TruckLicensePlate)
                .VehicleType("truck")
                .NewVehicle()
                .Build();

            newTruck.SetNewVehicle();
            _trucks.Add(newTruck);

            _truckTrailerView.TrucksDataSource = _trucks;
            _truckTrailerView.ID_Truck = newTruck.ID.ToString();
        }
        #endregion

        #region Trailers system
        public void LoadTrailers()
        {
            _truckTrailerView.TrailersDataSource = _trailers;
        }

        public void SetTrailerInfo()
        {
            var ID = _truckTrailerView.ID_Trailer;
            if (ID == "")
                return;

            var trailer = _trailers.Where(t => t.ID == Guid.Parse(ID)).First();
            _truckTrailerView.TrailerName = trailer.Name;
            _truckTrailerView.TrailerVinNumber = trailer.VinNumber;
            _truckTrailerView.TrailerSerialNumber = trailer.SerialNumber;
            _truckTrailerView.TrailerMake = trailer.Make;
            _truckTrailerView.TrailerModel = trailer.Model;
            _truckTrailerView.TrailerYear = trailer.Year.ToString();
            _truckTrailerView.TrailerLicensePlate = trailer.LicensePlate;
            _truckTrailerView.TrailerCargoType = trailer.Commodity;
        }

        public void UpdateTrailerInfo()
        {
            var trailer = _trailers.Where(t => t.ID == Guid.Parse(_truckTrailerView.ID_Trailer)).First();
            trailer.ValidateVinNumber(_truckTrailerView.TrailerVinNumber);
            trailer.ValidateSerialNumber(_truckTrailerView.TrailerSerialNumber);
            trailer.ValidateMake(_truckTrailerView.TrailerMake);
            trailer.ValidateModel(_truckTrailerView.TrailerModel);
            trailer.ValidateYear(_truckTrailerView.TrailerYear);
            trailer.ValidateLicensePlate(_truckTrailerView.TrailerLicensePlate);
            trailer.ValidateCommodity(_truckTrailerView.TrailerCargoType); 

            LoadTrailers();
        }

        public void NewTrailer()
        {
            _truckTrailerView.TrailerName = "";
            _truckTrailerView.TrailerVinNumber = "";
            _truckTrailerView.TrailerSerialNumber = "";
            _truckTrailerView.TrailerMake = "";
            _truckTrailerView.TrailerModel = "";
            _truckTrailerView.TrailerYear = "";
            _truckTrailerView.TrailerLicensePlate = "";
            _truckTrailerView.TrailerCargoType = "";
        }

        public void AddTrailer()
        {
            var newTrailer = new VehicleBuilder()
                .SetName(_truckTrailerView.TrailerName)
                .SetVinNumber(_truckTrailerView.TrailerVinNumber)
                .SetSerialNumber(_truckTrailerView.TrailerSerialNumber)
                .SetMake(_truckTrailerView.TrailerMake)
                .SetModel(_truckTrailerView.TrailerModel)
                .SetYear(_truckTrailerView.TrailerYear)
                .SetLicensePlate(_truckTrailerView.TrailerLicensePlate)
                .SetCommodity(_truckTrailerView.TrailerCargoType)
                .VehicleType("trailer")
                .NewVehicle()
                .Build();

            newTrailer.SetNewVehicle();
            _trailers.Add(newTrailer);

            LoadTrailers();
            _truckTrailerView.ID_Trailer = newTrailer.ID.ToString();
        }
        #endregion

        #region vehicles view
        IVehiclesView _vehiclesView;
        public void SetVehiclesView(IVehiclesView view)
        {
            _vehiclesView = view;
        }
        #endregion

        #region vehicles
        public void LoadVehicles()
        {
            _vehiclesView.VehiclesDataSource = _vehicles;
        }

        public void NewVehicle()
        {
            _vehiclesView.VehicleName = "";
            _vehiclesView.VinNumber = "";
            _vehiclesView.Serialnumber = "";
            _vehiclesView.Make = "";
            _vehiclesView.Model = "";
            _vehiclesView.Year = "";
            _vehiclesView.LicensePlate = "";
        }

        public void AddVehicle()
        {
            var newVehicle = new VehicleBuilder()
                .SetName(_vehiclesView.VehicleName)
                .SetVinNumber(_vehiclesView.VinNumber)
                .SetSerialNumber(_vehiclesView.Serialnumber)
                .SetMake(_vehiclesView.Make)
                .SetModel(_vehiclesView.Model)
                .SetYear(_vehiclesView.Year)
                .SetLicensePlate(_vehiclesView.LicensePlate)
                .VehicleType("trailer")
                .NewVehicle()
                .Build();

            newVehicle.SetNewVehicle();
            _vehicles.Add(newVehicle);

            LoadVehicles();
            _vehiclesView.ID_Vehicle = newVehicle.ID.ToString();
        }

        public void SetVehicleInfo()
        {
            var ID = _vehiclesView.ID_Vehicle;
            if (ID == "")
                return;

            var vehicle = _vehicles.Where(v => v.ID == Guid.Parse(ID)).First();
            _vehiclesView.VehicleName = vehicle.Name;
            _vehiclesView.VinNumber = vehicle.VinNumber;
            _vehiclesView.Serialnumber = vehicle.SerialNumber;
            _vehiclesView.Make = vehicle.Make;
            _vehiclesView.Model = vehicle.Model;
            _vehiclesView.Year = vehicle.Year.ToString();
            _vehiclesView.LicensePlate = vehicle.LicensePlate;
        }

        public void UpdateVehicleInfo()
        {
            var vehicle = _vehicles.Where(v => v.ID == Guid.Parse(_vehiclesView.ID_Vehicle)).First();
            vehicle.ValidateVinNumber(_vehiclesView.VinNumber);
            vehicle.ValidateSerialNumber(_vehiclesView.Serialnumber);
            vehicle.ValidateMake(_vehiclesView.Make);
            vehicle.ValidateModel(_vehiclesView.Model);
            vehicle.ValidateYear(_vehiclesView.Year);
            vehicle.ValidateLicensePlate(_vehiclesView.LicensePlate);

            _vehiclesView.VehiclesDataSource = _vehicles;
        }
        #endregion

        #region validations
        public enum vehicleInformation
        {
            Name,
            VinNumber,
            SerialNumber,
            Make,
            Model,
            Year,
            Plate
        }

        public enum vehicleTypes
        {
            Truck,
            Trailer,
            Vehicle
        }

        public bool TransportValidate()
        {
            return _truckTrailerView.ValidateTransport();
        }


        public (bool, string) TruckNameIsRegistered(string val, vehicleInformation type, vehicleTypes vehicleType, string name)
        {
            bool exists = false;
            bool res = false;
            string msg = "This field can't be empty";
            string vehicleName = name;

            IEnumerable<Vehicle> v = null;
            List<Vehicle> vehicles = null;
            
            if(val == "")
                return (res, msg);

            switch (vehicleType)
            {
                case vehicleTypes.Truck:
                    vehicles = _trucks;
                    break;
                case vehicleTypes.Trailer:
                    vehicles = _trailers;
                    break;
            }

            switch (type)
            {
                case vehicleInformation.Name:
                    v = vehicles.Where(t => t.Name == val);
                    exists = v.Count() > 0;

                    if (exists)
                        msg = $"A { vehicleType } with this name is already registered";
                    break;
                case vehicleInformation.VinNumber:
                    v = vehicleName != null ? vehicles.Where(t => t.VinNumber == val && t.Name != vehicleName) : vehicles.Where(t => t.VinNumber == val);
                    exists = v.Count() > 0;

                    if (exists)
                        msg = $"A { vehicleType } with name { v.First().Name } already have this Vin Number";
                    break;
                case vehicleInformation.SerialNumber:
                    v = vehicleName != null ? vehicles.Where(t => t.SerialNumber == val && t.Name != vehicleName) : vehicles.Where(t => t.SerialNumber == val);
                    exists = v.Count() > 0;

                    if (exists)
                        msg = $"A { vehicleType } with name { v.First().Name } already have this Serial Number";
                    break;
                case vehicleInformation.Plate:
                    v = vehicleName != null ? vehicles.Where(t => t.LicensePlate == val && t.Name != vehicleName) : vehicles.Where(t => t.LicensePlate == val);
                    exists = v.Count() > 0;

                    if (exists)
                        msg = $"A { vehicleType } with name { v.First().Name } already have this Plate";
                    break;
            }
            
            return (!exists, msg);
        }
        #endregion 
    }
}
