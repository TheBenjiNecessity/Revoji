using System;
using RevojiWebApi.DBTables;
using RevojiWebApi.StoredProcedures;

namespace RevojiWebApi.Models
{
    public class AppUserStats
    {
		public int FollowerCount { get; set; }

		public int FollowingCount { get; set; }

		public int LikeCount { get; set; }

		public int RecommendationCount { get; set; }

		public int ReviewCount { get; set; }

		public AppUserStats() { }

		public AppUserStats(int appUserId)
		{
			FollowerCount = AppUserSproc.GetAppUserFollowerCount(appUserId);
			FollowingCount = AppUserSproc.GetAppUserFollowingCount(appUserId);
			LikeCount = AppUserSproc.GetAppUserLikeCount(appUserId);
			RecommendationCount = AppUserSproc.GetAppUserRecommendationCount(appUserId);
			ReviewCount = AppUserSproc.GetAppUserReviewCount(appUserId);
		}
    }
}
