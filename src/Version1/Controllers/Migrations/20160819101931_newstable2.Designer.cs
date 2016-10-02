using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Version1.Models;

namespace Version1.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160819101931_newstable2")]
    partial class newstable2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Version1.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<int>("Age");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Portrait");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Version1.Models.Authorization_Object", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActionName")
                        .HasAnnotation("MaxLength", 150);

                    b.Property<string>("FullControllerName")
                        .HasAnnotation("MaxLength", 150);

                    b.Property<string>("ObjectDescription");

                    b.Property<string>("ObjectName")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 200);

                    b.Property<string>("ObjectType")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("ID");

                    b.ToTable("Authorization_Object");
                });

            modelBuilder.Entity("Version1.Models.Authorization_Object_Role", b =>
                {
                    b.Property<int>("Authorization_Object_ID");

                    b.Property<string>("RoleID");

                    b.HasKey("Authorization_Object_ID", "RoleID");

                    b.HasIndex("Authorization_Object_ID", "RoleID");

                    b.ToTable("Authorization_Object_Role");
                });

            modelBuilder.Entity("Version1.Models.Events", b =>
                {
                    b.Property<string>("event_ID");

                    b.Property<string>("event_address");

                    b.Property<DateTime>("event_datetime");

                    b.Property<string>("event_name");

                    b.Property<string>("event_picture");

                    b.Property<string>("event_profile");

                    b.Property<string>("teamid");

                    b.HasKey("event_ID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Version1.Models.News", b =>
                {
                    b.Property<string>("ID");

                    b.Property<string>("AuthorID")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<DateTime>("CensorTime");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("NewsContent");

                    b.Property<string>("NewsImage");

                    b.Property<string>("NewsTitle")
                        .HasAnnotation("MaxLength", 200);

                    b.Property<string>("NewsType")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<bool>("Selected");

                    b.Property<string>("SensorID")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("ID");

                    b.ToTable("News");
                });

            modelBuilder.Entity("Version1.Models.Newsletter", b =>
                {
                    b.Property<string>("NewsletterId");

                    b.Property<string>("Detail");

                    b.Property<string>("ImagePath");

                    b.Property<string>("NewsletterName");

                    b.Property<DateTime>("PublishDate");

                    b.HasKey("NewsletterId");

                    b.ToTable("Newsletters");
                });

            modelBuilder.Entity("Version1.Models.OperationLogs", b =>
                {
                    b.Property<string>("ID")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Action")
                        .HasAnnotation("MaxLength", 200);

                    b.Property<string>("Controller")
                        .HasAnnotation("MaxLength", 200);

                    b.Property<byte[]>("LogTimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<string>("Operation")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<string>("OperationResult")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<DateTime>("OperationTime");

                    b.Property<string>("OperationType")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("UserID")
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("ID");

                    b.ToTable("OperationLogs");
                });

            modelBuilder.Entity("Version1.Models.Team", b =>
                {
                    b.Property<string>("TeamId");

                    b.Property<string>("TeamDescription");

                    b.Property<string>("TeamImage");

                    b.Property<string>("TeamLeaderID");

                    b.Property<string>("TeamName")
                        .IsRequired();

                    b.HasKey("TeamId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Version1.Models.TeamMember", b =>
                {
                    b.Property<string>("TeamId");

                    b.Property<string>("UserID");

                    b.HasKey("TeamId", "UserID");

                    b.HasIndex("TeamId");

                    b.HasIndex("UserID");

                    b.ToTable("TeamMembers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Version1.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Version1.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Version1.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Version1.Models.TeamMember", b =>
                {
                    b.HasOne("Version1.Models.Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Version1.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
