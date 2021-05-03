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
            AddMoreCaptures AddMoreCaptures = new AddMoreCaptures();
            AddMoreCaptures.ShowDialog();
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
            frm_Image frm_Image = new frm_Image("", "", imgPath);
            frm_Image.ShowDialog();
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            AddIncidentDetails addIncidentView = new AddIncidentDetails();
            Controllers.Incidents.AddIncidentController addIncidentCtrl = new Controllers.Incidents.AddIncidentController(addIncidentView);
            addIncidentCtrl.LoadStates();
            addIncidentView.ShowDialog();

        }

        private void btn_View2_Click(object sender, EventArgs e)
        {
            Int32 index = gv_Incidents.FocusedRowHandle;
            string incidentId = gv_Incidents.GetRowCellValue(index, "ID_Incident").ToString();

            ViewIncidentDetails viewIncident = new ViewIncidentDetails();

            IncidentController incidentCtrl = new IncidentController(viewIncident, incidentId);
            incidentCtrl.LoadIncident();

            viewIncident.Show();
        }

        private void btn_Edit2_Click(object sender, EventArgs e)
        {
            string incidentId =  Utils.GetRowID(gv_Incidents, "ID_Incident");

            EditIncidentDetails viewEditIncident = new EditIncidentDetails();

            Controllers.Incidents.EditIncidentController incidentCtrl = new Controllers.Incidents.EditIncidentController(viewEditIncident, incidentId);
            incidentCtrl.LoadIncident();

            viewEditIncident.Show();
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

        private void btn_Email_click(object sender, EventArgs e)
        {
            var namefile = Utils.GetRowID(gv_Incidents, "Folio");
            string ReportPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"{namefile}.pdf");
            splashScreenManager1.ShowWaitForm();
            bool emailResponse = Utils.email_send(ReportPath, false);
            splashScreenManager1.CloseWaitForm();
            if (emailResponse)
            {
                MessageBox.Show("Mail Sent");
            }
            else
            {
                MessageBox.Show("Mail Error");
            }
        }

        private void btn_PDF_Click(object sender, EventArgs e)
        {
            string incidentId = Utils.GetRowID(gv_Incidents, "ID_Incident").ToString();
            IncidentReport report1 = new IncidentReport(IncidentService.list_Incidents("", "","","","", incidentId)[0]);
            var namefile = Utils.GetRowID(gv_Incidents, "Folio");
            string ReportPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"{namefile}.pdf");
            DevExpress.XtraPrinting.PdfExportOptions MyPdfOptions = new DevExpress.XtraPrinting.PdfExportOptions();
            try
            {
                report1.ExportToPdf(ReportPath);
                MessageBox.Show("PDF saved!");
            }
            catch
            {
                MessageBox.Show("Problem with the pdf");
                return;
            }
        }

        private void Main2_Load(object sender, EventArgs e)
        {
            //_controller.LoadChat();
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
            _controller.ChatListener();
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
        #endregion

    }
}