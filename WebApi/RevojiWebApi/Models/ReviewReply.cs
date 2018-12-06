using System;
using System.ComponentModel.DataAnnotations;
using RevojiWebApi.DBTables;
using RevojiWebApi.Models;

namespace RevojiWebApi
{
    public class Reply
    {
        [Required]
        public int AppUserID { get; set; }

        [Required]
        public int ReviewID { get; set; }

        [Required]
        public string Comment { get; set; }

        public DateTime Created { get; set; }

        public AppUser AppUser { get; set; }

        public Review Review { get; set; }

        public Reply() { }

        public Reply(DBReply dbReply)
        {
            Comment = dbReply.Comment;
            Created = dbReply.Created;
            AppUserID = dbReply.AppUserId;
            ReviewID = dbReply.ReviewId;
        }

        public void UpdateDB(DBReply dbReply)
        {
            dbReply.Comment = Comment;
            dbReply.Created = Created;
            dbReply.AppUserId = AppUserID;
            dbReply.ReviewId = ReviewID;
        }
    }
}