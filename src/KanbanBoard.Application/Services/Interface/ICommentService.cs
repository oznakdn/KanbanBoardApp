using KanbanBoard.Application.Dtos.CommentDtos;

namespace KanbanBoard.Application.Services.Interface;

public interface ICommentService
{
    Task<IEnumerable<GetCommentDto>>GetCommentsByIssueIdAsync(string issueId, CancellationToken cancellationToken = default(CancellationToken));
    Task<string> CreateCommentAsync(CreateCommentDto createComment, CancellationToken cancellationToken = default(CancellationToken));

}
