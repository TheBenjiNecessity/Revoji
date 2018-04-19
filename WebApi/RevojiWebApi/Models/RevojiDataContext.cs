using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.Models
{
    public class RevojiDataContext : IDisposable
    {
        RevojiDBContext context;

        public IQueryable<AppUser> AppUsers { get { return context.AppUsers; } }
        public IQueryable<AdminUser> AdminUsers { get { return context.AdminUsers; } }
        public IQueryable<Review> Reviews { get { return context.Reviews; } }
        public IQueryable<ReviewableEvent> ReviewableEvents { get { return context.ReviewableEvents; } }
        public IQueryable<ReviewableProduct> ReviewableProducts { get { return context.ReviewableProducts; } }
        public IQueryable<ReviewableService> ReviewableServices { get { return context.ReviewableServices; } }
        public IQueryable<ReviewableBusiness> ReviewableBusinesses { get { return context.ReviewableBusinesses; } }
        public IQueryable<Following> Followings { get { return context.Followings; } }
        public IQueryable<Like> Likes { get { return context.Likes; } }


        public RevojiDataContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<RevojiDBContext>();
            optionsBuilder.UseNpgsql("User ID = rev;Password=pleasechange;Server=localhost;Port=5432;Database=rev;Integrated Security=true;Pooling=true;");
            context = new RevojiDBContext(optionsBuilder.Options);
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public TEntity Get<TEntity>(int id) where TEntity : class
        {
            return context.Set<TEntity>().Find(id);
        }
    }
}
