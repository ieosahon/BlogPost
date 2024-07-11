namespace BlogPost.Application.Contract.Interface;

public interface IAuthorRepository
{
    Task<(List<AuthorResponseDto> AuthorList, int Count)> GetAllAuthors(int pageNumber, int pageSize);
}