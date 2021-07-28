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
using ResponseEmergencySystem.Builders;

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

        private void gridLookUpEdit1_Properties_EditValueChanged(object sender, EventArgs e)
        {
            GridLookUpEdit view = (GridLookUpEdit)sender;
            try
            {
                //_controller.GetVehicle(view.EditValue.ToString());
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

            if (((List<Vehicle>)lue_Vehicles.Properties.DataSource).Count == 0)
            {
                btn_UpdateVehicle.Enabled = false;
                lue_Vehicles.ReadOnly = true;
            }
        }

        private void btn_UpdateVehicle_Click(object sender, EventArgs e)
        {
            pnl_VehicleStatus.Visible = false;
            pnl_VehicleInformation.Visible = true;

            btn_UpdateVehicle.Enabled = false;
            btn_AddVehicle.Enabled = false;
            lue_Vehicles.ReadOnly = true;

            //_parentController.SetTruckInfo();
        }

        private void btn_SaveVehicle_Click_1(object sender, EventArgs e)
        {
            pnl_VehicleStatus.Visible = true;
            pnl_VehicleInformation.Visible = false;
            lue_Vehicles.ReadOnly = false;

            btn_UpdateVehicle.Enabled = true;
            btn_AddVehicle.Enabled = true;

            if (!edt_Name.ReadOnly)
                edt_Name.ReadOnly = true;

            if ((bool)ckedt_New.EditValue)
            {
                _controller.AddVehicle();
                ckedt_New.EditValue = false;
            }
            //else
            //    _parentController.UpdateTruckInfo();

            if (((List<Vehicle>)lue_Vehicles.Properties.DataSource).Count == 0)
            {
                btn_UpdateVehicle.Enabled = false;
                lue_Vehicles.ReadOnly = true;
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            pnl_VehicleStatus.Visible = true;
            pnl_VehicleInformation.Visible = false;
            lue_Vehicles.ReadOnly = false;

            btn_UpdateVehicle.Enabled = true;
            btn_AddVehicle.Enabled = true;

            if (!edt_Name.ReadOnly)
                edt_Name.ReadOnly = true;

            if (((List<Vehicle>)lue_Vehicles.Properties.DataSource).Count == 0)
            {
                btn_UpdateVehicle.Enabled = false;
                lue_Vehicles.ReadOnly = true;
            }
        }

        private void btn_AddVehicle_Click(object sender, EventArgs e)
        {
            pnl_VehicleStatus.Visible = false;
            pnl_VehicleInformation.Visible = true;

            lue_Vehicles.ReadOnly = true;

            btn_AddVehicle.Enabled = false;
            btn_UpdateVehicle.Enabled = false;

            edt_Name.ReadOnly = false;

            ckedt_New.EditValue = true;

            _controller.NewVehicle();

            if (((List<Vehicle>)lue_Vehicles.Properties.DataSource).Count > 0)
            {
                btn_UpdateVehicle.Enabled = true;
                lue_Vehicles.ReadOnly = false;
            }
        }
    }
}