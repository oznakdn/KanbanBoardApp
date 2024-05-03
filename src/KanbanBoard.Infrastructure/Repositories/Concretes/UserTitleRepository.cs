using KanbanBoard.Core.Models;
using KanbanBoard.Infrastructure.EFContext;
using KanbanBoard.Infrastructure.Repositories.Abstract;
using KanbanBoard.Infrastructure.Repositories.Interfaces;

namespace KanbanBoard.Infrastructure.Repositories.Concretes;

public class UserTitleRepository : AbstractRepository<UserTitle>, IUserTitleRepository
{
    public UserTitleRepository(EfDbContext dbContext) : base(dbContext)
    {
    }
}
