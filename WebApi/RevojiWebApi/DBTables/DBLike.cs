using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json.Linq;

namespace RevojiWebApi.DBTables
{
    [Table("review_like")]
    public class DBLike : DBTable
    {
        public DBLike()
        {
            DBAppUser = new DBAppUser();
            DBReview = new DBReview();
        }

        public DBLike(JObject like) : this()
        {
            AppUserId = like["app_user_id"] != null ? (int)like["app_user_id"] : 0;
            ReviewId = like["review_id"] != null ? (int)like["review_id"] : 0;
            agreeType = (string)like["type"];

            Created = DateTime.Now;
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
