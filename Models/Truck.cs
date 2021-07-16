using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models
{
    public class Truck
    {
        public Guid ID_Truck { get; }

        [DisplayName("truckNumber")]
        public string truckNumber { get; }

        public string ID { get; }

        public string ID_Samsara { get; }
        public string VinNumber { get; }
        public string SerialNumber { get; }
        public string Make { get; }
        public string Model { get; }
        public string Year { get; }
        public string LicensePlate { get; }

        public string TowingName { get; }
        public string TowedTo { get; }

        public bool Damages { get; }

        public bool CanMove { get; }

        public bool NeedCrane { get; }

        public Truck(Guid ID_Truck, string number)
        {
            this.ID_Truck = ID_Truck;
            truckNumber = number;
        }

        public Truck (Guid ID_Truck, string ID_Samsara, string number, string vin, string towingName, string towedTo)
        {
            this.ID_Truck = ID_Truck;
            this.ID_Samsara = ID_Samsara;
            truckNumber = number;
            VinNumber = vin;
            TowingName = towingName;
            TowedTo = towedTo;
        }

        public Truck(string ID_Truck, string number)
        {
            this.ID = ID_Truck;
            truckNumber = number;
        }

        public Truck(string ID, string ID_Samsara, string tNumber, string vinNumber, string serialNumber, string make, string model, string year, string license)
        {
            this.ID = ID;
            this.ID_Samsara = ID_Samsara;
            truckNumber = tNumber;
            VinNumber = vinNumber;
            SerialNumber = serialNumber;
            Make = make;
            Model = model;
            Year = year;
            LicensePlate = license;
        }

        public Truck(string ID, string truckNumber, bool damages, bool canMove, bool needCrane)
        {
            this.ID = ID;
            this.truckNumber = truckNumber;
            this.Damages = damages;
            this.CanMove = canMove;
            this.NeedCrane = needCrane;
        }

        public Truck(Guid ID_Truck, string ID_Samsara, string number, string vin, string towingName, string towedTo, bool damages, bool canMove, bool needCrane)
        {
            this.ID_Truck = ID_Truck;
            this.ID_Samsara = ID_Samsara;
            truckNumber = number;
            VinNumber = vin;
            TowingName = towingName;
            TowedTo = towedTo;
            this.Damages = damages;
            this.CanMove = canMove;
            this.NeedCrane = needCrane;
        }
    }
}
