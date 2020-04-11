using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevojiWebApi.DBTables
{
    [Table("blocking")]
    public class DBBlocking : DBTable
    {
        public DBBlocking() 
        {
            Blocker = new DBAppUser();
            Blocked = new DBAppUser();
        }

        [Column("blocker_app_user_id")]
        public int BlockerAppUserId { get; set; }

        [ForeignKey("BlockerAppUserId")]
        public virtual DBAppUser Blocker { get; set; }

        [Column("blocked_app_user_id")]
        public int BlockedAppUserId { get; set; }

        [ForeignKey("BlockedAppUserId")]
        public virtual DBAppUser Blocked { get; set; }
    }
}
