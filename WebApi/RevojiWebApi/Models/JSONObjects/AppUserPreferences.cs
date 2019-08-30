using System;
using System.Linq;

namespace RevojiWebApi.Models
{
    public class AppUserPreferences
    {
        public int AgeRangeMin { get; set; }
        public int AgeRangeMax { get; set; }
        public float PoliticalAffiliation { get; set; }
        public float PoliticalOpinion { get; set; }
        public string[] Location { get; set; }
        public float Religiosity { get; set; }
        public float Personality { get; set; }

        public bool locationsMatch(string[] locations)
        {
            return Location.Intersect(locations).Any();
        }
    }
}