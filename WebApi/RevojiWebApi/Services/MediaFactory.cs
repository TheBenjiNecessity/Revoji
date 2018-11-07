using System;
using RevojiWebApi.Services;

namespace RevojiWebApi
{
    public class MediaFactory : ReviewableAPIFactory
    {
        public ReviewableAPIAdaptor GetAPIAdaptor()
        {
            return new OMDBAPIAdaptor();
        }
    }
}
