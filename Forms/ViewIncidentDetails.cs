using DevExpress.XtraEditors;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Services;
using ResponseEmergencySystem.Models;
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

        public ViewIncidentDetails(string incidentId)
        {
            InitializeComponent();
            dt_InjuredPersons.Columns.Add("FullName");
            dt_InjuredPersons.Columns.Add("LastName1");
            dt_InjuredPersons.Columns.Add("LastName2");
            dt_InjuredPersons.Columns.Add("Phone");

            addEmptyRow();
            loadData(incidentId);
            
        }

        private void loadData(string incidentId)
        {
       
            Incident result = IncidentService.list_Incidents("", "", "", "", "", incidentId: incidentId)[0];
            edt_FullName.EditValue = result.Name;
            edt_PhoneNumber.EditValue = result.PhoneNumber;
            edt_License.EditValue = result.driver.License;
            dte_ExpirationDate.EditValue = result.driver.ExpirationDate;
            lue_DriverLicenceState.EditValue = result.driver.ID_StateOfExpedition;
            edt_TruckNumber.EditValue = result.truck.truckNumber;
            edt_TrailerNumber.EditValue = result.trailer.TrailerNumber;
            ckedt_truckDamages.Checked = result.TruckDamage;
            ckedt_TruckCanMove.Checked = result.TruckCanMove;
            ckedt_TruckNeedCrane.Checked = result.TruckNeedCrane;
            ckedt_TrailerDamages.Checked = result.TruckDamage;
            ckedt_TrailerCanMove.Checked = result.TruckCanMove;
            ckedt_TrailerNeedCrane.Checked = result.TrailerNeedCrane;
            ckedt_Spill.Checked = result.trailer.CargoSpill;
            edt_Cargo.EditValue = result.trailer.Commodity;
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

        private void ViewIncidentDetails_Load(object sender, EventArgs e)
        {
            lue_DriverLicenceState.Properties.DataSource = Functions.getStates();
        }
    }

}