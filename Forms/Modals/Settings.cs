using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResponseEmergencySystem.Properties;
using ResponseEmergencySystem.Code;

namespace ResponseEmergencySystem.Forms.Modals
{
    public partial class Settings : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void accordionControlElement1_Click(object sender, EventArgs e)
        {

        }

        private void fluentDesignFormContainer1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            xtraFolderBrowserDialog1.ShowDialog();
            textEdit1.EditValue = xtraFolderBrowserDialog1.SelectedPath;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AppFolder = Utils.GetEdtValue(textEdit1);
            Properties.Settings.Default.Save();
        }
    }
}
