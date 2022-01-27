using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models
{
    public class DocumentCapture
    {
        public int ID { get; set; }
        public string ID_Image { get; set; }
        public string ID_Document { get; set; }
        public string ID_CaptureType { get; set; }
        public string name { get; set; }
        public string containerName { get; set; }
        public string Path { get; set; }
        public string FirebaseUrl { get; set; }
        public string CaptureType { get; set; }
        public string Type { get; set; }
        public string comments { get; set; }
        public string ID_StatusDetail { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public string ImageFireBaseUrl { get; set; }
        public bool Uploaded { get; set; }
        public string Status { get; }

        public Image Img { get; }
        public Image Image { get; }

        public DocumentCapture(string cName, int idx)
        {
            containerName = cName;
            ID = idx;
        }

        public DocumentCapture(string cName, string dName, int idx)
        {
            containerName = cName;
            name = dName;
            ID = idx;
        }

        public DocumentCapture(string id, string idCaptureType, string idStatusDetail)
        {
            this.ID_Document = id;
            ID_CaptureType = idCaptureType;
            ID_StatusDetail = idStatusDetail;
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
