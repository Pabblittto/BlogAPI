﻿// <auto-generated />
using System;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlogAPI.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20190828213938_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BlogAPI.Models.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BlogName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime>("DateOfCreated")
                        .HasColumnType("Date");

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<string>("User");

                    b.HasKey("BlogId");

                    b.HasIndex("User")
                        .IsUnique();

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("BlogAPI.Models.BlogUserIdentity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("BlogUserIdentity");
                });

            modelBuilder.Entity("BlogAPI.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CommentId1");

                    b.Property<string>("Content")
                        .HasMaxLength(400);

                    b.Property<DateTime>("DateOfComment")
                        .HasColumnType("Date");

                    b.Property<string>("OwnerId")
                        .IsRequired();

                    b.Property<int>("PostId");

                    b.HasKey("CommentId");

                    b.HasIndex("CommentId1");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BlogAPI.Models.ImgPost", b =>
                {
                    b.Property<int>("PostId");

                    b.Property<string>("Src");

                    b.HasKey("PostId", "Src");

                    b.ToTable("ImgPost");
                });

            modelBuilder.Entity("BlogAPI.Models.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BlogId");

                    b.Property<string>("ContentOne");

                    b.Property<string>("ContentTwo");

                    b.Property<DateTime>("DateOfPost")
                        .HasColumnType("Date");

                    b.Property<string>("Thumbnail");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("PostId");

                    b.HasIndex("BlogId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("BlogAPI.Models.PostTag", b =>
                {
                    b.Property<int>("PostId");

                    b.Property<int>("TagId");

                    b.HasKey("PostId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("PostTags");
                });

            modelBuilder.Entity("BlogAPI.Models.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("OwnerID");

                    b.Property<bool>("PublicTag");

                    b.HasKey("TagId");

                    b.HasIndex("OwnerID");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BlogAPI.Models.Blog", b =>
                {
                    b.HasOne("BlogAPI.Models.BlogUserIdentity", "BlogUserIdentity")
                        .WithOne("Blog")
                        .HasForeignKey("BlogAPI.Models.Blog", "User")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BlogAPI.Models.Comment", b =>
                {
                    b.HasOne("BlogAPI.Models.Comment")
                        .WithMany("Answers")
                        .HasForeignKey("CommentId1");

                    b.HasOne("BlogAPI.Models.BlogUserIdentity", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BlogAPI.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BlogAPI.Models.ImgPost", b =>
                {
                    b.HasOne("BlogAPI.Models.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BlogAPI.Models.Post", b =>
                {
                    b.HasOne("BlogAPI.Models.Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId");
                });

            modelBuilder.Entity("BlogAPI.Models.PostTag", b =>
                {
                    b.HasOne("BlogAPI.Models.Post", "Post")
                        .WithMany("PostTags")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BlogAPI.Models.Tag", "Tag")
                        .WithMany("PostTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BlogAPI.Models.Tag", b =>
                {
                    b.HasOne("BlogAPI.Models.BlogUserIdentity", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerID");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
