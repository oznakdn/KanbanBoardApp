﻿using KanbanBoard.Application.Dtos.IssueDtos;
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
                    if(item.Order > updateIssueStatus.OldOrder)
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
}
