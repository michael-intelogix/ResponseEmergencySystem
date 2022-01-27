using Newtonsoft.Json.Linq;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Models.Samsara;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponseEmergencySystem.Services
{
    public static class SamsaraService
    {
        public static Driver FindDriver(Driver driver)
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

                    List<Driver> drivers = data.Select(p => new Driver
                    {
                        Name = p["name"].ToString().Trim(),
                        Phone = (string)p["phone"],
                        LicenseNumber = (string)p["licenseNumber"],
                        LicenseState = (string)p["TX"],
                        ID_Samsara = (string)p["id"]
                    }).ToList();

                    var filtered = drivers.Where(x => x.Name.Contains(driver.Name) || x.Phone == driver.Phone || x.LicenseNumber == driver.LicenseNumber);

                    foreach (var item in filtered)
                    {
                        Debug.WriteLine(item.Name);

                        //Testing samsara = new Testing(item.name, item.time, item.latitude, item.longitude, item.heading, item.speed, item.formattedLocation);
                        //samsara.Show();

                    }


                    //Dispose once all HttpClient calls are complete.This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
                    client.Dispose();
                }

                return driver;
            }
            catch (Exception ex)
            {
                Utils.ShowMessage(ex.Message, title: "System Error", type: "Error");
                return driver;
            }
        }

        public static void UpdateSamsaraDrivers()
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

                    List<Driver> drivers = data.Select(p => new Driver
                    {
                        Name = p["name"].ToString().Trim(),
                        Phone = (string)p["phone"],
                        LicenseNumber = (string)p["licenseNumber"],
                        LicenseState = (string)p["licenseState"],
                        ID_Samsara = (string)p["id"]
                    }).ToList();

                    foreach (var item in drivers)
                    {
                        //if (item.LicenseState == "")
                        //    Debug.WriteLine(item.LicenseState);
                        AddDriverToSamsaraTable(item);
                    }
                    Debug.WriteLine("FINISHED");

                    //Dispose once all HttpClient calls are complete.This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
                    client.Dispose();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowMessage(ex.Message, title: "System Error", type: "Error");
                
            }
        }

        private static void AddDriverToSamsaraTable(Driver driver)
        {
            try 
            { 
                using(SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"Update_SamsaraDriver",
                    CommandType = CommandType.StoredProcedure
                })
                    {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                    cmd.Connection.Open();

                    cmd.Parameters.AddWithValue("@ID_Samsara", driver.ID_Samsara == null ? "" : driver.ID_Samsara);
                    cmd.Parameters.AddWithValue("@Name", driver.Name == null ? "" : driver.Name);
                    cmd.Parameters.AddWithValue("@PhoneNumber", driver.Phone == null ? "" : driver.Phone);
                    cmd.Parameters.AddWithValue("@LicenseNumber", driver.LicenseNumber == null ? "" : driver.LicenseNumber);
                    cmd.Parameters.AddWithValue("@LicenseState", driver.LicenseState == null ? "" : driver.LicenseState);
                    cmd.Parameters.AddWithValue("@IsDeactivated", false);

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr == null)
                        {
                            throw new NullReferenceException("No Information Available.");
                        }
                        while (sdr.Read())
                        {
                            Debug.WriteLine((string)sdr["msg"]);
                            if ((int)sdr["Validacion"] == 0)
                            {
                                Debug.WriteLine(driver.LicenseState);
                                Debug.WriteLine((string)sdr["msg"]);
                            }
                        }
                    }
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                //MessageBox.Show($"Driver couldn't be found due: {ex.Message}");
            }
}

        public static void UpdateSamsaraVehicles()
        {
            const string url = "https://api.samsara.com/fleet/vehicles";

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

                    List<TruckTrailer> vehicles = data.Select(p => new TruckTrailer
                    (
                        p["name"].ToString().Trim(),
                        (string)p["vin"],
                        (string)p["serial"],
                        (string)p["make"],
                        (string)p["model"],
                        (string)p["year"],
                        (string)p["licensePlate"],
                        (string)p["id"]
                    )).ToList();

                    foreach (var item in vehicles)
                    {
                        //if (item.LicenseState == "")
                        //    Debug.WriteLine(item.LicenseState);
                        AddTruckTrailerToSamsaraTable(item);
                    }
                    Debug.WriteLine("FINISHED");

                    //Dispose once all HttpClient calls are complete.This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
                    client.Dispose();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowMessage(ex.Message, title: "System Error", type: "Error");

            }
        }

        private static void AddTruckTrailerToSamsaraTable(TruckTrailer vehicle)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"Update_SamsaraTruckTrailer",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                    cmd.Connection.Open();

                    cmd.Parameters.AddWithValue("@ID_Samsara", vehicle.ID_Samsara);
                    cmd.Parameters.AddWithValue("@Name", vehicle.Name == null ? "" : vehicle.Name);
                    cmd.Parameters.AddWithValue("@Vin", vehicle.VinNumber == null ? "" : vehicle.VinNumber);
                    cmd.Parameters.AddWithValue("@Serial", vehicle.SerialNumber == null ? "" : vehicle.SerialNumber);
                    cmd.Parameters.AddWithValue("@Make", vehicle.Make == null ? "" : vehicle.Make);
                    cmd.Parameters.AddWithValue("@Model", vehicle.Model == null ? "" : vehicle.Model);
                    cmd.Parameters.AddWithValue("@Year", vehicle.Year == null ? "" : vehicle.Year);
                    cmd.Parameters.AddWithValue("@LicensePlate", vehicle.LicensePlate == null ? "" : vehicle.LicensePlate);

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr == null)
                        {
                            throw new NullReferenceException("No Information Available.");
                        }
                        while (sdr.Read())
                        {
                            Debug.WriteLine((string)sdr["msg"]);

                            if ((int)sdr["Validacion"] == 0)
                            {
                                Debug.WriteLine(vehicle.Name);
                                Debug.WriteLine((string)sdr["msg"]);
                            }
                        }
                    }
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                //MessageBox.Show($"Driver couldn't be found due: {ex.Message}");
            }
        }

        public static void UpdateDriverSamsara(string ID)
        {
            string url = "https://api.samsara.com/fleet/drivers/" + ID;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "samsara_api_XwURzQhn0F9rijd0vqXwDgWir2zLWc");

                // List data response.
                HttpResponseMessage response = client.GetAsync(url).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.}

                var data = JObject.Parse(response.Content.ReadAsStringAsync().Result);

                Driver d = new Driver
                {
                    Name = data["data"]["name"].ToString().Trim(),
                    Phone = (string)data["data"]["phone"],
                    LicenseNumber = (string)data["data"]["licenseNumber"],
                    LicenseState = (string)data["data"]["licenseState"],
                    ID_Samsara = (string)data["data"]["id"]
                };

                //Dispose once all HttpClient calls are complete.This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
                client.Dispose();

                AddDriverToSamsaraTable(d);
            }
        }

        public static void UpdateTruckSamsara(string ID)
        {
            string url = "https://api.samsara.com/fleet/vehicles/" + ID;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "samsara_api_XwURzQhn0F9rijd0vqXwDgWir2zLWc");

                // List data response.
                HttpResponseMessage response = client.GetAsync(url).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.}

                var data = JObject.Parse(response.Content.ReadAsStringAsync().Result);

                TruckTrailer t = new TruckTrailer
                (
                    data["data"]["name"].ToString().Trim(),
                    (string)data["data"]["vin"],
                    (string)data["data"]["serial"],
                    (string)data["data"]["make"],
                    (string)data["data"]["model"],
                    (string)data["data"]["year"],
                    (string)data["data"]["licensePlate"],
                    (string)data["data"]["id"]
                );

                //Dispose once all HttpClient calls are complete.This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
                client.Dispose();

                AddTruckTrailerToSamsaraTable(t);
            }
        }

        public static List<TruckTrailer> List_SamsaraTrucks()
        {

            List<TruckTrailer> result = new List<TruckTrailer>();
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"List_TrucksSamsara",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Parameters.AddWithValue("@active", true);

                    cmd.Connection.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr == null)
                        {
                            throw new NullReferenceException("No Information Available.");
                        }

                        while (sdr.Read())
                        {

                            result.Add(
                               new TruckTrailer(
                                   (string)sdr["Name"],
                                   (string)sdr["VinNumber"],
                                   (string)sdr["SerialNumber"],
                                   (string)sdr["Make"],
                                   (string)sdr["Model"],
                                   (string)sdr["Year"],
                                   (string)sdr["LicensePlate"],
                                   (string)sdr["ID_Samsara"]
                               )
                           );
                        }
                    }
                    cmd.Connection.Close();

                }

                return result;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Driver couldn't be found due: {ex.Message}");

                return new List<TruckTrailer>();
            }
        }
    }
}
