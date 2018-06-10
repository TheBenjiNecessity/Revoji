using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevojiWebApi.DBTables
{
    public class DBFollowing : DBTable
    {
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
