using System;
using System.Data;
using Npgsql;

namespace RevojiWebApi.StoredProcedures
{
	public class FollowerCountSproc
    {
		const string appUserIdParam = ":app_user_id";
		const string commandText = "select follower_count(" + appUserIdParam + ")";
        
		public static int GetAppUserFollowerCount(int appUserId)
		{
			using (var connection = new NpgsqlConnection(AppSettings.ConnectionString))
			{
				connection.Open();

				var command = new NpgsqlCommand(commandText, connection);
				command.CommandType = CommandType.Text;
				command.Parameters.AddWithValue(appUserIdParam, appUserId);

				NpgsqlDataReader reader = command.ExecuteReader();

				reader.Read();
				return int.Parse(reader.GetString(0));
			}
		}
    }
}
