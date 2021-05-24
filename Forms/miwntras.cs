using DevExpress.XtraEditors;
using Newtonsoft.Json.Linq;
using ResponseEmergencySystem.Models;
using ResponseEmergencySystem.Models.Testing;
using ResponseEmergencySystem.Properties;
using ResponseEmergencySystem.Samsara_Models;
using ResponseEmergencySystem.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponseEmergencySystem.Forms
{
    public partial class miwntras : DevExpress.XtraEditors.XtraForm
    {
        private List<Models.Samsara.Driver> _DriversSamsara = new List<Models.Samsara.Driver>();
        private List<Models.Driver> _Drivers = new List<Models.Driver>();
        public List<DriverData> _DriverData = new List<DriverData>();

        List<Models.Truck> _trucks = new List<Truck>();
        List<Samsara_Models.Vehicle> _TrucksSamsara = new List<Samsara_Models.Vehicle>();

        public miwntras()
        {
            InitializeComponent();
        }

        private void miwntras_Load(object sender, EventArgs e)
        {
            CreatePanel(4);

            //_DriversSamsara = GetDriversSamsara();
            //_Drivers = GetDrivers();

            //gridControl4.DataSource = _Drivers;

            //gridControl3.DataSource = _DriversSamsara;

            //gridControl5.DataSource = GetTrucksSamsara();

            //gridControl6.DataSource = GeneralService.list_Trucks();

            //string path = $"{Settings.Default.AppFolder}\\Drivers Samsara.xlsx";
            //gridControl3.ExportToXlsx(path);

            //string path2 = $"{Settings.Default.AppFolder}\\Drivers Interno.xlsx";
            //gridControl4.ExportToXlsx(path2);

            //string path3 = $"{Settings.Default.AppFolder}\\Trucks Samsara.xlsx";
            //gridControl5.ExportToXlsx(path3);

            //string path4 = $"{Settings.Default.AppFolder}\\Trucks Interno.xlsx";
            //gridControl6.ExportToXlsx(path4);
            //// Open the created XLSX file with the default application.
            //Process.Start(path);
            //Process.Start(path2);
            //Process.Start(path3);
            //Process.Start(path4);

            //foreach (var driver in _Drivers)
            //{
            //    var obj = new DriverData();
            //    obj.DriverName = driver.Name;
            //    obj.PatSurname = driver.LastName1;
            //    obj.MatSurname = driver.LastName2;
            //    if (driver.LastName2 == "")
            //    {
            //        var result = _DriversSamsara.Where(ds => ds.Name.Contains(driver.Name) && ds.Name.Contains(driver.LastName1)).ToList<Models.Samsara.Driver>();

            //        if (result.Count > 0)
            //        {
            //            for (int i = 0; i < result.Count; i++)
            //            {
            //                string fullname = string.Join(" ", new string[] { driver.Name.Trim(), driver.LastName1.Trim(), driver.LastName2.Trim() });

            //                obj.Name2 = result[i].Name;
            //                obj.License = result[i].LicenseNumber;
            //                obj.PhoneNumber = result[i].Phone;
            //                obj.LicenseState = result[i].LicenseState;
            //                obj.Repeated = (i > 0);
            //                obj.NotFound = false;
            //                obj.FullNameMatched = result[i].Name == fullname.Trim();

            //                _DriverData.Add(obj);
            //            }
            //        }
            //        else
            //        {
            //            obj.Name2 = "";
            //            obj.License = "";
            //            obj.PhoneNumber = "";
            //            obj.LicenseState = "";
            //            obj.Repeated = false;
            //            obj.NotFound = true;

            //            _DriverData.Add(obj);
            //        }
            //    }
            //    else
            //    {
    
            //        var result = _DriversSamsara.Where(ds => ds.Name.Contains(driver.Name) && ds.Name.Contains(driver.LastName1) && ds.Name.Contains(driver.LastName2)).ToList<Models.Samsara.Driver>();

            //        if (result.Count > 0)
            //        {
            //            for (int i = 0; i < result.Count; i++)
            //            {
            //                string fullname = string.Join(" ", new string[] { driver.Name.Trim(), driver.LastName1.Trim(), driver.LastName2.Trim() });

            //                obj.Name2 = result[i].Name;
            //                obj.License = result[i].LicenseNumber;
            //                obj.PhoneNumber = result[i].Phone;
            //                obj.LicenseState = result[i].LicenseState;
            //                obj.Repeated = (i > 0);
            //                obj.NotFound = false;
            //                obj.FullNameMatched = result[i].Name == fullname.Trim();

            //                _DriverData.Add(obj);
            //            }
            //        }
            //        else
            //        {
            //            obj.Name2 = "";
            //            obj.License = "";
            //            obj.PhoneNumber = "";
            //            obj.LicenseState = "";
            //            obj.Repeated = false;
            //            obj.NotFound = true;

            //            _DriverData.Add(obj);
            //        }

            //    }
            //}

            //lbl_DriversCount.Text = $"Drivers Count: {_Drivers.Count}";
            //lbl_SamsaraCount.Text = $"Drivers Samsara Count: {_DriversSamsara.Count}";
            //lbl_DriversFound.Text = $"Drivers Found Count: {_DriverData.Where(dd => dd.NotFound == false).Count()}";
            //lbl_DriversRepeated.Text = $"Drivers Repeated Count: {_DriverData.Where(dd => dd.Repeated == true).Count()}";
            //lbl_DriversNameNotMatch.Text = $"Drivers Fullname Not Matched: {_DriverData.Where(dd => !dd.FullNameMatched).Count()}"; 
            //gridControl2.DataSource = _DriverData;
        }

        private List<Models.Samsara.Driver> GetDriversSamsara() 
        {
            const string url = "https://api.samsara.com/fleet/drivers";

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);

                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "samsara_api_XwURzQhn0F9rijd0vqXwDgWir2zLWc");

                    // List data response.
                    HttpResponseMessage response = client.GetAsync(url).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.}

                    var data = JArray.Parse(
                        JObject.Parse(
                            response.Content.ReadAsStringAsync().Result
                        )["data"].ToString()
                    );

                    List<Models.Samsara.Driver> drivers = data.Select(p => new Models.Samsara.Driver
                    {
                        ID = (string)p["id"],
                        Name = (string)p["name"],
                        Phone = (string)p["phone"],
                        LicenseNumber = (string)p["licenseNumber"],
                        LicenseState = (string)p["licenseState"]
                    }).ToList();

                    //Dispose once all HttpClient calls are complete.This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
                    client.Dispose();

                    return drivers;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Models.Samsara.Driver>();
            }
        }

        private List<Driver> GetDrivers()
        {
            return DriverService.List_Drivers();
        }

        private List<Samsara_Models.Vehicle> GetTrucksSamsara()
        {
            const string url = "https://api.samsara.com/fleet/vehicles/locations";

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);

                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "samsara_api_XwURzQhn0F9rijd0vqXwDgWir2zLWc");

                    // List data response.
                    HttpResponseMessage response = client.GetAsync(url).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.}

                    var data = JArray.Parse(
                        JObject.Parse(
                            response.Content.ReadAsStringAsync().Result
                        )["data"].ToString()
                    );

                    List<Vehicle> locs = data.Select(p => new Vehicle
                    {
                        ID = (string)p["id"],
                        name = p["name"].ToString().Trim(),
                        time = (DateTime)p["location"]["time"],
                        latitude = (float)p["location"]["latitude"],
                        longitude = (float)p["location"]["longitude"],
                        heading = (int)p["location"]["heading"],
                        speed = (int)p["location"]["speed"],
                        formattedLocation = (string)p["location"]["reverseGeo"]["formattedLocation"]
                    }).ToList();

                    //Dispose once all HttpClient calls are complete.This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
                    client.Dispose();

                    return locs;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return new List<Vehicle>();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(textEdit1.EditValue); 
        }

        private void CreatePanel(int number)
        {
            int space = (Convert.ToInt32(this.Size.Width) - (number * 245)) / (number + 1);

            for (int i = 0; i < number; i++)
            {
                space = (space < 50) ? 50 : space;

                var x = (i * 245) + ((i + 1) * space);

                PanelControl pnl = new PanelControl();
                pnl.Size = new Size(245, 122);
                pnl.Location = new Point(x , 46);

                PictureEdit pic = new PictureEdit();

                pic.Dock = DockStyle.Fill;
                pic.Image = Resources.placeholder;
                pic.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;

                pnl.Controls.Add(pic);


                this.Controls.Add(pnl);
               
               
            }

            

        }
    }


}