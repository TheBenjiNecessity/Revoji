using System;
using System.ComponentModel.DataAnnotations;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.Models
{
    public class Reviewable : Model
    {
        [Required(ErrorMessage = "reviewable_title_required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "reviewable_type_required")]
        public string Type { get; set; }

        public string Description { get; set; }

        public Review[] Reviews { get; set; }

        dynamic Content;

        dynamic Info;

        public Reviewable(DBReviewable dBReviewable) : base(dBReviewable.Id)
        {
            Title = dBReviewable.Title;
            Type = dBReviewable.Type;
            Description = dBReviewable.Description;
            Content = dBReviewable.Content;
            Info = dBReviewable.Info;
        }

        public override void UpdateDB(DBTable dbModel)
        {
            base.UpdateDB(dbModel);

            DBReviewable dBReviewable = dbModel as DBReviewable;

            dBReviewable.Title = Title;
            dBReviewable.Type = Type;
            dBReviewable.Description = Description;
            dBReviewable.Content = Content;
            dBReviewable.Info = Info;
        }
    }
}
