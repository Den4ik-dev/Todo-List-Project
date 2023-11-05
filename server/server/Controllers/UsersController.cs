using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Dto;
using server.Models;
using server.Services.Authorization;
using server.Services.Tokens;
using server.Services.Users;
using System.Security.Claims;
using System.Security.Cryptography;

namespace server.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
  private ITokenService _tokenService;
  private IUsersService _usersService;
  public UsersController(ITokenService tokenService, IUsersService usersService)
  {
    _tokenService = tokenService;
    _usersService = usersService;
  }

  // @desc Login user
  // @route POST api/users/login
  [Route("login")]
  [HttpPost]
  public async Task<IActionResult> Login([FromBody] LoginUser loginUser,
    [FromServices] IValidator<LoginUser> loginUserValidator)
  {
    var loginUserValidatorResult = loginUserValidator.Validate(loginUser);
    if (!loginUserValidatorResult.IsValid)
      return BadRequest(new { Message = loginUserValidatorResult.Errors?.First().ErrorMessage });

    User? user = await _usersService.FindUser(loginUser);

    if (user == null)
      return NotFound(new { Message = "Пользователь не найден" });

    var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Id.ToString()) };
    string accessToken = _tokenService.GenerateAccessToken(claims);

    string refreshToken = _tokenService.GenerateRefreshToken();
    await _usersService.SetUserRefreshToken(user, refreshToken);


    return Ok(new
    {
      Login = user.Login,
      Token = new TokenDto()
      {
        AccessToken = accessToken,
        RefreshToken = refreshToken
      }
    });
  }

  // @desc Register user
  // @route POST api/users/register
  [Route("register")]
  [HttpPost]
  public async Task<IActionResult> Register([FromBody] RegisteredUser registeredUser,
    [FromServices] IValidator<RegisteredUser> registeredUserValidator)
  {
    var registeredUserValidatorResult = registeredUserValidator.Validate(registeredUser);
    if (!registeredUserValidatorResult.IsValid)
      return BadRequest(new { Message = registeredUserValidatorResult.Errors.First().ErrorMessage });

    if (await _usersService.FindUser(u => u.Login == registeredUser.Login) != null)
      return BadRequest(new { Message = "Пользователь с данным логином уже зарегестрирован" });

    User user = await _usersService.AddUser(registeredUser);

    return Ok(new
    {
      Login = user.Login
    });
  }
}