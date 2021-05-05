using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResponseEmergencySystem.Models;
using ResponseEmergencySystem.Views.Captures;
using ResponseEmergencySystem.Services;
using System.Windows.Forms;
using System.IO;

namespace ResponseEmergencySystem.Controllers.Captures
{
    public class AddCapturesController
    {
        IAddCapturesView _view;
        public List<Capture> _captures;
        private Capture _selectedCaptureType;

        public AddCapturesController(IAddCapturesView view, List<Capture> captures)
        {
            _view = view;
            _captures = captures;
            view.SetController(this);
        }

        public void LoadCaptures()
        {
            _view.LoadCapturesTypes(_captures);
        }

        public void UploadImage(string imgName)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files(*.PNG;*.JPG;*.GIF;*.BMP)|*.PNG;*.JPG;*.GIF;*.BMP|PDF Files (*.PDF)|*.PDF|All Files (*.*)|*.*";
                ofd.ShowDialog();

                string ext = Path.GetExtension(ofd.FileName).ToUpper();
                try
                {
                    if (_selectedCaptureType != null)
                    {
                        if (ext == ".GIF" || ext == ".JPG" || ext == ".PNG" || ext == ".BMP")
                        {
                            MessageBox.Show(ofd.FileName);
                        }
                    }
                    else
                    {
                        MessageBox.Show("NULL");
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
        }

        public void SetType(string captureTypeId)
        {
            _selectedCaptureType = _captures.Where(c => c.ID_CaptureType == captureTypeId).First();

            switch(_selectedCaptureType.captureType)
            {
                case "Insurance Policy":
                    _view.PnlCapture1Visbility = true;
                    _view.PnlCapture2Visbility = false;
                    _view.PnlCapture3Visbility = false;
                    _view.PnlCapture4Visbility = false;
                    _view.LblCapture1Name = _selectedCaptureType.imagesListOfNames[0].name;
                    break;
                case "Testimony":
                    _view.PnlCapture1Visbility = true;
                    _view.PnlCapture2Visbility = false;
                    _view.PnlCapture3Visbility = false;
                    _view.PnlCapture4Visbility = false;
                    _view.LblCapture1Name = _selectedCaptureType.imagesListOfNames[0].name;
                    break;
                default:
                    _view.PnlCapture1Visbility = true;
                    _view.PnlCapture2Visbility = true;
                    _view.PnlCapture3Visbility = true;
                    _view.PnlCapture4Visbility = true;
                    _view.LblCapture1Name = _selectedCaptureType.imagesListOfNames[0].name;
                    _view.LblCapture2Name = _selectedCaptureType.imagesListOfNames[1].name;
                    _view.LblCapture3Name = _selectedCaptureType.imagesListOfNames[2].name;
                    _view.LblCapture4Name = _selectedCaptureType.imagesListOfNames[3].name;
                    break;
            }
            
        }


    }


}
