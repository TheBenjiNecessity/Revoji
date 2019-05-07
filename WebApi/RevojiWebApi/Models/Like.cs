using System;
using System.ComponentModel.DataAnnotations;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.Models
{
    public class Like
    {
        [Required]
        public int AppUserID { get; set; }

        [Required]
        public int ReviewID { get; set; }

        [Required]
        public string agreeType { get; set; }

        public DateTime? Created { get; set; }

        public AppUser AppUser { get; set; }

        public Review Review { get; set; }

        public Like() { }

        public Like(DBLike dbLike) {
            agreeType = dbLike.agreeType;
            Created = dbLike.Created;
            AppUserID = dbLike.AppUserId;
            ReviewID = dbLike.ReviewId;
        }

        public void UpdateDB(DBLike dBLike)
        {
            dBLike.agreeType = agreeType;
            dBLike.Created = Created;
            dBLike.AppUserId = AppUserID;
            dBLike.ReviewId = ReviewID;
        }
    }
}
