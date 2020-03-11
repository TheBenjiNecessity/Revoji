using System;
using System.Linq;
using System.Collections.Generic;

namespace RevojiWebApi.DBTables.Comparers
{
    public class UserPopularityComparer : IComparer<DBAppUser>
    {
        public UserPopularityComparer() { }

        public int Compare(DBAppUser firstUser, DBAppUser secondUser)
        {
            var firstUserFollowersCount = firstUser.Followers.Count();
            var secondUserFollowersCount = secondUser.Followers.Count();

            var firstUserReviewsCount = firstUser.Reviews.Count();
            var secondUserReviewsCount = secondUser.Reviews.Count();

            return ((firstUserFollowersCount * 5) - (secondUserFollowersCount * 5)) + (firstUserReviewsCount - secondUserReviewsCount);
        }
    }
}
