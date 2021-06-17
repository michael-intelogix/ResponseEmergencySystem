using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Properties;
using ResponseEmergencySystem.Controllers;
using ResponseEmergencySystem.Forms.Modals;
using ResponseEmergencySystem.Services;
using ResponseEmergencySystem.Views;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.IO;
using ResponseEmergencySystem.Models;
using DevExpress.XtraEditors.Controls;

using System.Data.SQLite;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using DevExpress.Utils.Menu;

namespace ResponseEmergencySystem.Forms
{
    
    public partial class AddIncidentDetails : XtraForm
    {
        // driver E2E7FBBB-6BF8-414A-B160-1A4EE294DC97
        // driver2 C7B06EF3-869B-4212-A1EC-7820B2D17CA4

        public AddIncidentDetails()
        {
            InitializeComponent();
        }

        private DataTable dt_InjuredPersons;
        private List<Models.Documents.DocumentCapture> _docs;

        

        Controllers.Incidents.AddIncidentController _controller;

        private void btn_AddRowsClick(object sender, EventArgs e)
        {
            //Int16 currentRow = 0;
            //while (currentRow < Convert.ToInt16(edt_NumberOfInjured.EditValue))
            //{
            //    _controller.addEmptyRow();
            //    currentRow++;
            //}

        }

        private void btn_DeleteRowClick(object sender, EventArgs e)
        {
            //Int32 index = gv_InjuredPersons.FocusedRowHandle;

            //DialogResult result = MessageBox.Show(
            //    "Are you sure you want to delete this row?", 
            //    "Delete injured person row", 
            //    MessageBoxButtons.OKCancel, 
            //    MessageBoxIcon.Information);
            //if (result.Equals(DialogResult.OK))
            //{
            //    gv_InjuredPersons.DeleteRow(index);
            //}
        }

        private void IncidentCapture_Load(object sender, EventArgs e)
        {
            _docs = new List<Models.Documents.DocumentCapture>();

            gMapControl1.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gMapControl1.Position = new GMap.NET.PointLatLng(36.05948, -102.51325);
            gMapControl1.ShowCenter = false;

            lue_Reason.Properties.DataSource = constants.reasons;

            lue_Actions.Properties.DataSource = constants.actions;
            //toolTipController1.ShowHint("HIIII");
        }

        private void btn_AddIncident_Click(object sender, EventArgs e)
        {

            if (dxValidationProvider1.Validate())
            {
                splashScreenManager1.ShowWaitForm();
                _controller.AddIncident();
                splashScreenManager1.CloseWaitForm();

                this.DialogResult = DialogResult.OK;
            }
            else
                Utils.ShowMessage("Please Check the information again", "Validation Error", type: "Error");

        }
       
        private void AddIncidentDetails_Shown(object sender, EventArgs e)
        { 
        }
        
        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            _controller.SetBroker();
        }


        #region view interface methods
        public void SetController(Controllers.Incidents.AddIncidentController controller)
        {
            _controller = controller;
        }

        public void LoadIncident(Incident incident)
        {
        }

        public void LoadStates(DataTable dt_States)
        {
            lue_StateExp.Properties.DataSource = dt_States;
            lue_DriverLicenseState.Properties.DataSource = dt_States;
        }


        public void LoadCities(DataTable dt_Cities)
        {
            lue_Cities.Properties.DataSource = dt_Cities;
        }

        public void LoadInjuredPersons(DataTable dt_InjuredPersons)
        {
        }
        #endregion

        #region events needed
        public void checkNumber_OnEdtKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                TextEdit edt_Number = (TextEdit)sender;
                _controller.CheckNumber(edt_Number.Name);
            }
        }

        public void checkNumber_OnEdtLeave(object sender, EventArgs e)
        {
            TextEdit edt_Number = (TextEdit)sender;
            _controller.CheckNumber(edt_Number.Name);
        }

        public void FindTruckSamsara_Click(object sender, EventArgs e)
        {
            if(Utils.GetEdtValue(edt_TruckNumber2) == "" || lbl_TruckExists.Visible)
            {
                gMapControl1.Position = new GMap.NET.PointLatLng(36.05948, -102.51325);
                Utils.ShowMessage("There is no truck to find, Please check\n the information again", "Samsara Error", type: "Error");
            }
            else
            {
                splashScreenManager1.ShowWaitForm();
                var res = _controller.GetTruckSamsara();
                gMapControl1.Position = new GMap.NET.PointLatLng(res[0], res[1]);

                GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
                GMap.NET.WindowsForms.GMapMarker marker = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
                                                                new GMap.NET.PointLatLng(res[0], res[1]),
                GMap.NET.WindowsForms.Markers.GMarkerGoogleType.red_dot);
                gMapControl1.Overlays.Clear();
                markers.Markers.Add(marker);
                gMapControl1.Overlays.Add(markers);
                splashScreenManager1.CloseWaitForm();
            }
            
        }

        public void Ckedt_OnValueChanged(object sender, EventArgs e)
        {
            CheckEdit cb = (CheckEdit)sender;

            _controller.CheckEditChanged(cb.Name, (bool)cb.EditValue);
        }

        public void OnStateEditValueChanged(object sender, EventArgs e)
        {
            _controller.GetCitiesByState();
        }

        #endregion

        #region form inputs
        public string DriverInfoSearch
        {
            get { return Utils.GetEdtValue(edt_SearchDriver); }
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
            set 
            {
                if (value == DateTime.Now)
                    dte_ExpirationDate.EditValue = null;
                else
                    dte_ExpirationDate.EditValue = value; 
            }
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
            get { return lue_Trucks.Text; }
            set { edt_TruckNumber2.EditValue = value; }
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
            get { return Utils.GetEdtValue(edt_TrailerNumber); }
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

        public DateTime IncidentDate {
            get 
            {
                DateTime date = Utils.GetDateTime(dte_IncidentDate.DateTime.Date, tme_IncidentTime.Time.Hour, tme_IncidentTime.Time.Minute);
                return date;
            } 
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
            get { return lue_StateExp.EditValue.ToString(); }
            set { lue_StateExp.EditValue = value; }
        }

        public string ID_City
        {
            get { return lue_Cities.EditValue.ToString(); }
            set { lue_Cities.EditValue = value; }
        }

        public string Comments
        {
            get { return memoEdit1.EditValue == null ? "" : memoEdit1.EditValue.ToString(); }
        }
        #endregion

        #region involved persons 
        public string IPFullName 
        { 
            get { return Utils.GetEdtValue(edt_IPFullName); } 
            set { edt_IPFullName.EditValue = value; }
        }
        public string IPLastName1 
        { 
            get { return Utils.GetEdtValue(edt_IPLastName1); }
            set { edt_IPLastName1.EditValue = value; }
        }
        //not in use
        public string IPLastName2 
        {
            get { return Utils.GetEdtValue(edt_IPLastName1); }
        }
        public string IPAge 
        { 
            get { return Utils.GetEdtValue(edt_IPAge); }
            set { edt_IPAge.EditValue = value; }
        }
        public string IPPhoneNumber 
        { 
            get { return Utils.GetEdtValue(edt_IPPhoneNumber); }
            set { edt_IPPhoneNumber.EditValue = value; }
        }
        public bool IPPassenger
        {
            get { return (bool)ckedt_IPPassenger.EditValue; }
            set { ckedt_IPPassenger.EditValue = value; }
        }
        public bool IPDriver 
        { 
            get { return (bool)ckedt_IPDriver.EditValue; }
            set { ckedt_IPDriver.EditValue = value; }
        }
        public string IPDriverLicense { 
            get { return Utils.GetEdtValue(edt_IPLicense); }
            set { edt_IPLicense.EditValue = value; }
        }
        public bool IPPrivate 
        { 
            get { return (bool)ckedt_IPPrivate.EditValue; }
            set { ckedt_IPPrivate.EditValue = value; }
        }
        public bool IPInjured 
        {
            get { return (bool)ckedt_IPInjured.EditValue; }
            set { ckedt_IPInjured.EditValue = value; }
        }

        public string IPHospital
        {
            get { return Utils.GetEdtValue(edt_IPHospital); }
            set { edt_IPHospital.EditValue = value; }
        }

        public string IPComments
        {
            get { return Utils.GetEdtValue(edt_IPComments); }
            set { edt_IPComments.EditValue = value; }
        }
        #endregion

        #region Form properties
        public bool PnlBolVisibility
        {
            set { pnl_BOL.Visible = value; }
        }

        public bool PnlPoliceReportVisibility
        {
            set { pnl_PoliceReport.Visible = value; }
        }

        public bool LblTruckExistsVisibility
        {
            set { lbl_TruckExists.Visible = value; }
        }

        public object LueCitiesDataSource
        {
            set { lue_Cities.Properties.DataSource = value; }
        }

        public object InvolvedPersonsDataSource
        {
            set { gc_InvolvedPersons.DataSource = value; }
        }

        public object DriversDataSource
        {
            set { lue_Drivers.Properties.DataSource = value; }
        }

        public object TrucksDataSource
        {
            set { lue_Trucks.Properties.DataSource = value; }
        }

        public bool PnlDriverInvolvedVisibility
        {
            set { pnl_DriverInvolved.Visible = value; }
        }

        public bool BtnAddInvolvedPersonVisibility
        {
            set { simpleButton5.Visible = value; }
        }

        public Point BtnAddInvolvedPersonLocation
        {
            set { simpleButton5.Location = value; }
        }

        public Size BtnAddInvolvedPersonSize
        {
            set { simpleButton5.Size = value; }
        }

        public bool BtnEditInvolvedPersonVisibility
        {
            set { simpleButton6.Visible = value; }
        }

        public Point BtnEditInvolvedPersonLocation
        {
            set { simpleButton6.Location = value; }
            get { return simpleButton6.Location; }
        }

        public bool LblEmptyFieldsVisibility
        {
            set { lbl_EmptyFields.Visible = value; }
        }

        public BorderStyles EdtFullNameBorder
        {
            get { return edt_IPFullName.BorderStyle; }
            set { edt_IPFullName.BorderStyle = value; }
        }

        public BorderStyles EdtLastNameBorder
        {
            get { return edt_IPLastName1.BorderStyle; }
            set { edt_IPLastName1.BorderStyle = value; }
        }

        public BorderStyles EdtPhoneNumberBorder
        {
            get { return edt_IPPhoneNumber.BorderStyle; }
            set { edt_IPPhoneNumber.BorderStyle = value; }
        }

        public BorderStyles EdtAgeBorder
        {
            get { return edt_IPAge.BorderStyle; }
            set { edt_IPAge.BorderStyle = value; }
        }

        public BorderStyles EdtLicenseBorder
        {
            get { return edt_IPLicense.BorderStyle; }
            set { edt_IPLicense.BorderStyle = value; }
        }

        public BorderStyles CkedtPassengerBorder
        {
            get { return ckedt_IPPassenger.BorderStyle; }
            set { ckedt_IPPassenger.BorderStyle = value; }
        }

        public BorderStyles CkedtDriverBorder
        {
            get { return ckedt_IPDriver.BorderStyle; }
            set { ckedt_IPDriver.BorderStyle = value; }
        }

        public bool EdtFullNameShowWarningIcon
        {
            set { pic_FullNameWarning.Visible = value; }
            get { return pic_FullNameWarning.Visible; }
        }

        public bool EdtLastName1ShowWarningIcon
        {
            set { pic_LastName1Warning.Visible = value; }
            get { return pic_LastName1Warning.Visible; }
        }

        public bool EdtPhoneNumberShowWarningIcon
        {
            set { pic_PhoneNumberWarning.Visible = value; }
        }

        public bool EdtAgeShowWarningIcon
        {
            set { pic_AgeWarning.Visible = value; }
        }


        public bool EdtLicenseShowWarningIcon
        {
            set { pic_LicenseWarning.Visible = value; }
        }


        public object DocumentCapturesDataSource
        {
            set { gc_DocumentCaptures.DataSource = value; }
        }
        #endregion

        #region truck exists
        public bool LblTrailerExistsVisibility
        {
            set { lbl_TrailerExists.Visible = value; }
        }

        public bool BtnAddTrailerVisibility
        {
            set { btn_AddTrailer.Visible = value; }
        }

        public bool EdtCommodityReadOnly
        {
            set { edt_Cargo.ReadOnly = value; }
        }
        #endregion

        #region documents
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

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            //gc_Documents.DataSource = _docs[gv_DocumentCaptures.FocusedRowHandle].documents;


            //gv_Documents.BestFitColumns();
            xtraScrollableControl1.Controls.Clear();
            Models.Documents.Document doc = UploadDocument();

            if (doc.Status == "disposed")
                return;

            if (doc.Status == "modified" || doc.Status == "created")
            {
                _docs[gv_DocumentCaptures.FocusedRowHandle].documents.Add(doc);
                _docs[gv_DocumentCaptures.FocusedRowHandle].Status = "updated";
                CreatePanel(4, _docs[gv_DocumentCaptures.FocusedRowHandle].documents);

            }
        }

        private Models.Documents.Document UploadDocument(int idx = 0, string name = "", string id = "", bool created = true)
        {
            Modals.DocumentModal addDocument;
            Models.Documents.Document doc = new Models.Documents.Document("", idx, id);
            doc.name = name;
            doc.Update("staged");
            return addDocumentView();

            Models.Documents.Document addDocumentView()
            {
                addDocument = new Modals.DocumentModal(doc);
                if (addDocument.ShowDialog() == DialogResult.OK)
                {
                    if (!created)
                        doc.SetStatus("updated");
                    else
                        doc.SetStatus("created");

                    return addDocument.doc;

                }
                else
                {
                    doc.SetStatus("disposed");
                    return doc;
                }


            }

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
                    pic.Properties.SizeMode = loadImage ? PictureSizeMode.Stretch : PictureSizeMode.Clip;
                    pic.Properties.ZoomPercent = 200;
                    pic.Properties.PictureAlignment = ContentAlignment.MiddleCenter;
                    pic.BackColor = Color.Transparent;
                    pic.BorderStyle = BorderStyles.NoBorder;
                }
                else
                {
                    pic.Image = documents[i].Image;
                    pic.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
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
                btnView.Location = new Point(49, 212);
                btnView.btnIdx = i;
                btnView.Click += (object sender, EventArgs e) =>
                {
                    string firebaseUrl = _docs[gv_DocumentCaptures.FocusedRowHandle].documents[btnView.btnIdx].FirebaseUrl;
                    string localUrl = _docs[gv_DocumentCaptures.FocusedRowHandle].documents[btnView.btnIdx].Path;
                    string status = _docs[gv_DocumentCaptures.FocusedRowHandle].documents[btnView.btnIdx].Status;
                    string type = _docs[gv_DocumentCaptures.FocusedRowHandle].documents[btnView.btnIdx].Type;
                    _controller.EditImageView(status == "created" ? localUrl : firebaseUrl, type, status != "created");
                };

                MyButton btnEdit = new MyButton();
                btnEdit.ImageOptions.SvgImage = Resources.actions_edit;
                btnEdit.ImageOptions.ImageToTextAlignment = ImageAlignToText.TopCenter;
                btnEdit.Size = new Size(36, 34);
                btnEdit.Location = new Point(103, 212);
                btnEdit.btnIdx = i;
                btnEdit.Click += (object sender, EventArgs e) =>
                {
                    string id = _docs[gv_DocumentCaptures.FocusedRowHandle].documents[btnEdit.btnIdx].ID_Document;
                    Models.Documents.Document doc = UploadDocument((sender as MyButton).btnIdx, _docs[gv_DocumentCaptures.FocusedRowHandle].documents[btnEdit.btnIdx].name, id, false);

                    if (doc.Status == "disposed")
                        return;

                    if (doc.Status == "updated")
                    {
                        _docs[gv_DocumentCaptures.FocusedRowHandle].documents[doc.ID] = doc;
                        _docs[gv_DocumentCaptures.FocusedRowHandle].Status = "updated";
                        xtraScrollableControl1.Controls.Clear();
                        CreatePanel(0, _docs[gv_DocumentCaptures.FocusedRowHandle].documents);
                    }

                };

                MyButton btnDelete = new MyButton();
                btnDelete.ImageOptions.SvgImage = Resources.delete;
                btnDelete.ImageOptions.ImageToTextAlignment = ImageAlignToText.TopCenter;
                btnDelete.Size = new Size(36, 34);
                btnDelete.Location = new Point(157, 212);
                btnDelete.btnIdx = i;
                btnDelete.Click += (object sender, EventArgs e) =>
                {
                    _docs[gv_DocumentCaptures.FocusedRowHandle].documents[btnDelete.btnIdx].SetStatus("deleted");
                    xtraScrollableControl1.Controls.Clear();
                    CreatePanel(0, _docs[gv_DocumentCaptures.FocusedRowHandle].documents);
                };

                pnl.Controls.Add(pic);
                pnl.Controls.Add(lbl);
                pnl.Controls.Add(btnView);
                pnl.Controls.Add(btnEdit);
                pnl.Controls.Add(btnDelete);

                xtraScrollableControl1.Controls.Add(pnl);
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
        #endregion

        private void ViewIncidentDetails_Load(object sender, EventArgs e)
        {
            lue_StateExp.Properties.DataSource = Functions.getStates();
        }

        private void btn_AddComments_Click(object sender, EventArgs e)
        {
            _controller.SetComments();
        }

        private void edt_Broker_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btn_FindDriver_Click(object sender, EventArgs e)
        {
            //_controller.GetDriver();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            _controller.SetBroker();
        }

        private void edt_SearchDriver_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                //_controller.GetDriver();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //Utils.ShowMessage("Are you sure you want to close?", "Incident");
            this.Close();
        }

        private void btn_EditPersonOnClick(object sender, EventArgs e)
        {
            _controller.EditInvolvedPersonByRow(gv_InvolvedPersons.FocusedRowHandle);
        }

        private void btn_RemovePersonOnClick(object sender, EventArgs e)
        {
            _controller.RemoveInvolvedPersonByRow(gv_InvolvedPersons.FocusedRowHandle);
            gv_InvolvedPersons.BestFitColumns();
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {

        }

        private void edt_Leave(object sender, EventArgs e)
        {
            TextEdit edt = (TextEdit)sender;

            _controller.validate(edt.Name);
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            //_controller.FindDriverInSamsara();
            SamsaraService.UpdateSamsaraDrivers();
        }

        private void lue_Drivers_EditValueChanged(object sender, EventArgs e)
        {
            LookUpEdit lue = (LookUpEdit)sender;
            _controller.GetDriver(lue.EditValue.ToString());
        }

        private void gridLookUpEdit1_Properties_EditValueChanged(object sender, EventArgs e)
        {
            GridLookUpEdit view = (GridLookUpEdit)sender;
            _controller.GetDriver(view.EditValue.ToString());
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {

            //AddMoreCaptures AddMoreCaptures = new AddMoreCaptures();
            //Controllers.Captures.AddCapturesController addCapturesCtrl = new Controllers.Captures.AddCapturesController(AddMoreCaptures, CaptureService.list_CaptureTypes());
            //addCapturesCtrl.LoadCaptures();
            ////addCapturesCtrl.LoadDocuments(_docs);
            //addCapturesCtrl.SetIncidentId(Guid.Empty.ToString());
            //if (AddMoreCaptures.ShowDialog() == DialogResult.OK)
            //{
            //    _docs.Add(addCapturesCtrl.GetDocuments());
            //    var docsType = _docs.Select(dc => new { dc.CaptureType, dc.ID_Capture });
            //    gc_DocumentCaptures.DataSource = docsType;
            //    gv_DocumentCaptures.BestFitColumns();
            //    //Utils.ShowMessage("the capture was added succesfully", "Capture");
            //    //_view.OpenSpinner();
            //    //_captures = CaptureService.list_Captures(_view.ID_Incident.ToString());
            //    //_view.CapturesDataSource = _captures;
            //    //if (_captures.Count > 0)
            //    //    _view.ImagesDatasSource = CaptureService.list_Images(_captures[0].ID_Capture.ToString());
            //    //else
            //    //    _view.ImagesDatasSource = new List<ImageCapture>();
            //    //_view.CloseSpinner();


            //    if (!simpleButton10.Visible)
            //        simpleButton10.Visible = true;
            //}
        }

        //private void gc_DocumentCaptures_DoubleClick(object sender, EventArgs e)
        //{
        //    int idx = gv_DocumentCaptures.GetFocusedDataSourceRowIndex();
        //    gc_Documents.DataSource = _docs[idx].documents;
        //}

        private void rpic_Image_Click(object sender, EventArgs e)
        {
            string imgPath = Utils.GetRowID(gv_Documents, "Path");
            if (imgPath == "")
            {
                splashScreenManager1.ShowWaitForm();
                var d = _controller.CheckDocument();
                if (d.Item1)
                {
                    _docs[gv_DocumentCaptures.FocusedRowHandle].documents[gv_Documents.FocusedRowHandle].Path = d.Item2;
                    _docs[gv_DocumentCaptures.FocusedRowHandle].documents[gv_Documents.FocusedRowHandle].Type = d.Item3;
                    _docs[gv_DocumentCaptures.FocusedRowHandle].documents[gv_Documents.FocusedRowHandle].SetImage();
                }
                splashScreenManager1.CloseWaitForm();
            }
            else
            {
                string fileType = Utils.GetRowID(gv_Documents, "Type");
                string path = _controller.EditImageView(imgPath, fileType);
                if (path != "")
                {
                    _docs[gv_DocumentCaptures.FocusedRowHandle].documents[gv_Documents.FocusedRowHandle].Path = path;
                    _docs[gv_DocumentCaptures.FocusedRowHandle].documents[gv_Documents.FocusedRowHandle].SetImage();
                }

            }
 
            gc_Documents.DataSource = _docs[gv_DocumentCaptures.FocusedRowHandle].documents;
            //gv_Documents.BestFitColumns();
        }

        private void gv_Documents_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                DXMenuItem item = new DXMenuItem("Change Document");
                item.Click += (o, args) => {
                    int row = this.gv_Documents.FocusedRowHandle;
                    splashScreenManager1.ShowWaitForm();
                    var d = _controller.CheckDocument();
                    if (d.Item1)
                    {
                        _docs[gv_DocumentCaptures.FocusedRowHandle].documents[row].Path = d.Item2;
                        _docs[gv_DocumentCaptures.FocusedRowHandle].documents[row].Type = d.Item3;
                        _docs[gv_DocumentCaptures.FocusedRowHandle].documents[row].SetImage();
                    }

                    gc_Documents.DataSource = _docs[gv_DocumentCaptures.FocusedRowHandle].documents;
                    //gv_Documents.BestFitColumns();
                    splashScreenManager1.CloseWaitForm();

                };
                e.Menu.Items.Add(item);
            }
        }

        private void simpleButton5_Click_1(object sender, EventArgs e)
        {
            _controller.AddPersonInvolved();
            gv_InvolvedPersons.BestFitColumns();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            _controller.UpdatePersonInvolved();
            gv_InvolvedPersons.BestFitColumns();
        }


        private void edt_CheckForErrors_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                TextEdit edt = (TextEdit)sender;
                _controller.validate(edt.Name);
                //_controller.GetDriver();
            }
        }

        private void edt_CheckForErrors_Leave(object sender, EventArgs e)
        {
            TextEdit edt = (TextEdit)sender;
            _controller.validate(edt.Name);
        }

        private void lue_Trucks_Properties_EditValueChanged(object sender, EventArgs e)
        {
           
        }

        private void lue_Trucks_Closed(object sender, ClosedEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            var res = _controller.GetTruckSamsara();
            gMapControl1.Position = new GMap.NET.PointLatLng(res[0], res[1]);

            GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
            GMap.NET.WindowsForms.GMapMarker marker = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
                                                            new GMap.NET.PointLatLng(res[0], res[1]),
            GMap.NET.WindowsForms.Markers.GMarkerGoogleType.red_dot);
            gMapControl1.Overlays.Clear();
            markers.Markers.Add(marker);
            gMapControl1.Overlays.Add(markers);
            //_controller.SetTruck("");
            splashScreenManager1.CloseWaitForm();
        }

        private void simpleButton14_Click(object sender, EventArgs e)
        {
            _controller.SetBroker();
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            _controller.SetBroker2();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            _controller.AddPersonInvolved();
            gv_InvolvedPersons.BestFitColumns();
        }

        private void simpleButton6_Click_1(object sender, EventArgs e)
        {
            _controller.UpdatePersonInvolved();
            gv_InvolvedPersons.BestFitColumns();
        }

        #region Logs
        private void lue_DriverLicenseState_EditValueChanged(object sender, EventArgs e)
        {
            _controller.SetLicenseStateChange();
        }
        #endregion
    }

}