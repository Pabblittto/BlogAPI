using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Models
{
    public class MyContext : IdentityDbContext
    {
        public MyContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<PostTag>().HasKey(ob => new { ob.PostId, ob.TagId });

            builder.Entity<PostTag>()
                .HasOne<Post>(ob => ob.Post)
                .WithMany(ob => ob.PostTags)
                .HasForeignKey(Posttagobj => Posttagobj.PostId);

            builder.Entity<PostTag>()
                .HasOne<Tag>(ob => ob.Tag)// tu mówimy że obiekt PostTag ma połączenie z jednym Tagiem
                .WithMany(ob => ob.PostTags)// tu że ten wspomniany tag ma wiele połączeń 
                .HasForeignKey(PostTagobj => PostTagobj.TagId);// i łączy się z PostTagiem za pomocą pola w PostTagu

            builder.Entity<BlogUserIdentity>()
                .HasOne<Blog>(user => user.Blog)
                .WithOne(ob => ob.BlogUserIdentity)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Blog>()
                .HasMany<Post>(ob => ob.Posts)
                .WithOne(ob => ob.Blog)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<BlogUserIdentity>(entity =>
            {
                entity.HasIndex(ob => ob.UserName).IsUnique(true);
            });

            builder.Entity<ImgPost>()
                .HasKey(ob => new { ob.PostId, ob.Src });

            builder.Entity<Blog>(entity => {
                entity.HasIndex(ob => ob.BlogName).IsUnique(true);
            });

            builder.Entity<Tag>(entity =>
            {
                entity.HasIndex(ob => ob.Name).IsUnique(true);
            });


        }



        public DbSet<Tag> Tags { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostTag> PostTags { get; set; }



    }
}
