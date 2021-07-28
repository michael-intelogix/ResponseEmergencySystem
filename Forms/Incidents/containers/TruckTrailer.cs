using DevExpress.XtraEditors;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Controllers.Incidents;
using ResponseEmergencySystem.Views.Incidents.Containers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponseEmergencySystem.Forms.Incidents.containers
{
    public partial class TruckTrailer : DevExpress.XtraEditors.XtraForm, ITrucksTrailersView
    {
        DriverIncidentController _parentController;

        public TruckTrailer()
        {
            InitializeComponent();
        }

        #region View Methods
        public void SetController(DriverIncidentController parentCtrl)
        {
            _parentController = parentCtrl;
        }
        #endregion

        #region IDs
        public string ID_Truck
        {
            get => lue_Trucks.EditValue == null ? "" : lue_Trucks.EditValue.ToString();
            set => lue_Trucks.EditValue = value;
        }

        public string ID_Trailer
        {
            get => lue_Trailers.EditValue == null ? "" : lue_Trailers.EditValue.ToString();
            set => lue_Trailers.EditValue = value;
        }
        #endregion

        #region Truck Information
        public string TruckName
        {
            get => Utils.GetEdtValue(edt_TruckName);
            set => edt_TruckName.EditValue = value;
        }

        public string TruckVinNumber
        {
            get => Utils.GetEdtValue(edt_TruckVinNumber);
            set => edt_TruckVinNumber.EditValue = value;
        }

        public string TruckSerialNumber
        {
            get => Utils.GetEdtValue(edt_TruckSerialNumber);
            set => edt_TruckSerialNumber.EditValue = value;
        }

        public string TruckMake
        {
            get => Utils.GetEdtValue(edt_TruckMake);
            set => edt_TruckMake.EditValue = value;
        }

        public string TruckModel
        {
            get => Utils.GetEdtValue(edt_TruckModel);
            set => edt_TruckModel.EditValue = value;
        }

        public string TruckYear
        {
            get => Utils.GetEdtValue(edt_TruckYear);
            set => edt_TruckYear.EditValue = value;
        }

        public string TruckLicensePlate
        {
            get => Utils.GetEdtValue(edt_TruckLicensePlate);
            set => edt_TruckLicensePlate.EditValue = value;
        }
        #endregion

        #region Trailer Information
        public string TrailerName
        {
            get => Utils.GetEdtValue(edt_TrailerName);
            set => edt_TrailerName.EditValue = value;        
        }

        public string TrailerCargoType
        {
            get => Utils.GetEdtValue(edt_TrailerCargoType);
            set => edt_TrailerCargoType.EditValue = value;
        }

        public string TrailerVinNumber
        {
            get => Utils.GetEdtValue(edt_TrailerVinNumber);
            set => edt_TrailerVinNumber.EditValue = value;
        }

        public string TrailerSerialNumber
        {
            get => Utils.GetEdtValue(edt_TrailerSerialNumber);
            set => edt_TrailerSerialNumber.EditValue = value;
        }

        public string TrailerMake
        {
            get => Utils.GetEdtValue(edt_TrailerMake);
            set => edt_TrailerMake.EditValue = value;
        }

        public string TrailerModel
        {
            get => Utils.GetEdtValue(edt_TrailerModel);
            set => edt_TrailerModel.EditValue = value;
        }

        public string TrailerYear
        {
            get => Utils.GetEdtValue(edt_TrailerYear);
            set => edt_TrailerYear.EditValue = value;
        }

        public string TrailerLicensePlate
        {
            get => Utils.GetEdtValue(edt_TrailerLicensePlate);
            set => edt_TrailerLicensePlate.EditValue = value;
        }
        #endregion

        #region view properties
        public object TrucksDataSource 
        {
            set => lue_Trucks.Properties.DataSource = value;
        }

        public object TrailersDataSource 
        {
            set => lue_Trailers.Properties.DataSource = value;
        }

        public bool IsNew
        {
            get => (bool)ckedt_New.EditValue;
        }
        #endregion

        private void lue_Trucks_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {

            //splashScreenManager1.ShowWaitForm();
            //var res = _controller.GetTruckSamsara();
            //gMapControl1.Position = new GMap.NET.PointLatLng(res[0], res[1]);

            //GMap.NET.WindowsForms.GMapOverlay markers = new GMap.NET.WindowsForms.GMapOverlay("markers");
            //GMap.NET.WindowsForms.GMapMarker marker = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
            //                                                new GMap.NET.PointLatLng(res[0], res[1]),
            //GMap.NET.WindowsForms.Markers.GMarkerGoogleType.red_dot);
            //gMapControl1.Overlays.Clear();
            //markers.Markers.Add(marker);
            //gMapControl1.Overlays.Add(markers);

            //splashScreenManager1.CloseWaitForm();
        }

        private void lue_Trucks_KeyDown(object sender, KeyEventArgs e)
        {
            GridLookUpEdit edit = sender as GridLookUpEdit;
            if (e.KeyData == Keys.Down || e.KeyData == Keys.Up)
            {
                if (!edit.IsPopupOpen)
                {
                    e.Handled = true;
                }
            }
        }

        private void btn_Broker2_Click(object sender, EventArgs e)
        {

        }

        private void Ckedt_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void btn_SaveVehicle_Click(object sender, EventArgs e)
        {
            pnl_TruckStatus.Visible = true;
            pnl_TruckInfo.Visible = false;
            lue_Trucks.ReadOnly = false;

            btn_UpdateTruck.Enabled = true;
            btn_AddTruck.Enabled = true;

            if (!edt_TruckName.ReadOnly)
                edt_TruckName.ReadOnly = true;

            if ((bool)ckedt_New.EditValue)
            {
                //_parentController.AddTruck();
                ckedt_New.EditValue = false;
            }
            //else
                //_parentController.UpdateTruckInfo();
        }

        private void btn_UpdateVinNumber_Click(object sender, EventArgs e)
        {
            pnl_TruckStatus.Visible = false;
            pnl_TruckInfo.Visible = true;

            btn_UpdateTruck.Enabled = false;
            btn_AddTruck.Enabled = false;
            lue_Trucks.ReadOnly = true;

            //_parentController.SetTruckInfo();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            pnl_TruckStatus.Visible = false;
            pnl_TruckInfo.Visible = true;

            btn_AddTruck.Enabled = false;
            btn_UpdateTruck.Enabled = false;

            edt_TruckName.ReadOnly = false;

            ckedt_New.EditValue = true;

            //_parentController.Newtruck();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            pnl_TrailerStatus.Visible = false;
            pnl_TrailerInfo.Visible = true;

            btn_UpdateTrailer.Enabled = false;
            btn_AddTrailer.Enabled = false;
            lue_Trailers.ReadOnly = true;

            //_parentController.SetTrailerInfo();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            pnl_TrailerStatus.Visible = true;
            pnl_TrailerInfo.Visible = false;

            lue_Trailers.ReadOnly = false;

            btn_UpdateTrailer.Enabled = true;
            btn_AddTrailer.Enabled = true;

            if (!edt_TrailerName.ReadOnly)
                edt_TrailerName.ReadOnly = true;

            if ((bool)ckedt_New.EditValue)
            {
                //_parentController.AddTrailer();
                ckedt_New.EditValue = false;
            }
            //else
                //_parentController.UpdateTrailerInfo();
        }

        private void TruckTrailer_Load(object sender, EventArgs e)
        {
            _parentController.SetTruckTrailerView(this);
            _parentController.LoadTrucks();
            //_parentController.LoadTrailers();
        }

        private void btn_Broker1_Click(object sender, EventArgs e)
        {
            //edt_Broker.EditValue = _parentController.GetBroker();
        }

        private void btn_AddTrailer_Click(object sender, EventArgs e)
        {
            pnl_TrailerStatus.Visible = false;
            pnl_TrailerInfo.Visible = true;

            btn_AddTrailer.Enabled = false;
            btn_UpdateTrailer.Enabled = false;
            
            edt_TrailerName.ReadOnly = false;

            ckedt_New.EditValue = true;

            //_parentController.NewTrailer();
        }
    }
}