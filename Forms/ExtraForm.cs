using DevExpress.XtraEditors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ResponseEmergencySystem.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ResponseEmergencySystem.Samsara_Models;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Services;

namespace ResponseEmergencySystem.Forms
{



    public partial class ExtraForm : DevExpress.XtraEditors.XtraForm
    {

        #region

        public class Location
        {
            public string name { get; set; }
            public DateTime time { get; set; }
            public float latitude { get; set; }
            public float longitude { get; set; }
            public int heading { get; set; }
            public int speed { get; set; }
            public string formattedLocation { get; set; }
        }


        #endregion

        public ExtraForm()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            //IncidentReport report1 = new IncidentReport("data1", "Data2", "Data3");
            var namefile = "test";
            string ReportPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"{namefile}.pdf");
            //DevExpress.XtraPrinting.PdfExportOptions MyPdfOptions = new DevExpress.XtraPrinting.PdfExportOptions();
            //try
            //{
            //    report1.ExportToPdf(ReportPath);
            //}
            //catch 
            //{
            //    MessageBox.Show("Problem with the pdf");
            //    return;
            //}


            Debug.WriteLine(Utils.email_send(ReportPath, false));

            //DialogResult = DialogResult.Yes;

            //form_driver_report tuventana = new form_driver_report();
            //tuventana.ShowDialog();
        }

        private void ExtraForm_Load(object sender, EventArgs e)
        {
            lue_Test.Properties.DataSource = StatusDetailService.list_StatusDetail();

            foreach( var capture in CaptureService.list_Captures())
            {
                if (capture.images != null)
                {
                    //foreach (var t in capture.images)
                    //{
                    //    Debug.WriteLine(t.Trim() + " of the " + capture.captureType);
                    //}
                }
            }
        }
    }
}