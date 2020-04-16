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

        }

        public DBNotification(JObject notification) : this()
        {
            AppUserId = notification["app_user_id"] != null ? (int)notification["app_user_id"] : 0;
            data = notification["data"] != null ? JsonConvert.SerializeObject(notification["data"]) : null;

            Created = DateTime.Now;
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
