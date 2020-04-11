using System;
using System.ComponentModel.DataAnnotations;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.Models
{
    public class Bookmark : Model
    {
        [Required]
        public int AppUserId { get; set; }

        [Required]
        public int ReviewId { get; set; }

        public DateTime? Created { get; set; }

        public AppUser AppUser { get; set; }

        public Review Review { get; set; }

        public Bookmark() { }

        public Bookmark(DBBookmark dBBookmark)
        {
            Created = dBBookmark.Created;
            AppUserId = dBBookmark.AppUserId;
            ReviewId = dBBookmark.ReviewId;
        }

        public void UpdateDB(DBBookmark dBBookmark)
        {
            dBBookmark.Created = Created;
            dBBookmark.AppUserId = AppUserId;
            dBBookmark.ReviewId = ReviewId;
        }
    }
}
