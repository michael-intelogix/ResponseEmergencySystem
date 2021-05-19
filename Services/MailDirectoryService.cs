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
    public static class MailDirectoryService
    {
        public static Response response;

        public static Response Add_Category(string category)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"Update_MailCategory",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Parameters.AddWithValue("@ID_Category", Guid.Empty);
                    cmd.Parameters.AddWithValue("@Category", category);
                    cmd.Parameters.AddWithValue("@Status", true);

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

                            //MessageBox.Show((string)sdr["msg"]);

                            response = new Response(Convert.ToBoolean(sdr["Validacion"]), sdr["msg"].ToString(), sdr["ID"].ToString());
                        }
                    }
                    cmd.Connection.Close();

                }

                return response;

            }
            catch (Exception ex)
            {
                return new Response(false, ex.Message, Guid.Empty.ToString());
            }
        }
        
        public static Response AddMailToDirectory(string mail, string category)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"Update_Mail",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Parameters.AddWithValue("@ID_Mail", Guid.Empty);
                    cmd.Parameters.AddWithValue("@Mail", mail);
                    cmd.Parameters.AddWithValue("@Category", category);
                    cmd.Parameters.AddWithValue("@Status", true);

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

                            //MessageBox.Show((string)sdr["msg"]);

                            response = new Response(Convert.ToBoolean(sdr["Validacion"]), sdr["msg"].ToString(), sdr["ID"].ToString());
                        }
                    }
                    cmd.Connection.Close();

                }

                return response;

            }
            catch (Exception ex)
            {
                return new Response(false, ex.Message, Guid.Empty.ToString());
            }
        }

        public static List<MailDirectory> GetMailDirectory(string category = "")
        {
            var directory = new List<MailDirectory>();

            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"List_Mails",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Parameters.AddWithValue("@Category", category == "" ? "" : category);

                    cmd.Connection.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr == null)
                        {
                            throw new NullReferenceException("No Information Available.");
                        }
                        //else
                        //{

                        //}
                        while (sdr.Read())
                        {

                            if (!Utils.CheckIfColumnExistInDataReader(sdr, "Validacion"))
                            {
                                Debug.WriteLine(sdr["ID_Mail"]);
                                directory.Add(
                                    new MailDirectory(
                                        Convert.ToString(sdr["ID_Mail"]),
                                        (string)sdr["Mail"],
                                        Convert.ToString(sdr["Category"])
                                    )
                                );
                            }
                            else
                            {
                                Utils.ShowMessage(Convert.ToString(sdr["msg"]), "Mail Directory Error");
                            }
                        }



                    }
                    cmd.Connection.Close();

                }
                return directory;

            }
            catch (Exception ex)
            {
                Utils.ShowMessage(ex.Message, "Mail Directory Error");
                return directory;
            }
            
        }

        public static List<MailCategory> GetCategories()
        {
            var categories = new List<MailCategory>();

            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"List_MailCategories",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Parameters.AddWithValue("@Category", "");

                    cmd.Connection.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        if (sdr == null)
                        {
                            throw new NullReferenceException("No Information Available.");
                        }

                        while (sdr.Read())
                        {

                            if (!Utils.CheckIfColumnExistInDataReader(sdr, "Validacion"))
                            {
                                Debug.WriteLine(sdr["ID_Category"]);
                                categories.Add(
                                    new MailCategory(
                                        Convert.ToString(sdr["ID_Category"]),
                                        (string)sdr["Category"]
                                    )
                                );
                            }
                            else
                            {
                                Utils.ShowMessage(Convert.ToString(sdr["msg"]), "Mail Directory Error");
                            }
                        }



                    }
                    cmd.Connection.Close();

                }
                return categories;

            }
            catch (Exception ex)
            {
                Utils.ShowMessage(ex.Message, "Mail Directory Error");
                return categories;
            }
        }
    }
}
