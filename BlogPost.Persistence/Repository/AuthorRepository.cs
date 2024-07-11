namespace BlogPost.Persistence.Repository;

public class AuthorRepository : IAuthorRepository
{
    private readonly BlogPostContext _context;

    public AuthorRepository(BlogPostContext context)
    {
        _context = context;
    }

    public async Task<(List<AuthorResponseDto> AuthorList, int Count)> GetAllAuthors(int pageNumber, int pageSize)
    {
        var query = (from a in _context.Authors
           
            select new AuthorResponseDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName =a.LastName,
                DateCreated = a.DateCreated,
                Email = a.Email
            }).AsQueryable();
                 
        var count = await query.CountAsync();
        var postList = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (postList, count);
    }
}