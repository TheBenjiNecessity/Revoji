using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using RevojiWebApi.DBTables.JSONObjects;

namespace RevojiWebApi.DBTables
{
    [Table("reviewable")]
    public class DBReviewable : DBTable
    {
        public DBReviewable()
        {
			DBReviews = new List<DBReview>();
        }

        [Required]
        [Column("title")]
        public string Title { get; set; }

        [Required]
        [Column("type")]
        public string Type { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("title_image_url")]
        public string TitleImageUrl { get; set; }

        [Column("content", TypeName = "json")]
        public string ReviewableContentJSON { get; set; }

        [Column("info", TypeName = "json")]
        public string ReviewableInfoJSON { get; set; }

        [NotMapped]
        public ReviewableContent Content
        {
            get { return JsonConvert.DeserializeObject<ReviewableContent>(ReviewableContentJSON); }
            set { ReviewableContentJSON = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public ReviewableContent Info
        {
            get { return JsonConvert.DeserializeObject<ReviewableContent>(ReviewableInfoJSON); }
            set { ReviewableInfoJSON = JsonConvert.SerializeObject(value); }
        }
        
        public virtual ICollection<DBReview> DBReviews { get; set; }
    }
}
