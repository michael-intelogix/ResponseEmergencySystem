using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models
{
    public class State
    {
        public string ID_State { get; }
        public string Country { get; }
        public string Name { get; }

        public State(string ID, string country, string state)
        {
            ID_State = ID;
            Country = country;
            Name = state;
        }
    }
}
