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

        private void gv_Incidents_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            _controller.SetCaptures();
            _controller.LoadChat();
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

        private void btn_Picture_Click(object sender, EventArgs e)
        {
            string imgPath = Utils.GetRowID(gv_Images, "ImagePath");
            _controller.EditImageView(imgPath);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
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
            string incidentId = Utils.GetRowID(gv_Incidents, "ID_Incident");

            _controller.EditIncidentView(incidentId);
        }

        private void btn_Comments_Click(object sender, EventArgs e)
        {
            _controller.SetComments();
            //gv_Captures.BestFitColumns();
        }

        private void btn_Edit4_Click(object sender, EventArgs e)
        {
            _controller.EditImageData();
        }

        #region IMain 
        public void SetController(MainController controller)
        {
            _controller = controller;
        }

        public void CloseSpinner()
        {
            splashScreenManager1.CloseWaitForm();
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

        public void LoadImageInTable(string imgUrl)
        {
            //Image newImg;
            //try
            //{
            //    System.Net.WebRequest request =
            //    System.Net.WebRequest.Create(imgUrl);
            //    System.Net.WebResponse response = request.GetResponse();
            //    System.IO.Stream responseStream = response.GetResponseStream();

            //    using (var bmpTemp = new Bitmap(responseStream))
            //    {
            //        newImg = new Bitmap(bmpTemp);
            //        (ImageEdit) gv_Images.GetRowCellValue(0, "show")
            //    }
            //}
            //catch (System.Net.WebException)
            //{
            //    MessageBox.Show("There was an error opening the image file.");
            //}
        }

        public string ID_Capture
        {
            get { return gv_Captures.GetFocusedRowCellValue("ID_Capture").ToString(); }
        }

        public string ID_Image
        {
            get { return gv_Images.GetFocusedRowCellValue("ID_Image").ToString(); }
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
            get { return gv_Incidents.GetFocusedRowCellValue("ID_Incident").ToString(); }
        }

        public string ImageName
        {
            get { return gv_Images.GetFocusedRowCellValue("ImageName").ToString(); }
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

        public object Incidents
        {
            set { gc_Incidents.DataSource = value; }
        }

        public object CapturesDataSource
        {
            set { gc_Captures.DataSource = value; }
        }

        public object ImagesDatasSource
        {
            set { gc_Images.DataSource = value; }
        }

        public MemoEdit chat
        {
            get { return memoEdit_Chat; }
        }
        #endregion

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            _controller.AddIncidentView();
            
        }

        private void simpleButton2_Click_2(object sender, EventArgs e)
        {
            _controller.AppSettings();
        }

        private void simpleButton4_Click_1(object sender, EventArgs e)
        {
            _controller.AddMoreCaptures();
            gv_Images.BestFitColumns();
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

        private void gv_Captures_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            _controller.SetImages();
        }

        private void Main2_Load(object sender, EventArgs e)
        {
            _controller.LoadData();
        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}