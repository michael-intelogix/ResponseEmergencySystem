using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Properties;
using ResponseEmergencySystem.Forms.Helpers;
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
using System.Configuration;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Collections;
using Firebase.Storage;

namespace ResponseEmergencySystem.Forms
{
    public partial class frm_Image : DevExpress.XtraEditors.XtraForm
    {
        OpenFileDialog ofd = new OpenFileDialog();
        string filepath = "";
        Image newImg = null;
        private string ID_Capture = "";
        private string fileName = "";

        public frm_Image(string ID_Capture, string captureName)
        {
            InitializeComponent();
            this.ID_Capture = ID_Capture;
            fileName = captureName;
            //var value = section["url"];
        }

        public void onClickLoadImage (object sender, EventArgs e)
        {
          
            ofd.Filter = "Image Files(*.PNG;*.JPG;*.GIF;*.BMP)|*.PNG;*.JPG;*.GIF;*.BMP|PDF Files (*.PDF)|*.PDF|All Files (*.*)|*.*";
            ofd.ShowDialog();

            string ext = Path.GetExtension(ofd.FileName).ToUpper();
            try
            {
                if (ext == ".GIF" || ext == ".JPG" || ext == ".PNG" || ext == ".BMP")
                {
                    img_Test .Visible = true;
                    filepath = ofd.FileName;

                    Image img;
                    using (var bmpTemp = new Bitmap(filepath))
                    {
                        newImg = new Bitmap(bmpTemp);
                        img_Test.Image = newImg;
                    }
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
        }


        private void simpleButton5_Click(object sender, EventArgs e)
        {
            Cursor.Current = new Cursor(((Bitmap)Resources.zoomin_16x161).GetHicon());
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
  
            Settings.Default.Reset();
        }

        private void img_Test_Click(object sender, MouseEventArgs e)
        {

            
            if (e.Button == MouseButtons.Left)
            {
                img_Test.Properties.ZoomPercent += 20;
            }
            else
            {
                img_Test.Properties.ZoomPercent -= 20;
            }
        }

        private void frm_Image_Shown(object sender, EventArgs e)
        {
            if (Settings.Default.ZoomMsg)
            {
                frm_Zoom zoomHelper = new frm_Zoom();
                zoomHelper.ShowDialog();
            }
        }

        private void labelControl3_Click(object sender, EventArgs e)
        {
            img_Test.Properties.ZoomPercent += 20;
        }

        private void labelControl2_Click(object sender, EventArgs e)
        {
            img_Test.Properties.ZoomPercent -= 20;
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {
            img_Test.Properties.ZoomPercent = 100;
        }

        private void img_Test_EditValueChanged(object sender, EventArgs e)
        {
            pnl_ImgControls.Visible = true;
            btn_SaveImage.Visible = true;
        }

        private async void btn_SaveImage_Click(object sender, EventArgs e)
        {
            btn_SaveImage.Visible = false;
            pnl_Uploading.BackColor = Color.FromArgb(17, 0, 0, 0);
            pnl_Uploading.Visible = true;
            //MessageBox.Show(filepath);
            // Get any Stream — it can be FileStream, MemoryStream or any other type of Stream
            var stream = File.Open(filepath, FileMode.Open);

            // Construct FirebaseStorage with path to where you want to upload the file and put it there
            var task = new FirebaseStorage("dcmanagement-3d402.appspot.com")
            .Child("SIREM")
            .Child("Captures")
            .Child(ID_Capture)
            .Child(fileName)
            .PutAsync(stream);

            // Track progress of the upload
            task.Progress.ProgressChanged += (s, ev) =>
            {
                progressBarControl1.EditValue = ev.Percentage;
                progressBarControl1.CreateGraphics().DrawString(ev.Percentage.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(progressBarControl1.Width / 2 - 10, progressBarControl1.Height / 2 - 7));
                //Console.WriteLine($"Progress: {ev.Percentage} %");
            };

            // Await the task to wait until upload is completed and get the download url
            var downloadUrl = await task;
            pnl_Uploading.Visible = false;

        }
    }
}