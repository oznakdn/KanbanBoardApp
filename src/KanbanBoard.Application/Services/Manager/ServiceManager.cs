using KanbanBoard.Application.Services.Interface;

namespace KanbanBoard.Application.Services.Manager;

public class ServiceManager : IServiceManager
{
    private readonly IIssueService _issue;
    private readonly IBoardService _board;
    public ServiceManager(IIssueService issue, IBoardService board)
    {
        _issue = issue;
        _board = board;
    }

    public IIssueService Issue => _issue;
    public IBoardService Board => _board;

}
