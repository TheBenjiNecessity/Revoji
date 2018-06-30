using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using RevojiWebApi.DBTables.JSONObjects;

namespace RevojiWebApi.DBTables
{
    public class DBReviewable : DBTable
    {
        [Required]
        [Column("title")]
        public string Title { get; set; }

        [Required]
        [Column("type")]
        public string Type { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("content")]
        public string ReviewableContentJSON { get; set; }

        [Column("info")]
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
    }
}
