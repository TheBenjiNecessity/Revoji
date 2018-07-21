using System;
using System.ComponentModel.DataAnnotations;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.Models
{
    public class Like : Model
    {
        [Required]
        public int AppUserID { get; set; }

        [Required]
        public int ReviewID { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public AppUser AppUser { get; set; }

        public Review Review { get; set; }

        public Like() { }

        public Like(DBLike dbLike) : base(dbLike.Id){
            Type = dbLike.Type;
            Created = dbLike.Created;
            AppUserID = dbLike.AppUserId;
            ReviewID = dbLike.ReviewId;

            AppUser = new AppUser(dbLike.DBAppUser);
            Review = new Review(dbLike.DBReview);
        }

        public override void UpdateDB(DBTable dbModel)
        {
            base.UpdateDB(dbModel);

            DBLike dbLike = dbModel as DBLike;

            dbLike.Type = Type;
            dbLike.Created = Created;
            dbLike.AppUserId = AppUserID;
            dbLike.ReviewId = ReviewID;
        }
    }
}
