using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResponseEmergencySystem.Models;
using ResponseEmergencySystem.Views;
using ResponseEmergencySystem.Services;
using ResponseEmergencySystem.Samsara_Models;
using System.Data;
using ResponseEmergencySystem.Code;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using ResponseEmergencySystem.Forms.Modals;

namespace ResponseEmergencySystem.Controllers
{
    public class IncidentController
    {
        IShowIncidentDetails _view;
        private string ID_Incident;
        Incident _selectedIncident;
        DataTable dt_InjuredPersons = new DataTable();

        public IncidentController(IShowIncidentDetails view, string incidentId)
        {
            ID_Incident = incidentId;
            _view = view;
            view.SetController(this);
        }

        public Incident Incident
        {
            get { return _selectedIncident; }
        }

        public void LoadIncident()
        {
            _selectedIncident = IncidentService.list_Incidents("", "", "", "", "", incidentId: ID_Incident)[0];
            dt_InjuredPersons = IncidentService.list_InjuredPerson(ID_Incident);

            _view.LoadIncident(_selectedIncident);
            _view.LoadStates(Functions.getStates());
            if (dt_InjuredPersons.Rows.Count > 0)
                _view.LoadInjuredPersons(dt_InjuredPersons);

        }

    }
}
