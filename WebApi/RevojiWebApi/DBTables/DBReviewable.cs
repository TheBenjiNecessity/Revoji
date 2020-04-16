using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RevojiWebApi.DBTables
{
    [Table("reviewable")]
    public class DBReviewable : DBTable
    {
        public DBReviewable()
        {
            DBReviews = new List<DBReview>();
        }

        public DBReviewable(JObject reviewable) : this()
        {
            Title = (string)reviewable["title"];
            Type = (string)reviewable["type"];
            TpId = (string)reviewable["tp_id"];
            TpName = (string)reviewable["tp_name"];
            Description = (string)reviewable["description"];
            TitleImageUrl = (string)reviewable["title_image_url"];

            Content = JsonConvert.SerializeObject(reviewable["content"]);
            Info = JsonConvert.SerializeObject(reviewable["info"]);
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

        public void update(JObject jObject)
        {
            Title = (string)jObject["title"] ?? Title;
            Type = (string)jObject["type"] ?? Type;
            TpId = (string)jObject["tp_id"] ?? TpId;
            TpName = (string)jObject["tp_name"] ?? TpName;
            Description = (string)jObject["description"] ?? Description;
            TitleImageUrl = (string)jObject["title_image_url"] ?? TitleImageUrl;

            if (jObject["content"] != null)
            {
                var ContentObject = JObject.Parse(Content);
                ContentObject.Merge(
                    (JObject)jObject["content"],
                    new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Union }
                );
                Content = JsonConvert.SerializeObject(ContentObject);
            }

            if (jObject["info"] != null)
            {
                var InfoObject = JObject.Parse(Info);
                InfoObject.Merge(
                    (JObject)jObject["info"],
                    new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Union }
                );
                Info = JsonConvert.SerializeObject(InfoObject);
            }
        }
    }
}
