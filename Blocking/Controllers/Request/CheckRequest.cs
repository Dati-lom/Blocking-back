namespace Blocking.Controllers.Request;

public class CheckRequest
{
    public List<string> Usernames { get; set; }
    public bool Blocked { get; set; }
}