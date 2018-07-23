using Microsoft.EntityFrameworkCore;
using RevojiWebApi.DBTables;

namespace RevojiWebApi.DBTables.DBContexts
{
    public class RevojiDBContext : DbContext
    {
        public DbSet<DBAppUser> AppUsers { get; set; }
        public DbSet<DBAdminUser> AdminUsers { get; set; }
        public DbSet<DBReview> Reviews { get; set; }
        public DbSet<DBReviewable> Reviewables { get; set; }
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

            modelBuilder.Entity<DBLike>()
                        .HasKey(l => new { l.ReviewId, l.AppUserId });

            modelBuilder.Entity<DBLike>()
                        .HasOne(l => l.DBReview)
                        .WithMany(r => r.DBLikes)
                        .HasForeignKey(l => l.ReviewId);

            modelBuilder.Entity<DBLike>()
                        .HasOne(l => l.DBAppUser)
                        .WithMany(a => a.DBLikes)
                        .HasForeignKey(l => l.AppUserId);
        }
    }
}
