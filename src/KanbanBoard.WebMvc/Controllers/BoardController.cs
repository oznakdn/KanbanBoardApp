using Microsoft.AspNetCore.Mvc;

namespace KanbanBoard.WebMvc.Controllers;

public class BoardController : Controller
{
    public IActionResult Index(string id)
    {
        // Board' un Status ve Issue lari gelecek drag and drop alani burasi
        return View();
    }
}
