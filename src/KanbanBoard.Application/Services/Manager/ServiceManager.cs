using KanbanBoard.Application.Services.Interface;

namespace KanbanBoard.Application.Services.Manager;

public class ServiceManager : IServiceManager
{
    private readonly IIssueService _issue;
    private readonly IBoardService _board;
    private readonly IStatusService _status;
    private readonly ICommentService _comment;
    public ServiceManager(IIssueService issue, IBoardService board, IStatusService status, ICommentService comment)
    {
        _issue = issue;
        _board = board;
        _status = status;
        _comment = comment;
    }

    public IIssueService Issue => _issue;
    public IBoardService Board => _board;
    public IStatusService Status => _status;
    public ICommentService Comment => _comment;

}
