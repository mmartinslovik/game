using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository _authRepository;

    public AuthController(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register(UserRegisterDTO request)
    {
        ServiceResponse<int> serviceResponse = await _authRepository.Register(new User { Username = request.Username }, request.Password);

        if (!serviceResponse.Success)
        {
            return BadRequest(serviceResponse);
        }

        return Ok(serviceResponse);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserLoginDTO request)
    {
        ServiceResponse<string> serviceResponse = await _authRepository.Login(request.Username, request.Password);

        if (!serviceResponse.Success)
        {
            return BadRequest(serviceResponse);
        }

        return Ok(serviceResponse);
    }
}