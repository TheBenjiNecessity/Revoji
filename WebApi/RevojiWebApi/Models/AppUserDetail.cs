﻿using System;
using System.Linq;
using Newtonsoft.Json;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.Models
{
    public class AppUserDetail : AppUser
    {
        public DateTime? DateOfBirth { get; set; }

        public string Gender { get; set; }

        public string Religion { get; set; }

        public string Politics { get; set; }

        public string Education { get; set; }

        public string Profession { get; set; }

        public string Interests { get; set; }

        public DateTime? Joined { get; set; }

        public int Age
        {
            get
            {
                if (DateOfBirth.HasValue)
                {
                    DateTime zeroTime = new DateTime(1, 1, 1);
                    TimeSpan span = DateTime.Now - DateOfBirth.Value;
                    return (zeroTime + span).Year - 1;
                }
                else 
                {
                    return -1;
                }
            }
        }

        public AppUserDetail() { }

        public AppUserDetail(DBAppUser dbAppUser) : base (dbAppUser)
        {
            DateOfBirth = dbAppUser.DateOfBirth;
            Gender = dbAppUser.Gender;
            Religion = dbAppUser.Religion;
            Politics = dbAppUser.Politics;
            Education = dbAppUser.Education;
            Profession = dbAppUser.Profession;
            Interests = dbAppUser.Interests;
            Joined = dbAppUser.Joined;
        }

        public override void UpdateDB(DBTable dbModel)
        {
            base.UpdateDB(dbModel);

            DBAppUser dBAppUser = dbModel as DBAppUser;

            dBAppUser.DateOfBirth = DateOfBirth;
            dBAppUser.Gender = Gender;
            dBAppUser.Religion = Religion;
            dBAppUser.Politics = Politics;
            dBAppUser.Education = Education;
            dBAppUser.Profession = Profession;
            dBAppUser.Interests = Interests;
            dBAppUser.Joined = Joined;
        }
    }
}
