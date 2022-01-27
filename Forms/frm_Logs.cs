using DevExpress.XtraEditors;
using ResponseEmergencySystem.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponseEmergencySystem.Forms
{
    public partial class frm_Logs : DevExpress.XtraEditors.XtraForm
    {
        public frm_Logs()
        {
            InitializeComponent();
        }

        private void frm_Logs_Load(object sender, EventArgs e)
        {
            List<Models.Logs.Log> logs = new List<Models.Logs.Log>();

            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"List_logs",
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

                            logs.Add(
                                new Models.Logs.Log(
                                    (string)sdr["Description"],
                                    (string)sdr["Value"],
                                    (string)sdr["NewValue"],
                                    (string)sdr["Username"],
                                    (DateTime)sdr["LogDate"]
                                    )
                                ); 
                            

                            //MessageBox.Show((string)sdr["msg"]);

                            
                        }
                    }
                    cmd.Connection.Close();

                    gc_Logs.DataSource = logs;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Incident couldn't be saved due: {ex.Message}");
                Debug.WriteLine(ex.Message);
        
            }

           
        }
    }
}