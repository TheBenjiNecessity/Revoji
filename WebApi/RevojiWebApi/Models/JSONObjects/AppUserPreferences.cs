using System;
namespace RevojiWebApi.Models
{
    public class AppUserPreferences
    {
        public string AgeRange { get; set; }
        public float PoliticalAffiliation { get; set; }
        public float PoliticalOpinion { get; set; }
        public string Location { get; set; }
        public float Religiosity { get; set; }
        public float Personality { get; set; }
    }
}
