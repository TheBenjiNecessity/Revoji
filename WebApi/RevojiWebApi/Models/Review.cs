using System;
using System.ComponentModel.DataAnnotations;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.Models
{
    public class Review : Model
    {
        [Required]
        public int AppUserID { get; set; }

        [Required]
        public int ReviewableID { get; set; }

        public string Title { get; set; }

        public DateTime Created { get; set; }

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

            AppUser = new AppUser(dBReview.DBAppUser);
            Reviewable = new Reviewable(dBReview.DBReviewable);
        }

        public override void UpdateDB(DBTable dbModel)
        {
            base.UpdateDB(dbModel);

            DBReview dBReview = dbModel as DBReview;

            dBReview.Title = Title;
            dBReview.Comment = Comment;
            dBReview.Emojis = Emojis;

            dBReview.AppUserId = AppUser.ID;
            dBReview.ReviewableId = Reviewable.ID;
        }
    }
}
