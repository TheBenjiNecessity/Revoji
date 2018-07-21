using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevojiWebApi.DBTables
{
    [Table("review")]
    public class DBReview : DBTable
    {
        [Column("title")]
        public string Title { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }

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
    }
}