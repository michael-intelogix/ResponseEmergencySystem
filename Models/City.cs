using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models
{
    public class City
    {
        public string ID_City { get; }
        public string State { get; }
        public string Name { get; }

        public City(string ID, string state, string city)
        {
            ID_City = ID;
            State = state;
            Name = city;
        }
    }
}
