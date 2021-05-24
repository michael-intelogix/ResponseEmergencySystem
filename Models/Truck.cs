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

        public Truck (Guid ID_Truck, string number)
        {
            this.ID_Truck = ID_Truck;
            truckNumber = number;
        }

        public Truck(string ID_Truck, string number)
        {
            this.ID = ID_Truck;
            truckNumber = number;
        }
    }
}
