using FluentValidation;
using server.Dto;

namespace server.Validation;
public class TokenDtoValidator : AbstractValidator<TokenDto>
{
  public TokenDtoValidator()
  {
    RuleFor(t => t.AccessToken)
      .NotNull()
      .NotEmpty();

    RuleFor(t => t.RefreshToken)
      .NotNull()
      .NotEmpty();
  }
}