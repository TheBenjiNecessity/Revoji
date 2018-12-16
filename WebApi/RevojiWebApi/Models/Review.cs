using System;
using System.ComponentModel.DataAnnotations;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.Models
{
    public class Review : Model
    {
        [Required]
        public int AppUserID { get; set; }

        public int? ReviewableID { get; set; }

        public string Title { get; set; }//TODO remove?

        public DateTime? Created { get; set; }

        public string Comment { get; set; }

        [Required]
        public string Emojis { get; set; }

        public AppUser AppUser { get; set; }

        public Reviewable Reviewable { get; set; }

        public Review() { }

        public Review(DBReview dBReview) : base(dBReview.Id)
        {
            Title = dBReview.Title;
            Comment = dBReview.Comment;
            Emojis = dBReview.Emojis;
            AppUserID = dBReview.AppUserId;
            ReviewableID = dBReview.ReviewableId;
            Created = dBReview.Created;

            if (dBReview.DBAppUser != null && dBReview.DBReviewable != null) { //TODO: This isn't quite right
                AppUser = new AppUser(dBReview.DBAppUser);
                Reviewable = new Reviewable(dBReview.DBReviewable);
            }
        }

        public override void UpdateDB(DBTable dbModel)
        {
            base.UpdateDB(dbModel);

            DBReview dBReview = dbModel as DBReview;

            dBReview.Title = Title;
            dBReview.Comment = Comment;
            dBReview.Emojis = Emojis;
            dBReview.Created = Created;

            dBReview.AppUserId = AppUserID;
            if (ReviewableID.HasValue)
                dBReview.ReviewableId = ReviewableID.Value;
        }
    }
}
