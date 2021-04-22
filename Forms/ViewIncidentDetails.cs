using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponseEmergencySystem.Forms
{
    // https://stackoverflow.com/questions/1774498/how-to-iterate-through-a-datatable
    public partial class ViewIncidentDetails : DevExpress.XtraEditors.XtraForm
    {
        DataTable dt_InjuredPersons = new DataTable();

        public ViewIncidentDetails()
        {
            InitializeComponent();
            dt_InjuredPersons.Columns.Add("FullName");
            dt_InjuredPersons.Columns.Add("LastName1");
            dt_InjuredPersons.Columns.Add("LastName2");
            dt_InjuredPersons.Columns.Add("Phone");

            addEmptyRow();
            //DataRow _data1 = dtInjured.NewRow();
            //_data1["Name"] = "holi";
            //_data1["Number"] = "1";
            //dtInjured.Rows.Add(_data1);
            //DataRow _data2 = dtInjured.NewRow();
            //_data2["Name"] = "hey";
            //_data2["Number"] = "2";
            //dtInjured.Rows.Add(_data2);
            //lookUpEdit1.Properties.DataSource = dtInjured;
        }

        private void CreateInjuredpersons(Int16 numOfRows)
        {
            Int16 currentRow = 0;
            while (currentRow <= numOfRows)
            {
                addEmptyRow();
                currentRow++;
            }
            

            //Task.Delay(1000);
            //return Task.FromResult("ok");
        }


        private void OnChangedCheckEdit(object sender, EventArgs e)
        {
            CheckEdit cb = (CheckEdit)sender;
            bool ckedtValue = (bool)cb.EditValue;
            cb.Properties.Caption = ckedtValue ? "Yes" : "No";

            if (cb.Name == "ckedt_Spill")
            {
                //pnl_BOL.Visible = ckedtValue;
            }

            if (cb.Name == "ckedt_Injured")
            {
                //pnl_AddInjuredFields.Visible = ckedtValue;
            }
        }

        private void refreshInjuredPersonsTable()
        {
            gc_InjuredPersons.DataSource = dt_InjuredPersons;
        }

        private void addEmptyRow()
        {
            DataRow _data = dt_InjuredPersons.NewRow();
            _data["FullName"] = "";
            _data["LastName1"] = "";
            _data["LastName2"] = "";
            _data["Phone"] = "";
            dt_InjuredPersons.Rows.Add(_data);
            refreshInjuredPersonsTable();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            CreateInjuredpersons(Convert.ToInt16(textEdit1.EditValue));
            //addEmptyRow();

        }

        private void btn_DeleteRowClick(object sender, EventArgs e)
        {
            Int32 index = gv_InjuredPersons.FocusedRowHandle;

            gv_InjuredPersons.DeleteRow(index);
        }
    }

}