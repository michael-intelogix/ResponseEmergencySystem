using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Views.Incidents
{
    public interface IIncidentView
    {
        void SetController(Controllers.Incidents.DriverIncidentController controller);

        void LoadIncident();
        void LoadStates(DataTable dt_States);
        void LoadInjuredPersons(DataTable dt_InjuredPersons);

        List<Models.Documents.DocumentCapture> Documents { get; set; }

        #region basic vehicle incident
        string Folio { set; }
        string FullName { get; set; }
        string PhoneNumber { get; set; }
        string License { get; set; }
        DateTime ExpirationDate { get; set; }
        string LicenseState { get; set; }
        object TruckNumber { get; set; }
        string TruckId { get; set; }
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
        DateTime IncidentDate { get; set; }
        bool PoliceReport { get; set; }
        string CitationReportNumber { get; set; }
        string Latitude { get; set; }
        string Longitude { get; set; }
        string ID_State { get; set; }
        string ID_City { get; set; }
        //this value can be highway street and other kind of references like that 
        string LocationReferences { get; set; }
        object Comments { get; set; }
        #endregion

        #region Involved Persons
        string IPFullName { get; set; }
        string IPLastName1 { get; set; }
        string IPPhoneNumber { get; set; }
        string IPAge { get; set; }
        bool IPPrivate { get; set; }
        bool IPInjured { get; set; }
        bool IPPassenger { get; set; }
        bool IPDriver { get; set; }
        string IPDriverLicense { get; set; }
        string IPHospital { get; set; }
        string IPComments { get; set; }
        #endregion

        object LueCitiesDataSource { set; }
        //object LueStatusDetailDataSource { set; }

        object InvolvedPersonsDataSource { set; }
        object DriversDataSource { set; }
        object TrucksDataSource { set; }
        //bool PnlDriverInvolvedVisibility { set; }

        bool BtnAddInvolvedPersonVisibility { set; }
        Point BtnAddInvolvedPersonLocation { set; }
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
        BorderStyles CkedtPassengerBorder { get; set; }
        BorderStyles CkedtDriverBorder { get; set; }

        bool EdtFullNameShowWarningIcon { get; set; }
        bool EdtLastName1ShowWarningIcon { get; set; }
        bool EdtPhoneNumberShowWarningIcon { set; }
        bool EdtAgeShowWarningIcon { set; }
        bool EdtLicenseShowWarningIcon { set; }

        #region Mailing
        bool SendToAllRecipientsInTheCategory { get; }
        string MailDirectoryCategory { get; }
        string SelectedMail { get; }
        object MailDirectoryDataSource { set; }
        object MailDirectoryCategoriesDataSource { set; }
        bool SendAfterSave { get; }
        #endregion

    }
}
