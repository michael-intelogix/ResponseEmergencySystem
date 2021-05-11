using ResponseEmergencySystem.Controllers.Incidents;
using ResponseEmergencySystem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponseEmergencySystem.Views
{
    public interface IAddIncidentView
    {
        void SetController(AddIncidentController controller);
        void LoadIncident(Incident incident);
        void LoadStates(DataTable dt_States);
        void LoadCities(DataTable dt_Cities);
        void LoadInjuredPersons(DataTable dt_InjuredPersons);

        // Controls Events Needed
        void checkNumber_OnEdtKeyPress(object sender, KeyPressEventArgs e);
        void checkNumber_OnEdtLeave(object sender, EventArgs e);
        void FindTruckSamsara_Click(object sender, EventArgs e);
        void Ckedt_OnValueChanged(object sender, EventArgs e);
        void OnStateEditValueChanged(object sender, EventArgs e);

        string DriverInfoSearch { get; }

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
        DateTime IncidentDate { get; }
        bool PoliceReport { get; set; }
        string CitationReportNumber { get; set; }
        string Latitude { get; set; }
        string Longitude { get; set; }
        string ID_State { get; set; }
        string ID_City { get; set; }
        //this value can be highway street and other kind of references like that 
        string LocationReferences { get; set; }

        string Comments { get; } 

        // form elements properties (change properties of especific elements in the form)
        bool PnlBolVisibility { set; }
        bool PnlPoliceReportVisibility { set; }
        bool LblTruckExistsVisibility { set; }
        bool LblTrailerExistsVisibility { set; }
        object LueCitiesDataSource { set; }
    }
}
