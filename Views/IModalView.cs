using ResponseEmergencySystem.Controllers;
using ResponseEmergencySystem.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Views
{
    public interface IModalView
    {
        void SetController(ModalController controller);

        void SetErrorIcon();
        void SetApprovedIcon();
        void SetWarningIcon();
        void SetMailSentIcon();

        string Category { get; }

        #region
        //object MailDirectoryDataSource { set; }
        #endregion
    }
}
