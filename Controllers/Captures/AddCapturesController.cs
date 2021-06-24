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
using ResponseEmergencySystem.Code;
using DevExpress.XtraEditors;
using ResponseEmergencySystem.Properties;

namespace ResponseEmergencySystem.Controllers.Captures
{
    public class AddCapturesController
    {
        IAddCapturesView _view;
        public List<Capture> _captures;
        
        private Capture _selectedCaptureType;
        private string ID_Incident;
        private string ID_Capture;
        private List<ImageCapture> _images;
        private List<DocumentCapture> _documents2;
        private Models.Documents.DocumentCapture _documentCapture;
        private List<Models.Documents.Document> _documents;
      

        private List<DocumentCapture> _docsLoaded;

        private string _imgUrl = "";

        public AddCapturesController(IAddCapturesView view, List<Capture> captures)
        {
            _view = view;
            _captures = captures;
            _images = new List<ImageCapture>();
            _documents2 = new List<DocumentCapture>();
            view.SetController(this);
        }

        public void SetIncidentId(string id)
        {
            ID_Incident = id;
            //if (id == Guid.Empty.ToString())
            _documents = new List<Models.Documents.Document>();
        }

        public void LoadCaptures()
        {
            _view.LoadCapturesTypes(_captures);
        }

        public void UploadImage(string imgName, int idx)
        {

            if (idx > _images.Count())
                _images.Add(new ImageCapture(imgName, _imgUrl, idx));
            else
                _images[idx - 1].ImagePath = _imgUrl;
            _view.LueTypeBlock = true;
            _view.SaveButtonEnable = true;
            //MessageBox.Show(ofd.FileName);
        }

        //public async void SaveAsync()
        //{
        //    if (img)
        //    {
        //        _view.SaveButtonEnable = false;

        //        //Thread.Sleep(5000);

        //        var t = new Task(() => SaveCapture());
        //        t.Start();
        //        t.Wait();

        //        for (var i = 0; i < _images.Count(); i++)
        //        {
        //            var id = _images[i].ID;
        //            var task = UploadImgFirebaseAsync(_images[i].ImagePath, _images[i].ImageName);
        //            _view.BtnArray[id].Visible = false;
        //            _view.PbrArray[id].Visible = true;

        //            // Track progress of the upload
        //            task.Progress.ProgressChanged += (s, ev) =>
        //            {
        //                _view.PbrArray[id].EditValue = ev.Percentage;
        //                _view.PbrArray[id].CreateGraphics().DrawString(ev.Percentage.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(_view.PbrArray[i].Width / 2 - 10, _view.PbrArray[i].Height / 2 - 7));
        //                Console.WriteLine($"Progress: {ev.Percentage} %");
        //            };

        //            _images[i].ImageFireBaseUrl = await task;

        //            var urlTest = _images[i].ImageFireBaseUrl.Split(new string[] { "%2F" }, StringSplitOptions.None)[2].Split('=')[2];
        //            //var urlTest2 = urlTest[2].Split('=');
        //            //var token = urlTest2[2];

        //            Response imgResponse = CaptureService.AddImage(Guid.NewGuid().ToString(), ID_Capture, _images[i].ImageFireBaseUrl, _images[i].ImageName, "");
        //            _images[i].ID_Image = imgResponse.ID;
        //            //MessageBox.Show(imgResponse.Message + ", ID: " + imgResponse.ID);
        //            //CaptureService.AddCapture(_selectedCaptureType.ID_CaptureType, ID_Incident, "test service", "");

        //            _view.PbrArray[id].Visible = false;
        //            _view.BtnArray[id].Text = "Uploaded";
        //            _view.BtnArray[id].Visible = true;
        //        }
        //        Reset();
        //    }
        //}

        public async void SaveAsync()
        {
            _view.LueTypeBlock = true;
            _view.SaveButtonEnable = false;
            //Thread.Sleep(5000);
            //Task t1;

            //if (ID_Incident == Guid.Empty.ToString())
            //{
            //    t1 = new Task(() => SaveEmptyIncident());
            //    t1.Start();
            //    t1.Wait();
            //}

            //var t = new Task(() => SaveCapture());
            //t.Start();
            //t.Wait();
            var captureId = Guid.NewGuid().ToString();

            var docsLoaded = _documents.Where(d => d.Path != "").ToList();
            for (var i = 0; i < docsLoaded.Count(); i++)
            {
                var document = docsLoaded[i];
                var id = docsLoaded[i].ID;
                var tempPath = Path.GetTempPath();
                var task = UploadImgFirebaseAsync(captureId, docsLoaded[i].Path, docsLoaded[i].name);

                ProgressBarControl pbr = _view.GetPbrControl(document.containerName, $"pbrDocument{document.ID}");

                _view.SetControlProperties(document.containerName, $"lblStatus{document.ID}", visibility: false);
                pbr.Visible = true;

                // Track progress of the upload
                task.Progress.ProgressChanged += (s, ev) =>
                {
                    pbr.EditValue = ev.Percentage;
                    pbr.CreateGraphics().DrawString(ev.Percentage.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(pbr.Width / 2 - 10, pbr.Height / 2 - 7));
                    Console.WriteLine($"Progress: {ev.Percentage} %");
                };

                document.FirebaseUrl = await task;


                pbr.Visible = false;
                _view.SetControlProperties(document.containerName, $"lblStatus{document.ID}", "Uploaded", true);

                //Response imgResponse = CaptureService.AddImage(Guid.NewGuid().ToString(), ID_Capture, document.FirebaseUrl, document.name, "", document.Type);
                //document.ID_Document = imgResponse.ID;
                document.SetImage(true);
            }


            
            _documentCapture = new Models.Documents.DocumentCapture(_selectedCaptureType.ID_CaptureType, _selectedCaptureType.captureType, _view.Comments);
            _documentCapture.ID_Capture = captureId;
            _documentCapture.Status = "created";
            _documentCapture.documents = _documents;


            ////_docsLoaded = _docsLoaded.Count > 0 ? _docsLoaded : new List<DocumentCapture>();
            //var tempDocs = _documentCapture.documents.Where(d => d.Path != null).ToList();
            //for (var i = 0; i < tempDocs.Count(); i++)
            //{
            //    var document = tempDocs[i];
            //    var id = tempDocs[i].ID;
            //    document.Comments = _view.Comments;
                
            //}
            Reset();
        }

        public void SaveLocal()
        {
            _view.LueTypeBlock = true;
            _view.SaveButtonEnable = false;

            //var t = new Task(() => SaveCapture());
            //t.Start();
            //t.Wait();

            _documentCapture = new Models.Documents.DocumentCapture(_selectedCaptureType.ID_CaptureType, _selectedCaptureType.captureType, _view.Comments);
            _documentCapture.Status = "created";
            _documentCapture.documents = _documents;

            //_docsLoaded = _docsLoaded.Count > 0 ? _docsLoaded : new List<DocumentCapture>();
            var tempDocs = _documentCapture.documents.Where(d => d.Path != null).ToList();
            for (var i = 0; i < tempDocs.Count(); i++)
            {
                var document = tempDocs[i];
                var id = tempDocs[i].ID;
                document.Comments = _view.Comments;
                
            }
            
            Reset();
        }

        public void SetType(string captureTypeId)
        {
            _selectedCaptureType = _captures.Where(c => c.ID_CaptureType == captureTypeId).First();
            _view.ClearCapturesPanel();
            List<ImageCapture> documentNames = _selectedCaptureType.imagesListOfNames;

            //if (ID_Incident != Guid.Empty.ToString())
            //    _documents2.Clear();
            //else
            _documents.Clear();

            switch (_selectedCaptureType.captureType)
            {
                case "Insurance Policy":
                    for (int i = 0; i < documentNames.Count; i++)
                    {
                        _view.SetPnlCapture(GetPnlCapture(i, documentNames[i].name));
                    }
                    break;
                case "Testimony":
                    for (int i = 0; i < documentNames.Count; i++)
                    {
                        _view.SetPnlCapture(GetPnlCapture(i, documentNames[i].name));
                    }
                    break;
                default:
                    for (int i = 0; i < documentNames.Count; i++)
                    {
                        _view.SetPnlCapture(GetPnlCapture(i, documentNames[i].name));
                    }
                    
                    //_view.PnlCapture1Visbility = true;
                    //_view.PnlCapture2Visbility = true;
                    //_view.PnlCapture3Visbility = true;
                    //_view.PnlCapture4Visbility = true;
                    //_view.LblCapture1Name = _selectedCaptureType.imagesListOfNames[0].name;
                    //_view.LblCapture2Name = _selectedCaptureType.imagesListOfNames[1].name;
                    //_view.LblCapture3Name = _selectedCaptureType.imagesListOfNames[2].name;
                    //_view.LblCapture4Name = _selectedCaptureType.imagesListOfNames[3].name;
                    break;
            }
            
        }

        private FirebaseStorageTask UploadImgFirebaseAsync(string captureId, string filepath, string name)
        {
            try
            {
                // Get any Stream — it can be FileStream, MemoryStream or any other type of Stream
                var stream = File.Open(filepath, FileMode.Open);

                // Construct FirebaseStorage with path to where you want to upload the file and put it there
                var task = new FirebaseStorage("dcmanagement-3d402.appspot.com")
                .Child("SIREM")
                .Child(captureId)
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
            //for (var i = 0; i < _images.Count(); i++)
            //{
            //    _view.BtnArray[_images[i].ID].Visible = false;
            //}
            //_images.Clear();
            //_view.LueTypeBlock = false;
            _view.CloseView();
            //_view.SaveButtonEnable = true;
        }

        private void SaveCapture()
        {
            var response = CaptureService.AddCapture("",_selectedCaptureType.ID_CaptureType, ID_Incident, "testing", _view.Comments);
            ID_Capture = response.ID;
        }

        private void SaveEmptyIncident()
        {
            var response = IncidentService.CreateEmptyIncident();
            ID_Incident = response.ID;
        }

        private PanelControl GetPnlCapture(int i, string lblText)
        {
            int y = i == 0 ? 3 : 3 + (i * 37);
            PanelControl pnl = new PanelControl();
            pnl.Location = new Point(3, y);
            pnl.Size = new Size(459, 31);
            pnl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            pnl.Name = $"documentContainer{i}";

            LabelControl lbl = new LabelControl();
            lbl.Location = new Point(8, 7);
            lbl.Font = new Font((string)"Malgun Gothic", (float)9.75);
            lbl.Text = lblText;

            SimpleButton btn = new SimpleButton(); btn.Location = new Point(188, 3);
            btn.Size = new Size(135, 23);
            btn.Font = new Font((string)"Malgun Gothic", (float)9.75);
            btn.ImageOptions.SvgImage = Resources.uploadWhite;
            btn.ImageOptions.SvgImageSize = new Size(25, 25);
            btn.Appearance.BackColor = Color.FromArgb(23, 40, 94);
            btn.Appearance.ForeColor = Color.White;
            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.Options.UseForeColor = true;
            btn.Text = "Upload Image";
            btn.Click += new System.EventHandler(btnUploadClick);

            ProgressBarControl pbrBar = new ProgressBarControl();
            pbrBar.Location = new Point(346, 7);
            pbrBar.Size = new Size(110, 17);
            pbrBar.Visible = false;
            pbrBar.Name = $"pbrDocument{i}";

            LabelControl lbl2 = new LabelControl();
            lbl2.Location = new Point(357, 4);
            lbl2.Font = new Font((string)"Malgun Gothic", (float)9.75, FontStyle.Bold);
            lbl2.ImageAlignToText = ImageAlignToText.LeftCenter;
            lbl2.ImageOptions.Image = Resources.apply_16x161;
            lbl2.Text = "Upload";
            lbl2.Appearance.ForeColor = Color.FromArgb(8, 138, 50);
            lbl2.Appearance.Options.UseForeColor = true;
            lbl2.Name = $"lblStatus{i}";
            lbl2.Visible = false;

            pnl.Controls.Add(lbl);
            pnl.Controls.Add(btn);
            pnl.Controls.Add(pbrBar);
            pnl.Controls.Add(lbl2);
            lbl2.BringToFront();

            _documents.Add(new Models.Documents.Document(pnl.Name, lblText, i, true));

            //if (ID_Incident == Guid.Empty.ToString())
            //{
            //    _documents.Add(new Models.Documents.Document(pnl.Name, lblText, i));
            //}
            //else
            //{
            //    _documents2.Add(new DocumentCapture(pnl.Name, lblText, i));
            //}

            return pnl;
        }

        private void btnUploadClick(object sender, EventArgs e)
        {
            var btn = (SimpleButton)sender;
            bool preloaded = CheckDocument(btn.Parent.Name);
            _view.LueTypeBlock = true;
            _view.SaveButtonEnable = true;
        }

        public bool CheckDocument(string containerName)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files(*.PNG;*.JPG;*.GIF;*.BMP)|*.PNG;*.JPG;*.GIF;*.BMP|PDF Files (*.PDF)|*.PDF|All Files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string ext = Path.GetExtension(ofd.FileName).ToUpper();
                    try
                    {
                        if (ID_Incident != Guid.Empty.ToString())
                        {
                            var document = _documents2.Where(d => d.containerName == containerName).First();

                            if (ext == ".GIF" || ext == ".JPG" || ext == ".PNG" || ext == ".BMP")
                            {
                                document.Path = ofd.FileName;
                                document.Type = "img";
                                _view.SetControlProperties(document.containerName, $"lblStatus{document.ID}", "Preloaded");

                                return true;
                            }
                            else if (ext == ".PDF")
                            {
                                document.Path = ofd.FileName;
                                document.Type = "pdf";
                                _view.SetControlProperties(document.containerName, $"lblStatus{document.ID}", "Preloaded");
                                return true;
                            }
                            else
                            {
                                Utils.ShowMessage("The file submitted is not an Image", title: "Image upload error", type: "Warning");
                                return false;
                            }
                        }
                        else
                        {
                            var document2 = _documents.Where(d => d.containerName == containerName).First();

                            if (ext == ".GIF" || ext == ".JPG" || ext == ".PNG" || ext == ".BMP")
                            {
                                document2.Path = ofd.FileName;
                                document2.Type = "img";
                                document2.SetStatus("created");
                                //document2.SetImage();
                                _view.SetControlProperties(document2.containerName, $"lblStatus{document2.ID}", "Preloaded");

                                return true;
                            }
                            else if (ext == ".PDF")
                            {
                                document2.Path = ofd.FileName;
                                document2.Type = "pdf";
                                document2.SetStatus("created");
                                //document2.SetImage();
                                _view.SetControlProperties(document2.containerName, $"lblStatus{document2.ID}", "Preloaded");
                                return true;
                            }
                            else
                            {
                                Utils.ShowMessage("The file submitted is not an Image", title: "Image upload error", type: "Warning");
                                return false;
                            }
                        }

                       
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Utils.ShowMessage(ex.Message, title: "Image upload error", type: "Error");
                        return false;
                    }
                }
                else
                {
                    return false;
                }



            }
        }

        public Models.Documents.DocumentCapture GetDocuments()
        {
            return _documentCapture;
        }

        public void LoadDocuments(List<DocumentCapture> docs)
        {
            _docsLoaded = docs;
        }
    }


}
