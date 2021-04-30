using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using ResponseEmergencySystem.Controllers;
using ResponseEmergencySystem.Models;

namespace ResponseEmergencySystem.Views
{
    public interface IMainView
    {
        void SetController(MainController controller);
        void LoadIncidents(List<Incident> incidents);
        void LoadCaptures(List<Capture> capture);
        void Refresh_Chat(DocumentSnapshot docsnap);
        //void LoadStates(DataTable dt_States);
        //void LoadCities(DataTable dt_Cities);
        //void LoadInjuredPersons(DataTable dt_InjuredPersons);

        string Message { get; set; }
        
        string ID_Incident { get; }
    }
}
