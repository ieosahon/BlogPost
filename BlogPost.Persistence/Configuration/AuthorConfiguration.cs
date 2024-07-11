namespace BlogPost.Persistence.Configuration;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> entity)
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id)
            .HasColumnName("Id");

        entity.Property(e => e.DateCreated)
            .HasColumnName("DateCreated")
            .IsRequired()
            .HasMaxLength(20);

        entity.Property(e => e.FirstName)
            .HasColumnName("FirstName")
            .IsRequired()
            .HasMaxLength(100);
        entity.Property(e => e.LastName)
            .HasColumnName("LastName")
            .IsRequired()
            .HasMaxLength(100);
        entity.Property(e => e.Email)
            .HasColumnName("Email")
            .IsRequired()
            .HasMaxLength(150);
    }
}