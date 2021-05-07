using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ResponseEmergencySystem.Controllers;
using ResponseEmergencySystem.Services;
using ResponseEmergencySystem.Views;
using ResponseEmergencySystem.Models;
using ResponseEmergencySystem.Code;
using DevExpress.XtraGrid.Views.Grid;
using System.Diagnostics;
using Google.Cloud.Firestore;
using System.IO;
using ResponseEmergencySystem.Reports;
using ResponseEmergencySystem.Properties;

namespace ResponseEmergencySystem.Forms
{
    public partial class Main2 : DevExpress.XtraEditors.XtraForm, IMainView
    {
        public Main2()
        {
            InitializeComponent();
        }

        MainController _controller;

        #region Mike Functions

        private void gv_Incidents_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            string incidentId = Utils.GetRowID(gv_Incidents, "ID_Incident");
            _controller.SetCaptures(incidentId);
            gc_Captures.DataSource = _controller._captures.Select(i => new { i.captureType, i.comments });

            gc_Images.DataSource = _controller._captures.Where(c => c.captureType == gv_Captures.GetRowCellValue(0, "captureType").ToString());
        }

        private void gv_Incidents_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            string incidentId = Utils.GetRowID(gv_Incidents, "ID_Incident");
            _controller.SetCaptures(incidentId);
            gc_Captures.DataSource = _controller._captures.Select(i => new { i.captureType, i.comments });

            gc_Images.DataSource = _controller._captures.Where(c => c.captureType == gv_Captures.GetRowCellValue(0, "captureType").ToString());
        }

        #endregion

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            _controller.AddMoreCaptures();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            _controller.Send();
        }

        private void edt_Message_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _controller.Send();
            }
        }

        private void labelControl4_Click(object sender, EventArgs e)
        {

        }

        private void labelControl5_Click(object sender, EventArgs e)
        {

        }

        private void btn_Picture_Click(object sender, EventArgs e)
        {
            string imgPath = Utils.GetRowID(gv_Images, "ImagePath");

            _controller.EditImageView(imgPath);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            _controller.AddIncidentView();
        }

        private void btn_View2_Click(object sender, EventArgs e)
        {
            string incidentId = Utils.GetRowID(gv_Incidents, "ID_Incident");
            string folio = Utils.GetRowID(gv_Incidents, "Folio");

            _controller.ShowIncident(incidentId, folio);
        }

        private void btn_Edit2_Click(object sender, EventArgs e)
        {
            string incidentId =  Utils.GetRowID(gv_Incidents, "ID_Incident");

            _controller.EditIncidentView(incidentId);
        }

        private void btn_Comments_Click(object sender, EventArgs e)
        {
            AddComments addComments = new AddComments();
            addComments.ShowDialog();
        }

        private void btn_Edit4_Click(object sender, EventArgs e)
        {
            Modals.EditComments editComments = new Modals.EditComments();
            editComments.ShowDialog();
        }

        private void Main2_Load(object sender, EventArgs e)
        {
        }

        #region IMain 
        public void SetController(MainController controller)
        {
            _controller = controller;
        }

        public void LoadIncidents(List<Incident> incidents)
        {
            gc_Incidents.DataSource = incidents.Select(i => new { i.ID_Incident, i.Name, i.Folio, i.IncidentDate, i.truck.truckNumber, i.ID_StatusDetail });
            _controller.LoadChat(incidents.FirstOrDefault().ID_Incident.ToString());
        }

        public void LoadCaptures(List<Capture> captures)
        {
            gc_Captures.DataSource = captures;
            gc_Images.DataSource = captures.Where(c => c.captureType == gv_Captures.GetRowCellValue(0, "captureType").ToString());
        }

        public void Refresh_Chat(DocumentSnapshot docsnap)
        {
            Data data = docsnap.ConvertTo<Data>();

            if (docsnap.Exists)
            {
                memoEdit_Chat.BeginInvoke((MethodInvoker)delegate ()
                {
                    memoEdit_Chat.MaskBox.AppendText(data.from + ":     ");
                    memoEdit_Chat.MaskBox.AppendText(data.text + "\r\n\r\n");
                });

            }
            else
            {
                MessageBox.Show("Chat is Empty");
            }

        }
    

        public string ChatText
        {
            get { return memoEdit_Chat.Text; }
            set { memoEdit_Chat.Text = value; }
        } 

        public string Message
        {
            get { return Utils.GetEdtValue(edt_Message); }
            set { edt_Message.EditValue = value; }
        }

        public string ID_Incident
        {   
            get { return gv_Incidents.GetFocusedRowCellValue("ID_Capture").ToString(); }
        }

        public string Date1 
        {
            get {
                string date1 = dateEdit1.DateTime.Date.ToString("MM/dd/yyyy");
                return date1 == "01/01/0001" ? "" : date1; 
            }
            set { dateEdit1.EditValue = value; }
        }
        public string Date2 
        {
            get {
                string date2 = dateEdit2.DateTime.Date.ToString("MM/dd/yyyy");
                return date2 == "01/01/0001" ? "" : date2;
            } 
            set { dateEdit2.EditValue = value; }
        }
        public string Folio 
        {
            get { return Utils.GetEdtValue(edt_Folio); } 
            set { edt_Folio.EditValue = value; }
        }
        public string DriverName 
        { 
            get { return Utils.GetEdtValue(edt_Name); } 
            set { edt_Name.EditValue = value; }
        }
        public string TruckNumber 
        { 
            get { return Utils.GetEdtValue(edt_TruckNum); }
            set { edt_TruckNum.EditValue = value; }
        }
        #endregion

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            //xtraFolderBrowserDialog1.ShowDialog();


            //Settings.Default.AppFolder = xtraFolderBrowserDialog1.SelectedPath;
            Settings.Default.Save();
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            _controller.AddIncidentView();
        }

        private void simpleButton2_Click_2(object sender, EventArgs e)
        {
            Modals.AppConfiguration appConfig = new Modals.AppConfiguration();
            appConfig.ShowDialog();
        }

        private void simpleButton4_Click_1(object sender, EventArgs e)
        {
            _controller.AddMoreCaptures();
        }

        private void textEdit1_Leave(object sender, EventArgs e)
        {

        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {

            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            dateEdit1.EditValue = null;
            dateEdit2.EditValue = null;
            edt_Folio.EditValue = "";
            edt_Name.EditValue = "";
            edt_TruckNum.EditValue = "";
            _controller.IncidentsFilter();
        }

        private void FilterEditValueChanged(object sender, EventArgs e)
        {
            _controller.IncidentsFilter();
        }
    }
}