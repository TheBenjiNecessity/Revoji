using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json.Linq;

namespace RevojiWebApi.DBTables
{
    [Table("follower")]
    public class DBFollowing : DBTable
    {
        public DBFollowing()
        {
            Follower = new DBAppUser();
            Following = new DBAppUser();
        }

        [Column("created")]
        public DateTime? Created { get; set; }

        [Column("follower_app_user_id")]
        public int FollowerAppUserId { get; set; }

        [ForeignKey("FollowerAppUserId")]
        public virtual DBAppUser Follower { get; set; }

        [Column("following_app_user_id")]
        public int FollowingAppUserId { get; set; }

        [ForeignKey("FollowingAppUserId")]
        public virtual DBAppUser Following { get; set; }
    }
}
