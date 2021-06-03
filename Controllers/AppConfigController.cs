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
    public class AppConfigController
    {
        IAppConfigView _view;
        private List<Models.MailDirectory> _mailDirectory;

        public AppConfigController(IAppConfigView view)
        {
            _view = view;
            view.SetController(this);
        }

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
            _mailDirectory = MailDirectoryService.GetMailDirectory();
            _view.MailDirectoryDataSource = _mailDirectory;
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
