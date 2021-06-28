using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using ResponseEmergencySystem.Models;
using System.Collections.Generic;
using ResponseEmergencySystem.Services;
using System.Data;

namespace ResponseEmergencySystem.Reports
{
    public partial class IncidentReport : DevExpress.XtraReports.UI.XtraReport
    {
        public IncidentReport(Incident incident)
        {
            InitializeComponent();

            var states = IncidentService.list_PersonsInvolved(incident.ID_Incident.ToString());
            if (states == null)
            {
                states = new List<PersonsInvolved>();
            }

            var locations = IncidentService.list_Locations(incident.ID_Incident.ToString());
            if (locations == null)
            {
                locations = new List<Location>();
            }

            DataSet dataSet1 = new DataSet();
            dataSet1.DataSetName = "nwindDataSet1";
            DataTable dataTable1 = new DataTable();

            dataSet1.Tables.Add(dataTable1);

            dataTable1.TableName = "Injured";
            dataTable1.Columns.Add("FullName", typeof(string));
            dataTable1.Columns.Add("Hospital", typeof(string));
            dataTable1.Columns.Add("PhoneNumber", typeof(string));

            for (var i = 0; i < 3; i++)
            {
                //dataSet1.Tables["Injured"].Rows.Add(new Object[] { states[i].ID_State, states[i].Name, states[i].Country });

            }

            this.DataSource = dataSet1.Tables[0];

            xrTableCell9.DataBindings.Add("Text", dataTable1, "FullName");
            xrTableCell10.DataBindings.Add("Text", dataTable1, "Hospital");
            xrTableCell11.DataBindings.Add("Text", dataTable1, "PhoneNumber");

            //CreateLabels(states);
            var test = CreateXRTable(states, new PointF(74f, 47.5f));
            this.Detail.Controls.Add(test);
            //this.Detail.Controls.Add(CreateXRTable2(locations, new PointF(74f, test.SizeF.Height + test.LocationF.Y + 20)));


            LoadData(incident);
        }

        private void LoadData(Incident incident)
        {
            lbl_Name.Text = incident.Name;
            lbl_License.Text = incident.driver.License;
            lbl_PhoneNumber.Text = incident.PhoneNumber;
            lbl_IncidentDate.Text = incident.IncidentDate.ToString("MM/dd/yyyy");
            lbl_IncidentTime.Text = incident.IncidentDate.ToString("hh:mm tt");
            lbl_LocationReferences.Text = incident.LocationReferences;
            cklbl_PoliceReport1.Checked = incident.PoliceReport == true ? true : false;
            cklbl_PoliceReport0.Checked = incident.PoliceReport == false ? true : false;
            lbl_PoliceReportNo.Text = incident.CitationReportNumber;
            lbl_TruckNo.Text = incident.truck.truckNumber;
            cklbl_TruckDamages1.Checked = incident.TruckDamage == true ? true : false;
            cklbl_TruckDamages0.Checked = incident.TruckDamage == false ? true : false;
            lbl_TrailerNo.Text = incident.trailer.TrailerNumber;
            cklbl_TrailerDamages1.Checked = incident.TrailerDamage == true ? true : false;
            cklbl_TrailerDamages0.Checked = incident.TrailerDamage == false ? true : false;
            lbl_Cargo.Text = incident.trailer.Commodity;
            cklbl_Spill1.Checked = incident.trailer.CargoSpill == true ? true : false;
            cklbl_Spill0.Checked = incident.trailer.CargoSpill == false ? true : false;
            lbl_BOL.Text = incident.ManifestNumber;

            lbl_VIN.Text = incident.truck.VinNumber;
            lbl_TruckTowingName.Text = incident.truck.TowingName;
            lbl_TruckTowingAddress.Text = incident.truck.TowedTo;

            lbl_TrailerTowingName.Text = incident.trailer.TowingName;
            lbl_TrailerTowingAddress.Text = incident.trailer.TowedTo;
        }

        public void CreateLabels(List<State> list)
        {
            
            for(var i = 0; i < list.Count; i ++)
            {
                XRLabel label = new XRLabel();
                label.Text = list[i].Name;
                label.LocationF = new Point(10, (i * 30));
                this.Detail.Controls.Add(label);
            }
        }

        public XRTable CreateXRTable(List<PersonsInvolved> list, PointF loc)
        {
            int cellsInRow = 4;
            int rowsCount = list.Count;
            float rowHeight = 200f;

            XRTable table = new XRTable();
            table.Name = "injured";
            table.Borders = DevExpress.XtraPrinting.BorderSide.All;
            table.SizeF = new SizeF(688.67f, 43.75f);
            table.LocationF = loc;
            table.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            table.BeginInit();

            table.Rows.Add(GetHeaderRow());

            for (int i = 0; i < rowsCount; i++)
            {
                XRTableRow row = new XRTableRow();
                row.HeightF = rowHeight;
                
                for (int j = 0; j < cellsInRow; j++)
                {
                    XRTableCell cell = new XRTableCell();

                    switch (j)
                    {
                        case 0:
                            cell.WidthF = (float)204.76;
                            cell.Text = list[i].FullName;
                            break;
                        case 1:
                            cell.WidthF = (float)154.74;
                            cell.Text = list[i].PhoneNumber;
                            break;
                        case 2:
                            cell.WidthF = (float)270.54;
                            cell.Text = list[i].Hospital;
                            break;
                        case 3:
                            cell.WidthF = (float)58.63;
                            cell.Text = list[i].IsPrivate;
                            break;
                    }
                        
                    row.Cells.Add(cell);
                }
                table.Rows.Add(row);
            }

            table.EndInit();
            return table;
        }

        private XRTableRow GetHeaderRow()
        {
            XRTableRow row = new XRTableRow();
            row.BackColor = Color.FromArgb(173, 216, 230);
            row.HeightF = 200f;

            for (int j = 0; j < 4; j++)
            {
                XRTableCell cell = new XRTableCell();

                switch (j)
                {
                    case 0:
                        cell.WidthF = (float)204.76;
                        cell.Text = "Name";
                        break;
                    case 1:
                        cell.WidthF = (float)154.74;
                        cell.Text = "Phone Number";
                        break;
                    case 2:
                        cell.WidthF = (float)270.54;
                        cell.Text = "Hospital";
                        break;
                    case 3:
                        cell.WidthF = (float)58.63;
                        cell.Text = "Own";
                        break;
                }

                row.Cells.Add(cell);
            }

            return row;
        }

        public XRTable CreateXRTable2(List<Location> list, PointF loc)
        {
            int cellsInRow = 4;
            int rowsCount = list.Count;
            float rowHeight = 200f;

            XRTable table = new XRTable();
            table.Borders = DevExpress.XtraPrinting.BorderSide.All;
            table.SizeF = new SizeF(688.67f, 43.75f);
            table.LocationF = loc;
            table.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            table.BeginInit();

            table.Rows.Add(GetHeaderRow2());

            for (int i = 0; i < rowsCount; i++)
            {
                XRTableRow row = new XRTableRow();
                row.HeightF = rowHeight;

                for (int j = 0; j < cellsInRow; j++)
                {
                    XRTableCell cell = new XRTableCell();

                    switch (j)
                    {
                        case 0:
                            cell.WidthF = (float)204.76;
                            cell.Text = list[i].Latitude;
                            break;
                        case 1:
                            cell.WidthF = (float)154.74;
                            cell.Text = list[i].Longitude;
                            break;
                        case 2:
                            cell.WidthF = (float)270.54;
                            cell.Text = list[i].Description;
                            break;
                        case 3:
                            cell.WidthF = (float)270.54;
                            cell.Text = list[i].CreatedAt.ToString();
                            break;
                    }

                    row.Cells.Add(cell);
                }
                table.Rows.Add(row);
            }

            table.EndInit();
            return table;
        }

        private XRTableRow GetHeaderRow2()
        {
            XRTableRow row = new XRTableRow();
            row.BackColor = Color.FromArgb(173, 216, 230);
            row.HeightF = 200f;

            for (int j = 0; j < 4; j++)
            {
                XRTableCell cell = new XRTableCell();

                switch (j)
                {
                    case 0:
                        cell.WidthF = (float)204.76;
                        cell.Text = "Latitude";
                        break;
                    case 1:
                        cell.WidthF = (float)154.74;
                        cell.Text = "Longitude";
                        break;
                    case 2:
                        cell.WidthF = (float)270.54;
                        cell.Text = "Description";
                        break;
                    case 3:
                        cell.WidthF = (float)270.54;
                        cell.Text = "Date";
                        break;
                }

                row.Cells.Add(cell);
            }

            return row;
        }


        //public void AddBoundTable(int numberOfDataColumns)
        //{
        //    // Create the table  
        //    XRTable detailTable = new XRTable();
        //    detailTable.LocationF = new PointF(0, 0);
        //    detailTable.BeginInit();

        //    // Create the row  
        //    XRTableRow newRow = new XRTableRow();

        //    newRow.BorderColor = Color.Black;
        //    newRow.BorderWidth = 1;
        //    newRow.Borders = DevExpress.XtraPrinting.BorderSide.All;
        //    newRow.HeightF = 25f;

        //    // Create the cells  
        //    for (int cellCount = 0; cellCount < numberOfDataColumns; cellCount++)
        //    {
        //        XRTableCell newCell = new XRTableCell();
        //        //string valueName = "DataItems[" + cellCount.ToString() + "]";  
        //        //newCell.DataBindings.Add("Text", DataSource, valueName);  

        //        newRow.Cells.Add(newCell);
        //    }
        //    detailTable.Rows.Add(newRow);

        //    // Size the table to fit the row  
        //    detailTable.AdjustSize();
        //    detailTable.EndInit();

        //    // Add the table to the ReportHeader band  
        //    Detail.Controls.Add(detailTable);
        //}

        //private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        //{
        //    List<RowObject> list = (List<RowObject>)this.DataSource;
        //    XRTable table = (XRTable)this.Detail.Controls[0];
        //    for (int i = 0; i < table.Rows[0].Cells.Count; i++)
        //    {
        //        table.Rows[0].Cells[i].Text = list[0].DataItems[i].ToString();
        //    }
        //}

    }
}
