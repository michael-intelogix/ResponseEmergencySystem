using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Views.Incidents.Containers
{
    public interface ITrucksTrailersView
    {
        void SetController(Controllers.Incidents.DriverIncidentController controller);

        bool ValidateTransport();

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
        string TruckBroker { get; set; }
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
        string TrailerBroker { get; set; }

        // cargo status
        bool TrailerCargoSpill { get; set; }
        string TrailerBOL { get; set; }
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

        #region inputs properties
        // 238, 24
        Size LueTrucksSize { set; }
        bool BtnEditTruckVisibility { set; }
        bool BtnAddTruckVisibility { set; }
        // broker 1
        bool BtnBroker1Visibility { set; }


        Size LueTrailerSize { set; }
        bool BtnEditTrailerVisibility { set; }
        bool BtnAddTrailerVisibility { set; }
        // broker 2
        bool BtnBroker2Visibility { set; }
        #endregion
    }
}
