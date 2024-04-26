using KanbanBoard.Core.Models;
using KanbanBoard.Infrastructure.EFContext;
using KanbanBoard.Infrastructure.Repositories.Abstract;
using KanbanBoard.Infrastructure.Repositories.Interfaces;

namespace KanbanBoard.Infrastructure.Repositories.Concretes;

public class StatusRepository : AbstractRepository<Status>, IStatusRepository
{
    public StatusRepository(EfDbContext dbContext) : base(dbContext)
    {
    }
}
