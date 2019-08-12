using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;

namespace RevojiWebApi.Controllers
{
    public partial class ReviewController
    {
        [Authorize]
        [HttpPost("reply")]
        public IActionResult CreateReply([FromBody]Reply reply)
        {
            using (var context = new RevojiDataContext())
            {
                DBReply dBReply = new DBReply();
                reply.UpdateDB(dBReply);

                dBReply.DBAppUser = null;
                dBReply.DBReview = null;

                context.Add(dBReply);
                context.Save();

                return Ok();
            }
        }

        [Authorize]
        [HttpDelete("reply/{id}")]
        public IActionResult DeleteReply(int id, int appUserId)
        {
            using (var context = new RevojiDataContext())
            {
                DBReply dBReply = context.Replies
                                       .Where(l => l.ReviewId == id &&
                                              l.AppUserId == appUserId)
                                       .FirstOrDefault();
                if (dBReply == null)
                {
                    return new NotFoundResult();
                }

                context.Remove(dBReply);
                context.Save();

                return Ok();
            }
        }

        [Authorize]
        [HttpGet("reply/can-reply/{id}")]
        public IActionResult CanReply(int id, int reviewableId)
        {
            using (var context = new RevojiDataContext())
            {
                var review = context.Reviews.Where(r => r.AppUserId == id && r.ReviewableId == reviewableId).Include(r => r.DBReplies).FirstOrDefault();

                return Ok(review == null || (!review.DBReplies.Any(r => r.AppUserId == ApiUser.ID) && review.AppUserId != ApiUser.ID));
            }
        }

        [Authorize]
        [HttpGet("reply/{id}")]
        public IActionResult ListByReview(int id,
                                          string order = "DESC",
                                          int pageStart = 0,
                                          int pageLimit = 20)
        {
            using (var context = new RevojiDataContext())
            {
                var replies = context.Replies
                                     .Where(r => r.ReviewId == id)
                                     .Include(r => r.DBAppUser);

                return applyReplyFilter(replies, order, pageStart, pageLimit);
            }
        }

        [Authorize]
        [HttpGet("reply/list")]
        public IActionResult ListRepliesByCreated(string order = "DESC", int pageStart = 0, int pageLimit = 20)
        {
            using (var context = new RevojiDataContext())
            {
                var replies = context.Replies.Include(r => r.DBAppUser).Include(r => r.DBReview).Include(r => r.DBReview.DBReviewable).Include(r => r.DBReview.DBAppUser);

                return applyReplyFilter(replies, order, pageStart, pageLimit);
            }
        }

        private IActionResult applyReplyFilter(IQueryable<DBReply> replies,
                                                string order,
                                                int pageStart,
                                                int pageLimit)
        {
            if (replies.Count() == 0)
            {
                return new NotFoundResult();
            }

            IOrderedQueryable<DBReply> orderedReplies;
            if (order == "DESC")
            {
                orderedReplies = replies.OrderByDescending(r => r.Created);
            }
            else if (order == "ASC")
            {
                orderedReplies = replies.OrderBy(r => r.Created);
            }
            else
            {
                return BadRequest("Bad order direction parameter given. Must be either DESC or ASC.");
            }

            IQueryable<DBReply> pageReviews = orderedReplies.Skip(pageStart)
                                                             .Take(pageLimit);

            Reply[] replyModels = pageReviews.Select(r => new Reply(r)).ToArray();

            return Ok(replyModels);
        }
    }
}
