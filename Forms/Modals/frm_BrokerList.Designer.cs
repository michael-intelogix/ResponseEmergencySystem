
namespace ResponseEmergencySystem.Forms.Modals
{
    partial class frm_BrokerList
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject9 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject10 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject11 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject12 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gc_Drivers = new DevExpress.XtraGrid.GridControl();
            this.gv_Drivers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_Broker = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_State = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_City = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Address = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Type = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Approved = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btn_approved = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.lookUpEdit1 = new DevExpress.XtraEditors.LookUpEdit();
            this.lookUpEdit2 = new DevExpress.XtraEditors.LookUpEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.checkEdit1 = new DevExpress.XtraEditors.CheckEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gc_Drivers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Drivers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_approved)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(666, 44);
            this.panelControl1.TabIndex = 2;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Constantia", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl1.Location = new System.Drawing.Point(61, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(520, 15);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "More than one broker has appeared in the search, Please select one from the list " +
    "with button";
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.gc_Drivers);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 44);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(666, 154);
            this.panelControl2.TabIndex = 3;
            // 
            // gc_Drivers
            // 
            this.gc_Drivers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_Drivers.Location = new System.Drawing.Point(0, 0);
            this.gc_Drivers.MainView = this.gv_Drivers;
            this.gc_Drivers.Name = "gc_Drivers";
            this.gc_Drivers.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btn_approved});
            this.gc_Drivers.Size = new System.Drawing.Size(666, 154);
            this.gc_Drivers.TabIndex = 1;
            this.gc_Drivers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_Drivers});
            // 
            // gv_Drivers
            // 
            this.gv_Drivers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_Broker,
            this.col_State,
            this.col_City,
            this.col_Address,
            this.col_Type,
            this.col_Approved});
            this.gv_Drivers.GridControl = this.gc_Drivers;
            this.gv_Drivers.Name = "gv_Drivers";
            this.gv_Drivers.OptionsView.ShowGroupPanel = false;
            // 
            // col_Broker
            // 
            this.col_Broker.Caption = "Broker";
            this.col_Broker.FieldName = "Broker";
            this.col_Broker.Name = "col_Broker";
            this.col_Broker.OptionsColumn.AllowEdit = false;
            this.col_Broker.Visible = true;
            this.col_Broker.VisibleIndex = 1;
            this.col_Broker.Width = 94;
            // 
            // col_State
            // 
            this.col_State.Caption = "State";
            this.col_State.FieldName = "State";
            this.col_State.Name = "col_State";
            this.col_State.OptionsColumn.AllowEdit = false;
            this.col_State.Visible = true;
            this.col_State.VisibleIndex = 3;
            this.col_State.Width = 94;
            // 
            // col_City
            // 
            this.col_City.Caption = "City";
            this.col_City.FieldName = "City";
            this.col_City.Name = "col_City";
            this.col_City.OptionsColumn.AllowEdit = false;
            this.col_City.Visible = true;
            this.col_City.VisibleIndex = 2;
            this.col_City.Width = 105;
            // 
            // col_Address
            // 
            this.col_Address.Caption = "Address";
            this.col_Address.FieldName = "Address";
            this.col_Address.Name = "col_Address";
            this.col_Address.OptionsColumn.AllowEdit = false;
            this.col_Address.Visible = true;
            this.col_Address.VisibleIndex = 4;
            this.col_Address.Width = 90;
            // 
            // col_Type
            // 
            this.col_Type.FieldName = "Type";
            this.col_Type.Name = "col_Type";
            this.col_Type.OptionsColumn.AllowEdit = false;
            this.col_Type.Visible = true;
            this.col_Type.VisibleIndex = 4;
            this.col_Type.Width = 95;
            // 
            // col_Approved
            // 
            this.col_Approved.ColumnEdit = this.btn_approved;
            this.col_Approved.Name = "col_Approved";
            this.col_Approved.Visible = true;
            this.col_Approved.VisibleIndex = 5;
            this.col_Approved.Width = 39;
            // 
            // btn_approved
            // 
            this.btn_approved.AutoHeight = false;
            editorButtonImageOptions3.Image = global::ResponseEmergencySystem.Properties.Resources.apply_16x16;
            this.btn_approved.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject9, serializableAppearanceObject10, serializableAppearanceObject11, serializableAppearanceObject12, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btn_approved.Name = "btn_approved";
            this.btn_approved.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.simpleButton1);
            this.panelControl3.Controls.Add(this.checkEdit1);
            this.panelControl3.Controls.Add(this.labelControl6);
            this.panelControl3.Controls.Add(this.labelControl5);
            this.panelControl3.Controls.Add(this.labelControl4);
            this.panelControl3.Controls.Add(this.lookUpEdit2);
            this.panelControl3.Controls.Add(this.lookUpEdit1);
            this.panelControl3.Controls.Add(this.textEdit2);
            this.panelControl3.Controls.Add(this.labelControl3);
            this.panelControl3.Controls.Add(this.textEdit1);
            this.panelControl3.Controls.Add(this.labelControl2);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 198);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(666, 80);
            this.panelControl3.TabIndex = 4;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 9);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(31, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Broker";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(49, 6);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(288, 20);
            this.textEdit1.TabIndex = 3;
            // 
            // textEdit2
            // 
            this.textEdit2.Location = new System.Drawing.Point(57, 44);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Size = new System.Drawing.Size(420, 20);
            this.textEdit2.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 47);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(39, 13);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "Address";
            // 
            // lookUpEdit1
            // 
            this.lookUpEdit1.Location = new System.Drawing.Point(403, 6);
            this.lookUpEdit1.Name = "lookUpEdit1";
            this.lookUpEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit1.Size = new System.Drawing.Size(100, 20);
            this.lookUpEdit1.TabIndex = 6;
            // 
            // lookUpEdit2
            // 
            this.lookUpEdit2.Location = new System.Drawing.Point(554, 6);
            this.lookUpEdit2.Name = "lookUpEdit2";
            this.lookUpEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lookUpEdit2.Size = new System.Drawing.Size(100, 20);
            this.lookUpEdit2.TabIndex = 7;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(366, 9);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(26, 13);
            this.labelControl4.TabIndex = 8;
            this.labelControl4.Text = "State";
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(529, 9);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(19, 13);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "City";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(449, 9);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(0, 13);
            this.labelControl6.TabIndex = 11;
            // 
            // checkEdit1
            // 
            this.checkEdit1.Location = new System.Drawing.Point(491, 45);
            this.checkEdit1.Name = "checkEdit1";
            this.checkEdit1.Properties.Caption = "Private";
            this.checkEdit1.Properties.ContentAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.checkEdit1.Size = new System.Drawing.Size(57, 19);
            this.checkEdit1.TabIndex = 12;
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = global::ResponseEmergencySystem.Properties.Resources.add_16x16;
            this.simpleButton1.Location = new System.Drawing.Point(561, 41);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(93, 23);
            this.simpleButton1.TabIndex = 13;
            this.simpleButton1.Text = "Add Broker";
            // 
            // frm_BrokerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 276);
            this.Controls.Add(this.panelControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.panelControl1);
            this.Name = "frm_BrokerList";
            this.Text = "frm_BrokerList";
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gc_Drivers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_Drivers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_approved)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEdit1.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gc_Drivers;
        private DevExpress.XtraGrid.Views.Grid.GridView gv_Drivers;
        private DevExpress.XtraGrid.Columns.GridColumn col_Broker;
        private DevExpress.XtraGrid.Columns.GridColumn col_State;
        private DevExpress.XtraGrid.Columns.GridColumn col_City;
        private DevExpress.XtraGrid.Columns.GridColumn col_Address;
        private DevExpress.XtraGrid.Columns.GridColumn col_Type;
        private DevExpress.XtraGrid.Columns.GridColumn col_Approved;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btn_approved;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.CheckEdit checkEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit2;
        private DevExpress.XtraEditors.LookUpEdit lookUpEdit1;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
    }
}