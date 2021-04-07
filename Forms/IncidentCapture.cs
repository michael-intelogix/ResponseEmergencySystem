﻿using DevExpress.XtraEditors;
using ResponseEmergencySystem.Entity_Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ResponseEmergencySystem.Code;

namespace ResponseEmergencySystem.Forms
{
    
    public partial class IncidentCapture : DevExpress.XtraEditors.XtraForm
    {
        public IncidentCapture()
        {
            InitializeComponent();
        }

        private void IncidentCapture_Load(object sender, EventArgs e)
        {
            using (var context = new SIREMLocalEntities())
            {
                lue_states.Properties.DataSource = context.List_States(Guid.Parse("69D30589-3090-45F9-8776-4DFEBCF39371"));
                lue_DriverLicenceState.Properties.DataSource = context.List_States(Guid.Parse("69D30589-3090-45F9-8776-4DFEBCF39371"));
            }
        }

        private void btn_AddIncident_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            float lat = rand.Next(-101, 101) + Truncate((float)rand.NextDouble(), 7);
            float lon = rand.Next(-101, 101) + Truncate((float)rand.NextDouble(), 7);
            Debug.WriteLine(lat.ToString());
            using (var context = new SIREMLocalEntities())
            {

                var results_1 = context.Database.SqlQuery<Update_Location_Result>("EXEC Update_Location {0}, {1}, {2}, {3}, {4}, {5}, {6}",
                    null,
                    Guid.Parse(lue_Cities.EditValue.ToString()),
                    Guid.Parse(lue_states.EditValue.ToString()),
                    edt_Highway.Text.ToString(),
                    lat.ToString(),
                    lon.ToString(),
                    ""
                ).ToListAsync();

                results_1.Wait();
                Debug.WriteLine(results_1.Result[0].msg);

                var id_driver = lbl_IdDriver.Text;
                Task<List<Update_Driver_Result>> results_2;
                if (id_driver == "empty") {
                    results_2 = context.Database.SqlQuery<Update_Driver_Result>("EXEC Update_Driver {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}",
                        null,
                        edt_FullName.Text.ToString(),
                        "address",
                        edt_License.Text.ToString(),
                        lue_DriverLicenceState.EditValue.ToString(),
                        dte_ExpirationDate.DateTime,
                        edt_PhoneNumber.Text.ToString(),
                        true
                    ).ToListAsync();

                    results_2.Wait();
                    id_driver = results_2.Result[0].ID.ToString();
                    Debug.WriteLine(results_2.Result[0].msg);
                }
                var results_3 = context.Database.SqlQuery<Update_Cargo_Result>("EXEC Update_Cargo {0}, {1}, {2}",
                    null,
                    edt_Cargo.EditValue.ToString(),
                    edt_manifest.ToString()
                ).ToListAsync();
                results_3.Wait();
                Debug.WriteLine(results_3.Result[0].msg);
                //var driver_response = context.Update_Driver(
                //    null,
                //    edt_FullName.Text.ToString(),
                //    "address",
                //    edt_License.Text.ToString(),
                //    lue_DriverLicenceState.EditValue.ToString(),
                //    dte_ExpirationDate.DateTime,
                //    edt_PhoneNumber.Text.ToString(),
                //    true
                //    );

                //Debug.WriteLine(driver_response.ToList<Update_Driver_Result>()[0].msg);

                //var results_3 = context.Database.SqlQuery<Update_Location_Result>("EXEC Update_Driver {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}",
                //    null,
                //    Guid.Parse(results_2.Result[0].ID.ToString()),
                //    Guid.Parse(results_1.Result[0].ID.ToString()),
                //    Guid.Parse("24C94B83-1E93-EB11-BA73-8CDCD457CB4F"),
                //    Guid.Parse("EB06B210-E102-4AEC-9820-8AD3A49060D9"),
                //    "178784111",
                //    DateTime.Now,
                //    DateTime.Now,
                //    "343234324",
                //    true,
                //    "2132354",
                //    true,
                //    "a lot of them",
                //    "4324324",
                //    "23432432",
                //    true,
                //    true,
                //    true,
                //    "43324324",
                //    Guid.Parse("7DC94B83-1E93-EB11-BA73-8CDCD457CB4F"),
                //    true
                //).ToListAsync();

                //results_3.Wait();
                //Debug.WriteLine(results_3.Result[0].msg);

                var no = rand.Next(0, 1000001).ToString();

                var Incident_Response = context.Update_Incident(
                    null,
                    //Guid.Parse("12D0FB1E-2797-EB11-BA73-8CDCD457CB4F"), 
                    Guid.Parse(id_driver),
                    Guid.Parse(results_1.Result[0].ID.ToString()),
                    Guid.Parse(results_3.Result[0].ID.ToString()),
                    Guid.Parse("EB06B210-E102-4AEC-9820-8AD3A49060D9"),
                    no,
                    DateTime.Now,
                    DateTime.Now,
                    "343234324",
                    true,
                    "2132354",
                    true,
                    "a lot of them",
                    "4324324",
                    "23432432",
                    true,
                    true,
                    true,
                    "43324324",
                    Guid.Parse("7DC94B83-1E93-EB11-BA73-8CDCD457CB4F"),
                    true
                );

                Debug.WriteLine(Incident_Response.ToList()[0].msg);

            }
        }

        private void lue_States_Properties_EditValueChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(lue_states.EditValue);

            using (var context = new SIREMLocalEntities())
            {
                lue_Cities.Properties.DataSource = context.List_Cities(Guid.Parse(lue_states.EditValue.ToString()), "");
            }
        }

        public float Truncate(float value, int digits)
        {
            double mult = Math.Pow(10.0, digits);
            double result = Math.Truncate(mult * value) / mult;
            return (float)result;
        }


        private static async void AddLocation(Location loc)
        {
            //Random rand = new Random();
            //float lat = rand.Next(-101, 101) + Truncate((float)rand.NextDouble(), 7);
            //float lon = rand.Next(-101, 101) + Truncate((float)rand.NextDouble(), 7);
            //Debug.WriteLine(lat.ToString());
            using (var context = new SIREMLocalEntities())
            {
                

                //var location_response = context.Update_Location(
                //    loc.ID_Location, 
                //    loc.ID_City,
                //    loc.ID_State,
                //    loc.Highway,
                //    loc.Latitude,
                //    loc.Longitude,
                //    loc.References
                //    );
                Debug.WriteLine("here");
                //return results;

            }
        }

        private void edt_License_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void btn_LookUpLicence_Click(object sender, EventArgs e)
        {
            using (var context = new SIREMLocalEntities())
            {
                var Driver_Response = context.Get_Driver(edt_License.EditValue.ToString()).ToList()[0];
                edt_FullName.EditValue = Driver_Response.Name.ToString();            
                edt_PhoneNumber.EditValue = Driver_Response.phone_number.ToString();            
                lue_DriverLicenceState.EditValue = Guid.Parse("583FB45E-E1B6-4E71-9471-7B2C5859FCD9");            
                dte_ExpirationDate.EditValue = Driver_Response.Expiration_Date.ToString();
                lbl_IdDriver.Text = Driver_Response.ID_Driver.ToString();
            }
        }
    }

}