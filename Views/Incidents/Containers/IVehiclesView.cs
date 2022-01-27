using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Views.Incidents.Containers
{
    public interface IVehiclesView
    {
        void SetController(Controllers.Incidents.DriverIncidentController controller);

        string VehicleName { get; set; }
        string VinNumber { get; set; }
        string Serialnumber { get; set; }
        string Make { get; set; }
        string Model { get; set; }
        string Year { get; set; }
        string LicensePlate { get; set; }

        #region IDs
        string ID_Vehicle { get; set; }
        #endregion

        #region view properties
        object VehiclesDataSource { set; }

        #endregion
    }
}
