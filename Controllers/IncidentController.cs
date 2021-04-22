using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResponseEmergencySystem.Models;

namespace ResponseEmergencySystem.Controllers
{
    public class IncidentController
    {
        IIncidentsView _view;
        Incident _incident;

        public IncidentController(IIncidentsView view)
        {
            _view = view;
            view.SetController(this);
        }
    }
}
