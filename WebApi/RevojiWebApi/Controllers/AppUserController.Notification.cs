using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using RevojiWebApi.DBTables;
using RevojiWebApi.DBTables.DBContexts;
using RevojiWebApi.Models;

namespace RevojiWebApi.Controllers
{
    public partial class AppUserController
    {
        [Authorize]
        [HttpGet("notification")]
        public IActionResult ListNotifications(int beforeId, int afterId, int limit = 20)
        {
            using (var context = new RevojiDataContext())
            {
                var query = context.Notifications.Where(b => b.AppUserId != ApiUser.ID);
                return applyNotificationFilter(query, beforeId, afterId, limit);
            }
        }

        [Authorize]
        [HttpPost("notification")]
        public IActionResult CreateNotification([FromBody]JObject notification)
        {
            using (var context = new RevojiDataContext())
            {
                DBNotification dBNotification = new DBNotification(notification);

                if (dBNotification.AppUserId == -1)
                {
                    return BadRequest("app_user_id_required");
                }

                dBNotification.Created = DateTime.Now;

                context.Add(dBNotification);
                context.Save();

                return Ok(new Notification(dBNotification));
            }
        }

        private IActionResult applyNotificationFilter(IQueryable<DBNotification> notifications, int beforeId, int afterId, int limit, bool ordered = true)
        {
            var orderedNotifications = notifications;

            if (ordered)
            {
                orderedNotifications = orderedNotifications.OrderByDescending(b => b.Id);
            }

            if (beforeId > 0)
            {
                orderedNotifications = orderedNotifications.Where(r => r.Id <= beforeId);
            }
            else if (afterId > 0)
            {
                orderedNotifications = orderedNotifications.Where(r => r.Id >= afterId);
            }

            if (limit > 0)
            {
                orderedNotifications = orderedNotifications.Take(limit);
            }

            orderedNotifications = orderedNotifications.Include(b => b.DBAppUser);

            Notification[] notificationModels = orderedNotifications.Select(b => new Notification(b)).ToArray();
            PagedResponse<Notification> pagedResponse = new PagedResponse<Notification>(notificationModels, new PageMetaData());
            return Ok(pagedResponse);
        }
    }
}
