using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResponseEmergencySystem.Code;
using DevExpress.XtraEditors;
using System.IO;

namespace ResponseEmergencySystem.Forms
{
    public partial class Main : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {

        DataTable access = new DataTable();
         
        public Main()
        {
            InitializeComponent();
        }

        private void IncidentReport_Load(object sender, EventArgs e)
        {

            Login login = new Login();

            DataTable states = Functions.getStates();
            DataRow state = states.Select().First();

            gc_Incidents.DataSource = Functions.list_Incidents("", "", "", "", "");
            lue_StatusDetail.DataSource = Functions.list_StatusDetail();

            //var namefile = "test";
            //string ReportPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"{namefile}.pdf");
            //Utils.email_send(ReportPath, false);
             
            //if (login.ShowDialog() == DialogResult.OK)
            //{
            //    access = login.myData;
            //    string idmysoftware = "2a5aa42b-2089-4fa8-b7cc-2cea2a017a8a";
            //    DataRow[] accesos = access.Select($"ID_Software = '{idmysoftware}'");
            //    if (accesos.Length > 0)
            //    {   
            //        constants.userName = accesos[0].ItemArray[13].ToString();
            //    }
            //}


        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            AddIncidentDetails capture = new AddIncidentDetails();

            capture.ShowDialog();
            
        }

        //private void gv_Incidents_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        //{
        //    constants.id_capture = Guid.Parse(gv_Incidents.GetFocusedRowCellValue("ID_Capture").ToString());

        //    frm_Incident_Captures driver = new frm_Incident_Captures();

        //    driver.Show();
        //}

        private void btn_View_ButtonClick(object sender, EventArgs e)
        {
            Int32 index = gv_Incidents.FocusedRowHandle;
            string incidentId = gv_Incidents.GetRowCellValue(index, "ID_Incident").ToString();
            ViewIncidentDetails viewIncident = new ViewIncidentDetails(incidentId);
            viewIncident.Show();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            frm_Incident_Captures captures = new frm_Incident_Captures();
            captures.Show();
        }

        private void dte_EndDate_EditValueChanged(object sender, EventArgs e)
        {
            dte_EndDate.Visible = (bool)ckedt_EndDate.EditValue;
        }

        private void dte_EndDate_EditValueChanged_1(object sender, EventArgs e)
        {
            
           
            
        }

        private void dte_StartDate_EditValueChanged(object sender, EventArgs e)
        {
            
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string truckNum = Utils.GetEdtValue(edt_TruckNumber);
            string dFirstName = Utils.GetEdtValue(edt_DriverName);
            string folio = Utils.GetEdtValue(edt_Folio);
            
            if (dte_StartDate.EditValue != null && dte_EndDate.EditValue == null)
            {
                string date1 = ((DateTime)dte_StartDate.EditValue).Year.ToString() + '-' + ((DateTime)dte_StartDate.EditValue).Month.ToString() + '-' + ((DateTime)dte_StartDate.EditValue).Day.ToString();
                gc_Incidents.DataSource = Functions.list_Incidents(folio, "", dFirstName, truckNum, "", date1: date1);
            }

            if (dte_StartDate.EditValue != null && dte_EndDate.EditValue != null)
            {
                string date1 = ((DateTime)dte_StartDate.EditValue).Year.ToString() + '-' + ((DateTime)dte_StartDate.EditValue).Month.ToString() + '-' + ((DateTime)dte_StartDate.EditValue).Day.ToString();
                string date2 = ((DateTime)dte_EndDate.EditValue).Year.ToString() + '-' + ((DateTime)dte_EndDate.EditValue).Month.ToString() + '-' + ((DateTime)dte_EndDate.EditValue).Day.ToString();
                gc_Incidents.DataSource = Functions.list_Incidents(folio, "", dFirstName, truckNum, "", date1: date1, date2: date2);
            }
        }

        private void find_OnEdtKeyPress(object sender, KeyPressEventArgs e)
        {
            string truckNum = Utils.GetEdtValue(edt_TruckNumber);
            string dFirstName = Utils.GetEdtValue(edt_DriverName);
            string folio = Utils.GetEdtValue(edt_Folio);

            if (dte_StartDate.EditValue != null && dte_EndDate.EditValue == null)
            {
                string date1 = ((DateTime)dte_StartDate.EditValue).Year.ToString() + '-' + ((DateTime)dte_StartDate.EditValue).Month.ToString() + '-' + ((DateTime)dte_StartDate.EditValue).Day.ToString();
                gc_Incidents.DataSource = Functions.list_Incidents(folio, "", dFirstName, truckNum, "", date1: date1);
            }
            else if (dte_StartDate.EditValue != null && dte_EndDate.EditValue != null)
            {
                string date1 = ((DateTime)dte_StartDate.EditValue).Year.ToString() + '-' + ((DateTime)dte_StartDate.EditValue).Month.ToString() + '-' + ((DateTime)dte_StartDate.EditValue).Day.ToString();
                string date2 = ((DateTime)dte_EndDate.EditValue).Year.ToString() + '-' + ((DateTime)dte_EndDate.EditValue).Month.ToString() + '-' + ((DateTime)dte_EndDate.EditValue).Day.ToString();
                gc_Incidents.DataSource = Functions.list_Incidents(folio, "", dFirstName, truckNum, "", date1: date1, date2: date2);
            }
            else
            {
                gc_Incidents.DataSource = Functions.list_Incidents(folio, "", dFirstName, truckNum, "");
            }
        }
    }
}
