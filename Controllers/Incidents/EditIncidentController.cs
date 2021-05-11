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

namespace ResponseEmergencySystem.Controllers.Incidents
{
    public class EditIncidentController
    {
        IEditIncidentView _view;
        private string ID_Incident;
        Incident _selectedIncident;
        DataTable dt_InjuredPersons = new DataTable();

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
            dt_InjuredPersons = IncidentService.list_InjuredPerson(ID_Incident);

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

            #region Accident Details
            //_view.IncidentDate = _selectedIncident.IncidentDate.Date;
            //_view.IncidentDate = _selectedIncident.IncidentDate.ToString("hh:mm:ss tt");
            _view.PoliceReport = _selectedIncident.PoliceReport;
            _view.CitationReportNumber = _selectedIncident.CitationReportNumber;
            _view.Latitude = _selectedIncident.IncidentLatitude;
            _view.Longitude = _selectedIncident.IncidentLongitude;
            _view.LocationReferences = _selectedIncident.LocationReferences;
            #endregion



            _view.LoadIncident(_selectedIncident);
            _view.LoadStates(Functions.getStates());
            if (dt_InjuredPersons.Rows.Count > 0)
                _view.LoadInjuredPersons(dt_InjuredPersons);

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

        public void GetTruckSamsara()
        {

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
                        _view.Latitude = item.latitude.ToString();
                        _view.Longitude = item.longitude.ToString();

                        Testing samsara = new Testing(item.name, item.time, item.latitude, item.longitude, item.heading, item.speed, item.formattedLocation);
                        samsara.Show();

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


        public void Update()
        {
            //DataRow folioReponse = Functions.Get_Folio().Select().First();
            //string folio = folioReponse.ItemArray[2].ToString() + "-" + folioReponse.ItemArray[3].ToString();

            //check location refreces
            try
            {
                IncidentService.UpdateIncident(
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
                    comments
                );
            } 
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


            /*MessageBox.Show(IncidentService.response.Message);*/
        }
    }
}
