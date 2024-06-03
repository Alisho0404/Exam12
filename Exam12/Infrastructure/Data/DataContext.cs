using Domain.Enteties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { } 

        public DbSet<Meeting>Meetings { get; set; }
        public DbSet<Notification> Notifications { get; set;}
        public DbSet<Role> Roles { get; set;}
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 


            modelBuilder.Entity<UserRole>()
           .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                   .HasOne(ur => ur.User)
                   .WithMany(u => u.UserRole)
                   .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRole)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<User>()
                .HasMany(x => x.UserRole)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Role>()
                .HasMany(x => x.UserRole)
                .WithOne(x => x.Role)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
