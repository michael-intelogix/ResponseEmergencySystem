using DevExpress.XtraEditors;
using ResponseEmergencySystem.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponseEmergencySystem.Forms
{
    public partial class ExtraForm : DevExpress.XtraEditors.XtraForm
    {
        public ExtraForm()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            IncidentReport report1 = new IncidentReport("data1", "Data2", "Data3");
            var namefile = "test";
            string ReportPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"{namefile}.pdf");
            DevExpress.XtraPrinting.PdfExportOptions MyPdfOptions = new DevExpress.XtraPrinting.PdfExportOptions();
            try
            {
                report1.ExportToPdf(ReportPath);
            }
            catch 
            {
                MessageBox.Show("Problem with the pdf");
                return;
            }
            //DialogResult = DialogResult.Yes;

            //form_driver_report tuventana = new form_driver_report();
            //tuventana.ShowDialog();
        }
    }
}