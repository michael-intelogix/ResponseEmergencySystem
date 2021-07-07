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
        public Guid ID_Incident { get; set; }
        public string ID_CaptureType { get; set; }
        public string ID_StatusDetail { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public string comments { get; set; }
        public string captureType { get; }
        public string Description { get; set; }

        public List<ImageCapture> imagesListOfNames { get; set; } = new List<ImageCapture>();

        private string name;

        public Capture()
        {

        }

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


        public Capture(string id, string statusId, string captureTypeId, string type, string imgPath, string comments, string Description, string NamesOfImages)
        {
            var localID = "FDD0C17C7-2D9C-4A84-8851-5647A8373669";
            var des = Description;
            ID_Capture = Guid.Parse(id);
            ID_StatusDetail = statusId;
            ID_CaptureType = captureTypeId;
            captureType = type;
            ImagePath = imgPath;
            if (!des.Contains(",") && des.Contains("_"))
            {
                ImageName = des.Split('_')[0] + " of the " + type;
                ImagePath = $"https://firebasestorage.googleapis.com/v0/b/dcmanagement-3d402.appspot.com/o/SIREM%2{localID}%2F{des.Split('_')[0]}%20of%20the%20{type}?alt=media&token={des.Split('-')[1]}";
            }
            else
            {
                foreach (var code in des.Split(','))
                {
                    ImageName = code.Split('_')[0] + " of the " + type;
                    ImagePath = $"https://firebasestorage.googleapis.com/v0/b/dcmanagement-3d402.appspot.com/o/SIREM%2{localID}%2F{code.Split('_')[0]}%20of%20the%20{type}?alt=media&token={code.Split('_')[1]}";
                }
            }
            
            this.comments = comments;
            this.Description = "";
            if (NamesOfImages.Length > 0)
            {
                foreach (var n in NamesOfImages.Split(','))
                {
                    //imagesListOfNames.Add(new ImageCapture(comments, statusId, captureTypeId, n.Trim(), captureType));
                }
            } 
            
        }

        public Capture(string id, string statusId, string captureTypeId, string type, string comments, string description)
        {
            ID_Capture = Guid.Parse(id);
            ID_StatusDetail = statusId;
            ID_CaptureType = captureTypeId;
            captureType = type;

            this.comments = comments;
            this.Description = description;

            ImagePath = "";
            ImageName = "";
        }

        public Capture(string id, string incidentId, string statusId, string captureTypeId, string type, string comments, string description)
        {
            ID_Incident = Guid.Parse(incidentId);

            ID_Capture = Guid.Parse(id);
            ID_StatusDetail = statusId;
            ID_CaptureType = captureTypeId;
            captureType = type;

            this.comments = comments;
            this.Description = description;

            ImagePath = "";
            ImageName = "";
        }


        public List<Documents.Document> GetCaptures(string captures, string captureType)
        {
            List<Documents.Document> _imagesListOfNames = new List<Documents.Document>();
            switch (captures)
            {
                case "Page":
                    string page = captures.Trim() + " " + 1;
                    _imagesListOfNames.Add(new Documents.Document(page, "empty"));
                    break;
                case "Capture":
                    name = captures.Trim().Trim() + " of the " + captureType;
                    _imagesListOfNames.Add(new Documents.Document(name, "empty"));
                    break;
                default:
                    foreach (var n in captures.Split(','))
                    {
                        name = n.Trim() + " of the " + captureType;
                        _imagesListOfNames.Add(new Documents.Document(name, "empty"));
                    }
                    break;
            }

            return _imagesListOfNames;
        }

    }
}
