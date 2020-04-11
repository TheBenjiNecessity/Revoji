using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevojiWebApi.DBTables
{
    [Table("review_reply")]
    public class DBReply : DBTable
    {
        public DBReply()
        {
            DBAppUser = new DBAppUser();
            DBReview = new DBReview();
        }

        [Column("comment")]
        public string Comment { get; set; }

        [Column("created")]
        public DateTime? Created { get; set; }

        [Column("app_user_id")]
        public int AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public virtual DBAppUser DBAppUser { get; set; }

        [Column("review_id")]
        public int ReviewId { get; set; }

        [ForeignKey("ReviewId")]
        public virtual DBReview DBReview { get; set; }
    }
}
