namespace KanbanBoard.Application.Dtos.IdentityDtos;

public class GetProfileDto
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Picture { get; set; }
    public string Title { get; set; }
}
