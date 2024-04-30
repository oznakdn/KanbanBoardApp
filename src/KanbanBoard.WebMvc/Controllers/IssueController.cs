using KanbanBoard.Application.Dtos.IssueDtos;
using KanbanBoard.Application.Services.Manager;
using KanbanBoard.Core.Enums;
using KanbanBoard.Core.Models;
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
        return RedirectToAction("Index", "Status", new { id = boardId });
    }

    public async Task<IActionResult> IssueDetail(string id)
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
            Priority = Convert.ToInt16(issue.Priority),
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


    public IActionResult Create(string statusId)
    {
        TempData["statusId"] = statusId;
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(string statusId, string summary, string description,int priorityType, int issueType)
    {
       
            string boardId = await _manager.Issue.CreateIssueAsync(new CreateIssueDto
            {
                StatusId = statusId,
                Summary = summary,
                Description = description,
                IssueType = issueType,
                Priority = priorityType
            });

            return RedirectToAction("Index", "Status", new { id = boardId });

    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        string boardId = await _manager.Issue.DeleteIssueAsync(id);
        return RedirectToAction("Index", "Status", new { id = boardId });
    }

}
