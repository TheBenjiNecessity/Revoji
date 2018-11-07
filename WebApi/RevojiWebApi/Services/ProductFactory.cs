using System;
using System.Threading.Tasks;
using RevojiWebApi.Models;
using RevojiWebApi.Services;

namespace RevojiWebApi
{
    public class ProductFactory : ReviewableAPIFactory
    {
        public ReviewableAPIAdaptor GetAPIAdaptor()
        {
            return new BarcodeAPIAdaptor();
        }
    }
}
