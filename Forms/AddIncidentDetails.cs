﻿using DevExpress.XtraEditors;
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
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Properties;
using ResponseEmergencySystem.Controllers;
using ResponseEmergencySystem.Forms.Modals;
using ResponseEmergencySystem.Services;
using ResponseEmergencySystem.Views;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.IO;
using ResponseEmergencySystem.Models;

namespace ResponseEmergencySystem.Forms
{
    
    public partial class AddIncidentDetails : XtraForm, IAddIncidentView
    {
        // driver E2E7FBBB-6BF8-414A-B160-1A4EE294DC97
        // driver2 C7B06EF3-869B-4212-A1EC-7820B2D17CA4
        
        private DataTable dt_InjuredPersons;

        public AddIncidentDetails()
        {
            InitializeComponent();
        }

        Controllers.Incidents.AddIncidentController _controller;

        private void btn_AddRowsClick(object sender, EventArgs e)
        {
            //Int16 currentRow = 0;
            //while (currentRow < Convert.ToInt16(edt_NumberOfInjured.EditValue))
            //{
            //    _controller.addEmptyRow();
            //    currentRow++;
            //}

        }

        private void btn_DeleteRowClick(object sender, EventArgs e)
        {
            //Int32 index = gv_InjuredPersons.FocusedRowHandle;

            //DialogResult result = MessageBox.Show(
            //    "Are you sure you want to delete this row?", 
            //    "Delete injured person row", 
            //    MessageBoxButtons.OKCancel, 
            //    MessageBoxIcon.Information);
            //if (result.Equals(DialogResult.OK))
            //{
            //    gv_InjuredPersons.DeleteRow(index);
            //}
        }

        private void IncidentCapture_Load(object sender, EventArgs e)
        {
            //_controller.CreateInjuredPersonsTable();
        }

        private void btn_AddIncident_Click(object sender, EventArgs e)
        {
            if (dxValidationProvider1.Validate())
                MessageBox.Show("Incorrect Data");
            else
                _controller.AddIncident();
            
        }
       
        private void AddIncidentDetails_Shown(object sender, EventArgs e)
        { 
        }
        
        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            _controller.SetBroker();
        }


        #region view interface
        public void SetController(Controllers.Incidents.AddIncidentController controller)
        {
            _controller = controller;
        }

        public void LoadIncident(Incident incident)
        {
        }

        public void LoadStates(DataTable dt_States)
        {
            lue_StateExp.Properties.DataSource = dt_States;
            lue_DriverLicenseState.Properties.DataSource = dt_States;
        }


        public void LoadCities(DataTable dt_Cities)
        {
            lue_Cities.Properties.DataSource = dt_Cities;
        }

        public void LoadInjuredPersons(DataTable dt_InjuredPersons)
        {
        }

        #region events needed
        public void checkNumber_OnEdtKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                TextEdit edt_Number = (TextEdit)sender;
                _controller.CheckNumber(edt_Number.Name);
            }
        }

        public void checkNumber_OnEdtLeave(object sender, EventArgs e)
        {
            TextEdit edt_Number = (TextEdit)sender;
            _controller.CheckNumber(edt_Number.Name);
        }

        public void FindTruckSamsara_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            _controller.GetTruckSamsara();
            splashScreenManager1.CloseWaitForm();
        }

        public void Ckedt_OnValueChanged(object sender, EventArgs e)
        {
            CheckEdit cb = (CheckEdit)sender;

            _controller.CheckEditChanged(cb.Name, (bool)cb.EditValue);
        }

        public void OnStateEditValueChanged(object sender, EventArgs e)
        {
            _controller.GetCitiesByState();
        }

        #endregion

        #region form inputs
        public string DriverInfoSearch
        {
            get { return Utils.GetEdtValue(edt_SearchDriver); }
        }

        public string FullName
        {
            get { return Utils.GetEdtValue(edt_FullName); }
            set { edt_FullName.EditValue = value; }
        }

        public string PhoneNumber
        {
            get { return Utils.GetEdtValue(edt_PhoneNumber); }
            set { edt_PhoneNumber.EditValue = value; }
        }

        public string License
        {
            get { return Utils.GetEdtValue(edt_License); }
            set { edt_License.EditValue = value; }
        }

        public DateTime ExpirationDate
        {
            get { return dte_ExpirationDate.DateTime; }
            set { dte_ExpirationDate.EditValue = value; }
        }

        public string LicenseState
        {
            get { return lue_DriverLicenseState.EditValue.ToString(); }
            set { lue_DriverLicenseState.EditValue = value; }
        }

        public string LocationReferences
        {
            get { return Utils.GetEdtValue(edt_Highway); }
            set { edt_Highway.EditValue = value; }
        }

        public string TruckNumber
        {
            get { return Utils.GetEdtValue(edt_TruckNumber); }
            set { edt_TruckNumber.EditValue = value; }
        }

        public bool TruckDamages
        {
            get { return (bool)ckedt_truckDamages.EditValue; }
            set { ckedt_truckDamages.EditValue = value; }
        }

        public bool TruckCanMove
        {
            get { return (bool)ckedt_TruckCanMove.EditValue; }
            set { ckedt_TruckCanMove.EditValue = value; }
        }

        public bool TruckNeedCrane
        {
            get { return (bool)ckedt_TruckNeedCrane.EditValue; }
            set { ckedt_TruckNeedCrane.EditValue = value; }
        }

        public string TrailerNumber
        {
            get { return Utils.GetEdtValue(edt_TrailerNumber); }
            set { edt_TrailerNumber.EditValue = value; }
        }

        public bool TrailerDamages
        {
            get { return (bool)ckedt_TrailerDamages.EditValue; }
            set { ckedt_TrailerDamages.EditValue = value; }
        }

        public bool TrailerCanMove
        {
            get { return (bool)ckedt_TrailerCanMove.EditValue; }
            set { ckedt_TrailerCanMove.EditValue = value; }
        }

        public bool TrailerNeedCrane
        {
            get { return (bool)ckedt_TrailerNeedCrane.EditValue; }
            set { ckedt_TrailerNeedCrane.EditValue = value; }
        }

        public string CargoType
        {
            get { return Utils.GetEdtValue(edt_Cargo); }
            set { edt_Cargo.EditValue = value; }
        }

        public bool CargoSpill
        {
            get { return (bool)ckedt_Spill.EditValue; }
            set { ckedt_Spill.EditValue = value; }
        }

        public string ManifestNumber
        {
            get { return Utils.GetEdtValue(edt_manifest); }
            set { edt_manifest.EditValue = value; }
        }

        public string Broker
        {
            get { return Utils.GetEdtValue(edt_Broker); }
            set { edt_Broker.EditValue = value; }
        }

        public DateTime IncidentDate {
            get { return new DateTime(dte_IncidentDate.DateTime.Ticks + tme_IncidentTime.Time.Ticks); } 
        }

        public bool PoliceReport
        {
            get { return (bool)ckedt_PoliceReport.EditValue; }
            set { ckedt_PoliceReport.EditValue = value; }
        }

        public string CitationReportNumber
        {
            get { return Utils.GetEdtValue(edt_PoliceReport); }
            set { edt_PoliceReport.EditValue = value; }
        }

        public string Latitude
        {
            get { return Utils.GetEdtValue(edt_Latitude); }
            set { edt_Latitude.EditValue = value; }
        }

        public string Longitude
        {
            get { return Utils.GetEdtValue(edt_Longitude); }
            set { edt_Longitude.EditValue = value; }
        }

        public string ID_State
        {
            get { return lue_StateExp.EditValue.ToString(); }
            set { lue_StateExp.EditValue = value; }
        }

        public string ID_City
        {
            get { return lue_Cities.EditValue.ToString(); }
            set { lue_Cities.EditValue = value; }
        }

        public string Comments
        {
            get { return memoEdit1.EditValue.ToString() == null ? "" : memoEdit1.EditValue.ToString(); }
        }
        #endregion

        #region Form properties
        public bool PnlBolVisibility
        {
            set { pnl_BOL.Visible = value; }
        }

        public bool PnlPoliceReportVisibility
        {
            set { pnl_PoliceReport.Visible = value; }
        }

        public bool LblTruckExistsVisibility
        {
            set { lbl_TruckExists.Visible = value; }
        }

        public bool LblTrailerExistsVisibility
        {
            set { lbl_TrailerExists.Visible = value; }
        }

        public object LueCitiesDataSource
        {
            set { lue_Cities.Properties.DataSource = value; }
        }
        #endregion

        #endregion
        private void ViewIncidentDetails_Load(object sender, EventArgs e)
        {
            lue_StateExp.Properties.DataSource = Functions.getStates();
        }

        private void btn_AddComments_Click(object sender, EventArgs e)
        {
            _controller.SetComments();
        }

        private void edt_Broker_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btn_FindDriver_Click(object sender, EventArgs e)
        {
            _controller.GetDriver();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            _controller.SetBroker();
        }

        private void edt_SearchDriver_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                _controller.GetDriver();
            }
        }
    }

}