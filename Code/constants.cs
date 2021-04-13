using ITXFramework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Storage;
using Google.Cloud.Firestore;
using System.Net;


namespace ResponseEmergencySystem.Code
{
    public static class constants
    {
        private static readonly ITXServerConection itx = new ITXServerConection();

        public static readonly string path = AppDomain.CurrentDomain.BaseDirectory + @"dcmanagement.json";
        public static SqlConnection GeneralConnection { get; }  = itx.DbCon("General");
        public static SqlConnection EmilioConn { get; } = itx.DbCon("DCManagement");

        public static Guid userID { get; set; }

        public static Guid idIncident { get; set; }

        public static Guid id_capture { get; set; }

        public static string userName { get; set; }




    }
}
