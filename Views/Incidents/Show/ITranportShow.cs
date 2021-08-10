using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Views.Incidents.Show
{
    public interface ITranportShow
    {
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

