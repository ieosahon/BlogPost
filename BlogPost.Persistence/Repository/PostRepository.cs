namespace BlogPost.Persistence.Repository;

public class PostRepository  : IPostRepository
{
    private readonly BlogPostContext _context;

    public PostRepository(BlogPostContext context)
    {
        _context = context;
    }

    public async Task<(List<PostResponseDto> PostList, int Count)> GetAllPostByBlogId(uint blogId, int pageNumber, int pageSize)
    {
        var query = (from p in _context.Posts
            join b in _context.Blogs on p.BlogId equals b.Id 
            where p.BlogId == blogId
            select new PostResponseDto
            {
                Id = p.Id,
                Content = p.Content,
                DatePublished = p.DatePublished,
                BlogId = b.Id,
                Blog = b.Name,
                BlogUrl = b.Url
                
            }).AsQueryable();
                 
        var count = await query.CountAsync();
        var postList = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (postList, count);
    }
}