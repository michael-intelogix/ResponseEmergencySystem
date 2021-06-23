using DevExpress.XtraEditors;
using Firebase.Storage;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Services;
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

namespace ResponseEmergencySystem.Forms.Modals
{
    public partial class DocumentModal : DevExpress.XtraEditors.XtraForm
    {
        public Models.Documents.Document doc = new Models.Documents.Document("", 0);
        private string ID_Incident;
        private string ID_Capture;

        public DocumentModal(Models.Documents.Document doc, string captureID)
        {
            InitializeComponent();
            this.doc = doc;
            this.ID_Capture = captureID;
        }

        private void btn_Capture1_Click(object sender, EventArgs e)
        {
            doc.Update("staged");

            if (doc.Type == "img")
            {
                pictureEdit1.Image = doc.Image;
                pdfViewer1.Visible = false;
                pictureEdit1.Visible = true;
            }

            if (doc.Type == "pdf")
            {
                pdfViewer1.LoadDocument(doc.Path);
                pdfViewer1.Visible = true;
                pictureEdit1.Visible = false;
            }
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            if (textEdit1.EditValue == null || textEdit1.EditValue.ToString() == "")
            {
                Utils.ShowMessage("Please give a valid name to the document");
            }
            else
            {
                doc.SetName(textEdit1.EditValue.ToString());
                SaveAsync(doc);
                //doc.SetImage();
                //pdfViewer1.Dispose();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }

        public async void SaveAsync(Models.Documents.Document doc)
        {
            //Task t1;

            //if (ID_Incident == Guid.Empty.ToString())
            //{
            //    t1 = new Task(() => SaveEmptyIncident());
            //    t1.Start();
            //    t1.Wait();
            //}

            //var t = new Task(() => SaveCapture());
            //t.Start();
            //t.Wait();

            if (doc.Path != "")
            {
                var task = UploadImgFirebaseAsync(doc.Path, doc.name);

                pbrUploading.Visible = true;

                // Track progress of the upload
                task.Progress.ProgressChanged += (s, ev) =>
                {
                    pbrUploading.EditValue = ev.Percentage;
                    pbrUploading.CreateGraphics().DrawString(ev.Percentage.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(pbrUploading.Width / 2 - 10, pbrUploading.Height / 2 - 7));
                    Console.WriteLine($"Progress: {ev.Percentage} %");
                };

                doc.FirebaseUrl = await task;
                doc.SetImage(true);
            }

        }

        private FirebaseStorageTask UploadImgFirebaseAsync(string filepath, string name)
        {
            try
            {
                // Get any Stream — it can be FileStream, MemoryStream or any other type of Stream
                var stream = File.Open(filepath, FileMode.Open);

                // Construct FirebaseStorage with path to where you want to upload the file and put it there
                var task = new FirebaseStorage("dcmanagement-3d402.appspot.com")
                .Child("SIREM")
                .Child(ID_Capture)
                .Child(name)
                .PutAsync(stream);

                // Await the task to wait until upload is completed and get the download url
                return task;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        //private void SaveCapture()
        //{
        //    var response = CaptureService.AddCapture(_selectedCaptureType.ID_CaptureType, ID_Incident, "testing", _view.Comments);
        //    ID_Capture = response.ID;
        //}

        private void SaveEmptyIncident()
        {
            var response = IncidentService.CreateEmptyIncident();
            ID_Incident = response.ID;
        }

        private void btn_Cancel2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void DocumentModal_Load(object sender, EventArgs e)
        {
            pdfViewer1.Size = new Size(381, 296);

            if (doc.Type == "img")
            {
                pictureEdit1.Image = doc.Image;
            }
            
            if (doc.Type == "pdf")
            {
                pdfViewer1.LoadDocument(doc.Path);
                
                pdfViewer1.Visible = true;
            }
            textEdit1.EditValue = doc.name;
        }

        private void DocumentModal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (doc.Type == "img")
            {
                pictureEdit1.Dispose();
            }

            if (doc.Type == "pdf")
            {
                pdfViewer1.Dispose();
            }
        }
    }
}