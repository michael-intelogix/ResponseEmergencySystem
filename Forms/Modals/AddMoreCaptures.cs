using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ResponseEmergencySystem.Views.Captures;
using ResponseEmergencySystem.Models;
using ResponseEmergencySystem.Controllers.Captures;
using ResponseEmergencySystem.Code;
using System.IO;
using System.Diagnostics;

namespace ResponseEmergencySystem.Forms
{
    public partial class AddMoreCaptures : DevExpress.XtraEditors.XtraForm, IAddCapturesView
    {
        public AddMoreCaptures()
        {
            InitializeComponent();
        }

        AddCapturesController _controller;



        private void btn_Cancel2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public void SetController(AddCapturesController controller)
        {
            _controller = controller;
        }

        public void LoadCapturesTypes(List<Capture> captures)
        {
            lue_Type.Properties.DataSource = captures;
        }

        private void AddMoreCaptures_Load(object sender, EventArgs e)
        {
           
        }

        private void UploadImageOnClick(object sender, EventArgs e)
        {
            //Debug.WriteLine(((SimpleButton)sender).Name.Split('_')[1]);
            //Debug.WriteLine(((SimpleButton)sender).Parent.Controls["lbl_" + ((SimpleButton)sender).Name.Split('_')[1]].Name);
            _controller.UploadImage(Utils.GetTextOfLabelInCaptures((SimpleButton)sender));
        }


        private void lue_Type_EditValueChanged(object sender, EventArgs e)
        {
            _controller.SetType(lue_Type.EditValue.ToString());
        }

    
        public bool LueTypeBlock
        {
            set { lue_Type.Properties.ReadOnly = value; }
        }

        public bool PnlCapture1Visbility
        {
            set { pnl_Capture1.Visible = value; }
        }

        public bool PnlCapture2Visbility
        {
            set { pnl_Capture2.Visible = value; }
        }
        public bool PnlCapture3Visbility
        {
            set { pnl_Capture3.Visible = value; }
        }
        public bool PnlCapture4Visbility
        {
            set { pnl_Capture4.Visible = value; }
        }

        public string LblCapture1Name
        {
            set { lbl_Capture1.Text = value; }
        }

        public string LblCapture2Name
        {
            set { lbl_Capture2.Text = value; }
        }

        public string LblCapture3Name
        {
            set { lbl_Capture3.Text = value; }
        }

        public string LblCapture4Name
        {
            set { lbl_Capture4.Text = value; }
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            _controller.SaveAsync();
            splashScreenManager1.CloseWaitForm();
        }
    }
}