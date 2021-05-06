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
        public AppConfiguration()
        {
            InitializeComponent();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            xtraFolderBrowserDialog1.ShowDialog();
            textEdit1.Text = xtraFolderBrowserDialog1.SelectedPath;
            MessageBox.Show(textEdit1.Text);
        }
    }
}