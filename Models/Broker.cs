using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models
{
    public class Broker
    {
        public string ID_Broker { get; }
        public string ID_State { get;  }
        public string ID_City { get; }
        public string State { get; }
        public string City { get; }
        public string Name { get; }
        public string Address { get; }

        public Broker (string ID_Broker, string ID_State, string ID_City, string state, string city, string name, string address)
        {
            this.ID_Broker = ID_Broker;
            this.ID_State = ID_State;
            this.ID_City = ID_City;
            State = state;
            City = city;
            Name = name;
            Address = address;
        }

        public Broker (string ID_State, string ID_City, string name, string address)
        {
            ID_Broker = Guid.NewGuid().ToString();
            this.ID_State = ID_State;
            this.ID_City = ID_City;
            State = "";
            City = "";
            Name = name;
            Address = address;
        }

        public Broker (string ID_Broker, string name)
        {
            this.ID_Broker = ID_Broker;
            Name = name;
            ID_State = "";
            ID_City = "";
            State = "";
            City = "";
            Address = "";
        }
    }
}
