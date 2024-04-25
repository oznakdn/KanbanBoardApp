using KanbanBoard.Application.Services.Interface;

namespace KanbanBoard.Application.Services.Manager;

public class ServiceManager : IServiceManager
{
    private readonly IIssueService _issue;
    public ServiceManager(IIssueService issue)
    {
        _issue = issue;
    }

    public IIssueService Issue => _issue;

}
