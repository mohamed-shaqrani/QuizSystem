using Infrastructure.AuthService;
using Infrastructure.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
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
        var auth = await _authService.RegisterAsync(model);
        if (auth.Message != null)
        {
            return BadRequest(auth.Message);
        }
        else
        {
            return Ok(auth);

        }
    }
}
