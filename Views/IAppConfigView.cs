using DevExpress.XtraEditors.Controls;
using ResponseEmergencySystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Views
{
    public interface IAppConfigView
    {
        void SetController(AppConfigController controller);

        string NewCategory { get; }
        string Category { get; set; }
        string Mail { get; set; }
        //string State { get; set; }
        //string City { get; set; }
        //string Address { get; set; }
        //bool Private { get; set; }

        //string StateName { get; }
        //string CityName { get; }
        #region validation
        bool CategoryWarningIcon { set; }
        BorderStyles EdtCategoryBorder { set; }
        bool LueMailCategoryWarningIcon { set; }
        BorderStyles LueMailCategoryBorder { set; }
        bool EdtMailWarningIcon { set; }
        BorderStyles EdtMailBorder { set; }
        #endregion

        #region DataSources
        object MailDirectoryDataSource { set; }
        object CategoriesDataSource { set; }
        object Categories2DataSource { set; }
        #endregion
    }
}
