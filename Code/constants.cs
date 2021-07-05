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

        private static string ProductionServer = "35.223.136.179";
        private static string TestsServer = "34.135.124.146";
        private static bool TestApp = true;


        //public static readonly string path = AppDomain.CurrentDomain.BaseDirectory + @"dcmanagement.json";
        public static readonly string path = Environment.CurrentDirectory + @"\dcmanagement.json";
        public static SqlConnection GeneralConnection { get; }  = itx.DbCon("General", TestApp);
        public static SqlConnection SIREMConnection { get; }  = itx.DbCon("SIREM", TestApp);
        public static SqlConnection DCManagement { get; } = itx.DbCon("DCManagement", TestApp);
        public static SqlConnection EmilioConn { get; } = itx.DbCon("DCManagement", TestApp);

        public static string userID { get; set; }
        public static Guid userIDTest { get; } = Guid.Parse("00000000-0000-0000-0000-000000000000");

        public static Guid idIncident { get; set; }

        public static bool tester { get; private set; }

        public static Guid emptyId
        {
            get { return Guid.Empty; }
        }

        public static Guid id_capture { get; set; }

        public static string userName { get; set; }

        public static string folioCode { get; private set; } 

        public static string system { get; } = "SIREM";

        public static List<State> states { get; set; }

        public static List<Reason> reasons { get; } = new List<Reason>(){ 
            new Reason("Driver Judgement"),
            new Reason("Weather"),
            new Reason("Animals"),
            new Reason("Automotive"),
            new Reason("Other")
        };
        //"Driver Judgement", "Weather", "Animals", "Mechanical Issues", "Other" };

        public static List<Models.Action> actions = new List<Models.Action>()
        {
            new Models.Action("Fix Object Collision"),
            new Models.Action("While Turning"),
            new Models.Action("While Backing"),
            new Models.Action("Moving Object Collision"),
            new Models.Action("Couldn't be prevented"),
            new Models.Action("other")
        };

        public static void SetTester(bool isTester)
        {
            tester = isTester;
            folioCode = tester ? "TEST" : "SIREM"; 
        }

    }
}
