using DevExpress.XtraEditors.Controls;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Models;
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
    public class AppConfigController
    {
        IAppConfigView _view;
        private List<Models.MailDirectory> _mailDirectory;

        bool _validation = true;

        public AppConfigController(IAppConfigView view)
        {
            _view = view;
            view.SetController(this);
        }

        public void AddCategory()
        {
            Response response;

            if (_validation)
            {
                response = MailDirectoryService.Add_Category(_view.NewCategory);
                _view.CategoriesDataSource = MailDirectoryService.GetCategories();
                _view.Categories2DataSource = MailDirectoryService.GetCategories();
                Utils.ShowMessage(response.Message, "Mail Category");
            }
            
        }

        public void validate(string field = "category", bool show = false)
        {
            switch(field)
            {
                case "category":
                    if (_view.NewCategory.Length < 2)
                    {
                        _view.EdtCategoryBorder = BorderStyles.Simple;
                        _view.CategoryWarningIcon = true;
                        if (show)
                            Utils.ShowMessage("The category can't be empty, please fill it and try again", "Mail Directory Error", type: "Warning");
                        _validation = false;
                    }
                    else
                    {
                        _view.EdtCategoryBorder = BorderStyles.Default;
                        _view.CategoryWarningIcon = false;
                        _validation = true;
                    }
                    break;
                case "mailCategory":
                    if (_view.Category.Length < 2)
                    {
                        _view.LueMailCategoryBorder = BorderStyles.Simple;
                        _view.LueMailCategoryWarningIcon = true;
                        if (show)
                            Utils.ShowMessage("The category can't be empty, please fill it and try again", "Mail Directory Error", type: "Warning");
                        _validation = false;
                    }
                    else
                    {
                        _view.LueMailCategoryBorder = BorderStyles.Default;
                        _view.LueMailCategoryWarningIcon = false;
                        _validation = true;
                    }
                    break;
                case "mail":
                    if (_view.Mail.Length < 2)
                    {
                        _view.EdtMailBorder = BorderStyles.Simple;
                        _view.EdtMailWarningIcon = true;
                        if (show)
                            Utils.ShowMessage("The Mail can't be empty, please fill it and try again", "Mail Directory Error", type: "Warning");
                        _validation = false;
                    }
                    else
                    {
                        _view.EdtMailBorder = BorderStyles.Default;
                        _view.EdtMailWarningIcon = false;
                        _validation = true;
                    }
                    break;

            }
        }

        public void AddMailToDirectory()
        {
            if (_validation)
            {
                var response = MailDirectoryService.AddMailToDirectory(_view.Mail, _view.Category);
                _mailDirectory = MailDirectoryService.GetMailDirectory();
                _view.MailDirectoryDataSource = _mailDirectory;
                _view.Mail = "";
                _view.Category = null;
                if (response.validation)
                    Utils.ShowMessage(response.Message, "Mail Response");
                else
                    Utils.ShowMessage(response.Message, "Mail Response", type: "error");
            }

        }

        public void LoadCategories()
        {
            var categories = MailDirectoryService.GetCategories();
            _view.CategoriesDataSource = categories;
            _view.Categories2DataSource = categories;
        }

        public void LoadMailDirectory()
        {
            _mailDirectory = MailDirectoryService.GetMailDirectory();
            if (_mailDirectory.Count > 0)
            {
                _view.MailDirectoryDataSource = _mailDirectory;
            }
        }

        public void DeleteMailFromDirectory(Int32 index)
        {
            if (Utils.ShowConfirmationMessage("Are you sure you want delete this mail?", title: "Delete Mail", type: "Warning"))
            {
                var response = MailDirectoryService.DeleteMailInDirectory(_mailDirectory[index].ID_Mail);
                _mailDirectory.RemoveAt(index);
                _view.MailDirectoryDataSource = _mailDirectory;

                Utils.ShowMessage(response.Message, "Mail Response");
            }
            
        }
    }
}
