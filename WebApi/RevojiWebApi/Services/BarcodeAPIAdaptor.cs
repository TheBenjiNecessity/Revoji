using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RevojiWebApi.Models;

namespace RevojiWebApi.Services
{
    public class BarcodeAPIAdaptor : ReviewableAPIAdaptor
    {
        public static string TPNAME = "barcode";

        public BarcodeAPIAdaptor()
        {
            apiKey = "lnh8fwquw4yk7yaaxszoa09xhpahvs";
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
                BarcodeJSONSearchResponse reviewableJSON = JsonConvert.DeserializeObject<BarcodeJSONSearchResponse>(reviewableString);
                var product = reviewableJSON.reviewables.First();
                reviewable = getReviewable(product);
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
                BarcodeJSONSearchResponse search = JsonConvert.DeserializeObject<BarcodeJSONSearchResponse>(reviewableString);
                reviewables = search.reviewables.Select(getReviewable).ToArray();
            }

            return reviewables;
        }

        private Reviewable getReviewable(BarcodeJSONResponse product)
        {
            var reviewable = new Reviewable();
            reviewable.Title = product.product_name;
            reviewable.TitleImageUrl = product.images.First();
            reviewable.TpId = product.barcode_number;
            reviewable.TpName = TPNAME;
            reviewable.Type = product.category.Split('>').Last().Trim();
            reviewable.Description = product.description;
            return reviewable;
        }
    }

    public class BarcodeJSONResponse
    {
        [JsonProperty("barcode_number")]
        public String barcode_number { get; set; }

        [JsonProperty("model")]
        public String model { get; set; }

        [JsonProperty("product_name")]
        public String product_name { get; set; }

        [JsonProperty("title")]
        public String title { get; set; }

        [JsonProperty("category")]
        public String category { get; set; }

        [JsonProperty("manufacturer")]
        public String manufacturer { get; set; }

        [JsonProperty("brand")]
        public String brand { get; set; }

        [JsonProperty("label")]
        public String label { get; set; }

        [JsonProperty("artist")]
        public String artist { get; set; }

        [JsonProperty("genre")]
        public String genre { get; set; }

        [JsonProperty("description")]
        public String description { get; set; }

        [JsonProperty("images")]
        public IList<string> images { get; set; }
    }

    public class BarcodeJSONSearchResponse
    {
        [JsonProperty("products")]
        public List<BarcodeJSONResponse> reviewables { get; set; }
    }
}
