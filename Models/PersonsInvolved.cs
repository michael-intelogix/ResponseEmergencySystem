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
        public bool PrivatePerson { get; set; }
        public bool Injured { get; set; }
        public string ID_Incident { get; set; }

        public string IsDriver { get; }
        public string IsPassenger { get; }
        public string IsInjured { get; }
        public string IsPrivate { get; }

        public PersonsInvolved (string fullname, string lastname1, string phonenumber, string age, bool driver, string license, bool privatePerson, bool injured, string incidentId, string lastname2 = "")
        {
            FullName = fullname;
            Lastname1 = lastname1;
            LastName2 = lastname2;
            PhoneNumber = phonenumber;
            Age = age;
            Driver = driver;
            DriverLicense = license;
            PrivatePerson = privatePerson;
            Injured = injured;
            ID_Incident = incidentId;

            IsDriver = driver ? "yes" : "no";
            IsPassenger = !driver ? "yes" : "no";
            IsInjured = injured ? "yes" : "no";
            IsPrivate = privatePerson ? "yes" : "no";
        }
    }
}
