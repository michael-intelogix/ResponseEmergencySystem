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
            BrokerService.update_Broker(_view.State, _view.City, _view.Broker, _view.Address, _view.PhoneNumber,  _view.Private);
        }

        public void LoadBrokers()
        {
            var states = GeneralService.list_States();
            _view.StatesDataSource = states;
            _view.BrokersDataSource = _brokers;
            //_view.LoadCaptures(_captures);
        }

        public void GetCities(string ID_State)
        {
            _view.CitiesDataSource = GeneralService.list_Cities(ID_State);
        }
        public void Save()
        {
            BrokerService.update_Broker(_view.State, _view.City, _view.Broker, _view.Address, _view.PhoneNumber, _view.Private);
            if (BrokerService.response.validation)
            {
                _brokers.Add(new Broker("", _view.State, _view.City, _view.StateName, _view.CityName, _view.Broker, _view.Address));
                Utils.ShowMessage("The towing company was added succesfully", "Towing company listing");
                _view.BrokersDataSource = _brokers;
            } 
            else
            {
                Utils.ShowMessage(BrokerService.response.Message, "Towing company listing", type: "Error");
                _view.BrokersDataSource = _brokers;
            } 
            
        }

        public Broker GetBrokerByIndex(Int32 idx)
        {
            return _brokers[idx];
        }
    }
}
