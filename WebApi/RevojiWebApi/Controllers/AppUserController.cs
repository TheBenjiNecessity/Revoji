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
    public partial class AppUserController : Controller
    {
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
       
        [Authorize]
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
        [HttpPost("{id}")]
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

                return Ok();
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
        [HttpPost("changepassword/{id}")]
        public IActionResult ChangePassword(int id, string newPassword, string oldPassword)
        {
            using (var context = new RevojiDataContext())
            {
                DBAppUser dbAppUser = context.Get<DBAppUser>(id);
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
    }
}
