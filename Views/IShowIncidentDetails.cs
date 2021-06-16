﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResponseEmergencySystem.Controllers;
using ResponseEmergencySystem.Models;

namespace ResponseEmergencySystem.Views
{
    public interface IShowIncidentDetails
    {
        void SetController(IncidentController controller);
        void LoadIncident();
        void LoadStates(DataTable dt_States);
        void LoadCities(DataTable dt_Cities);
        void LoadInjuredPersons(DataTable dt_InjuredPersons);

        List<Models.Documents.DocumentCapture> Documents { get; set; }

        bool ShowMailButton { set; }

        bool SendToAllRecipientsInTheCategory { get; }

        string FullName { get; set; }
        string PhoneNumber { get; set; }
        string License { get; set; }
        DateTime ExpirationDate { get; set; }
        string LicenseState { get; set; }
        string TruckNumber { get; set; }
        bool TruckDamages { get; set; }
        bool TruckCanMove { get; set; }
        bool TruckNeedCrane { get; set; }
        string TrailerNumber { get; set; }
        bool TrailerDamages { get; set; }
        bool TrailerCanMove { get; set; }
        bool TrailerNeedCrane { get; set; }
        string CargoType { get; set; }
        bool CargoSpill { get; set; }
        string ManifestNumber { get; set; }
        string Broker { get; set; }
        string Broker2 { get; set; }
        string IncidentDate { get; set; }
        string IncidentTime { get; set; }
        bool PoliceReport { get; set; }
        string CitationReportNumber { get; set; }
        string Latitude { get; set; }
        string Longitude { get; set; }
        string ID_State { get; set; }
        string ID_City { get; set; }
        //this value can be highway street and other kind of references like that 
        string LocationReferences { get; set; }

        string Comments { set; }

        string MailDirectoryCategory { get; }
        string SelectedMail { get; }

        object LueCitiesDataSource { set; }
        object InvolvedPersonsDataSorurce { set; }
        object MailDirectoryDataSource { set; }
        object MailDirectoryCategoriesDataSource { set; }

        #region References (not in use)
        //Incident.SexOfPerson Sex { get; set; }
        //bool CanModifyID { set; }
        #endregion
    }

}
