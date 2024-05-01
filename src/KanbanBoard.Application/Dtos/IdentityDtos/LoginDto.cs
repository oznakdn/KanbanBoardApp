namespace KanbanBoard.Application.Dtos.IdentityDtos;

public class LoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public bool Remember { get; set; }

}
