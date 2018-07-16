using System;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.Models
{
	public class AppUserFollowing : Model
    {
        public DateTime Created { get; set; }
        
        public int FollowerId { get; set; }
        
        public int FollowingId { get; set; }

        public AppUserFollowing() { }

        public AppUserFollowing(DBFollowing dbFollowing) : base(dbFollowing.Id)
        {
            Created = dbFollowing.Created;
            FollowerId = dbFollowing.FollowerAppUserId;
            FollowingId = dbFollowing.FollowingAppUserId;
        }

        public override void UpdateDB(DBTable dbModel)
        {
            base.UpdateDB(dbModel);

            DBFollowing dbFollowing = dbModel as DBFollowing;
         
            dbFollowing.Created = Created;
            dbFollowing.FollowerAppUserId = FollowerId;
            dbFollowing.FollowingAppUserId = FollowingId;
        }
    }
}
