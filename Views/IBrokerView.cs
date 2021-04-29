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
    }
}
