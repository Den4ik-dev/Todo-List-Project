namespace server.Services.Tokens;
public interface ITokenService
{
  public string GenerateAccessToken(IEnumerable<System.Security.Claims.Claim> claims);
  public string GenerateRefreshToken();
  public System.Security.Claims.ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}