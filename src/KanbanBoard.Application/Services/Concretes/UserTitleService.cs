using KanbanBoard.Application.Services.Interface;
using KanbanBoard.Core.Models;
using KanbanBoard.Infrastructure.Repositories.Manager;

namespace KanbanBoard.Application.Services.Concretes;

public class UserTitleService : IUserTitleService
{
    private readonly IRepositoryManager _repository;

    public UserTitleService(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<UserTitle> GetTitleByIdAsync(string id, CancellationToken cancellationToken = default)
    {
       return await _repository.UserTitle.FindByIdAsync(id, cancellationToken);
    }
}
