using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RevojiWebApi.Models;

namespace RevojiWebApi.Controllers
{
    public class UserController : Controller
    {
        public AppUser ApiUser { get; private set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var identity = User.Identity as ClaimsIdentity;
            string handle = identity.Claims.Where(cl => cl.Type == ClaimTypes.NameIdentifier)
                                    .Select(cl => cl.Value)
                                    .FirstOrDefault();
            ApiUser = AppUser.UserFromHandle(handle);

            base.OnActionExecuting(context);
        }
    }
}
