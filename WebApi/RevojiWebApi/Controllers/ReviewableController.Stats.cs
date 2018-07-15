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
                DBReviewable dbReviewable = context.Get<DBReviewable>(id);
                if (dbReviewable == null)
                {
                    return new NotFoundResult();
                }

                return Ok(new ReviewableDetail(dbReviewable).Reviews.Length);
            }
        }

        [Authorize]
        [HttpGet("stats/emojistats/{id}")]
        public IActionResult GetEmojiStats(int id)
        {
            Dictionary<string, int> emojiCounts = new Dictionary<string, int>();

            using (var context = new RevojiDataContext())
            {
                DBReviewable dbReviewable = context.Get<DBReviewable>(id);
                if (dbReviewable == null)
                {
                    return new NotFoundResult();
                }

                ReviewableDetail reviewable = new ReviewableDetail(dbReviewable);
                var emojis = reviewable.Reviews.SelectMany(r => r.Emojis.Split(','));
                foreach (string emoji in emojis)
                {
                    if (emojiCounts.ContainsKey(emoji))
                    {
                        emojiCounts[emoji]++;
                    }
                    else 
                    {
                        emojiCounts.Add(emoji, 1);
                    }
                }
            }

            return Ok(emojiCounts);
        }

        [Authorize]//what about one user being able to access another users stuff? claims?
        [HttpGet("stats/wordstats/{id}")]
        public IActionResult GetWordStats(int id)
        {
            using (var context = new RevojiDataContext())
            {
                DBReviewable dbReviewable = context.Get<DBReviewable>(id);
                if (dbReviewable == null)
                {
                    return new NotFoundResult();
                }

                ReviewableDetail reviewable = new ReviewableDetail(dbReviewable);

                Regex rgx = new Regex("[^a-zA-Z]");//TODO: this won't work with other languages

                //var words = reviewable.Reviews.SelectMany(r => r.Comment.Replace() Split());

                // Make a list of every word from every comment in every review (delimited by whitespace)

                // set all words to lowercase and strip out punctuation (period, quote)

                // remove all words that aren't adjectives

                // group words together into a dictionary (key = word, value = count for that word)

                return Ok();
            }
        }
    }
}
