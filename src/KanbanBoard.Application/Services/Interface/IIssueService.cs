using KanbanBoard.Application.Dtos.IssueDtos;

namespace KanbanBoard.Application.Services.Interface;

public interface IIssueService
{
    Task CreateIssueAsync(CreateIssueDto createIssue, CancellationToken cancellationToken = default(CancellationToken));
    Task<string> UpdateIssueAsync(UpdateIssueDto updateIssue, CancellationToken cancellationToken = default(CancellationToken));
    Task<string> UpdateIssueStatusAndOrderAsync(UpdateIssueStatusDto updateIssueStatus, CancellationToken cancellationToken = default(CancellationToken));
    Task<GetIssueDto> GetIssueByIdAsync(string id, CancellationToken cancellationToken = default(CancellationToken));


}
