using KanbanBoard.Application.Dtos.StatusDtos;
using KanbanBoard.Application.Services.Manager;
using KanbanBoard.WebMvc.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBoard.WebMvc.Controllers;

public class StatusController : Controller
{
    private readonly IServiceManager _manager;

    public StatusController(IServiceManager manager)
    {
        _manager = manager;
    }

    public async Task<IActionResult> Index(string id)
    {
        var board = await _manager.Board.GetBoardByIdAsync(id);
        var status = await _manager.Status.GetStatusByBoardIdAsync(id);
        ViewData["boardId"] = id;
        ViewData["boardTitle"] = board.Title;
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

        return RedirectToAction(nameof(Index), "Status", new { id = boardId });
    }

    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        string boardId = await _manager.Status.DeleteStatusAsync(id);
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
        return RedirectToAction(nameof(Index), "Status", new { id = boardId });
    }



}
