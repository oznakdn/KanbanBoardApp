using KanbanBoard.Application.Services.Concretes;
using KanbanBoard.Application.Services.Interface;
using KanbanBoard.Application.Services.Manager;
using KanbanBoard.Core.Models;
using KanbanBoard.Infrastructure.EFContext;
using KanbanBoard.Infrastructure.ServiceExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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


    public static void AddSeedData(this IApplicationBuilder app)
    {
        app.AutoMigration();
        var scope = app.ApplicationServices.CreateScope();
        var user = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

        if (!user.Users.Any())
        {
            user!.CreateAsync(new User
            {
                Email = "john.doe@mail.com",
                UserName = "john_doe",
                FirstName = "John",
                LastName = "Doe",
                ProfilePicture= "johndoe.jpg",
                TitleId = new Guid("dce1456b-cb9f-4089-a16c-a9f451d7b463").ToString()
            }, "Password123*").Wait();
        }

    }


    private static void AutoMigration(this IApplicationBuilder app)
    {
        var scope = app.ApplicationServices.CreateScope();
        var database = scope.ServiceProvider.GetRequiredService<EfDbContext>();
        database.Database.EnsureCreated();
    }


}
