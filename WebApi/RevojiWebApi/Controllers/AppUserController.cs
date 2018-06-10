using Microsoft.AspNetCore.Mvc;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;
using RevojiWebApi.Models;
using System.Linq;

namespace RevojiWebApi.Controllers
{
    [Route("api/[controller]")]
    public class AppUserController : Controller
    {
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
                return Ok(new AppUser(dbAppUser));
            }
        }

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
                return Ok(new AppUser(dbAppUser));
            }
        }
       

        [HttpPost]
        public IActionResult Create(AppUser appUser)
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

        [HttpPost("{id}")]
        public IActionResult Update(int id, [FromBody]AppUser appUser)
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

        //public IActionResult ChangePassword(string newPassword, string oldPassword)
        //{
            
        //}
    }
}
