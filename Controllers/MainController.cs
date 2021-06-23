using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResponseEmergencySystem.Views;
using ResponseEmergencySystem.Models;
using ResponseEmergencySystem.Services;
using Google.Cloud.Firestore;
using ResponseEmergencySystem.Code;
using System.Windows.Forms;
using ResponseEmergencySystem.Forms;
using System.Data;
using System.IO;
using ResponseEmergencySystem.Properties;

namespace ResponseEmergencySystem.Controllers
{
    public class MainController
    {
        IMainView _view;
        IMain2View _mainView;
        public List<Capture> _captures;
        List<Incident> _incidents;
        List<StatusDetail> _statusDetail;
        private string ID_Incident = "";
        private string ID_Capture = "";
        private string ID_StatusDetail = "423E82C9-EE3F-4D83-9066-01E6927FE14D";
        public bool loaded = false;

        DataTable access = new DataTable();

        FirestoreDb dataBase;

        FirestoreChangeListener listener;
        int listening = 0;

        //Incident _selectedIncident;
        //DataTable dt_InjuredPersons = new DataTable();

        public MainController(IMainView view, ref IMain2View main2View)
        {
            _incidents = new List<Incident>();
            _statusDetail = StatusDetailService.list_StatusDetail();
            _view = view;
            _mainView = main2View;
            view.SetController(this);
        }

        public void IncidentsFilter(string folio, string driverName, string truckNumber, string status, string date1 = "", string date2 = "", bool databaseFilter = false)
        {
            if (databaseFilter)
            {
                _incidents = IncidentService.list_Incidents(folio, "", driverName, truckNumber, status, date1: date1, date2: date2);
                if (_incidents.Count > 0)
                    _view.Incidents = _incidents;
                else
                    _view.Incidents = new List<Incident>();
            }
            else
                _view.SetGridFilters(driverName, truckNumber, folio);
            
        }

        public void EditImageData(bool capture)
        {
            var ID = capture ? _view.ID_Capture.ToString() : _view.ID_Image;
            Forms.Modals.EditComments editCommentsView = new Forms.Modals.EditComments();
            EditImageDataController editImageDataCtrl = new EditImageDataController(editCommentsView, ID, GetID("documentType"), capture);
            editImageDataCtrl.LoadStatusDetail();
            if (editCommentsView.ShowDialog() == DialogResult.OK)
            {
                if (capture)
                {
                    _view.CapturesDataSource = CaptureService.list_Captures(GetID("incident"));
                    Utils.ShowMessage("Capture information has been updated");
                }
                else
                {
                    _view.ImagesDatasSource = CaptureService.list_Images(GetID("capture"));
                    Utils.ShowMessage("Image information has been updated");
                }
                
            }
        }

        public void SetComments()
        {
            AddComments addComments = new AddComments();
            if (addComments.ShowDialog() == DialogResult.OK)
            {
                var t = new Task<Response>(() => CaptureService.UpdateCapture(GetID("capture"), "", "", "", addComments.comments));
                t.Start();
                t.Wait();
                Utils.ShowMessage(t.Result.Message, "Capture");
                _captures = CaptureService.list_Captures(_view.ID_Incident.ToString());
                _view.CapturesDataSource = _captures;
                //addComments.comments;
            }
        }

        public void ChatListener()
        {
            if (listening == 1)
            {
                _view.ChatText = "";
                listener.StopAsync();
                listening = 0;
            }

            //btn_Send.Enabled = true;
            CollectionReference messagesRef = dataBase.Collection("SIREM-Chats").Document(_view.ID_Incident.ToString()).Collection("messages");
            Query query = messagesRef;

            listener = query.Listen(snapshot =>
            {
                listening = 1;
                foreach (DocumentChange change in snapshot.Changes)
                {
                    if (change.ChangeType.ToString() == "Added")
                    {
                        _view.Refresh_Chat(change.Document);
                    }

                }
            });
        }

        public void LoadData()
        {
            //Login();
            //Test();
            _view.StatusDetailDataSource = _statusDetail;
            _incidents = IncidentService.list_Incidents("", "", "", "", "");
            if (_incidents.Count > 0)
            {
                _view.Incidents = _incidents;
                SetIncident();
                SetCaptures();            
            }

            if (!Directory.Exists(Settings.Default.AppFolder))
            {
                Forms.Modals.DirectoryError directoryErrorModal = new Forms.Modals.DirectoryError();
                directoryErrorModal.ShowDialog();
            }


        }

        public void LoadChat(string ID_incident = "")
        {
            if (_view.ID_Incident != null)
            {
                ///** Firestore Database Connection **/
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", constants.path);
                dataBase = FirestoreDb.Create("dcmanagement-3d402");
                ChatListener();
            }
        }

        public void SetCaptures()
        {
            _captures = CaptureService.list_Captures(GetID("incident").ToString());
            if (_captures.Count > 0)
            {
                ID_Capture = _captures[0].ID_Capture.ToString();
                _view.CapturesDataSource = _captures.Select(c => new { c.captureType, c.comments, c.ID_Capture });
                if (_captures.Count > 0)
                    _view.ImagesDatasSource = CaptureService.list_Images(ID_Capture);
                else
                    _view.ImagesDatasSource = new List<ImageCapture>();
            }
            else
            {
                _view.CapturesDataSource = new List<Capture>();
                _view.ImagesDatasSource = new List<ImageCapture>();
            }

        }

        public void SetImages()
        {
            if (_captures.Count > 0)
            {
                var images = CaptureService.list_Images(GetID("capture"));
                _view.ImagesDatasSource = images;
            }
               
        }

        private void GetImage()
        {
            //https://firebasestorage.googleapis.com/v0/b/dcmanagement-3d402.appspot.com/o/SIREM%2FCaptures%2F0B121804-EF9C-497D-816F-39B3BF3FF92A%2FFront%20of%20the%20Truck?alt=media&token=a2b4133a-affa-4234-8b62-bf5790fdfba4
            //btn_DownloadFile.Enabled = true;
            //btn_View.Enabled = false;
            //splashScreenManager1.ShowWaitForm();
            //imagePath = gv_Images.GetFocusedRowCellValue("ImagePath").ToString();
            //imagePath = imagePath.Replace(@"https://www.dropbox.com/", @"https://dl.dropboxusercontent.com/");

            //try
            //{
            //    System.Net.WebRequest request =
            //        System.Net.WebRequest.Create(imagePath);
            //    System.Net.WebResponse response = request.GetResponse();
            //    System.IO.Stream responseStream =
            //        response.GetResponseStream();
            //    Bitmap bitmap2 = new Bitmap(responseStream);
            //    pictureEdit_Image.Image = bitmap2;
            //    splashScreenManager1.CloseWaitForm();
            //    btn_View.Enabled = true;
            //    btn_RotateInverse.Enabled = true;
            //}
            //catch (System.Net.WebException)
            //{
            //    MessageBox.Show("There was an error opening the image file.");
            //}
        }

        public void Send()
        {
            //ITXFramework.ITXFramework itx = new ITXFramework.ITXFramework();

            // itx.cfrm_InsertForm
            string now = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            now = now.Replace("/", "-");

            DocumentReference docRef = dataBase.Collection("SIREM-Chats").Document(_view.ID_Incident.ToString()).Collection("messages").Document(now);
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                { "from", constants.userName },
                { "text", _view.Message }
            };

            if (_view.Message == "")
            {
                MessageBox.Show("Message is Empty");
                return;
            }
            docRef.SetAsync(data1);
            _view.Message = "";
        }

        public void EditIncidentView()
        {
            //Forms.Incidents.EditDriverIncident editIncidentView = new Forms.Incidents.EditDriverIncident();
            
            _view.OpenSpinner();

            //Incidents.EditIncidentController incidentCtrl = new Incidents.EditIncidentController(editIncidentView, GetID("incident"), ref _view);
            Forms.Incidents.DriverIncident editIncident = new Forms.Incidents.DriverIncident();
            Incidents.DriverIncidentController editIncidentCtrl = new Incidents.DriverIncidentController(editIncident, GetID("incident"), _view.Folio.ToString());

            editIncident.Load += new System.EventHandler((object sender, EventArgs e) =>
            {
                _view.CloseSpinner();
            });

            if (editIncident.ShowDialog() == DialogResult.OK)
            {
                Utils.ShowMessage("the Incident was updated succesfully", "Incident");
                ClearFilters();
            }

        }

        public void ShowIncident()
        {
            if (_view.Folio != null)
            {
                _view.OpenSpinner();
                Forms.Incidents.DriverIncident viewIncident = new Forms.Incidents.DriverIncident("show");
                Incidents.DriverIncidentController viewIncidentCtrl = new Incidents.DriverIncidentController(viewIncident, GetID("incident"), _view.Folio.ToString());

                viewIncident.Load += new System.EventHandler((object sender, EventArgs e) =>
                {
                    _view.CloseSpinner();
                });

                viewIncident.Show();
            }
               
        }

        public void AddIncidentView()
        {
            _view.OpenSpinner();

            Forms.Incidents.DriverIncident addIncident = new Forms.Incidents.DriverIncident("add");
            Incidents.DriverIncidentController addIncidentCtrl = new Incidents.DriverIncidentController(addIncident, Guid.Empty.ToString(), "");

            //addIncidentCtrl.LoadStates();
            //addIncidentCtrl.LoadDrivers();
            //addIncidentCtrl.LoadTrucks();

            addIncident.Load += new System.EventHandler((object sender, EventArgs e) =>
            {
                _view.CloseSpinner();
            });

            if (addIncident.ShowDialog() == DialogResult.OK)
            {
                Utils.ShowMessage("the Incident was added succesfully", "Incident");
                ClearFilters();
            }
              
        }

        public void EditImageView(string imgPath, string fileType)
        {
            if (fileType == "img")
            {
                frm_Image imageView = new frm_Image("", "", imgPath);
                ImageController appConfigCtrl = new ImageController(imageView, GetID("capture"), _view.ImageName);
                if (imageView.ShowDialog() == DialogResult.OK)
                {
                    Utils.ShowMessage("Image has been updated");
                }
            }

            if (fileType == "pdf")
            {
                frm_PdfViewer pdfViewer = new frm_PdfViewer(imgPath);
                pdfViewer.ShowDialog();
            }
            
        }

        public void AddMoreCaptures()
        {
            //AddMoreCaptures AddMoreCaptures = new AddMoreCaptures();
            //Captures.AddCapturesController addCapturesCtrl = new Captures.AddCapturesController(AddMoreCaptures, CaptureService.list_CaptureTypes());
            //addCapturesCtrl.LoadCaptures();
            //addCapturesCtrl.SetIncidentId(_view.ID_Incident.ToString());
            //if (AddMoreCaptures.ShowDialog() == DialogResult.OK)
            //{
            //    Utils.ShowMessage("the capture was added succesfully", "Capture");
            //    _view.OpenSpinner();
            //    _captures = CaptureService.list_Captures(_view.ID_Incident.ToString());
            //    _view.CapturesDataSource = _captures;
            //    if (_captures.Count > 0)
            //        _view.ImagesDatasSource = CaptureService.list_Images(_captures[0].ID_Capture.ToString());
            //    else
            //        _view.ImagesDatasSource = new List<ImageCapture>();
            //    _view.CloseSpinner();
            //}
        }

        public List<ImageCapture> GetImages(string captureId)
        {
            if (_captures.Count > 0)
                return CaptureService.list_Images(captureId);
            else
                return new List<ImageCapture>();
        }

        private string GetID(string name)
        {
            switch (name)
            {
                case "incident":
                    return _view.ID_Incident == null ? ID_Incident : _view.ID_Incident.ToString();
                case "capture":
                    return _view.ID_Capture == null ? ID_Capture : _view.ID_Capture.ToString();
                case "status":
                    return _view.ID_StatusDetail == null ? ID_StatusDetail : _view.ID_StatusDetail.ToString();
                case "truck":
                    return _view.TruckNumber == null ? "" : _view.TruckNumber.ToString();
                case "documentType":
                    return _view.DocumentType == null ? "" : _view.DocumentType.ToString();
                default:
                    return "";
            }

        }

        public void ClearFilters(string status = "423E82C9-EE3F-4D83-9066-01E6927FE14D")
        {
            IncidentsFilter("", "", "", status, databaseFilter: true);
            _view.ClearFilters();
        }

        public void SaveStatus(bool all = false)
        {
            _view.OpenSpinner();
            if (all)
            {
                List<Incident> incidents = (List<Incident>)_view.Incidents;
                if (incidents == null)
                {
                    _view.CloseSpinner();
                    return;
                }

                foreach (var incident in incidents)
                {
                    if (GetID("truck") != "")
                        IncidentService.UpdateStatus(incident.ID_Incident.ToString(), incident.ID_StatusDetail, incident.truck.ID);
                }

            }
            else
            {
                if (GetID("truck") != "")
                    IncidentService.UpdateStatus(GetID("incident"), GetID("status"), GetID("truck"));
            }
            ClearFilters();
            _view.CloseSpinner();


            Utils.ShowMessage("Status has been updated correctly.", title: "Status Updated", type: "Approved");
        }

        public void CloseIncident()
        {
            IncidentService.CloseIncident(GetID("incident"));
            _view.ID_StatusDetail = (object)"AF034BC4-3F32-4174-B042-3178B2EC8199";
        }

        public void SetIncident(string ID_Incident = "")
        {
            if (_view.Folio != null)
            {
                ID_Incident = GetID("incident");
                _view.LblFolio = _view.Folio.ToString();
                _view.LblFolioPosition();
            }
        }

        public void DeleteIncident()
        {
            if (_view.Folio != null)
            {
                _view.OpenSpinner();
                IncidentService.Delete(GetID("incident"), _view.Folio.ToString());
                ClearFilters();
                _view.CloseSpinner();
            }
        }

        public void AddDocumentsToCapture()
        {
            //_docs[gv_DocumentCaptures.FocusedRowHandle].documents.Add(new Models.Documents.Document("", 0));
            //gc_Documents.DataSource = _docs[gv_DocumentCaptures.FocusedRowHandle].documents;
            //gv_Documents.BestFitColumns();
        }

        public void AddLocation()
        {
            var t = new Task(() => IncidentService.UpdateLocation(GetID("incident"), GetID("truck")));
            t.Start();
            t.Wait();

            Utils.ShowMessage("location has been registered correctly.", title: "location", type: "Approved");
        }
    }

    #region firestore properties
    [FirestoreData]
    internal class Data
    {
        [FirestoreProperty]
        public string from { get; set; }
        [FirestoreProperty]
        public string text { get; set; }
    }
    #endregion
}
