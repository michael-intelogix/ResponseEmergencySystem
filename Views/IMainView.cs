using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using Google.Cloud.Firestore;
using ResponseEmergencySystem.Controllers;
using ResponseEmergencySystem.Models;

namespace ResponseEmergencySystem.Views
{
    public interface IMainView
    {
        void SetController(MainController controller);
        void Refresh_Chat(DocumentSnapshot docsnap);
        //void LoadStates(DataTable dt_States);
        //void LoadCities(DataTable dt_Cities);
        //void LoadInjuredPersons(DataTable dt_InjuredPersons);

        string ChatText { get; set; }

        string Message { get; set; }
        
        string ID_Incident { get; }
        string ID_Capture { get; }
        string ID_Image { get; }

        string ImageName { get; }

        //dateEdit1.DateTime.Date.ToString("MM/dd/yyyy")
        string Date1 { get; set;  }
        //dateEdit2.DateTime.Date.ToString("MM/dd/yyyy")
        string Date2 { get; set;  }
        string Folio { get; set;  }
        string DriverName { get; set;  }
        string TruckNumber { get; set; }

        object Incidents { set; }

        object CapturesDataSource { set; }
        object ImagesDatasSource { set; }

        MemoEdit chat { get; }

        
    }
}
