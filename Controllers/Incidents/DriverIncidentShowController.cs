using ResponseEmergencySystem.Views.Incidents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Controllers.Incidents
{
    class DriverIncidentShowController : DriverIncidentController
    {
        DriverIncidentShowController(IIncidentView view, string incidentId, string folio) : base(view, incidentId, folio)
        {
             
        }
    }
}
