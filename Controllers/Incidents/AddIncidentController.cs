using Newtonsoft.Json.Linq;
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

namespace ResponseEmergencySystem.Controllers.Incidents
{
    public class AddIncidentController
    {
        IAddIncidentView _view;
        Incident _selectedIncident;
        DataTable dt_InjuredPersons = new DataTable();

        private string ID_Driver;
        private string ID_Broker;
        private string ID_Truck;
        private string ID_Trailer;
        private string comments = "";

        public AddIncidentController(IAddIncidentView view)
        {
            _view = view;
            view.SetController(this);
        }

        public Incident Incident
        {
            get { return _selectedIncident; }
        }

        public void LoadIncident()
        {

        }

        public void LoadStates()
        {
            _view.LoadStates(Functions.getStates());
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
            string name = _view.FullName;
            var Driver_Response = DriverService.GetDriver(_view.License, _view.PhoneNumber, _view.FullName);


            ID_Driver = Driver_Response.ID_Driver.ToString();
            _view.FullName = Driver_Response.Name + " " + Driver_Response.LastName1;
            _view.PhoneNumber = Driver_Response.PhoneNumber;
            _view.License = Driver_Response.License;
            _view.ExpirationDate = ((DateTime)Driver_Response.ExpirationDate).Date;
            _view.LicenseState = Driver_Response.ID_StateOfExpedition;
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

        public void AddIncident()
        {
            DataRow folioReponse = Functions.Get_Folio().Select().First();
            string folio = folioReponse.ItemArray[2].ToString() + "-" + folioReponse.ItemArray[3].ToString();
            
            //check location refreces

            IncidentService.AdddIncident(
                ID_Driver.ToUpper(),
                _view.ID_State,
                _view.ID_City,
                ID_Broker.ToUpper(),
                ID_Truck,
                ID_Trailer,
                folio,
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

            /*MessageBox.Show(IncidentService.response.Message);*/
        }


    }
}
