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

        private static Boolean opSuccess;

        public static List<State> list_States()
        {
            opSuccess = false;
            List<State> result = new List<State>();
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.GeneralConnection,
                    CommandText = $"List_States",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Parameters.AddWithValue("@ID_Country", Guid.Parse("99F9B034-75BE-4615-88C6-8D64BC3549DC"));
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
                                new State(
                                    (string)sdr["pk_id"],
                                    (string)sdr["country"],
                                    (string)sdr["state"]
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
                MessageBox.Show($"State couldn't be found due: {ex.Message}");
            }

            return result;
        }

        public static List<City> list_Cities(string stateId)
        {
            opSuccess = false;
            List<City> result = new List<City>();
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.GeneralConnection,
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
                                    (string)sdr["pk_id"],
                                    (string)sdr["state"],
                                    (string)sdr["city"]
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
                MessageBox.Show($"City couldn't be found due: {ex.Message}");
            }

            return result;
        }
    }
}
