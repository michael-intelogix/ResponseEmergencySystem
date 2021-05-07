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
    public partial class AppConfiguration : DevExpress.XtraEditors.XtraForm
    {
        private string AppPath = "";

        public AppConfiguration()
        {
            InitializeComponent();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            xtraFolderBrowserDialog1.ShowDialog();
            AppPath = xtraFolderBrowserDialog1.SelectedPath;
            textEdit1.Text = AppPath.Replace("\\", "/");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AppFolder = AppPath;
            Properties.Settings.Default.Save();
        }

        private void AppConfiguration_Load(object sender, EventArgs e)
        {
            AppPath = Properties.Settings.Default.AppFolder;
            textEdit1.Text = AppPath.Replace("\\", "/");
        }
    }
}