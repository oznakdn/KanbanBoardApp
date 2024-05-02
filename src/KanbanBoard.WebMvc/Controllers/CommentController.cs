using KanbanBoard.Application.Dtos.CommentDtos;
using KanbanBoard.Application.Services.Manager;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KanbanBoard.WebMvc.Controllers;

public class CommentController : Controller
{
    private readonly IServiceManager _manager;
    public CommentController(IServiceManager manager)
    {
        _manager = manager;
    }

    [HttpPost]
    public async Task<IActionResult> Create(string issueId, string userId, string text)
    {
        string boardId = await _manager.Comment.CreateCommentAsync(new CreateCommentDto
        {
            IssueId = issueId,
            UserId = userId,
            Text = text
        });

        return RedirectToAction("Index", "Status", new { id = boardId });
    }
}
