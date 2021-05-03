﻿using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponseEmergencySystem.Forms
{
    public partial class frm_Login : DevExpress.XtraEditors.XtraForm
    {

        public DataTable myData = new DataTable();

        public frm_Login()
        {
            InitializeComponent();
        }

        private void frm_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.myData = loginCtrl1.Access;
            DialogResult = DialogResult.OK;
        }
    }
}