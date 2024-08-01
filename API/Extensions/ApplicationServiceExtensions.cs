
using Application.Interfaces;
using Application.Profiles;
using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServiceExtensions(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();
        services.AddEntityFrameworkNpgsql().AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
        });
        services.AddCors();
        services.AddAutoMapper(typeof(MappingProfiles).Assembly);
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        return services;
    }
}