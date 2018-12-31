using System;
using System.ComponentModel.DataAnnotations;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.Models
{
    public class AppUserBlocking
    {
        [Required]
        public int BlockerId { get; set; }

        [Required]
        public int BlockedId { get; set; }

        public AppUserBlocking() { }

        public AppUserBlocking(DBBlocking dBBlocking)
        {
            BlockerId = dBBlocking.BlockerAppUserId;
            BlockedId = dBBlocking.BlockedAppUserId;
        }

        public void UpdateDB(DBBlocking dBBlocking)
        {
            dBBlocking.BlockerAppUserId = BlockerId;
            dBBlocking.BlockedAppUserId = BlockedId;
        }
    }
}
