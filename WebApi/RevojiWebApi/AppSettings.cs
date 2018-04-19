using System;
using Microsoft.Extensions.Configuration;

namespace RevojiWebApi
{
    public class AppSettings
    {
        public static IConfiguration Configuration { private get; set; }

        public static string ConnectionString
        {
            get { return Configuration.GetConnectionString("DBConnectionString"); }
        }
    }
}
