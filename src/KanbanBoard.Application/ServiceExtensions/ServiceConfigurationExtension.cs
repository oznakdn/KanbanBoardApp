using KanbanBoard.Application.Services.Concretes;
using KanbanBoard.Application.Services.Interface;
using KanbanBoard.Application.Services.Manager;
using KanbanBoard.Core.Models;
using KanbanBoard.Infrastructure.ServiceExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KanbanBoard.Application.ServiceExtensions;

public static class ServiceConfigurationExtension
{
    public static void AddServiceContainer(this IServiceCollection services,IConfiguration configuration, string connectionString)
    {
        services.AddInfrastructureContainer(configuration,connectionString);

        services.AddScoped<IIssueService, IssueService>();
        services.AddScoped<IBoardService, BoardService>();
        services.AddScoped<IStatusService, StatusService>();


        services.AddScoped<IServiceManager, ServiceManager>();

    }

    //public static void AddSeedData(this IServiceProvider provider)
    //{
    //    var scope = provider.CreateScope();
    //    var user = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    //    if (user is null)
    //    {
    //        user!.CreateAsync(new User
    //        {
    //            Email = "john.doe@mail.com",
    //            UserName = "john_doe"
    //        }, "Password123*").Wait();
    //    }
    //}

    public static void AddSeedData(this IApplicationBuilder builder)
    {
        var scope = builder.ApplicationServices.CreateScope();
        var user = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        if (user is null)
        {
            user!.CreateAsync(new User
            {
                Email = "john.doe@mail.com",
                UserName = "john_doe",
                ProfilePicture = "https://us.123rf.com/450wm/djvstock/djvstock1508/djvstock150806893/44095667-web-developer-design-vector-illustration-eps-10.jpg?ver=6"
            }, "Password123*").Wait();
        }
    }


}
