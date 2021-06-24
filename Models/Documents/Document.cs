﻿using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponseEmergencySystem.Models.Documents
{
    public class Document
    {
        public int ID { get; set; }
        public string ID_Document { get; set; }
        public string name { get; set; }
        public string containerName { get; set; }
        public string Path { get; set; } = "";
        public string FirebaseUrl { get; set; }
        public string Comments { get; set; }
        public string Type { get; set; }
        public string Status { get; private set; } = "created";

        public Image Image { get; private set; } = Resources.no_photo;

        public Document(string cName, int idx, string id = "")
        {
            containerName = cName;
            ID = idx;
            ID_Document = id;
        }

        public Document(string cName, string dName, int idx)
        {
            containerName = cName;
            name = dName;
            ID = idx;
        }

        public Document(string document, string documentUrl, string name, string type)
        {
            ID_Document = document;
            FirebaseUrl = documentUrl;
            this.name = name;
            Type = type;
            SetImage(true);
            Status = "loaded";
        }

        public void SetImage(bool firebaseUrl = false)
        {
            if (firebaseUrl)
            {
                this.Image = this.Type == "img" ? GetImage(firebaseUrl) : Resources.pdf;
            }
            else
            {
                this.Image = this.Type == "img" ? GetImage() : Resources.pdf;
            }
        }

        private Image GetImage(bool firebaseUrl = false)
        {
            Image newImg;
            try
            {
                System.Net.WebRequest request =
                System.Net.WebRequest.Create(firebaseUrl ? this.FirebaseUrl : this.Path);
                System.Net.WebResponse response = request.GetResponse();
                System.IO.Stream responseStream = response.GetResponseStream();

                using (var bmpTemp = new Bitmap(responseStream))
                {
                    newImg = new Bitmap(bmpTemp);
                }

                return newImg;
            }
            catch (System.Net.WebException ex)
            {
                //MessageBox.Show("There was an error opening the image file.");
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void Update(string status = "modified")
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files(*.PNG;*.JPG;*.GIF;*.BMP)|*.PNG;*.JPG;*.GIF;*.BMP|PDF Files (*.PDF)|*.PDF|All Files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string ext = System.IO.Path.GetExtension(ofd.FileName).ToUpper();
                    try
                    {
                        if (ext == ".GIF" || ext == ".JPG" || ext == ".PNG" || ext == ".BMP")
                        {
                            this.Path = ofd.FileName;
                            this.Type = "img";
                            if (this.name == "")
                                this.name = System.IO.Path.GetFileName(ofd.FileName).Replace(ext.ToLower(), "");
                            //this.SetImage();
                            this.Status = status;
                        }
                        else if (ext == ".PDF")
                        {
                            this.Path = ofd.FileName;
                            this.Type = "pdf";
                            if (this.name == "")
                                this.name = System.IO.Path.GetFileName(ofd.FileName).Replace(ext.ToLower(), "");
                            //this.SetImage();
                            this.Status = status;
                        }
                        else
                        {
                            Utils.ShowMessage("The file submitted is not an Image or PDF", title: "Image upload error", type: "Warning");
                        }


                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Utils.ShowMessage(ex.Message, title: "Image upload error", type: "Error");
                    }

                }

            }
        }

        public void SetName(string name)
        {
            this.Status = "modified";
            this.name = name;
        }

        public void SetStatus(string status)
        {
            Status = status;
        }

        //public void SetImage()
        //{
        //    if (Upload())
        //        Utils.ShowMessage("The image has been updated");
        //    else
        //        Utils.ShowMessage("There was an error when the file was uploaded");
        //}
        private bool Upload()
        {
            bool uploaded = false;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files(*.PNG;*.JPG;*.GIF;*.BMP)|*.PNG;*.JPG;*.GIF;*.BMP|PDF Files (*.PDF)|*.PDF|All Files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string ext = System.IO.Path.GetExtension(ofd.FileName).ToUpper();
                    try
                    {
                        if (ext == ".GIF" || ext == ".JPG" || ext == ".PNG" || ext == ".BMP")
                        {
                            this.Path = ofd.FileName;
                            this.Type = "img";
                            this.SetImage();
                            uploaded = true;
                        }
                        else if (ext == ".PDF")
                        {
                            this.Path = ofd.FileName;
                            this.Type = "pdf";
                            this.SetImage();
                            uploaded = true;
                        }
                        else
                        {
                            Utils.ShowMessage("The file submitted is not an Image or PDF", title: "Image upload error", type: "Warning");
                        }


                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Utils.ShowMessage(ex.Message, title: "Image upload error", type: "Error");
                    }

                }

            }

            return uploaded;
        }
    }
}