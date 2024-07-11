global using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPost.Persistence.Configuration;

public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id)
            .HasColumnName("Id");

        entity.Property(e => e.DateCreated)
            .HasColumnName("DateCreated")
            .IsRequired()
            .HasMaxLength(20);

        entity.Property(e => e.Name)
            .HasColumnName("Name")
            .IsRequired()
            .HasMaxLength(150);

        entity.Property(e => e.Url)
            .HasColumnName("Url")
            .IsRequired()
            .HasMaxLength(250);

        entity.Property(e => e.DateCreated)
            .HasColumnName("DateCreated")
            .IsRequired();

        entity.Property(e => e.AuthorId)
            .HasColumnName("AuthorId")
            .HasMaxLength(150);
        entity.HasOne(e => e.Author)
            .WithMany(a => a.Blogs)
            .HasForeignKey(e => e.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}