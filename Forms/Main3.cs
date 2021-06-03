using DevExpress.XtraEditors;
using Google.Cloud.Firestore;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Controllers;
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

        public object ID_Capture => gv_Captures;

        public string ID_Image => gv_Images.GetFocusedRowCellValue("ID_Image").ToString();

        public string ImageName => gv_Images.GetFocusedRowCellValue("ImageName").ToString();

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
        public string Folio 
        { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
        }
        public string DriverName 
        { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
        }
        public string TruckNumber 
        { 
            get => throw new NotImplementedException(); 
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
            set => lue_StatusDetail.DataSource = value; 
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
            //_controller.LoadData();
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
            string incidentId = Utils.GetRowID(gv_Incidents, "ID_Incident");
            string folio = Utils.GetRowID(gv_Incidents, "Folio");

            _controller.ShowIncident(incidentId, folio);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            _controller.EditImageData();
        }

        private void btn_Picture_Click(object sender, EventArgs e)
        {
            string imgPath = Utils.GetRowID(gv_Images, "ImagePath");
            _controller.EditImageView(imgPath);
        }
    }
}