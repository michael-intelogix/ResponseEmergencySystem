using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models
{
    public class Capture
    {
        public Guid ID_Capture { get; } = Guid.NewGuid();
        public string ID_CaptureType { get; set; }
        public string captureType { get; }
        public List<ImageCapture> images { get; set; } = new List<ImageCapture>();

        private string name;

        public Capture(string id, string captureType, string captures)
        {
            ID_CaptureType = id;

            this.captureType = captureType;



            switch (captures)
            {
                case "Page":
                    string page = captures.Trim() + " " + 1;
                    images.Add(new ImageCapture(page, "", ""));
                    break;
                case "Capture":
                    name = captures.Trim().Trim() + " of the " + captureType;
                    images.Add(new ImageCapture(name, "", ""));
                    break;
                default:
                    foreach(var n in captures.Split(','))
                    {
                        name = n.Trim() + " of the " + captureType;
                        images.Add(new ImageCapture(name, "", ""));
                    }
                    break;
            }
        }

    }
}
