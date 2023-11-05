namespace server.Dto;

public class GetTodoDto : ITodoDto
{
  public int Id { get; set; }
  public string Name { get; set; }
  public bool Complete { get; set; }
  public bool IsChanging { get; set; }
}
