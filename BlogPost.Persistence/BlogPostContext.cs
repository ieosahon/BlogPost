global using BlogPost.Domain.Entity;
global using Microsoft.EntityFrameworkCore;

namespace BlogPost.Persistence;

public class BlogPostContext : DbContext
{
    public BlogPostContext(DbContextOptions opt) : base(opt) { }
    public virtual DbSet<Blog> Blogs { get; set; }
    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<Post> Posts { get; set; }
}