using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;
using RevojiWebApi.Models;

namespace RevojiWebApi.Controllers
{
    [Route("service-api/[controller]")]
    public partial class ReviewsController : Controller
    {
#region CRUD
        [Authorize]//what about one user being able to access another users stuff? claims?
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using (var context = new RevojiDataContext())
            {
                DBReview dbReview = context.Get<DBReview>(id);
                if (dbReview == null)
                {
                    return new NotFoundResult();
                }

                return Ok(new Review(dbReview));
            }
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody]Review review)
        {
            using (var context = new RevojiDataContext())
            {
                DBReview dbReview = new DBReview();
                review.UpdateDB(dbReview);

                context.Add(dbReview);
                context.Save();

                return Ok(new Review(dbReview));
            }
        }

        [Authorize]
        [HttpPost("{id}")]
        public IActionResult Update(int id, [FromBody]Review review)
        {
            using (var context = new RevojiDataContext())
            {
                DBReview dbReview = context.Get<DBReview>(id);
                if (dbReview == null)
                {
                    return new NotFoundResult();
                }

                review.UpdateDB(dbReview);
                context.Save();

                return Ok();
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var context = new RevojiDataContext())
            {
                DBReview dbReview = context.Get<DBReview>(id);
                if (dbReview == null)
                {
                    return new NotFoundResult();
                }

                context.Remove(dbReview);
                context.Save();

                return Ok();
            }
        }
#endregion

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult ListByUser(int id, [FromBody]ListFilter filter)
        {
            using (var context = new RevojiDataContext())
            {
                DBAppUser dbAppUser = context.Get<DBAppUser>(id);
                if (dbAppUser == null)
                {
                    return new NotFoundResult();
                }

                AppUserDetail appUser = new AppUserDetail(dbAppUser);
                var orderedReviews = appUser.Reviews.OrderByDescending(r => r.Created).Skip(pageStart).Take(pageLimit);

                if (order == "ASC")
                {
                    orderedReviews = appUser.Reviews.OrderBy(r => r.Created).Skip(pageStart).Take(pageLimit);
                }
                else
                {
                    return BadRequest("Bad order direction parameter given. Must be either DESC or ASC.");
                }

                return Ok(orderedReviews);
            }
        }
        
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult ListByReviewable(int id, [FromBody]ListFilter filter)
        {
            using (var context = new RevojiDataContext())
            {
                DBReviewable dbReviewable = context.Get<DBReviewable>(id);
                if (dbReviewable == null)
                {
                    return new NotFoundResult();
                }

                ReviewableDetail reviewable = new ReviewableDetail(dbReviewable); //TODO: I shouldn't need to create a '...detail' object to get reviews
                var orderedReviews = reviewable.Reviews.OrderByDescending(r => r.Created).Skip(pageStart).Take(pageLimit);

                if (order == "ASC")
                {
                    orderedReviews = reviewable.Reviews.OrderBy(r => r.Created).Skip(pageStart).Take(pageLimit);
                }
                else
                {
                    return BadRequest("Bad order direction parameter given. Must be either DESC or ASC.");
                }

                return Ok(reviewable.Reviews);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult ListByFollowings(int id, [FromBody]ListFilter filter)
        {
            using (var context = new RevojiDataContext())
            {
                DBAppUser dbAppUser = context.Get<DBAppUser>(id);
                if (dbAppUser == null)
                {
                    return new NotFoundResult();
                }

                AppUserDetail appUser = new AppUserDetail(dbAppUser);
                var appUserFollowings = appUser.Followings
                                               .Select(f => context.Get<DBAppUser>(f.FollowingId))
                                               .Select(a => new AppUserDetail(a));
                var reviews = appUserFollowings.SelectMany(a => a.Reviews);

                var orderedReviews = reviews.OrderByDescending(r => r.Created).Skip(pageStart).Take(pageLimit);

                if (order == "ASC")
                {
                    orderedReviews = reviews.OrderBy(r => r.Created).Skip(pageStart).Take(pageLimit);
                }
                else
                {
                    return BadRequest("Bad order direction parameter given. Must be either DESC or ASC.");
                }


                return Ok(reviews);
            }
        }

        /**
         * list (add ability to filter by (demographics/category):
         *    trending reviews (latest/highest rated/most rated/...)
         *    reviews by user interest
         */
    }
}
