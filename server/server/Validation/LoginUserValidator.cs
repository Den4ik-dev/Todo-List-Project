using FluentValidation;
using server.Dto;

namespace server.Validation;

public class LoginUserValidator : AbstractValidator<LoginUser>
{
  public LoginUserValidator()
  {
    RuleFor(u => u.Login)
      .NotNull()
      .NotEmpty()
      .WithMessage("Вы не указали логин!");

    RuleFor(u => u.Password)
      .NotNull()
      .NotEmpty()
      .WithMessage("Вы не указали пароль!");
  }
}