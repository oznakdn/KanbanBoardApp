using KanbanBoard.Application.Dtos.StatusDtos;
using KanbanBoard.Application.Services.Manager;
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

    public async Task<IActionResult>Create(CreateStatusDto createStatus)
    {
        await _manager.Status.CreateStatusAsync(createStatus);
        return Json(new { redirectToUrl = Url.Action("Index", "Status", new { id = createStatus.BoardId }) });
    }

}
