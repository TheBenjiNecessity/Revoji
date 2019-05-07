using Microsoft.AspNetCore.Mvc;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;
using RevojiWebApi.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System;

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
        public IActionResult Create([FromBody]AppUserDetail appUser)
        {
            using (var context = new RevojiDataContext()) 
            {
                DBAppUser dbAppUser = new DBAppUser();
                appUser.UpdateDB(dbAppUser);

                context.Add(dbAppUser);
                context.Save();

                return Ok(new AppUser(dbAppUser));
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

                return Ok(new AppUser(dbAppUser));
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (var context = new RevojiDataContext())
            {
                DBAppUser dbAppUser = context.Get<DBAppUser>(id);
                if (dbAppUser == null)
                {
                    return new NotFoundResult();
                }

                context.Remove(dbAppUser);
                context.Save();

                return Ok();
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
                // the search text.
                var query = context.AppUsers.Where(au => au.Id != ApiUser.ID &&
                                                   (au.FirstName.Contains(text) ||
                                                    au.LastName.Contains(text) ||
                                                    au.Handle.Contains(text)));

                var blockings = context.Blockings.Where(b => b.BlockerAppUserId == ApiUser.ID).Select(b => b.Blocker);

                query = query.Except(blockings);

                var users = query.Skip(pageStart).Take(pageLimit).Select(au => new AppUser(au)).ToArray();

                // Filter out results based on popularity/is a following of the apiuser/past searches

                return Ok(users);
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
}
