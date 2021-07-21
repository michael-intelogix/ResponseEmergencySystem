﻿using DevExpress.XtraEditors;
using ResponseEmergencySystem.Code;
using ResponseEmergencySystem.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Diagnostics.Debug;

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
            

            if (myData != null)
            {
                string idmysoftware = "2a5aa42b-2089-4fa8-b7cc-2cea2a017a8a";

                DataRow[] accesos = myData.Select($"ID_Software = '{idmysoftware}'");

                if (accesos.Length > 0)
                {
                    constants.userName = accesos[0].ItemArray[13].ToString();
                    constants.SetUserId(accesos[0].ItemArray[10].ToString());

                    foreach (var c in myData.Columns)
                    {
                        WriteLine(c.ToString());
                    }
                    

                    DataRow[] module = myData.Select($"Module = 'Test'");
              
                    constants.SetTester(module.Count() > 0);

                    ITXFramework.ITXFramework iTX = new ITXFramework.ITXFramework();
                    iTX.Software_Log(Guid.Parse(constants.userID), Guid.Parse(accesos[0]["ID_Software"].ToString()), Guid.Parse(accesos[0]["ID_Module"].ToString()), Application.ProductVersion, false);
                        
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    DialogResult = DialogResult.Cancel;
                    Utils.ShowMessage("You don't have access to this application, please talk to the administrator", title: "Access Denied", type: "Error");
                }
            }

        }

        private void loginCtrl1_Load(object sender, EventArgs e)
        {

        }
    }
}