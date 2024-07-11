// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
namespace BlogPost.Domain.Entity;

public class Author
{
    public uint Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? DateCreated { get; set; } = DateTime.Now.ToString("dd-MMM-yyyy");
    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
}