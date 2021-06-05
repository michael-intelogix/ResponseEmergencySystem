using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Forms;
using ResponseEmergencySystem.Services;
using ResponseEmergencySystem.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponseEmergencySystem.Controllers
{
    public class Main2Controller
    {
        IMain2View _view;
        MainController _mainController;
        DataTable _access;

        public Main2Controller(IMain2View view)
        {
            _view = view;
            _access = new DataTable();
            view.SetController(this);
        }

        public void SetMainController(MainController controller)
        {
            _mainController = controller;
        }

        public void IncidentsFilter()
        {
            _mainController.IncidentsFilter(_view.Folio, _view.DriverName, _view.TruckNumber, _view.Status, date1: _view.Date1, date2: _view.Date1 == "" ? "" : _view.Date2, true);
        }

        public void IncidentsFilterGrid()
        {
            _mainController.IncidentsFilter(_view.Folio, _view.DriverName, _view.TruckNumber, _view.Status);
        }

        public void ClearFilters()
        {
            _view.Date1 = null;
            _view.Date2 = null;
            _view.Status = "423E82C9-EE3F-4D83-9066-01E6927FE14D";
            _view.Folio = "";
            _view.DriverName = "";
            _view.TruckNumber = "";
            _mainController.ClearFilters();
        }


        public void AddIncident()
        {
            _mainController.AddIncidentView();
        }

        public void ShowSpinner()
        {
            _view.OpenSpinner();
        }

        public void CloseSpinner()
        {
            _view.CloseSpinner();
        }

        public void Settings()
        {
            Forms.Modals.AppConfiguration appConfigView = new Forms.Modals.AppConfiguration();
            AppConfigController appConfigCtrl = new AppConfigController(appConfigView);
            appConfigCtrl.LoadCategories();
            appConfigCtrl.LoadMailDirectory();
            if (appConfigView.ShowDialog() == DialogResult.OK)
            {
                Utils.ShowMessage("Aplication settings has been updated");
            }
        }

        public void Login()
        {
            frm_Login login = new frm_Login();
            try
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    _access = login.myData;
                    string idmysoftware = "2a5aa42b-2089-4fa8-b7cc-2cea2a017a8a";
                    DataRow[] accesos = _access.Select($"ID_Software = '{idmysoftware}'");
                    if (accesos.Length > 0)
                    {
                        constants.userName = accesos[0].ItemArray[13].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void AddCaptures()
        {
            _mainController.AddMoreCaptures();
        }

        public void ActionsMenu(string action)
        {
            switch(action)
            {
                case "show":
                    _mainController.ShowIncident();
                    break;
                case "edit":
                    _mainController.EditIncidentView();
                    break;
                case "status":
                    _mainController.SaveStatus();
                    break;
                case "close":
                    _mainController.CloseIncident();
                    break;
                case "delete":
                    _mainController.DeleteIncident();
                    break;

            }
        }
    }
}
