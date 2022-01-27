using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models
{
    public class MailCategory
    {
        public string ID_Category { get; set; }
        public string Category { get; set; }

        public MailCategory(string id, string des)
        {
            ID_Category = id;
            Category = des;
        }
    }
}
