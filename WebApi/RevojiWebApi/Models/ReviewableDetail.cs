using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;

namespace RevojiWebApi.Models
{
	public class ReviewableDetail : Reviewable
    {
        public int reviewCount { get; set; }

        public Dictionary<string, int> emojiCounts { get; set; }

        public string ReviewableContentJSON { get; set; }

        public string ReviewableInfoJSON { get; set; }

        public ReviewableContent Content
        {
            get { return JsonConvert.DeserializeObject<ReviewableContent>(ReviewableContentJSON); }
            set { ReviewableContentJSON = JsonConvert.SerializeObject(value); }
        }

        public ReviewableContent Info
        {
            get { return JsonConvert.DeserializeObject<ReviewableContent>(ReviewableInfoJSON); }
            set { ReviewableInfoJSON = JsonConvert.SerializeObject(value); }
        }

        public ReviewableDetail() { }

		public ReviewableDetail(DBReviewable dBReviewable) : base(dBReviewable)
		{
            ReviewableContentJSON = dBReviewable.Content;
            ReviewableInfoJSON = dBReviewable.Info;

            using (var context = new RevojiDataContext())
            {
                var reviews = context.Reviews.Where(r => r.ReviewableId == ID);
                reviewCount = reviews.Count();

                if (reviewCount == 0)
                {
                    emojiCounts = null;
                }

                string emojis = new string(reviews.SelectMany(r => r.Emojis)
                                                  .ToArray());

                emojiCounts = emojis.Split(",")
                                    .GroupBy(e => e, StringComparer.OrdinalIgnoreCase)
                                    .ToDictionary(group => group.Key, group => group.Count());
            }
        }

		public override void UpdateDB(DBTable dbModel)
		{
			base.UpdateDB(dbModel);

			DBReviewable dBReviewable = dbModel as DBReviewable;

            dBReviewable.Content = ReviewableContentJSON;
            dBReviewable.Info = ReviewableInfoJSON;
        }
    }
}
