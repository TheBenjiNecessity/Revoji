using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;
using RevojiWebApi.Models;

namespace RevojiWebApi.Controllers
{
    [Route("service-api/[controller]")]
    public partial class ReviewsController : UserController
    {
#region CRUD
        [Authorize]//what about one user being able to access another users stuff? claims?
        [HttpGet("{id}")]
        public IActionResult Get(int id, int reviewableId)
        {
            using (var context = new RevojiDataContext())
            {
                DBReview dbReview = context.Reviews
                                           .Where(r => r.AppUserId == id && 
                                                  r.ReviewableId == reviewableId)
                                           .Include(r => r.DBReviewable)
                                           .Include(r => r.DBAppUser)
                                           .FirstOrDefault();
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
                dbReview.Created = DateTime.Now;

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

                return Ok(new Review(dbReview));
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
        [HttpGet("list/user/{id}")]
        public IActionResult ListByUser(int id,
                                        string order = "DESC", 
                                        int pageStart = 0,
                                        int pageLimit = 20)
        {
            using (var context = new RevojiDataContext())
            {
                var reviews = context.Reviews
                                     .Where(r => r.AppUserId == id)
                                     .Include(r => r.DBAppUser)
                                     .Include(r => r.DBReviewable);

                return applyReviewFilter(reviews, order, pageStart, pageLimit);
            }
        }
        
        [Authorize]
        [HttpGet("list/reviewable/{id}")]
        public IActionResult ListByReviewable(int id,
                                              string order = "DESC",
                                              int pageStart = 0,
                                              int pageLimit = 20)
        {
            using (var context = new RevojiDataContext())
            {
                var reviews = context.Reviews
                                     .Where(r => r.ReviewableId == id)
                                     .Include(r => r.DBAppUser)
                                     .Include(r => r.DBReviewable);

                return applyReviewFilter(reviews, order, pageStart, pageLimit);
            }
        }

        [Authorize]
        [HttpGet("list/followings/{id}")]
        public IActionResult ListByFollowings(int id,
                                              string order = "DESC",
                                              int pageStart = 0,
                                              int pageLimit = 20)
        {
            using (var context = new RevojiDataContext())
            {
                var appUser = context.AppUsers
                                     .Where(a => a.Id == id)
                                     .Include(a => a.Followings)
                                     .FirstOrDefault();
                var followings = appUser.Followings
                                        .Select(f => f.FollowingAppUserId)
                                        .ToList();

                if (appUser == null || followings.Count() == 0)
                {
                    return new NotFoundResult();
                }

                var reviews = context.Reviews
                                     .Where(r => followings.Contains(r.AppUserId))
                                     .Include(r => r.DBAppUser)
                                     .Include(r => r.DBReviewable);

                return applyReviewFilter(reviews, order, pageStart, pageLimit);
            }
        }

        private IActionResult applyReviewFilter(IQueryable<DBReview> reviews,
                                                string order,
                                                int pageStart,
                                                int pageLimit) {
            if (reviews.Count() == 0)
            {
                return new NotFoundResult();
            }

            IOrderedQueryable<DBReview> orderedReviews;
            if (order == "DESC")
            {
                orderedReviews = reviews.OrderByDescending(r => r.Created);
            }
            else if (order == "ASC")
            {
                orderedReviews = reviews.OrderBy(r => r.Created);
            }
            else
            {
                return BadRequest("Bad order direction parameter given. Must be either DESC or ASC.");
            }

            IQueryable<DBReview> pageReviews = orderedReviews.Skip(pageStart)
                                                             .Take(pageLimit);

            Review[] reviewModels = pageReviews.Select(r => new Review(r)).ToArray();

            return Ok(reviewModels);
        }

        /**
         * list (add ability to filter by (demographics/category):
         *    trending reviews (latest/highest rated/most rated/...)
         *    reviews by user interest
         */
    }
}
