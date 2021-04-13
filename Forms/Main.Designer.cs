
namespace ResponseEmergencySystem.Forms
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.fluentDesignFormContainer1 = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer();
            this.gc_Incidents = new DevExpress.XtraGrid.GridControl();
            this.gv_Incidents = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID_Incident = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ID_Driver = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ID_Capture = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Driver_Name = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Incident_No = new DevExpress.XtraGrid.Columns.GridColumn();
            this.StatusDetail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lue_test = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.fluentFormDefaultManager1 = new DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager(this.components);
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.accordionControl1 = new DevExpress.XtraBars.Navigation.AccordionControl();
            this.accordionControlElement1 = new DevExpress.XtraBars.Navigation.AccordionControlElement();
            this.fluentDesignFormControl1 = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl();
            this.fluentDesignFormContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_Incidents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Incidents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lue_test)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentFormDefaultManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // fluentDesignFormContainer1
            // 
            this.fluentDesignFormContainer1.Controls.Add(this.gc_Incidents);
            this.fluentDesignFormContainer1.Controls.Add(this.panelControl1);
            this.fluentDesignFormContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fluentDesignFormContainer1.Location = new System.Drawing.Point(40, 27);
            this.fluentDesignFormContainer1.Name = "fluentDesignFormContainer1";
            this.fluentDesignFormContainer1.Size = new System.Drawing.Size(643, 442);
            this.fluentDesignFormContainer1.TabIndex = 0;
            // 
            // gc_Incidents
            // 
            this.gc_Incidents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_Incidents.Location = new System.Drawing.Point(0, 0);
            this.gc_Incidents.MainView = this.gv_Incidents;
            this.gc_Incidents.MenuManager = this.fluentFormDefaultManager1;
            this.gc_Incidents.Name = "gc_Incidents";
            this.gc_Incidents.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.lue_test,
            this.repositoryItemButtonEdit1});
            this.gc_Incidents.Size = new System.Drawing.Size(643, 399);
            this.gc_Incidents.TabIndex = 2;
            this.gc_Incidents.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_Incidents});
            // 
            // gv_Incidents
            // 
            this.gv_Incidents.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID_Incident,
            this.ID_Driver,
            this.ID_Capture,
            this.Driver_Name,
            this.Incident_No,
            this.StatusDetail});
            this.gv_Incidents.GridControl = this.gc_Incidents;
            this.gv_Incidents.Name = "gv_Incidents";
            this.gv_Incidents.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gv_Incidents_RowClick);
            // 
            // ID_Incident
            // 
            this.ID_Incident.Caption = "ID_Incident";
            this.ID_Incident.FieldName = "ID_Incident";
            this.ID_Incident.Name = "ID_Incident";
            this.ID_Incident.OptionsColumn.AllowEdit = false;
            // 
            // ID_Driver
            // 
            this.ID_Driver.Caption = "Driver";
            this.ID_Driver.FieldName = "ID_Driver";
            this.ID_Driver.Name = "ID_Driver";
            // 
            // ID_Capture
            // 
            this.ID_Capture.Caption = "capture";
            this.ID_Capture.FieldName = "ID_Capture";
            this.ID_Capture.Name = "ID_Capture";
            // 
            // Driver_Name
            // 
            this.Driver_Name.Caption = "Driver Name";
            this.Driver_Name.FieldName = "Name";
            this.Driver_Name.Name = "Driver_Name";
            this.Driver_Name.OptionsColumn.AllowEdit = false;
            this.Driver_Name.Visible = true;
            this.Driver_Name.VisibleIndex = 0;
            // 
            // Incident_No
            // 
            this.Incident_No.Caption = "Incident No";
            this.Incident_No.FieldName = "Incident_No";
            this.Incident_No.Name = "Incident_No";
            this.Incident_No.OptionsColumn.AllowEdit = false;
            this.Incident_No.Visible = true;
            this.Incident_No.VisibleIndex = 1;
            // 
            // StatusDetail
            // 
            this.StatusDetail.Caption = "Status Detail";
            this.StatusDetail.ColumnEdit = this.lue_test;
            this.StatusDetail.FieldName = "ID_Status_Detail";
            this.StatusDetail.Name = "StatusDetail";
            this.StatusDetail.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.StatusDetail.Visible = true;
            this.StatusDetail.VisibleIndex = 2;
            // 
            // lue_test
            // 
            this.lue_test.AutoHeight = false;
            this.lue_test.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lue_test.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID_Status_Detail", "ID", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Name", "Name")});
            this.lue_test.DisplayMember = "Name";
            this.lue_test.Name = "lue_test";
            this.lue_test.ValueMember = "ID_Status_Detail";
            // 
            // fluentFormDefaultManager1
            // 
            this.fluentFormDefaultManager1.DockingEnabled = false;
            this.fluentFormDefaultManager1.Form = this;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 399);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(643, 43);
            this.panelControl1.TabIndex = 1;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(541, 8);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "simpleButton1";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // accordionControl1
            // 
            this.accordionControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.accordionControl1.Elements.AddRange(new DevExpress.XtraBars.Navigation.AccordionControlElement[] {
            this.accordionControlElement1});
            this.accordionControl1.Location = new System.Drawing.Point(0, 27);
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.OptionsMinimizing.State = DevExpress.XtraBars.Navigation.AccordionControlState.Minimized;
            this.accordionControl1.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Touch;
            this.accordionControl1.Size = new System.Drawing.Size(40, 442);
            this.accordionControl1.TabIndex = 1;
            this.accordionControl1.ViewType = DevExpress.XtraBars.Navigation.AccordionControlViewType.HamburgerMenu;
            // 
            // accordionControlElement1
            // 
            this.accordionControlElement1.Name = "accordionControlElement1";
            this.accordionControlElement1.Text = "Element1";
            // 
            // fluentDesignFormControl1
            // 
            this.fluentDesignFormControl1.FluentDesignForm = this;
            this.fluentDesignFormControl1.Location = new System.Drawing.Point(0, 0);
            this.fluentDesignFormControl1.Manager = this.fluentFormDefaultManager1;
            this.fluentDesignFormControl1.Name = "fluentDesignFormControl1";
            this.fluentDesignFormControl1.Size = new System.Drawing.Size(683, 27);
            this.fluentDesignFormControl1.TabIndex = 2;
            this.fluentDesignFormControl1.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 469);
            this.ControlContainer = this.fluentDesignFormContainer1;
            this.Controls.Add(this.fluentDesignFormContainer1);
            this.Controls.Add(this.accordionControl1);
            this.Controls.Add(this.fluentDesignFormControl1);
            this.FluentDesignFormControl = this.fluentDesignFormControl1;
            this.Name = "Main";
            this.NavigationControl = this.accordionControl1;
            this.Text = "Incidents";
            this.Load += new System.EventHandler(this.IncidentReport_Load);
            this.fluentDesignFormContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_Incidents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Incidents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lue_test)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentFormDefaultManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.accordionControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fluentDesignFormControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer fluentDesignFormContainer1;
        private DevExpress.XtraBars.Navigation.AccordionControl accordionControl1;
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl fluentDesignFormControl1;
        private DevExpress.XtraBars.Navigation.AccordionControlElement accordionControlElement1;
        private DevExpress.XtraBars.FluentDesignSystem.FluentFormDefaultManager fluentFormDefaultManager1;
        private DevExpress.XtraGrid.GridControl gc_Incidents;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_Incidents;
        private DevExpress.XtraGrid.Columns.GridColumn ID_Driver;
        private DevExpress.XtraGrid.Columns.GridColumn Incident_No;
        private DevExpress.XtraGrid.Columns.GridColumn StatusDetail;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lue_test;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.Columns.GridColumn Driver_Name;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn ID_Incident;
        private DevExpress.XtraGrid.Columns.GridColumn ID_Capture;
    }
}