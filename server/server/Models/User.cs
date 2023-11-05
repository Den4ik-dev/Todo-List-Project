namespace server.Models;

public class User
{
  public int Id { get; set; }
  public string Login { get; set; }
  public string Password { get; set; }

  public string? RefreshToken { get; set; }
  public DateTime? RefreshTokenExpiryTime { get; set; }

  public List<Todo> TodoList { get; set; }
}