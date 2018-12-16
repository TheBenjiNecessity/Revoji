using System;
using System.Linq;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.Models
{
    public class AppUserDetail : AppUser
    {
        public DateTime? DOB { get; set; }

        public string Gender { get; set; }

        public string Religion { get; set; }

        public string Politics { get; set; }

        public string Education { get; set; }

        public string Profession { get; set; }

        public string Interests { get; set; }

        public DateTime? Joined { get; set; }

        dynamic Content;

        dynamic Settings;

        public AppUserDetail() { }

        public AppUserDetail(DBAppUser dbAppUser) : base (dbAppUser)
        {
            DOB = dbAppUser.DOB;
            Gender = dbAppUser.Gender;
            Religion = dbAppUser.Religion;
            Politics = dbAppUser.Politics;
            Education = dbAppUser.Education;
            Profession = dbAppUser.Profession;
            Interests = dbAppUser.Interests;
        }

        public override void UpdateDB(DBTable dbModel)
        {
            base.UpdateDB(dbModel);

            DBAppUser dBAppUser = dbModel as DBAppUser;

            dBAppUser.DOB = DOB;
            dBAppUser.Gender = Gender;
            dBAppUser.Religion = Religion;
            dBAppUser.Politics = Politics;
            dBAppUser.Education = Education;
            dBAppUser.Profession = Profession;
            dBAppUser.Interests = Interests;
        }
    }
}
