using Blocking.Controllers.Request;
using Blocking.Service;
using BlockingUsers.Modules;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blocking.Controllers;

[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class TableController:Controller
{
    private readonly ITableService _tableService;
    private readonly IConfiguration _configuration;

    public TableController(IConfiguration configuration, ITableService tableService)
    {
        _tableService = tableService;
        _configuration = configuration;
    }

    [HttpDelete("delete")]
    public IActionResult DeleteUsers(List<UserDto> users)
    {
        _tableService.DeleteSelected(users);
        return Ok("Deleted");
    }

    [HttpPut("update")]
    public IActionResult BlockSelected([FromBody] CheckRequest checkRequest)
    {
        if (checkRequest == null || checkRequest.Usernames == null || checkRequest.Usernames.Count == 0)
        {
            return BadRequest("Bad Request");
        }

        _tableService.BlockSelected(checkRequest.Usernames,checkRequest.Blocked);

        return Ok("Blocked Status Updated");
    }

    [HttpGet("getAll")]
    public IActionResult GetAll()
    {
        return Ok(_tableService.GetAll());
    }

}