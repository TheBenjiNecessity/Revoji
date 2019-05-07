using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;
using RevojiWebApi.Models;

namespace RevojiWebApi.Controllers
{
    public partial class ReviewController
    {
        [Authorize]
        [HttpGet("{id}/stats")]
        public IActionResult Stats(int id)
        {
            using (var context = new RevojiDataContext())
            {
                var likes = context.Likes.Where(l => l.ReviewId == id);

                var replyCount = context.Replies.Where(r => r.ReviewId == id).Count();
                int agreeCount = likes.Select(l => new Like(l)).Where(l => l.agreeType == "great").Count();
                int disagreeCount = likes.Select(l => new Like(l)).Where(l => l.agreeType == "bad").Count();

                var stats = new ReviewStats(replyCount, agreeCount, disagreeCount);

                return Ok(stats);
            }
        }
    }

    class ReviewStats
    {
        public int replyCount { get; set; }
        public int agreeCount { get; set; }
        public int disagreeCount { get; set; }

        public ReviewStats(int replyCount, int agreeCount, int disagreeCount)
        {
            this.replyCount = replyCount;
            this.agreeCount = agreeCount;
            this.disagreeCount = disagreeCount;
        }
    }
}
