using DevExpress.XtraEditors;
using ResponseEmergencySystem.Code;
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

namespace ResponseEmergencySystem.Forms.Modals
{
    public partial class DocumentModal : DevExpress.XtraEditors.XtraForm
    {
        public Models.Documents.Document doc = new Models.Documents.Document("", 0);

        public DocumentModal(Models.Documents.Document doc)
        {
            InitializeComponent();
            this.doc = doc;
        }

        private void btn_Capture1_Click(object sender, EventArgs e)
        {


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
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }

        private void btn_Cancel2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void DocumentModal_Load(object sender, EventArgs e)
        {
            pictureEdit1.Image = doc.Image;
            textEdit1.EditValue = doc.name;
        }
    }
}