using Core.Constants;
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
    [HttpPost("register")]
    public async Task<IActionResult> RegisterStudent(RegisterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.RegisterAsync(model, UserRole.Student);
        if (!result.IsAuthenticated)
            return BadRequest(result.Message);
        else
            return Ok(result);
    }
    [HttpPost("register-instructor")]
    public async Task<IActionResult> RegisterInstructor(RegisterModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.RegisterAsync(model, UserRole.Instructor);
        if (!result.IsAuthenticated)
            return BadRequest(result.Message);
        else
            return Ok(result);
    }
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] TokenRequestModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.GetTokenAsync(model);
        if (!result.IsAuthenticated)
            return BadRequest(result.Message);
        else
            return Ok(result);
    }
    [HttpPost("instructor-login")]
    public async Task<ActionResult> LoginAsInstructor([FromBody] TokenRequestModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.GetTokenAsync(model);
        if (!result.IsAuthenticated)
            return BadRequest(result.Message);
        else
            return Ok(result);
    }

}
