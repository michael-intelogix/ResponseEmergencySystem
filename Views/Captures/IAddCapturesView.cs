using ResponseEmergencySystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResponseEmergencySystem.Controllers.Captures;
using ResponseEmergencySystem.Models;

namespace ResponseEmergencySystem.Views.Captures
{
    public interface IAddCapturesView
    {
        void SetController(AddCapturesController controller);
        void LoadCapturesTypes(List<Capture> captures);

        bool LueTypeBlock { set; }
        bool PnlCapture1Visbility { set; }
        bool PnlCapture2Visbility { set; }
        bool PnlCapture3Visbility { set; }
        bool PnlCapture4Visbility { set; }
        string LblCapture1Name { set; }
        string LblCapture2Name { set; }
        string LblCapture3Name { set; }
        string LblCapture4Name { set; }
    }

}
