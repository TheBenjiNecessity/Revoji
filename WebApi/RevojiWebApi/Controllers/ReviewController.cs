using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.Comparers;
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
        public IActionResult Create([FromBody]JObject review)
        {
            using (var context = new RevojiDataContext())
            {
                DBReview dbReview = new DBReview(review);

                if (dbReview.DBReviewable == null)
                {
                    return BadRequest(new { ErrorMessage = "Reviewable not populated." });
                }

                // If there is no reviewable in the db that the review refers to then create one
                if (dbReview.ReviewableId == 0 &&
                    dbReview.DBReviewable.TpId != null &&
                    !context.Reviewables.Any(r => r.TpId == dbReview.DBReviewable.TpId))
                {
                    //DBReviewable dBReviewable = new DBReviewable(reviewable);
                    context.Add(dbReview.DBReviewable);
                    context.Save();

                    dbReview.ReviewableId = dbReview.DBReviewable.Id;
                }

                dbReview.DBAppUser = null;
                dbReview.DBReviewable = null;

                context.Add(dbReview);
                context.Save();

                var result = new Review(dbReview);
                return Ok(result);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]JObject review)
        {
            using (var context = new RevojiDataContext())
            {
                DBReview dbReview = context.Get<DBReview>(id);
                if (dbReview == null)
                {
                    return new NotFoundResult();
                }

                dbReview.update(review);
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
        public IActionResult ListByUser(int id, int beforeId, int afterId, int limit = 20)
        {
            using (var context = new RevojiDataContext())
            {
                var reviews = context.Reviews.Where(r => r.AppUserId == id);
                return applyReviewFilter(reviews, beforeId, afterId, limit);
            }
        }
        
        [Authorize]
        [HttpGet("reviewable/{tpId}")]
        public IActionResult ListByReviewable(string tpId, string tpName, int beforeId, int afterId, int limit = 20)
        {
            using (var context = new RevojiDataContext())
            {
                var reviews = context.Reviews.Where(r => r.DBReviewable.TpId == tpId && r.DBReviewable.TpName == tpName);
                return applyReviewFilter(reviews, beforeId, afterId, limit);
            }
        }

        [Authorize]
        [HttpGet("following/{id}")]
        public IActionResult ListByFollowings(int id, int beforeId, int afterId, int limit = 20)
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
                    return Ok(new Review[0]);
                }

                var reviews = context.Reviews.Where(r => followings.Contains(r.AppUserId));

                return applyReviewFilter(reviews, beforeId, afterId, limit);
            }
        }

        [Authorize]
        [HttpGet("list")]
        public IActionResult ListByCreated(int beforeId, int afterId, int limit = 20)
        {
            using (var context = new RevojiDataContext())
            {
                var reviews = context.Reviews.Where(r => r.AppUserId == ApiUser.ID);

                return applyReviewFilter(reviews, beforeId, afterId, limit);
            }
        }

        [Authorize]
        [HttpGet("trending")]
        public IActionResult ListTrending(int limit = 20)
        {
            using (var context = new RevojiDataContext())
            {
                var reviewComparer = new TrendingReviewComparer();
                var reviews = context.Reviews.Where(r => r.AppUserId != ApiUser.ID)
                                             .OrderBy(r => reviewComparer)
                                             .Take(limit);

                var includableReviews = reviews.Include(r => r.DBAppUser).Include(r => r.DBReviewable);

                Review[] reviewModels = includableReviews.Select(r => new Review(r)).ToArray();
                PagedResponse<Review> pagedResponse = new PagedResponse<Review>(reviewModels, new PageMetaData());
                return Ok(pagedResponse);
            }
        }

        private IActionResult applyReviewFilter(IQueryable<DBReview> reviews, int beforeId, int afterId, int limit, bool ordered = true)
        {
            var orderedReviews = reviews;

            if (ordered)
            {
                orderedReviews = orderedReviews.OrderByDescending(r => r.Id);
            }

            if (beforeId > 0)
            {
                orderedReviews = orderedReviews.Where(r => r.Id <= beforeId);
            }
            else if (afterId > 0)
            {
                orderedReviews = orderedReviews.Where(r => r.Id >= afterId);
            }

            if (limit > 0)
            {
                orderedReviews = orderedReviews.Take(limit);
            }
            
            orderedReviews = orderedReviews.Include(r => r.DBAppUser).Include(r => r.DBReviewable);

            Review[] reviewModels = orderedReviews.Select(r => new Review(r)).ToArray();
            PagedResponse<Review> pagedResponse = new PagedResponse<Review>(reviewModels, new PageMetaData());
            return Ok(pagedResponse);
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
