using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponseEmergencySystem.Services
{
    public static class IncidentService
    {

        private static DataTable result;
        private static Boolean opSuccess;

        public static List<Incident> list_Incidents(string folio, string driverId, string driverName, string truckNum, string statusDetailId, string incidentId = "00000000-0000-0000-0000-000000000000", string date1 = "", string date2 = "")
        {
            opSuccess = false;
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

                    cmd.Parameters.AddWithValue("@ID_Incident", Guid.Parse(incidentId));
                    cmd.Parameters.AddWithValue("@Folio", folio);
                    cmd.Parameters.AddWithValue("@ID_Driver", driverId);
                    cmd.Parameters.AddWithValue("@ID_StatusDetail", statusDetailId);
                    cmd.Parameters.AddWithValue("@DriverName", driverName);
                    cmd.Parameters.AddWithValue("@Truck_No", truckNum);
                    cmd.Parameters.AddWithValue("@Trailer_No", "");
                    cmd.Parameters.AddWithValue("@Date1", date1);
                    cmd.Parameters.AddWithValue("@Date2", date2);

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
                                    new Truck (Guid.Parse((string)sdr["ID_Truck"]), (string)sdr["TruckNumber"]),
                                    (bool)sdr["TruckDamage"],
                                    (bool)sdr["TruckCanMove"],
                                    (bool)sdr["TruckNeedCrane"],
                                    new Trailer(Guid.Parse((string)sdr["ID_Truck"]), (string)sdr["TruckNumber"], (string)sdr["commodity"], (bool)sdr["CargoSpill"]),
                                    (bool)sdr["TrailerDamage"],
                                    (bool)sdr["TrailerCanMove"],
                                    (bool)sdr["TrailerNeedCrane"],
                                    new Driver(Guid.Parse((string)sdr["ID_Driver"]), (string)sdr["License"], (string)sdr["Expedition_State"], sdr["Expiration_Date"].ToString()),
                                    (string)sdr["ID_City"],
                                    (string)sdr["ID_State"],
                                    (string)sdr["ID_Broker"],
                                    (string)sdr["ID_StatusDetail"],
                                    (string)sdr["Description"],
                                    (string)sdr["Name"],
                                    (string)sdr["PhoneNumber"]
                                )
                            );
                        }
                    }
                    cmd.Connection.Close();
                    opSuccess = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Incident type couldn't be found due: {ex.Message}");
            }

            return result;
        }

        public static DataTable list_InjuredPerson(string incidentId)
        {
            opSuccess = false;
            result = new DataTable();
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"List_InjuredPerson",
                    CommandType = CommandType.StoredProcedure
                })
                {

                    cmd.Parameters.AddWithValue("@ID_Incident", Guid.Parse(incidentId));

                    
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(result);
                    }
                    opSuccess = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Incident type couldn't be found due: {ex.Message}");
            }

            return result;
        }

    }
}
