using System;
using System.ComponentModel.DataAnnotations;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.Models
{
    public class User : Model
    {
        [Required(ErrorMessage = "handle_required")]
        public string Handle { get; set; }

        [Required(ErrorMessage = "password_required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "email_required")]
        public string Email { get; set; }

        public User() { }

        public User (DBUser dbUser) : base(dbUser.Id){
            Handle = dbUser.Handle;
            Password = dbUser.Password;
            Email = dbUser.Email;
        }

        public override void UpdateDB(DBTable dbModel)
        {
            base.UpdateDB(dbModel);

            DBUser dbUser = dbModel as DBUser;

            dbUser.Handle = Handle;
            dbUser.Password = Password;
            dbUser.Email = Email;

            if (!string.IsNullOrEmpty(Password))//should this be here? should be set once
            {
                dbUser.SetPassword(Password);
            }
        }
    }
}
