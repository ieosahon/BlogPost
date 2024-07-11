// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
namespace BlogPost.Domain.Entity;

public class Blog
{
    public uint Id { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
    public string? DateCreated { get; set; }  = DateTime.Now.ToString("dd-MMM-yyyy");
    public uint AuthorId { get; set; }
    public virtual Author? Author { get; set; }
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}