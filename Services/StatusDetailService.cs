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
using ResponseEmergencySystem.EF;

namespace ResponseEmergencySystem.Services
{
    public static class StatusDetailService
    {

        public static List<StatusDetail> list_StatusDetail()
        {
            
            List<StatusDetail> result = new List<StatusDetail>();
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.GeneralConnection,
                    CommandText = $"List_StatusDetail",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Connection.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr == null)
                        {
                            throw new NullReferenceException("No Information Available.");
                        }
                        while (sdr.Read())
                        {
                            Debug.WriteLine((string)sdr["Description"]);

                            result.Add(
                                new StatusDetail(
                                    (string)sdr["ID_StatusDetail"],
                                    (string)sdr["Description"]
                                )
                            );
                        }
                    }
                    cmd.Connection.Close();
   
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
