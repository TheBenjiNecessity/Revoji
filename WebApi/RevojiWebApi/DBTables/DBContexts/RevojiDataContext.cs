using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.DBTables.DBContexts
{
    public class RevojiDataContext : IDisposable
    {
        protected RevojiDBContext context;

        public IQueryable<DBAppUser> AppUsers { get { return context.AppUsers; } }
        public IQueryable<DBAdminUser> AdminUsers { get { return context.AdminUsers; } }
        public IQueryable<DBReview> Reviews { get { return context.Reviews; } }
        public IQueryable<DBReviewable> Reviewables { get { return context.Reviewables; } }
        public IQueryable<DBFollowing> Followings { get { return context.Followings; } }
        public IQueryable<DBLike> Likes { get { return context.Likes; } }
        public IQueryable<DBReply> Replies { get { return context.Replies; } }


        public RevojiDataContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<RevojiDBContext>();
            optionsBuilder.UseNpgsql(AppSettings.ConnectionString);
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

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            context.Set<TEntity>().Add(entity);
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            context.Set<TEntity>().Remove(entity);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
