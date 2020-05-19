using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Business.Abstract;
using MyBlog.Entities.Dtos;
using System.Linq;
using System.Security.Claims;

namespace MyBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;
        IUserService _userService;
        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var user = _userService.GetByEmail(email);
            if (!user.Success)
                return BadRequest(user.Message);

            var result = _authService.CreateAccessToken(user.Data);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDto userLoginDto)
        {
            var userToLogin = _authService.Login(userLoginDto);
            if (!userToLogin.Success)
                return BadRequest(userToLogin.Message);

            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("register")]
        public IActionResult Register(UserRegisterDto userRegisterDto)
        {
            var userToRegister = _authService.Register(userRegisterDto);
            if (!userToRegister.Success)
                return BadRequest(userToRegister.Message);

            var result = _authService.CreateAccessToken(userToRegister.Data);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

    }
}