using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Models;

namespace ResponseEmergencySystem.Services
{
    public static class GeneralService
    {

        public static List<State> list_States()
        {
            List<State> result = new List<State>();
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"List_States",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Parameters.AddWithValue("@ID_Country", "8E87EA2A-5D45-4CF5-B752-FF8675483C74");
                    cmd.Connection.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr == null)
                        {
                            throw new NullReferenceException("No Information Available.");
                        }

                        while (sdr.Read())
                        {
                            Debug.WriteLine((string)sdr["state"]);
                            result.Add(
                                new State(
                                    sdr["pk_id"].ToString(),
                                    (string)sdr["Country"],
                                    (string)sdr["state"]
                                )
                            );
                        }
                    }
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"State couldn't be found due: {ex.Message}");
            }

            return result;
        }

        public static List<City> list_Cities(string stateId)
        {
            List<City> result = new List<City>();
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"List_Cities",
                    CommandType = CommandType.StoredProcedure
                })
                {

                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                    //cmd.Parameters.AddWithValue("@ID_State", Guid.Parse("608C35C3-2ED5-43AA-AE86-3C515CB6E612"));
                    cmd.Parameters.AddWithValue("@ID_State", Guid.Parse(stateId));
                    cmd.Parameters.AddWithValue("@State_Name", "");
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
                                new City(
                                    sdr["pk_id"].ToString(),
                                    (string)sdr["state"],
                                    (string)sdr["city"]
                                )
                            );
                        }
                    }
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"City couldn't be found due: {ex.Message}");
            }

            return result;
        }

        public static List<Truck> list_Trucks()
        {
        
            List<Truck> result = new List<Truck>();

            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.GeneralConnection,
                    CommandText = $"List_Trucks",
                    CommandType = CommandType.StoredProcedure
                })
                {
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
                            result.Add(
                                new Truck(
                                    sdr["pk_id"] == DBNull.Value ? Guid.Empty.ToString() : (string)sdr["pk_id"],
                                    (string)sdr["ID_Samsara"],
                                    (string)sdr["Name"],
                                    (string)sdr["VinNumber"],
                                    (string)sdr["SerialNumber"],
                                    (string)sdr["Make"],
                                    (string)sdr["Model"],
                                    (string)sdr["Year"],
                                    (string)sdr["LicensePlate"]
                                )
                            );
                        }
                    }
                    cmd.Connection.Close();

                    return result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Driver couldn't be found due: {ex.Message}");

                return new List<Truck>();
            }
            //return result;
        }

        public static List<Trailer> list_Trailers()
        {
            List<Trailer> result = new List<Trailer>();

            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.GeneralConnection,
                    CommandText = $"List_Trailers",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                    cmd.Connection.Open();

                    cmd.Parameters.AddWithValue("@ID_Capture", "");

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr == null)
                        {
                            throw new NullReferenceException("No Information Available.");
                        }
                        while (sdr.Read())
                        {
                            result.Add(
                                new Trailer(
                                    Guid.Parse((string)sdr["pk_id"]),
                                    (string)sdr["trailer"],
                                    (string)sdr["commodity"],
                                    false
                                )
                            );
                        }
                    }
                    cmd.Connection.Close();
                   

                    return result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Driver couldn't be found due: {ex.Message}");

                return new List<Trailer>();
            }
            //return result;
        }

        public static Response UpdateTrailer(string Trailer, string commodity)
        {
            var response = new Response();
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"UpdateTrailers",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Parameters.AddWithValue("@sID", "");
                    cmd.Parameters.AddWithValue("@sTrailer", Trailer);
                    cmd.Parameters.AddWithValue("@sVIN", "");
                    cmd.Parameters.AddWithValue("@sPlate", "");
                    cmd.Parameters.AddWithValue("@sMake", "");
                    cmd.Parameters.AddWithValue("@sModel", "");
                    cmd.Parameters.AddWithValue("@sType", "");
                    cmd.Parameters.AddWithValue("@sYear", "");
                    cmd.Parameters.AddWithValue("@sCommodity", commodity);
                    cmd.Parameters.AddWithValue("@sStatus", "1");
                    cmd.Parameters.AddWithValue("@sLienholder", "");
                    cmd.Parameters.AddWithValue("@sLienholderType", "");

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
                return new Response(false, ex.Message, Guid.Empty.ToString());
            }
        }
    }
}
