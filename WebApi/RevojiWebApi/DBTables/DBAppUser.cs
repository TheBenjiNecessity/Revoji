using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

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
    }
}
