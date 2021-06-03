using ResponseEmergencySystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Views
{
    public interface IMain2View
    {
        void SetController(Main2Controller controller);
        //void Refresh_Chat(DocumentSnapshot docsnap);
        void OpenSpinner();
        void CloseSpinner();

        //void LoadStates(DataTable dt_States);
        //void LoadCities(DataTable dt_Cities);
        //void LoadInjuredPersons(DataTable dt_InjuredPersons);

        //dateEdit1.DateTime.Date.ToString("MM/dd/yyyy")
        string Date1 { get; set; }
        //dateEdit2.DateTime.Date.ToString("MM/dd/yyyy")
        string Date2 { get; set; }
        string Folio { get; set; }
        string DriverName { get; set; }
        string TruckNumber { get; set; }

        string Status { get; }

        object IncidentsDataSource { get; set; }
    }
}
