using System;
using System.ComponentModel.DataAnnotations;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.Models
{
    public class Like : Model
    {
        public string Type { get; set; }

        [Required]
        public int AppUserId { get; set; }

        [Required]
        public int ReviewId { get; set; }

        public Like(DBLike dbLike) : base(dbLike.Id){
            Type = dbLike.Type;
            AppUserId = dbLike.AppUserId;
            ReviewId = dbLike.ReviewId;
        }

        public override void UpdateDB(DBTable dbModel)
        {
            base.UpdateDB(dbModel);

            DBLike dbLike = dbModel as DBLike;

            dbLike.Type = Type;
            dbLike.AppUserId = AppUserId;
            dbLike.ReviewId = ReviewId;
        }
    }
}
