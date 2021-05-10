using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models
{
    public class PersonsInvolved
    {
        public string ID_Injured { get; }
        public string FullName { get; set; }
        public string Lastname1 { get; set; }
        public string LastName2 { get; set; }
        public string Age { get; set; }
        public string PhoneNumber { get; set; }
        public bool Driver { get; set; }
        public string DriverLicense { get; set; }
        public bool Private { get; set; }
        public bool Injured { get; set; }
        public string ID_Incident { get; set; }

        public PersonsInvolved (string fullname, string lastname1, string lastname2, string phonenumber)
        {

        }
    }
}
