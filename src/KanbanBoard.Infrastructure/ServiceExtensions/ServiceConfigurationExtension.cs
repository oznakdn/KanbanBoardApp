using KanbanBoard.Core.Models;
using KanbanBoard.Infrastructure.EFContext;
using KanbanBoard.Infrastructure.Repositories.Concretes;
using KanbanBoard.Infrastructure.Repositories.Interfaces;
using KanbanBoard.Infrastructure.Repositories.Manager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KanbanBoard.Infrastructure.ServiceExtensions;

public static class ServiceConfigurationExtension
{
    public static void AddInfrastructureContainer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EfDbContext>(options => options.UseSqlite(configuration.GetConnectionString("SqliteConnection")));

        services.AddIdentityCore<User>(opt =>
        {
            opt.Password.RequiredLength = 6;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequireDigit = false;
            opt.Password.RequireLowercase = false;
            opt.Password.RequireUppercase = false;

        }).AddEntityFrameworkStores<EfDbContext>();


        services.AddScoped<IIssueRepository, IssueRepository>();
        services.AddScoped<IRepositoryManager, RepositoryManager>();

    }
}
