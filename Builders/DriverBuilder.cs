using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Builders
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

    }

    class DriverBuilder
    {
    }
}
