// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable ClassWithVirtualMembersNeverInherited.Global
namespace BlogPost.Domain.Entity;

public class Post
{
    public uint Id { get; set; }
    public string? Content { get; set; }
    public string? DatePublished { get; set; } = DateTime.Now.ToString("dd-MMM-yyyy");
    public uint BlogId { get; set; }
    public virtual Blog? Blog { get; set; }
}