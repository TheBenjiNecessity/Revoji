using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;
using RevojiWebApi.Models;

namespace RevojiWebApi.Controllers
{
    public partial class AppUserController
    {
        [Authorize]
        [HttpPost("block/{blockedId}")]
        public IActionResult BlockUser(int blockedId)
        {
            using (var context = new RevojiDataContext())
            {
                DBBlocking dBBlocking = new DBBlocking();
                dBBlocking.BlockedAppUserId = blockedId;
                dBBlocking.BlockerAppUserId = ApiUser.ID;

                context.Add(dBBlocking);
                context.Save();

                return Ok(new AppUserBlocking(dBBlocking));
            }
        }

        [Authorize]
        [HttpDelete("block/{blockedId}")]
        public IActionResult UnblockUser(int blockedId)
        {

            using (var context = new RevojiDataContext())
            {
                var dBBlocking = context.Blockings
                                         .Where(f => f.BlockerAppUserId == ApiUser.ID &&
                                                     f.BlockedAppUserId == blockedId)
                                         .FirstOrDefault();

                if (dBBlocking == null)
                {
                    return new NotFoundResult();
                }

                context.Remove(dBBlocking);
                context.Save();

                return Ok(new AppUserBlocking(dBBlocking));
            }
        }

        [Authorize]
        [HttpGet("block/{blockedId}")]
        public IActionResult getFollowing(int blockedId) //this should be get following
        {
            using (var context = new RevojiDataContext())
            {
                var dBBlocking = context.Blockings
                                         .Where(f => f.BlockerAppUserId == ApiUser.ID &&
                                                     f.BlockedAppUserId == blockedId)
                                         .FirstOrDefault();

                if (dBBlocking == null)
                {
                    return new NotFoundResult();
                }

                return Ok(new AppUserBlocking(dBBlocking));
            }
        }

        [Authorize]
        [HttpGet("block")]
        public IActionResult ListBlockedUsers(string order = "DESC", int pageStart = 0, int pageLimit = 20)
        {
            using (var context = new RevojiDataContext())
            {
                var blockings = context.Blockings
                                        .Where(f => f.BlockerAppUserId == ApiUser.ID)
                                        .Include(f => f.Blocked);

                if (blockings.Count() == 0)
                {
                    return new NotFoundResult();
                }

                // Get an ordered list of followings from the given user by appUserId
                // where the 'following' is the user given and the follower is the other users
                IOrderedQueryable<DBBlocking> orderedAppUserBlockings;
                if (order == "DESC")
                {
                    orderedAppUserBlockings = blockings.OrderByDescending(b => b.Blocked.Handle);
                }
                else if (order == "ASC")
                {
                    orderedAppUserBlockings = blockings.OrderBy(b => b.Blocked.Handle);
                }
                else
                {
                    return BadRequest("Bad order direction parameter given. Must be either DESC or ASC.");
                }

                // Get a list of followings offset by pageStart and limited by pageLimit
                IQueryable<DBBlocking> pagedBlockings = orderedAppUserBlockings.Skip(pageStart)
                                                                                .Take(pageLimit);

                // Map this list of followings to a list of AppUser models
                IEnumerable<AppUser> appUserBlockings = pagedBlockings.Select(b => b.Blocked)
                                                                      .Select(a => new AppUser(a));

                return Ok(appUserBlockings.ToArray());
            }
        }
    }
}
