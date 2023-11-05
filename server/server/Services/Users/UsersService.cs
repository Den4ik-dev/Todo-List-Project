using Microsoft.EntityFrameworkCore;
using server.Dto;
using server.Models;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace server.Services.Users;
public class UsersService : IUsersService
{
  private ApplicationContext _db;
  public UsersService(ApplicationContext db)
  {
    _db = db;
  }

  public async Task<User?> FindUser(Expression<Func<User, bool>> predicate) =>
    await _db.Users.FirstOrDefaultAsync(predicate);
  public async Task<User?> FindUser(LoginUser loginUser) =>
    await FindUser(u =>
      u.Login == loginUser.Login &&
      u.Password == Convert.ToHexString(
        SHA512.Create().ComputeHash(
          System.Text.Encoding.UTF8.GetBytes(loginUser.Password))));

  public async Task SetUserRefreshToken(User user, string refreshToken)
  {
    user.RefreshToken = refreshToken;
    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

    await _db.SaveChangesAsync();
  }
  public async Task<User> AddUser(RegisteredUser registeredUser)
  {
    User newUser = _db.Users.Add(new User()
    {
      Login = registeredUser.Login,
      Password = Convert.ToHexString(
        SHA512.Create().ComputeHash(
          System.Text.Encoding.UTF8.GetBytes(registeredUser.Password)))
    }).Entity;
    await _db.SaveChangesAsync();

    return newUser;
  }
}