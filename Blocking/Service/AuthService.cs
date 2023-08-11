using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blocking.Context;
using BlockingUsers.Modules;
using Microsoft.IdentityModel.Tokens;

namespace Blocking.Service;


public class AuthService:IAuthService
{
    private readonly AuthContext _authContext;
    private IConfiguration _configuration;

    public AuthService(AuthContext authContext,IConfiguration configuration)
    {
        _authContext = authContext;
        _configuration = configuration;
    }


    public IEnumerable<Users> user { get; }

    public bool Register(UserDto request)
    {
        Users users = new Users();
        if (_authContext.Users.Any(u => u.Username == request.Username))
        {
            return false;
        }
        
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        users.Username = request.Username;
        users.PasswordHash = passwordHash;
        users.Blocked = false;

        _authContext.Users.Add(users);
        _authContext.SaveChanges();
        return true;
    }
    public Users Login(string username,string password)
    {
        var user = _authContext.Users.FirstOrDefault(u => u.Username == username);

        if (user == null) return null;
        
        UserDto userDto = new UserDto();
        userDto.Username = username;
        userDto.Password = password;

        string token = CreateToken(userDto);

        user.Token = token;
        _authContext.SaveChanges();

        return user;

    }

    public Users LogOut(string username)
    {
        var user = _authContext.Users.FirstOrDefault(e => e.Username == username);

        if (user == null) return null;
        user.Token = "";
        _authContext.SaveChanges();
        return user;
    }

    private string CreateToken(UserDto user)
    {
        List<Claim> claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Username)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: cred
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}