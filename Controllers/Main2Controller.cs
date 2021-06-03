using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Services;
using ResponseEmergencySystem.Views;
using System;
using System.Collections.Generic;
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

        public Main2Controller(IMain2View view)
        {
            _view = view;
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
    }
}
