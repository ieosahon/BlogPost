namespace BlogPost.Domain.DTOs.Request;

public class PostDto
{
    public string? Content { get; set; }
    public uint BlogId { get; set; }
}