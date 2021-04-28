using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public static Boolean opSuccess;

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
    }
}
