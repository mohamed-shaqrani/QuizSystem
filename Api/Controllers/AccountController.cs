using Infrastructure.AuthService;
using Infrastructure.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost]
    public async Task<IActionResult> RegisterStudent(RegisterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.RegisterAsync(model);
        if (!result.IsAuthenticated)
            return BadRequest(result.Message);
        else
            return Ok(result);
    }
    [HttpGet]
    public async Task<ActionResult> Login([FromQuery] TokenRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _authService.GetTokenAsync(model);
        if (!result.IsAuthenticated)
            return BadRequest(result.Message);
        else
            return Ok(result);
    }
}
