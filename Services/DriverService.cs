﻿using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Forms;
using ResponseEmergencySystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponseEmergencySystem.Services
{
    public static class DriverService
    {
        public static bool opSuccess;
        public static Response response;
        public static DataTable result;

        public static Driver GetDriver(string license, string phone, string name)
        {
            opSuccess = false;
            List<Driver> result = new List<Driver>();

            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.GeneralConnection,
                    CommandText = $"Get_Driver",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                    cmd.Connection.Open();

                    cmd.Parameters.AddWithValue("@License", license);
                    cmd.Parameters.AddWithValue("@Phone_Number", phone);
                    cmd.Parameters.AddWithValue("@Name", name);

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr == null)
                        {
                            throw new NullReferenceException("No Information Available.");
                        }
                        while (sdr.Read())
                        {
                            result.Add(
                                new Driver(
                                    Guid.Parse(sdr["pk_id"].ToString()),
                                    (string)sdr["driverName"],
                                    (string)sdr["pat_surname"],
                                    (string)sdr["mat_surname"],
                                    (string)sdr["phone_number"],
                                    (string)sdr["License"],
                                    (string)sdr["ID_State"],
                                    (string)sdr["state_name"],
                                    Convert.ToDateTime(sdr["Expiration_Date"]).Date.ToString()
                                )
                            ) ;
                        }
                    }
                    cmd.Connection.Close();
                    opSuccess = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Driver couldn't be found due: {ex.Message}");
            }

            if (result.Count > 1)
            {
                frm_DriverSearchList drivers = new frm_DriverSearchList(result);
                if (drivers.ShowDialog() == DialogResult.OK)
                {
                    return result[drivers.dt_DriverRowSelected];
                }

                return result.FirstOrDefault();
            }
            else if (result.Count == 1)
            {
                return result.First();
            }
            else
            {
                return null;
                MessageBox.Show("There is no driver with the information supplied");
            }
            //return result;
        }

    }
}