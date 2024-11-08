using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS3_Back_End.DTO.RequestDTOs;
using MS3_Back_End.IService;

namespace MS3_Back_End.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpRequestDTO request)
        {
            try
            {
                var userData = await _userService.SignUp(request);
                return Ok(userData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("signin")]

        public async Task<IActionResult> SignIn(SignInRequestDTO signInRequest)
        {
            try
            {
                var returnMessage = await _userService.SignIn(signInRequest);
                return Ok(returnMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
