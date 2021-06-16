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
        string _Type;

        public EditImageDataController(IEditImageData view, string ID_Image, string documentType)
        {
            _view = view;
            _ImageID = ID_Image;
            _Type = documentType;
            view.SetController(this);
        }

        public void LoadStatusDetail()
        {
            _view.StatusDetailDataSource = StatusDetailService.list_StatusDetail();
        }

        public void UpdateData()
        {
            CaptureService.UpdateImageData(_ImageID, "", _view.Comments, _Type); ;
            _view.CloseForm();

            //if (_view.StatusDetail == null)
            //{
            //    Utils.ShowMessage("Please select an status detail", "Image information", type: "Warning");
            //}

        }

        
    }
}
