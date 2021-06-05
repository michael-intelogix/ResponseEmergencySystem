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
        private static Boolean opSuccess;

        public static List<StatusDetail> list_StatusDetail()
        {
            opSuccess = false;
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
                    opSuccess = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Capture type couldn't be found due: {ex.Message}");
            }

            return result;
        }

        public static void UpdateStatus(string incidentID, string status)
        {
            Guid ID_Incident = Guid.Parse(incidentID);
            using (var db = new SIREMEntities())
            {
                Incidents incident = (Incidents)db.Incidents.Where(i => i.ID_Incident == ID_Incident).FirstOrDefault();

                incident.ID_StatusDetail = status;

                db.Entry(incident).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

                Console.WriteLine("Registro actualizado correctamente.");
                Utils.ShowMessage("Status has been updated correctly.", title: "Status Updated", type: "Approved");
                //return new Response()
            }
        }
    }
}
