using DevExpress.XtraEditors;
using ResponseEmergencySystem.Controllers;
using ResponseEmergencySystem.Models;
using ResponseEmergencySystem.Properties;
using ResponseEmergencySystem.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponseEmergencySystem.Code
{
    class Utils
    {
        private static string Employee_Email = "noreply@intelogix.mx";
        private static string PasswordEmail = "Intelogix1";
        private static string EmailDestination = "michaelreyesfernandez@hotmail.com";

        public static bool email_send(string filePath, bool resend, string mailAddress = "", string categoryID = "")
        {
            //jgonzalez@intelogix.mx Ingeniero en Sistemas
            bool MailSent = false;
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress(Employee_Email);

            if (mailAddress != "")
                mail.To.Add(mailAddress);

            if (categoryID != "")
            {
                var mailDirectory = MailDirectoryService.GetMailDirectory(categoryID);
                foreach (var m in mailDirectory)
                {
                    mail.To.Add(m.Mail);
                }
            }

            //if (mails.Length > 1)
            //    foreach (var m in mails)
            //    {
            //        mail.To.Add(m);
            //    }

            //mail.To.Add("jjind@citlogistics.us");
            mail.Subject = "Información del reporte registrado";
            mail.Body = "Buen día,\n\rEnvío información el reporte sobre el accidente que se registro hoy";
            if (resend)
            {
                mail.Subject = "Corrección de información de nuevo empleado";
                mail.Body = "Buen día,\n\rEnvío correción de información de nuevo empleado para su registro";
            }

            Attachment attachment;
            attachment = new Attachment(filePath);
            mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(Employee_Email, PasswordEmail);
            SmtpServer.EnableSsl = true;

            try
            {
                SmtpServer.Send(mail);
                MailSent = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            attachment.Dispose();
            mail.Attachments.Clear();
            mail.Dispose();
            return MailSent;
        }

        public static string GetEdtValue(TextEdit edt)
        {
            string result = edt.EditValue == null ? "" : edt.EditValue.ToString();
            return result;
        }

        public static string GetRowID(DevExpress.XtraGrid.Views.Grid.GridView gv, string ID_Name)
        {
            try
            {
                Int32 index = gv.FocusedRowHandle;
                string ID = gv.GetRowCellValue(index, ID_Name).ToString();
                return ID;
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
                return "";
            }


        }

        public static string GetRowID(DevExpress.XtraGrid.Views.Card.CardView gv, string ID_Name)
        {
            try
            {
                Int32 index = gv.FocusedRowHandle;
                string ID = gv.GetRowCellValue(index, ID_Name).ToString();
                return ID;
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
                return "";
            }


        }

        public static string GetRowID(DevExpress.XtraGrid.Views.Layout.LayoutView gv, string ID_Name)
        {
            try
            {
                Int32 index = gv.FocusedRowHandle;
                string ID = gv.GetRowCellValue(index, ID_Name).ToString();
                return ID;
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
                return "";
            }

        }

            // this only works if the name of the label is like the button and they are sibilings (lbl_text1 - btn_text1 both childs of pnl_1)
        public static string GetTextOfLabelInCaptures(SimpleButton btn)
        {
            return btn.Parent.Controls["lbl_" + btn.Name.Split('_')[1]].Text;
        }

        public static ProgressBarControl GetCurrentProgressBar(Form form, string pnlName, string progressBarName)
        {
            return (ProgressBarControl)form.Controls[pnlName].Controls[progressBarName];
        }

        public static void CustomizeButton(SimpleButton btn, Color c, DevExpress.Utils.Svg.SvgImage svg, string t)
        {
            btn.ForeColor = c;
            btn.ImageOptions.SvgImage = svg;
            btn.Text = t;
        }

        public static void ShowMessage(string msg, string title = "", bool confirmation = false, string type = "approved")
        {
            Forms.Modals.Modals modalView = new Forms.Modals.Modals(msg, title);
            ModalController modalCtrl = new ModalController(modalView);

            switch(type)
            {
                case "approved":
                    modalView.SetApprovedIcon();
                    break;
                case "Error":
                    modalView.SetErrorIcon();
                    break;
                case "Warning":
                    modalView.SetWarningIcon();
                    break;
                case "MailSent":
                    modalView.SetMailSentIcon();
                    break;
            }
            //if(!confirmation)
            //{
            //    using (var control = G) 
            //    { 
            //        spnl_1.Visible = false; 
            //    }

            //    using (var spnl2 = (DevExpress.Utils.Layout.StackPanel) modalView.Controls["stackPanel2"]) { spnl2.LayoutDirection = DevExpress.Utils.Layout.StackPanelLayoutDirection.RightToLeft; };
            //}
           
            modalView.ShowDialog();
        }

        public static bool ShowConfirmationMessage(string msg, string title = "", bool confirmation = false, string type = "approved")
        {
            Forms.Modals.ConfirmationModal confirmationModalView = new Forms.Modals.ConfirmationModal(msg, title);
            ConfirmationModalController confirmationModalCtrl = new ConfirmationModalController(confirmationModalView);

            switch (type)
            {
                case "approved":
                    confirmationModalView.SetApprovedIcon();
                    break;
                case "Error":
                    confirmationModalView.SetErrorIcon();
                    break;
                case "Warning":
                    confirmationModalView.SetWarningIcon();
                    break;
                case "MailSent":
                    confirmationModalView.SetMailSentIcon();
                    break;
            }

            if (confirmationModalView.ShowDialog() == DialogResult.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static DateTime GetDateTime(DateTime date, int hours, int minutes)
        {
            date = date.AddHours(hours);
            date = date.AddMinutes(minutes);
            return date;
        }



        private Control GetControlByName(string Name)
        {
            //foreach (Control c in this.Controls)
            //    if (c.Name == Name)
            //        return c;

            return null;
        }

        public static bool CheckIfColumnExistInDataReader(SqlDataReader sdr, string columnName)
        {
            for (int i = 0; i < sdr.FieldCount; i++)
            {

                if (sdr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        public static Image GetImage(string path)
        {
            Image newImg;
            try
            {
                System.Net.WebRequest request =
                System.Net.WebRequest.Create(path);
                System.Net.WebResponse response = request.GetResponse();
                System.IO.Stream responseStream = response.GetResponseStream();

                using (var bmpTemp = new Bitmap(responseStream))
                {
                    newImg = new Bitmap(bmpTemp);
                }

                return newImg;
            }
            catch (System.Net.WebException ex)
            {
                //MessageBox.Show("There was an error opening the image file.");
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
