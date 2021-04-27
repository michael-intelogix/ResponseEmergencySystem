using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models
{
    public class InjuredPerson
    {
        public Guid ID_InjuredPerson { get; }
        public string FullName { get; set; }
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public string PhoneNumber { get; set; }

        public InjuredPerson(Guid id, string fullname, string lastname1, string lastname2, string phone)
        {
            ID_InjuredPerson = id;
            FullName = fullname;
            LastName1 = lastname1;
            LastName2 = lastname2;
            PhoneNumber = phone;
        }
    }
}
