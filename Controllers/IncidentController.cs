﻿using System;
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

            _view.FullName = _selectedIncident.Name;
            _view.PhoneNumber = _selectedIncident.PhoneNumber;
            _view.License = _selectedIncident.driver.License;
            _view.ExpirationDate = _selectedIncident.driver.ExpirationDate.ToString();
            _view.LicenseState = _selectedIncident.driver.ID_StateOfExpedition;
            _view.TruckNumber = _selectedIncident.truck.truckNumber;
            _view.TrailerNumber = _selectedIncident.trailer.TrailerNumber;
            _view.TruckDamages = _selectedIncident.TruckDamage;
            _view.TruckCanMove = _selectedIncident.TruckCanMove;
            _view.TruckNeedCrane = _selectedIncident.TruckNeedCrane;
            _view.TrailerDamages = _selectedIncident.TrailerDamage;
            _view.TrailerCanMove = _selectedIncident.TrailerCanMove;
            _view.TrailerNeedCrane = _selectedIncident.TrailerNeedCrane;
            _view.CargoSpill = _selectedIncident.trailer.CargoSpill;
            _view.CargoType = _selectedIncident.trailer.Commodity;

            #region Accident Details
            _view.IncidentDate = _selectedIncident.IncidentDate.ToString("MM/dd/yyyy");
            _view.IncidentTime = _selectedIncident.IncidentDate.ToString("hh:mm:ss tt");
            _view.PoliceReport = _selectedIncident.PoliceReport;
            _view.CitationReportNumber = _selectedIncident.CitationReportNumber;
            _view.Latitude = _selectedIncident.IncidentLatitude;
            _view.Longitude = _selectedIncident.IncidentLongitude;
            _view.LocationReferences = _selectedIncident.LocationReferences;
            #endregion

            _view.LoadStates(Functions.getStates());
            if (dt_InjuredPersons.Rows.Count > 0)
                _view.LoadInjuredPersons(dt_InjuredPersons);

        }

    }
}
