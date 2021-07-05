﻿ using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.EF;
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
        public static Response response;

        public static List<Models.Capture> list_CaptureTypes()
        {
            
            List<Models.Capture> result = new List<Models.Capture>();

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
                                new Models.Capture(
                                    (string)sdr["ID_CaptureType"],
                                    (string)sdr["Name"],
                                    (string)sdr["CapturesNames"]
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

        public static List<Models.Capture> list_Captures(string ID_Incident)
        {
            
            List<Models.Capture> result = new List<Models.Capture>();

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
                                new Models.Capture(
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
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Capture couldn't be found due: {ex.Message}");
            }

            return result;
        }

        public static List<ImageCapture> list_Images(string ID_Capture)
        {
            
            List<ImageCapture> result = new List<ImageCapture>();

            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"List_Images",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                    {
                        cmd.Connection.Close();
                    }

                    cmd.Parameters.AddWithValue("@ID_Capture", Guid.Parse(ID_Capture));

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
                                new ImageCapture(
                                    sdr["ID_Image"].ToString(),
                                    (string)sdr["Description"],
                                    (string)sdr["ImageUrl"],
                                    (string)sdr["ID_StatusDetail"].ToString(),
                                    (string)sdr["Status"],
                                    (string)sdr["Comments"],
                                    (string)sdr["FileType"]
                                )
                            );
                        }
                    }
                    cmd.Connection.Close();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Image couldn't be found due: {ex.Message}");
            }

            return result;
        }

        public static Response AddCapture(string captureId, string captureTypeId, string incidentId, string description, string comments, string statusDetailId = "", bool create = true)
        {
            

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

                    cmd.Parameters.AddWithValue("@ID_Capture", captureId);
                    cmd.Parameters.AddWithValue("@ID_CaptureType", captureTypeId);
                    cmd.Parameters.AddWithValue("@ID_Incident", incidentId);
                    cmd.Parameters.AddWithValue("@ID_StatusDetail", statusDetailId);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Comments", comments);
                    cmd.Parameters.AddWithValue("@Create", create);

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

                            return new Response(Convert.ToBoolean(sdr["Validacion"]), sdr["msg"].ToString(), sdr["ID"].ToString());
                        }
                    }
                    cmd.Connection.Close();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Capture couldn't be saved due: {ex.Message}");

                return new Response(false, ex.Message, Guid.Empty.ToString());
            }

            return new Response(false, "", Guid.Empty.ToString());
        }

        public static Response AddImage(string imageId, string captureId, string imageUrl, string description, string comments, string fileType)
        {
            

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
                    cmd.Parameters.AddWithValue("@FileType", fileType);
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

                            Debug.WriteLine(sdr["Validacion"]);
                            Debug.WriteLine(sdr["msg"]);
                            Debug.WriteLine(sdr["ID"]);

                            //MessageBox.Show((string)sdr["msg"]);

                            return new Response(Convert.ToBoolean(sdr["Validacion"]), sdr["msg"].ToString(), sdr["ID"].ToString());
                        }
                    }
                    cmd.Connection.Close();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Image couldn't be saved due: {ex.Message}");

                return new Response(false, ex.Message, Guid.Empty.ToString());
            }

            return new Response(false, "", Guid.Empty.ToString());
        }
        
        public static async Task<Response> AddImage(string imageId, string captureId, Firebase.Storage.FirebaseStorageTask imageUrl, string description, string comments, string fileType)
        {
            

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
                    cmd.Parameters.AddWithValue("@ImageUrl", await imageUrl);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Comments", comments);
                    cmd.Parameters.AddWithValue("@FileType", fileType);

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

                            return new Response(Convert.ToBoolean(sdr["Validacion"]), sdr["msg"].ToString(), sdr["ID"].ToString());
                        }
                    }
                    cmd.Connection.Close();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Image couldn't be saved due: {ex.Message}");

                return new Response(false, ex.Message, Guid.Empty.ToString());
            }

            return new Response(false, "", Guid.Empty.ToString());
        }

        public static Response UpdateImage(string imageId, string captureId, string imageUrl, string description, string comments, string fileType)
        {
            

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
                    cmd.Parameters.AddWithValue("@FileType", fileType);
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

                            Debug.WriteLine(sdr["Validacion"]);
                            Debug.WriteLine(sdr["msg"]);
                            Debug.WriteLine(sdr["ID"]);

                            //MessageBox.Show((string)sdr["msg"]);

                            return new Response(Convert.ToBoolean(sdr["Validacion"]), sdr["msg"].ToString(), sdr["ID"].ToString());
                        }
                    }
                    cmd.Connection.Close();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Image couldn't be saved due: {ex.Message}");

                return new Response(false, ex.Message, Guid.Empty.ToString());
            }

            return new Response(false, "", Guid.Empty.ToString());
        }

        public static async Task<Response> UpdateImage(string imageId, string captureId, Firebase.Storage.FirebaseStorageTask imageUrl, string description, string comments, string fileType)
        {
            

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
                    cmd.Parameters.AddWithValue("@ImageUrl", await imageUrl);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Comments", comments);
                    cmd.Parameters.AddWithValue("@FileType", fileType);
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

                            Debug.WriteLine(sdr["Validacion"]);
                            Debug.WriteLine(sdr["msg"]);
                            Debug.WriteLine(sdr["ID"]);

                            //MessageBox.Show((string)sdr["msg"]);

                            return new Response(Convert.ToBoolean(sdr["Validacion"]), sdr["msg"].ToString(), sdr["ID"].ToString());
                        }
                    }
                    cmd.Connection.Close();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Image couldn't be saved due: {ex.Message}");

                return new Response(false, ex.Message, Guid.Empty.ToString());
            }

            return new Response(false, "", Guid.Empty.ToString());
        }

        public static Response UpdateCapture(string ID_Capture, string captureTypeId, string incidentId, string description, string comments, string statusDetailId = "")
        {
            

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

                    cmd.Parameters.AddWithValue("@ID_Capture", Guid.Parse(ID_Capture));
                    cmd.Parameters.AddWithValue("@ID_CaptureType", Guid.Empty);
                    cmd.Parameters.AddWithValue("@ID_Incident", Guid.Empty);
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

                            //MessageBox.Show((string)sdr["msg"]);

                            return new Response(Convert.ToBoolean(sdr["Validacion"]), sdr["msg"].ToString(), sdr["ID"].ToString());
                        }
                    }
                    cmd.Connection.Close();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Capture couldn't be saved due: {ex.Message}");

                return new Response(false, ex.Message, Guid.Empty.ToString());
            }

            return new Response(false, "", Guid.Empty.ToString());
        }

        public static Response UpdateImageData(string ID_Image, string ImgUrl, string comments, string fileType)
        {
            

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

                    cmd.Parameters.AddWithValue("@ID_Image", Guid.Parse(ID_Image));
                    cmd.Parameters.AddWithValue("@ID_Capture", Guid.Empty);
                    cmd.Parameters.AddWithValue("@ID_StatusDetail", "");
                    cmd.Parameters.AddWithValue("@ImageUrl", "");
                    cmd.Parameters.AddWithValue("@Description", "");
                    cmd.Parameters.AddWithValue("@Comments", comments);
                    cmd.Parameters.AddWithValue("@FileType", fileType);
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

                            return new Response(Convert.ToBoolean(sdr["Validacion"]), sdr["msg"].ToString(), sdr["ID"].ToString());
                        }
                    }
                    cmd.Connection.Close();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Capture couldn't be saved due: {ex.Message}");

                return new Response(false, ex.Message, Guid.Empty.ToString());
            }

            return new Response(false, "", Guid.Empty.ToString());
        }
        
        public static Response UpdateCaptureData(string ID_Capture, string comments)
        {
            

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

                    cmd.Parameters.AddWithValue("@ID_Capture", Guid.Parse(ID_Capture));
                    cmd.Parameters.AddWithValue("@ID_CaptureType", Guid.Empty);
                    cmd.Parameters.AddWithValue("@ID_Incident", Guid.Empty);
                    cmd.Parameters.AddWithValue("@ID_StatusDetail", "");
                    cmd.Parameters.AddWithValue("@Description", "");
                    cmd.Parameters.AddWithValue("@Comments", comments);
                    cmd.Parameters.AddWithValue("@Create", false);

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

                            return new Response(Convert.ToBoolean(sdr["Validacion"]), sdr["msg"].ToString(), sdr["ID"].ToString());
                        }
                    }
                    cmd.Connection.Close();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Capture couldn't be saved due: {ex.Message}");

                return new Response(false, ex.Message, Guid.Empty.ToString());
            }

            return new Response(false, "", Guid.Empty.ToString());
        }

        public static List<Models.Documents.DocumentCapture> ListDocumentsCapture(Guid incident)
        {
            List<Models.Documents.DocumentCapture> result = new List<Models.Documents.DocumentCapture>();

            Guid ID_Incident = incident;
            using (var db = new SIREMEntities1())
            {
                var captures = db.Captures.Where(i => i.ID_Incident == ID_Incident).ToList();

                using (var DCManagement = new DCManagementEntities1())
                {
                    if (captures.Count > 0)
                    {
                        for (var i = 0; i < captures.Count(); i++)
                        {
                            Guid ID_CaptureType = (Guid)captures[i].ID_CaptureType;
                            Guid ID_Capture = (Guid)captures[i].ID_Capture;

                            var captureType = DCManagement.Capture_Type.Where(ct => ct.ID_CaptureType == ID_CaptureType).First();
                            var documents = db.Images.Where(ic => ic.ID_Capture == ID_Capture && ic.Status == true).ToList();

                            result.Add(
                                new Models.Documents.DocumentCapture(
                                    captures[i].ID_Capture.ToString(),
                                    captures[i].ID_CaptureType.ToString(),
                                    captureType.Name,
                                    ""
                                    )
                                );

                            result[i].documents = new List<Models.Documents.Document>();

                            var c = new Models.Capture();
                            var x = c.GetCaptures(captureType.CapturesNames, captureType.Name);

                            for (var y = 0; y < x.Count; y++)
                            {
                                var docFound = documents.Where(d => d.Description == x[y].name).ToList();
                                if (docFound.Count > 0)
                                {
                                    var doc = docFound.FirstOrDefault();
                                    result[i].documents.Add(
                                    new Models.Documents.Document(
                                        doc.ID_Image.ToString(),
                                        doc.ImageUrl,
                                        doc.Description,
                                        doc.FileType,
                                        true
                                        )
                                    );
                                }
                                else
                                {
                                    result[i].documents.Add(
                                    new Models.Documents.Document(
                                        x[y].name,
                                        "empty"
                                        )
                                    );
                                }

                            }

                            for (var t = 0; t < documents.Count; t++)
                            {
                                var docFound = result[i].documents.Where(d => d.name == documents[t].Description).ToList();
                                if (docFound.Count == 0)
                                {
                                    result[i].documents.Add(
                                    new Models.Documents.Document(
                                        documents[t].ID_Image.ToString(),
                                        documents[t].ImageUrl,
                                        documents[t].Description,
                                        documents[t].FileType
                                        )
                                    );
                                }

                            }
                        }
                    }
                }

                return result;
                //Console.WriteLine("Registro actualizado correctamente.");
                //Utils.ShowMessage($"Incident with folio: {folio} has been deleted", title: "Incident Deleted", type: "approved");
                //return new Response()
            }


        }

        public static void DeleteImageCapture(string imageID)
        {
            Guid ID_Image = Guid.Parse(imageID);
            using (var db = new SIREMEntities1())
            {
                Images imageCapture = db.Images.Where(i => i.ID_Image == ID_Image).FirstOrDefault();

                imageCapture.Status = false;

                db.Entry(imageCapture).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();

                Console.WriteLine("Registro actualizado correctamente.");

                //return new Response()
            }
        }


        //public static Response DeleteDocument(ID_)
        
    }
}
