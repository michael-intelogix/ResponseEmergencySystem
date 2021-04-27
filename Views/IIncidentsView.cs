using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Controllers
{
    public interface IIncidentsView
    {
        void SetController(IncidentController controller);

        DataRow GetDriverData();

        string FirstName { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
        string License { get; set; }
    }
}
