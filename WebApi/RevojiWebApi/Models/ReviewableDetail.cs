using System;
using System.Linq;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.Models
{
	public class ReviewableDetail : Reviewable
    {
		dynamic Content;

		dynamic Info;

		public Review[] Reviews { get; set; }

		public ReviewableDetail() { }

		public ReviewableDetail(DBReviewable dBReviewable) : base(dBReviewable)
		{
			Content = dBReviewable.Content;
			Info = dBReviewable.Info;
			Reviews = dBReviewable.DBReviews.Select(r => new Review(r)).ToArray();
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
