using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace Version1.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<TeamsMembersJoin>().HasKey(table => new { table.UserId, table.TeamId });

            builder.Entity<TeamsMembersJoin>().HasOne(teamsMembersJoin => teamsMembersJoin.Team)
                .WithMany(team => team.TeamsMembersJoins)
                .HasForeignKey(tm => tm.TeamId);

            builder.Entity<TeamsMembersJoin>().HasOne(teamsMembersJoin => teamsMembersJoin.User)
              .WithMany(member => member.TeamsMembersJoins)
              .HasForeignKey(tm => tm.UserId);

        }
    }
}
