using KanbanBoard.Application.Dtos.IssueDto;
using KanbanBoard.Application.Services.Interface;
using KanbanBoard.Core.Models;
using KanbanBoard.Infrastructure.Repositories.Manager;

namespace KanbanBoard.Application.Services.Concretes;

public class IssueService : IIssueService
{
    private readonly IRepositoryManager _repository;

    public IssueService(IRepositoryManager repository)
    {
        _repository = repository;
    }


    public async Task CreateIssueAsync(CreateIssueDto createIssue, CancellationToken cancellationToken = default)
    {
        var issue = new Issue
        {
            StatusId = createIssue.StatusId,
            Summary = createIssue.Summary,
            Description = createIssue.Description,
            IssueType = createIssue.IssueType,
            Priority = createIssue.Priority,
            Order = await _repository.Issue.GetNextOrderAsync(createIssue.StatusId)
        };

        _repository.Issue.Insers(issue);
        await _repository.SaveAsync(cancellationToken);
    }

    public async Task UpdateIssueStatusAsync(UpdateIssueStatusDto updateIssueStatus, CancellationToken cancellationToken = default)
    {
        var issue = await _repository.Issue.FindByIdAsync(updateIssueStatus.IssueId);

        issue.StatusId = updateIssueStatus.StatusId;
        _repository.Issue.Update(issue);
        await _repository.SaveAsync(cancellationToken);
    }
}
