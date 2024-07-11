namespace BlogPost.Persistence.Configuration;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> entity)
    {
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id)
            .HasColumnName("Id");

        entity.Property(e => e.DatePublished)
            .HasColumnName("DatePublished")
            .IsRequired()
            .HasMaxLength(20);

        entity.Property(e => e.Content)
            .HasColumnName("Content")
            .IsRequired()
            .HasMaxLength(5000);

        entity.Property(e => e.BlogId)
            .HasColumnName("BlogId")
            .IsRequired();

        entity.HasOne(e => e.Blog)
            .WithMany(b => b.Posts)
            .HasForeignKey(e => e.BlogId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}