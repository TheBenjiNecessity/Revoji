using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BCrypt;
using Newtonsoft.Json.Linq;

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

        public bool isPasswordCorrect(string givenPassword)
        {
            return BCryptHelper.CheckPassword(givenPassword, Password);
        }

        public void SetPassword(string password)
        {
            Salt = BCryptHelper.GenerateSalt();
            Password = BCryptHelper.HashPassword(password, Salt);
        }

        public DBUser() { }

        public DBUser(JObject appUser)
        {
            Handle = (string)appUser["handle"];
            Email = (string)appUser["email"];

            SetPassword((string)appUser["password"]);
        }

        public void update(JObject appUser)
        {
            Handle = (string)appUser["handle"] ?? Handle;
            Email = (string)appUser["email"] ?? Email;

            if (appUser["password"] != null)
            {
                SetPassword((string)appUser["password"]);
            }
        }
    }
}
