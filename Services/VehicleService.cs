using ResponseEmergencySystem.Builders;
using ResponseEmergencySystem.Code;
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
    class VehicleService
    {
        public static List<Vehicle> list_Trucks()
        {

            List<Vehicle> result = new List<Vehicle>();

            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
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
                                new VehicleBuilder()
                                    .IsRegisteredInGeneral(sdr["ID_General"] == DBNull.Value ? Guid.Empty : Guid.Parse(sdr["ID_General"].ToString()), sdr["ID_Samsara"] == DBNull.Value ? "0" : (string)sdr["ID_Samsara"])
                                    .HasVehicleID(sdr["ID_Vehicle"] == DBNull.Value ? Guid.Empty : (Guid)sdr["ID_Vehicle"])
                                    .SetName(sdr["ID_Vehicle"] == DBNull.Value ? (string)sdr["truckNumber"] : (string)sdr["InternalName"])
                                    .SetVinNumber(sdr["ID_Vehicle"] == DBNull.Value ? (string)sdr["VinNumber"] : (string)sdr["InternalVinNumber"])
                                    .SetSerialNumber(sdr["ID_Vehicle"] == DBNull.Value ? (string)sdr["SerialNumber"] : (string)sdr["InternalSerialNumber"])
                                    .SetMake(sdr["ID_Vehicle"] == DBNull.Value ? (string)sdr["Make"] : (string)sdr["InternalMake"])
                                    .SetModel(sdr["ID_Vehicle"] == DBNull.Value ? (string)sdr["Model"] : (string)sdr["InternalModel"])
                                    .SetYear(sdr["ID_Vehicle"] == DBNull.Value ? (string)sdr["Year"] : (string)sdr["InternalYear"])
                                    .SetLicensePlate(sdr["ID_Vehicle"] == DBNull.Value ? (string)sdr["LicensePlate"] : (string)sdr["InternalLicensePlate"])
                                    .VehicleType(sdr["ID_Vehicle"] == DBNull.Value ? "truck" : (string)sdr["VehicleType"])
                                    .NewVehicle()
                                    .Build()
                            );
                        }
                    }
                    cmd.Connection.Close();

                    return result.Where(v => v.IsTruck).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Truck couldn't be found due: {ex.Message}");

                return new List<Vehicle>();
            }
            //return result;
        }

        public static List<Vehicle> list_Trailers()
        {
            List<Vehicle> result = new List<Vehicle>();

            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"List_Trailers",
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
                                new VehicleBuilder()
                                    .IsRegisteredInGeneral(sdr["ID_General"] == DBNull.Value ? Guid.Empty : Guid.Parse(sdr["ID_General"].ToString()), "0")
                                    .HasVehicleID(sdr["ID_Vehicle"] == DBNull.Value ? Guid.Empty : (Guid)sdr["ID_Vehicle"])
                                    .SetName(sdr["ID_Vehicle"] == DBNull.Value ? (string)sdr["trailer"] : (string)sdr["InternalName"])
                                    .SetCommodity(sdr["ID_Vehicle"] == DBNull.Value ? (string)sdr["commodity"] : (string)sdr["InternalCommodity"])
                                    .SetVinNumber(sdr["ID_Vehicle"] == DBNull.Value ? (string)sdr["VinNumber"] : (string)sdr["InternalVinNumber"])
                                    .SetSerialNumber(sdr["ID_Vehicle"] == DBNull.Value ? (string)sdr["SerialNumber"] : (string)sdr["InternalSerialNumber"])
                                    .SetMake(sdr["ID_Vehicle"] == DBNull.Value ? (string)sdr["Make"] : (string)sdr["InternalMake"])
                                    .SetModel(sdr["ID_Vehicle"] ==DBNull.Value ? (string)sdr["Model"] : (string)sdr["InternalModel"])
                                    .SetYear(sdr["ID_Vehicle"] == DBNull.Value ? (string)sdr["Year"] : (string)sdr["InternalYear"])
                                    .SetLicensePlate(sdr["ID_Vehicle"] == DBNull.Value ? (string)sdr["LicensePlate"] : (string)sdr["InternalLicensePlate"])
                                    .VehicleType(sdr["ID_Vehicle"] == DBNull.Value ? "trailer" : (string)sdr["VehicleType"])
                                    .NewVehicle()
                                    .Build()
                            );
                        }
                    }
                    cmd.Connection.Close();


                    return result.Where(t => t.IsTrailer).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Trailer couldn't be found due: {ex.Message}");

                return new List<Vehicle>();
            }
            //return result;
        }

        public static List<Vehicle> list_Vehicles()
        {
            List<Vehicle> result = new List<Vehicle>();

            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"List_Vehicles",
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
                                new VehicleBuilder()
                                    .IsRegisteredInGeneral(Guid.Empty, "0")
                                    .HasVehicleID((Guid)sdr["ID_Vehicle"])
                                    .SetName((string)sdr["Name"])
                                    .SetVinNumber((string)sdr["VinNumber"])
                                    .SetSerialNumber((string)sdr["SerialNumber"])
                                    .SetMake((string)sdr["Make"])
                                    .SetModel((string)sdr["Model"])
                                    .SetYear((string)sdr["Year"])
                                    .SetLicensePlate((string)sdr["LicensePlate"])
                                    .VehicleType((string)sdr["VehicleType"])
                                    .NewVehicle()
                                    .Build()
                            );
                        }
                    }
                    cmd.Connection.Close();


                    return result.Where(t => t.IsVehicle).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Vehicle couldn't be found due: {ex.Message}");

                return new List<Vehicle>();
            }
            //return result;
        }

        //public static Response UpdateTrailer(string Trailer, string commodity)
        //{
        //    var response = new Response();
        //    try
        //    {
        //        using (SqlCommand cmd = new SqlCommand
        //        {
        //            Connection = constants.SIREMConnection,
        //            CommandText = $"UpdateTrailers",
        //            CommandType = CommandType.StoredProcedure
        //        })
        //        {
        //            if (cmd.Connection.State == ConnectionState.Open)
        //            {
        //                cmd.Connection.Close();
        //            }

        //            cmd.Parameters.AddWithValue("@sID", "");
        //            cmd.Parameters.AddWithValue("@sTrailer", Trailer);
        //            cmd.Parameters.AddWithValue("@sVIN", "");
        //            cmd.Parameters.AddWithValue("@sPlate", "");
        //            cmd.Parameters.AddWithValue("@sMake", "");
        //            cmd.Parameters.AddWithValue("@sModel", "");
        //            cmd.Parameters.AddWithValue("@sType", "");
        //            cmd.Parameters.AddWithValue("@sYear", "");
        //            cmd.Parameters.AddWithValue("@sCommodity", commodity);
        //            cmd.Parameters.AddWithValue("@sStatus", "1");
        //            cmd.Parameters.AddWithValue("@sLienholder", "");
        //            cmd.Parameters.AddWithValue("@sLienholderType", "");

        //            cmd.Connection.Open();
        //            using (SqlDataReader sdr = cmd.ExecuteReader())
        //            {
        //                if (sdr == null)
        //                {
        //                    throw new NullReferenceException("No Information Available.");
        //                }

        //                while (sdr.Read())
        //                {
        //                    Debug.WriteLine(sdr["Validacion"]);
        //                    Debug.WriteLine(sdr["msg"]);
        //                    Debug.WriteLine(sdr["ID"]);

        //                    //MessageBox.Show((string)sdr["msg"]);

        //                    response = new Response(Convert.ToBoolean(sdr["Validacion"]), sdr["msg"].ToString(), sdr["ID"].ToString());
        //                }
        //            }
        //            cmd.Connection.Close();

        //        }

        //        return response;

        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response(false, ex.Message, Guid.Empty.ToString());
        //    }
        //}
    }
}
