using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Code
{
    class Location
    {

        public Guid? ID_Location { get; set; }
        public Guid ID_City { get; }
        public Guid ID_State { get; }
        public string Highway { get; }
        public string Latitude { get; }
        public string Longitude { get; }
        public string References { get; }

        public Location(Guid city, Guid state, string highway, string lat, string lon, string references)
        {
            ID_City = city;
            ID_State = state;
            Highway = highway;
            Latitude = lat;
            Longitude = lon;
            References = references;
        }
    }
}
