using System;
using System.Collections.Generic;
using System.Linq;

namespace RevojiWebApi.DBTables.Comparers
{
    public class ReviewComparer : IComparer<DBReview>
    {
        public ReviewComparer() { }

        public int Compare(DBReview firstReview, DBReview secondReview)
        {
            var repliesALength = firstReview.DBReplies.Count();
            var repliesBLength = secondReview.DBReplies.Count();
            var repliesLengthDifference = repliesALength - repliesBLength;

            var reviewAGreatLikesCount = firstReview.DBLikes.Where(r => r.ReviewId == firstReview.Id && r.agreeType == "great").Count();
            var reviewABadLikesCount = firstReview.DBLikes.Where(r => r.ReviewId == firstReview.Id && r.agreeType == "bad").Count();
            var reviewBGreatLikesCount = secondReview.DBLikes.Where(r => r.ReviewId == secondReview.Id && r.agreeType == "great").Count();
            var reviewBBadLikesCount = secondReview.DBLikes.Where(r => r.ReviewId == secondReview.Id && r.agreeType == "bad").Count();

            var reviewLikesDifference = (reviewAGreatLikesCount - reviewABadLikesCount) - (reviewBGreatLikesCount - reviewBBadLikesCount);

            return (repliesLengthDifference * 5) + reviewLikesDifference; // Replies are considered (5?) times more important than likes
        }
    }
}
