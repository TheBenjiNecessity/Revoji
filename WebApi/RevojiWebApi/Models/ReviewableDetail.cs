using System;
using System.Linq;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.Models
{
	public class ReviewableDetail : Reviewable
    {
		dynamic Content;

		dynamic Info;

		public ReviewableDetail() { }

		public ReviewableDetail(DBReviewable dBReviewable) : base(dBReviewable)
		{
			Content = dBReviewable.Content;
			Info = dBReviewable.Info;
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
