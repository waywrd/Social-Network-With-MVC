using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Share.Data.Models;

namespace Share.Data
{
    public class ShareDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {

        public ShareDbContext(DbContextOptions<ShareDbContext> options)
            : base(options)
        {

        }


        public DbSet<Post> Posts { get; set; }

        public DbSet<Friend> Friends { get; set; }

        public DbSet<Group> Groups { get; set; }

        public  DbSet<GroupPost> GroupPosts { get; set; }

       public DbSet<GroupMembers> GroupMembers { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Friend>().HasKey(fs => new { fs.ApplicationUserId, fs.ApplicationUserFriendId });
            builder.Entity<Friend>().HasOne(p => p.ApplicationUser).WithMany(p => p.FriendList).HasForeignKey(p => p.ApplicationUserId);
            builder.Entity<Friend>().HasOne(p => p.ApplicationUserFriend).WithMany().HasForeignKey(p => p.ApplicationUserFriendId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Post>().HasOne(p => p.ApplicationUser).WithMany(p => p.Posts).HasForeignKey(p => p.ApplicationUserId);
            builder.Entity<GroupPost>().HasOne(p => p.Group).WithMany(p => p.GroupPosts);
            builder.Entity<GroupMembers>().HasKey(p => new { p.MemberId, p.GroupId });
            builder.Entity<GroupMembers>().HasOne(p => p.Group).WithMany(p => p.Members);
            builder.Entity<GroupMembers>().HasOne(p => p.Member).WithMany(p => p.Groups);
        }


    }
}
