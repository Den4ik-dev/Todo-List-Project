using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Dto;
using server.Models;
using server.Services.Todoes;

namespace server.Controllers;


[ApiController]
[Route("api/todoes")]
public class TodoesController : ControllerBase
{
  private ITodoesService _todoesService;
  public TodoesController(ITodoesService todoesService) =>
    _todoesService = todoesService;


  // @desc Get todoes
  // @route GET api/todoes
  [Authorize]
  [HttpGet]
  public IActionResult GetTodoes()
  {
    int userId = int.Parse(User.Identity.Name);
    var todoes = _todoesService.GetTodoesByUserId(userId);

    return Ok(todoes);
  }

  // @desc Add todoes
  // @route POST api/todoes
  [Authorize]
  [HttpPost]
  public async Task<IActionResult> CreateTodo([FromBody] PostTodoDto newTodo,
    [FromServices] IValidator<ITodoDto> postTodoDtoValidator)
  {
    var postTodoDtoValidatorResult = postTodoDtoValidator.Validate(newTodo);
    if (!postTodoDtoValidatorResult.IsValid)
      return BadRequest(new { Message = postTodoDtoValidatorResult.Errors.First().ErrorMessage });

    Todo todo = await _todoesService.AddTodo( newTodo.Name, int.Parse(User.Identity.Name));

    return Ok(new
    {
      Message = "Дело успешно добавлено",
      Todo = new GetTodoDto()
      {
        Id = todo.Id,
        Name = todo.Name,
        Complete = todo.Complete
      }
    });
  }

  // @desc Change todo
  // @route PUT api/todoes/{id:int}
  [Authorize]
  [HttpPut("{id:int}")]
  public async Task<IActionResult> ChangeTodo(int id, [FromBody] PutTodoDto changedTodo, 
    IValidator<ITodoDto> putTodoDtoValidator)
  {
    var putTodoDtoValidatorResult = putTodoDtoValidator.Validate(changedTodo);
    if (!putTodoDtoValidatorResult.IsValid)
      return BadRequest(new { Message = putTodoDtoValidatorResult.Errors.First().ErrorMessage });

    Todo todo = await _todoesService.FindTodo(id, int.Parse(User.Identity.Name));

    if (todo == null)
      return NotFound(new { Message = "Данное дело не найдено!" });

    await _todoesService.ChangeTodo(todo, changedTodo);

    return Ok(new { Message = "Дело успешно изменено!" });
  }

  // @desc Delete todo
  // @route DELETE api/todoes/{id:int}
  [Authorize]
  [HttpDelete("{id:int}")]
  public async Task<IActionResult> RemoveTodo(int id)
  {
    Todo todo = await _todoesService.FindTodo(id, int.Parse(User.Identity.Name));

    if (todo == null)
      return NotFound(new { Message = "Данное дело не найдено!" });

    await _todoesService.RemoveTodo(todo);

    return Ok(new { Message = "Дело успешно удалено" });
  }
}
