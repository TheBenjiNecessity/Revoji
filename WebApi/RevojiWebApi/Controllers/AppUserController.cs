using Microsoft.AspNetCore.Mvc;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;
using RevojiWebApi.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System;
using RevojiWebApi.DBTables.Comparers;
using Microsoft.EntityFrameworkCore;

namespace RevojiWebApi.Controllers
{
    [Route("service-api/[controller]")]
    public partial class AppUserController : UserController
    {
        [Authorize]//what about one user being able to access another users stuff? claims?
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(ApiUser);
        }

        [Authorize]//what about one user being able to access another users stuff? claims?
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using (var context = new RevojiDataContext())
            {
                DBAppUser dbAppUser = context.Get<DBAppUser>(id);
                if (dbAppUser == null)
                {
                    return new NotFoundResult();
                }
                return Ok(new AppUserDetail(dbAppUser));
            }
        }

        [Authorize]
        [HttpGet("handle/{handle}")]
        public IActionResult Get(string handle)
        {
            using (var context = new RevojiDataContext())
            {
                DBAppUser dbAppUser = context.AppUsers.FirstOrDefault(user => user.Handle == handle);
                if (dbAppUser == null)
                {
                    return new NotFoundResult();
                }
                return Ok(new AppUserDetail(dbAppUser));
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody]AppUserCreateModel userCreateModel)
        {
            using (var context = new RevojiDataContext())
            {
                var appUser = userCreateModel.User;

                if (context.AppUsers.Any(user => user.Handle == appUser.Handle))
                {
                    return BadRequest("duplicate_user_handle");
                }

                if (context.AppUsers.Any(user => user.Email == appUser.Email))
                {
                    return BadRequest("duplicate_user_email");
                }

                if (string.IsNullOrEmpty(userCreateModel.Password))// TODO: handle things like #chars, capital/lower case, symbols?
                {
                    return BadRequest("password_not_set");
                }

                DBAppUser dbAppUser = new DBAppUser();
                appUser.UpdateDB(dbAppUser);

                dbAppUser.SetPassword(userCreateModel.Password);

                context.Add(dbAppUser);
                context.Save();

                return Ok(new AppUserDetail(dbAppUser));
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]AppUserDetail appUser)
        {
            using (var context = new RevojiDataContext())
            {
                DBAppUser dbAppUser = context.Get<DBAppUser>(id);
                if (dbAppUser == null)
                {
                    return new NotFoundResult();
                }

                appUser.UpdateDB(dbAppUser);
                context.Save();

                return Ok(new AppUserDetail(dbAppUser));
            }
        }

        [Authorize]
        [HttpDelete("")]
        public IActionResult Delete()
        {
            using (var context = new RevojiDataContext())
            {
                DBAppUser dbAppUser = context.Get<DBAppUser>(ApiUser.ID);
                if (dbAppUser == null)
                {
                    return new NotFoundResult();
                }

                context.Remove(dbAppUser);
                context.Save();

                return Ok();
            }
        }

        /// <summary>
        /// Performs a search of the users database with the given text and paging
        /// </summary>
        /// <returns>A list of users that fit the search criteria (paging added)</returns>
        /// <param name="text">the search text (usually from a textbox)</param>
        /// <param name="pageStart">page start</param>
        /// <param name="pageLimit">page limit</param>
        [Authorize]
        [HttpGet("search/{text}")]
        public IActionResult Search(string text, int pageStart = 0, int pageLimit = 20)
        {
            using (var context = new RevojiDataContext())
            {
                // Filter out users whose first name, last name, or handle don't contain
                // the search text. Also filter out the current user.
                // This also sorts on user popularity so that only the most popular users
                // show up first.
                var userPopularityComparer = new UserPopularityComparer();
                var query = context.AppUsers.Where(au => au.Id != ApiUser.ID &&
                                                   (au.FirstName.Contains(text) ||
                                                    au.LastName.Contains(text) ||
                                                    au.Handle.Contains(text)))
                                            .OrderBy(r => userPopularityComparer);

                var users = query.Skip(pageStart).Take(pageLimit).Select(au => new AppUser(au)).ToArray();

                return Ok(users);
            }
        }

        [Authorize]
        [HttpGet("is-following/{id}")]
        public IActionResult isFollowing(int id)
        {
            using (var context = new RevojiDataContext())
            {
                var user = context.AppUsers.Where(au => au.Id == id).FirstOrDefault();
                var currentUser = context.AppUsers.Where(au => au.Id == ApiUser.ID).Include(a => a.Followings).FirstOrDefault();
                var isFollowing = currentUser.Followings.Any(f => f.FollowingAppUserId == user.Id);
                return Ok(isFollowing);
            }
        }

        [Authorize]
        [HttpGet("is-blocking/{id}")]
        public IActionResult isBlocking(int id)
        {
            using (var context = new RevojiDataContext())
            {
                var user = context.AppUsers.Where(au => au.Id == id).FirstOrDefault();
                var currentUser = context.AppUsers.Where(au => au.Id == ApiUser.ID).Include(a => a.Blockings).FirstOrDefault();
                var isBlocking = currentUser.Blockings.Any(f => f.BlockedAppUserId == user.Id);
                return Ok(isBlocking);
            }
        }

        [Authorize]
        [HttpPut("changepassword")]
        public IActionResult ChangePassword(string newPassword, string oldPassword)
        {
            using (var context = new RevojiDataContext())
            {
                DBAppUser dbAppUser = context.Get<DBAppUser>(ApiUser.ID);
                if (dbAppUser == null)
                {
                    return new NotFoundResult();
                }

                if (!dbAppUser.isPasswordCorrect(oldPassword))
                {
                    return new BadRequestResult();
                }

                dbAppUser.SetPassword(newPassword);
                context.Save();

                return Ok();
            }
        }

        [Authorize]
        [HttpPut("changeemail/{email}")]
        public IActionResult ChangeEmail(string email)
        {
            using (var context = new RevojiDataContext())
            {
                DBAppUser dbAppUser = context.Get<DBAppUser>(ApiUser.ID);
                if (dbAppUser == null)
                {
                    return new NotFoundResult();
                }

                //if (email is valid)
                //{
                //    return new BadRequestResult();
                //}

                dbAppUser.Email = email;
                context.Save();

                return Ok();
            }
        }
    }

    public class AppUserCreateModel
    {
        public AppUserDetail User { get; set; }
        public string Password { get; set; }
    }
}
