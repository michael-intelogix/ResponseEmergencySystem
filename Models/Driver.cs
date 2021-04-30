using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models
{
    public class Driver
    {
        public Guid ID_Driver { get; }
        public string Name { get; }
        public string LastName1 { get; }
        public string LastName2 { get; }
        public string PhoneNumber { get; }
        public string License { get; }
        public string ID_StateOfExpedition { get; }
        public string State { get; }
        public DateTime? ExpirationDate { get; set; }

        public Driver(Guid id, string license, string Expedition_State, string ExpirationDate = "")
        {
            ID_Driver = id;
            Name = "";
            LastName1 = "";
            LastName2 = "";
            PhoneNumber = "";
            ID_StateOfExpedition = Expedition_State;
            State = "";
            License = license;
            if (ExpirationDate != "")
                this.ExpirationDate = Convert.ToDateTime(ExpirationDate);
        }

        public Driver (Guid id, string name, string lastname1, string lastanme2, string phonenumber, string license, string Expedition_State, string state, string ExpirationDate = "")
        {
            ID_Driver = id;
            Name = name;
            LastName1 = lastname1;
            LastName2 = lastanme2;
            PhoneNumber = phonenumber;
            ID_StateOfExpedition = Expedition_State;
            State = state;
            License = license;
            if (ExpirationDate != "")
                this.ExpirationDate = Convert.ToDateTime(ExpirationDate);
        }
    }
}
