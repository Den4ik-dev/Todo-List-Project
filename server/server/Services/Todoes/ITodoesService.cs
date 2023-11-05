using server.Dto;
using server.Models;

namespace server.Services.Todoes;
public interface ITodoesService
{
  public IEnumerable<GetTodoDto> GetTodoesByUserId(int userId);
  public Task<Todo> AddTodo(string todoName, int userId);
  public Task<Todo> FindTodo(int todoId, int userId);
  public Task ChangeTodo(Todo todo, PutTodoDto changedTodo);
  public Task RemoveTodo(Todo deletedTodo);
}