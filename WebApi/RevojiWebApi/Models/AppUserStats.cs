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

		public AppUserStats(int appUserId)
		{
			FollowerCount = FollowerCountSproc.GetAppUserFollowerCount(appUserId);
			FollowingCount = FollowerCountSproc.GetAppUserFollowerCount(appUserId);
			LikeCount = FollowerCountSproc.GetAppUserFollowerCount(appUserId);
			RecommendationCount = FollowerCountSproc.GetAppUserFollowerCount(appUserId);
			ReviewCount = FollowerCountSproc.GetAppUserFollowerCount(appUserId);
		}
    }
}
