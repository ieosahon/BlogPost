namespace BlogPost.Application.Contract.Interface;

public interface IBlogRepository
{
    Task<(List<BlogResponseDto> BlogList, int Count)> GetAllBlogByAuthorId(uint authorId, int pageNumber, int pageSize);
}
