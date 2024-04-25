﻿using KanbanBoard.Infrastructure.Repositories.Interfaces;

namespace KanbanBoard.Infrastructure.Repositories.Manager;

public interface IRepositoryManager : IAsyncDisposable
{
    IIssueRepository Issue { get; }

    Task<int> SaveAsync(CancellationToken cancellationToken = default(CancellationToken));
}
