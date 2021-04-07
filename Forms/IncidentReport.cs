using DevExpress.XtraBars;
using DevExpress.XtraGrid.Views.Grid;
using ResponseEmergencySystem.Entity_Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ResponseEmergencySystem.Forms
{
    public partial class IncidentReport : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
         
        public IncidentReport()
        {
            InitializeComponent();
        }

        private void IncidentReport_Load(object sender, EventArgs e)
        {
            using (var context = new SIREMLocalEntities())
            {
                var courses = context.List_Status_Detail();

                lue_test.DataSource = context.List_Status_Detail().ToHashSet<List_Status_Detail_Result>();
                //foreach (var cs in courses)
                //    Console.WriteLine(cs.ID_Status_Detail);
            }

            using (var context = new SIREMLocalEntities())
            {
                var courses = context.List_Status_Detail();

                gc_Incidents.DataSource = context.List_Incidents("", "", "", "","","");
                //Console.WriteLine(Guid.Empty.ToString());
            }

            //Connection.Connection.List_Incidents();
            //gc_Incidents.DataSource = Connection.Connection.Dt_Incidents;

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            using (var context = new SIREMLocalEntities())
            {
                // var courses = context.List_Status_Detail();

                var response = context.Update_Status_Detail(null, "test884354513654"); 
                Console.WriteLine(response.ToList<Update_Status_Detail_Result>()[0].msg);
            }
        }

        private void gv_Incidents_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            Console.WriteLine((sender as GridView).GetRowCellValue((sender as GridView).FocusedRowHandle, "ID_Incident").ToString());
        }
    }
}
