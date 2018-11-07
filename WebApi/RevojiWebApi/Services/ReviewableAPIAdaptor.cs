using System;
using System.Net.Http;
using System.Threading.Tasks;
using RevojiWebApi.Models;

namespace RevojiWebApi.Services
{
    public abstract class ReviewableAPIAdaptor
    {
        public ReviewableAPIAdaptor()
        {
            client = new HttpClient();
        }

        protected string apiUrl;
        protected string apiKey;
        protected HttpClient client;

        public abstract string getUrlForId(string id);
        public abstract string getUrlForSearch(string searchText, int pageOffset, int pageLimit);

        public abstract Task<Reviewable> GetReviewableByIDAsync(string id);
        public abstract Task<Reviewable[]> SearchReviewablesAsync(string searchText, int pageOffset, int pageLimit);
    }
}
