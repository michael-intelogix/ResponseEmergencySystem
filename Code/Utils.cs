using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponseEmergencySystem.Code
{
    class Utils
    {
        private static string Employee_Email = "michael.reyes.intelogix@gmail.com";
        private static string PasswordEmail = "Int3logix2";
        private static string EmailDestination = "michaelreyesfernandez@hotmail.com";

        public static bool email_send(string filePath, bool resend)
        {
            //jgonzalez@intelogix.mx Ingeniero en Sistemas
            bool MailSent = false;
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress(Employee_Email);
            string[] Emails = EmailDestination.Split(';');
            foreach (string email in Emails)
            {
                mail.To.Add(email);
            }
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
            Int32 index = gv.FocusedRowHandle;
            string ID = gv.GetRowCellValue(index, ID_Name).ToString();
            return ID;
        }

        // this only works if the name of the label is like the button and they are sibilings (lbl_text1 - btn_text1 both childs of pnl_1)
        public static string GetTextOfLabelInCaptures(SimpleButton btn)
        {
            return btn.Parent.Controls["lbl_" + btn.Name.Split('_')[1]].Text;
        }

    }
}
