﻿using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace RevojiWebApi
{
    public class AppSettings
    {
        public static IConfiguration Configuration { private get; set; }

        public static string ConnectionString
        {
            get {
                var appConfig = ConfigurationManager.AppSettings;

                string dbname = appConfig["RDS_DB_NAME"];

                if (string.IsNullOrEmpty(dbname)) return null;

                string username = appConfig["RDS_USERNAME"];
                string password = appConfig["RDS_PASSWORD"];
                string hostname = appConfig["RDS_HOSTNAME"];
                string port = appConfig["RDS_PORT"];

                return "User ID=" + username + ";Password=" + password + ";Server=" + hostname + ";Port=" + port + ";Database=" + dbname + ";Integrated Security= true; Pooling=true;";
            }
        }
    }
}
