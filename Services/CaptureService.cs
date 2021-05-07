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
    public static class CaptureService
    {

        private static Boolean opSuccess;
        public static Response response;

        public static List<Capture> list_CaptureTypes()
        {
            opSuccess = false;
            List<Capture> result = new List<Capture>();

            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.DCManagement,
                    CommandText = $"List_CaptureType",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Parameters.AddWithValue("@ID_CaptureType", "");
                    cmd.Parameters.AddWithValue("@Name", "");
                    cmd.Parameters.AddWithValue("@Description", "SIREM");
                    cmd.Parameters.AddWithValue("@Status", 1);

                    cmd.Connection.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr == null)
                        {
                            throw new NullReferenceException("No Information Available.");
                        }
                        while (sdr.Read())
                        {
                            Debug.WriteLine((string)sdr["CapturesNames"]);

                            result.Add(
                                new Capture(
                                    (string)sdr["ID_CaptureType"],
                                    (string)sdr["Name"],
                                    (string)sdr["CapturesNames"]
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
                MessageBox.Show($"Capture type couldn't be found due: {ex.Message}");
            }

            return result;
        }

        public static List<Capture> list_Captures(string ID_Incident)
        {
            opSuccess = false;
            List<Capture> result = new List<Capture>();

            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"List_Capture",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Parameters.AddWithValue("@ID_Incident", ID_Incident);

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
                                new Capture(
                                    sdr["ID_Capture"].ToString(),
                                    (string)sdr["ID_StatusDetail"],
                                    sdr["ID_CaptureType"].ToString(),
                                    (string)sdr["Name"],
                                    (string)sdr["Comments"],
                                    (string)sdr["Description"]
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
                MessageBox.Show($"Capture couldn't be found due: {ex.Message}");
            }

            return result;
        }

        public static Response AddCapture(string captureTypeId, string incidentId, string description, string comments, string statusDetailId = "")
        {
            opSuccess = false;

            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"Update_Capture",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Parameters.AddWithValue("@ID_Capture", Guid.Empty);
                    cmd.Parameters.AddWithValue("@ID_CaptureType", captureTypeId);
                    cmd.Parameters.AddWithValue("@ID_Incident", incidentId);
                    cmd.Parameters.AddWithValue("@ID_StatusDetail", statusDetailId);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Comments", comments);

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

                            return new Response(Convert.ToBoolean(sdr["Validacion"]), sdr["msg"].ToString(), sdr["ID"].ToString());
                        }
                    }
                    cmd.Connection.Close();
                    opSuccess = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Capture couldn't be saved due: {ex.Message}");

                return new Response(false, ex.Message, Guid.Empty.ToString());
            }

            return new Response(false, "", Guid.Empty.ToString());
        }

        public static Response AddImage(string imageId, string captureId, string imageUrl, string description, string comments)
        {
            opSuccess = false;

            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"Update_Image",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Parameters.AddWithValue("@ID_Image", imageId);
                    cmd.Parameters.AddWithValue("@ID_Capture", captureId);
                    cmd.Parameters.AddWithValue("@ID_StatusDetail", "");
                    cmd.Parameters.AddWithValue("@ImageUrl", imageUrl);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Comments", comments);

                    cmd.Connection.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr == null)
                        {
                            throw new NullReferenceException("No Information Available.");
                        }
                        while (sdr.Read())
                        {
                            Debug.WriteLine((string)sdr["CapturesNames"]);

                            Debug.WriteLine(sdr["Validacion"]);
                            Debug.WriteLine(sdr["msg"]);
                            Debug.WriteLine(sdr["ID"]);

                            MessageBox.Show((string)sdr["msg"]);

                            return new Response(Convert.ToBoolean(sdr["Validacion"]), sdr["msg"].ToString(), sdr["ID"].ToString());
                        }
                    }
                    cmd.Connection.Close();
                    opSuccess = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Image couldn't be saved due: {ex.Message}");

                return new Response(false, ex.Message, Guid.Empty.ToString());
            }

            return new Response(false, "", Guid.Empty.ToString());
        }
    }
}
