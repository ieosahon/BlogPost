global using BlogPost.Domain.DTOs.Response;

namespace BlogPost.Persistence.Repository;


public class BlogRepository : IBlogRepository
{
    private readonly BlogPostContext _context;

    public BlogRepository(BlogPostContext context)
    {
        _context = context;
    }

    public async Task<(List<BlogResponseDto> BlogList, int Count)> GetAllBlogByAuthorId(uint authorId, int pageNumber, int pageSize)
    {
        var query = (from b in _context.Blogs
            where b.AuthorId == authorId
            select new BlogResponseDto
            {
                Id = b.Id,
                Name = b.Name,
                Url = b.Url,
                DateCreated = b.DateCreated
            }).AsQueryable();
                 
        var count = await query.CountAsync();
        var blogList = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (blogList, count);
    }
}
