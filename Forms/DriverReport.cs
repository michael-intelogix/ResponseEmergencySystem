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


namespace ResponseEmergencySystem.Forms
{
    public partial class form_driver_report : DevExpress.XtraEditors.XtraForm
    {
        // public DateTime DateTime { get; set; }
        FirestoreDb dataBase;
        string user;
        

        public form_driver_report()
        {
            InitializeComponent();
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

        private void form_driver_report_Load(object sender, EventArgs e)
        {
            /** SQL Server Database Connection **/
            string idBusiness = "";
            string idBranchOffice = "";
            string idCaptureType = "";
            string dateTime = "";
            int status = 1;
            Connection.Connection.Refresh_Captures(idBusiness, idBranchOffice, idCaptureType, dateTime, status);
            gc_Captures.DataSource = Connection.Connection.Dt_Captures;
            Connection.Connection.List_StatusDetails();
            DataTable table = Connection.Connection.Dt_StatusDetail;
            lue_StatusDetail.DataSource = Connection.Connection.Dt_StatusDetail;

            /** Firestore Database Connection **/
            string path = AppDomain.CurrentDomain.BaseDirectory + @"dcmanagement.json";
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", path);
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
            Main main = new Main();

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
                task.Progress.ProgressChanged += (s, ev) => Console.WriteLine($"Progress: {ev.Percentage} %");

                // Await the task to wait until upload is completed and get the download url
                var downloadUrl = await task;

                Image img = new Bitmap(ofd.FileName);
                // image.Img = ofd.FileName;
                imageBox.Image = img.GetThumbnailImage(350, 200, null, new IntPtr());
            }
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
            string now = System.DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            now = now.Replace("/", "-");

            DocumentReference docRef = dataBase.Collection("chats").Document(idCapture).Collection("messages").Document(now);
            Dictionary<string, object> data1 = new Dictionary<string, object>()
            {
                {"from", user  },
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