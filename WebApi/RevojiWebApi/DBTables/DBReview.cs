using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using RevojiWebApi.DBTables.DBContexts;

namespace RevojiWebApi.DBTables
{
    [Table("review")]
    public class DBReview : DBTable, IComparable
    {
        public DBReview()
        {
            DBAppUser = new DBAppUser();
            DBReviewable = new DBReviewable();
            DBLikes = new List<DBLike>();
            DBReplies = new List<DBReply>();
        }

        [Column("title")]
        public string Title { get; set; }

        [Column("created")]
        public DateTime? Created { get; set; }

        [Column("comment")]
        public string Comment { get; set; }

        [Column("emojis")]//TODO: should this be strictly emojis? Should be simple but not just emojis
        public string Emojis { get; set; }

        [Column("app_user_id")]
        public int AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public virtual DBAppUser DBAppUser { get; set; }

        [Column("reviewable_id")]
        public int ReviewableId { get; set; }

        [ForeignKey("ReviewableId")]
        public virtual DBReviewable DBReviewable { get; set; }

        public virtual ICollection<DBLike> DBLikes { get; set; }
        public virtual ICollection<DBReply> DBReplies { get; set; }

        public int CompareTo(object obj) // Compares 'popularity'
        {
            var otherReview = obj as DBReview;
            var repliesALength = DBReplies.Count();
            var repliesBLength = otherReview.DBReplies.Count();
            var repliesLengthDifference = repliesALength - repliesBLength;

            var reviewAGreatLikesCount = DBLikes.Where(r => r.ReviewId == Id && r.agreeType == "great").Count();
            var reviewABadLikesCount = otherReview.DBLikes.Where(r => r.ReviewId == Id && r.agreeType == "bad").Count();
            var reviewBGreatLikesCount = DBLikes.Where(r => r.ReviewId == otherReview.Id && r.agreeType == "great").Count();
            var reviewBBadLikesCount = otherReview.DBLikes.Where(r => r.ReviewId == otherReview.Id && r.agreeType == "bad").Count();

            var reviewLikesDifference = (reviewAGreatLikesCount - reviewABadLikesCount) - (reviewBGreatLikesCount - reviewBBadLikesCount);

            return (repliesLengthDifference * 5) + reviewLikesDifference; //x5?
        }
    }
}