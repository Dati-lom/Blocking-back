using BlockingUsers.Modules;

namespace Blocking.Service;

public interface IAuthService
{
    IEnumerable<Users> user { get; }
    bool Register(UserDto user);
    Users Login(string username, string password);

    Users LogOut(string token);
}