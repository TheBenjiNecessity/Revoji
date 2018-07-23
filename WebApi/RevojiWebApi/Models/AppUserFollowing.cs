using System;
using System.ComponentModel.DataAnnotations;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.Models
{
	public class AppUserFollowing
    {
        [Required]
        public int FollowerId { get; set; }

        [Required]
        public int FollowingId { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public AppUser Follower { get; set; }

        public AppUser Following { get; set; }

        public AppUserFollowing() { }

        public AppUserFollowing(DBFollowing dbFollowing)
        {
            Created = dbFollowing.Created;
            FollowerId = dbFollowing.FollowerAppUserId;
            FollowingId = dbFollowing.FollowingAppUserId;

            Follower = new AppUser(dbFollowing.Follower);
            Following = new AppUser(dbFollowing.Following);
        }

        public void UpdateDB(DBFollowing dbFollowing)
        {
            dbFollowing.Created = Created;
            dbFollowing.FollowerAppUserId = FollowerId;
            dbFollowing.FollowingAppUserId = FollowingId;
        }
    }
}
