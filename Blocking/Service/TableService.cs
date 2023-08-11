using Blocking.Context;
using BlockingUsers.Modules;
using Microsoft.EntityFrameworkCore;

namespace Blocking.Service;

public class TableService:ITableService
{
    private readonly IConfiguration _configuration;
    private readonly AuthContext _authContext;

    public TableService(IConfiguration configuration,AuthContext authContext)
    {
        _authContext = authContext;
        _configuration = configuration;
    }

    public string DeleteSelected(List<UserDto> usersList)
    {
        if (usersList == null || usersList.Count == 0)
        {
            return "No Input";
        }

        foreach (var userDto in usersList)
        {
            var existUser = _authContext.Users.FirstOrDefault(u => u.Username == userDto.Username);
            if (existUser != null) _authContext.Users.Remove(existUser);
        }

        _authContext.SaveChanges();
        return "removed successfully";

    }

    public string BlockSelected(List<string> usernames, bool blocked)
    {
        var usersToupdate = _authContext.Users.Where(u => usernames.Contains(u.Username)).ToList();
        foreach (var user in usersToupdate)
        {
            user.Blocked = blocked;
        }

        _authContext.SaveChanges();

        return "Updated";
    }

    public List<Users> GetAll()
    {
        return _authContext.Users.ToList();
    }
}