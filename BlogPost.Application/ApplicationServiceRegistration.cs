global using MediatR;
global using Microsoft.Extensions.DependencyInjection;
global using System.Reflection;

namespace BlogPost.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        return services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}