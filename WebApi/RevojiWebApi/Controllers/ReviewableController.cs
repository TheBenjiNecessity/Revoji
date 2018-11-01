using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;
using RevojiWebApi.Models;

namespace RevojiWebApi.Controllers
{
    [Route("service-api/[controller]")]
    public partial class ReviewableController : UserController
    {
        [Authorize]//what about one user being able to access another users stuff? claims?
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using (var context = new RevojiDataContext())
            {
                DBReviewable dbReviewable = context.Get<DBReviewable>(id);
                if (dbReviewable == null)
                {
                    return new NotFoundResult();
                }
                return Ok(new ReviewableDetail(dbReviewable));
            }
        }

        [Authorize]
        [HttpGet("search")]
        public IActionResult Search(string text, int pageStart = 0, int pageLimit = 20)
        {
            using (var context = new RevojiDataContext())
            {
                var query = context.Reviewables.Where(r => r.Title.Contains(text));
                var reviewables = query.Skip(pageStart).Take(pageLimit).Select(r => new Reviewable(r)).ToArray();

                // Filter out results based on popularity/past searches

                return Ok(reviewables);
            }
        }

        [Authorize]
        [HttpGet("list/type/{type}")]
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
    }
}
