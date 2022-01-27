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
    public partial class DirectoryError : DevExpress.XtraEditors.XtraForm
    {
        private string AppPath = "";

        public DirectoryError()
        {
            InitializeComponent();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            
            if (xtraFolderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                AppPath = xtraFolderBrowserDialog1.SelectedPath;
                textEdit1.Text = AppPath.Replace("\\", "/");

                Properties.Settings.Default.AppFolder = AppPath;
                Properties.Settings.Default.Save();
            }
                
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}