using KanbanBoard.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KanbanBoard.Infrastructure.EFContext;

public class EfDbContext : IdentityDbContext<User, IdentityRole, string>
{
    public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
    {

    }

    public DbSet<Board> Boards { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Issue> Issues { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<UserIssues> UserIssues { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);


        string boardId = Guid.NewGuid().ToString();

        builder.Entity<Board>()
            .HasData(new Board
            {
                Id = boardId,
                Title = "First Board",
                Description = "First Board Description",


            });


        string statusId = Guid.NewGuid().ToString();

        builder.Entity<Status>()
            .HasData(
             new Status
             {
                 Id = statusId,
                 BoardId = boardId,
                 Name = "To Do"
             },
              new Status
              {
                  BoardId = boardId,
                  Name = "In Progress"
              },
              new Status
              {
                  BoardId = boardId,
                  Name = "Testing"
              },
              new Status
              {
                  BoardId = boardId,
                  Name = "Done"
              });

        builder.Entity<Issue>()
            .HasData(
            new Issue
            {
                StatusId = statusId,
                Summary = "First Issue",
                Description = "First Issue Description",
                IssueType = Core.Enums.IssueType.Task,
                Priority = Core.Enums.PriorityType.Medium,
                Order = 0
            },
            new Issue
            {
                StatusId = statusId,
                Summary = "Second Issue",
                Description = "Second Issue Description",
                IssueType = Core.Enums.IssueType.Story,
                Priority = Core.Enums.PriorityType.Low,
                Order = 1
            },
            new Issue
            {
                StatusId = statusId,
                Summary = "Third Issue",
                Description = "Third Issue Description",
                IssueType = Core.Enums.IssueType.Bug,
                Priority = Core.Enums.PriorityType.High,
                Order = 2
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
