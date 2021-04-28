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
using ResponseEmergencySystem.Controllers;
using ResponseEmergencySystem.Services;
using ResponseEmergencySystem.Views;
using ResponseEmergencySystem.Models;
using ResponseEmergencySystem.Code;
using DevExpress.XtraGrid.Views.Grid;

namespace ResponseEmergencySystem.Forms
{
    public partial class Main2 : DevExpress.XtraEditors.XtraForm, IMainView
    {
        public Main2()
        {
            InitializeComponent();
        }

        MainController _controller;

        #region Mike Functions

        private void gv_Incidents_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            var gv = (GridView)sender;

            Int32 index = gv_Incidents.FocusedRowHandle;
            string incidentId = gv_Incidents.GetRowCellValue(index, "ID_Incident").ToString();

        }

        #endregion

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
            frm_Image frm_Image = new frm_Image("", "");
            frm_Image.ShowDialog();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            AddIncidentDetails capture = new AddIncidentDetails();

            capture.ShowDialog();

        }

        private void btn_View2_Click(object sender, EventArgs e)
        {
            Int32 index = gv_Incidents.FocusedRowHandle;
            string incidentId = gv_Incidents.GetRowCellValue(index, "ID_Incident").ToString();

            ViewIncidentDetails viewIncident = new ViewIncidentDetails();

            IncidentController incidentCtrl = new IncidentController(viewIncident, incidentId);
            incidentCtrl.LoadIncident();

            viewIncident.Show();
            //AddIncidentDetails AddIncidentDetails = new AddIncidentDetails();
            //AddIncidentDetails.ShowDialog();
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

        private void Main2_Load(object sender, EventArgs e)
        {
            //gc_Incidents.DataSource = IncidentService.list_Incidents("", "", "", "", "").Select(i => new { i.ID_Incident, i.Name, i.Folio, i.IncidentDate, i.truck.truckNumber, i.ID_StatusDetail });
            //lue_StatusDetail.DataSource = Functions.list_StatusDetail();
        }

        #region IMain 
        public void SetController(MainController controller)
        {
            _controller = controller;
        }

        public void LoadIncidents(List<Incident> incidents)
        {
            //.Select(i => new { i.ID_Incident, i.Name, i.Folio, i.IncidentDate, i.truck.truckNumber, i.ID_StatusDetail }).ToList()
            gc_Incidents.DataSource = incidents.Select(i => new { i.ID_Incident, i.Name, i.Folio, i.IncidentDate, i.truck.truckNumber, i.ID_StatusDetail });
            //gv_Incidents
        }

        public void LoadCaptures(List<Capture> captures)
        {

        }
        #endregion

   
    }
}