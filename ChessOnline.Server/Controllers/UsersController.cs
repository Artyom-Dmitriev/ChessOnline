using ChessOnline.Server.Data;
using ChessOnline.Shared.DTOs;
using ChessOnline.Server.Models;
using ChessOnline.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

        [AllowAnonymous]
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

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
                return Unauthorized("Неверный email или пароль.");


            if (user.IsGuest == true)
                return Unauthorized("Гостевые аккаунты не могут войти с помощью email и пароля." +
                    " Пожалуйста, зарегистрируйтесь.");


            if (BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash) == false)
                return Unauthorized("Неверный email или пароль.");

            string token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }

        [AllowAnonymous]
        [HttpPost("guest")]
        public async Task<IActionResult> Guest()
        {
            string guestNickname = $"Guest_{Guid.NewGuid().ToString("N")[..8]}";
            User guestUser = new User
            {
                Nickname = guestNickname,           
                IsGuest = true,
                GuestExpiresAt = DateTime.UtcNow.AddHours(24)
            };
            _context.Users.Add(guestUser);
            await _context.SaveChangesAsync();
            string token = _tokenService.GenerateToken(guestUser);
            return Ok(new { Token = token });
        }

        [Authorize]
        [HttpPost("upgrade")]
        public async Task<IActionResult> Upgrade(RegisterRequest request)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId); 

            if (user == null)
                return NotFound("Такого пользователя нет!");

            if (user.IsGuest == false)
                return BadRequest("Аккаунт уже является постоянным!");

            if (await _context.Users.AnyAsync(u => u.Email == request.Email && u.Id != userId))
                return BadRequest("Данный email уже используется.");

            if (await _context.Users.AnyAsync(u => u.Nickname == request.Nickname && u.Id != userId))
                return BadRequest("Данный nickname уже используется.");
            
                user.Nickname = request.Nickname;
                user.Email = request.Email;
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
                user.IsGuest = false;
                user.GuestExpiresAt = null;           

            _context.Users.Update(user);
            await _context.SaveChangesAsync(); 
            string token = _tokenService.GenerateToken(user);

            return Ok(new { Token = token });
        }
    }
}
