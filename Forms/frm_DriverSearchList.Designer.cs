
namespace ResponseEmergencySystem.Forms
{
    partial class frm_DriverSearchList
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gc_Drivers = new DevExpress.XtraGrid.GridControl();
            this.gv_Drivers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_DriverName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_LastName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_PhoneNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_License = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_ExpeditionState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_ExpirationDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Approved = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btn_approved = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_Drivers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Drivers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_approved)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(734, 58);
            this.panelControl1.TabIndex = 1;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Constantia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl1.Location = new System.Drawing.Point(71, 16);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(516, 15);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "More than one driver has appeared in the search, Please select one from the list " +
    "with button";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.gc_Drivers);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 58);
            this.panelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(734, 320);
            this.panelControl2.TabIndex = 2;
            // 
            // gc_Drivers
            // 
            this.gc_Drivers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_Drivers.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gc_Drivers.Location = new System.Drawing.Point(0, 0);
            this.gc_Drivers.MainView = this.gv_Drivers;
            this.gc_Drivers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gc_Drivers.Name = "gc_Drivers";
            this.gc_Drivers.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btn_approved});
            this.gc_Drivers.Size = new System.Drawing.Size(734, 320);
            this.gc_Drivers.TabIndex = 1;
            this.gc_Drivers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_Drivers});
            // 
            // gv_Drivers
            // 
            this.gv_Drivers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_DriverName,
            this.col_LastName,
            this.col_PhoneNumber,
            this.col_License,
            this.col_ExpeditionState,
            this.col_ExpirationDate,
            this.col_Approved});
            this.gv_Drivers.DetailHeight = 458;
            this.gv_Drivers.GridControl = this.gc_Drivers;
            this.gv_Drivers.Name = "gv_Drivers";
            this.gv_Drivers.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gv_Drivers.OptionsView.ShowAutoFilterRow = true;
            this.gv_Drivers.OptionsView.ShowGroupPanel = false;
            this.gv_Drivers.OptionsView.ShowIndicator = false;
            // 
            // col_DriverName
            // 
            this.col_DriverName.Caption = "Name";
            this.col_DriverName.FieldName = "Name";
            this.col_DriverName.MinWidth = 23;
            this.col_DriverName.Name = "col_DriverName";
            this.col_DriverName.OptionsColumn.AllowEdit = false;
            this.col_DriverName.Visible = true;
            this.col_DriverName.VisibleIndex = 0;
            this.col_DriverName.Width = 109;
            // 
            // col_LastName
            // 
            this.col_LastName.Caption = "Last Name";
            this.col_LastName.FieldName = "LastName1";
            this.col_LastName.MinWidth = 23;
            this.col_LastName.Name = "col_LastName";
            this.col_LastName.OptionsColumn.AllowEdit = false;
            this.col_LastName.Visible = true;
            this.col_LastName.VisibleIndex = 1;
            this.col_LastName.Width = 109;
            // 
            // col_PhoneNumber
            // 
            this.col_PhoneNumber.Caption = "phone number";
            this.col_PhoneNumber.FieldName = "PhoneNumber";
            this.col_PhoneNumber.MinWidth = 23;
            this.col_PhoneNumber.Name = "col_PhoneNumber";
            this.col_PhoneNumber.OptionsColumn.AllowEdit = false;
            this.col_PhoneNumber.Visible = true;
            this.col_PhoneNumber.VisibleIndex = 3;
            this.col_PhoneNumber.Width = 109;
            // 
            // col_License
            // 
            this.col_License.Caption = "License";
            this.col_License.FieldName = "License";
            this.col_License.MinWidth = 23;
            this.col_License.Name = "col_License";
            this.col_License.OptionsColumn.AllowEdit = false;
            this.col_License.Visible = true;
            this.col_License.VisibleIndex = 2;
            this.col_License.Width = 121;
            // 
            // col_ExpeditionState
            // 
            this.col_ExpeditionState.Caption = "Expedition State";
            this.col_ExpeditionState.FieldName = "State";
            this.col_ExpeditionState.MinWidth = 23;
            this.col_ExpeditionState.Name = "col_ExpeditionState";
            this.col_ExpeditionState.OptionsColumn.AllowEdit = false;
            this.col_ExpeditionState.Visible = true;
            this.col_ExpeditionState.VisibleIndex = 4;
            this.col_ExpeditionState.Width = 104;
            // 
            // col_ExpirationDate
            // 
            this.col_ExpirationDate.Caption = "Expiration Date";
            this.col_ExpirationDate.FieldName = "ExpirationDate";
            this.col_ExpirationDate.MinWidth = 23;
            this.col_ExpirationDate.Name = "col_ExpirationDate";
            this.col_ExpirationDate.OptionsColumn.AllowEdit = false;
            this.col_ExpirationDate.Visible = true;
            this.col_ExpirationDate.VisibleIndex = 5;
            this.col_ExpirationDate.Width = 117;
            // 
            // col_Approved
            // 
            this.col_Approved.ColumnEdit = this.btn_approved;
            this.col_Approved.MinWidth = 23;
            this.col_Approved.Name = "col_Approved";
            this.col_Approved.Visible = true;
            this.col_Approved.VisibleIndex = 6;
            this.col_Approved.Width = 38;
            // 
            // btn_approved
            // 
            this.btn_approved.AutoHeight = false;
            editorButtonImageOptions1.SvgImage = global::ResponseEmergencySystem.Properties.Resources.checkGreen;
            editorButtonImageOptions1.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btn_approved.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btn_approved.Name = "btn_approved";
            this.btn_approved.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.btn_approved.Click += new System.EventHandler(this.btn_ApprovedDriver);
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 378);
            this.panelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(734, 92);
            this.panelControl3.TabIndex = 3;
            // 
            // frm_DriverSearchList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 379);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frm_DriverSearchList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Driver Catalog";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_Drivers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Drivers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_approved)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gc_Drivers;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_Drivers;
        private DevExpress.XtraGrid.Columns.GridColumn col_DriverName;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraGrid.Columns.GridColumn col_LastName;
        private DevExpress.XtraGrid.Columns.GridColumn col_PhoneNumber;
        private DevExpress.XtraGrid.Columns.GridColumn col_ExpeditionState;
        private DevExpress.XtraGrid.Columns.GridColumn col_ExpirationDate;
        private DevExpress.XtraGrid.Columns.GridColumn col_Approved;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btn_approved;
        private DevExpress.XtraGrid.Columns.GridColumn col_License;
    }
}