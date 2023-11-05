using Microsoft.EntityFrameworkCore;
using server.Dto;
using server.Models;

namespace server.Services.Todoes;

public class TodoesService : ITodoesService
{
  private ApplicationContext _db;
  public TodoesService(ApplicationContext db) =>
    _db = db;

  public IEnumerable<GetTodoDto> GetTodoesByUserId(int userId) =>
    _db.Todoes
      .Where(t => t.UserId == userId)
      .Select(t => new GetTodoDto()
      {
        Id = t.Id,
        Name = t.Name,
        Complete = t.Complete
      })
      .ToArray();
  public async Task<Todo> AddTodo(string todoName, int userId)
  {
    Todo newTodo = _db.Todoes.Add(new Todo()
    {
      Name = todoName,
      UserId = userId
    }).Entity;
    await _db.SaveChangesAsync();

    return newTodo;
  }

  public async Task<Todo?> FindTodo(int todoId, int userId) =>
    await _db.Todoes.FirstOrDefaultAsync(t => 
      t.Id == todoId &&
      t.UserId == userId);

  public async Task ChangeTodo(Todo todo, PutTodoDto changedTodo)
  {
    todo.Name = changedTodo.Name;
    todo.Complete = changedTodo.Complete;

    await _db.SaveChangesAsync();
  }

  public async Task RemoveTodo(Todo deletedTodo)
  {
    _db.Todoes.Remove(deletedTodo);

    await _db.SaveChangesAsync();
  }
}
