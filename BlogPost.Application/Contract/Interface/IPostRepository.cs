namespace BlogPost.Application.Contract.Interface;

public interface IPostRepository
{
    Task<(List<PostResponseDto> PostList, int Count)> GetAllPostByBlogId(uint blogId, int pageNumber, int pageSize);
}