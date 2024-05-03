using KanbanBoard.Application.Services.Interface;

namespace KanbanBoard.Application.Services.Manager;

public class ServiceManager : IServiceManager
{
    private readonly IIssueService _issue;
    private readonly IBoardService _board;
    private readonly IStatusService _status;
    private readonly ICommentService _comment;
    private readonly IUserTitleService _userTitle;

    public ServiceManager(IIssueService issue, IBoardService board, IStatusService status, ICommentService comment, IUserTitleService userTitle)
    {
        _issue = issue;
        _board = board;
        _status = status;
        _comment = comment;
        _userTitle = userTitle;
    }

    public IIssueService Issue => _issue;
    public IBoardService Board => _board;
    public IStatusService Status => _status;
    public ICommentService Comment => _comment;
    public IUserTitleService UserTitle => _userTitle;

}

