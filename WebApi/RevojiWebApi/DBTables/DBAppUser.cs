using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RevojiWebApi.DBTables
{
    [Table("app_user")]
    public class DBAppUser : DBUser
    {
        public DBAppUser() : base()
        {
            Followers = new List<DBFollowing>();
            Followings = new List<DBFollowing>();
            Reviews = new List<DBReview>();

            Blockings = new List<DBBlocking>();
            Blockers = new List<DBBlocking>();
            Likes = new List<DBLike>();
            Replies = new List<DBReply>();
            Bookmarks = new List<DBBookmark>();
            Notifications = new List<DBNotification>();
        }

        public DBAppUser(JObject appUser) : base(appUser)
        {
            FirstName = (string)appUser["first_name"];
            LastName = (string)appUser["last_name"];
            Gender = (string)appUser["gender"];
            Religion = (string)appUser["religion"];
            Politics = (string)appUser["politics"];
            Education = (string)appUser["education"];
            Profession = (string)appUser["profession"];
            Interests = (string)appUser["interests"];
            City = (string)appUser["city"];
            AdministrativeArea = (string)appUser["administrative_area"];
            Country = (string)appUser["country"];
            
            Content = JsonConvert.SerializeObject(appUser["content"]);
            Settings = JsonConvert.SerializeObject(appUser["settings"]);
            Preferences = JsonConvert.SerializeObject(appUser["preferences"]);

            Joined = DateTime.Now;

            if (appUser["review_id"] != null)
            {
                DateOfBirth = DateTime.Parse((string)appUser["review_id"]);
            }

            Followers = new List<DBFollowing>();
            Followings = new List<DBFollowing>();
            Reviews = new List<DBReview>();

            Blockings = new List<DBBlocking>();
            Blockers = new List<DBBlocking>();
            Likes = new List<DBLike>();
            Replies = new List<DBReply>();
            Bookmarks = new List<DBBookmark>();
            Notifications = new List<DBNotification>();
        }

        [Required]
        [Column("firstname")]
        public string FirstName { get; set; }

        [Required]
        [Column("lastname")]
        public string LastName { get; set; }

        [Column("dob")]
        public DateTime? DateOfBirth { get; set; }

        [Column("gender")]
        public string Gender { get; set; }

        [Column("religion")]
        public string Religion { get; set; }

        [Column("politics")]
        public string Politics { get; set; }

        [Column("education")]
        public string Education { get; set; }

        [Column("profession")]
        public string Profession { get; set; }

        [Column("interests")]
        public string Interests { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("administrative_area")]
        public string AdministrativeArea { get; set; }

        [Column("country")]
        public string Country { get; set; }

        [Column("joined")]
        public DateTime? Joined { get; set; }

        [Column("content")]
        public string Content { get; set; }

        [Column("settings")]
        public string Settings { get; set; }

        [Column("preferences")]
        public string Preferences { get; set; }

        public virtual ICollection<DBFollowing> Followings { get; set; }
        public virtual ICollection<DBFollowing> Followers { get; set; }
        public virtual ICollection<DBBlocking> Blockings { get; set; }
        public virtual ICollection<DBBlocking> Blockers { get; set; }

        public virtual ICollection<DBReview> Reviews { get; set; }
        public virtual ICollection<DBLike> Likes { get; set; }// Liked many reviews
        public virtual ICollection<DBReply> Replies { get; set; }
        public virtual ICollection<DBBookmark> Bookmarks { get; set; }
        public virtual ICollection<DBNotification> Notifications { get; set; }

        public void update(JObject jObject)
        {
            FirstName = (string)jObject["first_name"] ?? FirstName;
            LastName = (string)jObject["last_name"] ?? LastName;
            DateOfBirth = jObject["review_id"] != null ? DateTime.Parse((string)jObject["review_id"]) : DateOfBirth;
            Gender = (string)jObject["gender"] ?? Gender;
            Religion = (string)jObject["religion"] ?? Religion;
            Politics = (string)jObject["politics"] ?? Politics;
            Education = (string)jObject["education"] ?? Education;
            Profession = (string)jObject["profession"] ?? Profession;
            Interests = (string)jObject["interests"] ?? Interests;
            City = (string)jObject["city"] ?? City;
            AdministrativeArea = (string)jObject["administrative_area"] ?? AdministrativeArea;
            Country = (string)jObject["country"] ?? Country;

            if (jObject["content"] != null)
            {
                var ContentObject = JObject.Parse(Content);
                ContentObject.Merge(
                    (JObject)jObject["content"],
                    new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Union }
                );
                Content = JsonConvert.SerializeObject(ContentObject);
            }

            if (jObject["settings"] != null)
            {
                var SettingsObject = JObject.Parse(Settings);
                SettingsObject.Merge(
                    (JObject)jObject["settings"],
                    new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Union }
                );
                Settings = JsonConvert.SerializeObject(SettingsObject);
            }

            if (jObject["preferences"] != null)
            {
                var PreferencesObject = JObject.Parse(Preferences);
                PreferencesObject.Merge(
                    (JObject)jObject["preferences"],
                    new JsonMergeSettings { MergeArrayHandling = MergeArrayHandling.Union }
                );
                Preferences = JsonConvert.SerializeObject(PreferencesObject);
            }
        }
    }
}
