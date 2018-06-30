using System;
using System.ComponentModel.DataAnnotations;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;

namespace RevojiWebApi.Models
{
    public class AppUser : User
    {
        [Required(ErrorMessage = "first_name_required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "last_name_required")]
        public string LastName { get; set; }

        public string City { get; set; }

        public string AdministrativeArea { get; set; }

        public string Country { get; set; }

        public AppUser(DBAppUser dBAppUser) : base(dBAppUser) {
            FirstName = dBAppUser.FirstName;
            LastName = dBAppUser.LastName;
            Handle = dBAppUser.Handle;
            Password = dBAppUser.Password;
            Email = dBAppUser.Email;
            City = dBAppUser.City;
            AdministrativeArea = dBAppUser.AdministrativeArea;
            Country = dBAppUser.Country;
        }

        public override void UpdateDB(DBTable dbModel)
        {
            base.UpdateDB(dbModel);

            DBAppUser dBAppUser = dbModel as DBAppUser;
            dBAppUser.FirstName = FirstName;
            dBAppUser.LastName = LastName;
            dBAppUser.Handle = Handle;
            dBAppUser.Email = Email;
            dBAppUser.City = City;
            dBAppUser.AdministrativeArea = AdministrativeArea;
            dBAppUser.Country = Country;

            if (!string.IsNullOrEmpty(Password))//should this be here? should be set oe
            {
                dBAppUser.SetPassword(Password);
            }
        }
    }
}
