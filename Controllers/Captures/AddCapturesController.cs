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
using System.Threading;
using Firebase.Storage;

namespace ResponseEmergencySystem.Controllers.Captures
{
    public class AddCapturesController
    {
        IAddCapturesView _view;
        public List<Capture> _captures;
        private Capture _selectedCaptureType;
        private List<ImageCapture> _images;
        private bool img = false;

        public AddCapturesController(IAddCapturesView view, List<Capture> captures)
        {
            _view = view;
            _captures = captures;
            _images = new List<ImageCapture>();
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
                            var test = _captures.Where(c => c.captureType == _selectedCaptureType.captureType).First();
                            _images.Add(new ImageCapture(imgName, ofd.FileName));
                            _view.LueTypeBlock = true;
                            img = true;
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

        public async void SaveAsync()
        {
            if (img)
            {
                //Thread.Sleep(5000);
                
                var test = await UploadImgFirebaseAsync(_images[0].ImagePath);
                MessageBox.Show(test);
                _view.LueTypeBlock = false;
            }
        }

        public void SetType(string captureTypeId)
        {
            _selectedCaptureType = _captures.Where(c => c.ID_CaptureType == captureTypeId).First();

            switch(_selectedCaptureType.captureType)
            {
                case "Insurance Policy":
                    _view.LblCapture1Name = _selectedCaptureType.imagesListOfNames[0].name;
                    _view.PnlCapture1Visbility = true;
                    _view.PnlCapture2Visbility = false;
                    _view.PnlCapture3Visbility = false;
                    _view.PnlCapture4Visbility = false;
                    break;
                case "Testimony":
                    _view.LblCapture1Name = _selectedCaptureType.imagesListOfNames[0].name;
                    _view.PnlCapture1Visbility = true;
                    _view.PnlCapture2Visbility = false;
                    _view.PnlCapture3Visbility = false;
                    _view.PnlCapture4Visbility = false;
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

        private FirebaseStorageTask UploadImgFirebaseAsync(string filepath)
        {
            try
            {
                // Get any Stream — it can be FileStream, MemoryStream or any other type of Stream
                var stream = File.Open(filepath, FileMode.Open);

                // Construct FirebaseStorage with path to where you want to upload the file and put it there
                var task = new FirebaseStorage("dcmanagement-3d402.appspot.com")
                .Child("SIREM")
                .Child("DD0C17C7-2D9C-4A84-8851-5647A8373669")
                .Child("test")
                .PutAsync(stream);

                // Track progress of the upload
                task.Progress.ProgressChanged += (s, ev) =>
                {
                    //progressBarControl1.EditValue = ev.Percentage;
                    //progressBarControl1.CreateGraphics().DrawString(ev.Percentage.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(progressBarControl1.Width / 2 - 10, progressBarControl1.Height / 2 - 7));
                    Console.WriteLine($"Progress: {ev.Percentage} %");
                };

                // Await the task to wait until upload is completed and get the download url
                return task;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }  
        }
    }


}
