using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;
using RevojiWebApi.Models;

namespace RevojiWebApi.Controllers
{
    public partial class ReviewController
    {
        [Authorize]
        [HttpPost("like")]
        public IActionResult CreateLike([FromBody]JObject like)
        {
            using (var context = new RevojiDataContext())
            {
                DBLike dBLike = new DBLike(like);

                context.Add(dBLike);
                context.Save();

                return Ok(new Like(dBLike));
            }
        }

        [Authorize]
        [HttpDelete("like/{id}")]
        public IActionResult DeleteLike(int id, int appUserId)
        {
            using (var context = new RevojiDataContext())
            {
                DBLike dBLike = context.Likes
                                       .Where(l => l.ReviewId == id &&
                                              l.AppUserId == appUserId)
                                       .FirstOrDefault();
                if (dBLike == null)
                {
                    return new NotFoundResult();
                }

                context.Remove(dBLike);
                context.Save();

                return Ok();
            }
        }

        [Authorize]
        [HttpGet("like/{id}")]
        public IActionResult GetLike(int id, int appUserId)
        {
            using (var context = new RevojiDataContext())
            {
                DBLike dBLike = context.Likes
                                       .Where(l => l.ReviewId == id &&
                                              l.AppUserId == appUserId)
                                       .FirstOrDefault();

                if (dBLike == null)
                {
                    return new NotFoundResult();
                }

                return Ok(new Like(dBLike));
            }
        }

        [Authorize]
        [HttpGet("like/list")]
        public IActionResult ListLikesByCreated(string order = "DESC", int pageStart = 0, int pageLimit = 20)
        {
            using (var context = new RevojiDataContext())
            {
                var likes = context.Likes.Where(l => l.AppUserId == ApiUser.ID);

                return applyLikeFilter(likes, order, pageStart, pageLimit);
            }
        }

        private IActionResult applyLikeFilter(IQueryable<DBLike> likes,
                                                string order,
                                                int pageStart,
                                                int pageLimit)
        {
            if (likes.Count() == 0)
            {
                return Ok(new Like[0]);
            }

            IOrderedQueryable<DBLike> orderedLikes;
            if (order == "DESC")
            {
                orderedLikes = likes.OrderByDescending(l => l.Created);
            }
            else if (order == "ASC")
            {
                orderedLikes = likes.OrderBy(l => l.Created);
            }
            else
            {
                return BadRequest("Bad order direction parameter given. Must be either DESC or ASC.");
            }

            IQueryable<DBLike> pagedLikes = orderedLikes.Skip(pageStart)
                                                        .Take(pageLimit);

            Like[] likeModels = pagedLikes.Select(l => new Like(l)).ToArray();

            return Ok(likeModels);
        }
    }
}
