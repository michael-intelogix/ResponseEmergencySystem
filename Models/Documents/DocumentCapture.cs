using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models.Documents
{
    public class DocumentCapture
    {
        public string ID_Capture { get; set; }
        public string ID_CaptureType { get; set; }
        public string ID_Incident { get; set; }
        public string name { get; set; }
        public string CaptureType { get; set; }
        public string comments { get; set; }
        public List<Document> documents { get; set; }
        public string Status { get; }


        public DocumentCapture(string captureTypeID, string captureType, string c)
        {
            ID_Incident = Guid.Empty.ToString();
            ID_CaptureType = captureTypeID;
            CaptureType = captureType;
            this.comments = comments;
        }
    }
}
