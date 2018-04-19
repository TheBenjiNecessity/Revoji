﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevojiWebApi.DBTables
{
    [Table("admin_user")]
    public class AdminUser : User
    {
        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [Column("user_type")]
        public string UserType { get; set; }
    }
}
