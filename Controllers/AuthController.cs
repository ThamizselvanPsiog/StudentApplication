using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudTeacher.Data;
using StudTeacher.DTO;
using StudTeacher.Models;
using StudTeacher.Services;

namespace StudTeacher.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var response = await _authService.RegisterAsync(dto);
            if (response == null) return BadRequest("User already exists.");
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var response = await _authService.LoginAsync(dto);
            if (response == null) return Unauthorized("Invalid credentials.");
            return Ok(response);
        }
    }
}
