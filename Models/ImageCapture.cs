using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models
{
    public class ImageCapture
    {
        public string name { get; set; }
        public string comments { get; set; }
        public string ID_StatusDetail { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }

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

        public ImageCapture(string n, string c, string id, string imgPath)
        {
            name = n;
            comments = c;
            ID_StatusDetail = id;
            ImageName = imgPath.Split(new string[] { "%2F" }, StringSplitOptions.None)[3].Split('?')[0].Replace("%20", " ");
        }
    }
}
