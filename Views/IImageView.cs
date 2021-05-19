using DevExpress.XtraEditors;
using ResponseEmergencySystem.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseEmergencySystem.Views
{
    public interface IImageView
    {
        void SetController(ImageController controller);
        void CloseForm();

        ProgressBarControl PbImage { get; }
        bool PnlUploadingVisibility { set; }
        bool BtnSaveEnable { set; }
        Color PnlUploadingBackColor { set; } 
    }
}
