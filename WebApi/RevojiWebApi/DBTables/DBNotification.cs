using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevojiWebApi.DBTables
{
    [Table("user_notification")]
    public class DBNotification : DBTable
    {
        public DBNotification()
        {
            DBAppUser = new DBAppUser();
        }

        [Column("notification_data")]
        public string data { get; set; }

        [Column("created")]
        public DateTime? Created { get; set; }

        [Column("app_user_id")]
        public int AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public virtual DBAppUser DBAppUser { get; set; }
    }
}
