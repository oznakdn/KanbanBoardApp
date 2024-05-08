using AspNetCoreHero.ToastNotification.Abstractions;
using KanbanBoard.Application.Dtos.IssueDtos;
using KanbanBoard.Application.Services.Manager;
using KanbanBoard.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace KanbanBoard.WebMvc.Controllers;

public class IssueController : Controller
{

    private readonly IServiceManager _manager;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly INotyfService _notyfService;

    public IssueController(IServiceManager manager, INotyfService notyfService, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _manager = manager;
        _notyfService = notyfService;
        _userManager = userManager;
        _signInManager = signInManager;
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

        TempData["issueId"] = id;
        TempData["userId"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
        TempData["boardId"] = issueDto.BoardId;
        //var issueComments = await _manager.Comment.GetCommentsByIssueIdAsync(id);
        //TempData["issueComments"] = issueComments;

        return View(issueDto);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(UpdateIssueDto updateIssue)
    {
        string boardId = await _manager.Issue.UpdateIssueAsync(updateIssue);
        _notyfService.Success("Issue has been updated successful.");
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

        _notyfService.Success("Issue has been created successful.");
        return RedirectToAction("Index", "Status", new { id = boardId });

    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        string boardId = await _manager.Issue.DeleteIssueAsync(id);
        _notyfService.Success("Issue has been deleted successful.");
        return RedirectToAction("Index", "Status", new { id = boardId });
    }

}
