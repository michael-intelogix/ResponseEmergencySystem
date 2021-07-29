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
using ITXFramework;
using ResponseEmergencySystem.Forms;
using ResponseEmergencySystem.Controllers;
using ResponseEmergencySystem.Views;
using ResponseEmergencySystem.Services;
using ResponseEmergencySystem.Code;

namespace ResponseEmergencySystem.Forms
{
    public partial class frm_Main : DevExpress.XtraBars.Ribbon.RibbonForm, IMain2View
    {
        ITXFramework.ITXFramework itx = new ITXFramework.ITXFramework();

        Main2Controller _controller;
        MainController _mainCtrl;
        Main3 _mainView;

        string IMain2View.Date1
        {
            get
            {
                string date1 = bar_DateFrom.EditValue == null ? "" : bar_DateFrom.EditValue.ToString();
                return date1 == "01/01/0001" ? "" : date1;
            }
            set { bar_DateFrom.EditValue = value; }
        }
        string IMain2View.Date2
        {
            get
            {
                string date2 = bar_DateTo.EditValue == null ? "" : bar_DateTo.EditValue.ToString();
                return date2 == "01/01/0001" ? "" : date2;
            }
            set { bar_DateTo.EditValue = value; }
        }
        string IMain2View.Folio
        {
            get => bar_Folio.EditValue == null ? "" : bar_Folio.EditValue.ToString();
            set => bar_Folio.EditValue = value;
        }
        string IMain2View.DriverName
        {
            get => bar_Driver.EditValue == null ? "" : bar_Driver.EditValue.ToString();
            set => bar_Driver.EditValue = value;
        }
        string IMain2View.TruckNumber
        {
            get => bar_TruckNumber.EditValue == null ? "" : bar_TruckNumber.EditValue.ToString();
            set => bar_TruckNumber.EditValue = value;
        }

        string IMain2View.Status
        {
            get => bar_StatusDetail.EditValue == null ? "" : bar_StatusDetail.EditValue.ToString();
            set => bar_StatusDetail.EditValue = value;
        }

        object IMain2View.IncidentsDataSource { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        void IMain2View.OpenSpinner()
        {
            splashScreenManager1.ShowWaitForm();
        }

        void IMain2View.CloseSpinner()
        {
            splashScreenManager1.CloseWaitForm();
        }

        #region View Controls Properties
        bool IMain2View.EnableShowIncidentButton 
        {
            set { barButtonItem7.Enabled = value; }
        }

        bool IMain2View.EnableEditIncidentButton
        {
            set { barButtonItem8.Enabled = value; }
        }

        bool IMain2View.EnableCloseIncidentButton
        {
            set { barButtonItem10.Enabled = value; }
        }

        bool IMain2View.EnableDeleteIncidentButton
        {
            set { barButtonItem11.Enabled = value; }
        }

        bool IMain2View.EnableSaveAllButton
        {
            set { barButtonItem9.Enabled = value; }
        }
        #endregion

        public frm_Main()
        {
            InitializeComponent();
        }

        private void barEditItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _controller.IncidentsFilter();
        }

        private void frm_Main_Load(object sender, EventArgs e)
        {
            _mainView = new Main3();
            IMain2View main2View = this;
            _mainCtrl = new MainController(_mainView, ref main2View);
            //_mainCtrl.LoadData();
            _controller.SetMainController(_mainCtrl);
            repositoryItemLookUpEdit1.DataSource = StatusDetailService.list_StatusDetail();
            itx.cfrm_InsertForm(_mainView, pnl_Container);
            itx.RefreshForms(pnl_Container);

            VehicleService.get_CategoriesIncidentVehicle();

            splashScreenManager1.CloseWaitForm();


        }

        private void pnl_Container_SizeChanged(object sender, EventArgs e)
        {
            itx.RefreshForms(pnl_Container);
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _controller.AddIncident();
        }

        void IMain2View.SetController(Main2Controller controller)
        {
            _controller = controller;
        }

        private void bar_TruckNumber_EditValueChanged(object sender, EventArgs e)
        {
            _controller.IncidentsFilterGrid();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bar_StatusDetail.EditValue = "423E82C9-EE3F-4D83-9066-01E6927FE14D";
            _mainCtrl.ClearFilters();
        }

        private void bar_Folio_EditValueChanged(object sender, EventArgs e)
        {
            _controller.IncidentsFilterGrid();
        }

        private void bar_Driver_EditValueChanged(object sender, EventArgs e)
        {
            _controller.IncidentsFilterGrid();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _controller.Settings();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _controller.AddCaptures();
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _controller.ActionsMenu("close");
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _controller.ActionsMenu("show");
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _controller.ActionsMenu("edit");
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _mainCtrl.SaveStatus(true);
            //_controller.ActionsMenu("status");
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _controller.ActionsMenu("delete");
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            SamsaraService.UpdateSamsaraVehicles();
            splashScreenManager1.CloseWaitForm();
            Utils.ShowMessage("Samsara trailers have been updated", "Trucks Samsara");
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            SamsaraService.UpdateSamsaraDrivers();
            splashScreenManager1.CloseWaitForm();
            Utils.ShowMessage("Samsara drivers have been updated", "Drivers Samsara");
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frm_Logs logsView = new frm_Logs();
            logsView.Show();
        }

        private void frm_Main_SizeChanged(object sender, EventArgs e)
        {
            itx.RefreshForms(pnl_Container);
        }
    }
}