
namespace ResponseEmergencySystem.Forms
{
    partial class mientras
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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            this.gv_InjuredPersons = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.col_FullName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_LastName1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_LastName2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_PhoneNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Delete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btn_DeleteInjuredRow = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_InjuredPersons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_DeleteInjuredRow)).BeginInit();
            this.SuspendLayout();
            // 
            // gv_InjuredPersons
            // 
            this.gv_InjuredPersons.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_FullName,
            this.col_LastName1,
            this.col_LastName2,
            this.col_PhoneNumber,
            this.col_Delete});
            this.gv_InjuredPersons.DetailHeight = 458;
            // 
            // gridControl1
            // 
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Location = new System.Drawing.Point(12, 644);
            this.gridControl1.MainView = this.gv_InjuredPersons;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.btn_DeleteInjuredRow});
            this.gridControl1.Size = new System.Drawing.Size(832, 185);
            this.gridControl1.TabIndex = 109;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gv_InjuredPersons});
            this.gv_InjuredPersons.GridControl = this.gridControl1;
            this.gv_InjuredPersons.Name = "gv_InjuredPersons";
            this.gv_InjuredPersons.OptionsView.ShowGroupPanel = false;
            // 
            // col_FullName
            // 
            this.col_FullName.Caption = "Full Name";
            this.col_FullName.FieldName = "FullName";
            this.col_FullName.MinWidth = 23;
            this.col_FullName.Name = "col_FullName";
            this.col_FullName.Visible = true;
            this.col_FullName.VisibleIndex = 0;
            this.col_FullName.Width = 162;
            // 
            // col_LastName1
            // 
            this.col_LastName1.Caption = "Last Name";
            this.col_LastName1.FieldName = "LastName1";
            this.col_LastName1.MinWidth = 23;
            this.col_LastName1.Name = "col_LastName1";
            this.col_LastName1.Visible = true;
            this.col_LastName1.VisibleIndex = 1;
            this.col_LastName1.Width = 162;
            // 
            // col_LastName2
            // 
            this.col_LastName2.Caption = "Second Last Name";
            this.col_LastName2.FieldName = "LastName2";
            this.col_LastName2.MinWidth = 23;
            this.col_LastName2.Name = "col_LastName2";
            this.col_LastName2.Visible = true;
            this.col_LastName2.VisibleIndex = 2;
            this.col_LastName2.Width = 162;
            // 
            // col_PhoneNumber
            // 
            this.col_PhoneNumber.Caption = "Phone Number";
            this.col_PhoneNumber.FieldName = "Phone";
            this.col_PhoneNumber.MinWidth = 23;
            this.col_PhoneNumber.Name = "col_PhoneNumber";
            this.col_PhoneNumber.Visible = true;
            this.col_PhoneNumber.VisibleIndex = 3;
            this.col_PhoneNumber.Width = 162;
            // 
            // col_Delete
            // 
            this.col_Delete.ColumnEdit = this.btn_DeleteInjuredRow;
            this.col_Delete.MinWidth = 23;
            this.col_Delete.Name = "col_Delete";
            this.col_Delete.Visible = true;
            this.col_Delete.VisibleIndex = 4;
            this.col_Delete.Width = 23;
            // 
            // btn_DeleteInjuredRow
            // 
            this.btn_DeleteInjuredRow.AutoHeight = false;
            editorButtonImageOptions2.SvgImage = global::ResponseEmergencySystem.Properties.Resources.cancelRed;
            editorButtonImageOptions2.SvgImageSize = new System.Drawing.Size(20, 25);
            this.btn_DeleteInjuredRow.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.btn_DeleteInjuredRow.Name = "btn_DeleteInjuredRow";
            this.btn_DeleteInjuredRow.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // mientras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 600);
            this.Name = "mientras";
            this.Text = "mientras";
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gv_InjuredPersons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_DeleteInjuredRow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.Views.Grid.GridView gv_InjuredPersons;
        private DevExpress.XtraGrid.Columns.GridColumn col_FullName;
        private DevExpress.XtraGrid.Columns.GridColumn col_LastName1;
        private DevExpress.XtraGrid.Columns.GridColumn col_LastName2;
        private DevExpress.XtraGrid.Columns.GridColumn col_PhoneNumber;
        private DevExpress.XtraGrid.Columns.GridColumn col_Delete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit btn_DeleteInjuredRow;
        private DevExpress.XtraGrid.GridControl gridControl1;
    }
}