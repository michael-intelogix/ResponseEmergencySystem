using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Samsara_Models
{
    class Vehicle
    {
        public string ID { get; set; } = "";
        public string name { get; set; }
        public DateTime time { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public int heading { get; set; }
        public int speed { get; set; }
        public string formattedLocation { get; set; }
    }
}
