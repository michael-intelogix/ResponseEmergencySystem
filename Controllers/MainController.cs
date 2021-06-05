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
using ResponseEmergencySystem.Forms ;

namespace ResponseEmergencySystem.Controllers
{
    public class MainController
    {
        IMainView _view;
        public List<Capture> _captures;
        List<Incident> _incidents;
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

        public MainController(IMainView view)
        {
            _incidents = new List<Incident>();
            _view = view;
            view.SetController(this);
        }

        public void IncidentsFilter(string folio, string driverName, string truckNumber, string status, string date1 = "", string date2 = "", bool databaseFilter = false)
        {
            if (databaseFilter)
            {
                _incidents = IncidentService.list_Incidents(folio, "", driverName, truckNumber, status, date1: date1, date2: date2);
                if (_incidents.Count > 0)
                    _view.Incidents = _incidents;
            }
            else
                _view.SetGridFilters(driverName, truckNumber, folio);
            
        }

        public void Test()
        {
            Forms.Modals.Testing test = new Forms.Modals.Testing();
            test.Show();
            
        }

        public void EditImageData()
        {
            Forms.Modals.EditComments editCommentsView = new Forms.Modals.EditComments();
            EditImageDataController editImageDataCtrl = new EditImageDataController(editCommentsView, _view.ID_Image);
            editImageDataCtrl.LoadStatusDetail();
            if (editCommentsView.ShowDialog() == DialogResult.OK)
            {
                _view.ImagesDatasSource = CaptureService.list_Images(GetID("capture"));
                Utils.ShowMessage("Image information has been updated");
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
            _view.StatusDetailDataSource = StatusDetailService.list_StatusDetail();
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
                _view.CapturesDataSource = _captures;
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
            EditIncidentDetails editIncidentView = new EditIncidentDetails();
            
            _view.OpenSpinner();

            Incidents.EditIncidentController incidentCtrl = new Incidents.EditIncidentController(editIncidentView, GetID("incident"));
            incidentCtrl.LoadIncident();

            editIncidentView.Load += new System.EventHandler((object sender, EventArgs e) =>
            {
                _view.CloseSpinner();
            });

            if (editIncidentView.ShowDialog() == DialogResult.OK)
            {
                Utils.ShowMessage("the Incident was updated succesfully", "Incident");
                _view.Incidents = IncidentService.list_Incidents("", "", "", "", "").Select(i => new { i.ID_Incident, i.Name, i.Folio, i.IncidentDate, i.truck.truckNumber, i.ID_StatusDetail });
            }

        }

        public void ShowIncident(string incidentId = "", string folio = "")
        {
            if (_view.Folio != null)
            {
                ViewIncidentDetails viewIncident = new ViewIncidentDetails();

                _view.OpenSpinner();

                IncidentController incidentCtrl = new IncidentController(viewIncident, GetID("incident"), _view.Folio.ToString());
                incidentCtrl.LoadIncident();

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
            AddIncidentDetails addIncidentView = new AddIncidentDetails();
            Controllers.Incidents.AddIncidentController addIncidentCtrl = new Controllers.Incidents.AddIncidentController(addIncidentView);
            addIncidentCtrl.LoadStates();
            addIncidentView.Load += new System.EventHandler((object sender, EventArgs e) =>
            {
                _view.CloseSpinner();
            });

            if (addIncidentView.ShowDialog() == DialogResult.OK)
            {
                Utils.ShowMessage("the Incident was added succesfully", "Incident");
                _view.Incidents = IncidentService.list_Incidents("", "", "", "", "").Select(i => new { i.ID_Incident, i.Name, i.Folio, i.IncidentDate, i.truck.truckNumber, i.ID_StatusDetail });
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
            AddMoreCaptures AddMoreCaptures = new AddMoreCaptures();
            Captures.AddCapturesController addCapturesCtrl = new Captures.AddCapturesController(AddMoreCaptures, CaptureService.list_CaptureTypes());
            addCapturesCtrl.LoadCaptures();
            addCapturesCtrl.SetIncidentId(_view.ID_Incident.ToString());
            if (AddMoreCaptures.ShowDialog() == DialogResult.OK)
            {
                Utils.ShowMessage("the capture was added succesfully", "Capture");
                _view.OpenSpinner();
                _captures = CaptureService.list_Captures(_view.ID_Incident.ToString());
                _view.CapturesDataSource = _captures;
                if (_captures.Count > 0)
                    _view.ImagesDatasSource = CaptureService.list_Images(_captures[0].ID_Capture.ToString());
                else
                    _view.ImagesDatasSource = new List<ImageCapture>();
                _view.CloseSpinner();
            }
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
                default:
                    return "";
            }

        }

        public void ClearFilters()
        {
            IncidentsFilter("", "", "", "", databaseFilter: true);
            _view.ClearFilters();
        }

        public void SaveStatus()
        {
            StatusDetailService.UpdateStatus(GetID("incident"), GetID("status"));
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
