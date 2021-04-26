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

        public static List<Capture> list_Captures()
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

    }
}
