using System;
using System.ComponentModel.DataAnnotations;

namespace RevojiWebApi.Models
{
    public class User
    {
        [Required(ErrorMessage = "handle_required")]
        public string Handle { get; set; }

        [Required(ErrorMessage = "password_required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "email_required")]
        public string Email { get; set; }

        public User()
        {
        }
    }
}
