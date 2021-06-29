using DevExpress.XtraEditors;
using Google.Cloud.Firestore;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Controllers;
using ResponseEmergencySystem.Models;
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

namespace ResponseEmergencySystem.Forms
{
    public partial class Main3 : DevExpress.XtraEditors.XtraForm, IMainView
    {
        public Main3()
        {
            InitializeComponent();
        }

        MainController _controller;

        #region Chat
        public string ChatText
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public string Message
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public MemoEdit chat => throw new NotImplementedException();

        public void Refresh_Chat(DocumentSnapshot docsnap)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Main Properties
        public object ID_Incident => gv_Incidents.GetFocusedRowCellValue("ID_Incident");

        public object ID_StatusDetail
        {
            get
            {
                var status = gv_Incidents.GetFocusedRowCellValue("ID_StatusDetail");
                return status;
            }
            set => gv_Incidents.SetFocusedRowCellValue("ID_StatusDetail", value);
        }

        public object ID_Capture 
        {
            get {
                gv_Captures.FocusedColumn = gv_Captures.VisibleColumns[0];
                return gv_Captures.GetFocusedRowCellValue("ID_Capture").ToString(); 
            }
        }

        public string ID_Image => gv_Images.GetFocusedRowCellValue("ID_Image").ToString();

        public string ImageName => gv_Images.GetFocusedRowCellValue("ImageName").ToString();

        public string LblFolio { set => lbl_Folio.Text = value; }

        public bool LblFolioVisibility { set => lbl_Folio.Visible = value; }

        public object DocumentType => gv_Images.GetFocusedRowCellValue("FileType");

        public object CaptureComments
        {
            get
            {
                return gv_Captures.GetFocusedRowCellValue("comments");
            }
        }

        public object ImageComments
        {
            get
            {
                return gv_Images.GetFocusedRowCellValue("comments");
            }
        }
        //ScaleData_Form.Scales_GridView.SetRowCellValue(ScaleData_Form.Scales_GridControl.AutoFilterRowHandle, ScaleData_Form.Scales_GridView.Columns("Ticket"), Me.Bar_Ticket.EditValue.ToString)
        // 049
        #endregion

        #region Filters
        public string Date1
        { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
        }
        public string Date2 
        { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); }
        public object Folio 
        { 
            get => gv_Incidents.GetFocusedRowCellValue("Folio"); 
            set => throw new NotImplementedException(); 
        }
        public string DriverName 
        { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
        }
        public object TruckNumber 
        { 
            get => gv_Incidents.GetFocusedRowCellValue("truck.truckNumber"); 
            set => throw new NotImplementedException(); 
        }

        public void SetGridFilters(string driver, string truck, string folio)
        {
            gv_Incidents.SetAutoFilterValue(col_DriverName, driver, DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains);
            gv_Incidents.SetAutoFilterValue(col_TruckNumber, truck, DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains);
            gv_Incidents.SetAutoFilterValue(col_Folio, folio, DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains);
        }

        public void ClearFilters()
        {
            gv_Incidents.ClearColumnsFilter();
        }
        #endregion

        #region DataSources
        public object Incidents 
        { 
            set => gc_Incidents.DataSource = value;
            get
            {
                gv_Incidents.FocusedColumn = gv_Incidents.VisibleColumns[0];
                return (List<Incident>)gv_Incidents.GridControl.DataSource;
            }
        }
        public object CapturesDataSource 
        { 
            set => gc_Captures.DataSource = value; 
        }
        public object ImagesDatasSource 
        {
            set => gc_Images.DataSource = value; 
        }
        public object StatusDetailDataSource 
        { 
            set
            {
                lue_StatusDetail.DataSource = value;
                lue_StatusDetail.BestFit();
            } 
        }
        
        #endregion

        #region Interface Methods
        public void OpenSpinner()
        {
            splashScreenManager1.ShowWaitForm();
        }

        public void CloseSpinner()
        {
            splashScreenManager1.CloseWaitForm();
        }

        public void SetController(MainController controller)
        {
            _controller = controller;
        }
        #endregion

        #region Form Methods
        private void Main3_Load(object sender, EventArgs e)
        {
            _controller.LoadData();
        }

        public void LblFolioPosition()
        {
            lbl_Folio.Location = new Point((panelControl1.Width - lbl_Folio.Width) / 2, (panelControl1.Height - lbl_Folio.Height) / 2);
        }

        public void TEST()
        {
            var dt = (List<Incident>)gv_Incidents.GridControl.DataSource;
           
            MessageBox.Show(dt.Count.ToString());
        }
        #endregion

        #region Incidents Methods
        private void ShowEditIncident(object sender, EventArgs e)
        {
            _controller.EditIncidentView();
        }
        #endregion

        private void btn_ViewIncident_Click(object sender, EventArgs e)
        {
            _controller.ShowIncident();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            _controller.EditImageData(false);
        }

        private void btn_EditCapture_Click(object sender, EventArgs e)
        {
            int idx = gv_Captures.FocusedRowHandle;
            _controller.EditImageData(true, idx);
        }

        private void btn_Picture_Click(object sender, EventArgs e)
        {
            string imgPath = Utils.GetRowID(gv_Images, "ImagePath");
            string fileType = Utils.GetRowID(gv_Images, "FileType");
            _controller.EditImageView(imgPath, fileType);
        }

        private void btn_SaveStatus_Click(object sender, EventArgs e)
        {
            _controller.AddLocation();
        }

        private void btn_CloseIncident_Click(object sender, EventArgs e)
        {
            _controller.CloseIncident();
        }

        private void gv_Incidents_DoubleClick(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            _controller.SetCaptures();
            splashScreenManager1.CloseWaitForm();
        }

        private void gv_Captures_DoubleClick(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
             _controller.SetImages();
            splashScreenManager1.CloseWaitForm();
        }

        private void gc_Incidents_Click(object sender, EventArgs e)
        {

        }

        private void gv_Incidents_Click(object sender, EventArgs e)
        {
            _controller.SetIncident();
        }

        private void Main3_SizeChanged(object sender, EventArgs e)
        {
            LblFolioPosition();
        }
    }
}