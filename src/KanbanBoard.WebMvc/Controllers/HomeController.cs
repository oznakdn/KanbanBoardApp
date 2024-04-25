using KanbanBoard.Application.Dtos.BoardDtos;
using KanbanBoard.Application.Services.Manager;
using KanbanBoard.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KanbanBoard.WebMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceManager _manager;

        public HomeController(ILogger<HomeController> logger, IServiceManager manager)
        {
            _logger = logger;
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
