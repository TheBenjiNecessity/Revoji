using Microsoft.EntityFrameworkCore;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.Models
{
    public class RevojiDBContext : DbContext
    {
        public RevojiDBContext(DbContextOptions<RevojiDBContext> options) : base(options) {}

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewableEvent> ReviewableEvents { get; set; }
        public DbSet<ReviewableProduct> ReviewableProducts { get; set; }
        public DbSet<ReviewableService> ReviewableServices { get; set; }
        public DbSet<ReviewableBusiness> ReviewableBusinesses { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Following> Followings { get; set; }
    }
}
