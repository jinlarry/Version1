using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Version1.Models;


namespace Version1.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<Authorization_Object_Role>().HasKey(t => new { t.Authorization_Object_ID, t.RoleID });
            builder.Entity<Authorization_Object_Role>().HasIndex(t => new { t.Authorization_Object_ID, t.RoleID });

            //Team_mumber manytomany entity(Team and volunteer) statement
            builder.Entity<TeamMember>().HasKey(t => new { t.TeamId, t.UserID });
            builder.Entity<TeamMember>().HasOne(t => t.Teams).WithMany(p => p.TeamMembers).HasForeignKey(pt => pt.TeamId);
            builder.Entity<TeamMember>().HasOne(b => b.Volunteers).WithMany(p => p.TeamMembers).HasForeignKey(pt => pt.UserID);
            builder.Entity<Events>().HasKey(a => a.event_ID);
        }

        public DbSet<Authorization_Object> Authorization_Object { get; set; }

        public DbSet<Authorization_Object_Role> Authorization_Object_Role { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<TeamMember> TeamMembers { get; set; }

        public DbSet<Events> Events { get; set; }

        public DbSet<Newsletter> Newsletters { get; set; }

        public DbSet<BookingOrder> BookingOrders { get; set; }

        public DbSet<Venue> Venues { get; set; }
    }

}
