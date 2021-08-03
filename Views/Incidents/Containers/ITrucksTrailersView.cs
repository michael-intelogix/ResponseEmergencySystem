using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Views.Incidents.Containers
{
    public interface ITrucksTrailersView
    {
        void SetController(Controllers.Incidents.DriverIncidentController controller);
        
        #region Truck Information
        string TruckName { get; set; }
        string TruckVinNumber { get; set; }
        string TruckSerialNumber { get; set; }
        string TruckMake { get; set; }
        string TruckModel { get; set; }
        string TruckYear { get; set; }
        string TruckLicensePlate { get; set; }

        // truck status
        bool TruckDamage { get; set; }
        bool TruckCanMove { get; set; }
        bool TruckNeedCrane { get; set; }

        //truck broker
        #endregion

        #region Trailer Information
        string TrailerName { get; set; }
        string TrailerCargoType { get; set; }
        string TrailerVinNumber { get; set; }
        string TrailerSerialNumber { get; set; }
        string TrailerMake { get; set; }
        string TrailerModel { get; set; }
        string TrailerYear { get; set; }
        string TrailerLicensePlate { get; set; }

        // trailer status
        bool TrailerDamage { get; set; }
        bool TrailerCanMove { get; set; }
        bool TrailerNeedCrane { get; set; }

        // trailer broker
        #endregion

        #region IDs
        string ID_Truck { get; set; }
        string ID_Trailer { get; set; }
        #endregion

        #region view properties
        object TrucksDataSource { set; }
        object TrailersDataSource { set; }
        bool IsNew { get; }
        #endregion
    }
}
