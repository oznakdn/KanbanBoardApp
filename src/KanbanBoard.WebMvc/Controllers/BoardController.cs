using KanbanBoard.Application.Dtos.BoardDtos;
using KanbanBoard.Application.Services.Manager;
using Microsoft.AspNetCore.Mvc;

namespace KanbanBoard.WebMvc.Controllers;

public class BoardController : Controller
{
    private readonly IServiceManager _manager;

    public BoardController(IServiceManager manager)
    {
        _manager = manager;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<JsonResult> Boards(CancellationToken cancellation = default)
    {
        IEnumerable<GetBoardDto> boards = await _manager.Board.GetBoardsAsync(cancellation);
        return Json(boards);
    }
}
