using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResponseEmergencySystem.Views;
using ResponseEmergencySystem.Models;
using ResponseEmergencySystem.Services;

namespace ResponseEmergencySystem.Controllers
{
    public class MainController
    {
        IMainView _view;
        public List<Capture> _captures;
        List<Incident> _incidents;
        public string ID_Incident;
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
    }
}
