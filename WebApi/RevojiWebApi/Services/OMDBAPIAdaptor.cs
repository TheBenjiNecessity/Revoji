using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RevojiWebApi.Models;

namespace RevojiWebApi.Services
{
    public class OMDBAPIAdaptor : ReviewableAPIAdaptor
    {
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
                reviewables = search.reviewables.Select(r => {
                    var reviewable = new Reviewable();
                    reviewable.Title = r.title;
                    reviewable.Type = r.type;
                    reviewable.TitleImageUrl = r.image;
                    return reviewable;
                }).ToArray();
            }

            return reviewables;
        }
    }

    public class OMDBJSONSearchResponse
    {
        [JsonProperty("search")]
        public List<OMDBJSONSearchItemResponse> reviewables { get; set; }
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

//{
//    "Title": "A Bug's Life",
//    "Year": "1998",
//    "Rated": "G",
//    "Released": "25 Nov 1998",
//    "Runtime": "95 min",
//    "Genre": "Animation, Adventure, Comedy",
//    "Director": "John Lasseter, Andrew Stanton(co-director)",
//    "Writer": "John Lasseter (original story by), Andrew Stanton (original story by), Joe Ranft (original story by), Andrew Stanton (screenplay by), Don McEnery (screenplay by), Bob Shaw (screenplay by)",
//    "Actors": "Dave Foley, Kevin Spacey, Julia Louis-Dreyfus, Hayden Panettiere",
//    "Plot": "A misfit ant, looking for \"warriors\" to save his colony from greedy grasshoppers, recruits a group of bugs that turn out to be an inept circus troupe.",
//    "Language": "English",
//    "Country": "USA",
//    "Awards": "Nominated for 1 Oscar. Another 14 wins & 20 nominations.",
//    "Poster": "https://m.media-amazon.com/images/M/MV5BNThmZGY4NzgtMTM4OC00NzNkLWEwNmEtMjdhMGY5YTc1NDE4XkEyXkFqcGdeQXVyMTQxNzMzNDI@._V1_SX300.jpg",
//    "Ratings": [{
//        "Source": "Internet Movie Database",
//        "Value": "7.2/10"
//    }, {
//        "Source": "Rotten Tomatoes",
//        "Value": "92%"
//    }, {
//        "Source": "Metacritic",
//        "Value": "77/100"
//    }],
//    "Metascore": "77",
//    "imdbRating": "7.2",
//    "imdbVotes": "239,627",
//    "imdbID": "tt0120623",
//    "Type": "movie",
//    "DVD": "20 Apr 1999",
//    "BoxOffice": "N/A",
//    "Production": "Buena Vista Pictures",
//    "Website": "http://www.abugslife.com",
//    "Response": "True"
//}


//"{\"Search\":[
    //{\"Title\":\"The Avengers\",\"Year\":\"2012\",\"imdbID\":\"tt0848228\",\"Type\":\"movie\",\"Poster\":\"https://m.media-amazon.com/images/M/MV5BNDYxNjQyMjAtNTdiOS00NGYwLWFmNTAtNThmYjU5ZGI2YTI1XkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_SX300.jpg\"},
//{\"Title\":\"The Avengers\",\"Year\":\"1998\",\"imdbID\":\"tt0118661\",\"Type\":\"movie\",\"Poster\":\"https://m.media-amazon.com/images/M/MV5BYWE1NTdjOWQtYTQ2Ny00Nzc5LWExYzMtNmRlOThmOTE2N2I4XkEyXkFqcGdeQXVyNjUwNzk3NDc@._V1_SX300.jpg\"},{\"Title\":\"The Avengers: Earth's Mightiest Heroes\",\"Year\":\"2010–2012\",\"imdbID\":\"tt1626038\",\"Type\":\"series\",\"Poster\":\"https://m.media-amazon.com/images/M/MV5BYzA4ZjVhYzctZmI0NC00ZmIxLWFmYTgtOGIxMDYxODhmMGQ2XkEyXkFqcGdeQXVyNjExODE1MDc@._V1_SX300.jpg\"},{\"Title\":\"The Avengers\",\"Year\":\"1961–1969\",\"imdbID\":\"tt0054518\",\"Type\":\"series\",\"Poster\":\"https://images-na.ssl-images-amazon.com/images/M/MV5BNjkwMzMzOTQxMF5BMl5BanBnXkFtZTcwNjQzMzIzMQ@@._V1_SX300.jpg\"},{\"Title\":\"The New Avengers\",\"Year\":\"1976–1977\",\"imdbID\":\"tt0074031\",\"Type\":\"series\",\"Poster\":\"https://images-na.ssl-images-amazon.com/images/M/MV5BMTIwNDg4NzE1N15BMl5BanBnXkFtZTcwNTIwMDYyMQ@@._V1_SX300.jpg\"},{\"Title\":\"The Masked Avengers\",\"Year\":\"1981\",\"imdbID\":\"tt0082153\",\"Type\":\"movie\",\"Poster\":\"https://images-na.ssl-images-amazon.com/images/M/MV5BMTlmYjI4MDAtMThlZC00NmVlLWFiN2ItMTQzODM3ZjYyMjU1XkEyXkFqcGdeQXVyMjAyNTEwOQ@@._V1_SX300.jpg\"},{\"Title\":\"The Avengers\",\"Year\":\"1942\",\"imdbID\":\"tt0034639\",\"Type\":\"movie\",\"Poster\":\"https://images-na.ssl-images-amazon.com/images/M/MV5BMDY5N2E1N2ItMjEzZi00NTZjLWJhZjMtYTBjYTI0NWYyMDc4L2ltYWdlXkEyXkFqcGdeQXVyNzIwNTU1OTg@._V1_SX300.jpg\"},{\"Title\":\"Marvel Disk Wars: The Avengers\",\"Year\":\"2014–\",\"imdbID\":\"tt3644256\",\"Type\":\"series\",\"Poster\":\"https://images-na.ssl-images-amazon.com/images/M/MV5BNDZmYjNmYTktNDVjMi00N2I0LWI0MjEtNzEyYzYzZjU5MGEwXkEyXkFqcGdeQXVyNjExODE1MDc@._V1_SX300.jpg\"},{\"Title\":\"Captain America and the Avengers\",\"Year\":\"1991\",\"imdbID\":\"tt0421939\",\"Type\":\"game\",\"Poster\":\"http://ia.media-imdb.com/images/M/MV5BZDhiN2JlMTUtZTZjZS00ODM3LWE1YmYtYjk2MTkxZmNhNGYxXkEyXkFqcGdeQXVyNDQ2OTk4MzI@._V1_SX300.jpg\"},{\"Title\":\"The AXI: Avengers of eXtreme Illusions\",\"Year\":\"2011–\",\"imdbID\":\"tt2256925\",\"Type\":\"series\",\"Poster\":\"https://images-na.ssl-images-amazon.com/images/M/MV5BMjAwODY5NzYyM15BMl5BanBnXkFtZTgwNzY5NzkxNDE@._V1_SX300.jpg\"}],\"totalResults\":\"37\",\"Response\":\"True\"}"