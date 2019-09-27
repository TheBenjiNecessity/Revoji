using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;
using RevojiWebApi.Models;
using RevojiWebApi.Services;

namespace RevojiWebApi.Controllers
{
    public partial class AppUserController
    {
        [Authorize]
        [HttpPost("{id}/profilepicture")]
        public IActionResult uploadProfilePicture(int id, [FromForm]IFormFile file)
        {
            using (var context = new RevojiDataContext())
            {
                DBAppUser dbAppUser = context.Get<DBAppUser>(id);
                if (dbAppUser == null)
                {
                    return new NotFoundResult();
                }

                var result = AWSFileUploader.UploadObject(file, "profile_pictures").Result;

                var content = JsonConvert.DeserializeObject<AppUserContent>(dbAppUser.Content);
                content.Avatar = result.Url;

                dbAppUser.Content = JsonConvert.SerializeObject(content);
                context.Save();

                return Ok(result);
            }
        }
    }
}
