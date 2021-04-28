using ResponseEmergencySystem.Controllers;
using ResponseEmergencySystem.Forms;
using ResponseEmergencySystem.Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            Main2 mainView = new Main2();
            MainController mainCtrl = new MainController(mainView, CaptureService.list_Captures(), IncidentService.list_Incidents("", "", "", "", ""));
            mainCtrl.LoadData();


            Application.Run(mainView);
        }
    }
}
