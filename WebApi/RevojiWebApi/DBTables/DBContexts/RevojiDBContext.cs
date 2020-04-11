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
        public DbSet<DBReply> Replies { get; set; }
        public DbSet<DBBlocking> Blockings { get; set; }
        public DbSet<DBBookmark> Bookmarks { get; set; }

        public RevojiDBContext(DbContextOptions<RevojiDBContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DBFollowing>()
                        .HasKey(f => new { f.FollowerAppUserId, f.FollowingAppUserId });
            
            modelBuilder.Entity<DBFollowing>()
                        .HasOne(f => f.Follower)
                        .WithMany(a => a.Followings)
                        .HasForeignKey(f => f.FollowerAppUserId);
            
            modelBuilder.Entity<DBFollowing>()
                        .HasOne(f => f.Following)
                        .WithMany(a => a.Followers)
                        .HasForeignKey(f => f.FollowingAppUserId);

            modelBuilder.Entity<DBFollowing>()
                        .Property(f => f.Created)
                        .HasDefaultValueSql("now()");

            modelBuilder.Entity<DBLike>()
                        .HasKey(l => new { l.ReviewId, l.AppUserId });

            modelBuilder.Entity<DBLike>()
                        .HasOne(l => l.DBReview)
                        .WithMany(r => r.DBLikes)
                        .HasForeignKey(l => l.ReviewId);

            modelBuilder.Entity<DBLike>()
                        .HasOne(l => l.DBAppUser)
                        .WithMany(a => a.Likes)
                        .HasForeignKey(l => l.AppUserId);

            modelBuilder.Entity<DBLike>()
                        .Property(l => l.Created)
                        .HasDefaultValueSql("now()");

            modelBuilder.Entity<DBReply>()
                        .HasKey(l => new { l.ReviewId, l.AppUserId });

            modelBuilder.Entity<DBReply>()
                        .HasOne(l => l.DBReview)
                        .WithMany(r => r.DBReplies)
                        .HasForeignKey(l => l.ReviewId);

            modelBuilder.Entity<DBReply>()
                        .HasOne(l => l.DBAppUser)
                        .WithMany(a => a.Replies)
                        .HasForeignKey(l => l.AppUserId);

            modelBuilder.Entity<DBReply>()
                        .Property(r => r.Created)
                        .HasDefaultValueSql("now()");

            modelBuilder.Entity<DBReview>()
                        .HasOne(r => r.DBReviewable)
                        .WithMany(r => r.DBReviews)
                        .HasForeignKey(r => r.ReviewableId);

            modelBuilder.Entity<DBReview>()
                        .HasOne(r => r.DBAppUser)
                        .WithMany(r => r.Reviews)
                        .HasForeignKey(r => r.AppUserId);

            modelBuilder.Entity<DBReview>()
                        .Property(r => r.Created)
                        .HasDefaultValueSql("now()");

            modelBuilder.Entity<DBAppUser>()
                        .Property(a => a.Joined)
                        .HasDefaultValueSql("now()");

            modelBuilder.Entity<DBBlocking>()
                        .HasKey(b => new { b.BlockerAppUserId, b.BlockedAppUserId });

            modelBuilder.Entity<DBBlocking>()
                        .HasOne(b => b.Blocker)
                        .WithMany(a => a.Blockings)
                        .HasForeignKey(b => b.BlockerAppUserId);

            modelBuilder.Entity<DBBlocking>()
                        .HasOne(b => b.Blocked)
                        .WithMany(a => a.Blockers)
                        .HasForeignKey(b => b.BlockedAppUserId);

            modelBuilder.Entity<DBBookmark>()
                        .HasKey(l => new { l.ReviewId, l.AppUserId });

            modelBuilder.Entity<DBBookmark>()
                        .HasOne(l => l.DBReview)
                        .WithMany(r => r.DBBookmarks)
                        .HasForeignKey(l => l.ReviewId);

            modelBuilder.Entity<DBBookmark>()
                        .HasOne(l => l.DBAppUser)
                        .WithMany(a => a.Bookmarks)
                        .HasForeignKey(l => l.AppUserId);

            modelBuilder.Entity<DBAppUser>().Property(a => a.Content).HasColumnType("json");
            modelBuilder.Entity<DBAppUser>().Property(a => a.Settings).HasColumnType("json");
            modelBuilder.Entity<DBAppUser>().Property(a => a.Preferences).HasColumnType("json");

            modelBuilder.Entity<DBReviewable>().Property(r => r.Content).HasColumnType("json");
            modelBuilder.Entity<DBReviewable>().Property(r => r.Info).HasColumnType("json");
        }
    }
}
