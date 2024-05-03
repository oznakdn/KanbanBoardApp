using KanbanBoard.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.Infrastructure.EFContext;

public class EfDbContext : IdentityDbContext<User>
{
    public static string User_Title_Id = Guid.NewGuid().ToString();
    public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
    {

    }

    public DbSet<Board> Boards { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Issue> Issues { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<UserTitle> UserTitles { get; set; }
    public DbSet<UserIssues> UserIssues { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);


        builder.Entity<UserTitle>()
            .HasData(
            new UserTitle
            {
                Id = User_Title_Id,
                Title = "Software Developer"
            },
            new UserTitle
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Back End Developer"
            },
            new UserTitle
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Front End Developer"
            },
            new UserTitle
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Software Engineer"
            },
            new UserTitle
            {
                Id = Guid.NewGuid().ToString(),
                Title = "DevOps Engineer"
            },
            new UserTitle
            {
                Id = Guid.NewGuid().ToString(),
                Title = "Product Manager"
            });


        string boardId = Guid.NewGuid().ToString();

        builder.Entity<Board>()
            .HasData(new Board
            {
                Id = boardId,
                Title = "Revamping User Onboarding",
                Description = "Develop a step-by-step interactive tutorial that guides new users through the key features and functionalities of the application.",
                CreatedDate = DateTime.UtcNow
            });


        string statusId = Guid.NewGuid().ToString();

        builder.Entity<Status>()
            .HasData(
             new Status
             {
                 Id = statusId,
                 BoardId = boardId,
                 Name = "To Do",
                 CreatedDate = DateTime.UtcNow
             },
              new Status
              {
                  BoardId = boardId,
                  Name = "In Progress",
                  CreatedDate = DateTime.UtcNow
              },
              new Status
              {
                  BoardId = boardId,
                  Name = "Testing",
                  CreatedDate = DateTime.UtcNow
              },
              new Status
              {
                  BoardId = boardId,
                  Name = "Done",
                  CreatedDate = DateTime.UtcNow
              });


        string issueId = Guid.NewGuid().ToString();

        builder.Entity<Issue>()
            .HasData(
            new Issue
            {
                Id = issueId,
                StatusId = statusId,
                Summary = " Create Interactive Tutorial for New Users",
                Description = "Develop a step-by-step interactive tutorial that guides new users through the key features and functionalities of the application. This will improve user experience and decrease the learning curve for new users.",
                IssueType = Core.Enums.IssueType.Story,
                Priority = Core.Enums.PriorityType.Medium,
                Order = 0,
                CreatedDate = DateTime.UtcNow
            },
            new Issue
            {
                StatusId = statusId,
                Summary = "Design Mobile-Friendly Welcome Screen",
                Description = "Design a user-friendly and informative welcome screen specifically optimized for mobile devices. This will ensure a smooth onboarding experience for users accessing the application on their phones.",
                IssueType = Core.Enums.IssueType.Task,
                Priority = Core.Enums.PriorityType.Low,
                Order = 1,
                CreatedDate = DateTime.UtcNow
            },
            new Issue
            {
                StatusId = statusId,
                Summary = "Welcome Email Not Sending to New Users",
                Description = "Investigate and fix the bug that is preventing welcome emails from being sent to new users upon registration. This will ensure users receive important information and next steps after signing up.",
                IssueType = Core.Enums.IssueType.Bug,
                Priority = Core.Enums.PriorityType.High,
                Order = 2,
                CreatedDate = DateTime.UtcNow
            });





        builder.Entity<UserIssues>()
            .HasOne<User>()
            .WithMany(x => x.UserIssues)
            .HasForeignKey(x => x.UserId);

        builder.Entity<UserIssues>()
            .HasOne<Issue>()
            .WithMany(x => x.UserIssues)
            .HasForeignKey(x => x.IssueId);




    }


}
