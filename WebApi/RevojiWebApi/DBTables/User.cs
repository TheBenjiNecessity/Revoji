using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RevojiWebApi.DBTables
{
    public class User : Table
    {
        [Column("handle")]
        public string Handle { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Required]
        [Column("email")]
        public string Email { get; set; }
    }
}
