using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        public bool locked { get; private set; } = false;

        private System.IO.Stream _responseStream;

        public Image Image { get; private set; } = Resources.no_photo;

        public Document(string name, string status)
        {
            this.name = name;
            this.Status = status;
            locked = true;
        }

        public Document(string cName, int idx, string id = "")
        {
            containerName = cName;
            ID = idx;
            ID_Document = id;
        }

        public Document(string cName, string dName, int idx, bool empty = false)
        {
            containerName = cName;
            name = dName;
            ID = idx;
            locked = true;
            if (empty)
                Status = "empty";
        }

        public Document(string document, string documentUrl, string name, string type, bool locked = false)
        {
            ID_Document = document;
            FirebaseUrl = documentUrl;
            this.name = name;
            Type = type;
            SetImage(true);
            Status = "loaded";
            this.locked = locked;
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

        public void ClearImage()
        {
            this.Image = null;
            this.Image = Resources.no_photo;
        }

        public Image GetImage(bool firebaseUrl = false, bool temp = false)
        {
            CloseStream();
            Image newImg;
            try
            {
                System.Net.WebRequest request =
                System.Net.WebRequest.Create(firebaseUrl ? this.FirebaseUrl : this.Path);
                System.Net.WebResponse response = request.GetResponse();
                _responseStream = response.GetResponseStream();

                using (var bmpTemp = new Bitmap(_responseStream))
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

        public void Update(string status = "modified", bool get = false)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files(*.PNG;*.JPG;*.GIF;*.BMP)|*.PNG;*.JPG;*.GIF;*.BMP|PDF Files (*.PDF)|*.PDF|All Files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    bool isFileLocked = IsFileLocked(ofd.FileName);
                    Utils.ShowMessage($"the file is locked: {isFileLocked}", type: isFileLocked ? "Error" : "approved");
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
                
                if (ofd.ShowDialog() == DialogResult.Cancel)
                {
                    this.Status = "disposed";
                }

            }
        }

        private bool IsFileLocked(string filePath)
        {
            try
            {
                using (FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    if (fileStream != null) fileStream.Close();  //This line is me being overly cautious, fileStream will never be null unless an exception occurs... and I know the "using" does it but its helpful to be explicit - especially when we encounter errors - at least for me anyway!
                }
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }

            //file is not locked
            return false;
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
        public void CloseStream()
        {
            Image = Resources.no_photo;
            if (_responseStream != null)
                _responseStream.Close();
            _responseStream = null;
        }

        public void SetLocked()
        {
            locked = !locked;
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
