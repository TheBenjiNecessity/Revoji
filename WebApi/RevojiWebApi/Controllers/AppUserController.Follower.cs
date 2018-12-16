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
        /// <summary>
        /// Lists users that follow a given user and applies sorting and paging.
        /// </summary>
        /// <returns>The followers for a given user.</returns>
        /// <param name="id">App user identifier.</param>
        /// <param name="order">Order direction.</param>
        /// <param name="pageStart">Page start.</param>
        /// <param name="pageLimit">Page limit.</param>
        [Authorize]
        [HttpGet("{id}/followers")]
        public IActionResult ListFollowers(int id, string order = "DESC", int pageStart = 0, int pageLimit = 20)
        {
            using (var context = new RevojiDataContext())
            {
                var followers = context.Followings
                                       .Where(f => f.FollowingAppUserId == id)
                                       .Include(f => f.Follower);

                if (followers.Count() == 0)
                {
                    return new NotFoundResult();
                }

                // Get an ordered list of followings from the given user by appUserId
                // where the 'following' is the user given and the follower is the other users
                IOrderedQueryable<DBFollowing> orderedAppUserFollowers;
                if (order == "DESC")
                {
                    orderedAppUserFollowers = followers.OrderByDescending(f => f.Created);
                }
                else if (order == "ASC")
                {
                    orderedAppUserFollowers = followers.OrderBy(f => f.Created);
                }
                else
                {
                    return BadRequest("Bad order direction parameter given. Must be either DESC or ASC.");
                }

                // Get a list of followings offset by pageStart and limited by pageLimit
                IQueryable<DBFollowing> pagedFollowers = orderedAppUserFollowers.Skip(pageStart)
                                                                                 .Take(pageLimit);

                // Map this list of followings to a list of AppUser models
                IEnumerable<AppUser> appUserFollowers = pagedFollowers.Select(f => f.Follower)
                                                                      .Select(a => new AppUser(a));

                return Ok(appUserFollowers.ToArray());
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
        [HttpGet("{id}/followings")]
        public IActionResult ListFollowings(int id, string order = "DESC", int pageStart = 0, int pageLimit = 20)
        {
            using (var context = new RevojiDataContext())
            {
                var followings = context.Followings
                                        .Where(f => f.FollowerAppUserId == id)
                                        .Include(f => f.Following);



                if (followings.Count() == 0)
                {
                    return new NotFoundResult();
                }

                // Get an ordered list of followings from the given user by appUserId
                // where the 'following' is the user given and the follower is the other users
                IOrderedQueryable<DBFollowing> orderedAppUserFollowers;
                if (order == "DESC")
                {
                    orderedAppUserFollowers = followings.OrderByDescending(f => f.Created);
                }
                else if (order == "ASC")
                {
                    orderedAppUserFollowers = followings.OrderBy(f => f.Created);
                }
                else
                {
                    return BadRequest("Bad order direction parameter given. Must be either DESC or ASC.");
                }

                // Get a list of followings offset by pageStart and limited by pageLimit
                IQueryable<DBFollowing> pagedFollowers = orderedAppUserFollowers.Skip(pageStart)
                                                                                .Take(pageLimit);

                // Map this list of followings to a list of AppUser models
                IEnumerable<AppUser> appUserFollowers = pagedFollowers.Select(f => f.Following)
                                                                      .Select(a => new AppUser(a));

                return Ok(appUserFollowers.ToArray());
            }
        }

        [Authorize]
        [HttpPost("following/{followingId}")]
        public IActionResult AddFollowing(int followingId)
        {
            using (var context = new RevojiDataContext())
            {
                DBFollowing dbFollowing = new DBFollowing();
                dbFollowing.FollowingAppUserId = followingId;
                dbFollowing.FollowerAppUserId = ApiUser.ID;
                dbFollowing.Created = DateTime.Now;//TODO: 0001/01/01 ?

                context.Add(dbFollowing);
                context.Save();

                return Ok(new AppUserFollowing(dbFollowing));
            }
        }

        [Authorize]
        [HttpDelete("following/{followingId}")]//id is the id of the follower
        public IActionResult RemoveFollowing(int followingId)
        {
            using (var context = new RevojiDataContext())
            {
                var dbFollowing = context.Followings
                                         .Where(f => f.FollowerAppUserId == ApiUser.ID &&
                                                f.FollowingAppUserId == followingId)
                                         .FirstOrDefault();

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
