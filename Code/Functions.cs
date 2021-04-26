using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ResponseEmergencySystem.Code.Captures;
using ResponseEmergencySystem.Forms;
using ResponseEmergencySystem.Forms.Modals;

namespace ResponseEmergencySystem.Code
{
    public static class Functions
    {
        private static DataTable result;
        private static Boolean opSuccess;

        private static DataTable errorsResult(string error)
        {
            DataTable dt_Errors = new DataTable();
            dt_Errors.Columns.Add("validation");
            dt_Errors.Columns.Add("result");

            DataRow newErrorRow = dt_Errors.NewRow();
            newErrorRow["validation"] = "0";
            newErrorRow["result"] = error;

            dt_Errors.Rows.Add(newErrorRow);

            return dt_Errors;
        } 

        public static DataTable getStates()
        {
            opSuccess = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.GeneralConnection,
                    CommandText = $"List_States",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@ID_Country", Guid.Parse("99F9B034-75BE-4615-88C6-8D64BC3549DC"));
                    result = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(result);
                    }
                    opSuccess = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"States couldn't be found due: {ex.Message}");
            }
            return result;
        }

        public static DataTable getCities(Guid ID_State, string state)
        {
            opSuccess = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.GeneralConnection,
                    CommandText = $"List_Cities",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@ID_State", ID_State);
                    cmd.Parameters.AddWithValue("@State_Name", state);
                    result = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(result);
                    }
                    opSuccess = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cities couldn't be found due: {ex.Message}");
            }
            return result;
        }

        public static DataRow getDriver(string license, string phone, string name) 
        {
            opSuccess = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.GeneralConnection,
                    CommandText = $"Get_Driver",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@License", license);
                    cmd.Parameters.AddWithValue("@Phone_Number", phone);
                    cmd.Parameters.AddWithValue("@Name", name);
                    result = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(result);
                    }
                    opSuccess = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Driver couldn't be found due: {ex.Message}");
            }

            if (result.Rows.Count > 1)
            {
                frm_DriverSearchList drivers = new frm_DriverSearchList(result);
                if (drivers.ShowDialog() == DialogResult.OK)
                {
                    return result.Select()[drivers.dt_DriverRowSelected];
                }

                return errorsResult("Please select a driver from the list").Select().First();
            }
            else if (result.Rows.Count == 1)
            {
                return result.Select().First();
            }
            else
            {
                if (license != "")
                {
                    return errorsResult($"There is no driver with license: {license}").Select().First();
                }

                if (phone != "")
                {
                    return errorsResult($"There is no driver with phone: {phone}").Select().First();
                }

                if (name != "")
                {
                    return errorsResult($"There is no driver with name: {name}").Select().First();
                }

                return errorsResult("There is no driver with the information supplied").Select().First();
            }
            //return result;
        }

        public static DataTable captureTable(string [] columns, List<Capture> data)
        {
            DataTable result = new DataTable();
            foreach (string column in columns)
            {
                result.Columns.Add(column);
            }

            foreach (Capture ic in data)
            {
                DataRow _data = result.NewRow();
                _data["ID_capture"] = ic.ID_Capture;
                _data["capture_date"] = ic.captureDate;
                _data["Type"] = ic.type;
                _data["CapturesByType"] = ic.totalOfcaptures;
                _data["Comments"] = ic.comments;
                result.Rows.Add(_data);
            }
            return result;
        }

        public static DataTable updateInjuredPerson(Guid ID, string fullName, string lastName1, string lastName2, string phone, Guid ID_Incident)
        {
            opSuccess = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"Update_InjuredPerson",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@ID_InjuredPerson", ID);
                    cmd.Parameters.AddWithValue("@fullName", fullName);
                    cmd.Parameters.AddWithValue("@lastName1", lastName1);
                    cmd.Parameters.AddWithValue("@lastName2", lastName2);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@ID_Incident", ID_Incident);
                    result = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(result);
                    }
                    opSuccess = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Injured person couldn't be saved due: {ex.Message}");
            }
            return result;
        }

        public static DataTable Get_Truck(string truckNumber)
        {
            opSuccess = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.GeneralConnection,
                    CommandText = $"Get_Truck",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@TruckNumber", truckNumber);
                    result = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(result);
                    }
                    opSuccess = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Truck couldn't be found due: {ex.Message}");
            }

            return result;
        }

        public static DataTable Get_Trailer(string trailerNumber)
        {
            opSuccess = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.GeneralConnection,
                    CommandText = $"Get_Trailer",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@TrailerNumber", trailerNumber);
                    result = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(result);
                    }
                    opSuccess = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Trailer couldn't be found due: {ex.Message}");
            }

            return result;
        }

        public static DataTable Get_Folio()
        {
            opSuccess = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"Update_Folio",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@Description", constants.folioCode);
                    result = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(result);
                    }
                    opSuccess = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Folio couldn't be found due: {ex.Message}");
            }
            return result;
        }

        public static DataTable AddIncidentReport(
            string ID_Driver,
            string ID_State,
            string ID_City,
            string ID_Broker,
            string ID_Truck,
            string ID_Trailer,
            string folio,
            DateTime incidentDate,
            bool policeReport,
            string citationReport,
            bool cargoSpill,
            string manifestNumber,
            string locationReferences,
            string incidentLatitude,
            string incidentLongitude,
            bool truckDamage,
            bool truckCanMove,
            bool truckNeedCrane,
            bool trailerDamage,
            bool trailerCanMove,
            bool trailerNeedCrane,
            string ID_User,
            string comments
        )
        {
            result = new DataTable();
            opSuccess = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"Update_Incident",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@ID_Incident", Guid.Empty);
                    cmd.Parameters.AddWithValue("@ID_Driver", ID_Driver);
                    cmd.Parameters.AddWithValue("@ID_State", ID_State);
                    cmd.Parameters.AddWithValue("@ID_City", ID_City);
                    cmd.Parameters.AddWithValue("@ID_Broker", ID_Broker);
                    cmd.Parameters.AddWithValue("@ID_Truck", ID_Truck);
                    cmd.Parameters.AddWithValue("@ID_Trailer", ID_Trailer);
                    cmd.Parameters.AddWithValue("@Folio", folio);
                    cmd.Parameters.AddWithValue("@IncidentDate", incidentDate);
                    cmd.Parameters.AddWithValue("@IncidentCloseDate", "");
                    cmd.Parameters.AddWithValue("@PoliceReportBoolean", policeReport);
                    cmd.Parameters.AddWithValue("@CitationReportNumber", citationReport);
                    cmd.Parameters.AddWithValue("@CargoSpill", cargoSpill);
                    cmd.Parameters.AddWithValue("@ManifestNumber", manifestNumber);
                    cmd.Parameters.AddWithValue("@LocationReferences", locationReferences);
                    cmd.Parameters.AddWithValue("@IncidentLatitude", incidentLatitude);
                    cmd.Parameters.AddWithValue("@IncidentLongitude", incidentLongitude);
                    cmd.Parameters.AddWithValue("@TruckDamage", truckDamage);
                    cmd.Parameters.AddWithValue("@TruckCanMove", truckCanMove);
                    cmd.Parameters.AddWithValue("@TruckNeedCrane", truckNeedCrane);
                    cmd.Parameters.AddWithValue("@TrailerDamage", trailerDamage);
                    cmd.Parameters.AddWithValue("@TrailerCanMove", trailerCanMove);
                    cmd.Parameters.AddWithValue("@TrailerNeedCrane", trailerNeedCrane);
                    cmd.Parameters.AddWithValue("@ID_User", ID_User);
                    cmd.Parameters.AddWithValue("@Comments", comments);
                    
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(result);
                    }
                    opSuccess = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Incident couldn't be found due: {ex.Message}");
            }
            return result;
        }

        public static DataTable list_CaptureType()
        {

            opSuccess = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.DCManagement,
                    CommandText = $"List_CaptureType",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@ID_CaptureType", "");
                    cmd.Parameters.AddWithValue("@Name", "");
                    cmd.Parameters.AddWithValue("@Description", constants.system);
                    cmd.Parameters.AddWithValue("@Status", 1);
                    result = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(result);
                    }
                    opSuccess = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Capture type couldn't be found due: {ex.Message}");
            }

            return result;
        }

        public static DataRow getBroker(string name)
        {
            opSuccess = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"List_Brokers",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@ID_State", "");
                    cmd.Parameters.AddWithValue("@ID_City", "");
                    cmd.Parameters.AddWithValue("@Broker", name);
                    cmd.Parameters.AddWithValue("@Address", "");
                    result = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(result);
                    }
                    opSuccess = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Broker couldn't be found due: {ex.Message}");
            }

            if (result.Rows.Count > 1)
            {
                frm_BrokerList brokers = new frm_BrokerList(result);
                if (brokers.ShowDialog() == DialogResult.OK)
                {
                    return result.Select()[brokers.dt_BrokerRowSelected];
                }

                return errorsResult("Please select a broker from the list").Select().First();
            }
            else if (result.Rows.Count == 1)
            {
                return result.Select().First();
            }
            
            return result.Select().First();
        }

        public static DataRow saveBroker(string name)
        {
            opSuccess = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"List_Brokers",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@ID_State", "");
                    cmd.Parameters.AddWithValue("@ID_City", "");
                    cmd.Parameters.AddWithValue("@Broker", name);
                    cmd.Parameters.AddWithValue("@Address", "");
                    result = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(result);
                    }
                    opSuccess = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Broker couldn't be found due: {ex.Message}");
            }

            if (result.Rows.Count > 1)
            {
                frm_BrokerList brokers = new frm_BrokerList(result);
                if (brokers.ShowDialog() == DialogResult.OK)
                {
                    return result.Select()[brokers.dt_BrokerRowSelected];
                }

                return errorsResult("Please select a broker from the list").Select().First();
            }
            else if (result.Rows.Count == 1)
            {
                return result.Select().First();
            }

            return result.Select().First();
        }

        public static DataTable list_Incidents(string folio, string driverId, string driverName, string truckNum, string statusDetailId, string incidentId = "00000000-0000-0000-0000-000000000000", string date1 = "", string date2 = "")
        {
            opSuccess = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.SIREMConnection,
                    CommandText = $"List_Incidents",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    cmd.Parameters.AddWithValue("@ID_Incident", Guid.Parse(incidentId));
                    cmd.Parameters.AddWithValue("@Folio", folio);
                    cmd.Parameters.AddWithValue("@ID_Driver", driverId);
                    cmd.Parameters.AddWithValue("@ID_StatusDetail", statusDetailId);
                    cmd.Parameters.AddWithValue("@DriverName", driverName);
                    cmd.Parameters.AddWithValue("@Truck_No", truckNum);
                    cmd.Parameters.AddWithValue("@Trailer_No", "");
                    cmd.Parameters.AddWithValue("@Date1", date1);
                    cmd.Parameters.AddWithValue("@Date2", date2);
                    result = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(result);
                    }
                    opSuccess = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Capture type couldn't be found due: {ex.Message}");
            }

            return result;
        }

        public static DataTable list_StatusDetail()
        {
            opSuccess = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand
                {
                    Connection = constants.GeneralConnection,
                    CommandText = $"List_StatusDetail",
                    CommandType = CommandType.StoredProcedure
                })
                {
                    result = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(result);
                    }
                    opSuccess = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Status Detail couldn't be found due: {ex.Message}");
            }

            return result;
        }

        //public static DataTable UpdateEmployee(Employee updateEmployee, bool bln_UsaInfo, out bool opSuccess)
        //{
        //    opSuccess = false;
        //    try
        //    {
        //        using (SqlCommand cmd = new SqlCommand
        //        {
        //            Connection = constants.GeneralConnection,
        //            CommandText = $"List_States",
        //            CommandType = CommandType.StoredProcedure
        //        })
        //        {
        //            cmd.Parameters.AddWithValue("@ID_Country", Guid.Parse("99F9B034-75BE-4615-88C6-8D64BC3549DC"));
        //            //cmd.Parameters.AddWithValue("@sID_Employee", updateEmployee.ID_Employee);
        //            //cmd.Parameters.AddWithValue("@sID_Position", updateEmployee.ID_Position);
        //            //cmd.Parameters.AddWithValue("@sID_Department", updateEmployee.ID_Department);
        //            //cmd.Parameters.AddWithValue("@sID_Payroll", updateEmployee.ID_PayRoll);
        //            //cmd.Parameters.AddWithValue("@EmployeeNumber", updateEmployee.EmployeeNumber);
        //            //cmd.Parameters.AddWithValue("@sName", updateEmployee.Name);
        //            //cmd.Parameters.AddWithValue("@sP_LastName", updateEmployee.P_LastName);
        //            //cmd.Parameters.AddWithValue("@sM_LastName", updateEmployee.M_LastName);
        //            //cmd.Parameters.AddWithValue("@sEmail", updateEmployee.Email);
        //            //cmd.Parameters.AddWithValue("@sEmail2", updateEmployee.Email2);
        //            //cmd.Parameters.AddWithValue("@sEmail3", updateEmployee.Email3);
        //            //cmd.Parameters.AddWithValue("@sPhone", updateEmployee.Phone);
        //            //cmd.Parameters.AddWithValue("@sCellphone", updateEmployee.Cellphone);
        //            //cmd.Parameters.AddWithValue("@sAD", updateEmployee.AD);
        //            ////cmd.Parameters.AddWithValue("@sPic", updateEmployee.Pic);
        //            //cmd.Parameters.Add("@sPic2", SqlDbType.VarBinary);
        //            //cmd.Parameters["@sPic2"].Value = updateEmployee.Pic;
        //            //cmd.Parameters.AddWithValue("@nPayroll", 0);
        //            //cmd.Parameters.AddWithValue("@bStatus", updateEmployee.Status);
        //            //cmd.Parameters.AddWithValue("@dAdmission", updateEmployee.HireDate);
        //            //cmd.Parameters.AddWithValue("@dBirthdate", updateEmployee.BirthDate);
        //            //cmd.Parameters.AddWithValue("@sBirthplace", updateEmployee.BirthPlace);
        //            //cmd.Parameters.AddWithValue("@sNSS", updateEmployee.NSS);
        //            //cmd.Parameters.AddWithValue("@sRFC", updateEmployee.RFC);
        //            //cmd.Parameters.AddWithValue("@sCURP", updateEmployee.CURP);
        //            //cmd.Parameters.AddWithValue("@sInfonavit", updateEmployee.Infonavit);
        //            //cmd.Parameters.AddWithValue("@sFonacot", updateEmployee.Fonacot);
        //            //cmd.Parameters.AddWithValue("@sBBVA", updateEmployee.BBVA);
        //            //cmd.Parameters.AddWithValue("@sMother", updateEmployee.Mother);
        //            //cmd.Parameters.AddWithValue("@sFather", updateEmployee.Father);
        //            //cmd.Parameters.AddWithValue("@sAddress", updateEmployee.Address);
        //            //cmd.Parameters.AddWithValue("@sID_City", updateEmployee.ID_City);
        //            //cmd.Parameters.AddWithValue("@sID_State", updateEmployee.ID_State);
        //            //cmd.Parameters.AddWithValue("@sZipCode", updateEmployee.ZipCode);
        //            //cmd.Parameters.AddWithValue("@sID_Gender", updateEmployee.Gender);
        //            //cmd.Parameters.AddWithValue("@sID_Manager", updateEmployee.ID_Manager);
        //            //cmd.Parameters.AddWithValue("@sAddedBy", updateEmployee.AddedBy);
        //            //cmd.Parameters.AddWithValue("@sAddedDate", updateEmployee.AddedDate);
        //            //cmd.Parameters.AddWithValue("@sUpdatedBy", updateEmployee.UpdatedBy);
        //            //cmd.Parameters.AddWithValue("@sUpdatedDate", updateEmployee.UpdatedDate);
        //            //cmd.Parameters.AddWithValue("@ID_Details", updateEmployee.ID_Detail);
        //            //cmd.Parameters.AddWithValue("@ID_BO", updateEmployee.ID_BO);
        //            //cmd.Parameters.AddWithValue("@NameEm", updateEmployee.NameEm);
        //            //cmd.Parameters.AddWithValue("@PhoneEm", updateEmployee.PhoneEm);
        //            //cmd.Parameters.AddWithValue("@CellEm", updateEmployee.CellEm);
        //            //cmd.Parameters.AddWithValue("@RelationEm", updateEmployee.RelationEm);
        //            //cmd.Parameters.AddWithValue("@ID_MaritalStatus", updateEmployee.ID_MaritalStatus);
        //            //cmd.Parameters.AddWithValue("@ID_Commodity", updateEmployee.ID_Commodity);
        //            //cmd.Parameters.AddWithValue("@ID_Bank", updateEmployee.ID_Bank);
        //            //cmd.Parameters.AddWithValue("@IDNumber", updateEmployee.IDNumber);
        //            //cmd.Parameters.AddWithValue("@EconomicDependants", updateEmployee.EconomicDependant);
        //            //cmd.Parameters.AddWithValue("@UsaInfo", bln_UsaInfo);
        //            result = new DataTable();
        //            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
        //            {
        //                sda.Fill(result);
        //            }
        //            opSuccess = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Record couldn't be saved due: {ex.Message}");
        //    }
        //    return result;
        //}
    }
}
