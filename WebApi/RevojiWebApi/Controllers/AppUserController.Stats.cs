using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;
using RevojiWebApi.Models;
using RevojiWebApi.StoredProcedures;

namespace RevojiWebApi.Controllers
{
    public partial class AppUserController
	{
		[Authorize]//what about one user being able to access another users stuff? claims?
        [HttpGet("{id}/counts")]
        public IActionResult GetCounts(int id)
        {
            using (var context = new RevojiDataContext())
            {
                DBAppUser dbAppUser = context.Get<DBAppUser>(id);
                if (dbAppUser == null)
                {
                    return new NotFoundResult();
                }

                AppUserStats stats = new AppUserStats(id);

                return Ok(stats);
            }
        }
    }
}
