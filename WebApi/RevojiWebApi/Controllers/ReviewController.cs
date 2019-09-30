﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;
using RevojiWebApi.Models;

namespace RevojiWebApi.Controllers
{
    [Route("service-api/[controller]")]
    public partial class ReviewController : UserController
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
                if (review.ReviewableID == null && review.Reviewable == null)
                {
                    return BadRequest(new { ErrorMessage = "Reviewable not populated." });
                }

                DBReview dbReview = new DBReview();
                review.UpdateDB(dbReview);

                // If there is no reviewable in the db that the review refers to then create one
                if (review.ReviewableID == null && !context.Reviewables.Any(r => r.TpId == review.Reviewable.TpId))
                {
                    DBReviewable dBReviewable = new DBReviewable();
                    review.Reviewable.UpdateDB(dBReviewable);
                    context.Add(dBReviewable);
                    context.Save();

                    dbReview.ReviewableId = dBReviewable.Id;
                }

                dbReview.DBAppUser = null;
                dbReview.DBReviewable = null;

                context.Add(dbReview);
                context.Save();

                var result = new Review(dbReview);
                result.AppUser = review.AppUser;
                result.Reviewable = review.Reviewable;

                return Ok(result);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
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
        [HttpGet("user/{id}")]
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
        [HttpGet("reviewable/{tpId}")]
        public IActionResult ListByReviewable(string tpId, string tpName,
                                              string order = "DESC",
                                              int pageStart = 0,
                                              int pageLimit = 20)
        {
            using (var context = new RevojiDataContext())
            {
                var reviews = context.Reviews
                                     .Where(r => r.DBReviewable.TpId == tpId &&
                                            r.DBReviewable.TpName == tpName)
                                     .Include(r => r.DBAppUser)
                                     .Include(r => r.DBReviewable);

                return applyReviewFilter(reviews, order, pageStart, pageLimit);
            }
        }

        [Authorize]
        [HttpGet("following/{id}")]
        public IActionResult ListByFollowings(int id,
                                              string order = "DESC",
                                              int pageStart = 0,
                                              int pageLimit = 20)
        {
            using (var context = new RevojiDataContext())
            {
                var appUser = context.AppUsers
                                     .Where(a => a.Id == id)
                                     .Include(a => a.Followers)
                                     .FirstOrDefault();
                var followings = appUser.Followers
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

        [Authorize]
        [HttpGet("list")]
        public IActionResult ListByCreated(string order = "DESC", int pageStart = 0, int pageLimit = 20)
        {
            using (var context = new RevojiDataContext())
            {
                var reviews = context.Reviews
                                     .Where(r => r.AppUserId == ApiUser.ID)
                                     .Include(r => r.DBAppUser)
                                     .Include(r => r.DBReviewable);

                return applyReviewFilter(reviews, order, pageStart, pageLimit);
            }
        }

        [Authorize]
        [HttpGet("trending")]
        public IActionResult ListTrending(int pageStart = 0, int pageLimit = 20)
        {
            using (var context = new RevojiDataContext())
            {
                var reviews = filterReviewsForUsers(context).OrderBy(r => r);

                return applyReviewFilter(reviews, "DESC", pageStart, pageLimit);
            }
        }

        private IActionResult applyReviewFilter(IQueryable<DBReview> reviews,
                                                string order,
                                                int pageStart,
                                                int pageLimit)
        {
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

        private IQueryable<DBReview> filterReviewsForUsers(RevojiDataContext context)
        {
            string dbApiUserPreferences = context.AppUsers
                                                 .Where(au => au.Id == ApiUser.ID)
                                                 .FirstOrDefault()
                                                 .Preferences;
            AppUserPreferences ApiUserPreferences = null; 

            if (!string.IsNullOrEmpty(dbApiUserPreferences)) {
                ApiUserPreferences = JsonConvert.DeserializeObject<AppUserPreferences>(dbApiUserPreferences);
            }

            var users = from user in context.AppUsers
                        let p = !string.IsNullOrEmpty(user.Preferences) ? JsonConvert.DeserializeObject<AppUserPreferences>(user.Preferences) : null
                        where p != null && ApiUser.ID != user.Id &&
                            ApiUser.Age >= p.AgeRangeMin &&
                            ApiUser.Age <= p.AgeRangeMax &&
                            isEqualWithTolerance(ApiUserPreferences.Personality, p.Personality) &&
                            isEqualWithTolerance(ApiUserPreferences.PoliticalAffiliation, p.PoliticalAffiliation) &&
                            isEqualWithTolerance(ApiUserPreferences.PoliticalOpinion, p.PoliticalOpinion) &&
                            isEqualWithTolerance(ApiUserPreferences.Religiosity, p.Religiosity) &&
                            ApiUserPreferences.Location.Intersect(p.Location).Any()
                        select user.Id;

            var reviews = context.Reviews
                                 .Where(r => users.Contains(r.AppUserId))
                                 .Include(r => r.DBAppUser)
                                 .Include(r => r.DBReviewable);

            return reviews;
        }

        private bool isEqualWithTolerance(float valueWithOffset, float target)
        {
            float tolerance = 10;
            float lowerBound = valueWithOffset - tolerance;
            float upperBound = valueWithOffset + tolerance;

            return lowerBound < target && upperBound > target;
        }
    }
}
