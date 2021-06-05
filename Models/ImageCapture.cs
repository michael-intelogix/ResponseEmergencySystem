using ResponseEmergencySystem.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models
{
    public class ImageCapture
    {
        public int ID { get; set; }
        public string ID_Image { get; set; }
        public string name { get; set; }
        public string comments { get; set; }
        public string ID_StatusDetail { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public string ImageFireBaseUrl { get; set; }
        public bool Uploaded { get; set; }
        public string Status { get; }

        public string FileType { get; }

        public Image Img { get; }
        public Image Image { get; }

        public ImageCapture(string n)
        {
            name = n;
            comments = "";
            ID_StatusDetail = "";
            ImagePath = "";
            ImageName = "";
        } 

        public ImageCapture(string n, string c, string id)
        {
            name = n;
            comments = c;
            ID_StatusDetail = id;
        }

        public ImageCapture(string imgName, string imgPath, int id)
        {
            this.name = "";
            this.comments = "";
            this.ID_StatusDetail = "";
            this.ImagePath = imgPath;
            this.ImageName = imgName;
            this.Uploaded = false;
            this.ID = id - 1;
        }

        public ImageCapture(string imageID, string imgName, string imgPath, string statusID, string status, string comments, string fileType)
        {
            this.ID_Image = imageID;
            this.name = "";
            this.comments = comments;
            this.ID_StatusDetail = statusID;
            this.ImagePath = imgPath;
            this.ImageName = imgName;
            this.Status = status;
            this.Uploaded = false;
            this.ID = 0;

            this.FileType = fileType;
            this.Image = fileType == "img" ? GetImage() : Resources.pdf;
        }

        //public ImageCapture(string imgName, string imgPath, string )
        //{
        //    this.name = "";
        //    this.comments = "";
        //    this.ID_StatusDetail = "";
        //    this.ImagePath = imgPath;
        //    this.ImageName = imgName;
        //    this.Uploaded = false;
        //    this.ID = id - 1;
        //}

        //public ImageCapture(string n, string c, string id, string imgPath)
        //{
        //    name = n;
        //    comments = c;
        //    ID_StatusDetail = id;
        //    ImageName = imgPath.Split(new string[] { "%2F" }, StringSplitOptions.None)[3].Split('?')[0].Replace("%20", " ");
        //}
        public ImageCapture(string c, string id, string captureId, string imgSubType, string imgType)
        {
            name = imgSubType + " of the " + imgType;
            comments = c;
            ID_StatusDetail = id;
            ImagePath = $"https://firebasestorage.googleapis.com/v0/b/dcmanagement-3d402.appspot.com/o/SIREM%2FCaptures%2F{captureId}%2F{imgSubType}%20of%20the%20{imgType}?alt=media&token=a2b4133a-affa-4234-8b62-bf5790fdfba4";
        }


        private Image GetImage()
        {
            Image newImg;
            try
            {
                System.Net.WebRequest request =
                System.Net.WebRequest.Create(this.ImagePath);
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
    }
}
