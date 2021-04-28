using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Cloud.Firestore;
using System.Net;
using System.IO;
using Firebase.Storage;
using ITXFramework;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Services;
using ResponseEmergencySystem.Models;

namespace ResponseEmergencySystem.Forms
{
    public partial class frm_Incident_Captures : DevExpress.XtraEditors.XtraForm
    {
        // public DateTime DateTime { get; set; }
        FirestoreDb dataBase;
        string user;

        private FirestoreChangeListener listener;
        private int listening = 0;

        private DataTable dt_IncidentCaptures = new DataTable();
        private DataTable dtImagesTest = new DataTable();
        private DataTable dt_CaptureTypes;

        private List<Capture> captures = new List<Capture>();

        private Guid CaptureSelected;

        public frm_Incident_Captures()
        {
            InitializeComponent();
        }

        private void btn_ListOfImages(object sender, EventArgs e)
        {
            //pnl_ListCaptures.Visible = true;
            Int32 idx = gv_Captures.FocusedRowHandle;
            CaptureSelected = captures[idx].ID_Capture;

            gc_Images.DataSource = captures[idx].imagesListOfNames;


            //this.dt_DriverRowSelected = index;
            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        private void btn_DeleteCaptureClick(object sender, EventArgs e)
        {
            Int32 index = gv_Captures.FocusedRowHandle;

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this row?",
                "Delete injured person row",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {
                gv_Captures.DeleteRow(index);
            }
        }

        private void btn_AddCaptureClick(object sender, EventArgs e)
        {
            DataRow row = dt_CaptureTypes.Select().First();
            addEmptyRowCaptures(row["ID_CaptureType"].ToString());
            refreshTable(gc_Captures);
        }

        private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //InforY.
        }

        private void radioGroup2_SelectedIndexChanged(object sender, EventArgs e)
        {
            RadioGroup edit = sender as RadioGroup;
            if (edit.SelectedIndex == 0) Debug.WriteLine("hello1");
            else Debug.WriteLine("Hello2");

        }

        private void refreshTable(DevExpress.XtraGrid.GridControl gc)
        {
            gc.DataSource = dt_IncidentCaptures;
        }

        private void addEmptyRowCaptures(string ID_CaptureType)
        {
            DataRow _data = dt_IncidentCaptures.NewRow();
            _data["CaptureType"] = ID_CaptureType;
            dt_IncidentCaptures.Rows.Add(_data);
            refreshTable(gc_Captures);
        }
        System.Drawing.Image ZoomPicture(System.Drawing.Image img, Size size)
        {
            Bitmap bm = new Bitmap(img, Convert.ToInt32(img.Width + size.Width), Convert.ToInt32(img.Height + size.Height));
            Graphics gpu = Graphics.FromImage(bm);
            gpu.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bm;
        }

        private void form_driver_report_Load(object sender, EventArgs e)
        {
            /** SQL Server Database Connection **/
            captures = CaptureService.list_CaptureTypes();
            lue_Types.DataSource = captures;

            dt_IncidentCaptures.Columns.Add("Date");
            dt_IncidentCaptures.Columns.Add("CaptureType");

            foreach (var capture in CaptureService.list_CaptureTypes())
            {
                addEmptyRowCaptures(capture.ID_CaptureType);
            }

            ///** Firestore Database Connection **/
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", constants.path);
            dataBase = FirestoreDb.Create("dcmanagement-3d402");

            CollectionReference messagesRef = dataBase.Collection("chats").Document("1654ec38-ec4f-4e05-9728-46154ad7d9ab").Collection("messages");
            Query query = messagesRef;

            FirestoreChangeListener listener = query.Listen(snapshot =>
            {
                foreach (DocumentChange change in snapshot.Changes)
                {
                    if (change.ChangeType.ToString() == "Added")
                    {
                        Refresh_Chat(change.Document);

                    }
                }
            });
        }

        private async void simpleButton1_Click(object sender, EventArgs e)
        {
            ExtraForm main = new ExtraForm();

            if (main.ShowDialog() == DialogResult.Yes)
            {
                Debug.WriteLine("good");
            }
        }

        private async void simpleButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select Image";
            ofd.Filter = "Image Files(*.jpg) | *.jpg";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // imageBox.ImageLocation = ofd.FileName;
                

                string filepath = ofd.FileName;
                Debug.WriteLine(filepath);
                MessageBox.Show(filepath);
                // Get any Stream — it can be FileStream, MemoryStream or any other type of Stream
                var stream = File.Open(filepath, FileMode.Open);

                // Construct FirebaseStorage with path to where you want to upload the file and put it there
                var task = new FirebaseStorage("dcmanagement-3d402.appspot.com")
                .Child("data")
                .Child("random")
                .Child("file.png")
                .PutAsync(stream);

                // Track progress of the upload
                //task.Progress.ProgressChanged += (s, ev) => Console.WriteLine($"Progress: {ev.Percentage} %");
                task.Progress.ProgressChanged += (s, ev) => {
                    progressBarControl1.Refresh();
                    progressBarControl1.EditValue = ev.Percentage;
                    progressBarControl1.CreateGraphics().DrawString(ev.Percentage.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(progressBarControl1.Width / 2 - 10, progressBarControl1.Height / 2 - 7)); 
                };

                // Await the task to wait until upload is completed and get the download url
                var downloadUrl = await task;

                System.Drawing.Image img = new Bitmap(filepath);
                // image.Img = ofd.FileName;
                imageBox.Image = img.GetThumbnailImage(350, 200, null, new IntPtr());
            }
        }

        private async void uploadImage(object sender, EventArgs e)
        {
            Int32 idx = gv_Images.FocusedRowHandle;

            var capture = captures.Where(c => c.ID_Capture == CaptureSelected).First();
            
            frm_Image captureFrm = new frm_Image(capture.ID_Capture.ToString().ToUpper(), capture.imagesListOfNames[idx].name);
            captureFrm.ShowDialog();
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Title = "Select Image";
            //ofd.Filter = "Image Files(*.jpg) | *.jpg";

            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    // imageBox.ImageLocation = ofd.FileName;

            //    pnl_Uploading.Visible = true;

            //    string filepath = ofd.FileName;
            //    Debug.WriteLine(filepath);
            //    //MessageBox.Show(filepath);
            //    // Get any Stream — it can be FileStream, MemoryStream or any other type of Stream
            //    var stream = File.Open(filepath, FileMode.Open);

            //    // Construct FirebaseStorage with path to where you want to upload the file and put it there
            //    var task = new FirebaseStorage("dcmanagement-3d402.appspot.com")
            //    .Child("data")
            //    .Child("random")
            //    .Child("file.png")
            //    .PutAsync(stream);

            //    // Track progress of the upload
            //    task.Progress.ProgressChanged += (s, ev) =>
            //    {
            //        progressBarControl1.EditValue = ev.Percentage;
            //        progressBarControl1.CreateGraphics().DrawString(ev.Percentage.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(progressBarControl1.Width / 2 - 10, progressBarControl1.Height / 2 - 7));
            //        //Console.WriteLine($"Progress: {ev.Percentage} %");
            //    };

            //    // Await the task to wait until upload is completed and get the download url
            //    var downloadUrl = await task;
            //    pnl_Uploading.Visible = false;

            //    System.Drawing.Image img = new Bitmap(ofd.FileName);
            //    // image.Img = ofd.FileName;

            //    imageBox.Image = img.GetThumbnailImage(350, 200, null, new IntPtr());
            //    pnl_Zoom.Visible = true;
            //}
        }

        private void zoomOut(object sender, EventArgs e)
        {
            imageBox.Image = ZoomPicture(imageBox.Image, new Size(50, 50));
        }

        private void zoomIn(object sender, EventArgs e)
        {
            imageBox.Image = ZoomPicture(imageBox.Image, new Size(-50, -50));
        }

        /************************************Functions************************************************/

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
        public void Send()
        {


            ITXFramework.ITXFramework itx = new ITXFramework.ITXFramework();

            // itx.cfrm_InsertForm
            // string idCapture = gv_Captures.GetFocusedRowCellValue("ID_Capture").ToString();
            string idCapture = "1654ec38-ec4f-4e05-9728-46154ad7d9ab";
            string now = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            now = now.Replace("/", "-");

            DocumentReference docRef = dataBase.Collection("chats").Document(idCapture).Collection("messages").Document(now);
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                {"from", constants.userName  },
                {"text", edt_Message.Text }
            };

            if (edt_Message.Text == "")
            {
                MessageBox.Show("Message is Empty");
                return;
            }
            docRef.SetAsync(data1);
            edt_Message.Text = "";
        }
        private async Task DownloadFile(string url, string name)
        {
            using (var client = new WebClient())
            {
                await client.DownloadFileTaskAsync(url, name);

            }
        }

        private void edt_Message_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Send();
            }
        }

        private async void simpleButton3_Click_1(object sender, EventArgs e)
        {
            
        }

        private void btn_Send_Click(object sender, EventArgs e)
        {
            Send();
        }

        /*
        private async void btn_UpdateImage_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
        string idImage = gv_Images.GetFocusedRowCellValue("ID_Image").ToString();
        string statusDetail = gv_Images.GetFocusedRowCellValue("StatusDetail").ToString();
        Connection.Connection.Update_Image_StatusDetail(idImage, statusDetail);
        MessageBox.Show("Status changed to " + gv_Images.GetFocusedRowCellDisplayText("StatusDetail"));

        int count = gv_Images.RowCount;
        int statusCounter = 0;
        for (int i = 0; i < gv_Images.DataRowCount; i++)
        {
        if (gv_Images.GetRowCellDisplayText(i, gv_Images.Columns.ColumnByFieldName("StatusDetail")).ToString() == "APPROVED")
        {
        statusCounter++;
        }
        }
        MessageBox.Show(statusCounter.ToString() + " of " + count.ToString() + "Captures Approved");
        string idCapture = gv_Images.GetFocusedRowCellValue("ID_Capture").ToString();
        if (statusCounter == count)
        {
        Connection.Connection.Update_CaptureStatus(idCapture, "D42952F8-C18A-4FF1-B6BF-1DD61C57908E");
        Connection.Connection.Refresh_Captures("", "", "", "", 1);
        gc_Captures.DataSource = Connection.Connection.Dt_Captures;
        }
        else
        {
        Connection.Connection.Update_CaptureStatus(idCapture, "0B335BC3-24AE-4399-8925-74935417B4F2");
        Connection.Connection.Refresh_Captures("", "", "", "", 1);
        gc_Captures.DataSource = Connection.Connection.Dt_Captures;
        }

        }
        */

    }

    [FirestoreData]
    internal class Data
    {
        [FirestoreProperty]
        public string from { get; set; }
        [FirestoreProperty]
        public string text { get; set; }
    }
}