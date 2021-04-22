using DevExpress.XtraEditors;
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
using ResponseEmergencySystem.Properties;
using ResponseEmergencySystem.Controllers;

namespace ResponseEmergencySystem.Forms
{
    
    public partial class AddIncidentDetails : XtraForm, IIncidentsView
    {
        // driver E2E7FBBB-6BF8-414A-B160-1A4EE294DC97
        // driver2 C7B06EF3-869B-4212-A1EC-7820B2D17CA4
        private string ID_Driver = "";
        private string ID_Truck = "";
        private string ID_Trailer = "";
        private bool truckDamages = false;
        private int cont = 0;
        private Point pnl_loc = new Point(6, 3);
        private string [] Injured = { "First", "Second", "Third", "Four", "Five" };
        private int injuredPersonsCount = 0;
        private PanelControl pnl_Injures = new PanelControl();
        private LabelControl lbl_InjuredName = new LabelControl();
        private TextEdit edt_InjuredName = new TextEdit();
        private LabelControl lbl_InjuredContactNumber = new LabelControl();
        private TextEdit edt_InjuredContactNumber = new TextEdit();
        private SimpleButton btn_AddInjuredPerson = new SimpleButton();
        private SimpleButton btn_RemoveInjuredPerson = new SimpleButton();

        private DataTable dt_InjuredPersons;

        public AddIncidentDetails()
        {
            InitializeComponent();
            initializeDatatable();

            //dtInjured.Columns.Add("Name");
            //dtInjured.Columns.Add("Number");
            //DataRow _data1 = dtInjured.NewRow();
            //_data1["Name"] = "holi";
            //_data1["Number"] = "1";
            //dtInjured.Rows.Add(_data1);
            //DataRow _data2 = dtInjured.NewRow();
            //_data2["Name"] = "hey";
            //_data2["Number"] = "2";
            //dtInjured.Rows.Add(_data2);
            //lookUpEdit1.Properties.DataSource = dtInjured;

        }

        IncidentController _controller;

        private void initializeDatatable()
        {
            dt_InjuredPersons = new DataTable();
            dt_InjuredPersons.Columns.Add("FullName");
            dt_InjuredPersons.Columns.Add("LastName1");
            dt_InjuredPersons.Columns.Add("LastName2");
            dt_InjuredPersons.Columns.Add("Phone");
            gc_InjuredPersons.DataSource = dt_InjuredPersons;
        }

        private void initializeLookUpsData()
        {
            DataTable dt_States = Functions.getStates();
            lue_StateExp.Properties.DataSource = dt_States;
            lue_states.Properties.DataSource = dt_States;
        }

        private void CreateInjuredpersons(Int16 numOfRows)
        {
            Int16 currentRow = 0;
            while (currentRow < numOfRows)
            {
                addEmptyRow();
                currentRow++;
            }


            //Task.Delay(1000);
            //return Task.FromResult("ok");
        }

        private void refreshInjuredPersonsTable()
        {
            gc_InjuredPersons.DataSource = dt_InjuredPersons;
        }

        private void addEmptyRow()
        {
            DataRow _data = dt_InjuredPersons.NewRow();
            _data["FullName"] = "";
            _data["LastName1"] = "";
            _data["LastName2"] = "";
            _data["Phone"] = "";
            dt_InjuredPersons.Rows.Add(_data);
            refreshInjuredPersonsTable();
        }

        private void numberExists(LabelControl lbl_Exists, DataRow response, string type = "")
        {

            if (response.ItemArray[0].ToString() == "0")
            {
                lbl_Exists.ImageOptions.Image = Resources.cancel_16x161;
                lbl_Exists.Visible = true;
                if (type == "Trailer") { lbl_CargoType.Visible = false; }
            }
            else
            {
                lbl_Exists.ImageOptions.Image = Resources.apply_16x161;
                lbl_Exists.Visible = true;

                if (type == "Trailer")
                {
                    lbl_CargoType.Visible = true;
                    lbl_CargoType.Text = response.ItemArray[3].ToString();
                }
                
            }
        }

        private void btn_AddRowsClick(object sender, EventArgs e)
        {
            CreateInjuredpersons(Convert.ToInt16(edt_NumberOfInjured.EditValue));
            //addEmptyRow();

        }

        private void btn_DeleteRowClick(object sender, EventArgs e)
        {
            Int32 index = gv_InjuredPersons.FocusedRowHandle;

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this row?", 
                "Delete injured person row", 
                MessageBoxButtons.OKCancel, 
                MessageBoxIcon.Information);
            if (result.Equals(DialogResult.OK))
            {
                gv_InjuredPersons.DeleteRow(index);
            }
        }

        //private Task<string> CreatePersonInjuredCapture (Point loc, XtraScrollableControl spnl_Injured, string pnlName)
        //{
        //    //if (injuredNumber < 1)
        //    //{
        //    //    TextEdit edt_PersonInjured = new TextEdit();
        //    //    edt_PersonInjured.Width = 138;
        //    //    edt_PersonInjured.Height = 20;

        //    //    var edtLocX = edt_PersonInjured.Location.X;
        //    //    var edtLocY = edt_PersonInjured.Location.Y;

        //    //    if (lastEdt.Location.X + lastEdt.Size.Width <= 564)
        //    //    {
        //    //        edtLocX = lastEdt.Location.X + lastEdt.Size.Width + 20;
        //    //        edtLocY = lastEdt.Location.Y;

        //    //        edt_PersonInjured.Size = new System.Drawing.Size(138, 20);
        //    //        edt_PersonInjured.Location = new System.Drawing.Point(lastEdt.Location.X + lastEdt.Size.Width + 20, lastEdt.Location.Y);

        //    //        Debug.WriteLine(lastEdt.Location.Y);
        //    //        groupControl3.Controls.Add(edt_PersonInjured);
        //    //        Controls.Add(edt_PersonInjured);
        //    //        panelControl1.Refresh();
        //    //    }

        //    //}

        //    PanelControl pnl_Injures = new PanelControl();
        //    LabelControl lbl_InjuredName = new LabelControl();
        //    TextEdit edt_InjuredName = new TextEdit();
        //    LabelControl lbl_InjuredContactNumber = new LabelControl();
        //    TextEdit edt_InjuredContactNumber = new TextEdit();
        //    SimpleButton btn_AddInjuredPerson = new SimpleButton();
        //    SimpleButton btn_RemoveInjuredPerson = new SimpleButton();

        //    pnl_Injures.Location = loc;

        //    pnl_Injures.Name = pnlName;
        //    pnl_Injures.Size = new Size(601, 33);
        //    pnl_Injures.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;

        //    lbl_InjuredName.Text = "Name of the person injured";
        //    lbl_InjuredName.Location = new Point(5, 10);
        //    lbl_InjuredName.Size = new Size(137, 13);

        //    edt_InjuredName.Location = new Point(151, 7);
        //    edt_InjuredName.Size = new Size(138, 20);

        //    lbl_InjuredContactNumber.Text = "Contact Number";
        //    lbl_InjuredContactNumber.Location = new Point(300, 10);
        //    lbl_InjuredContactNumber.Size = new Size(78, 13);

        //    edt_InjuredContactNumber.Location = new Point(385, 7);
        //    edt_InjuredContactNumber.Size = new Size(138, 20);

        //    btn_RemoveInjuredPerson.Location = new Point(536, 5);
        //    btn_RemoveInjuredPerson.Size = new Size(25, 23);
        //    btn_RemoveInjuredPerson.Click += RemoveIncidentCapture;
        //    btn_RemoveInjuredPerson.ImageOptions.Image = Resources.cancel_16x16;

        //    //btn_AddInjuredPerson.Location = new Point(571, 5);
        //    //btn_AddInjuredPerson.Size = new Size(25, 23);
        //    //btn_AddInjuredPerson.ImageOptions.Image = Resources.add_16x16;

        //    pnl_Injures.Controls.Add(lbl_InjuredName);
        //    pnl_Injures.Controls.Add(edt_InjuredName);
        //    pnl_Injures.Controls.Add(lbl_InjuredContactNumber);
        //    pnl_Injures.Controls.Add(edt_InjuredContactNumber);
        //    //pnl_Injures.Controls.Add(btn_AddInjuredPerson);
        //    pnl_Injures.Controls.Add(btn_RemoveInjuredPerson);

        //    Task.Delay(1000);

        //    spnl_Injured.Controls.Add(pnl_Injures);
        //    Debug.WriteLine(pnl_Injures.Name);
        //    return Task.FromResult("ok");
        //}

        //private void RemoveIncidentCapture(object sender, EventArgs e)
        //{
        //    // pnl_InjuredCapture-1
        //    SimpleButton sb = (SimpleButton)sender;
        //    PanelControl pnl_Injured = (PanelControl) sb.Parent;
        //    XtraScrollableControl spnl_Injuredpersons = (XtraScrollableControl) sb.Parent.Parent;
        //    string pnlName = (string)pnl_Injured.Name.ToString();

        //    Int16 indx = Convert.ToInt16(pnlName.Split('-')[1]);
        //    string controlName = pnlName.Split('-')[0];
        //    Int16 loc_Y = 0;

        //    Debug.WriteLine("removed: " + (string)pnl_Injured.Name);
        //    injuredPersonsCount -= 1;
        //    loc_Y = Convert.ToInt16(pnl_Injured.Location.Y);
        //    if (injuredPersonsCount == 1) 
        //    {
        //        pnl_loc.Y = (injuredPersonsCount * 30) + 3;
        //        loc_Y = 3;
        //    } 

        //    if (injuredPersonsCount > 1)
        //    {
        //        pnl_loc.Y = (injuredPersonsCount * 30) + 3;
        //        loc_Y = (Int16) (pnl_loc.Y - 30);
        //    }

        //    pnl_Injured.Dispose();

        //    if (injuredPersonsCount > 0)
        //    {   

        //        for (Int16 i = indx; i < injuredPersonsCount; i++)
        //        {
        //            Debug.WriteLine("here");
        //            Debug.WriteLine(spnl_Injuredpersons.Controls[i].Name);
        //            spnl_Injuredpersons.Controls[i].Name = controlName + (i - 1);
        //            spnl_Injuredpersons.Controls[i].Location = new Point(spnl_Injuredpersons.Controls[i].Location.X, loc_Y);
        //            spnl_Injuredpersons.Refresh();
        //        }


        //    }

        //}

        //private void removeIncidentCapture(object sender, EventArgs e, string name, XtraScrollableControl spnl_Injured)
        //{
        //    // pnl_InjuredCapture-1
        //    Int16 indx = Convert.ToInt16(name.Split('-')[1]);
        //    string controlName = name.Split('-')[0];

        //    spnl_Injured.Controls[indx].Dispose();

        //    for (Int16 i = indx; indx < injuredPersonsCount; i++)
        //    {
        //        spnl_Injured.Controls[indx].Name = controlName + '-' + (i - 1);
        //        spnl_Injured.Controls[indx].Location = new Point(spnl_Injured.Controls[indx].Location.X, spnl_Injured.Controls[indx].Location.Y - 30);
        //    }
        //}

        private void IncidentCapture_Load(object sender, EventArgs e)
        {

            initializeLookUpsData();
            //using (var context = new SIREMLocalEntities())
            //{
            //    lue_states.Properties.DataSource = context.List_States(null);

            //    lue_DriverLicenceState.Properties.DataSource = context.List_States(null);
            //}
        }

        private void btn_AddIncident_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            float lat = rand.Next(-101, 101) + Truncate((float)rand.NextDouble(), 7);
            float lon = rand.Next(-101, 101) + Truncate((float)rand.NextDouble(), 7);
            Debug.WriteLine(lat.ToString());
            DataRow folioReponse = Functions.Get_Folio().Select().First();
            string folio = folioReponse.ItemArray[2].ToString() + "-" + folioReponse.ItemArray[3].ToString();

            // receive a table of the SP
            DataTable dt_Injured = new DataTable();

            DataRow incidentResponse = Functions.AddIncidentReport(
                ID_Driver,
                lue_states.EditValue.ToString(),
                lue_Cities.EditValue.ToString(),
                Guid.NewGuid().ToString(),
                ID_Truck,
                ID_Trailer,
                folio,
                new DateTime(dte_IncidentDate.DateTime.Ticks + tme_IncidentTime.Time.Ticks),
                (bool)ckedt_PoliceReport.EditValue,
                GetEdtValue(edt_PoliceReport),
                (bool)ckedt_Spill.EditValue,
                GetEdtValue(edt_manifest),
                GetEdtValue(edt_Highway),
                lat.ToString(),
                lon.ToString(),
                (bool)ckedt_truckDamages.EditValue,
                (bool)ckedt_TruckCanMove.EditValue,
                (bool)ckedt_TruckNeedCrane.EditValue,
                (bool)ckedt_TrailerDamage.EditValue,
                (bool)ckedt_TrailerCanMove.EditValue,
                (bool)ckedt_TrailerNeedCrane.EditValue,
                constants.userID.ToString(),
                ""
            ).Select().First();


            foreach (DataRow row in dt_InjuredPersons.Rows)
            {
                string fullName = row["FullName"].ToString();
                string lastName1 = row["LastName1"].ToString();
                string lastName2 = row["LastName2"].ToString();
                string phoneNumber = row["Phone"].ToString();
                dt_Injured = Functions.updateInjuredPerson(Guid.Empty, fullName, lastName1, lastName2, phoneNumber, Guid.Parse(incidentResponse.ItemArray[2].ToString()));
                
            }

            if (dt_Injured.Rows.Count > 0)
            {
                MessageBox.Show(dt_Injured.Select().First().ItemArray[1].ToString());
            }
            
            //spnl_InjuredPersons.Controls.Add(CreatePersonInjuredCapture(pnl_loc));
            pnl_loc.Y += 39;

            //using (var context = new SIREMLocalEntities())
            //{

                //var results_1 = context.Database.SqlQuery<Update_Location_Result>("EXEC Update_Location {0}, {1}, {2}, {3}, {4}, {5}, {6}",
                //    null,
                //    Guid.Parse(lue_Cities.EditValue.ToString()),
                //    Guid.Parse(lue_states.EditValue.ToString()),
                //    edt_Highway.Text.ToString(),
                //    lat.ToString(),
                //    lon.ToString(),
                //    ""
                //).ToListAsync();

                //results_1.Wait();
                //Debug.WriteLine(results_1.Result[0].msg);

                //var id_driver = lbl_IdDriver.Text;
                //Task<List<Update_Driver_Result>> results_2;
                //if (id_driver == "empty")
                //{
                //    results_2 = context.Database.SqlQuery<Update_Driver_Result>("EXEC Update_Driver {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}",
                //        null,
                //        edt_FullName.Text.ToString(),
                //        "address",
                //        edt_License.Text.ToString(),
                //        lue_DriverLicenceState.EditValue.ToString(),
                //        dte_ExpirationDate.DateTime,
                //        edt_PhoneNumber.Text.ToString(),
                //        true
                //    ).ToListAsync();

                //    results_2.Wait();
                //    id_driver = results_2.Result[0].ID.ToString();
                //    Debug.WriteLine(results_2.Result[0].msg);
                //}
                //var results_3 = context.Database.SqlQuery<Update_Cargo_Result>("EXEC Update_Cargo {0}, {1}, {2}",
                //    null,
                //    edt_Cargo.EditValue.ToString(),
                //    edt_manifest.ToString()
                //).ToListAsync();
                //results_3.Wait();
                //Debug.WriteLine(results_3.Result[0].msg);
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

                //DateTime incidentDate = new DateTime(dte_IncidentDate.DateTime.Ticks + tme_IncidentTime.Time.Ticks);
                //bool policeReportBoolean = GetRadioGroupYesNoSelection(rdgrp_PoliceReport);
                //string policeReportNumber = GetEdtValue(edt_PoliceReport);
                //bool injuredBoolean = GetRadioGroupYesNoSelection(rdgrp_Injured);
                //string injuredNames = GetEdtValue(edt_InjuredNames);
                //string truckNumber = GetEdtValue(edt_TruckNumber);
                //string trailerNumber = GetEdtValue(edt_TrailerNumber);
                //bool truckDamage = GetRadioGroupYesNoSelection(rdgrp_TruckDamage);
                //bool trailerDamage = GetRadioGroupYesNoSelection(rdgrp_TrailerDamage);
                //bool cargoSpill = GetRadioGroupYesNoSelection(rdgrp_CargoSpill);
                //string manifestNumber = GetEdtValue(edt_manifest);
                //bool status = true;

                //    var Incident_Response = context.Update_Incident(
                //    null,
                //    //Guid.Parse("12D0FB1E-2797-EB11-BA73-8CDCD457CB4F"), 
                //    //Guid.Parse(id_driver),
                //    //Guid.Parse(results_1.Result[0].ID.ToString()),
                //    //Guid.Parse(results_3.Result[0].ID.ToString()),
                //    Guid.Parse("EB06B210-E102-4AEC-9820-8AD3A49060D9"),
                //    no,
                //    incidentDate,
                //    null,
                //    policeReportNumber,
                //    policeReportBoolean,
                //    "",
                //    injuredBoolean,
                //    injuredNames,
                //    truckNumber,
                //    trailerNumber,
                //    truckDamage,
                //    trailerDamage,
                //    cargoSpill,
                //    manifestNumber,
                //    Guid.Parse("7DC94B83-1E93-EB11-BA73-8CDCD457CB4F"),
                //    status
                //);

                //Debug.WriteLine(Incident_Response.ToList()[0].msg);

                //Debug.WriteLine(policeReportBool);
            //}

        }

        private void lue_States_Properties_EditValueChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(lue_states.EditValue);

            lue_Cities.Properties.DataSource = Functions.getCities(Guid.Parse(lue_states.EditValue.ToString()), "");
            //using (var context = new SIREMLocalEntities())
            //{
            //    lue_Cities.Properties.DataSource = context.List_Cities(Guid.Parse(lue_states.EditValue.ToString()), "");
            //}
        }

        public float Truncate(float value, int digits)
        {
            double mult = Math.Pow(10.0, digits);
            double result = Math.Truncate(mult * value) / mult;
            return (float)result;
        }


        //private static async void AddLocation(Location loc)
        //{
        //    //Random rand = new Random();
        //    //float lat = rand.Next(-101, 101) + Truncate((float)rand.NextDouble(), 7);
        //    //float lon = rand.Next(-101, 101) + Truncate((float)rand.NextDouble(), 7);
        //    //Debug.WriteLine(lat.ToString());
        //    using (var context = new SIREMLocalEntities())
        //    {


        //        //var location_response = context.Update_Location(
        //        //    loc.ID_Location, 
        //        //    loc.ID_City,
        //        //    loc.ID_State,
        //        //    loc.Highway,
        //        //    loc.Latitude,
        //        //    loc.Longitude,
        //        //    loc.References
        //        //    );
        //        Debug.WriteLine("here");
        //        //return results;

        //    }
        //}

        //private void edt_License_KeyUp(object sender, KeyEventArgs e)
        //{

        //}

        private void btn_LookUpLicence_Click(object sender, EventArgs e)
        {
            //using (var context = new SIREMLocalEntities())
            //{
            //    string license = GetEdtValue(edt_License);
            //var Driver_Response = context.Get_Driver(license, "", "").ToList()[0];
            //edt_FullName.EditValue = Driver_Response.Name.ToString();            
            //edt_PhoneNumber.EditValue = Driver_Response.phone_number.ToString();
            //edt_License.EditValue = Driver_Response.License.ToString();
            //lue_DriverLicenceState.EditValue = Guid.Parse(Driver_Response.Expidation_State.ToString());            
            //dte_ExpirationDate.EditValue = Driver_Response.Expiration_Date.ToString();
            //lbl_IdDriver.Text = Driver_Response.ID_Driver.ToString();
            //}
            //9092825

            string license = GetEdtValue(edt_License);
            var Driver_Response = Functions.getDriver(license, "", "");
            if (Driver_Response.ItemArray[0].ToString() == "0")
            {
                MessageBox.Show(Driver_Response.ItemArray[1].ToString());
            }
            else
            {
                ID_Driver = Driver_Response.ItemArray[0].ToString();
                string fullName = Driver_Response.ItemArray[3].ToString() + " " + Driver_Response.ItemArray[4].ToString();
                edt_FullName.EditValue = fullName;
                edt_PhoneNumber.EditValue = Driver_Response.ItemArray[6].ToString();
                edt_License.EditValue = Driver_Response.ItemArray[2].ToString();
                string[] dateArray = Driver_Response.ItemArray[8].ToString().Split('/');
                dte_ExpirationDate.EditValue = new DateTime(
                    Convert.ToInt32(dateArray[2].Split(' ')[0]),
                    Convert.ToInt32(dateArray[0]),
                    Convert.ToInt32(dateArray[1]));

                string filterExpression = "pk_id = '" + Driver_Response.ItemArray[1].ToString() + "'";
                lue_StateExp.EditValue = Functions.getStates().Select(filterExpression).First().ItemArray[0];
                //var dt_Driver = Functions.getDriver("9092825", "", "");
                Debug.WriteLine(Functions.getStates().Select(filterExpression).First().ItemArray[0].ToString());

            }
        }

        private void btn_LookUpPhoneNumber_Click(object sender, EventArgs e)
        {
            //using (var context = new SIREMLocalEntities())
            //{
            //    //string phoneNumber = GetEdtValue(edt_PhoneNumber);
            //    //var Driver_Response = context.Get_Driver("", "", "").ToList()[0];
            //    //edt_FullName.EditValue = Driver_Response.Name.ToString();
            //    ////edt_PhoneNumber.EditValue = Driver_Response.PhoneNumber.ToString();
            //    //edt_PhoneNumber.EditValue = "5611541325";
            //    //edt_License.EditValue = Driver_Response.License.ToString();
            //    //lue_DriverLicenceState.EditValue = Guid.Parse(Driver_Response.ExpidationState.ToString());
            //    //dte_ExpirationDate.EditValue = Driver_Response.ExpirationDate.ToString();
            //    //lbl_IdDriver.Text = Driver_Response.ID_Driver.ToString();
            //}

            string phoneNumber = GetEdtValue(edt_PhoneNumber);
            var Driver_Response = Functions.getDriver("", phoneNumber, "");
            if (Driver_Response.ItemArray[0].ToString() == "0")
            {
                MessageBox.Show(Driver_Response.ItemArray[1].ToString());
            }
            else
            {
                ID_Driver = Driver_Response.ItemArray[0].ToString();
                string fullName = Driver_Response.ItemArray[3].ToString() + " " + Driver_Response.ItemArray[4].ToString();
                edt_FullName.EditValue = fullName;
                edt_PhoneNumber.EditValue = Driver_Response.ItemArray[6].ToString();
                edt_License.EditValue = Driver_Response.ItemArray[2].ToString();
                string[] dateArray = Driver_Response.ItemArray[8].ToString().Split('/');
                dte_ExpirationDate.EditValue = new DateTime(
                    Convert.ToInt32(dateArray[2].Split(' ')[0]),
                    Convert.ToInt32(dateArray[0]),
                    Convert.ToInt32(dateArray[1]));

                string filterExpression = "pk_id = '" + Driver_Response.ItemArray[1].ToString() + "'";
                lue_StateExp.EditValue = Functions.getStates().Select(filterExpression).First().ItemArray[0];
            }
        }

        private void btn_LookUpName_Click(object sender, EventArgs e)
        {
            //using (var context = new SIREMLocalEntities())
            //{
            //    string fullName = GetEdtValue(edt_FullName);
            //    //var Driver_Response = context.Get_Driver("", "", fullName).ToList()[0];
            //    //edt_FullName.EditValue = Driver_Response.Name.ToString();
            //    //edt_PhoneNumber.EditValue = Driver_Response.phone_number.ToString();
            //    //edt_License.EditValue = Driver_Response.License.ToString();
            //    //lue_DriverLicenceState.EditValue = Guid.Parse(Driver_Response.Expidation_State.ToString());
            //    //dte_ExpirationDate.EditValue = Driver_Response.Expiration_Date.ToString();
            //    //lbl_IdDriver.Text = Driver_Response.ID_Driver.ToString();
            //}

            string name = GetEdtValue(edt_FullName);
            var Driver_Response = Functions.getDriver("", "", name);
            if (Driver_Response.ItemArray[0].ToString() == "0")
            {
                MessageBox.Show(Driver_Response.ItemArray[1].ToString());
            } 
            else
            {
                ID_Driver = Driver_Response.ItemArray[0].ToString();
                string fullName = Driver_Response.ItemArray[3].ToString() + " " + Driver_Response.ItemArray[4].ToString();
                edt_FullName.EditValue = fullName;
                edt_PhoneNumber.EditValue = Driver_Response.ItemArray[6].ToString();
                edt_License.EditValue = Driver_Response.ItemArray[2].ToString();
                string[] dateArray = Driver_Response.ItemArray[8].ToString().Split('/');
                dte_ExpirationDate.EditValue = new DateTime(
                    Convert.ToInt32(dateArray[2].Split(' ')[0]),
                    Convert.ToInt32(dateArray[0]),
                    Convert.ToInt32(dateArray[1]));

                string filterExpression = "pk_id = '" + Driver_Response.ItemArray[1].ToString() + "'";
                lue_StateExp.EditValue = Functions.getStates().Select(filterExpression).First().ItemArray[0];
            }

        }

        //private bool GetRadioGroupYesNoSelection(RadioGroup rdgrp)
        //{
        //    bool result = rdgrp.Properties.Items[rdgrp.SelectedIndex].Description.ToString().ToLower() == "yes";
        //    return result;
        //}

        private string GetEdtValue(TextEdit edt)
        {
            string result = edt.EditValue == null ? "" : edt.EditValue.ToString();
            return result;
        }

        private void SetEdtValue(TextEdit edt, string value)
        {
            edt.EditValue = value.ToString();

        }

        private void OnChangedCheckEdit(object sender, EventArgs e)
        {
            CheckEdit cb = (CheckEdit)sender;
            bool ckedtValue = (bool)cb.EditValue;
            cb.Properties.Caption = ckedtValue ? "Yes" : "No";

            switch (cb.Name)
            {
                case "ckedt_Spill":
                    pnl_BOL.Visible = ckedtValue;
                    break;
                case "ckedt_PoliceReport":
                    pnl_PoliceReport.Visible = ckedtValue;
                    break;
                case "ckedt_Injured":
                    panelControl3.Visible = ckedtValue;
                    pnl_AddInjuredFields.Visible = ckedtValue;
                    gc_InjuredPersons.Enabled = ckedtValue;

                    if (dt_InjuredPersons.Rows.Count == 0)
                        addEmptyRow();

                    break;
            }

        }

        private void AddIncidentDetails_Shown(object sender, EventArgs e)
        {
            if (edt_InjuredNames.Location.X + edt_InjuredNames.Size.Width <= 564)
            {
                //MessageBox.Show("you are fine");
                
            }
            else
            {
                //MessageBox.Show("creo que no se a poder carnalito");
            }
        }
        
        private void checkNumber_OnEdtLeave (object sender, EventArgs e)
        {
            TextEdit edt_Number =  (TextEdit) sender;
            DataTable dt_Response = new DataTable();
            string number = GetEdtValue(edt_Number);


            switch (edt_Number.Name)
            {
                case "edt_TruckNumber":
                    dt_Response = Functions.Get_Truck(number);
                    DataRow truckResponse = dt_Response.Select().First();
                    ID_Truck = truckResponse.ItemArray[0].ToString();
                    numberExists(lbl_TruckExists, truckResponse);
                    break;
                case "edt_TrailerNumber":
                    dt_Response = Functions.Get_Trailer(number);
                    DataRow trailerResponse = dt_Response.Select().First();
                    ID_Trailer = trailerResponse.ItemArray[0].ToString();
                    numberExists(
                        lbl_TrailerExists, 
                        trailerResponse,
                        "Trailer"
                    );
                    break;
            }
        }

        private void checkNumber_OnEdtKeyPress(object sender, KeyPressEventArgs e)
        {
            TextEdit edt_Number = (TextEdit)sender;
            DataTable dt_Response = new DataTable();
            string number = GetEdtValue(edt_Number);

            if (e.KeyChar == (char)13)
            { 
                switch (edt_Number.Name)
                {
                    case "edt_TruckNumber":
                        dt_Response = Functions.Get_Truck(number);
                        DataRow truckResponse = dt_Response.Select().First();
                        ID_Truck = truckResponse.ItemArray[0].ToString();
                        numberExists(lbl_TruckExists, truckResponse);
                        break;
                    case "edt_TrailerNumber":
                        dt_Response = Functions.Get_Trailer(number);
                        DataRow trailerResponse = dt_Response.Select().First();
                        ID_Trailer = trailerResponse.ItemArray[0].ToString();
                        numberExists(
                            lbl_TrailerExists, 
                            trailerResponse,
                            "Trailer"
                        );
                        break;
                }
            }


        }

        //private async void btn_AddInjuredPersonsControls_Click(object sender, EventArgs e)
        //{
        //    //int total = Convert.ToInt16(edt_NumberOfInjured.EditValue);
        //    //edt_NumberOfInjured.Enabled = false;
        //    //btn_AddInjuredPersonsControls.Enabled = false;
        //    //List<Task<string>> listOfTasks = new List<Task<string>>();

        //    //injuredPersonsCount += total;

        //    //for (int i = 0; i < total; i++)
        //    //{
        //    //    string pnlName = "pnl_InjuredCapture-" + i;
        //    //    //PanelControl pnl_Injured = CreatePersonInjuredCapture(pnl_loc);
        //    //    listOfTasks.Add(CreatePersonInjuredCapture(pnl_loc, spnl_InjuredPersons, pnlName));
        //    //    //spnl_InjuredPersons.Controls.Add(pnl_Injured);

        //    //    pnl_loc.Y += 30;
        //    //}

        //    //await Task.WhenAll<string>(listOfTasks);
        //    //edt_NumberOfInjured.Enabled = true;
        //    //btn_AddInjuredPersonsControls.Enabled = true;
        //}

        #region ICatalogView implementation
        public void SetController(IncidentController controller)
        {
            _controller = controller;
        }

        public DataRow GetDriverData()
        {
            DataTable dt_Empty = new DataTable();
            return dt_Empty.Select().First();
        }

        public string FirstName
        {
            get { return GetEdtValue(edt_FullName); }
            set { edt_FullName.EditValue.ToString(); }
        }

        public string LastName
        {
            get { return GetEdtValue(edt_FullName); }
            set { edt_FullName.EditValue.ToString(); }
        }

        public string PhoneNumber
        {
            get { return GetEdtValue(edt_PhoneNumber); }
            set { edt_FullName.EditValue.ToString(); }
        }


        public string License
        {
            get { return GetEdtValue(edt_License); }
            set { edt_FullName.EditValue.ToString(); }
        }
        #endregion

    }

}