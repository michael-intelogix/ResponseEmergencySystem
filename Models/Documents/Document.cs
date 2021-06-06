using ResponseEmergencySystem.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string comments { get; set; }
        public string Type { get; set; }
        public string Status { get; }

        public Image Image { get; private set; } = Resources.no_photo;

        public Document(string cName, int idx)
        {
            containerName = cName;
            ID = idx;
        }

        public Document(string cName, string dName, int idx)
        {
            containerName = cName;
            name = dName;
            ID = idx;
        }

        public void SetImage()
        {
            this.Image = this.Type == "img" ? GetImage() : Resources.pdf;
        }

        private Image GetImage()
        {
            Image newImg;
            try
            {
                System.Net.WebRequest request =
                System.Net.WebRequest.Create(this.Path);
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
