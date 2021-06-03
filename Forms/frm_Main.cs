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

        public frm_Main()
        {
            InitializeComponent();
        }

        private void barEditItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barEditItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _controller.IncidentsFilter();
        }

        private void frm_Main_Load(object sender, EventArgs e)
        {
            _mainView = new Main3();
            _mainCtrl = new MainController(_mainView);
            _mainCtrl.LoadData();
            _controller.SetMainController(_mainCtrl);
            repositoryItemLookUpEdit1.DataSource = StatusDetailService.list_StatusDetail();
            itx.cfrm_InsertForm(_mainView, pnl_Container);
            splashScreenManager1.CloseWaitForm();
        }

        private void pnl_Container_SizeChanged(object sender, EventArgs e)
        {
            itx.RefreshForms(pnl_Container);
        }

        private void barEditItem2_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

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
            _controller.ClearFilters();
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
    }
}