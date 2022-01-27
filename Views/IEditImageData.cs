using ResponseEmergencySystem.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Views
{
    public interface IEditImageData
    {
        void SetController(EditImageDataController controller);
        void CloseForm();

        object StatusDetail { get; }
        string Comments { get; }

        object StatusDetailDataSource { set; }
    }
}
