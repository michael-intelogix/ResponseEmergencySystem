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
    public class EditImageDataController
    {
        IEditImageData _view;
        string _ImageID;

        public EditImageDataController(IEditImageData view, string ID_Image)
        {
            _view = view;
            _ImageID = ID_Image;
            view.SetController(this);
        }

        public void LoadStatusDetail()
        {
            _view.StatusDetailDataSource = StatusDetailService.list_StatusDetail();
        }

        public void UpdateData()
        {
            if (_view.StatusDetail != null)
            {
                CaptureService.UpdateImageData(_ImageID, _view.StatusDetail.ToString(), _view.Comments);
                _view.CloseForm();
            }
            else
            {
                Utils.ShowMessage("Please select an status detail", "Image information", type: "Warning");
            }

        }

        
    }
}
