using System;
using System.ComponentModel.DataAnnotations;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.Models
{
    public class Notification : Model
    {
        [Required]
        public int AppUserId { get; set; }

        public DateTime? Created { get; set; }

        public AppUser AppUser { get; set; }

        public Notification() { }

        public Notification(DBNotification dBNotification)
        {
            Created = dBNotification.Created;
            AppUserId = dBNotification.AppUserId;
        }

        public void UpdateDB(DBNotification dBNotification)
        {
            dBNotification.Created = Created;
            dBNotification.AppUserId = AppUserId;
        }
    }
}
