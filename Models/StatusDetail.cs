using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models
{
    public class StatusDetail
    {
        public string ID_StatusDetail { get; set; }
        public string Description { get; set; }

        public StatusDetail(string id, string des)
        {
            ID_StatusDetail = id;
            Description = des;
        }
    }
}
