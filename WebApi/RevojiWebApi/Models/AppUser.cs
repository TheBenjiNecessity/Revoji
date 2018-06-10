using System;
using System.ComponentModel.DataAnnotations;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;

namespace RevojiWebApi.Models
{
    public class AppUser : User
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "first_name_required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "last_name_required")]
        public string LastName { get; set; }

        public DateTime? DOB { get; set; }

        public string Gender { get; set; }

        public string Religion { get; set; }

        public string Politics { get; set; }

        public string Education { get; set; }

        public string Profession { get; set; }

        public string Interests { get; set; }

        public string City { get; set; }

        public string AdministrativeArea { get; set; }

        public string Country { get; set; }

        public string AppUserContentJSON { get; set; }

        public string AppUserSettingsJSON { get; set; }

        public void UpdateDB(DBAppUser dbAppUser)
        {
            dbAppUser.FirstName = FirstName;
            dbAppUser.LastName = LastName;
            dbAppUser.Handle = Handle;
            dbAppUser.Email = Email;
            dbAppUser.DOB = DOB;
            dbAppUser.Gender = Gender;
            dbAppUser.Religion = Religion;
            dbAppUser.Politics = Politics;
            dbAppUser.Education = Education;
            dbAppUser.Profession = Profession;
            dbAppUser.Interests = Interests;
            dbAppUser.City = City;
            dbAppUser.AdministrativeArea = AdministrativeArea;
            dbAppUser.Country = Country;

            if (!string.IsNullOrEmpty(Password))//should this be here? should be set once
            {
                dbAppUser.SetPassword(Password);
            }
        }

        public AppUser(DBAppUser dbAppUser)
        {
            FirstName = dbAppUser.FirstName;
            LastName = dbAppUser.LastName;
            Handle = dbAppUser.Handle;
            Password = dbAppUser.Password;
            Email = dbAppUser.Email;
            DOB = dbAppUser.DOB;
            Gender = dbAppUser.Gender;
            Religion = dbAppUser.Religion;
            Politics = dbAppUser.Politics;
            Education = dbAppUser.Education;
            Profession = dbAppUser.Profession;
            Interests = dbAppUser.Interests;
            City = dbAppUser.City;
            AdministrativeArea = dbAppUser.AdministrativeArea;
            Country = dbAppUser.Country;
        }

        public AppUser() {}
    }
}
