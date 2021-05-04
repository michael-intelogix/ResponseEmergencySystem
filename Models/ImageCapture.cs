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
        public ImageCapture(string c, string id, string captureId, string imgSubType, string imgType)
        {
            name = imgSubType + " of the " + imgType;
            comments = c;
            ID_StatusDetail = id;
            ImagePath = $"https://firebasestorage.googleapis.com/v0/b/dcmanagement-3d402.appspot.com/o/SIREM%2FCaptures%2F{captureId}%2F{imgSubType}%20of%20the%20{imgType}?alt=media&token=a2b4133a-affa-4234-8b62-bf5790fdfba4";
        }
    }
}
