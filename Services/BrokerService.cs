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
    public static class BrokerService
    {
        public static bool opSuccess;
        public static Response response;

        public static List<Broker> list_Brokers()
        {
            opSuccess = false;
            List<Broker> result = new List<Broker>();

            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"List_Brokers",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@ID_State", "");
                    cmd.Parameters.AddWithValue("@ID_City", "");
                    cmd.Parameters.AddWithValue("@Broker", "");
                    cmd.Parameters.AddWithValue("@Address", "");

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
                                new Broker(
                                    (string)sdr["ID_Broker"].ToString(),
                                    (string)sdr["ID_State"],
                                    (string)sdr["ID_City"],
                                    (string)sdr["State"],
                                    (string)sdr["City"],
                                    (string)sdr["Broker"],
                                    (string)sdr["Address"]
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
                MessageBox.Show($"Broker couldn't be found due: {ex.Message}");
            }

            return result;
        }

        public static void update_Broker(string ID_State, string ID_City, string Name, string address, string ID = "00000000-0000-0000-0000-000000000000")
        {
            opSuccess = false;
            List<Broker> result = new List<Broker>();

            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"Update_Broker",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Parameters.AddWithValue("@ID_Broker", Guid.Parse(ID));
                    cmd.Parameters.AddWithValue("@ID_State", ID_State);
                    cmd.Parameters.AddWithValue("@ID_City", ID_City);
                    cmd.Parameters.AddWithValue("@Broker", Name);
                    cmd.Parameters.AddWithValue("@Address", address);

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

                            MessageBox.Show((string)sdr["msg"]);

                            response = new Response(Convert.ToBoolean(sdr["Validacion"]), sdr["msg"].ToString(), sdr["ID"].ToString());
                            //result.Add(
                            //    new Broker(
                            //        (string)sdr["ID_Broker"].ToString(),
                            //        (string)sdr["ID_State"],
                            //        (string)sdr["ID_City"],
                            //        (string)sdr["State"],
                            //        (string)sdr["City"],
                            //        (string)sdr["Broker"],
                            //        (string)sdr["Address"]
                            //    )
                            //);
                        }
                    }
                    cmd.Connection.Close();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Broker couldn't be saved due: {ex.Message}");
            }

            //return result;
        }
    }
}
