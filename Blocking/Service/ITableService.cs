using BlockingUsers.Modules;

namespace Blocking.Service;

public interface ITableService
{
    
    public string DeleteSelected(List<UserDto> usersList);
    public string BlockSelected(List<string> usersList, bool blocked);
    public List<Users> GetAll();
}