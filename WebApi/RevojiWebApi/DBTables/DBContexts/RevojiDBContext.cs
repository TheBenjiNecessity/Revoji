using Microsoft.EntityFrameworkCore;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.DBTables.DBContexts
{
    public class RevojiDBContext : DbContext
    {
        public RevojiDBContext(DbContextOptions<RevojiDBContext> options) : base(options) {}

        public DbSet<DBAppUser> AppUsers { get; set; }
        public DbSet<DBAdminUser> AdminUsers { get; set; }
        public DbSet<DBReview> Reviews { get; set; }
        public DbSet<DBReviewable> Reviewable { get; set; }
        public DbSet<DBLike> Likes { get; set; }
        public DbSet<DBFollowing> Followings { get; set; }
    }
}
