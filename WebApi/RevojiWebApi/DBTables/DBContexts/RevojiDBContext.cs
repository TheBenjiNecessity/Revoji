using Microsoft.EntityFrameworkCore;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.DBTables.DBContexts
{
    public class RevojiDBContext : DbContext
    {
        public DbSet<DBAppUser> AppUsers { get; set; }
        public DbSet<DBAdminUser> AdminUsers { get; set; }
        public DbSet<DBReview> Reviews { get; set; }
        public DbSet<DBReviewable> Reviewable { get; set; }
        public DbSet<DBLike> Likes { get; set; }
        public DbSet<DBFollowing> Followings { get; set; }
        
        public RevojiDBContext(DbContextOptions<RevojiDBContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DBFollowing>()
                        .HasKey(f => new { f.FollowerAppUserId, f.FollowingAppUserId });
            
            modelBuilder.Entity<DBFollowing>()
                        .HasOne(f => f.Follower)
                        .WithMany(a => a.Followers)
                        .HasForeignKey(f => f.FollowerAppUserId);
            
            modelBuilder.Entity<DBFollowing>()
                        .HasOne(f => f.Following)
                        .WithMany(a => a.Followings)
                        .HasForeignKey(f => f.FollowingAppUserId);
        }
    }
}
