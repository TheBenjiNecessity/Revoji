using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RevojiWebApi.DBTables;
using RevojiWebApi.Models;

namespace RevojiWebApi.Controllers
{
    [Route("api/[controller]")]
    public class ReviewsController : Controller
    {
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "1";
        }
    }
}
