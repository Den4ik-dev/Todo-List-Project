using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Dto;
using server.Models;
using server.Services.Tokens;
using server.Services.Users;
using System.Security.Claims;

namespace server.Controllers;

[ApiController]
[Route("api/token")]
public class TokenController : ControllerBase
{
  [HttpPost]
  [Route("refresh")]
  public async Task<IActionResult> Refresh([FromBody] TokenDto token,
    [FromServices] IValidator<TokenDto> tokenValidator,
    [FromServices] ITokenService tokenService,
    [FromServices] IUsersService usersService)
  {
    if (!tokenValidator.Validate(token).IsValid)
      return BadRequest(new { Message = "Некорректные данные" });

    ClaimsPrincipal? principal = null;
    try
    {
      principal = tokenService.GetPrincipalFromExpiredToken(token.AccessToken);
    }
    catch (Exception e)
    {
      return BadRequest(new { Message = e.Message });
    }

    int userId = int.Parse(principal.Identity.Name);
    User? user = await usersService.FindUser(u => u.Id == userId);

    if (user is null || user.RefreshToken != token.RefreshToken 
        || user.RefreshTokenExpiryTime <= DateTime.Now)
      return BadRequest(new { Message = "Некорректные данные" });

    string newAccessToken = tokenService.GenerateAccessToken(principal.Claims);
    
    string newRefreshToken = tokenService.GenerateRefreshToken();
    await usersService.SetUserRefreshToken(user, newRefreshToken);

    return Ok(new TokenDto()
    {
      AccessToken = newAccessToken,
      RefreshToken = newRefreshToken
    });
  }
}