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

namespace ResponseEmergencySystem.Forms.Modals
{
    public partial class Modals : DevExpress.XtraEditors.XtraForm
    {
        public Modals(String Message, string Title)
        {
            InitializeComponent();
            labelControl1.Text = Title;
            label1.Text = Message;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void stackPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}