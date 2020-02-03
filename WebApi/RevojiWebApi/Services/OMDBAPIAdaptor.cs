using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RevojiWebApi.DBTables.DBContexts;
using RevojiWebApi.Models;

namespace RevojiWebApi.Services
{
    public class OMDBAPIAdaptor : ReviewableAPIAdaptor
    {
        public static string TPNAME = "imdb";

        public OMDBAPIAdaptor()
        {
            apiKey = "72f1abd1";
            apiUrl = "http://www.omdbapi.com/?apikey=" + apiKey;
        }

        public override string getUrlForId(string id) 
        {
            return apiUrl + "&i=" + id;
        }

        public override string getUrlForSearch(string searchText, int pageOffset, int pageLimit)
        {
            return apiUrl + "&s=" + searchText + "&p=" + pageOffset;
        }

        public override async Task<Reviewable> GetReviewableByIDAsync(string id)
        {
            Reviewable reviewable = null;
            HttpResponseMessage response = await client.GetAsync(getUrlForId(id));

            if (response.IsSuccessStatusCode)
            {
                var reviewableString = await response.Content.ReadAsStringAsync();
                OMDBJSONResponse reviewableJSON = JsonConvert.DeserializeObject<OMDBJSONResponse>(reviewableString);

                reviewable = new Reviewable();
                reviewable.Title = reviewableJSON.title;
                reviewable.Type = reviewableJSON.type;
                reviewable.TitleImageUrl = reviewableJSON.poster;
                reviewable.TpId = reviewableJSON.imdbID;
                reviewable.TpName = TPNAME;
                reviewable.Description = reviewableJSON.plot;

                using (var context = new RevojiDataContext())
                {
                    var rev = context.Reviewables.Where(r => r.TpId == reviewable.TpId && r.TpName == TPNAME).FirstOrDefault();
                    if (rev != null) {
                        reviewable.ID = rev.Id;
                    }
                }
            }

            return reviewable;
        }

        public override async Task<Reviewable[]> SearchReviewablesAsync(string searchText, int pageOffset, int pageLimit)
        {
            Reviewable[] reviewables = null;
            HttpResponseMessage response = await client.GetAsync(getUrlForSearch(searchText, pageOffset, pageLimit));

            if (response.IsSuccessStatusCode)
            {
                var reviewableString = await response.Content.ReadAsStringAsync();
                OMDBJSONSearchResponse search = JsonConvert.DeserializeObject<OMDBJSONSearchResponse>(reviewableString);

                if (search.Response)
                {
                    reviewables = search.reviewables.Select(r => {
                        var reviewable = new Reviewable();
                        reviewable.Title = r.title;
                        reviewable.Type = r.type;
                        reviewable.TitleImageUrl = r.image;
                        reviewable.TpId = r.imdbID;
                        reviewable.TpName = TPNAME;
                        return reviewable;
                    }).ToArray();
                }
            }

            return reviewables;
        }
    }

    public class OMDBJSONSearchResponse
    {
        [JsonProperty("search")]
        public List<OMDBJSONSearchItemResponse> reviewables { get; set; }

        public bool Response { get; set; }
    }

    public class OMDBJSONSearchItemResponse
    {
        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("poster")]
        public string image { get; set; }

        [JsonProperty("year")]
        public string year { get; set; }

        [JsonProperty("imdbID")]
        public string imdbID { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }
    }

    public class OMDBJSONResponse
    {
        [JsonProperty("title")]
        public string title { get; set; }

        [JsonProperty("year")]
        public string year { get; set; }

        [JsonProperty("poster")]
        public string poster { get; set; }

        [JsonProperty("plot")]
        public string plot { get; set; }

        [JsonProperty("imdbID")]
        public string imdbID { get; set; }

        [JsonProperty("type")]
        public string type { get; set; }
    }
}
