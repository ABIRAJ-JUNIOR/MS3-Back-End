using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTOs.RequestDTOs.Auth;
using MS3_Back_End.IService;

namespace MS3_Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("SignUp")]
        public async Task<ActionResult<string>> SignUp(SignUpRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Sign up data is required.");
            }

            try
            {
                var data = await _authService.SignUp(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during sign up");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SignIn")]
        public async Task<ActionResult<string>> SignIn(SignInRequestDTO request)
        {
            if (request == null)
            {
                return BadRequest("Sign in data is required.");
            }

            try
            {
                var data = await _authService.SignIn(request);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during sign in");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Verify/{userId}")]
        public async Task<ActionResult<string>> EmailVerify(Guid userId)
        {
            try
            {
                var data = await _authService.EmailVerify(userId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error verifying email for user id {userId}");
                return BadRequest(ex.Message);
            }
        }
    }
}
