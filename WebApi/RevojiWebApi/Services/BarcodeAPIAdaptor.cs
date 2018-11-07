using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RevojiWebApi.Models;

namespace RevojiWebApi.Services
{
    public class BarcodeAPIAdaptor : ReviewableAPIAdaptor
    {
        public BarcodeAPIAdaptor()
        {
            apiKey = "";
            apiUrl = "https://api.barcodelookup.com/v2/products?key=" + apiKey;
        }

        public override string getUrlForId(string id)
        {
            return apiUrl + "&barcode=" + id;
        }

        public override string getUrlForSearch(string searchText, int pageOffset, int pageLimit)
        {
            return apiUrl + "&search=" + searchText;
        }

        public override async Task<Reviewable> GetReviewableByIDAsync(string id)
        {
            Reviewable reviewable = null;
            HttpResponseMessage response = await client.GetAsync(getUrlForId(id));
            if (response.IsSuccessStatusCode)
            {
                var reviewableString = await response.Content.ReadAsStringAsync();
                BarcodeJSONResponse reviewableJSON = JsonConvert.DeserializeObject<BarcodeJSONResponse>(reviewableString);

                reviewable = new Reviewable();
                reviewable.Title = reviewableJSON.title;
                reviewable.TitleImageUrl = reviewableJSON.images[0];
            }
            return reviewable;
        }

        public override async Task<Reviewable[]> SearchReviewablesAsync(string searchText, int pageOffset, int pageLimit)
        {
            Reviewable reviewable = null;
            HttpResponseMessage response = await client.GetAsync(getUrlForSearch(searchText, pageOffset, pageLimit));
            if (response.IsSuccessStatusCode)
            {
                var reviewableString = await response.Content.ReadAsStringAsync();
                List<BarcodeJSONResponse> reviewableJSON = JsonConvert.DeserializeObject<List<BarcodeJSONResponse>>(reviewableString);
            }
            return null;
        }
    }

    public class BarcodeJSONResponse
    {
        public String barcode_number { get; set; }
        public String model { get; set; }
        public String product_name { get; set; }
        public String title { get; set; }
        public String category { get; set; }
        public String manufacturer { get; set; }
        public String brand { get; set; }
        public String label { get; set; }
        public String artist { get; set; }
        public String genre { get; set; }
        public String description { get; set; }
        public IList<string> images { get; set; }
    }
}
