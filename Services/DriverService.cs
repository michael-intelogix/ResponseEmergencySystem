using ResponseEmergencySystem.Builders;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Forms;
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
    public static class DriverService
    {
        public static bool opSuccess;
        public static Response response;
        public static DataTable result;

        public static List<Employee> GetDriver(string search)
        {
            opSuccess = false;
            List<Employee> result = new List<Employee>();

            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"Get_Driver",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }
                    cmd.Connection.Open();

                    cmd.Parameters.AddWithValue("@driver", search);

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr == null)
                        {
                            throw new NullReferenceException("No Information Available.");
                        }
                        while (sdr.Read())
                        {
                            result.Add(
                                new EmployeeBuilder()
                                    .Called(sdr["SamsaraDriverName"] == DBNull.Value ? (string)sdr["EmployeeName"] : (string)sdr["SamsaraDriverName"])
                                    .PhoneNumber(sdr["SamsaraPhoneNumber"] == DBNull.Value ? (string)sdr["EmployeePhoneNumber"] : (string)sdr["SamsaraPhoneNumber"])
                                    .LicenseNumber(sdr["SamsaraLicenseNumber"] == DBNull.Value ? (string)sdr["EmployeeLicense"] : (string)sdr["SamsaraLicenseNumber"])
                                    .State(sdr["SamsaraState"] == DBNull.Value ? "" : (string)sdr["SamsaraState"])
                                    .IsRegisteredInGeneral(
                                        sdr["pk_id"] == DBNull.Value ? Guid.Empty : Guid.Parse(sdr["pk_id"].ToString()),
                                        sdr["ID_Samsara"] == DBNull.Value ? "0" : sdr["ID_Samsara"].ToString()
                                     )
                                    .HasEmployeeID(sdr["ID_Employee"] == DBNull.Value ? Guid.Empty : Guid.Parse(sdr["ID_Employee"].ToString()))
                                    .SetNewID()
                                    .Build()
                            //new Driver(
                            //        Guid.Parse(sdr["ID"].ToString()),
                            //        sdr["ID_Employee"] == DBNull.Value ? Guid.Empty : Guid.Parse(sdr["ID_Employee"].ToString()),
                            //        sdr["pk_id"] == DBNull.Value ? Guid.Empty : Guid.Parse(sdr["pk_id"].ToString()),
                            //        sdr["ID_Samsara"] == DBNull.Value ? "0" : sdr["ID_Samsara"].ToString(),
                            //        sdr["SamsaraDriverName"] == DBNull.Value ? (string)sdr["EmployeeName"] : (string)sdr["SamsaraDriverName"],
                            //        sdr["SamsaraPhoneNumber"] == DBNull.Value ? (string)sdr["EmployeePhoneNumber"] : (string)sdr["SamsaraPhoneNumber"],
                            //        sdr["SamsaraLicenseNumber"] == DBNull.Value ? (string)sdr["EmployeeLicense"] : (string)sdr["SamsaraLicenseNumber"],
                            //        sdr["SamsaraState"] == DBNull.Value ? "" : (string)sdr["SamsaraState"],
                            //        sdr["SamsaraLicenseState"] == DBNull.Value ? "" : (string)sdr["SamsaraLicenseState"],
                            //        sdr["Expiration_Date"] == DBNull.Value ? "" : Convert.ToDateTime(sdr["Expiration_Date"]).Date.ToString()
                            //    //DateTime.Now.Date.ToString()
                            //    //Convert.ToDateTime(sdr["Expiration_Date"]).Date.ToString()
                            //    )
                            );
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

            return result;

            //if (result.Count > 1)
            //{
            //    frm_DriverSearchList drivers = new frm_DriverSearchList(result);
            //    if (drivers.ShowDialog() == DialogResult.OK)
            //    {
            //        return result[drivers.dt_DriverRowSelected];
            //    }

            //    return result.FirstOrDefault();
            //}
            //else if (result.Count == 1)
            //{
            //    return result.First();
            //}
            //else
            //{
            //    return null;
            //    MessageBox.Show("There is no driver with the information supplied");
            //}
            //return result;
        }

        public static List<Driver> List_Drivers()
        {
            opSuccess = false;
            List<Driver> result = new List<Driver>();

            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"List_Drivers",
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
                                new Driver(
                                    Guid.Parse(sdr["pk_id"].ToString()),
                                    (string)sdr["ID_Samsara"],
                                    (string)sdr["driverName"],
                                    (string)sdr["pat_surname"],
                                    (string)sdr["mat_surname"],
                                    "",
                                    "",
                                    "",
                                    ""
                                //Convert.ToDateTime(sdr["Expiration_Date"]).Date.ToString()
                                
                                )

                            );

                            Console.WriteLine(sdr["ID_Samsara"]);
                        }
                    }
                    cmd.Connection.Close();
                    opSuccess = true;

                    return result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Driver couldn't be found due: {ex.Message}");

                return new List<Driver>();
            }
            //return result;
        }

        //public static Response UpdateDriver(Driver d)
        //{
//            try
//            {
//                using (SqlCommand cmd = new SqlCommand
//                {
//                    Connection = constants.GeneralConnection,
//                    CommandText = $"Update_Driver",
//                    CommandType = CommandType.StoredProcedure
//    })
//                {
//                    if (cmd.Connection.State == ConnectionState.Open)
//                    {
//                        cmd.Connection.Close();
//                    }

//cmd.Parameters.AddWithValue("@ID_Category", Guid.Empty);
//cmd.Parameters.AddWithValue("@Category", category);
//cmd.Parameters.AddWithValue("@Status", true);

//cmd.Connection.Open();
//using (SqlDataReader sdr = cmd.ExecuteReader())
//{
//    if (sdr == null)
//    {
//        throw new NullReferenceException("No Information Available.");
//    }

//    while (sdr.Read())
//    {
//        Debug.WriteLine(sdr["Validacion"]);
//        Debug.WriteLine(sdr["msg"]);
//        Debug.WriteLine(sdr["ID"]);

//        //MessageBox.Show((string)sdr["msg"]);

//        response = new Response(Convert.ToBoolean(sdr["Validacion"]), sdr["msg"].ToString(), sdr["ID"].ToString());
//    }
//}
//cmd.Connection.Close();

//                }

//                return response;

//            }
//            catch (Exception ex)
//{
//    return new Response(false, ex.Message, Guid.Empty.ToString());
//}

//}

        public static List<Driver> List_SamsaraDrivers()
        {

            List<Driver> result = new List<Driver>();
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.GeneralConnection,
                    CommandText = $"List_DriversSamsara",
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
                               new Driver(
                                   (string)sdr["ID_Samsara"],
                                   (string)sdr["Name"],
                                   (string)sdr["PhoneNumber"],
                                   (string)sdr["LicenseNumber"],
                                   (string)sdr["LicenseState"]
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

                return new List<Driver>();
            }
        }

        public static Response AddDriver(Employee emp)
        {
            
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"Update_Employee",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Parameters.AddWithValue("@ID_Employee", emp.ID_Employee);
                    cmd.Parameters.AddWithValue("@Name", emp.Name);
                    cmd.Parameters.AddWithValue("@PhoneNumber", emp.PhoneNumber);
                    cmd.Parameters.AddWithValue("@License", emp.License);
                    if (emp.IsSamsara)
                        cmd.Parameters.AddWithValue("@ID_Samsara", emp.ID_Samsara);
                    if (emp.IsLocal)
                    {
                        cmd.Parameters.AddWithValue("@ID_General", emp.ID_General);
                        cmd.Parameters.AddWithValue("@ID_Samsara", emp.ID_Samsara);
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
                            Debug.WriteLine(sdr["Validacion"]);
                            Debug.WriteLine(sdr["msg"]);
                            Debug.WriteLine(sdr["ID"]);

                            return new Response(Convert.ToBoolean(sdr["Validacion"]), sdr["msg"].ToString(), sdr["ID"].ToString());
                        }
                    }
                    cmd.Connection.Close();

                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Broker couldn't be saved due: {ex.Message}");
                return new Response(false, ex.Message, Guid.Empty.ToString());

            }

            return new Response(false, "Failed request", Guid.Empty.ToString());
        }

        public static Response UpdateDriver(Employee emp)
        {

            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"Update_Employee",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Parameters.AddWithValue("@ID_Employee", emp.ID_Employee);
                    cmd.Parameters.AddWithValue("@Name", emp.Name);
                    cmd.Parameters.AddWithValue("@PhoneNumber", emp.PhoneNumber);
                    cmd.Parameters.AddWithValue("@License", emp.License);

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

                            return new Response(Convert.ToBoolean(sdr["Validacion"]), sdr["msg"].ToString(), sdr["ID"].ToString());
                        }
                    }
                    cmd.Connection.Close();

                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Broker couldn't be saved due: {ex.Message}");
                return new Response(false, ex.Message, Guid.Empty.ToString());

            }

            return new Response(false, "Failed request", Guid.Empty.ToString());
        }
    }
}
