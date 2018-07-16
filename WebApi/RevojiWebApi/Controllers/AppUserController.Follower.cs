using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;
using RevojiWebApi.Models;

namespace RevojiWebApi.Controllers
{
    public partial class AppUserController
    {
        /// <summary>
        /// Lists users that follow a given user and applies sorting and paging.
        /// </summary>
        /// <returns>The followers for a given user.</returns>
        /// <param name="id">App user identifier.</param>
        /// <param name="order">Order direction.</param>
        /// <param name="pageStart">Page start.</param>
        /// <param name="pageLimit">Page limit.</param>
        [Authorize]
        [HttpGet("followers/{id}")]
        public IActionResult ListFollowers(int id, [FromBody]ListFilter filter)
        {
            using (var context = new RevojiDataContext())
            {
                // Grab the app user from the database
                DBAppUser dbAppUser = context.Get<DBAppUser>(id);
                if (dbAppUser == null)
                {
                    return new NotFoundResult();
                }

                // Get an ordered list of followings from the given user by appUserId
                // where the 'following' is the user given and the follower is the other users
                IOrderedEnumerable<DBFollowing> orderedAppUserFollowers;
                if (filter.order == "DESC")
                {
                    orderedAppUserFollowers = dbAppUser.Followers
                                                       .OrderByDescending(f => f.Created);
                }
                else if (filter.order == "ASC")
                {
                    orderedAppUserFollowers = dbAppUser.Followers
                                                       .OrderBy(f => f.Created);
                }
                else
                {
                    return BadRequest("Bad order direction parameter given. Must be either DESC or ASC.");
                }
                
                // Get a list of followings offset by pageStart and limited by pageLimit
                IEnumerable<DBFollowing> pagedFollowers = orderedAppUserFollowers.Skip(filter.pageStart)
                                                                                 .Take(filter.pageLimit);

                // Map this list of followings to a list of AppUser models
                IEnumerable<AppUser> appUserFollowers = pagedFollowers.Select(f => context.Get<DBAppUser>(f.FollowerAppUserId))
                                                                      .Select(a => new AppUser(a));
                                                                      
                return Ok(appUserFollowers);
            }
        }

        /// <summary>
        /// Lists users that are being followed by a given user and applies sorting and paging.
        /// </summary>
        /// <returns>The followings for a given user.</returns>
        /// <param name="id">App user identifier.</param>
        /// <param name="order">Order direction.</param>
        /// <param name="pageStart">Page start.</param>
        /// <param name="pageLimit">Page limit.</param>
        [Authorize]
        [HttpGet("followings/{id}")]
        public IActionResult ListFollowings(int id, [FromBody]ListFilter filter)
        {
            using (var context = new RevojiDataContext())
            {
                // Grab the app user from the database
                DBAppUser dbAppUser = context.Get<DBAppUser>(id);
                if (dbAppUser == null)
                {
                    return new NotFoundResult();
                }

                // Get an ordered list of followings from the given user by appUserId
                // where the 'follower' is the user given and the following is the other users
                IOrderedEnumerable<DBFollowing> orderedAppUserFollowings;
                if (filter.order == "DESC")
                {
                    orderedAppUserFollowings = dbAppUser.Followings
                                                        .OrderByDescending(f => f.Created);
                }
                else if (filter.order == "ASC")
                {
                    orderedAppUserFollowings = dbAppUser.Followings
                                                        .OrderBy(f => f.Created);
                }
                else
                {
                    return BadRequest("Bad order direction parameter given. Must be either DESC or ASC.");
                }

                // Get a list of followings offset by pageStart and limited by pageLimit
                IEnumerable<DBFollowing> pagedFollowings = orderedAppUserFollowings.Skip(filter.pageStart)
                                                                                   .Take(filter.pageLimit);

                // Map this list of followings to a list of AppUser models
                IEnumerable<AppUser> appUserFollowings = pagedFollowings.Select(f => context.Get<DBAppUser>(f.FollowingAppUserId))
                                                                        .Select(a => new AppUser(a));

                return Ok(appUserFollowings);
            }
        }

        [Authorize]
        [HttpPost("follower")]
        public IActionResult AddFollowing([FromBody]AppUserFollowing following)
        {
            using (var context = new RevojiDataContext())
            {
                DBFollowing dbFollowing = new DBFollowing();
                following.UpdateDB(dbFollowing);
                following.Created = DateTime.Now;

                context.Add(dbFollowing);
                context.Save();

                return Ok(new AppUserFollowing(dbFollowing));
            }
        }

        [Authorize]
        [HttpDelete("follower/{id}")]
        public IActionResult RemoveFollowing(int id)
        {        
            using (var context = new RevojiDataContext())
            {
                DBFollowing dbFollowing = context.Get<DBFollowing>(id);
                if (dbFollowing == null)
                {
                    return new NotFoundResult();
                }

                context.Remove(dbFollowing);
                context.Save();

                return Ok();
            }
        }
    }
}
