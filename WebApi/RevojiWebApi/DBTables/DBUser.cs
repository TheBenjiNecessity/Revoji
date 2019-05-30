using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BCrypt;

namespace RevojiWebApi.DBTables
{
    public class DBUser : DBTable
    {
        [Required]
        [Column("handle")]
        public string Handle { get; set; }

        [Required]
        [Column("password")]
        public string Password { get; set; }

        [Required]
        [Column("salt")]
        public string Salt { get; set; }

        [Required]
        [Column("email")]
        public string Email { get; set; }

        public bool isPasswordCorrect(string givenPassword) {
            return BCryptHelper.CheckPassword(givenPassword, Password);
        }

        public void SetPassword(string password) {
            Salt = BCryptHelper.GenerateSalt();
            Password = BCryptHelper.HashPassword(password, Salt);
        }
    }
}
