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

            //private static SqlConnection cn(string database = "General")
            //{
            //    SqlConnection sqcn = new SqlConnection(
            //        //$@"Server=35.223.136.179,1433;Initial Catalog={database}; Persist Security Info = True; User ID = sqluser; Password = Int3logix20.-");
            //        $@"Server=DESKTOP-G404ISP\INTELOGIX205;Initial Catalog={database}; Persist Security Info = True; User ID = Yearim; Password = Taekwondo84 ");
            //    if (sqcn.State == ConnectionState.Open)
            //    {
            //        sqcn.Close();
            //    }
            //    sqcn.Open();
            //    return sqcn;
            //}

            constants.EmilioConn.Open();
            if (constants.EmilioConn.State == ConnectionState.Open)
            {
                Debug.WriteLine("IT'S ALIVEEEEEEEEEEEEEEEEEEEE");
            }

            DataTable states = Functions.getStates();
            DataRow state = states.Select().First();
            Debug.WriteLine(state.ItemArray[2]);
             
            if (login.ShowDialog() == DialogResult.OK)
            {
                access = login.myData;
                string idmysoftware = "2a5aa42b-2089-4fa8-b7cc-2cea2a017a8a";
                DataRow[] accesos = access.Select($"ID_Software = '{idmysoftware}'");
                if (accesos.Length > 0)
                {   
                    constants.userName = accesos[0].ItemArray[13].ToString();
                }
            }


        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            AddIncidentDetails capture = new AddIncidentDetails();

            capture.ShowDialog();
            
        }

        private void gv_Incidents_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            constants.id_capture = Guid.Parse(gv_Incidents.GetFocusedRowCellValue("ID_Capture").ToString());

            frm_Incident_Captures driver = new frm_Incident_Captures();

            driver.Show();
        }

        private void btn_View_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            ExtraForm test = new ExtraForm();
           
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            frm_Incident_Captures captures = new frm_Incident_Captures();
            captures.Show();
        }
    }
}
