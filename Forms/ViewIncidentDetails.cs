using DevExpress.XtraEditors;
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

namespace ResponseEmergencySystem.Forms
{
    // https://stackoverflow.com/questions/1774498/how-to-iterate-through-a-datatable
    public partial class ViewIncidentDetails : DevExpress.XtraEditors.XtraForm, IShowIncidentDetails
    {
        DataTable dt_InjuredPersons = new DataTable();

        public ViewIncidentDetails()
        {
            InitializeComponent();

            addEmptyRow();
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

        public bool TruckNeedCrane 
        {
            get { return (bool)ckedt_PoliceReport.EditValue; }
            set { ckedt_PoliceReport.EditValue = value; }
        }

        public string TrailerNumber 
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        public bool TrailerDamages 
        {
            get { return (bool)ckedt_PoliceReport.EditValue; }
            set { ckedt_PoliceReport.EditValue = value; }
        }

        public bool TrailerCanMove 
        {
            get { return (bool)ckedt_PoliceReport.EditValue; }
            set { ckedt_PoliceReport.EditValue = value; }
        }

        public bool TrailerNeedCrane 
        {
            get { return (bool)ckedt_PoliceReport.EditValue; }
            set { ckedt_PoliceReport.EditValue = value; }
        }

        public string CargoType 
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        public bool CargoSpill 
        {
            get { return (bool)ckedt_PoliceReport.EditValue; }
            set { ckedt_PoliceReport.EditValue = value; }
        }

        public string ManifestNumber 
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        public string Broker 
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        public DateTime IncidentDate { get; set; }

        public bool PoliceReport 
        {
            get { return (bool)ckedt_PoliceReport.EditValue; }
            set { ckedt_PoliceReport.EditValue = value; }
        }

        public string CitationReportNumber 
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        public string Latitude 
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        public string Longitude 
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        public string ID_State 
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }

        public string ID_City 
        {
            get { return edt_FullName.EditValue.ToString(); }
            set { edt_FullName.EditValue = value; }
        }
    }

}