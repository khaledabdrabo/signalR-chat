using System;
using System.Collections.Generic;
using System.Text;
using chatProject1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace chatProject1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }
        //public virtual DbSet<UserFriend> UserFriends { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);//error for
            //modelBuilder.Entity<UserFriend>().HasKey(Uf => new
            //{
            //    Uf.userId,
            //    Uf.friendId
            //});
            //modelBuilder.Entity<UserFriend>().HasOne(f => f.Friend).WithMany(uf => uf.UserFriend).HasForeignKey(u => u.userId);
            //modelBuilder.Entity<UserFriend>().HasOne(f => f.User).WithMany(uf => uf.UserFriend).HasForeignKey(u => u.friendId);

            ////base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Friend>()
            //    .HasKey(o => new { o.userID, o.FriendId });
        }
    }
}
