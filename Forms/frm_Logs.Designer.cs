
namespace ResponseEmergencySystem.Forms
{
    partial class frm_Logs
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
            this.gc_Logs = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.col_Date = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_Description = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_OldValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col_NewValue = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gc_Logs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gc_Logs
            // 
            this.gc_Logs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc_Logs.Location = new System.Drawing.Point(0, 0);
            this.gc_Logs.MainView = this.gridView1;
            this.gc_Logs.Name = "gc_Logs";
            this.gc_Logs.Size = new System.Drawing.Size(932, 455);
            this.gc_Logs.TabIndex = 0;
            this.gc_Logs.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col_Date,
            this.col_Description,
            this.col_OldValue,
            this.col_NewValue});
            this.gridView1.GridControl = this.gc_Logs;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // col_Date
            // 
            this.col_Date.Caption = "Date";
            this.col_Date.FieldName = "CreatedAt";
            this.col_Date.Name = "col_Date";
            this.col_Date.Visible = true;
            this.col_Date.VisibleIndex = 0;
            // 
            // col_Description
            // 
            this.col_Description.Caption = "Description";
            this.col_Description.FieldName = "Description";
            this.col_Description.Name = "col_Description";
            this.col_Description.Visible = true;
            this.col_Description.VisibleIndex = 1;
            // 
            // col_OldValue
            // 
            this.col_OldValue.Caption = "Old Value";
            this.col_OldValue.FieldName = "OldValue";
            this.col_OldValue.Name = "col_OldValue";
            this.col_OldValue.Visible = true;
            this.col_OldValue.VisibleIndex = 2;
            // 
            // col_NewValue
            // 
            this.col_NewValue.Caption = "New Value";
            this.col_NewValue.FieldName = "NewValue";
            this.col_NewValue.Name = "col_NewValue";
            this.col_NewValue.Visible = true;
            this.col_NewValue.VisibleIndex = 3;
            // 
            // frm_Logs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 455);
            this.Controls.Add(this.gc_Logs);
            this.Name = "frm_Logs";
            this.Text = "frm_Logs";
            this.Load += new System.EventHandler(this.frm_Logs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gc_Logs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gc_Logs;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn col_Date;
        private DevExpress.XtraGrid.Columns.GridColumn col_Description;
        private DevExpress.XtraGrid.Columns.GridColumn col_OldValue;
        private DevExpress.XtraGrid.Columns.GridColumn col_NewValue;
    }
}