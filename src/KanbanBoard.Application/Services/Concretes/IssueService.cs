using KanbanBoard.Application.Dtos.IssueDtos;
using KanbanBoard.Application.Services.Interface;
using KanbanBoard.Core.Enums;
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


    public async Task<string> CreateIssueAsync(CreateIssueDto createIssue, CancellationToken cancellationToken = default)
    {
        var issue = new Issue
        {
            StatusId = createIssue.StatusId,
            Summary = createIssue.Summary,
            Description = createIssue.Description,
            IssueType = (IssueType)createIssue.IssueType,
            Priority = (PriorityType)createIssue.Priority,
            Order = await _repository.Issue.GetNextOrderAsync(createIssue.StatusId)
        };

        _repository.Issue.Insers(issue);
        await _repository.SaveAsync(cancellationToken);

        var status = await _repository.Status.FindByIdAsync(issue.StatusId, cancellationToken);
        return status.BoardId;
    }

    public async Task<string> UpdateIssueAsync(UpdateIssueDto updateIssue, CancellationToken cancellationToken = default)
    {
        var issue = await _repository.Issue.FindByIdAsync(updateIssue.Id, cancellationToken, x => x.Status!);

        issue.StatusId = updateIssue.StatusId;
        issue.Summary = updateIssue.Summary;
        issue.Description = updateIssue.Description;
        issue.Priority = (PriorityType)updateIssue.Priority;
        issue.IssueType = (IssueType)updateIssue.IssueType;

        _repository.Issue.Update(issue);
        await _repository.SaveAsync(cancellationToken);

        return issue.Status!.BoardId;
    }


    public async Task<string> UpdateIssueStatusAndOrderAsync(UpdateIssueStatusDto updateIssueStatus, CancellationToken cancellationToken = default)
    {

        var issue = await _repository.Issue.FindByIdAsync(updateIssueStatus.IssueId, cancellationToken, x => x.Status!);
        string oldStatusId = issue.StatusId;

        // Farkli status icerisinde order degisikligi oldugunda
        if (issue.StatusId != updateIssueStatus.StatusId)
        {

            issue.StatusId = updateIssueStatus.StatusId;
            issue.Order = updateIssueStatus.NewOrder;
            _repository.Issue.Update(issue);
            await _repository.SaveAsync(cancellationToken);

            var status = await _repository.Status.FindByIdAsync(updateIssueStatus.StatusId, cancellationToken, x => x.Issues);

            if (status.Issues.Count > 1)
            {
                foreach (var item in status.Issues)
                {
                    if (item.Id == issue.Id) continue;

                    if (item.Order >= issue.Order)
                    {
                        item.Order++;
                        _repository.Issue.Update(item);
                        await _repository.SaveAsync(cancellationToken);
                    }
                }
            }

            // Old Status item updated
            var oldStatus = await _repository.Status.FindByIdAsync(oldStatusId, cancellationToken, x => x.Issues);
            if (oldStatus.Issues.Count > 0)
            {
                foreach (var item in oldStatus.Issues)
                {
                    if (item.Order > updateIssueStatus.OldOrder)
                    {
                        item.Order--;
                        _repository.Issue.Update(item);
                        await _repository.SaveAsync(cancellationToken);
                    }
                }
            }

        }
        else // Ayni status icerisinde order degisikligi oldugunda
        {
            await UpdateOrder(updateIssueStatus, issue, cancellationToken);

        }



        return issue.Status!.BoardId;

    }

    private async Task UpdateOrder(UpdateIssueStatusDto updateIssueStatus, Issue issue, CancellationToken cancellationToken)
    {
        var status = await _repository.Status.FindByIdAsync(issue.StatusId, cancellationToken, x => x.Issues);

        int a = updateIssueStatus.OldOrder - updateIssueStatus.NewOrder;

        // Yukaridan asagiya yada asagidan yukariya kendi aralarinda drop etmek

        if (a == 1 || a == -1)
        {
            foreach (var item in status.Issues)
            {
                if (item.Order == updateIssueStatus.NewOrder)
                {
                    issue.Order = updateIssueStatus.NewOrder;
                    _repository.Issue.Update(issue);
                    await _repository.SaveAsync();


                    item.Order = updateIssueStatus.OldOrder;
                    _repository.Issue.Update(item);
                    await _repository.SaveAsync();
                }
            }
        }

        // Asagidan yukariya dogru arada issue atlayarak drop etmek

        if (a > 1)
        {
            issue.Order = updateIssueStatus.NewOrder;
            _repository.Issue.Update(issue);
            await _repository.SaveAsync(cancellationToken);

            foreach (var item in status.Issues)
            {
                if (item.Id == issue.Id)
                {
                    continue;
                }

                item.Order++;
                _repository.Issue.Update(item);
                await _repository.SaveAsync();
            }
        }

        // Yukardan asagiya dogru arada issue atlayarak drop etmek
        if (a < -1)
        {
            issue.Order = updateIssueStatus.NewOrder;
            _repository.Issue.Update(issue);
            await _repository.SaveAsync(cancellationToken);

            foreach (var item in status.Issues)
            {
                if (item.Id == issue.Id)
                {
                    continue;
                }

                item.Order--;
                _repository.Issue.Update(item);
                await _repository.SaveAsync();
            }
        }
    }

    public async Task<GetIssueDto> GetIssueByIdAsync(string id, CancellationToken cancellationToken = default)
    {

        var issue = await _repository.Issue.FindByIdAsync(id, cancellationToken);

        return new GetIssueDto
        {
            Id = issue.Id,
            Summary = issue.Summary,
            Description = issue.Description,
            IssueType = issue.IssueType,
            Priority = issue.Priority,
            Order = issue.Order,
            StatusId = issue.StatusId
        };
    }

    public async Task<string> DeleteIssueAsync(string id, CancellationToken cancellationToken = default)
    {
        var issue = await _repository.Issue.FindByIdAsync(id, cancellationToken);
        var status = await _repository.Status.FindByIdAsync(issue.StatusId, cancellationToken);

        _repository.Issue.Delete(issue);
        await _repository.SaveAsync(cancellationToken);
        return status.BoardId;
    }
}
