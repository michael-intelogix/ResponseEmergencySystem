using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models
{
    public class Capture
    {
        public Guid ID_Capture { get; }
        public string ID_CaptureType { get; set; }
        public string ID_StatusDetail { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public string comments { get; }
        public string captureType { get; }

        public List<ImageCapture> imagesListOfNames { get; set; } = new List<ImageCapture>();

        private string name;

        public Capture(string id, string captureType, string captures)
        {
            ID_Capture = Guid.NewGuid();
            ID_CaptureType = id;
            ID_StatusDetail = "423E82C9-EE3F-4D83-9066-01E6927FE14D";
            this.comments = "";

            this.captureType = captureType;

            switch (captures)
            {
                case "Page":
                    string page = captures.Trim() + " " + 1;
                    imagesListOfNames.Add(new ImageCapture(page));
                    break;
                case "Capture":
                    name = captures.Trim().Trim() + " of the " + captureType;
                    imagesListOfNames.Add(new ImageCapture(name));
                    break;
                default:
                    foreach(var n in captures.Split(','))
                    {
                        name = n.Trim() + " of the " + captureType;
                        imagesListOfNames.Add(new ImageCapture(name));
                    }
                    break;
            }
        }

        public Capture(string id, string statusId, string captureTypeId, string type, string imgPath, string comments)
        {
            ID_Capture = Guid.Parse(id);
            ID_StatusDetail = statusId;
            ID_CaptureType = captureTypeId;
            captureType = type;
            ImagePath = imgPath;
            ImageName = imgPath.Split(new string[] { "%2F" }, StringSplitOptions.None)[3].Split('?')[0].Replace("%20", " ");
            this.comments = comments;

        }

    }
}
