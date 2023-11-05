using server.Dto;
using server.Models;
using System.Linq.Expressions;

namespace server.Services.Users;
public interface IUsersService
{
  public Task<User> FindUser(Expression<Func<User, bool>> predicate);
  public Task<User?> FindUser(LoginUser loginUser);
  public Task SetUserRefreshToken(User user, string refreshToken);
  public Task<User> AddUser(RegisteredUser registeredUser);
}