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
using ResponseEmergencySystem.Views.Incidents.Containers;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Controllers.Incidents;

namespace ResponseEmergencySystem.Forms.Incidents.containers
{
    public partial class Vehicles : DevExpress.XtraEditors.XtraForm, IVehiclesView
    {
        DriverIncidentController _controller;

        public Vehicles()
        {
            InitializeComponent();
        }

        #region interface inputs
        public string VehicleName
        {
            get => Utils.GetEdtValue(edt_Name);
            set => edt_Name.EditValue = value;
        }

        public string VinNumber 
        { 
            get => Utils.GetEdtValue(edt_VinNumber);
            set => edt_VinNumber.EditValue = value;
        }
        public string Serialnumber 
        {
            get => Utils.GetEdtValue(edt_SerialNumber);
            set => edt_SerialNumber.EditValue = value; 
        }
        public string Make {
            get => Utils.GetEdtValue(edt_Make);
            set => edt_Make.EditValue = value; 
        }
        public string Model 
        {
            get => Utils.GetEdtValue(edt_Model);
            set => edt_Model.EditValue = value; 
        }
        public string Year 
        {
            get => Utils.GetEdtValue(edt_Year);
            set => edt_Year.EditValue = value; 
        }
        public string LicensePlate 
        {
            get => Utils.GetEdtValue(edt_LicensePlate);
            set => edt_LicensePlate.EditValue = value; 
        }

        public string ID_Vehicle
        {
            get => lue_Vehicles.EditValue == null ? "" : lue_Vehicles.EditValue.ToString();
            set => lue_Vehicles.EditValue = value;
        }
        #endregion

        #region interface properties
        public object VehiclesDataSource
        {
            set { lue_Vehicles.Properties.DataSource = value; }
        }
        #endregion

        #region interface methods
        public void SetController(DriverIncidentController controller)
        {
            _controller = controller;
        }
        #endregion

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            edt_Name.EditValue = "";
            edt_Name.ReadOnly = false;
            edt_VinNumber.EditValue = "";
            edt_VinNumber.ReadOnly = false;
            edt_SerialNumber.EditValue = "";
            edt_SerialNumber.ReadOnly = false;
            edt_Make.EditValue = "";
            edt_Make.ReadOnly = false;
            edt_Year.EditValue = "";
            edt_Year.ReadOnly = false;
            edt_Model.EditValue = "";
            edt_Model.ReadOnly = false;
            edt_LicensePlate.EditValue = "";
            edt_LicensePlate.ReadOnly = false;

            lue_Vehicles.ReadOnly = true;
            btn_AddVehicle.Visible = false;
            btn_SaveVehicle.Visible = true;
        }

        private void gridLookUpEdit1_Properties_EditValueChanged(object sender, EventArgs e)
        {
            GridLookUpEdit view = (GridLookUpEdit)sender;
            try
            {
                _controller.GetVehicle(view.EditValue.ToString());
            }
            catch (Exception ex)
            {
                Utils.ShowMessage(ex.Message, "Error Employee", type: "Error");
            }
        }

        private void Vehicles_Load(object sender, EventArgs e)
        {
            _controller.SetVehiclesView(this);
            _controller.LoadVehicles();
        }

        private void btn_SaveVehicle_Click(object sender, EventArgs e)
        {
            _controller.AddVehicle();
            lue_Vehicles.ReadOnly = false;
        }

        private void btn_UpdateVinNumber_Click(object sender, EventArgs e)
        {
            edt_VinNumber.ReadOnly = false;
            edt_VinNumber.EditValue = "";
            edt_VinNumber.Focus();
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            edt_SerialNumber.ReadOnly = false;
            edt_SerialNumber.EditValue = "";
            edt_SerialNumber.Focus();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            edt_Model.ReadOnly = false;
            edt_Model.EditValue = "";
            edt_Model.Focus();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            edt_LicensePlate.ReadOnly = false;
            edt_LicensePlate.EditValue = "";
            edt_LicensePlate.Focus();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            edt_Make.ReadOnly = false;
            edt_Make.EditValue = "";
            edt_Make.Focus();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            edt_Year.ReadOnly = false;
            edt_Year.EditValue = "";
            edt_Year.Focus();
        }

        private void edt_VinNumber_Leave(object sender, EventArgs e)
        {
            _controller.UpdateVehicleInfo(Utils.GetEdtValue((TextEdit)sender), ((TextEdit)sender).Name);
            ((TextEdit)sender).ReadOnly = true;
        }

        private void edt_SerialNumber_Leave(object sender, EventArgs e)
        {
            _controller.UpdateEmployeeInfo(Utils.GetEdtValue((TextEdit)sender), ((TextEdit)sender).Name);
            ((TextEdit)sender).ReadOnly = true;
        }

        private void edt_Make_Leave(object sender, EventArgs e)
        {
            _controller.UpdateEmployeeInfo(Utils.GetEdtValue((TextEdit)sender), ((TextEdit)sender).Name);
            ((TextEdit)sender).ReadOnly = true;
        }

        private void edt_Model_Leave(object sender, EventArgs e)
        {
            _controller.UpdateEmployeeInfo(Utils.GetEdtValue((TextEdit)sender), ((TextEdit)sender).Name);
            ((TextEdit)sender).ReadOnly = true;
        }

        private void edt_Year_Leave(object sender, EventArgs e)
        {
            _controller.UpdateEmployeeInfo(Utils.GetEdtValue((TextEdit)sender), ((TextEdit)sender).Name);
            ((TextEdit)sender).ReadOnly = true;
        }

        private void edt_LicensePlate_Leave(object sender, EventArgs e)
        {
            _controller.UpdateEmployeeInfo(Utils.GetEdtValue((TextEdit)sender), ((TextEdit)sender).Name);
            ((TextEdit)sender).ReadOnly = true;
        }
    }
}