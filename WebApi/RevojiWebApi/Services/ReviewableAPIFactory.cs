using System;
using System.Threading.Tasks;
using RevojiWebApi.Models;

namespace RevojiWebApi.Services
{
    public interface ReviewableAPIFactory
    {
        ReviewableAPIAdaptor GetAPIAdaptor();
    }
}
