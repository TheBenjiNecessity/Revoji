using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

namespace RevojiWebApi.StoredProcedures
{
    public class AppUserSproc
    {
        const string appUserIdParam = ":app_user_id";

        public static int GetAppUserFollowerCount(int appUserId)
        {
            return getCount("follower_count", parameters(appUserId));
        }

        public static int GetAppUserFollowingCount(int appUserId)
        {
            return getCount("following_count", parameters(appUserId));
        }

        public static int GetAppUserLikeCount(int appUserId)
        {
            return getCount("like_count", parameters(appUserId));
        }

        public static int GetAppUserRecommendationCount(int appUserId)
        {
            return -1;//TODO
        }

        public static int GetAppUserReviewCount(int appUserId)
        {
            return getCount("review_count", parameters(appUserId));
        }

        private static Dictionary<string, int> parameters(int appUserId)
        {
            return new Dictionary<string, int>()
            {
                { appUserIdParam, appUserId }
            };
        }

        static int getCount(string functionText, Dictionary<string, int> paramaters)
        {
            string commandText = "select " + functionText + "(" + appUserIdParam + ")";

            using (var connection = new NpgsqlConnection(AppSettings.ConnectionString))
            {
                connection.Open();

                var command = new NpgsqlCommand(commandText, connection);
                command.CommandType = CommandType.Text;

                foreach(KeyValuePair<string, int> paramater in paramaters)
                {
                    command.Parameters.AddWithValue(paramater.Key, paramater.Value);
                }

                NpgsqlDataReader reader = command.ExecuteReader();

                reader.Read();
                return int.Parse(reader.GetString(0));
            }
        }
    }
}
