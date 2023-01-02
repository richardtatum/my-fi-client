using Microsoft.AspNetCore.Mvc;
using MyFi.TheBadlands.Models;
using MyFi.TheBadlands.Repositories;

namespace MyFi.TheBadlands.Controllers;

[Route("api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly CommandRepository _command;
    private readonly QueryRepository _query;

    public UserController(CommandRepository command, QueryRepository query)
    {
        _command = command;
        _query = query;
    }

    [HttpGet]
    public Task<IEnumerable<User>> Get()
    {
        return _query.GetUsersAsync();
    }

    [HttpGet("{id}")]
    public Task<User?> Get(Guid id)
    {
        return _query.GetUserAsync(id);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody]InsertUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request?.Name))
        {
            return BadRequest();
        }

        var userId = await _command.InsertUserAsync(request.Name);
        return Ok(userId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(Guid id, [FromBody]InsertUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request?.Name))
        {
            return BadRequest();
        }

        await _command.UpdateUserAsync(id, request.Name);
        return Ok();
    }

    [HttpDelete("{id}")]
    public Task Delete(Guid id)
    {
        return _command.DeleteUserAsync(id);
    }
}