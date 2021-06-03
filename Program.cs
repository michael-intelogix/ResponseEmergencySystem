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
            //constants.states = GeneralService.list_States();

            //Main3 mainView = new Main3();
            //List<Incident> incidents = IncidentService.list_Incidents("", "", "", "", "");
            //var incidents = new Task<List<Incident>>(() => IncidentService.list_Incidents("", "", "", "", ""));
            //incidents.Wait();
            //var captures = new Task<List<Capture>>(() => CaptureService.list_Captures(incidents.Result[0].ID_Incident.ToString()));
            //captures.Wait();
            //List<Capture> captureByIncident = CaptureService.list_Captures(incidents[0].ID_Incident.ToString());
            //mainCtrl.LoadData();
            //new MainController(mainView);


            ////Forms.Modals.Testing test = new Forms.Modals.Testing();

            frm_Main frmMainView = new frm_Main();
            Main2Controller frmMainCtrl = new Main2Controller(frmMainView);
            frmMainCtrl.ShowSpinner();
            Application.Run(frmMainView);


            //Application.Run(new miwntras());
        }


    }
}
