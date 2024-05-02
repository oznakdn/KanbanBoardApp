using KanbanBoard.Application.Dtos.CommentDtos;
using KanbanBoard.Application.Services.Interface;
using KanbanBoard.Core.Models;
using KanbanBoard.Infrastructure.Repositories.Manager;

namespace KanbanBoard.Application.Services.Concretes;

public class CommentService : ICommentService
{
    private readonly IRepositoryManager _repositoryManager;

    public CommentService(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public async Task<string> CreateCommentAsync(CreateCommentDto createComment, CancellationToken cancellationToken = default)
    {
        var comment = new Comment
        {
            IssueId = createComment.IssueId,
            UserId = createComment.UserId,
            Text = createComment.Text
        };

        _repositoryManager.Comment.Insers(comment);
        await _repositoryManager.SaveAsync(cancellationToken);

        var issue = await _repositoryManager.Issue.FindByIdAsync(comment.IssueId, cancellationToken, x => x.Status!);
        return issue.Status!.BoardId;
    }

    public async Task<IEnumerable<GetCommentDto>> GetCommentsByIssueIdAsync(string issueId, CancellationToken cancellationToken = default)
    {
        var issueComments = await _repositoryManager.Comment.FindAllAsync(cancellationToken, filter: x => x.IssueId == issueId, includes: x => x.User!);

        return issueComments.OrderByDescending(x=>x.CreatedDate).Select(x => new GetCommentDto
        {
            UserProfilePicture = x.User!.ProfilePicture,
            Text = x.Text,
            CommentDate = x.CreatedDate,
            Username = x.User.UserName!
        });
    }
}
