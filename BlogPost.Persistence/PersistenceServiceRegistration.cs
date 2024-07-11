global using BlogPost.Persistence.Repository;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;

namespace BlogPost.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<BlogPostContext>(options => { options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")); });
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IBlogRepository, BlogRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        return services;
    }

}