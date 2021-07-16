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
        public string ID_Samsara { get; }
        public string Name { get; }
        public string LastName1 { get; }
        public string LastName2 { get; }
        public string PhoneNumber { get; }
        public string License { get; }
        public string ID_StateOfExpedition { get; }
        public string State { get; }
        public DateTime? ExpirationDate { get; set; }

        public bool dSamsara;

        //List Drivers
        public Driver()
        {
            ID_Driver = Guid.Empty;
            ID_Samsara = "";
            Name = "";
            LastName1 = "";
            LastName2 = "";
            PhoneNumber = "";
            ID_StateOfExpedition = "";
            State = "";
            License = "";
        }

        //Get Driver
        public Driver(string id, string fullname, string phonenumber, string license, string Expedition_State, bool DSamsara, string ExpirationDate = "")
        {
            ID_Driver = !DSamsara ? Guid.Parse(id) : Guid.Empty;
            ID_Samsara = DSamsara ? id : "";
            Name = fullname;
            LastName1 = "";
            LastName2 = "";
            PhoneNumber = phonenumber;
            ID_StateOfExpedition = Expedition_State;
            State = "";
            License = license;
            if (ExpirationDate != "")
                this.ExpirationDate = Convert.ToDateTime(ExpirationDate);
        }

        public Driver (Guid id, string ID_Samsara, string fullName, string phonenumber, string license, string Expedition_State, string state, string ExpirationDate = "")
        {
            ID_Driver = id;
            this.ID_Samsara = ID_Samsara;
            Name = fullName;
            PhoneNumber = phonenumber;
            ID_StateOfExpedition = Expedition_State;
            State = state;
            License = license;
            if (ExpirationDate != "")
                this.ExpirationDate = Convert.ToDateTime(ExpirationDate);
        }

        public Driver(Guid id, string ID_Samsara, string name, string lastname1, string lastanme2, string phonenumber, string license, string Expedition_State, string state, string ExpirationDate = "")
        {
            ID_Driver = id;
            this.ID_Samsara = ID_Samsara;
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

        public Driver(string ID_Samsara, string name, string phonenNumber, string licenseNumber, string licenseState)
        {
            this.ID_Samsara = ID_Samsara;
            this.Name = name;
            PhoneNumber = phonenNumber;
            License = licenseNumber;
            State = licenseState;
        }

        public Driver(string ID_Driver, string ID_Samsara, string driverName, bool dSamsara)
        {
            this.ID_Driver = Guid.Parse(ID_Driver == Guid.Empty.ToString() ? ID_Samsara : ID_Driver);
            this.Name = driverName;
            this.dSamsara = dSamsara;
        }

    }
}
