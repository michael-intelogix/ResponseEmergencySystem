using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResponseEmergencySystem.Views;
using ResponseEmergencySystem.Models;
using ResponseEmergencySystem.Services;
using ResponseEmergencySystem.Code;

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

        public void SaveBroker()
        {
            BrokerService.update_Broker(_view.State, _view.City, _view.Broker, _view.Address);
        }

        public void LoadBrokers()
        {
            _view.LoadBrokers(_brokers);
            _view.LoadStates(constants.states);
            //_view.LoadCaptures(_captures);
        }

        public List<City> GetCities(string ID_State)
        {
            return GeneralService.list_Cities(ID_State);
        }
        public List<Broker> Save()
        {
            BrokerService.update_Broker(_view.State, _view.City, _view.Broker, _view.Address);
            if (BrokerService.response.validation)
            {
                var state = constants.states.Where(s => s.ID_State == _view.State).Select(s => new { s.ID_State, s.Name }).FirstOrDefault();
                string cityName = GeneralService.list_Cities(state.ID_State).Where(c => c.ID_City == _view.City).FirstOrDefault().Name;
                _brokers.Add(new Broker("", _view.State, _view.City, state.Name, cityName, _view.Broker, _view.Address));
                return _brokers;
            }
            return _brokers;
        }

        public Broker GetBrokerByIndex(Int32 idx)
        {
            return _brokers[idx];
        }
    }
}
