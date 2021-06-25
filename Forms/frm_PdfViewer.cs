using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponseEmergencySystem.Forms
{
    public partial class frm_PdfViewer : DevExpress.XtraEditors.XtraForm
    {
        string _PdfFireabseUrl;
        bool firebase = true;

        public frm_PdfViewer(string url, bool firebase = true)
        {
            InitializeComponent();
            this.firebase = firebase;
            _PdfFireabseUrl = url;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            pdfViewer1.CloseDocument();
            this.Close();
        }

        private void frm_PdfViewer_Load(object sender, EventArgs e)
        {
            if(this.firebase)
            {
                MemoryStream stream;
                WebClient wc = new WebClient();
                byte[] data = wc.DownloadData(_PdfFireabseUrl);
                stream = new MemoryStream(data);
                pdfViewer1.LoadDocument(stream);
            }
            else
            {
                pdfViewer1.LoadDocument(_PdfFireabseUrl);
            }
            
        }
    }
}