using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Controllers.Incidents;
using ResponseEmergencySystem.Models.Documents;
using ResponseEmergencySystem.Properties;
using ResponseEmergencySystem.Services;
using ResponseEmergencySystem.Views.Incidents;
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
    public partial class DriverIncident : DevExpress.XtraEditors.XtraForm, IIncidentView
    {
        DriverIncidentController _controller;
        private List<Models.Documents.DocumentCapture> _docs;
        List<Models.Capture> _captures;
        bool isNew = false;
        bool isShow = false;
        string ID_Incident = "";

        public DriverIncident(string view = "edit")
        {
            InitializeComponent();

            switch(view)
            {
                case "show":
                    pnl_DriversSamsara.Visible = false;
                    pnl_InvolvedPersonsCapture.Visible = false;
                    pnl_CapturesHeader.Visible = false;
                    lue_Trucks.ReadOnly = true;
                    btn_Close.Location = new Point((pnl_Footer.Width - btn_Close.Width) / 2, btn_Close.Location.Y);
                    btn_Save.Visible = false;
                    ckedt_SaveAndSend.Visible = false;
                    col_Delete.Visible = false;
                    col_Edit.Visible = false;
                    isShow = true;
                    break;
                case "add":
                    pnl_Header.Visible = true;
                    ckedt_SaveAndSend.Visible = true;
                    isNew = true;
                    lbl_Folio.Visible = false;
                    pnl_PDFControls.Visible = false;
                    break;
            }
        }

        private void SaveEmptyIncident()
        {
            var response = IncidentService.CreateEmptyIncident();
            ID_Incident = response.ID;
        }

        #region view methods
        public void ViewClose()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
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
        #endregion

        #region View Properties
        public string Folio
        {
            set { lbl_Folio.Text = value; }
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
        public object TruckNumber 
        { 
            get { return lue_Trucks.Text; }
            set { lue_Trucks.EditValue = value; }
        }
        public string TruckId
        {
            get { return lue_Trucks.EditValue == null ? "" : lue_Trucks.EditValue.ToString(); }
            set { lue_Trucks.EditValue = value; }
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
        public DateTime IncidentDate 
        {
            get { return new DateTime(dte_IncidentDate.DateTime.Ticks); }
            set
            {
                dte_IncidentDate.DateTime = value;
                tme_IncidentTime.Time = value;
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
            get { return lue_states.EditValue.ToString(); }
            set { lue_states.EditValue = value; }
        }
        public string ID_City 
        {
            get { return lue_Cities.EditValue.ToString(); }
            set { lue_Cities.EditValue = value; }
        }
        public string LocationReferences 
        {
            get { return Utils.GetEdtValue(edt_Highway); }
            set { edt_Highway.EditValue = value; } 
        }
        public object Comments 
        {
            get { return memoEdit1.EditValue; }
            set { memoEdit1.EditValue = value; }
        }
        #endregion

        #region view involved persons
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
        public string IPPhoneNumber
        {
            get { return Utils.GetEdtValue(edt_IPPhoneNumber); }
            set { edt_IPPhoneNumber.EditValue = value; }
        }
        public string IPAge
        {
            get { return Utils.GetEdtValue(edt_IPAge); }
            set { edt_IPAge.EditValue = value; }
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
        public string IPDriverLicense
        {
            get { return Utils.GetEdtValue(edt_IPLicense); }
            set { edt_IPLicense.EditValue = value; }
        }

        public string IPHospital
        {
            get { return Utils.GetEdtValue(edt_IPHospital); }
            set { edt_IPHospital.EditValue = value; }
        }

        public string IPComments
        {
            get { return medt_IPComments.EditValue == null ? "" : medt_IPComments.EditValue.ToString(); }
            set { medt_IPComments.EditValue = value; }
        }

        public object InvolvedPersonsDataSource
        {
            set { gc_InvolvedPersons.DataSource = value; }
        }
        #endregion

        #region involved persons inputs properties
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
            get { return pic_FullNameWarning.Visible; }
            set { pic_FullNameWarning.Visible = value; }
        }

        public bool EdtLastName1ShowWarningIcon
        {
            get { return pic_LastName1Warning.Visible; }
            set { pic_LastName1Warning.Visible = value; }
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

        public bool BtnAddInvolvedPersonVisibility
        {
            set { btn_AddInvolvedPerson.Visible = value; }
        }

        public Point BtnEditInvolvedPersonLocation
        {
            set { simpleButton6.Location = value; }
            get { return simpleButton6.Location; }
        }

        public bool BtnEditInvolvedPersonVisibility
        {
            set { simpleButton6.Visible = value; }
        }

        //public bool PnlDriverInvolvedVisibility
        //{
        //    set { pnl_DriverInvolved.Visible = value; }
        //}
        #endregion

        #region involved persons methods
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

        public bool LblEmptyFieldsVisibility
        {
            set { lbl_EmptyFields.Visible = value; }
        }

        private void btn_EditPersonOnClick(object sender, EventArgs e)
        {
            _controller.EditInvolvedPersonByRow(gv_InvolvedPersons.FocusedRowHandle);
        }
        #endregion

        #region Utils on this view
        private void Ckedt_OnValueChanged(object sender, EventArgs e)
        {
            CheckEdit cb = (CheckEdit)sender;

            _controller.CheckEditChanged(cb.Name, (bool)cb.EditValue);
        }
        #endregion

        #region Captures methods
        public void LoadDocuments()
        {
            //_docs = new List<Models.Documents.DocumentCapture>();
            var docmentsByCaptureType = _docs.Select(dc => new { dc.CaptureType }).ToList();
            _captures = capturesTypesAvaible();
            gc_DocumentCaptures.DataSource = docmentsByCaptureType;
            if (!simpleButton10.Visible && docmentsByCaptureType.Count > 0)
                simpleButton10.Visible = true;
        }

        private List<Models.Capture> capturesTypesAvaible()
        {
            var captures = CaptureService.list_CaptureTypes();

            foreach (var currentCapture in _docs)
            {
                var count = captures.Where(c => c.ID_CaptureType == currentCapture.ID_CaptureType.ToUpper()).Count();
                if (count > 0)
                {
                    captures.RemoveAll(c => c.ID_CaptureType == currentCapture.ID_CaptureType.ToUpper());
                }
            }

            return captures;
        }

        private void gc_DocumentCaptures_DoubleClick(object sender, EventArgs e)
        {
            int idx = gv_DocumentCaptures.GetFocusedDataSourceRowIndex();
            //gc_Documents.DataSource = _docs[idx].documents;
            xtraScrollableControl1.Controls.Clear();
            CreatePanel(4, _docs[idx].documents);
        }

        private Models.Documents.Document UploadDocument(string ID_Capture, bool locked, int idx = 0, string name = "", string id = "", bool created = true)
        {
            Modals.DocumentModal addDocument;
            Models.Documents.Document doc = new Models.Documents.Document("", idx, id);
            doc.name = name;
            doc.Update("staged");
            
            if (locked)
                doc.SetLocked();
            
            //checkPath(doc);

            if (doc.Status == "disposed")
            {
                return doc;
            }

            return addDocumentView();

            Models.Documents.Document addDocumentView()
            {

                addDocument = new Modals.DocumentModal(ref doc, ID_Capture);
                if (addDocument.ShowDialog() == DialogResult.OK)
                {
                    if (!created)
                        doc.SetStatus("updated");
                    else
                        doc.SetStatus("created");

                    doc.SetImage(true);
                    return addDocument.doc;

                }
                else
                {
                    doc.SetStatus("disposed");
                    return doc;
                }
            }

        }

        public Models.Documents.Document checkPath(Models.Documents.Document doc)
        {
            foreach (var documentCapture in _docs)
            {
                foreach (var document in documentCapture.documents)
                {
                    if (doc.Path == "")
                        continue;

                    if (System.IO.Path.GetFileName(document.Path) == System.IO.Path.GetFileName(doc.Path))
                    {
                        //Utils.ShowMessage("This file has already been uploaded", "File error", type: "Warning");
                        doc.SetStatus("disposed");
                        return doc;
                    }
                }
            }

            return doc;

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
                pnl.Size = new Size(242, 249);
                pnl.Location = new Point(x, 18);
                pnl.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;

                PictureEdit pic = new PictureEdit();
                pic.Location = new Point(12, 5);
                pic.Size = new Size(218, 162);
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
                edt.Location = new Point(12, 202);
                edt.Size = new Size(199, 24);
                edt.Text = "Caption of the police report";

                LabelControl lbl = new LabelControl();
                lbl.AutoSizeMode = LabelAutoSizeMode.None;
                lbl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                lbl.Location = new Point(12, 173);
                lbl.Size = new Size(218, 24);
                lbl.Text = documents[i].name;


                MyButton btnView = new MyButton();
                btnView.ImageOptions.SvgImage = Resources.EyeBlue;
                btnView.ImageOptions.SvgImageSize = new Size(35, 35);
                btnView.ImageOptions.ImageToTextAlignment = ImageAlignToText.TopCenter;
                btnView.Size = new Size(36, 34);
                btnView.Location = isShow ? new Point(107, 203) : new Point(59, 203);
                btnView.btnIdx = i;
                btnView.Click += (object sender, EventArgs e) =>
                {
                    string firebaseUrl = _docs[gv_DocumentCaptures.FocusedRowHandle].documents[btnView.btnIdx].FirebaseUrl;
                    string localUrl = _docs[gv_DocumentCaptures.FocusedRowHandle].documents[btnView.btnIdx].Path;
                    string status = _docs[gv_DocumentCaptures.FocusedRowHandle].documents[btnView.btnIdx].Status;
                    string type = _docs[gv_DocumentCaptures.FocusedRowHandle].documents[btnView.btnIdx].Type;
                    _controller.EditImageView(status == "created" || status == "updated" ? localUrl : firebaseUrl, type, status != "created" && status != "updated");
                };

                MyButton btnEdit = new MyButton();
                btnEdit.ImageOptions.SvgImage = Resources.actions_edit;
                btnEdit.ImageOptions.ImageToTextAlignment = ImageAlignToText.TopCenter;
                btnEdit.Size = new Size(36, 34);
                btnEdit.Location = new Point(107, 203);
                btnEdit.btnIdx = i;
                if (isShow)
                    btnEdit.Visible = false;
                btnEdit.Click += (object sender, EventArgs e) =>
                {
                    string id = _docs[gv_DocumentCaptures.FocusedRowHandle].documents[btnEdit.btnIdx].ID_Document;
                    string ID_Capture = _docs[gv_DocumentCaptures.FocusedRowHandle].ID_Capture;
                    bool locked = _docs[gv_DocumentCaptures.FocusedRowHandle].documents[btnEdit.btnIdx].locked;
                    string status = _docs[gv_DocumentCaptures.FocusedRowHandle].documents[btnEdit.btnIdx].Status;
                    Models.Documents.Document doc = UploadDocument(ID_Capture, locked, (sender as MyButton).btnIdx, _docs[gv_DocumentCaptures.FocusedRowHandle].documents[btnEdit.btnIdx].name, id, status == "empty");

                    if (doc.Status == "disposed")
                        return;

                    if (doc.Status == "updated" || doc.Status == "created")
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
                btnDelete.Location = new Point(154, 203);
                btnDelete.btnIdx = i;
                if (isShow)
                    btnDelete.Visible = false;
                if (_docs[gv_DocumentCaptures.FocusedRowHandle].documents[i].locked)
                    btnDelete.Enabled = false;
                btnDelete.Click += (object sender, EventArgs e) =>
                {
                    if (_docs[gv_DocumentCaptures.FocusedRowHandle].documents[btnDelete.btnIdx].Status == "loaded")
                    {
                        _docs[gv_DocumentCaptures.FocusedRowHandle].Status = "updated";
                    }

                    _docs[gv_DocumentCaptures.FocusedRowHandle].documents[btnDelete.btnIdx].SetStatus("deleted");
                    xtraScrollableControl1.Controls.Clear();
                    CreatePanel(0, _docs[gv_DocumentCaptures.FocusedRowHandle].documents);
                };

                ProgressBarControl pbrProgress = new ProgressBarControl();
                pbrProgress.Size = new Size(218, 18);
                pbrProgress.Location = new Point(12, 176);
                pbrProgress.Visible = false;

                pnl.Controls.Add(pic);
                pnl.Controls.Add(lbl);
                pnl.Controls.Add(btnView);
                pnl.Controls.Add(btnEdit);
                pnl.Controls.Add(btnDelete);
                //pnl.Controls.Add(pbrProgress);

                xtraScrollableControl1.Controls.Add(pnl);
            }

        }
        #endregion

        public object LueCitiesDataSource 
        { 
            set { lue_Cities.Properties.DataSource = value; } 
        }
        public object DriversDataSource 
        {
            set { lue_Drivers.Properties.DataSource = value; }
        }
        public object TrucksDataSource 
        { 
            set { lue_Trucks.Properties.DataSource = value; }
        }

        public Point BtnAddInvolvedPersonLocation { set => throw new NotImplementedException(); }

        #region Mailining
        public bool SendToAllRecipientsInTheCategory
        {
            get { return (bool)ckedt_MailByCategory.EditValue; }
        }

        public string SelectedMail
        {
            get { return lue_MailDirectory.EditValue == null ? "" : lue_MailDirectory.EditValue.ToString(); }
        }

        public string MailDirectoryCategory
        {
            get { return lue_MailDirectoryCategories.EditValue == null ? "" : lue_MailDirectoryCategories.EditValue.ToString(); }
        }

        public object MailDirectoryCategoriesDataSource
        {
            set { lue_MailDirectoryCategories.Properties.DataSource = value; }
        }

        public object MailDirectoryDataSource
        {
            set { lue_MailDirectory.Properties.DataSource = value; }
        }

        public bool SendAfterSave
        {
            get { return (bool)ckedt_SaveAndSend.EditValue; }
        }

        public BorderStyles MailValidationBorder
        {
            get { return lue_MailDirectory.BorderStyle; }
            set { lue_MailDirectory.BorderStyle = value; }
        }


        public BorderStyles CategoryValidationBorder
        {
            get { return lue_MailDirectoryCategories.BorderStyle; }
            set { lue_MailDirectoryCategories.BorderStyle = value; }
        }

        private void lue_MailDirectoryCategories_EditValueChanged(object sender, EventArgs e)
        {
            _controller.GetMailsByCategory();
        }
        #endregion

        private void Incident_Load(object sender, EventArgs e)
        {
            if(!isNew)
            {
                _controller.LoadIncident();

                LoadDocuments();

                var docsTypes = _docs.Select(dc => new { dc.CaptureType }).ToList();
                gc_DocumentCaptures.DataSource = docsTypes;
                gv_DocumentCaptures.BestFitColumns();
            }

            if (isNew)
            {
                _docs = new List<DocumentCapture>();
                LoadDocuments();
            }

            if (!isShow)
            {
                btn_AddInvolvedPerson.Location = new Point((pnl_InvolvedPersonsCapture.Width - btn_AddInvolvedPerson.Width) / 2, btn_AddInvolvedPerson.Location.Y);
                simpleButton6.Location = new Point((pnl_InvolvedPersonsCapture.Width - simpleButton6.Width) / 2, simpleButton6.Location.Y);
            }

            gv_InvolvedPersons.BestFitColumns();

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

        private void gridLookUpEdit1_Properties_EditValueChanged(object sender, EventArgs e)
        {
            GridLookUpEdit view = (GridLookUpEdit)sender;
            _controller.GetDriver(view.EditValue.ToString());
        }


        public void SetController(DriverIncidentController controller)
        {
            _controller = controller;
        }

        public void LoadIncident()
        {
            throw new NotImplementedException();
        }

        public void LoadStates(DataTable dt_States)
        {
            lue_states.Properties.DataSource = dt_States;
            lue_DriverLicenseState.Properties.DataSource = dt_States;
        }

        public void OnStateEditValueChanged(object sender, EventArgs e)
        {
            _controller.GetCitiesByState();
        }

        public void LoadInjuredPersons(DataTable dt_InjuredPersons)
        {
            throw new NotImplementedException();
        }

        private void btn_Broker1_Click(object sender, EventArgs e)
        {
            _controller.GetBroker();
        }

        private void btn_Broker2_Click(object sender, EventArgs e)
        {
            _controller.GetBroker2();
        }

        private void simpleButton16_Click(object sender, EventArgs e)
        {
            if (dxValidationProvider1.Validate())
            {
                splashScreenManager1.ShowWaitForm();
                if (isNew)
                {
                    _controller.AddIncident();
                }
                else
                {
                    _controller.Update();
                }

                splashScreenManager1.CloseWaitForm();


                //if ((bool)ckedt_SaveAndSend.EditValue)
                //{
                //    bool sended = _controller.SendEmail();
                //    if (sended)
                //    {

                //        //splashScreenManager1.CloseWaitForm();
                //        xtraScrollableControl1.Controls.Clear();
                //        _controller.Update();
                //        this.DialogResult = DialogResult.OK;

                //    }
                //    else
                //    {
                //        //splashScreenManager1.CloseWaitForm();
                //    }
                //}
                //else
                //{

                //    xtraScrollableControl1.Controls.Clear();
                //    _controller.Update();

                //}


            }
            else
                Utils.ShowMessage("Please Check the information again", "Validation Error", type: "Warning");
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

        private void lue_Trucks_KeyDown(object sender, KeyEventArgs e)
        {
            GridLookUpEdit edit = sender as GridLookUpEdit;
            if (e.KeyData == Keys.Down || e.KeyData == Keys.Up)
            {
                if (!edit.IsPopupOpen)
                {
                    e.Handled = true;
                }
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_AddInvolvedPerson_Click(object sender, EventArgs e)
        {
            _controller.AddPersonInvolved();
            gv_InvolvedPersons.BestFitColumns();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            _controller.UpdatePersonInvolved();
            gv_InvolvedPersons.BestFitColumns();
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            if (_captures.Count > 0)
            {
                AddMoreCaptures AddMoreCaptures = new AddMoreCaptures();
                Controllers.Captures.AddCapturesController addCapturesCtrl = new Controllers.Captures.AddCapturesController(AddMoreCaptures, _captures);
                addCapturesCtrl.LoadCaptures();

                addCapturesCtrl.SetIncidentId(Guid.Empty.ToString());
                if (AddMoreCaptures.ShowDialog() == DialogResult.OK)
                {
                    var capture = addCapturesCtrl.GetDocuments();

                    //for(var i = 0; i < capture.documents.Count; i++)
                    //{
                    //    var doc = capture.documents[i];
                    //    if (checkPath(doc).Status == "disposed")
                    //    {
                    //        capture.documents[i].SetStatus("created");
                    //        capture.documents[i].Path = "";
                    //    }
                        
                    //}

                    _docs.Add(capture);
                    var docsType = _docs.Select(dc => new { dc.CaptureType, dc.ID_Capture });
                    _captures.RemoveAll(c => c.ID_CaptureType == capture.ID_CaptureType.ToUpper());
                    gc_DocumentCaptures.DataSource = docsType;
                    gv_DocumentCaptures.BestFitColumns();

                    if (!simpleButton10.Visible)
                        simpleButton10.Visible = true;
                }
            }
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            string ID_Capture = _docs[gv_DocumentCaptures.FocusedRowHandle].ID_Capture;


            //gv_Documents.BestFitColumns();
            xtraScrollableControl1.Controls.Clear();
            Models.Documents.Document doc = UploadDocument(ID_Capture, false);

            if (doc.Status == "disposed")
            {
                CreatePanel(4, _docs[gv_DocumentCaptures.FocusedRowHandle].documents);
            }


            if (doc.Status == "modified" || doc.Status == "created")
            {
                _docs[gv_DocumentCaptures.FocusedRowHandle].documents.Add(doc);
                CreatePanel(4, _docs[gv_DocumentCaptures.FocusedRowHandle].documents);

            }

            if (doc.Status == "loaded" || doc.Status == "empty")
            {
                _docs[gv_DocumentCaptures.FocusedRowHandle].documents.Add(doc);
                _docs[gv_DocumentCaptures.FocusedRowHandle].Status = "updated";
                CreatePanel(4, _docs[gv_DocumentCaptures.FocusedRowHandle].documents);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            _controller.SendEmail();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            _controller.PDF();
        }

        private void btn_DeletePerson_Click(object sender, EventArgs e)
        {
            int idx = gv_InvolvedPersons.FocusedRowHandle;
            _controller.RemoveInvolvedPersonByRow(idx);
            gv_InvolvedPersons.BestFitColumns();
        }

        private void lue_MailDirectory_Properties_EditValueChanged(object sender, EventArgs e)
        {
            _controller.validate("selectedMail");
        }

        private void lue_MailDirectoryCategories_Properties_EditValueChanged(object sender, EventArgs e)
        {
            _controller.validate("selectedCategory");
        }
    }
}