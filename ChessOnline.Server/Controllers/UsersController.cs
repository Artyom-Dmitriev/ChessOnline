using ChessOnline.Server.Data;
using ChessOnline.Server.DTOs;
using ChessOnline.Server.Models;
using ChessOnline.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChessOnline.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public UsersController(AppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                return BadRequest("Данный email уже используется.");
            if (await _context.Users.AnyAsync(u => u.Nickname == request.Nickname))
                return BadRequest("Данный nickname уже используется.");

            User user = new User
            {
                Nickname = request.Nickname,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                IsGuest = false
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            string token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
                return Unauthorized("Неверный email или пароль.");

            if (BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash) == false)
                return Unauthorized("Неверный email или пароль.");

            string token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }
    }
}
