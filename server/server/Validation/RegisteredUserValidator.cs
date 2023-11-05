using FluentValidation;
using server.Dto;

namespace server.Validation;

public class RegisteredUserValidator : AbstractValidator<RegisteredUser>
{
  public RegisteredUserValidator()
  {
    RuleFor(u => u.Login)
      .NotNull()
      .NotEmpty()
      .WithMessage("Вы не указали логин!");

    RuleFor(u => u.Password)
      .NotNull()
      .NotEmpty()
      .WithMessage("Вы не указали пароль!");

    RuleFor(u => u.Password)
      .MinimumLength(8)
      .WithMessage("Пароль должен сотволять не менее 8 символов");
  }
}