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
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public string Age { get; set; }
        public string PhoneNumber { get; set; }
        public bool Driver { get; set; }
        public string DriverLicense { get; set; }
        public bool PrivatePerson { get; set; }
        public bool Injured { get; set; }
        public string ID_Incident { get; set; }

        public string IsDriver { get; private set; }
        public string IsPassenger { get; private set; }
        public string IsInjured { get; private set; }
        public string IsPrivate { get; private set; }

        public string Hospital { get; set; }
        public string Comments { get; set; }

        public PersonsInvolved(string ID, string fullname, string lastname1, string phonenumber, string age, bool driver, string license, bool privatePerson, bool injured, string hospital, string comments, string incidentId, string lastname2 = "")
        {
            ID_Injured = ID;
            FullName = fullname;
            LastName1 = lastname1;
            LastName2 = lastname2;
            PhoneNumber = phonenumber;
            Age = age;
            Driver = driver;
            DriverLicense = license;
            PrivatePerson = privatePerson;
            Injured = injured;
            Hospital = hospital;
            Comments = comments;
            ID_Incident = incidentId;

            IsDriver = driver ? "yes" : "no";
            IsPassenger = !driver ? "yes" : "no";
            IsInjured = injured ? "yes" : "no";
            IsPrivate = privatePerson ? "yes" : "no";
        }

        public PersonsInvolved(string fullname, string lastname1, string phonenumber, string age, bool driver, string license, bool privatePerson, bool injured, string hospital, string comments, string incidentId, string lastname2 = "")
        {
            ID_Injured = Guid.Empty.ToString();
            FullName = fullname;
            LastName1 = lastname1;
            LastName2 = lastname2;
            PhoneNumber = phonenumber;
            Age = age;
            Driver = driver;
            DriverLicense = license;
            PrivatePerson = privatePerson;
            Injured = injured;
            Hospital = hospital;
            Comments = comments;
            ID_Incident = incidentId;

            IsDriver = driver ? "yes" : "no";
            IsPassenger = !driver ? "yes" : "no";
            IsInjured = injured ? "yes" : "no";
            IsPrivate = privatePerson ? "yes" : "no";
        }

        public PersonsInvolved (string fullname, string lastname1, string phonenumber, string age, bool driver, string license, bool privatePerson, bool injured, string incidentId, string lastname2 = "")
        {
            ID_Injured = Guid.Empty.ToString();
            FullName = fullname;
            LastName1 = lastname1;
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

        public void SetPrivate(bool p)
        {
            PrivatePerson = p;
            IsPrivate = p ? "yes" : "no";
        }

        public void SetDriver(bool d)
        {
            Driver = d;
            IsDriver = d ? "yes" : "no";
            IsPassenger = !d ? "yes" : "no";
        }

        public void SetInjured(bool i)
        {
            Injured = i;
            IsInjured = i ? "yes" : "no";
        }
    }
}
