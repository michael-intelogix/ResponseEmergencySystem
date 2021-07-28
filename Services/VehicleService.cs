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
                                new VehicleBuilder()
                                    .
                                (
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
                MessageBox.Show($"Truck couldn't be found due: {ex.Message}");

                return new List<Vehicle>();
            }
            //return result;
        }

        //public static List<Trailer> list_Trailers()
        //{
        //    List<Trailer> result = new List<Trailer>();

        //    try
        //    {
        //        using (SqlCommand cmd = new SqlCommand
        //        {
        //            Connection = constants.GeneralConnection,
        //            CommandText = $"List_Trailers",
        //            CommandType = CommandType.StoredProcedure
        //        })
        //        {
        //            if (cmd.Connection.State == ConnectionState.Open)
        //            {
        //                cmd.Connection.Close();
        //            }
        //            cmd.Connection.Open();

        //            cmd.Parameters.AddWithValue("@ID_Capture", "");

        //            using (SqlDataReader sdr = cmd.ExecuteReader())
        //            {
        //                if (sdr == null)
        //                {
        //                    throw new NullReferenceException("No Information Available.");
        //                }
        //                while (sdr.Read())
        //                {
        //                    result.Add(
        //                        new Trailer(
        //                            Guid.Parse((string)sdr["pk_id"]),
        //                            (string)sdr["trailer"],
        //                            (string)sdr["commodity"],
        //                            false
        //                        )
        //                    );
        //                }
        //            }
        //            cmd.Connection.Close();


        //            return result;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Driver couldn't be found due: {ex.Message}");

        //        return new List<Trailer>();
        //    }
        //    //return result;
        //}

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
