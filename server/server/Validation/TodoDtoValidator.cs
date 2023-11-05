using FluentValidation;
using server.Dto;

namespace server.Validation;

public class TodoDtoValidator : AbstractValidator<ITodoDto>
{
  public TodoDtoValidator()
  {
    RuleFor(t => t.Name)
      .NotNull()
      .NotEmpty()
      .WithMessage("Вы не указали дело");
  }
}