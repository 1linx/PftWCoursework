using Blog.Platform2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

public class PostsContext : DbContext
{
    // You can add custom code to this file. Changes will not be overwritten.
    // 
    // If you want Entity Framework to drop and regenerate your database
    // automatically whenever you change your model schema, please use data migrations.
    // For more information refer to the documentation:
    // http://msdn.microsoft.com/en-us/data/jj591621.aspx

    public PostsContext() : base("name=PostsContext")
    {
    }
    

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
        .HasMany(t => t.Keywords)
        .WithMany(t => t.Posts);
    }

    public DbSet<Blog.Platform2.Models.Post> Posts { get; set; }
    public DbSet<Blog.Platform2.Models.Keyword> Keywords { get; set; }
}
