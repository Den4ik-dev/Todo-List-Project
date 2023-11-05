using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace server.Services.Tokens;
public class JwtTokenService : ITokenService
{
  private IConfiguration _configuration;
  public JwtTokenService(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public string GenerateAccessToken(IEnumerable<Claim> claims)
  {
    JwtSecurityToken jwt = new JwtSecurityToken(
      issuer: _configuration["JWT:ISSUER"], 
      audience: _configuration["JWT:AUDIENCE"],
      claims: claims,
      expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)), 
      signingCredentials: new SigningCredentials(
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:KEY"])),
        SecurityAlgorithms.HmacSha256));

    return new JwtSecurityTokenHandler().WriteToken(jwt);
  }

  public string GenerateRefreshToken()
  {
    byte[] randomNumber = new byte[32];
    using(var rng = RandomNumberGenerator.Create())
    {
      rng.GetBytes(randomNumber);
      return Convert.ToBase64String(randomNumber);
    }
  }

  public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
  {
    var tokenValidationParameters = new TokenValidationParameters()
    {
      ValidateIssuer = true,
      ValidIssuer = _configuration["JWT:ISSUER"],
      ValidateAudience = true,
      ValidAudience = _configuration["JWT:AUDIENCE"],
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:KEY"])),
      ValidateLifetime = false
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    ClaimsPrincipal principal =
      tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

    if (securityToken is not JwtSecurityToken jwtSecurityToken || 
        !jwtSecurityToken.Header.Alg.Equals(
          SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
      throw new SecurityTokenException("Некорректный токен");

    return principal;
  }
}
