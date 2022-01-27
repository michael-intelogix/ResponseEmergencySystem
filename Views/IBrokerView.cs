using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResponseEmergencySystem.Controllers;
using ResponseEmergencySystem.Models;

namespace ResponseEmergencySystem.Views
{
    public interface IBrokerView
    {
        void SetController(BrokerController controller);

        string Broker { get; set; }
        string State { get; set; }
        string City { get; set; }
        string Address { get; set; }
        string PhoneNumber { get; set; }
        bool Private { get; set; }

        string StateName { get; }
        string CityName { get; }

        #region
        object CitiesDataSource { set; }
        object StatesDataSource { set; }
        object BrokersDataSource { set; }
        #endregion

    }
}
