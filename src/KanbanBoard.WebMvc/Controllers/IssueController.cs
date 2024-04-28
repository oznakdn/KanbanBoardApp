using KanbanBoard.Application.Dtos.IssueDtos;
using KanbanBoard.Application.Services.Manager;
using KanbanBoard.Core.Enums;
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
        var status = await _manager.Status.GetStatusByIdAsync(issue.StatusId);

        var issueDto = new UpdateIssueDto
        {
            Id = issue.Id,
            StatusId = issue.StatusId,
            Summary = issue.Summary,
            Description = issue.Description,
            IssueType = Convert.ToInt16(issue.IssueType),
            Priority =Convert.ToInt16(issue.Priority),
            BoardId = status.BoardId
        };

        return View(issueDto);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(UpdateIssueDto updateIssue)
    {
        string boardId = await _manager.Issue.UpdateIssueAsync(updateIssue);
        return Json(new { redirectToUrl = Url.Action("Index", "Status", new { id = boardId }) });
    }
}
