using System;
using System.Collections.Generic;
using System.Linq;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;

namespace RevojiWebApi.Models
{
	public class ReviewableDetail : Reviewable
    {
        public int reviewCount { get; set; }

        public Dictionary<string, int> emojiCounts { get; set; }

        dynamic Content;

        dynamic Info;

        public ReviewableDetail() { }

		public ReviewableDetail(DBReviewable dBReviewable) : base(dBReviewable)
		{
			Content = dBReviewable.Content;
			Info = dBReviewable.Info;

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

			dBReviewable.Content = Content;
			dBReviewable.Info = Info;
		}
    }
}
