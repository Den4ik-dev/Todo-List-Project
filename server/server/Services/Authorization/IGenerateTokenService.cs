
namespace server.Services.Authorization;

public interface IGenerateTokenService
{
  public string GetToken(List<System.Security.Claims.Claim> claims, DateTime expires = default);
}
