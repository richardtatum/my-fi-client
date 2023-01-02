using Microsoft.AspNetCore.Mvc;
using MyFi.TheBadlands.Services;

namespace MyFi.TheBadlands.Controllers;

[Route("api/v1/[controller]")]
public class AuthController: ControllerBase
{
    private readonly UserService _user;
    private readonly MonzoService _monzo;

    public AuthController(UserService user, MonzoService monzo)
    {
        _user = user;
        _monzo = monzo;
    }

    [HttpGet("callback")]
    public async Task<IActionResult> Callback(string code, Guid state)
    {
        if (!await _user.UserExistsAsync(state))
        {
            return UnprocessableEntity("Unable to find user");
        }

        await _monzo.ProcessUser(code, state);

        return Ok();
    }
}