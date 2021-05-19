using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models
{
    public class MailDirectory
    {
        public string ID_Mail { get; }
        public string Mail { get; set; }
        public string ID_Category { get; set; }

        public MailDirectory(string ID, string mail, string category)
        {
            ID_Mail = ID;
            Mail = mail;
            ID_Category = category;
        }
    }
}
