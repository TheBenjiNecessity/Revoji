using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using RevojiWebApi.DBTables.JSONObjects;

namespace RevojiWebApi.DBTables
{
    [Table("app_user")]
    public class DBAppUser : DBUser
    {
        public DBAppUser()
        {
            Followers = new List<DBFollowing>();
            Followings = new List<DBFollowing>();
            Reviews = new List<DBReview>();
        }

        [Required]
        [Column("firstname")]
        public string FirstName { get; set; }

        [Required]
        [Column("lastname")]
        public string LastName { get; set; }

        [Column("dob")]
        public DateTime? DOB { get; set; }

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
        private string AppUserContentJSON { get; set; }

        [Column("settings")]
        private string AppUserSettingsJSON { get; set; }

        [Column("preferences")]
        private string AppUserPreferencesJSON { get; set; }

        [NotMapped]
        public AppUserContent Content
        {
            get { return JsonConvert.DeserializeObject<AppUserContent>(AppUserContentJSON); }
            set { AppUserContentJSON = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public AppUserSettings Settings
        {
            get { return JsonConvert.DeserializeObject<AppUserSettings>(AppUserSettingsJSON); }
            set { AppUserSettingsJSON = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public AppUserPreferences Preferences
        {
            get { return JsonConvert.DeserializeObject<AppUserPreferences>(AppUserPreferencesJSON); }
            set { AppUserPreferencesJSON = JsonConvert.SerializeObject(value); }
        }

        public virtual ICollection<DBFollowing> Followings { get; set; }
        public virtual ICollection<DBFollowing> Followers { get; set; }
        public virtual ICollection<DBBlocking> Blockings { get; set; }
        public virtual ICollection<DBBlocking> Blockers { get; set; }

        public virtual ICollection<DBReview> Reviews { get; set; }
        public virtual ICollection<DBLike> DBLikes { get; set; }// Liked many reviews
        public virtual ICollection<DBReply> DBReplies { get; set; }
    }
}
