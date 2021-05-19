using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Services;
using ResponseEmergencySystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Controllers
{
    public class AppConfigController
    {
        IAppConfigView _view;

        public AppConfigController(IAppConfigView view)
        {
            _view = view;
            view.SetController(this);
        }

        //public void CheckEditChanged(string ckedtName, bool ckedtValue)
        //{
        //    switch (ckedtName)
        //    {
        //        case "ckedt_Spill":
        //            _view.PnlBolVisibility = ckedtValue;
        //            break;
        //        case "ckedt_PoliceReport":
        //            _view.PnlPoliceReportVisibility = ckedtValue;
        //            break;
        //        case "ckedt_IPDriver":
        //            _view.PnlDriverInvolvedVisibility = ckedtValue;
        //            break;
        //            //case "ckedt_Injured":
        //            //    panelControl3.Visible = ckedtValue;
        //            //    pnl_AddInjuredFields.Visible = ckedtValue;
        //            //    //gc_InjuredPersons.Enabled = ckedtValue;

        //            //    //if (_controller.dt_InjuredPersons.Rows.Count == 0)
        //            //    //    _controller.addEmptyRow();

        //            //    break;
        //    }
        //}

        public void AddCategory()
        {
            var response = MailDirectoryService.Add_Category(_view.NewCategory);
            _view.CategoriesDataSource = MailDirectoryService.GetCategories();
            _view.Categories2DataSource = MailDirectoryService.GetCategories();
            Utils.ShowMessage(response.Message, "Mail Category");
        }

        public void AddMailToDirectory()
        {
            var response = MailDirectoryService.AddMailToDirectory(_view.Mail, _view.Category);
            _view.MailDirectoryDataSource = MailDirectoryService.GetMailDirectory();
            _view.Mail = "";
            _view.Category = null;
            Utils.ShowMessage(response.Message, "Mail Response");
        }

        public void LoadCategories()
        {
            var categories = MailDirectoryService.GetCategories();
            _view.CategoriesDataSource = categories;
            _view.Categories2DataSource = categories;
        }

        public void LoadMailDirectory()
        {
            var directory = MailDirectoryService.GetMailDirectory();
            if (directory.Count > 0)
            {
                _view.MailDirectoryDataSource = directory;
            }
        }
    }
}
