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
        public string ID_Status_detail { get; set; }

        public ImageCapture(string n, string c, string id)
        {
            name = n;
            comments = c;
            ID_Status_detail = id;
        }
    }
}
