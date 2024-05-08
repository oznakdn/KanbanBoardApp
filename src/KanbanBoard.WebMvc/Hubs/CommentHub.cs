using KanbanBoard.Application.Dtos.CommentDtos;
using KanbanBoard.Application.Services.Manager;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KanbanBoard.WebMvc.Hubs;

public class CommentHub : Hub
{
    private readonly IServiceManager _serviceManager;

    public CommentHub(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task<List<GetCommentDto>> CreateComment(CreateCommentDto createComment)
    {
        await _serviceManager.Comment.CreateCommentAsync(createComment);
        var issueComments = await _serviceManager.Comment.GetCommentsByIssueIdAsync(createComment.IssueId);
        return issueComments.ToList();
    }

    public async Task<List<GetCommentDto>> GetComments(string issueId)
    {
        var issueComments = await _serviceManager.Comment.GetCommentsByIssueIdAsync(issueId);
        return issueComments.ToList();
    }

  

}
