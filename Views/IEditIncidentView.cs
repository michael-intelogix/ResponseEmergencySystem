﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors.Controls;
using ResponseEmergencySystem.Controllers;
using ResponseEmergencySystem.Controllers.Incidents;
using ResponseEmergencySystem.Models;


namespace ResponseEmergencySystem.Views
{
    public interface IEditIncidentView
    {
        void SetController(EditIncidentController controller);
        void LoadIncident(Incident incident);
        void LoadStates(DataTable dt_States);
        void LoadInjuredPersons(DataTable dt_InjuredPersons);

        string FullName { get; set; }
        string PhoneNumber { get; set; }
        string License { get; set; }
        DateTime ExpirationDate { get; set; }

        string DriverSearch { get; }

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
        DateTime IncidentDate { get; set; }
        bool PoliceReport { get; set; }
        string CitationReportNumber { get; set; }
        string Latitude { get; set; }
        string Longitude { get; set; }
        string ID_State { get; set; }
        string ID_City { get; set; }
        //this value can be highway street and other kind of references like that 
        string LocationReferences { get; set; }
        string Comments { get; set; }

        string IPFullName { get; set; }
        string IPLastName1 { get; set; }
        string IPPhoneNumber { get; set; }
        string IPAge { get; set; }
        bool IPPrivate { get; set; }
        bool IPInjured { get; set; }
        bool IPPassenger { get; set; }
        bool IPDriver { get; set; }
        string IPLicense { get; set; }

        bool LblTruckExistsVisibility { set; }
        bool LblTrailerExistsVisibility { set; }
        
        object LueCitiesDataSource { set; }

        object InvolvedPersonsDataSorurce { set; }
        bool PnlDriverInvolvedVisibility { set; }
        string BtnAddInvolvedPersonText { set; }

        bool BtnAddInvolvedPersonVisibility { set; }
        Point BtnAddInvolvedPersonLocation { set; }
        Size BtnAddInvolvedPersonSize { set; }
        //521, 85
        //n 494,85

        //108, 23
        //n 135, 23

        bool BtnEditInvolvedPersonVisibility { set; }
        Point BtnEditInvolvedPersonLocation { set; get; }

        bool LblEmptyFieldsVisibility { set; }

        BorderStyles EdtFullNameBorder { get; set; }
        BorderStyles EdtLastNameBorder { get; set; }
        BorderStyles EdtPhoneNumberBorder { get; set; }
        BorderStyles EdtAgeBorder { get; set; }
        BorderStyles EdtLicenseBorder { get; set; }

        bool EdtFullNameShowWarningIcon { set; }
        bool EdtLastName1ShowWarningIcon { set; }
        bool EdtPhoneNumberShowWarningIcon { set; }
        bool EdtAgeShowWarningIcon { set; }
        bool EdtLicenseShowWarningIcon { set; }
    }
}
