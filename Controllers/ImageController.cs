using Firebase.Storage;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponseEmergencySystem.Controllers
{
    public class ImageController
    {
        IImageView _view;
        string _CaptureID;
        string _ImageName;
        string filepath = "";

        public ImageController(IImageView view, string ID_Capture, string imageName)
        {
            _view = view;
            _CaptureID = ID_Capture;
            _ImageName = imageName;
            view.SetController(this);
        }

        public ImageController(IImageView view)
        {
            _view = view;
            view.SetController(this);
        }

        public void DisableImageLoad()
        {
            _view.DisableLoad();
        }

        public void UpdateImage()
        {

            using (OpenFileDialog ofd = new OpenFileDialog())
            {

                ofd.Filter = "Image Files(*.PNG;*.JPG;*.GIF;*.BMP)|*.PNG;*.JPG;*.GIF;*.BMP|PDF Files (*.PDF)|*.PDF|All Files (*.*)|*.*";
                ofd.ShowDialog();

                string ext = Path.GetExtension(ofd.FileName).ToUpper();
                try
                {
                    if (ext == ".GIF" || ext == ".JPG" || ext == ".PNG" || ext == ".BMP")
                    {
                        //img_Test.Visible = true;
                        filepath = ofd.FileName;

                        using (var bmpTemp = new Bitmap(filepath))
                        {
                            var newImg = new Bitmap(bmpTemp);
                            //img_Test.Image = newImg;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            
        }
        public async void SaveImage(string filepath2)
        {

            if (_CaptureID == null)
            {
                _view.CloseForm();
                return;
            }

            if (filepath2 == "")
            {
                Utils.ShowMessage("Please upload an image first to be updated", type: "Warning");
            }
            else { 
                //pnl_Uploading.BackColor = Color.FromArgb(17, 0, 0, 0);
                _view.PnlUploadingBackColor = Color.FromArgb(17, 0, 0, 0);
                _view.PnlUploadingVisibility = true;
                _view.BtnSaveEnable = false;
                //MessageBox.Show(filepath);

                try
                {
                    // Get any Stream — it can be FileStream, MemoryStream or any other type of Stream
                    var stream = File.Open(filepath2, FileMode.Open);

                    // Construct FirebaseStorage with path to where you want to upload the file and put it there
                    var task = new FirebaseStorage("dcmanagement-3d402.appspot.com")
                    .Child("SIREM")
                    .Child(_CaptureID)
                    .Child(_ImageName)
                    .PutAsync(stream);

                    // Track progress of the upload
                    task.Progress.ProgressChanged += (s, ev) =>
                    {
                        _view.PbImage.EditValue = ev.Percentage;
                        _view.PbImage.CreateGraphics().DrawString(ev.Percentage.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(_view.PbImage.Width / 2 - 10, _view.PbImage.Height / 2 - 7));
                        Console.WriteLine($"Progress: {ev.Percentage} %");
                    };

                    // Await the task to wait until upload is completed and get the download url
                    var downloadUrl = await task;
                    Debug.WriteLine(downloadUrl);
                    _view.PnlUploadingVisibility = false;
                    _view.CloseForm();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
    }
}
