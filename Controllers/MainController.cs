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
    public class MainController
    {
        IMainView _view;
        List<Capture> _captures;
        List<Incident> _incidents;
        public string ID_Incident;
        //Incident _selectedIncident;
        //DataTable dt_InjuredPersons = new DataTable();

        public MainController(IMainView view, List<Capture> captures, List<Incident> incidents)
        {
            _captures = captures;
            _incidents = incidents;
            _view = view;
            view.SetController(this);
        }

        //public Incident Incident
        //{
        //    get { return _selectedIncident; }
        //}

        public void LoadData()
        {
            _view.LoadIncidents(_incidents);

        }
    }
}
