using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

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

        [Required]
        [Column("tp_id")]
        public string TpId { get; set; }

        [Required]
        [Column("tp_name")]
        public string TpName { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("title_image_url")]
        public string TitleImageUrl { get; set; }

        [Column("content")]
        public string Content{ get; set; }

        [Column("info")]
        public string Info { get; set; }
        
        public virtual ICollection<DBReview> DBReviews { get; set; }
    }
}
