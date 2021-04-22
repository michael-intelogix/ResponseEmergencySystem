using DevExpress.XtraEditors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ResponseEmergencySystem.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ResponseEmergencySystem.Samsara_Models;

namespace ResponseEmergencySystem.Forms
{



    public partial class ExtraForm : DevExpress.XtraEditors.XtraForm
    {

        #region

        public class Location
        {
            public string name { get; set; }
            public DateTime time { get; set; }
            public float latitude { get; set; }
            public float longitude { get; set; }
            public int heading { get; set; }
            public int speed { get; set; }
            public string formattedLocation { get; set; }
        }


        #endregion

        public ExtraForm()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            //IncidentReport report1 = new IncidentReport("data1", "Data2", "Data3");
            //var namefile = "test";
            //string ReportPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"{namefile}.pdf");
            //DevExpress.XtraPrinting.PdfExportOptions MyPdfOptions = new DevExpress.XtraPrinting.PdfExportOptions();
            //try
            //{
            //    report1.ExportToPdf(ReportPath);
            //}
            //catch 
            //{
            //    MessageBox.Show("Problem with the pdf");
            //    return;
            //}

            try
            {
                var url = "https://api.samsara.com/fleet/vehicles/locations";

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
                );

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "samsara_api_XwURzQhn0F9rijd0vqXwDgWir2zLWc");


                // List data response.
                HttpResponseMessage response = client.GetAsync(url).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.

                JObject rss = JObject.Parse(response.Content.ReadAsStringAsync().Result);

                JArray test = JArray.Parse(rss["data"].ToString());

                IList<Vehicle> locs = test.Select(p => new Vehicle
                {
                    name = (string)p["name"],
                    time = (DateTime)p["location"]["time"],
                    latitude = (float)p["location"]["latitude"],
                    longitude = (float)p["location"]["longitude"],
                    heading = (int)p["location"]["heading"],
                    speed = (int)p["location"]["speed"],
                    formattedLocation = (string)p["location"]["reverseGeo"]["formattedLocation"]
                }).ToList();

                var filtered = locs.Where(x => x.name == "046");

                foreach (var item in filtered)
                {
                    Debug.WriteLine(item.latitude.ToString());
                    Debug.WriteLine(item.longitude.ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            //DialogResult = DialogResult.Yes;

            //form_driver_report tuventana = new form_driver_report();
            //tuventana.ShowDialog();
        }
    }
}