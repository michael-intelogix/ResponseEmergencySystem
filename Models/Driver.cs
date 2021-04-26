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
        public string License { get; }
        public string ID_StateOfExpedition { get; }
        public DateTime? ExpirationDate { get; set; }

        public Driver(Guid id, string license, string Expedition_State, string ExpirationDate = "")
        {
            ID_Driver = id;
            ID_StateOfExpedition = Expedition_State;
            License = license;
            if (ExpirationDate != "")
                this.ExpirationDate = Convert.ToDateTime(ExpirationDate);
        }
    }
}
