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
        void LoadBrokers(List<Broker> brokers);
        void LoadStates(List<State> states);
        void LoadCities(List<City> cities);

        string Broker { get; set; }
        string State { get; set; }
        string City { get; set; }
        string Address { get; set; }
        bool Private { get; set; }

    }
}
