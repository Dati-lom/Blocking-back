using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlockingUsers.Modules;
using Microsoft.AspNetCore.Mvc;
using BCrypt;
using Blocking.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32.SafeHandles;

namespace BlockingUsers.Controller;

[Route("api/[controller]")]
[ApiController]
public class AuthController:ControllerBase
{
    
    
    private readonly IConfiguration _configuration;
    private readonly IAuthService _authService;
    
    public AuthController(IConfiguration configuration, IAuthService authService)
    {
        _configuration = configuration;
        _authService = authService;
    }

    
    [HttpPost("register")]
    public ActionResult<Users> Register(UserDto request)
    {
        if (!_authService.Register(request))
        {
            return Conflict("Username Already exists");
        }

        return Ok("Registration Successful");
    }
    
    [HttpGet("login")]
    public ActionResult<Users> Login(UserDto request)
    {
        var user = _authService.Login(request.Username, request.Password);

        if (user == null) 
        {
            return BadRequest("User Not Found");
        }
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return BadRequest("Wrong Password");
        }
        return Ok(user);
    }
    
    [HttpGet("logout")]
    public ActionResult<Users> Logout(UserDto request)
    {
        var user = _authService.LogOut(request.Username);
        if (user == null) return BadRequest("Something went wrong");

        return Ok(user);
    }

}