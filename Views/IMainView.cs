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
        void OpenSpinner();
        void CloseSpinner();

        void SetGridFilters(string driver, string truck, string folio);
        void ClearFilters();

        void LblFolioPosition();
        void TEST();
        //void LoadCities(DataTable dt_Cities);
        //void LoadInjuredPersons(DataTable dt_InjuredPersons);

        string ChatText { get; set; }

        string Message { get; set; }
        
        object ID_Incident { get; }
        object ID_Capture { get; }
        string ID_Image { get; }
        object ID_StatusDetail { get; set; }
        object DocumentType { get; }

        string ImageName { get; }

        //dateEdit1.DateTime.Date.ToString("MM/dd/yyyy")
        string Date1 { get; set;  }
        //dateEdit2.DateTime.Date.ToString("MM/dd/yyyy")
        string Date2 { get; set;  }
        object Folio { get; set;  }
        string DriverName { get; set;  }
        object TruckNumber { get; set; }

        object Incidents { set; get; }

        object CapturesDataSource { set; }
        object ImagesDatasSource { set; }
        object StatusDetailDataSource { set; }

        MemoEdit chat { get; }

        string LblFolio { set; }
        bool LblFolioVisibility { set; }
    }
}
