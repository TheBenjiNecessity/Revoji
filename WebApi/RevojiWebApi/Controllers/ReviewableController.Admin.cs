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
        [HttpPost]
        public IActionResult Create([FromBody]ReviewableDetail reviewable)
        {
            using (var context = new RevojiDataContext())
            {
                DBReviewable dbReviewable = new DBReviewable();
                reviewable.UpdateDB(dbReviewable);

                context.Add(dbReviewable);
                context.Save();

                return Ok(new Reviewable(dbReviewable));
            }
        }

        [Authorize]
        [HttpPost("{id}")]
        public IActionResult Update(int id, [FromBody]ReviewableDetail reviewable)
        {
            using (var context = new RevojiDataContext())
            {
                DBReviewable dbReviewable = context.Get<DBReviewable>(id);
                if (dbReviewable == null)
                {
                    return new NotFoundResult();
                }

                reviewable.UpdateDB(dbReviewable);
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
                DBReviewable dbReviewable = context.Get<DBReviewable>(id);
                if (dbReviewable == null)
                {
                    return new NotFoundResult();
                }

                context.Remove(dbReviewable);
                context.Save();

                return Ok();
            }
        }
    }
}
