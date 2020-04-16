using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;
using RevojiWebApi.Models;

namespace RevojiWebApi.Controllers
{
    public partial class AppUserController
    {
        [Authorize]
        [HttpGet("bookmark")]
        public IActionResult ListBookmarks(int beforeId, int afterId, int limit = 20    )
        {
            using (var context = new RevojiDataContext())
            {
                var query = context.Bookmarks.Where(b => b.AppUserId != ApiUser.ID);
                return applyBookmarkFilter(query, beforeId, afterId, limit);
            }
        }

        [Authorize]
        [HttpPost("bookmark")]
        public IActionResult CreateBookmark([FromBody]JObject bookmark)
        {
            using (var context = new RevojiDataContext())
            {
                DBBookmark dBBookmark = new DBBookmark(bookmark);
                dBBookmark.Created = DateTime.Now;

                context.Add(dBBookmark);
                context.Save();

                return Ok(new Bookmark(dBBookmark));
            }
        }

        private IActionResult applyBookmarkFilter(IQueryable<DBBookmark> bookmarks, int beforeId, int afterId, int limit, bool ordered = true)
        {
            var orderedBookmarks = bookmarks;

            if (ordered)
            {
                orderedBookmarks = orderedBookmarks.OrderByDescending(b => b.Id);
            }

            if (beforeId > 0)
            {
                orderedBookmarks = orderedBookmarks.Where(r => r.Id <= beforeId);
            }
            else if (afterId > 0)
            {
                orderedBookmarks = orderedBookmarks.Where(r => r.Id >= afterId);
            }

            if (limit > 0)
            {
                orderedBookmarks = orderedBookmarks.Take(limit);
            }

            orderedBookmarks = orderedBookmarks.Include(b => b.DBAppUser).Include(b => b.DBReview);

            Bookmark[] bookmarkModels = orderedBookmarks.Select(b => new Bookmark(b)).ToArray();
            PagedResponse<Bookmark> pagedResponse = new PagedResponse<Bookmark>(bookmarkModels, new PageMetaData());
            return Ok(pagedResponse);
        }
    }
}
