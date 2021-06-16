using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Controllers;
using ResponseEmergencySystem.Models;
using ResponseEmergencySystem.Properties;
using ResponseEmergencySystem.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponseEmergencySystem.Forms.Incidents
{
    public partial class ShowDriverIncident : DevExpress.XtraEditors.XtraForm, IShowIncidentDetails
    {
        public ShowDriverIncident()
        {
            InitializeComponent();
        }
        private List<Models.Documents.DocumentCapture> _docs;

        IncidentController _controller;

        private void loadData(string incidentId)
        {

        }

        public void SetController(IncidentController controller)
        {
            _controller = controller;
        }
        #region documents
        public void LoadIncident()
        {
            //_docs = new List<Models.Documents.DocumentCapture>();
            var docmentsByCaptureType = _docs.Select(dc => new { dc.CaptureType }).ToList();
            gc_DocumentCaptures.DataSource = docmentsByCaptureType;

            if (Utils.GetEdtValue(edt_TruckNumber) == "")
            {
                gMapControl1.Position = new GMap.NET.PointLatLng(36.05948, -102.51325);
            }
            else
            {
                
                var res = _controller.GetTruckSamsara();
                gMapControl1.Position = new GMap.NET.PointLatLng(res[0], res[1]);
                GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
                GMap.NET.WindowsForms.GMapMarker marker = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(new GMap.NET.PointLatLng(res[0], res[1]), GMap.NET.WindowsForms.Markers.GMarkerGoogleType.red_dot);
                gMapControl1.Overlays.Clear();
                markers.Markers.Add(marker);
                gMapControl1.Overlays.Add(markers);
                
            }
        }

        // clear the panel and redraw using the new cards
        private void gc_DocumentCaptures_DoubleClick(object sender, EventArgs e)
        {
            int idx = gv_DocumentCaptures.GetFocusedDataSourceRowIndex();
            //gc_Documents.DataSource = _docs[idx].documents;
            xtraScrollableControl1.Controls.Clear();
            CreatePanel(4, _docs[idx].documents);
        }

        private void CreatePanel(int number, List<Models.Documents.Document> documents)
        {
            int documentsNotDeletedCount = documents.Where(dnd => dnd.Status != "deleted").ToList().Count;
            int space = (Convert.ToInt32(xtraScrollableControl1.Width) - (documentsNotDeletedCount * 245)) / (documentsNotDeletedCount + 1);
            int cont = 0;

            for (int i = 0; i < documents.Count; i++)
            {

                if (_docs[gv_DocumentCaptures.FocusedRowHandle].documents[i].Status == "deleted")
                    continue;

                space = (space < 50) ? 50 : space;

                var x = (cont * 245) + ((cont + 1) * space);

                cont++;

                _docs[gv_DocumentCaptures.FocusedRowHandle].documents[i].ID = i;
                bool loadImage = _docs[gv_DocumentCaptures.FocusedRowHandle].documents[i].Status == "loaded";
                Image docImage = Resources.add_32x32;
                if (loadImage)
                    docImage = _docs[gv_DocumentCaptures.FocusedRowHandle].documents[i].Image;



                PanelControl pnl = new PanelControl();
                pnl.Size = new Size(242, 261);
                pnl.Location = new Point(x, 19);
                pnl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;

                PictureEdit pic = new PictureEdit();
                pic.Location = new Point(12, 5);
                pic.Size = new Size(218, 171);
                if (documents[i].Path == "")
                {
                    pic.Image = docImage;
                    pic.Properties.SizeMode = loadImage ? PictureSizeMode.Zoom : PictureSizeMode.Clip;
                    pic.Properties.ZoomPercent = 200;
                    pic.Properties.PictureAlignment = ContentAlignment.MiddleCenter;
                    pic.BackColor = Color.Transparent;
                    pic.BorderStyle = BorderStyles.NoBorder;
                }
                else
                {
                    pic.Image = documents[i].Image;
                    pic.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
                }

                TextEdit edt = new TextEdit();
                edt.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                edt.Location = new Point(5, 139);
                edt.Size = new Size(199, 24);
                edt.Text = "Caption of the police report";

                LabelControl lbl = new LabelControl();
                lbl.AutoSizeMode = LabelAutoSizeMode.None;
                lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                lbl.Location = new Point(12, 182);
                lbl.Size = new Size(218, 24);
                lbl.Text = documents[i].name;


                MyButton btnView = new MyButton();
                btnView.ImageOptions.SvgImage = Resources.EyeBlue;
                btnView.ImageOptions.SvgImageSize = new Size(35, 35);
                btnView.ImageOptions.ImageToTextAlignment = ImageAlignToText.TopCenter;
                btnView.Size = new Size(36, 34);
                btnView.Location = new Point(108, 212);
                btnView.btnIdx = i;
                btnView.Click += (object sender, EventArgs e) =>
                {
                    string firebaseUrl = _docs[gv_DocumentCaptures.FocusedRowHandle].documents[btnView.btnIdx].FirebaseUrl;
                    string localUrl = _docs[gv_DocumentCaptures.FocusedRowHandle].documents[btnView.btnIdx].Path;
                    string status = _docs[gv_DocumentCaptures.FocusedRowHandle].documents[btnView.btnIdx].Status;
                    string type = _docs[gv_DocumentCaptures.FocusedRowHandle].documents[btnView.btnIdx].Type;
                    _controller.EditImageView(status == "created" || status == "updated" ? localUrl : firebaseUrl, type, status != "created" && status != "updated");
                };

                pnl.Controls.Add(pic);
                pnl.Controls.Add(lbl);
                pnl.Controls.Add(btnView);

                xtraScrollableControl1.Controls.Add(pnl);
            }

        }
        #endregion

        public void LoadStates(DataTable dt_States)
        {
            lue_DriverLicenseState.Properties.DataSource = dt_States;
            lue_LocationStates.Properties.DataSource = dt_States;
        }
        public void LoadCities(DataTable dt_Cities)
        {

        }
        public void LoadInjuredPersons(DataTable dt_InjuredPersons)
        {
            //gc_InjuredPersons.DataSource = dt_InjuredPersons;
        }

        #region Form Properties

        public List<Models.Documents.DocumentCapture> Documents
        {
            get
            {
                return _docs;
            }
            set
            {
                _docs = value;
            }
        }

        public bool ShowMailButton
        {
            set { simpleButton2.Visible = value; }
        }

        public object LueCitiesDataSource
        {
            set { lue_LocationCities.Properties.DataSource = value; }
        }

        public object InvolvedPersonsDataSorurce
        {
            set { gc_InvolvedPersons.DataSource = value; }
        }

        public object MailDirectoryCategoriesDataSource
        {
            set 
            {
                lue_MailDirectoryCategories.Properties.DataSource = value;
            }
        }

        public object MailDirectoryDataSource
        {
            set 
            {
                lue_MailDirectory.Properties.DataSource = value;
            }
        }

        #endregion

        #region view inputs

        public bool SendToAllRecipientsInTheCategory
        {
            get 
            { 
                return (bool)ckedt_MailByCategory.EditValue; 
            }
        }

        public string SelectedMail
        {
            get { return lue_MailDirectory.EditValue == null ? "" : lue_MailDirectory.EditValue.ToString(); }
        }

        public string MailDirectoryCategory
        {
            get { return lue_MailDirectoryCategories.EditValue == null ? "" : lue_MailDirectoryCategories.EditValue.ToString(); }
        }

        public string FullName
        {
            get { return Utils.GetEdtValue(edt_FullName); }
            set { edt_FullName.EditValue = value; }
        }

        public string PhoneNumber
        {
            get { return Utils.GetEdtValue(edt_PhoneNumber); }
            set { edt_PhoneNumber.EditValue = value; }
        }

        public string License
        {
            get { return Utils.GetEdtValue(edt_License); }
            set { edt_License.EditValue = value; }
        }

        public DateTime ExpirationDate
        {
            get { return dte_ExpirationDate.DateTime; }
            set { dte_ExpirationDate.EditValue = value; }
        }

        public string LicenseState
        {
            get { return lue_DriverLicenseState.EditValue.ToString(); }
            set { lue_DriverLicenseState.EditValue = value; }
        }

        public string LocationReferences
        {
            get { return Utils.GetEdtValue(edt_Highway); }
            set { edt_Highway.EditValue = value; }
        }

        public string TruckNumber
        {
            get { return Utils.GetEdtValue(edt_TruckNumber); }
            set { edt_TruckNumber.EditValue = value; }
        }

        public bool TruckDamages
        {
            get { return (bool)ckedt_truckDamages.EditValue; }
            set { ckedt_truckDamages.EditValue = value; }
        }

        public bool TruckCanMove
        {
            get { return (bool)ckedt_TruckCanMove.EditValue; }
            set { ckedt_TruckCanMove.EditValue = value; }
        }

        public bool TruckNeedCrane
        {
            get { return (bool)ckedt_TruckNeedCrane.EditValue; }
            set { ckedt_TruckNeedCrane.EditValue = value; }
        }

        public string TrailerNumber
        {
            get { return edt_TrailerNumber.EditValue.ToString(); }
            set { edt_TrailerNumber.EditValue = value; }
        }

        public bool TrailerDamages
        {
            get { return (bool)ckedt_TrailerDamages.EditValue; }
            set { ckedt_TrailerDamages.EditValue = value; }
        }

        public bool TrailerCanMove
        {
            get { return (bool)ckedt_TrailerCanMove.EditValue; }
            set { ckedt_TrailerCanMove.EditValue = value; }
        }

        public bool TrailerNeedCrane
        {
            get { return (bool)ckedt_TrailerNeedCrane.EditValue; }
            set { ckedt_TrailerNeedCrane.EditValue = value; }
        }

        public string CargoType
        {
            get { return Utils.GetEdtValue(edt_Cargo); }
            set { edt_Cargo.EditValue = value; }
        }

        public bool CargoSpill
        {
            get { return (bool)ckedt_Spill.EditValue; }
            set { ckedt_Spill.EditValue = value; }
        }

        public string ManifestNumber
        {
            get { return Utils.GetEdtValue(edt_manifest); }
            set { edt_manifest.EditValue = value; }
        }

        public string Broker
        {
            get { return Utils.GetEdtValue(edt_Broker); }
            set { edt_Broker.EditValue = value; }
        }

        public string Broker2
        {
            get { return Utils.GetEdtValue(edt_Broker2); }
            set { edt_Broker2.EditValue = value; }
        }

        public string IncidentDate
        {
            get { return dte_IncidentDate.DateTime.Date.ToString(); }
            set { dte_IncidentDate.EditValue = value; }
        }

        public string IncidentTime
        {
            get { return tme_IncidentTime.Time.ToString(); }
            set { tme_IncidentTime.EditValue = value; }
        }

        public bool PoliceReport
        {
            get { return (bool)ckedt_PoliceReport.EditValue; }
            set { ckedt_PoliceReport.EditValue = value; }
        }

        public string CitationReportNumber
        {
            get { return Utils.GetEdtValue(edt_PoliceReport); }
            set { edt_PoliceReport.EditValue = value; }
        }

        public string Latitude
        {
            get { return Utils.GetEdtValue(edt_Latitude); }
            set { edt_Latitude.EditValue = value; }
        }

        public string Longitude
        {
            get { return Utils.GetEdtValue(edt_Longitude); }
            set { edt_Longitude.EditValue = value; }
        }

        public string ID_State
        {
            get { return lue_LocationStates.EditValue.ToString(); }
            set { lue_LocationStates.EditValue = value; }
        }

        public string ID_City
        {
            get { return lue_LocationCities.EditValue.ToString(); }
            set { lue_LocationCities.EditValue = value; }
        }

        public string Comments
        {
            set { memoEdit1.Text = value; }
        }
        #endregion

        private void ViewIncidentDetails_Load(object sender, EventArgs e)
        {
            lue_DriverLicenseState.Properties.DataSource = Functions.getStates();
            lue_LocationStates.Properties.DataSource = Functions.getStates();

            gMapControl1.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gMapControl1.Position = new GMap.NET.PointLatLng(_controller.latitude, _controller.longitude);
            gMapControl1.ShowCenter = false;

            GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
            GMap.NET.WindowsForms.GMapMarker marker = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
                                                            new GMap.NET.PointLatLng(_controller.latitude, _controller.longitude),
            GMap.NET.WindowsForms.Markers.GMarkerGoogleType.red_dot);
            markers.Markers.Add(marker);
            gMapControl1.Overlays.Add(markers);
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            _controller.PDF();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            _controller.PDF();
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            _controller.SendEmail();
        }

        private void labelControl11_Click(object sender, EventArgs e)
        {

        }

        public void OnStateEditValueChanged(object sender, EventArgs e)
        {
            _controller.GetCitiesByState();
        }

        private void lue_MailDirectoryCategories_EditValueChanged(object sender, EventArgs e)
        {
            _controller.GetMailsByCategory();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowDriverIncident_Load(object sender, EventArgs e)
        {
            try
            {
                gMapControl1.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
                GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
                gMapControl1.Position = new GMap.NET.PointLatLng(_controller.latitude, _controller.longitude);
                gMapControl1.ShowCenter = false;

                GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
                GMap.NET.WindowsForms.GMapMarker marker = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
                                                                new GMap.NET.PointLatLng(_controller.latitude, _controller.longitude),
                GMap.NET.WindowsForms.Markers.GMarkerGoogleType.red_dot);
                gMapControl1.Overlays.Clear();
                markers.Markers.Add(marker);
                gMapControl1.Overlays.Add(markers);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}