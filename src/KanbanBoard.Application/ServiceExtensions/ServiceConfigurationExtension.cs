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
    public static void AddServiceContainer(this IServiceCollection services, IConfiguration configuration, string connectionString)
    {
        services.AddInfrastructureContainer(configuration, connectionString);

        services.AddScoped<IIssueService, IssueService>();
        services.AddScoped<IBoardService, BoardService>();
        services.AddScoped<IStatusService, StatusService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IUserTitleService, UserTitleService>();

        services.AddScoped<IServiceManager, ServiceManager>();
    }


    public static void AddSeedData(this IApplicationBuilder builder)
    {
        var scope = builder.ApplicationServices.CreateScope();
        var user = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        if (!user.Users.Any())
        {
            user!.CreateAsync(new User
            {
                Email = "john.doe@mail.com",
                UserName = "john_doe",
                FirstName = "John",
                LastName = "Doe",
                TitleId = "1c0a1307-6f4a-44e4-a5b9-b2ed3a7f91d8",
                ProfilePicture = "https://us.123rf.com/450wm/djvstock/djvstock1508/djvstock150806893/44095667-web-developer-design-vector-illustration-eps-10.jpg?ver=6"
            }, "Password123*").Wait();
        }
    }


}
