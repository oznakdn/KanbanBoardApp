using KanbanBoard.Application.Services.Manager;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KanbanBoard.WebMvc.ViewComponents;

public class GetIssueStatisticsViewComponent : ViewComponent
{
    private readonly IServiceManager _serviceManager;

    public GetIssueStatisticsViewComponent(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var result = await _serviceManager.Issue.GetIssueStatisticsAsync();
        return View(result);
    }
}
