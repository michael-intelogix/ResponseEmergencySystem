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

namespace ResponseEmergencySystem.Controllers
{
    public class MainController
    {
        IMainView _view;
        public List<Capture> _captures;
        List<Incident> _incidents;
        public string ID_Incident;

        FirestoreDb dataBase;

        //Incident _selectedIncident;
        //DataTable dt_InjuredPersons = new DataTable();

        public MainController(IMainView view, List<Capture> captures, List<Incident> incidents)
        {
            _captures = captures;
            _incidents = incidents;
            _view = view;
            view.SetController(this);
        }

        //public Incident Incident
        //{
        //    get { return _selectedIncident; }
        //}

        public void LoadData()
        {
            _view.LoadIncidents(_incidents);
            _view.LoadCaptures(_captures);

        }

        public void LoadChat(string ID_incident = "")
        {
            string document = ID_Incident != "" ? ID_Incident : _view.ID_Incident;
            ///** Firestore Database Connection **/
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", constants.path);
            dataBase = FirestoreDb.Create("dcmanagement-3d402");

            CollectionReference messagesRef = dataBase.Collection("SIREM-chats").Document("75329DD7-BD87-4D65-BF84-1B7EBF3C8DD6").Collection("messages");
            Query query = messagesRef;

            FirestoreChangeListener listener = query.Listen(snapshot =>
            {
                foreach (DocumentChange change in snapshot.Changes)
                {
                    if (change.ChangeType.ToString() == "Added")
                    {
                        _view.Refresh_Chat(change.Document);

                    }
                }
            });
        }

        public void SetCaptures(string ID_Incident)
        {
            _captures = CaptureService.list_Captures(ID_Incident);
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
            ITXFramework.ITXFramework itx = new ITXFramework.ITXFramework();

            // itx.cfrm_InsertForm
            string now = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            now = now.Replace("/", "-");

            DocumentReference docRef = dataBase.Collection("SIREM-Chats").Document("75329DD7-BD87-4D65-BF84-1B7EBF3C8DD6").Collection("messages").Document(now);
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                {"from", constants.userName  },
                {"text", _view.Message }
            };

            if (_view.Message == "")
            {
                MessageBox.Show("Message is Empty");
                return;
            }
            docRef.SetAsync(data1);
            _view.Message = "";
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
