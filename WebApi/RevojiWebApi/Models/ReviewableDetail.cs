using System;
using System.Linq;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.Models
{
	public class ReviewableDetail : Reviewable
    {
		public Review[] Reviews { get; set; }

		dynamic Content;

		dynamic Info;

		public ReviewableDetail() { }

		public ReviewableDetail(DBReviewable dBReviewable) : base(dBReviewable)
		{
			Content = dBReviewable.Content;
			Info = dBReviewable.Info;
			Reviews = dBReviewable.Reviews.Select(r => new Review(r)).ToArray();
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
