using KanbanBoard.Application.Services.Concretes;
using KanbanBoard.Application.Services.Interface;
using KanbanBoard.Application.Services.Manager;
using KanbanBoard.Infrastructure.ServiceExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KanbanBoard.Application.ServiceExtensions;

public static class ServiceConfigurationExtension
{
    public static void AddServiceContainer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructureContainer(configuration);

        services.AddScoped<IIssueService, IssueService>();
        services.AddScoped<IBoardService, BoardService>();
        services.AddScoped<IStatusService, StatusService>();


        services.AddScoped<IServiceManager,ServiceManager>();

    }
}
