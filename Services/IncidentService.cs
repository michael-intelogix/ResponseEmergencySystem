using Newtonsoft.Json.Linq;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.EF;
using ResponseEmergencySystem.Models;
using ResponseEmergencySystem.Samsara_Models;
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
    public static class IncidentService
    {
        public static Response response;

        public static List<Incident> list_Incidents(string folio, string driverId, string driverName, string truckNum, string statusDetailId, string date1 = "", string date2 = "")
        {

            List<Incident> result = new List<Incident>();
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"List_Incidents",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Parameters.AddWithValue("@ID_Incident", Guid.Empty);
                    cmd.Parameters.AddWithValue("@Folio", constants.folioCode);
                    cmd.Parameters.AddWithValue("@ID_Driver", driverId);
                    cmd.Parameters.AddWithValue("@ID_StatusDetail", statusDetailId == "" ? "423E82C9-EE3F-4D83-9066-01E6927FE14D" : statusDetailId);
                    cmd.Parameters.AddWithValue("@DriverName", driverName);
                    cmd.Parameters.AddWithValue("@Truck_No", truckNum);
                    cmd.Parameters.AddWithValue("@Trailer_No", "");
                    cmd.Parameters.AddWithValue("@Date1", date1);
                    cmd.Parameters.AddWithValue("@Date2", date2);
                    cmd.Parameters.AddWithValue("@Tester", constants.tester);

                    cmd.Connection.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr == null)
                        {
                            throw new NullReferenceException("No Information Available.");
                        }
                        while (sdr.Read())
                        {
                            //Debug.WriteLine(sdr["IncidentCloseDate"]);

                            result.Add(
                                new Incident(
                                    (Guid)sdr["ID_Incident"],
                                    (string)sdr["Folio"],
                                    Convert.ToDateTime(sdr["IncidentDate"]),
                                    DateTime.Now,
                                    (bool)sdr["PoliceReport"],
                                    (string)sdr["CitationReportNumber"],
                                    (string)sdr["ManifestNumber"],
                                    (string)sdr["LocationReferences"],
                                    (string)sdr["IncidentLatitude"],
                                    (string)sdr["IncidentLongitude"],
                                    (string)sdr["Comments"],
                                    new Truck(Guid.Parse((string)sdr["ID_Truck"]), (string)sdr["TruckNumber"]),
                                    (bool)sdr["TruckDamage"],
                                    (bool)sdr["TruckCanMove"],
                                    (bool)sdr["TruckNeedCrane"],
                                    new Trailer(
                                        sdr["ID_Trailer"] == DBNull.Value ? Guid.Empty : Guid.Parse((string)sdr["ID_Trailer"]),
                                        sdr["TrailerNumber"] == DBNull.Value ? "" : (string)sdr["TrailerNumber"],
                                        sdr["TrailerCommodity"] == DBNull.Value ? "" : (string)sdr["TrailerCommodity"],
                                        (bool)sdr["CargoSpill"]),
                                    (bool)sdr["TrailerDamage"],
                                    (bool)sdr["TrailerCanMove"],
                                    (bool)sdr["TrailerNeedCrane"],
                                    new Driver(),
                                    //new Driver((string)sdr["ID_Driver"], (string)sdr["License"], (string)sdr["Expedition_State"], ExpirationDate: sdr["Expiration_Date"] == DBNull.Value ? "" : Convert.ToDateTime(sdr["Expiration_Date"]).Date.ToString()),
                                    (string)sdr["ID_City"],
                                    (string)sdr["ID_State"],
                                    (string)sdr["ID_Broker"],
                                    new Broker((string)sdr["ID_Broker"], (string)sdr["Broker"]),
                                    (string)sdr["ID_StatusDetail"],
                                    (string)sdr["Description"],
                                    (string)sdr["DriverName"],
                                    ""
                                )
                            );
                        }
                    }
                    cmd.Connection.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Incident type couldn't be found due: {ex.Message}");
            }

            return result;
        }

        public static List<Incident> GetIncident(string incidentId)
        {

            List<Incident> result = new List<Incident>();
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"List_Incidents",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Parameters.AddWithValue("@ID_Incident", Guid.Parse(incidentId));
                    cmd.Parameters.AddWithValue("@Folio", "");
                    cmd.Parameters.AddWithValue("@ID_Driver", "");
                    cmd.Parameters.AddWithValue("@ID_StatusDetail", "");
                    cmd.Parameters.AddWithValue("@DriverName", "");
                    cmd.Parameters.AddWithValue("@Truck_No", "");
                    cmd.Parameters.AddWithValue("@Trailer_No", "");
                    cmd.Parameters.AddWithValue("@Date1", "");
                    cmd.Parameters.AddWithValue("@Date2", "");

                    cmd.Connection.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr == null)
                        {
                            throw new NullReferenceException("No Information Available.");
                        }
                        while (sdr.Read())
                        {
                            //Debug.WriteLine(sdr["IncidentCloseDate"]);

                            result.Add(
                                new Incident(
                                    (Guid)sdr["ID_Incident"],
                                    (string)sdr["Folio"],
                                    Convert.ToDateTime(sdr["IncidentDate"]),
                                    DateTime.Now,
                                    (bool)sdr["PoliceReport"],
                                    (string)sdr["CitationReportNumber"],
                                    (string)sdr["ManifestNumber"],
                                    (string)sdr["LocationReferences"],
                                    (string)sdr["IncidentLatitude"],
                                    (string)sdr["IncidentLongitude"],
                                    (string)sdr["Comments"],
                                    new Truck(
                                        Guid.Parse((string)sdr["ID_Truck"]),
                                        (string)sdr["TruckSamsaraID"],
                                        (string)sdr["TruckNumber"],
                                        (string)sdr["VinNumber"],
                                        (string)sdr["Broker"],
                                        (string)sdr["Address"]),
                                    (bool)sdr["TruckDamage"],
                                    (bool)sdr["TruckCanMove"],
                                    (bool)sdr["TruckNeedCrane"],
                                    new Trailer(
                                        sdr["ID_Trailer"] == DBNull.Value ? Guid.Empty : Guid.Parse((string)sdr["ID_Trailer"]),
                                        sdr["TrailerNumber"] == DBNull.Value ? "" : (string)sdr["TrailerNumber"],
                                        sdr["TrailerCommodity"] == DBNull.Value ? "" : (string)sdr["TrailerCommodity"],
                                        (bool)sdr["CargoSpill"],
                                        (string)sdr["TrailerBrokerName"],
                                        (string)sdr["TrailerBrokerAddress"]),
                                    (bool)sdr["TrailerDamage"],
                                    (bool)sdr["TrailerCanMove"],
                                    (bool)sdr["TrailerNeedCrane"],
                                    new Driver(
                                        (string)sdr["ID_Driver"],
                                        (string)sdr["Name"],
                                        sdr["PhoneNumber"] == DBNull.Value ? "" : (string)sdr["PhoneNumber"],
                                        sdr["License"] == DBNull.Value ? "" : (string)sdr["License"],
                                        sdr["Expedition_State"] == DBNull.Value ? Guid.Empty.ToString() : (string)sdr["Expedition_State"],
                                        (bool)sdr["DSamsara"],
                                        ExpirationDate: sdr["Expiration_Date"] == DBNull.Value ? "" : Convert.ToDateTime(sdr["Expiration_Date"]).Date.ToString()
                                    ),
                                    (string)sdr["ID_City"],
                                    (string)sdr["ID_State"],
                                    (string)sdr["ID_Broker"],
                                    new Broker((string)sdr["ID_Broker"], (string)sdr["Broker"], (string)sdr["Address"]),
                                    new Broker((string)sdr["TrailerBroker"], (string)sdr["TrailerBrokerName"], (string)sdr["TrailerBrokerAddress"]),
                                    (string)sdr["ID_StatusDetail"],
                                    (string)sdr["Description"],
                                    (string)sdr["Name"],
                                    (string)sdr["PhoneNumber"]
                                )
                            );
                        }
                    }
                    cmd.Connection.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Incident type couldn't be found due: {ex.Message}");
            }

            return result;
        }

        public static List<PersonsInvolved> list_PersonsInvolved(string incidentId)
        {

            List<PersonsInvolved> result = new List<PersonsInvolved>();
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"List_InjuredPerson",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Parameters.AddWithValue("@ID_Incident", Guid.Parse(incidentId));

                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Connection.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr == null)
                        {
                            throw new NullReferenceException("No Information Available.");
                        }
                        while (sdr.Read())
                        {
                            //Debug.WriteLine(sdr["IncidentCloseDate"]);

                            result.Add(
                                new PersonsInvolved(
                                    Convert.ToString(sdr["ID_InjuredPerson"]),
                                    (string)sdr["FullName"],
                                    (string)sdr["LastName1"],
                                    (string)sdr["Phone"],
                                    (string)sdr["Age"],
                                    Convert.ToBoolean(sdr["IsDriver"]),
                                    (string)sdr["DriverLicense"],
                                    (bool)sdr["PrivatePerson"],
                                    (bool)sdr["Injured"],
                                    (string)sdr["Hospital"],
                                    (string)sdr["Comments"],
                                    Convert.ToString(sdr["ID_Incident"])
                                )
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Person type couldn't be found due: {ex.Message}");
            }

            return result;
        }

        public static Response AddIncident(
            string ID_Driver,
            string driverName,
            string ID_State,
            string ID_City,
            string ID_Broker,
            string ID_Broker2,
            string ID_Truck,
            string folio,
            string TruckNumber,
            string trailerNumber,
            string trailerCommodity,
            DateTime incidentDate,
            bool policeReport,
            string citationReport,
            bool cargoSpill,
            string manifestNumber,
            string locationReferences,
            string incidentLatitude,
            string incidentLongitude,
            bool truckDamage,
            bool truckCanMove,
            bool truckNeedCrane,
            bool trailerDamage,
            bool trailerCanMove,
            bool trailerNeedCrane,
            string ID_User,
            string comments,
            bool dSamsara
        )
        {


            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"Update_Incident",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Parameters.AddWithValue("@ID_Incident", Guid.Empty);
                    cmd.Parameters.AddWithValue("@ID_Driver", ID_Driver);
                    cmd.Parameters.AddWithValue("@DriverName", driverName);
                    cmd.Parameters.AddWithValue("@ID_State", ID_State);
                    cmd.Parameters.AddWithValue("@ID_City", ID_City);
                    cmd.Parameters.AddWithValue("@ID_Broker", ID_Broker);
                    cmd.Parameters.AddWithValue("@ID_Broker2", ID_Broker2);
                    cmd.Parameters.AddWithValue("@ID_Truck", "");
                    cmd.Parameters.AddWithValue("@TruckNumber", TruckNumber);
                    cmd.Parameters.AddWithValue("@TrailerNumber", trailerNumber);
                    cmd.Parameters.AddWithValue("@TrailerCommodity", trailerCommodity);
                    cmd.Parameters.AddWithValue("@ID_StatusDetail", "");
                    cmd.Parameters.AddWithValue("@Folio", folio);
                    cmd.Parameters.AddWithValue("@IncidentDate", incidentDate);
                    cmd.Parameters.AddWithValue("@IncidentCloseDate", "");
                    cmd.Parameters.AddWithValue("@PoliceReportBoolean", policeReport);
                    cmd.Parameters.AddWithValue("@CitationReportNumber", citationReport);
                    cmd.Parameters.AddWithValue("@CargoSpill", cargoSpill);
                    cmd.Parameters.AddWithValue("@ManifestNumber", manifestNumber);
                    cmd.Parameters.AddWithValue("@LocationReferences", locationReferences);
                    cmd.Parameters.AddWithValue("@IncidentLatitude", incidentLatitude);
                    cmd.Parameters.AddWithValue("@IncidentLongitude", incidentLongitude);
                    cmd.Parameters.AddWithValue("@TruckDamage", truckDamage);
                    cmd.Parameters.AddWithValue("@TruckCanMove", truckCanMove);
                    cmd.Parameters.AddWithValue("@TruckNeedCrane", truckNeedCrane);
                    cmd.Parameters.AddWithValue("@TrailerDamage", trailerDamage);
                    cmd.Parameters.AddWithValue("@TrailerCanMove", trailerCanMove);
                    cmd.Parameters.AddWithValue("@TrailerNeedCrane", trailerNeedCrane);
                    cmd.Parameters.AddWithValue("@ID_User", ID_User);
                    cmd.Parameters.AddWithValue("@Comments", comments);
                    cmd.Parameters.AddWithValue("@DSamsara", dSamsara);
                    cmd.Parameters.AddWithValue("@Status", true);
                    cmd.Parameters.AddWithValue("@Tester", constants.tester);

                    cmd.Connection.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr == null)
                        {
                            throw new NullReferenceException("No Information Available.");
                        }
                        while (sdr.Read())
                        {
                            Debug.WriteLine(sdr["Validacion"]);
                            Debug.WriteLine(sdr["msg"]);
                            Debug.WriteLine(sdr["ID"]);

                            response = new Response(Convert.ToBoolean(sdr["Validacion"]), sdr["msg"].ToString(), sdr["ID"].ToString());
                        }
                    }
                    cmd.Connection.Close();

                }

                return response;
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Incident couldn't be saved due: {ex.Message}");
                return new Response(false, ex.Message, Guid.Empty.ToString());
            }

        }

        public static Response UpdateIncident(
            string ID_Incident,
            string ID_Driver,
            string driverName,
            string ID_State,
            string ID_City,
            string ID_Broker,
            string ID_Broker2,
            string ID_Truck,
            string TruckNumber,
            string trailerNumber,
            string trailerCommodity,
            DateTime incidentDate,
            bool policeReport,
            string citationReport,
            bool cargoSpill,
            string manifestNumber,
            string locationReferences,
            string incidentLatitude,
            string incidentLongitude,
            bool truckDamage,
            bool truckCanMove,
            bool truckNeedCrane,
            bool trailerDamage,
            bool trailerCanMove,
            bool trailerNeedCrane,
            string ID_User,
            string comments,
            bool dSamsara
        )
        {


            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"Update_Incident",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Parameters.AddWithValue("@ID_Incident", Guid.Parse(ID_Incident));
                    cmd.Parameters.AddWithValue("@ID_Driver", ID_Driver);
                    cmd.Parameters.AddWithValue("@DriverName", driverName);
                    cmd.Parameters.AddWithValue("@ID_State", ID_State);
                    cmd.Parameters.AddWithValue("@ID_City", ID_City);
                    cmd.Parameters.AddWithValue("@ID_Broker", ID_Broker);
                    cmd.Parameters.AddWithValue("@ID_Broker2", ID_Broker2);
                    cmd.Parameters.AddWithValue("@ID_Truck", ID_Truck);
                    cmd.Parameters.AddWithValue("@TruckNumber", TruckNumber);
                    cmd.Parameters.AddWithValue("@TrailerNumber", trailerNumber);
                    cmd.Parameters.AddWithValue("@TrailerCommodity", trailerCommodity);
                    cmd.Parameters.AddWithValue("@ID_StatusDetail", "");
                    cmd.Parameters.AddWithValue("@Folio", "");
                    cmd.Parameters.AddWithValue("@IncidentDate", incidentDate);
                    cmd.Parameters.AddWithValue("@IncidentCloseDate", "");
                    cmd.Parameters.AddWithValue("@PoliceReportBoolean", policeReport);
                    cmd.Parameters.AddWithValue("@CitationReportNumber", citationReport);
                    cmd.Parameters.AddWithValue("@CargoSpill", cargoSpill);
                    cmd.Parameters.AddWithValue("@ManifestNumber", manifestNumber);
                    cmd.Parameters.AddWithValue("@LocationReferences", locationReferences);
                    cmd.Parameters.AddWithValue("@IncidentLatitude", incidentLatitude);
                    cmd.Parameters.AddWithValue("@IncidentLongitude", incidentLongitude);
                    cmd.Parameters.AddWithValue("@TruckDamage", truckDamage);
                    cmd.Parameters.AddWithValue("@TruckCanMove", truckCanMove);
                    cmd.Parameters.AddWithValue("@TruckNeedCrane", truckNeedCrane);
                    cmd.Parameters.AddWithValue("@TrailerDamage", trailerDamage);
                    cmd.Parameters.AddWithValue("@TrailerCanMove", trailerCanMove);
                    cmd.Parameters.AddWithValue("@TrailerNeedCrane", trailerNeedCrane);
                    cmd.Parameters.AddWithValue("@ID_User", ID_User);
                    cmd.Parameters.AddWithValue("@Comments", comments);
                    cmd.Parameters.AddWithValue("@DSamsara", dSamsara);
                    cmd.Parameters.AddWithValue("@Status", true);

                    cmd.Connection.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr == null)
                        {
                            throw new NullReferenceException("No Information Available.");
                        }
                        while (sdr.Read())
                        {
                            Debug.WriteLine(sdr["Validacion"]);
                            Debug.WriteLine(sdr["msg"]);
                            Debug.WriteLine(sdr["ID"]);

                            //MessageBox.Show((string)sdr["msg"]);

                            response = new Response(Convert.ToBoolean(sdr["Validacion"]), sdr["msg"].ToString(), sdr["ID"].ToString());
                        }
                    }
                    cmd.Connection.Close();

                }

                return response;
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Incident couldn't be saved due: {ex.Message}");
                Debug.WriteLine(ex.Message);
                return new Response(false, ex.Message, Guid.Empty.ToString());
            }

        }

        public static void AddPersonInvolved(PersonsInvolved involved)
        {


            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"Update_InjuredPerson",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Parameters.AddWithValue("@ID_InjuredPerson", involved.ID_Injured);
                    cmd.Parameters.AddWithValue("@fullName", involved.FullName);
                    cmd.Parameters.AddWithValue("@lastName1", involved.LastName1);
                    cmd.Parameters.AddWithValue("@lastName2", involved.LastName2);
                    cmd.Parameters.AddWithValue("@phone", involved.PhoneNumber);
                    cmd.Parameters.AddWithValue("@age", involved.Age);
                    cmd.Parameters.AddWithValue("@isDriver", involved.Driver);
                    cmd.Parameters.AddWithValue("@isPassenger", !involved.Driver);
                    cmd.Parameters.AddWithValue("@driverLicense", involved.DriverLicense);
                    cmd.Parameters.AddWithValue("@privatepPerson", involved.PrivatePerson);
                    cmd.Parameters.AddWithValue("@injured", involved.Injured);
                    cmd.Parameters.AddWithValue("@Hospital", involved.Hospital);
                    cmd.Parameters.AddWithValue("@Comments", involved.Comments);
                    cmd.Parameters.AddWithValue("@ID_Incident", involved.ID_Incident);

                    cmd.Connection.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr == null)
                        {
                            throw new NullReferenceException("No Information Available.");
                        }
                        while (sdr.Read())
                        {
                            Debug.WriteLine(sdr["Validacion"]);
                            Debug.WriteLine(sdr["msg"]);
                            Debug.WriteLine(sdr["ID"]);

                            //MessageBox.Show((string)sdr["msg"]);

                            response = new Response(Convert.ToBoolean(sdr["Validacion"]), sdr["msg"].ToString(), sdr["ID"].ToString());
                        }
                    }
                    cmd.Connection.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Involved person couldn't be saved due: {ex.Message}");
            }

        }

        public static void CloseIncident(string incidentID)
        {
            Guid ID_Incident = Guid.Parse(incidentID);
            using (var db = new SIREMEntities())
            {
                Incidents incident = (Incidents)db.Incidents.Where(i => i.ID_Incident == ID_Incident).FirstOrDefault();

                incident.ID_StatusDetail = "AF034BC4-3F32-4174-B042-3178B2EC8199";
                incident.IncidentCloseDate = DateTime.Now;

                db.Entry(incident).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

                Console.WriteLine("Registro actualizado correctamente.");
                Utils.ShowMessage("Incindent has been closed.", title: "Incident Closed", type: "Approved");
                //return new Response()
            }
        }

        public static void Delete(string incidentID, string folio)
        {
            if (Utils.ShowConfirmationMessage($"Are you sure you want to delete this incident with {folio}?", type: "Warning"))
            {
                Guid ID_Incident = Guid.Parse(incidentID);
                using (var db = new SIREMEntities())
                {
                    Incidents incident = (Incidents)db.Incidents.Where(i => i.ID_Incident == ID_Incident).FirstOrDefault();

                    incident.Status = false;

                    db.Entry(incident).State = System.Data.Entity.EntityState.Modified;

                    db.SaveChanges();

                    //Console.WriteLine("Registro actualizado correctamente.");
                    Utils.ShowMessage($"Incident with folio: {folio} has been deleted", title: "Incident Deleted", type: "approved");
                    //return new Response()
                }
            }

        }

        public static void UpdateStatus(string incidentID, string status, string truckNum)
        {
            Guid ID_Incident = Guid.Parse(incidentID);
            using (var db = new SIREMEntities())
            {
                Incidents incident = (Incidents)db.Incidents.Where(i => i.ID_Incident == ID_Incident).FirstOrDefault();

                incident.ID_StatusDetail = status;

                db.Entry(incident).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

                Console.WriteLine("Registro actualizado correctamente.");

                //return new Response()
            }



        }

        public static void UpdateLocation(string incidentID, string truckNum)
        {
            Guid ID_Incident = Guid.Parse(incidentID);
            using (var db = new SIREMEntities())
            { 
                var truckLoc = GetTruckSamsara(truckNum);
                if (truckLoc.validation == true)
                {
                    var loc = new Locations()
                    {
                        ID_Location = Guid.NewGuid(),
                        ID_Incident = ID_Incident,
                        Latitude = truckLoc.latitude.ToString(),
                        Longitude = truckLoc.longitude.ToString(),
                        Description = truckLoc.FormattedLocation,
                        CreatedAt = truckLoc.currentTime
                    };

                    db.Locations.Add(loc);

                    db.SaveChanges();

                    Console.WriteLine("Locaction added.");
                }
            }
        }

        private static (double latitude, double longitude, DateTime currentTime, string FormattedLocation, bool validation) GetTruckSamsara(string truckNumber)
        {
            double latitude = 0;
            double longitude = 0;
            DateTime currentTime = DateTime.Now;
            const string url = "https://api.samsara.com/fleet/vehicles/locations";
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

                    var filtered = locs.Where(x => x.name == truckNumber).FirstOrDefault();

                    latitude = (double)filtered.latitude;
                    longitude = (double)filtered.longitude;
                    


                    //Dispose once all HttpClient calls are complete.This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
                    client.Dispose();

                    return (latitude, longitude, currentTime, filtered.formattedLocation, true);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return (latitude, longitude, currentTime, "", false);
        }

        public static Response CreateEmptyIncident()
        {

            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"Update_Incident",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Parameters.AddWithValue("@ID_Incident", Guid.Empty);
                    cmd.Parameters.AddWithValue("@ID_Driver", "");
                    cmd.Parameters.AddWithValue("@DriverName", "");
                    cmd.Parameters.AddWithValue("@ID_State", Guid.Empty);
                    cmd.Parameters.AddWithValue("@ID_City", "");
                    cmd.Parameters.AddWithValue("@ID_Broker", "");
                    cmd.Parameters.AddWithValue("@ID_Broker2", "");
                    cmd.Parameters.AddWithValue("@ID_Truck", "");
                    cmd.Parameters.AddWithValue("@TruckNumber", "");
                    cmd.Parameters.AddWithValue("@TrailerNumber", "");
                    cmd.Parameters.AddWithValue("@TrailerCommodity", "");
                    cmd.Parameters.AddWithValue("@ID_StatusDetail", "");
                    cmd.Parameters.AddWithValue("@Folio", "");
                    cmd.Parameters.AddWithValue("@IncidentDate", "");
                    cmd.Parameters.AddWithValue("@IncidentCloseDate", "");
                    cmd.Parameters.AddWithValue("@PoliceReportBoolean", "");
                    cmd.Parameters.AddWithValue("@CitationReportNumber", "");
                    cmd.Parameters.AddWithValue("@CargoSpill", "");
                    cmd.Parameters.AddWithValue("@ManifestNumber", "");
                    cmd.Parameters.AddWithValue("@LocationReferences", "");
                    cmd.Parameters.AddWithValue("@IncidentLatitude", "");
                    cmd.Parameters.AddWithValue("@IncidentLongitude", "");
                    cmd.Parameters.AddWithValue("@TruckDamage", "");
                    cmd.Parameters.AddWithValue("@TruckCanMove", "");
                    cmd.Parameters.AddWithValue("@TruckNeedCrane", "");
                    cmd.Parameters.AddWithValue("@TrailerDamage", "");
                    cmd.Parameters.AddWithValue("@TrailerCanMove", "");
                    cmd.Parameters.AddWithValue("@TrailerNeedCrane", "");
                    cmd.Parameters.AddWithValue("@ID_User", "");
                    cmd.Parameters.AddWithValue("@Comments", "");
                    cmd.Parameters.AddWithValue("@DSamsara", false);
                    cmd.Parameters.AddWithValue("@Status", false);

                    cmd.Connection.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr == null)
                        {
                            throw new NullReferenceException("No Information Available.");
                        }
                        while (sdr.Read())
                        {
                            Debug.WriteLine(sdr["Validacion"]);
                            Debug.WriteLine(sdr["msg"]);
                            Debug.WriteLine(sdr["ID"]);

                            response = new Response(Convert.ToBoolean(sdr["Validacion"]), sdr["msg"].ToString(), sdr["ID"].ToString());
                        }
                    }
                    cmd.Connection.Close();

                }

                return response;
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Incident couldn't be saved due: {ex.Message}");
                return new Response(false, ex.Message, Guid.Empty.ToString());
            }

        }

        public static List<Location> list_Locations(string incidentId)
        {

            List<Location> result = new List<Location>();
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"List_Locations",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    //cmd.Parameters.AddWithValue("@ID_Incident", Guid.Parse(incidentId));
                    cmd.Parameters.AddWithValue("@ID_Incident", "ECFB89AD-542D-4D96-97DF-A6CDD5FA401F");

                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Connection.Open();

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr == null)
                        {
                            throw new NullReferenceException("No Information Available.");
                        }
                        while (sdr.Read())
                        {
                            //Debug.WriteLine(sdr["IncidentCloseDate"]);

                            result.Add(
                                new Location(
                                    (string)sdr["Latitude"],
                                    (string)sdr["Longitude"],
                                    (string)sdr["Description"],
                                    (DateTime)sdr["CreatedAt"]
                                )
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Person type couldn't be found due: {ex.Message}");
            }

            return result;
        }

        public static List<Reasons> List_Reasons()
        {
            using (var db = new SIREMEntities())
            {
                List<Reasons> reasons = db.Reasons.OrderByDescending(r => r.Reason).Where(r => r.Status == true).ToList();

                return reasons;
            }
        }
    }
}
