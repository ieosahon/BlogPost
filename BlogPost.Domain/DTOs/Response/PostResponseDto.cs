// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace BlogPost.Domain.DTOs.Response;

public class PostResponseDto
{
    public uint Id { get; set; }
    public string? Content { get; set; }
    public string? DatePublished { get; set; }
    public uint BlogId { get; set; }
    public string? Blog { get; set; }
    public string? BlogUrl { get; set; }
}