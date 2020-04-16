using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RevojiWebApi.DBTables
{
    [Table("bookmark")]
    public class DBBookmark : DBTable
    {
        public DBBookmark()
        {
            DBAppUser = new DBAppUser();
            DBReview = new DBReview();
        }

        public DBBookmark(JObject bookmark) : this()
        {
            AppUserId = bookmark["app_user_id"] != null ? (int)bookmark["app_user_id"] : 0;
            ReviewId = bookmark["review_id"] != null ? (int)bookmark["review_id"] : 0;
            data = bookmark["data"] != null ? JsonConvert.SerializeObject(bookmark["data"]) : null;

            Created = DateTime.Now;
        }

        [Column("bookmark_data")]
        public string data { get; set; }

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
