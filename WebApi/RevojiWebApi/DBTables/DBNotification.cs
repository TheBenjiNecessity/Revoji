using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RevojiWebApi.DBTables
{
    [Table("user_notification")]
    public class DBNotification : DBTable
    {
        public DBNotification()
        {
            DBAppUser = new DBAppUser();
        }

        public DBNotification(JObject notification)
        {
            AppUserId = -1;
            if (notification["appUserID"] != null)
            {
                AppUserId = (int)notification["appUserID"];
            }

            data = JsonConvert.SerializeObject(notification["data"]);
        }

        [Column("notification_data", TypeName = "jsonb")]
        public string data { get; set; }

        [Column("created")]
        public DateTime? Created { get; set; }

        [Column("app_user_id")]
        public int AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public virtual DBAppUser DBAppUser { get; set; }
    }
}
