﻿using KanbanBoard.Application.Dtos.IssueDtos;

namespace KanbanBoard.Application.Services.Interface;

public interface IIssueService
{
    Task CreateIssueAsync(CreateIssueDto createIssue, CancellationToken cancellationToken = default(CancellationToken));
    Task UpdateIssueStatusAsync(UpdateIssueStatusDto updateIssueStatus, CancellationToken cancellationToken = default(CancellationToken));

}
