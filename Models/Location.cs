using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models
{
    public class Location
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime CreatedAt { get; set; }

        public Location(string lat, string lon, DateTime createdAt)
        {
            Latitude = lat;
            Longitude = lon;
            CreatedAt = createdAt;
        }
    }
}
