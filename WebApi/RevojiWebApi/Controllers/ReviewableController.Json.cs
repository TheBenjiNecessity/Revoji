using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;
using RevojiWebApi.Models;

namespace RevojiWebApi.Controllers
{
    public partial class ReviewableController
    {
        [Authorize]
        [HttpGet("{id}/content")]
        public IActionResult GetContent(int id)
        {
            using (var context = new RevojiDataContext())
            {
                DBReviewable dbReviewable = context.Get<DBReviewable>(id);
                if (dbReviewable == null)
                {
                    return new NotFoundResult();
                }

                var reviewable = new ReviewableDetail(dbReviewable);

                return Ok(reviewable.Content);
            }
        }

        [Authorize]
        [HttpPost("{id}/content")]
        public IActionResult SetContent(int id)
        {
            using (var context = new RevojiDataContext())
            {
                DBReviewable dbReviewable = context.Get<DBReviewable>(id);
                if (dbReviewable == null)
                {
                    return new NotFoundResult();
                }

                var reviewable = new ReviewableDetail(dbReviewable);

                return Ok(reviewable.Content);
            }
        }

        [Authorize]
        [HttpGet("{id}/info")]
        public IActionResult GetInfo(int id)
        {
            using (var context = new RevojiDataContext())
            {
                DBReviewable dbReviewable = context.Get<DBReviewable>(id);
                if (dbReviewable == null)
                {
                    return new NotFoundResult();
                }

                var reviewable = new ReviewableDetail(dbReviewable);

                return Ok(reviewable.Info);
            }
        }
    }
}
