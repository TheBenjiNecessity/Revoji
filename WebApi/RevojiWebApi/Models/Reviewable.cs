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

        [Required(ErrorMessage = "reviewable_tpid_required")]
        public string TpId { get; set; }

        [Required(ErrorMessage = "reviewable_tpname_required")]
        public string TpName { get; set; } // The name in base64 (why?) of the third party api used

        public string Description { get; set; }

        public string TitleImageUrl { get; set; }

        public Reviewable() { }

        public Reviewable(DBReviewable dBReviewable) : base(dBReviewable.Id)
        {
            Title = dBReviewable.Title;
            Type = dBReviewable.Type;
            TitleImageUrl = dBReviewable.TitleImageUrl;
            Description = dBReviewable.Description;
            TpId = dBReviewable.TpId;
            TpName = dBReviewable.TpName;
        }

        public override void UpdateDB(DBTable dbModel)
        {
            base.UpdateDB(dbModel);

            DBReviewable dBReviewable = dbModel as DBReviewable;

            dBReviewable.Title = Title;
            dBReviewable.Type = Type;
            dBReviewable.TitleImageUrl = TitleImageUrl;
            dBReviewable.Description = Description;
            dBReviewable.TpId = TpId;
            dBReviewable.TpName = TpName;
        }
    }
}
