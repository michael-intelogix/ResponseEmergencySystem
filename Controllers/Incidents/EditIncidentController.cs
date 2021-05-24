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

namespace ResponseEmergencySystem.Controllers.Incidents
{
    public class EditIncidentController
    {
        IEditIncidentView _view;
        private string ID_Incident;
        Incident _selectedIncident;
        DataTable dt_InjuredPersons = new DataTable();

        private Int32 _selectedPerson = 0;

        private List<PersonsInvolved> _PersonsInvolved;

        public double latitude;
        public double longitude;

        private string ID_Driver;
        private string ID_Broker;
        private string ID_Truck;
        private string ID_Trailer;
        private string comments = "";

        public EditIncidentController(IEditIncidentView view, string incidentId)
        {
            ID_Incident = incidentId;
            _view = view;
            view.SetController(this);
        }

        public Incident Incident
        {
            get { return _selectedIncident; }
        }

        public void LoadIncident()
        {
            _selectedIncident = IncidentService.list_Incidents("", "", "", "", "", incidentId: ID_Incident)[0];
            _PersonsInvolved = IncidentService.list_PersonsInvolved(ID_Incident);

            ID_Driver = _selectedIncident.driver.ID_Driver.ToString(); ;
            ID_Broker = _selectedIncident.broker.ID_Broker;
            ID_Truck = _selectedIncident.truck.ID_Truck.ToString();
            ID_Trailer = _selectedIncident.trailer.ID_Trailer.ToString();

            _view.FullName = _selectedIncident.Name;
            _view.PhoneNumber = _selectedIncident.PhoneNumber;
            _view.License = _selectedIncident.driver.License;
            _view.ExpirationDate = Convert.ToDateTime(_selectedIncident.driver.ExpirationDate).Date;
            _view.LicenseState = _selectedIncident.driver.ID_StateOfExpedition;
            _view.TruckNumber = _selectedIncident.truck.truckNumber;
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
            if (_PersonsInvolved.Count > 0)
                _view.InvolvedPersonsDataSorurce = _PersonsInvolved;

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

        public void GetDriver()
        {
            var Driver_Response = DriverService.GetDriver(_view.DriverSearch);

            if (Driver_Response == null)
                MessageBox.Show("There is no driver with that search information");
            else
            {
                ID_Driver = Driver_Response.ID_Driver.ToString();
                _view.FullName = Driver_Response.Name + " " + Driver_Response.LastName1;
                _view.PhoneNumber = Driver_Response.PhoneNumber;
                _view.License = Driver_Response.License;
                _view.ExpirationDate = ((DateTime)Driver_Response.ExpirationDate).Date;
                _view.LicenseState = Driver_Response.ID_StateOfExpedition;
            }
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
            int errors = 0;
            if (_view.IPFullName.Length == 0)
            {
                _view.EdtFullNameBorder = BorderStyles.Simple;
                _view.EdtFullNameShowWarningIcon = true;
                errors += 1;
            }
            else
            {
                _view.EdtFullNameBorder = BorderStyles.Default;
                _view.EdtFullNameShowWarningIcon = false;
            }

            if (_view.IPLastName1.Length == 0)
            {
                _view.EdtLastNameBorder = BorderStyles.Simple;
                _view.EdtLastName1ShowWarningIcon = true;
                errors += 1;
            }
            else
            {
                _view.EdtLastNameBorder = BorderStyles.Default;
                _view.EdtLastName1ShowWarningIcon = false;
            }
            //if (_view.IPPhoneNumber.Length == 0)
            //{
            //    _view.EdtPhoneNumberBorder = BorderStyles.Simple;
            //    errors += 1;
            //}
            //else
            //    _view.EdtPhoneNumberBorder = BorderStyles.Default;

            //if (_view.IPAge.Length == 0)
            //{
            //    _view.EdtAgeBorder = BorderStyles.Simple;
            //    errors += 1;
            //}
            //else
            //    _view.EdtAgeBorder = BorderStyles.Default;

            if (_view.IPDriver)
            {
                if (_view.IPLicense.Length == 0)
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
                _PersonsInvolved.Add(new PersonsInvolved(_view.IPFullName, _view.IPLastName1, _view.IPPhoneNumber, _view.IPAge, _view.IPDriver, _view.IPLicense, _view.IPPrivate, _view.IPInjured, Guid.Empty.ToString()));
                _view.InvolvedPersonsDataSorurce = _PersonsInvolved;

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
            if (_view.IPFullName.Length == 0)
            {
                _view.EdtFullNameBorder = BorderStyles.Simple;
                _view.EdtFullNameShowWarningIcon = true;
                errors += 1;
            }
            else
            {
                _view.EdtFullNameBorder = BorderStyles.Default;
                _view.EdtFullNameShowWarningIcon = false;
            }

            if (_view.IPLastName1.Length == 0)
            {
                _view.EdtLastNameBorder = BorderStyles.Simple;
                _view.EdtLastName1ShowWarningIcon = true;
                errors += 1;
            }
            else
            {
                _view.EdtLastNameBorder = BorderStyles.Default;
                _view.EdtLastName1ShowWarningIcon = false;
            }

            //if (_view.IPPhoneNumber.Length == 0)
            //{
            //    _view.EdtPhoneNumberBorder = BorderStyles.Simple;
            //    errors += 1;
            //}
            //else
            //    _view.EdtPhoneNumberBorder = BorderStyles.Default;

            //if (_view.IPAge.Length == 0)
            //{
            //    _view.EdtAgeBorder = BorderStyles.Simple;
            //    errors += 1;
            //}
            //else
            //_view.EdtAgeBorder = BorderStyles.Default;



            if (_view.IPDriver)
            {
                if (_view.IPLicense.Length == 0)
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
                _PersonsInvolved[_selectedPerson].FullName = _view.IPFullName; 
                _PersonsInvolved[_selectedPerson].LastName1 = _view.IPLastName1; 
                _PersonsInvolved[_selectedPerson].PhoneNumber = _view.IPPhoneNumber; 
                _PersonsInvolved[_selectedPerson].Age = _view.IPAge; 
                _PersonsInvolved[_selectedPerson].Driver = _view.IPDriver; 
                _PersonsInvolved[_selectedPerson].DriverLicense = _view.IPLicense;
                _PersonsInvolved[_selectedPerson].PrivatePerson = _view.IPPrivate;
                _PersonsInvolved[_selectedPerson].Injured = _view.IPInjured;

                _view.InvolvedPersonsDataSorurce = _PersonsInvolved;

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
            _view.IPLicense = person.DriverLicense;

            _view.BtnAddInvolvedPersonVisibility = false;
            _view.BtnEditInvolvedPersonVisibility = true;

            //_view.BtnEditInvolvedPersonText = "Update person";
            if (_view.BtnEditInvolvedPersonLocation.X == 13)
                _view.BtnEditInvolvedPersonLocation = new System.Drawing.Point(494, 85);
            //_view.BtnAddInvolvedPersonSize = new System.Drawing.Size(135, 23);
            
        }

        public void RemoveInvolvedPersonByRow(int idx)
        {
            _PersonsInvolved.RemoveAt(idx);
            _view.InvolvedPersonsDataSorurce = _PersonsInvolved;
        }

        public void Update()
        {
            //DataRow folioReponse = Functions.Get_Folio().Select().First();
            //string folio = folioReponse.ItemArray[2].ToString() + "-" + folioReponse.ItemArray[3].ToString();

            //check location refreces
            try
            {
                var t = new Task<Response>(() => IncidentService.UpdateIncident(
                    ID_Incident,
                    ID_Driver.ToUpper(),
                    _view.ID_State,
                    _view.ID_City,
                    ID_Broker.ToUpper(),
                    ID_Truck,
                    ID_Trailer,
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
                    _view.Comments
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
            } 
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


            /*MessageBox.Show(IncidentService.response.Message);*/
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
            _view.IPLicense = "";
        }
    }
}
