using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevojiWebApi.DBTables
{
    [Table("review_like")]
    public class DBLike
    {
        public DBLike()
        {
            DBAppUser = new DBAppUser();
            DBReview = new DBReview();
        }

        [Column("type")]
        public string agreeType { get; set; }

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
