using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;
using RevojiWebApi.Models;
using Newtonsoft.Json;

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
                    return Ok();
                }

                var reviewable = new ReviewableDetail(dbReviewable);

                return Ok(reviewable.Content);
            }
        }

        [Authorize]
        [HttpPost("{id}/content")]
        public IActionResult SetContent(int id, [FromBody]ReviewableContent reviewableContent)
        {
            using (var context = new RevojiDataContext())
            {
                DBReviewable dbReviewable = context.Get<DBReviewable>(id);
                if (dbReviewable == null)
                {
                    return new NotFoundResult();
                }

                dbReviewable.Content = JsonConvert.SerializeObject(reviewableContent);
                context.Save();

                return Ok();
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
                    return Ok();
                }

                var reviewable = new ReviewableDetail(dbReviewable);

                return Ok(reviewable.Info);
            }
        }

        [Authorize]
        [HttpPost("{id}/info")]
        public IActionResult SetInfo(int id, [FromBody]ReviewableInfo reviewableInfo)
        {
            using (var context = new RevojiDataContext())
            {
                DBReviewable dbReviewable = context.Get<DBReviewable>(id);
                if (dbReviewable == null)
                {
                    return new NotFoundResult();
                }

                dbReviewable.Content = JsonConvert.SerializeObject(reviewableInfo);
                context.Save();

                return Ok();
            }
        }
    }
}
