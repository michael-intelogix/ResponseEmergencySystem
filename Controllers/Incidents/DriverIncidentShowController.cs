using ResponseEmergencySystem.Views.Incidents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Controllers.Incidents
{
    public class DriverIncidentShowController : DriverIncidentController
    {
        IIncidentView _view;
        public DriverIncidentShowController(IIncidentView view, string incidentId, string folio) : base(view, incidentId, folio)
        {
            _view = view;
        }

        public void TransportShown()
        {
            //trucks
            _truckTrailerView.LueTrucksSize = new System.Drawing.Size(238, 24);
            _truckTrailerView.BtnEditTruckVisibility = false;
            _truckTrailerView.BtnAddTruckVisibility = false;
            _truckTrailerView.BtnBroker1Visibility = false;
            //trailers
            _truckTrailerView.LueTrailerSize = new System.Drawing.Size(238, 24);
            _truckTrailerView.BtnEditTrailerVisibility = false;
            _truckTrailerView.BtnAddTrailerVisibility = false;
            _truckTrailerView.BtnBroker2Visibility = false;

            _view.enableReadOnly();
        }

    }
}
