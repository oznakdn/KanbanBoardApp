using KanbanBoard.Application.Services.Interface;

namespace KanbanBoard.Application.Services.Manager;

public interface IServiceManager
{
    IIssueService Issue { get; }
    IBoardService Board { get; }

}
