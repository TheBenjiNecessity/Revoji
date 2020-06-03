using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json.Linq;
using RevojiWebApi.DBTables.DBContexts;

namespace RevojiWebApi.DBTables
{
    [Table("review")]
    public class DBReview : DBTable
    {
        public DBReview()
        {
            DBLikes = new List<DBLike>();
            DBReplies = new List<DBReply>();
            DBBookmarks = new List<DBBookmark>();
        }

        public DBReview(JObject review) : this()
        {
            AppUserId = review["app_user_id"] != null ? (int)review["app_user_id"] : 0;
            ReviewableId = review["reviewable_id"] != null ? (int)review["reviewable_id"] : 0;
            Title = (string)review["title"];
            Comment = (string)review["comment"];
            Emojis = (string)review["emojis"];

            if (review["reviewable"] != null)
            {
                DBReviewable = new DBReviewable((JObject)review["reviewable"]);
            }

            if (review["app_user"] != null)
            {
                DBAppUser = new DBAppUser((JObject)review["app_user"]);
            }

            Created = DateTime.Now;
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
        public virtual ICollection<DBBookmark> DBBookmarks { get; set; }

        public void update(JObject review)
        {
            AppUserId = (int)review["app_user_id"] == 0 ? AppUserId : (int)review["app_user_id"];
            ReviewableId = (int)review["reviewable_id"] == 0 ? ReviewableId : (int)review["reviewable_id"];
            Title = (string)review["title"] ?? Title;
            Comment = (string)review["comment"] ?? Comment;
            Emojis = (string)review["emojis"] ?? Emojis;
        }
    }
}