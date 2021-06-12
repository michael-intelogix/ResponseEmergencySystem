using DevExpress.XtraReports.UI;
using ResponseEmergencySystem.Services;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace ResponseEmergencySystem.Reports
{
    public partial class XtraReport2 : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReport2()
        {
            InitializeComponent();
            xrTableCell1.DataBindings.Add("Text", this.DataSource, "Name");
        }

    }
}
