using System;
using System.Threading.Tasks;
using RevojiWebApi.Models;
using RevojiWebApi.Services;

namespace RevojiWebApi
{
    /// <summary>
    /// API Adaptor for adapting to the apptweak.io api.
    /// </summary>
    public class AppTweakAPIAdaptor : ReviewableAPIAdaptor
    {
        public AppTweakAPIAdaptor()
        {
            apiUrl = "https://apptweak.io";
            apiKey = "";
        }

        public override string getThirdPartyName()
        {
            return "QXBwVHdlYWs=";
        }

        public override Task<Reviewable> GetReviewableByIDAsync(string id)
        {
            throw new NotImplementedException();
        }

        public override string getUrlForId(string id)
        {
            throw new NotImplementedException();
        }

        public override string getUrlForSearch(string searchText, int pageOffset, int pageLimit)
        {
            throw new NotImplementedException();
        }

        public override Task<Reviewable[]> SearchReviewablesAsync(string searchText, int pageOffset, int pageLimit)
        {
            throw new NotImplementedException();
        }
    }
}
