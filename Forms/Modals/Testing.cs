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

namespace ResponseEmergencySystem.Forms.Modals
{
    public partial class Testing : DevExpress.XtraEditors.XtraForm
    {
        public Testing(string name, DateTime time, float latitude, float longitude, int heading, int speed, string formattedLocation)
        {
            InitializeComponent();
            lbl_Name.Text = name;
            dte_Date.EditValue = time;
            tme_Date.EditValue = time;
            lbl_Latitude.Text = latitude.ToString();
            lbl_Longitude.Text = longitude.ToString();
            lbl_Heading.Text = heading.ToString();
            lbl_Speed.Text = speed.ToString();
            lbl_FormattedLocation.Text = formattedLocation;
        }
    }
}