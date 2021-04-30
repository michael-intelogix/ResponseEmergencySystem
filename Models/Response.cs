using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Models
{
    public class Response
    {
        public bool validation { get; }
        public string Message { get; }
        public string ID { get; }

        public Response (bool v, string m, string id)
        {
            validation = v;
            Message = m;
            ID = id;
        }

        public Response ()
        {
            validation = false;
            Message = "";
            ID = "00000000-0000-0000-0000-000000000000";
        }
    }
}
