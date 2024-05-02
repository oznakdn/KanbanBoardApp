using KanbanBoard.Core.Models;
using KanbanBoard.Infrastructure.EFContext;
using KanbanBoard.Infrastructure.Repositories.Abstract;
using KanbanBoard.Infrastructure.Repositories.Interfaces;

namespace KanbanBoard.Infrastructure.Repositories.Concretes;

public class CommentRepository : AbstractRepository<Comment>, ICommentRepository
{
    public CommentRepository(EfDbContext dbContext) : base(dbContext)
    {
    }
}
