using AspNetCoreHero.ToastNotification.Abstractions;
using KanbanBoard.Application.Dtos.BoardDtos;
using KanbanBoard.Application.Services.Manager;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KanbanBoard.WebMvc.Controllers;

public class BoardController : Controller
{
    private readonly IServiceManager _manager;
    private readonly INotyfService _notyfService;
    public BoardController(IServiceManager manager, INotyfService notyfService)
    {
        _manager = manager;
        _notyfService = notyfService;
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

    [HttpPost]
    public async Task<IActionResult>Create(string title, string description)
    {
        var errors = new StringBuilder();

        if(string.IsNullOrWhiteSpace(title))
        {
            errors.AppendLine("Title cannot be empty!");
        }

        if(errors.Length>0)
        {
            _notyfService.Warning(errors.ToString());
            return RedirectToAction(nameof(Index));
        }


        await _manager.Board.CreateBoardAsync(new CreateBoardDto
        {
            Title = title,
            Description = description
        });

        _notyfService.Success("Project has been created successful.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Update(string id, string title, string description)
    {
        await _manager.Board.UpdateBoardAsync(new UpdateBoardDto
        {
            Id = id,
            Title = title,
            Description = description
        });

        _notyfService.Success("Project has been updated successful.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult>Delete(string id)
    {
        await _manager.Board.DeleteBoardAsync(id);
        _notyfService.Success("Project has been deleted successful.");
        return RedirectToAction(nameof(Index));
    }


}
