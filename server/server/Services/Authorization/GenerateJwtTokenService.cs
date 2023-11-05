using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace server.Services.Authorization;

public class GenerateJwtTokenService : IGenerateTokenService
{
  private IConfiguration _configuration;
  public GenerateJwtTokenService(IConfiguration configuration) =>
    _configuration = configuration;

  public string GetToken(List<System.Security.Claims.Claim> claims, DateTime expires = default)
  {
    string? issuer = _configuration["JWT:ISSUER"];
    string? audience = _configuration["JWT:AUDIENCE"];

    string? key = _configuration["JWT:KEY"];
    SigningCredentials signingCredentials = new SigningCredentials(
        new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key)),
        SecurityAlgorithms.HmacSha256);


    JwtSecurityToken jwt = new JwtSecurityToken(
      issuer: issuer,
      audience: audience,
      claims: claims,
      expires: expires == default 
        ? DateTime.UtcNow.Add(TimeSpan.FromMinutes(15))
        : expires,
      signingCredentials: signingCredentials);


    return new JwtSecurityTokenHandler().WriteToken(jwt);
  }
}