using System;
using RevojiWebApi.Services;

namespace RevojiWebApi
{
    public class AppFactory : ReviewableAPIFactory
    {
        public ReviewableAPIAdaptor GetAPIAdaptor()
        {
            return new OMDBAPIAdaptor();
        }
    }
}
