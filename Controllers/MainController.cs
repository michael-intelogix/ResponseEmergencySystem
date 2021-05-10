﻿using System;
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

namespace ResponseEmergencySystem.Controllers
{
    public class MainController
    {
        IMainView _view;
        public List<Capture> _captures;
        List<Incident> _incidents;
        private string ID_Incident;
        private string ID_Capture;
        public bool loaded = false;

        DataTable access = new DataTable();

        FirestoreDb dataBase;

        FirestoreChangeListener listener;
        int listening = 0;

        //Incident _selectedIncident;
        //DataTable dt_InjuredPersons = new DataTable();

        public MainController(IMainView view, List<Capture> captures, List<Incident> incidents)
        {
            _captures = captures;
            _incidents = incidents;
            _view = view;

            if (incidents.Count > 0)
                ID_Incident = incidents[0].ID_Incident.ToString();
            view.SetController(this);
        }

        //public Incident Incident
        //{
        //    get { return _selectedIncident; }
        //}

        public void IncidentsFilter()
        {
            _incidents = IncidentService.list_Incidents(_view.Folio, "", _view.DriverName, _view.TruckNumber, "", date1: _view.Date1, date2: _view.Date1 == "" ? "" : _view.Date2 );
            if (_incidents.Count > 0)
                _view.Incidents = _incidents;   
        }

        public void Login()
        {
            frm_Login login = new frm_Login();

            if (login.ShowDialog() == DialogResult.OK)
            {
                access = login.myData;
                string idmysoftware = "2a5aa42b-2089-4fa8-b7cc-2cea2a017a8a";
                DataRow[] accesos = access.Select($"ID_Software = '{idmysoftware}'");
                if (accesos.Length > 0)
                {
                    constants.userName = accesos[0].ItemArray[13].ToString();
                }
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
            CollectionReference messagesRef = dataBase.Collection("SIREM-Chats").Document(_view.ID_Incident).Collection("messages");
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
            Login();
            _view.Incidents = _incidents.Select(i => new { i.ID_Incident, i.Name, i.Folio, i.IncidentDate, i.truck.truckNumber, i.ID_StatusDetail });
            _view.CapturesDataSource = _captures.Select(i => new { i.captureType, i.comments, i.ID_Capture });
        }

        public void LoadChat(string ID_incident = "")
        {
            ///** Firestore Database Connection **/
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", constants.path);
            dataBase = FirestoreDb.Create("dcmanagement-3d402");
            ChatListener();
        }

        public void SetCaptures()
        {
            _captures = CaptureService.list_Captures(_view.ID_Incident);
            _view.CapturesDataSource = _captures;
            if (_captures.Count > 0)
                _view.ImagesDatasSource = CaptureService.list_Images(_captures[0].ID_Capture.ToString());
            else
                _view.ImagesDatasSource = new List<ImageCapture>();
        }

        public void SetImages()
        {
            _view.ImagesDatasSource = CaptureService.list_Images(_view.ID_Capture);
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

            DocumentReference docRef = dataBase.Collection("SIREM-Chats").Document(_view.ID_Incident).Collection("messages").Document(now);
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

        public void EditIncidentView(string incidentId)
        {
            EditIncidentDetails viewEditIncident = new EditIncidentDetails();

            Incidents.EditIncidentController incidentCtrl = new Incidents.EditIncidentController(viewEditIncident, incidentId);
            incidentCtrl.LoadIncident();

            viewEditIncident.Show();
        }

        public void ShowIncident(string incidentId, string folio)
        {
            ViewIncidentDetails viewIncident = new ViewIncidentDetails();

            IncidentController incidentCtrl = new IncidentController(viewIncident, incidentId, folio);
            incidentCtrl.LoadIncident();

            viewIncident.Show();
        }

        public void AddIncidentView()
        {
            AddIncidentDetails addIncidentView = new AddIncidentDetails();
            Controllers.Incidents.AddIncidentController addIncidentCtrl = new Controllers.Incidents.AddIncidentController(addIncidentView);
            addIncidentCtrl.LoadStates();
            addIncidentView.ShowDialog();
        }

        public void EditImageView(string imgPath)
        {
            frm_Image frm_Image = new frm_Image("", "", imgPath);
            frm_Image.ShowDialog();
        }

        public void AddMoreCaptures()
        {
            AddMoreCaptures AddMoreCaptures = new AddMoreCaptures();
            Captures.AddCapturesController addCapturesCtrl = new Captures.AddCapturesController(AddMoreCaptures, CaptureService.list_CaptureTypes());
            addCapturesCtrl.LoadCaptures();
            addCapturesCtrl.SetIncidentId(_view.ID_Incident);
            AddMoreCaptures.ShowDialog();
        }

        public List<ImageCapture> GetImages(string captureId)
        {
            if (_captures.Count > 0)
                return CaptureService.list_Images(captureId);
            else
                return new List<ImageCapture>();
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
