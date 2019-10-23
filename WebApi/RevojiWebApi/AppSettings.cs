using System;
using System.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace RevojiWebApi
{
    public class AppSettings
    {
        public static IConfiguration Configuration { private get; set; }
        public static IHostEnvironment CurrentEnvironment { get; set; }

        public static string ConnectionString
        {
            get {
                if (CurrentEnvironment.IsProduction()) {
                    var appConfig = ConfigurationManager.AppSettings;

                    string dbname = "ebdb"; //appConfig["RDS_DB_NAME"];

                    if (string.IsNullOrEmpty(dbname)) return null;

                    string username = "benjinecessity"; //appConfig["RDS_USERNAME"];
                    string password = "thenetnecessity";//appConfig["RDS_PASSWORD"];
                    string hostname = "aa18y48p9gnz56o.ce1ceaxa3cbr.us-west-2.rds.amazonaws.com";//appConfig["RDS_HOSTNAME"];
                    string port = "5432";// appConfig["RDS_PORT"];

                    return "User ID=" + username + ";Password=" + password + ";Host=" + hostname + ";Port=" + port + ";Database=" + dbname + ";Integrated Security=true;Pooling=true;";
                } else {
                    return Configuration.GetConnectionString("DBConnectionString");
                }
            }
        }
    }
}
