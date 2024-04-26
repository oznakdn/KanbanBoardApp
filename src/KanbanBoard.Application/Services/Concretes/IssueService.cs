using KanbanBoard.Application.Dtos.IssueDtos;
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
        // Second Issue
        var issue = await _repository.Issue.FindByIdAsync(updateIssueStatus.IssueId, cancellationToken, x => x.Status!);

  
        // Farkli status icerisinde order degisikligi oldugunda
        if (issue.StatusId != updateIssueStatus.StatusId)
        {

            issue.StatusId = updateIssueStatus.StatusId;
            _repository.Issue.Update(issue);
            await _repository.SaveAsync(cancellationToken);

            var status = await _repository.Status.FindByIdAsync(issue.StatusId, cancellationToken, x => x.Issues);

            foreach (var item in status.Issues)
            {

                if (item.Order == updateIssueStatus.NewOrder)
                {
                    item.Order = issue.Order;
                    _repository.Issue.Update(item);
                    var res = await _repository.SaveAsync(cancellationToken);

                    issue.Order = updateIssueStatus.NewOrder;
                    _repository.Issue.Update(issue);

                    var result = await _repository.SaveAsync(cancellationToken);
                }
                else
                {
                    issue.Order = updateIssueStatus.NewOrder;
                    _repository.Issue.Update(issue);

                    var result = await _repository.SaveAsync(cancellationToken);
                }

            }
        }
        else // Ayni status icerisinde order degisikligi oldugunda
        {
            //issue.Order = updateIssueStatus.NewOrder;

            var status = await _repository.Status.FindByIdAsync(issue.StatusId, cancellationToken, x => x.Issues);

            foreach (var item in status.Issues)
            {

                if (item.Order == updateIssueStatus.NewOrder)
                {
                    item.Order = issue.Order;
                    _repository.Issue.Update(item);
                   var res=  await _repository.SaveAsync(cancellationToken);

                    issue.Order = updateIssueStatus.NewOrder;
                    _repository.Issue.Update(issue);

                    var result = await _repository.SaveAsync(cancellationToken);
                }

            }


        }





        return issue.Status.BoardId;

    }

}
