using System;
using System.ComponentModel.DataAnnotations;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.Models
{
    public class Review : Model
    {
        public string Title { get; set; }

        public DateTime Created { get; set; }

        public string Comment { get; set; }

        public string Emojis { get; set; }

        public Reviewable Reviewable { get; set; }

        public Review() { }

        public Review(DBReview dbReview) : base(dbReview.Id)
        {
            Title = dbReview.Title;
            Comment = dbReview.Comment;
            Emojis = dbReview.Emojis;
        }

        public override void UpdateDB(DBTable dbModel)
        {
            base.UpdateDB(dbModel);

            DBReview dbReview = dbModel as DBReview;

            dbReview.Title = Title;
            dbReview.Comment = Comment;
            dbReview.Emojis = Emojis;
        }
    }
}
