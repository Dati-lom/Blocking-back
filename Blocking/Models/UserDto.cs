namespace BlockingUsers.Modules;

public class UserDto
{
    public string Username { get; set; } = string.Empty;
    public bool Blocked { get; set; } = false;
    public string Password { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}