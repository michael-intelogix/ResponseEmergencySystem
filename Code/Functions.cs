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

namespace ResponseEmergencySystem.Code
{
    public static class Functions
    {
        private static DataTable result;
        private static Boolean opSuccess;

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
                MessageBox.Show($"Record couldn't be saved due: {ex.Message}");
            }
            return result;
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
