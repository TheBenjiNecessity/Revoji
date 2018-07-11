using System;
namespace RevojiWebApi.Models
{
    public class ReviewableStats
    {
		public string[] emojis { get; set; }

		public string[] words { get; set; }
        
		public ReviewableStats()
		{
		}
    }
}
