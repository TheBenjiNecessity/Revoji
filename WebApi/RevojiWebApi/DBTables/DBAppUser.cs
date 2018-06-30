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

        [Column("content")]
        private string AppUserContentJSON { get; set; }

        [Column("settings")]
        private string AppUserSettingsJSON { get; set; }

        [NotMapped]
        public ReviewableContent Content
        {
            get { return JsonConvert.DeserializeObject<ReviewableContent>(AppUserContentJSON); }
            set { AppUserContentJSON = JsonConvert.SerializeObject(value); }
        }

        [NotMapped]
        public ReviewableContent Settings
        {
            get { return JsonConvert.DeserializeObject<ReviewableContent>(AppUserSettingsJSON); }
            set { AppUserSettingsJSON = JsonConvert.SerializeObject(value); }
        }
         
        public virtual ICollection<DBFollowing> Followings { get; set; }
        public virtual ICollection<DBFollowing> Followers { get; set; }
    }
}
