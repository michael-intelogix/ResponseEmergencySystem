using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResponseEmergencySystem.Views;
using ResponseEmergencySystem.Models;
using ResponseEmergencySystem.Services;

namespace ResponseEmergencySystem.Controllers
{
    public class BrokerController
    {
        IBrokerView _view;
        public List<Broker> _brokers;
        List<Incident> _incidents;
        public string ID_Incident;
        //Incident _selectedIncident;
        //DataTable dt_InjuredPersons = new DataTable();

        public BrokerController(IBrokerView view, List<Broker> brokers)
        {
            _brokers = brokers;
            _view = view;
            view.SetController(this);
        }

        //public Incident Incident
        //{
        //    get { return _selectedIncident; }
        //}

        public void LoadBrokers()
        {
            _view.LoadBrokers(_brokers);
            //_view.LoadCaptures(_captures);
        }
    }
}
