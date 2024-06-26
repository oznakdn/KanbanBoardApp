﻿using KanbanBoard.Core.Models;

namespace KanbanBoard.Application.Services.Interface;

public interface IUserTitleService
{
    Task<UserTitle>GetTitleByIdAsync(string id, CancellationToken cancellationToken = default(CancellationToken));
    Task<IEnumerable<UserTitle>> GetTitlesAsync(CancellationToken cancellationToken = default(CancellationToken));

}
