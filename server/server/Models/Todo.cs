namespace server.Models;

public class Todo
{
  public int Id { get; set; }
  public string Name { get; set; }
  public bool Complete { get; set; } = false;

  public int UserId { get; set; }
  public User User { get; set; }
}
