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
using ResponseEmergencySystem.Models;
using ResponseEmergencySystem.Services;

namespace ResponseEmergencySystem.Code
{
    public static class constants
    {
        private static readonly ITXServerConection itx = new ITXServerConection();

        //public static readonly string path = AppDomain.CurrentDomain.BaseDirectory + @"dcmanagement.json";
        public static readonly string path = Environment.CurrentDirectory + @"\dcmanagement.json";
        public static SqlConnection GeneralConnection { get; }  = itx.DbCon("General");
        public static SqlConnection SIREMConnection { get; }  = itx.DbCon("SIREM");
        public static SqlConnection DCManagement { get; } = itx.DbCon("DCManagement");
        public static SqlConnection EmilioConn { get; } = itx.DbCon("DCManagement");

        public static Guid userID { get; set; }
        public static Guid userIDTest { get; } = Guid.Parse("00000000-0000-0000-0000-000000000000");

        public static Guid idIncident { get; set; }

        public static Guid emptyId
        {
            get { return Guid.Empty; }
        }

        public static Guid id_capture { get; set; }

        public static string userName { get; set; }

        public static string folioCode { get; } = "SIREM";

        public static string system { get; } = "SIREM";

        public static List<State> states { get; set; }

    }
}
