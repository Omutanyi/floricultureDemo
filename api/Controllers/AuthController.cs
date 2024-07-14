using api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly JwtService _jwtService;

    public AuthController(UserService userService, JwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
    {
        try
        {
            var user = await _userService.RegisterUser(dto.UserName, dto.Email, dto.Password);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
    {
        var user = await _userService.AuthenticateUser(dto.UserName, dto.Password);
        if (user == null)
        {
            return Unauthorized();
        }

        // Generate JWT token here and return
        //  var token = _jwtService.GenerateToken(user.Email);

        // return Ok(new { Token = token });
        return Ok();
    }
}

public class RegisterUserDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class LoginUserDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
