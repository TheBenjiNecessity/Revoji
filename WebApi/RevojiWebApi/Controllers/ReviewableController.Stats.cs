using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;
using RevojiWebApi.Models;

namespace RevojiWebApi.Controllers
{
    public partial class ReviewableController
    {
        [Authorize]//what about one user being able to access another users stuff? claims?
        [HttpGet("stats/reviewcount/{id}")]
        public IActionResult GetReviewCount(int id)
        {
            using (var context = new RevojiDataContext())
            {
                // Could this be gotten from stored procedure?
                int count = context.Reviews
                                   .Where(r => r.ReviewableId == id)
                                   .Count();

                return Ok(count);
            }
        }

        [Authorize]
        [HttpGet("{id}/stats/icon")]
        public IActionResult GetEmojiStats(int id)
        {
            Dictionary<string, int> emojiCounts = new Dictionary<string, int>();

            using (var context = new RevojiDataContext())
            {
                var reviews = context.Reviews.Where(r => r.ReviewableId == id);

                if (reviews.Count() == 0)
                {
                    return new NotFoundResult();
                }

                string emojis = new string(reviews.SelectMany(r => r.Emojis)
                                                  .ToArray());

                emojiCounts = emojis.Split(",")
                                    .GroupBy(e => e, StringComparer.OrdinalIgnoreCase)
                                    .ToDictionary(group => group.Key, group => group.Count());

                return Ok(emojiCounts);
            }
        }

        [Authorize]//what about one user being able to access another users stuff? claims?
        [HttpGet("{id}/stats/word")]
        public IActionResult GetWordStats(int id)
        {
            throw new NotImplementedException();
        }
    }
}
