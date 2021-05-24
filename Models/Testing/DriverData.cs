using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models.Testing
{
    public class DriverData
    {
        public string DriverName { get; set; }
        public string PatSurname { get; set; }
        public string MatSurname { get; set; }
        public string Name2 { get; set; }
        public string License { get; set; }
        public string PhoneNumber { get; set; }
        public string LicenseState { get; set; }
        public bool Repeated { get; set; }
        public bool NotFound { get; set; }
        public bool FullNameMatched { get; set; }
    }
}
