// ReSharper disable ClassNeverInstantiated.Global
namespace BlogPost.Domain.DTOs.Response;

public class AuthorResponseDto
{
    public uint Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? DateCreated { get; set; }
}