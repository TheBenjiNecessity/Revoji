﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;
using RevojiWebApi.Models;
using RevojiWebApi.Services;

namespace RevojiWebApi.Controllers
{
    //enum AdaptorType

    [Route("service-api/[controller]")]
    public partial class ReviewableController : UserController
    {
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Get(string id, string type)
        {
            ReviewableAPIFactory reviewableAPIFactory;
            if (type.Equals("media") || type.Equals(OMDBAPIAdaptor.TPNAME))//TODO this isn't right
            {
                reviewableAPIFactory = new MediaFactory();
            }
            else if (type.Equals("products"))
            {
                reviewableAPIFactory = new ProductFactory();
            }
            else
            {
                reviewableAPIFactory = new MediaFactory();
            }

            return Ok(reviewableAPIFactory.GetAPIAdaptor().GetReviewableByIDAsync(id).Result);
        }

        [Authorize]
        [HttpGet("search/{text}")]
        public IActionResult Search(string text, string type, int pageStart = 0, int pageLimit = 20)
        {
            ReviewableAPIFactory reviewableAPIFactory;
            if (type.Equals("media"))
            {
                reviewableAPIFactory = new MediaFactory();
            }
            else if (type.Equals("products"))
            {
                reviewableAPIFactory = new ProductFactory();
            }
            else
            {
                reviewableAPIFactory = new MediaFactory();
            }

            return Ok(reviewableAPIFactory.GetAPIAdaptor().SearchReviewablesAsync(text, pageStart, pageLimit).Result);
        }

        [Authorize]
        [HttpGet("type/{type}")]
        public IActionResult ListByType(string type, string order = "DESC", int pageStart = 0, int pageLimit = 20)
        {
            //what would you order?
            // title (alphabetically), company name (alphabetically), 

            using (var context = new RevojiDataContext())
            {
                var reviewables = context.Reviewables.Where(r => r.Type == type);

                IQueryable<DBReviewable> orderedReviews;
                if (order == "DESC")
                {
                    orderedReviews = reviewables.OrderByDescending(r => r.Title)
                                                .Skip(pageStart)
                                                .Take(pageLimit);
                }
                else if (order == "ASC")
                {
                    orderedReviews = reviewables.OrderBy(r => r.Title)
                                                .Skip(pageStart)
                                                .Take(pageLimit);
                }
                else
                {
                    return BadRequest("Bad order direction parameter given. Must be either DESC or ASC.");
                }

                return Ok(orderedReviews.Select(r => new Reviewable(r)));
            }
        }

        [Authorize]
        [HttpGet("trending")]
        public IActionResult ListTrending(string order = "DESC", int pageStart = 0, int pageLimit = 20)
        {
            //TODO
            return Ok();
        }

        [Authorize]
        [HttpGet("can-review/{tpId}")]
        public IActionResult CanReview(string tpId, string tpName)
        {
            using (var context = new RevojiDataContext())
            {
                var reviewable = context.Reviewables.Where(r => r.TpId == tpId && r.TpName == tpName).Include(r => r.DBReviews).FirstOrDefault();

                if (reviewable == null || !reviewable.DBReviews.Any(r => r.AppUserId == ApiUser.ID))
                {
                    return Ok();
                } 
                else 
                {
                    return BadRequest();
                }
            }
        }
    }
}
