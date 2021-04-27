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

namespace ResponseEmergencySystem.Forms
{
    public partial class Main2 : DevExpress.XtraEditors.XtraForm
    {
        public Main2()
        {
            InitializeComponent();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            AddMoreCaptures AddMoreCaptures = new AddMoreCaptures();
            AddMoreCaptures.ShowDialog();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }

        private void labelControl4_Click(object sender, EventArgs e)
        {

        }

        private void labelControl5_Click(object sender, EventArgs e)
        {

        }

        private void btn_Picture_Click(object sender, EventArgs e)
        {
            frm_Image frm_Image = new frm_Image();
            frm_Image.ShowDialog();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            AddIncidentDetails capture = new AddIncidentDetails();

            capture.ShowDialog();

        }

        private void btn_View2_Click(object sender, EventArgs e)
        {
            AddIncidentDetails AddIncidentDetails = new AddIncidentDetails();
            AddIncidentDetails.ShowDialog();
        }

        private void btn_Edit2_Click(object sender, EventArgs e)
        {
            AddIncidentDetails AddIncidentDetails = new AddIncidentDetails();
            AddIncidentDetails.ShowDialog();
        }

        private void btn_Comments_Click(object sender, EventArgs e)
        {
            AddComments addComments = new AddComments();
            addComments.ShowDialog();
        }

        private void btn_Edit4_Click(object sender, EventArgs e)
        {
            Modals.EditComments editComments = new Modals.EditComments();
            editComments.ShowDialog();
        }
    }
}