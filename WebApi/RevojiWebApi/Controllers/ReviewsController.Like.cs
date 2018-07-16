using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;
using RevojiWebApi.Models;

namespace RevojiWebApi.Controllers
{
    public partial class ReviewsController
    {
        [Authorize]
        [HttpPost("like/{id}")]
        public IActionResult CreateLike([FromBody]Like like)
        {
            using (var context = new RevojiDataContext())
            {
                DBLike dBLike = new DBLike();
                like.UpdateDB(dBLike);

                context.Add(dBLike);
                context.Save();

                return Ok(new Like(dBLike));
            }
        }

        [Authorize]
        [HttpDelete("like/{id}")]
        public IActionResult DeleteLike(int id)
        {
            using (var context = new RevojiDataContext())
            {
                DBLike dBLike = context.Get<DBLike>(id);
                if (dBLike == null)
                {
                    return new NotFoundResult();
                }

                context.Remove(dBLike);
                context.Save();

                return Ok();
            }
        }
    }
}
