using KanbanBoard.Application.Dtos.IssueDtos;
using KanbanBoard.Application.Services.Manager;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBoard.WebMvc.Controllers;

public class IssueController : Controller
{

    private readonly IServiceManager _manager;

    public IssueController(IServiceManager manager)
    {
        _manager = manager;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateIssue(UpdateIssueStatusDto updateIssue)
    {
        var boardId = await _manager.Issue.UpdateIssueStatusAndOrderAsync(updateIssue);
        return RedirectToAction("Index", "Status", new { id= boardId });
    }

    public async Task<IActionResult>IssueDetail(string id)
    {
        var issue = await _manager.Issue.GetIssueByIdAsync(id);

        var issueDto = new GetIssueDto
        {
            Id = issue.Id,
            StatusId = issue.StatusId,
            Summary = issue.Summary,
            Description = issue.Description,
            IssueType = issue.IssueType,
            Priority = issue.Priority,
            Order = issue.Order
        };

        return View(issueDto);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update()
    {
        return View();
    }
}
