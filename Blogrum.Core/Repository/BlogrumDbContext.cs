using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blogrum.Core.Domain.Posts;
using System.Data.Entity;

namespace Blogrum.Core.Repository
{
    public class BlogrumDbContext : DbContext
    {
        public BlogrumDbContext()
            : base("BlogrumDbContext") { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Posts

            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Post>().ToTable("Post");
            modelBuilder.Entity<Tag>().ToTable("Tag");

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Tags)
                .WithMany(t => t.Posts)
                .Map(m =>
                {
                    m.ToTable("Post_Tag");
                    m.MapLeftKey("PostId");
                    m.MapRightKey("TagId");
                });

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
