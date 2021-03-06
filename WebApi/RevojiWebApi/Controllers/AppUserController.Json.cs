﻿using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;
using RevojiWebApi.Models;

namespace RevojiWebApi.Controllers
{
    public partial class AppUserController // TODO: delete this, this is unnecessary
    {
        [Authorize]
        [HttpGet("{id}/content")]
        public IActionResult GetContent(int id)
        {
            using (var context = new RevojiDataContext())
            {
                DBAppUser dbAppUser = context.Get<DBAppUser>(id);
                if (dbAppUser == null || string.IsNullOrEmpty(dbAppUser.Content) || dbAppUser.Content == "{}")
                {
                    return Ok();
                }

                return Ok(JsonConvert.DeserializeObject<AppUserContent>(dbAppUser.Content));
            }
        }

        [Authorize]
        [HttpGet("{id}/settings")]
        public IActionResult GetSettings(int id)
        {
            using (var context = new RevojiDataContext())
            {
                DBAppUser dbAppUser = context.Get<DBAppUser>(id);
                if (dbAppUser == null || string.IsNullOrEmpty(dbAppUser.Settings) || dbAppUser.Content == "{}")
                {
                    return Ok();
                }

                return Ok(JsonConvert.DeserializeObject<AppUserSettings>(dbAppUser.Settings));
            }


        }

        [Authorize]
        [HttpGet("{id}/preferences")]
        public IActionResult GetPreferences(int id)
        {
            using (var context = new RevojiDataContext())
            {
                DBAppUser dbAppUser = context.Get<DBAppUser>(id);
                if (dbAppUser == null || string.IsNullOrEmpty(dbAppUser.Preferences) || dbAppUser.Content == "{}")
                {
                    return Ok();
                }

                return Ok(JsonConvert.DeserializeObject<AppUserPreferences>(dbAppUser.Preferences));
            }
        }

        [Authorize]
        [HttpPost("{id}/content")]
        public IActionResult SetContent(int id, [FromBody]JObject appUserContent)
        {
            using (var context = new RevojiDataContext())
            {
                DBAppUser dbAppUser = context.Get<DBAppUser>(id);
                if (dbAppUser == null)
                {
                    return new NotFoundResult();
                }

                dbAppUser.Content = JsonConvert.SerializeObject(appUserContent);
                context.Save();

                return Ok();
            }
        }

        [Authorize]
        [HttpPost("{id}/settings")]
        public IActionResult SetSettings(int id, [FromBody]JObject appUserSettings)
        {
            using (var context = new RevojiDataContext())
            {
                DBAppUser dbAppUser = context.Get<DBAppUser>(id);
                if (dbAppUser == null)
                {
                    return new NotFoundResult();
                }

                dbAppUser.Settings = JsonConvert.SerializeObject(appUserSettings);
                context.Save();

                return Ok();
            }
        }

        [Authorize]
        [HttpPost("{id}/preferences")]
        public IActionResult SetPreferences(int id, [FromBody]JObject appUserPreferences)
        {
            using (var context = new RevojiDataContext())
            {
                DBAppUser dbAppUser = context.Get<DBAppUser>(id);
                if (dbAppUser == null)
                {
                    return new NotFoundResult();
                }

                dbAppUser.Preferences = JsonConvert.SerializeObject(appUserPreferences);
                context.Save();

                return Ok();
            }
        }
    }
}
