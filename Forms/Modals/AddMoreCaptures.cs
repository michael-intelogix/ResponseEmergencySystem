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
using ResponseEmergencySystem.Properties;

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
            var btn = (SimpleButton)sender;
            var name = btn.Name.Split('_')[1];
            var status = btn.Parent.Controls["status_" + name];
            _controller.UploadImage(Utils.GetTextOfLabelInCaptures(btn), Convert.ToInt32(name.Replace("Capture",  "")));
            status.Text = "Preloaded";
            status.Visible = true;
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

        public bool SaveButtonEnable
        {
            set { btn_Save.Enabled = value; }
        }

        public ProgressBarControl[] PbrArray
        {
            get { return new ProgressBarControl[] { pbr_Cpature1, pbr_Capture2, pbr_Capture3, pbr_Capture4}; }
        }

        public LabelControl[] LblArray
        {
            get { return new LabelControl[] { lbl_Capture1, lbl_Capture2, lbl_Capture3, lbl_Capture4 }; }
        }

        public SimpleButton[] BtnArray
        {
            get { return new SimpleButton[] { status_Capture1, status_Capture2, status_Capture3, status_Capture4 }; }
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            _controller.SaveAsync();
            splashScreenManager1.CloseWaitForm();
        }
    }
}