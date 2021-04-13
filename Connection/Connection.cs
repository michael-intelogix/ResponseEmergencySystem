using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using ITXFramework;

namespace ResponseEmergencySystem.Connection
{
    class Connection
    {
        private static ITXServerConection itx = new ITXServerConection();
        private static SqlConnection GeneralConnection = itx.DbCon("SIREM");
        private static SqlConnection EmilioConn = itx.DbCon("DCManagement");

        public const string cn = @"Server=35.223.136.179; Initial Catalog=DCManagement; Persist Security Info=True;User ID=sqluser;Password=Int3logix20.-";
        private static DataTable dt_Captures = new DataTable();
        private static DataTable dt_Images = new DataTable();
        private static DataTable dt_StatusDetail = new DataTable();
        private static DataTable dt_Incidents = new DataTable();
 
        public static DataTable Dt_Captures
        {
            get { return dt_Captures; }
        }
        public static DataTable Dt_Images
        {
            get { return dt_Images; }
        }
        public static DataTable Dt_StatusDetail
        {
            get { return dt_StatusDetail; }
        }

        public static DataTable Dt_Incidents
        {
            get { return dt_Incidents; }
        }



        public static void Refresh_Captures(string idBusiness, string idBranchOffice, string idCaptureType, string dateTime, int status)
        {
            SqlConnection cn = new SqlConnection(Connection.cn);
            dt_Captures = new DataTable();

            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
            cn.Open();

            using (SqlCommand cmd = new SqlCommand
            {
                Connection = cn,
                CommandText = "SELECT * FROM List_Capture('" + idBusiness + "','" + idBranchOffice + "','" + idCaptureType + "','" + dateTime + "', " + status + ")"
            })
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt_Captures);
                }
            }

            cn.Close();
        }

        public static void List_StatusDetails()
        {
            SqlConnection cn = new SqlConnection(Connection.cn);
            dt_StatusDetail = new DataTable();

            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
            cn.Open();

            using (SqlCommand cmd = new SqlCommand
            {
                Connection = cn,
                CommandText = "SELECT * FROM List_StatusDetail()"
            })
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt_StatusDetail);
                }
            }

            cn.Close();
        }

        public static void Update_Incident(string idImage, string statusDetail)
        {
            SqlConnection cn = new SqlConnection(Connection.cn);
            dt_StatusDetail = new DataTable();

            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
            cn.Open();

            using (SqlCommand cmd = new SqlCommand
            {
                Connection = cn,
                CommandText = "exec Update_Image_StatusDetail '" + idImage + "' , '" + statusDetail + "'"
            })
            {
                cmd.ExecuteNonQuery();
            }

            cn.Close();
        }

        public static void List_Incidents()
        {

            SqlConnection cn = GeneralConnection;
            //SqlConnection cn = new SqlConnection(Connection.cn);
            dt_Incidents = new DataTable();


            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }

            cn.Open();

            using (SqlCommand cmd = new SqlCommand
            {
                Connection = cn,
                CommandText = "select *  from list_statusdetail()"
            })
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    sda.Fill(dt_Incidents);
                }
            }

            cn.Close();
        }
    }
}
