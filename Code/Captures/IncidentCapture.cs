using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Code.Captures
{
    public class Capture
    {
        public Guid ID_Capture;

        public DateTime captureDate;

        public string type;

        public int totalOfcaptures;

        public string comments;

        public Capture(DateTime captureDate, string type, int totalOfCaptures, string comments)
        {
            ID_Capture = Guid.NewGuid();
            this.captureDate = captureDate;
            this.type = type;
            this.totalOfcaptures = totalOfCaptures;
            this.comments = comments;
        }
    }
}
