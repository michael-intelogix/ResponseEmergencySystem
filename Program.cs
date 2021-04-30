using ResponseEmergencySystem.Controllers;
using ResponseEmergencySystem.Forms;
using ResponseEmergencySystem.Services;
using ResponseEmergencySystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ResponseEmergencySystem.Code;

namespace ResponseEmergencySystem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Main2());

            constants.states = GeneralService.list_States();

            Main2 mainView = new Main2();
            List<Incident> incidents = IncidentService.list_Incidents("", "", "", "", "");
            List<Capture> captureByIncident = CaptureService.list_Captures(incidents[0].ID_Incident.ToString());
            MainController mainCtrl = new MainController(mainView, captureByIncident, incidents);

            mainCtrl.LoadData();

           
            Application.Run(mainView);
        }
    }
}
