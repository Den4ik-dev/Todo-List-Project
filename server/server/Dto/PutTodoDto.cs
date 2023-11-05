namespace server.Dto;

public class PutTodoDto : ITodoDto
{
  public string Name { get; set; }
  public bool Complete { get; set; }
}