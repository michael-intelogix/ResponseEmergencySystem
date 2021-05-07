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
using System.Drawing;

namespace ResponseEmergencySystem.Controllers.Captures
{
    public class AddCapturesController
    {
        IAddCapturesView _view;
        public List<Capture> _captures;
        private Capture _selectedCaptureType;
        private string ID_Incident;
        private List<ImageCapture> _images;
        private bool img = false;

        public AddCapturesController(IAddCapturesView view, List<Capture> captures)
        {
            _view = view;
            _captures = captures;
            _images = new List<ImageCapture>();
            view.SetController(this);
        }

        public void SetIncidentId(string id)
        {
            ID_Incident = id;
        }

        public void LoadCaptures()
        {
            _view.LoadCapturesTypes(_captures);
        }

        public void UploadImage(string imgName, int idx)
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
                            if (idx > _images.Count())
                                _images.Add(new ImageCapture(imgName, ofd.FileName, idx));
                            else
                                _images[idx].ImagePath = ofd.FileName;
                            _view.LueTypeBlock = true;
                            _view.SaveButtonEnable = true;
                            img = true;
                            //MessageBox.Show(ofd.FileName);
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
                _view.SaveButtonEnable = false;

                //Thread.Sleep(5000);

                for (var i = 0; i < _images.Count(); i++)
                {
                    var id = _images[i].ID;
                    var task = UploadImgFirebaseAsync(_images[i].ImagePath, _images[i].ImageName);
                    _view.BtnArray[id].Visible = false;
                    _view.PbrArray[id].Visible = true;

                    // Track progress of the upload
                    task.Progress.ProgressChanged += (s, ev) =>
                    {
                        _view.PbrArray[id].EditValue = ev.Percentage;
                        _view.PbrArray[id].CreateGraphics().DrawString(ev.Percentage.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(_view.PbrArray[i].Width / 2 - 10, _view.PbrArray[i].Height / 2 - 7));
                        Console.WriteLine($"Progress: {ev.Percentage} %");
                    };

                    _images[i].ImageFireBaseUrl = await task;

                    var urlTest = _images[i].ImageFireBaseUrl.Split(new string[] { "%2F" }, StringSplitOptions.None)[2].Split('=')[2];
                    //var urlTest2 = urlTest[2].Split('=');
                    //var token = urlTest2[2];

                    CaptureService.AddCapture(_selectedCaptureType.ID_CaptureType, ID_Incident, "test service", "");

                    _view.PbrArray[id].Visible = false;
                    _view.BtnArray[id].Text = "Uploaded";
                    _view.BtnArray[id].Visible = true;
                }
                Reset();
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

        private FirebaseStorageTask UploadImgFirebaseAsync(string filepath, string name)
        {
            try
            {
                // Get any Stream — it can be FileStream, MemoryStream or any other type of Stream
                var stream = File.Open(filepath, FileMode.Open);

                // Construct FirebaseStorage with path to where you want to upload the file and put it there
                var task = new FirebaseStorage("dcmanagement-3d402.appspot.com")
                .Child("SIREM")
                .Child("DD0C17C7-2D9C-4A84-8851-5647A8373669")
                .Child(name)
                .PutAsync(stream);

                

                // Await the task to wait until upload is completed and get the download url
                return task;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }  
        }

        private void Reset()
        {
            for (var i = 0; i < _images.Count(); i++)
            {
                _view.BtnArray[_images[i].ID].Visible = false;
            }
            _images.Clear();
            _view.LueTypeBlock = false;
            //_view.SaveButtonEnable = true;
        }
    }


}
