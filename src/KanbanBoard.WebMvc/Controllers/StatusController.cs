using AspNetCoreHero.ToastNotification.Abstractions;
using KanbanBoard.Application.Dtos.StatusDtos;
using KanbanBoard.Application.Services.Manager;
using KanbanBoard.WebMvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KanbanBoard.WebMvc.Controllers;

public class StatusController : Controller
{
    private readonly IServiceManager _manager;
    private readonly INotyfService _notyfService;


    public StatusController(IServiceManager manager, INotyfService notyfService)
    {
        _manager = manager;
        _notyfService = notyfService;
    }

    public async Task<IActionResult> Index(string id)
    {
        var board = await _manager.Board.GetBoardByIdAsync(id);
        var status = await _manager.Status.GetStatusByBoardIdAsync(id);
        ViewData["boardId"] = id;
        ViewData["boardTitle"] = board.Title;
        ViewData["boardDescription"] = board.Description;
        return View(status);
    }

    [HttpPost]
    public async Task<IActionResult> Create(string boardId, string name)
    {
        await _manager.Status.CreateStatusAsync(new CreateStatusDto
        {
            BoardId = boardId,
            Name = name
        });

        _notyfService.Success("Status has been created successful.");
        return RedirectToAction(nameof(Index), "Status", new { id = boardId });
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        string boardId = await _manager.Status.DeleteStatusAsync(id);
        _notyfService.Success("Status has been deleted successful.");
        return RedirectToAction(nameof(Index), "Status", new { id = boardId });
    }

    [HttpPost]
    public async Task<IActionResult> Update(string id, string name)
    {
        
        string boardId = await _manager.Status.UpdateStatusAsync(new UpdateStatusDto
        {
            Id = id,
            Name = name
        });

        _notyfService.Success("Status has been updated successful.");
        return RedirectToAction(nameof(Index), "Status", new { id = boardId });
    }



}
