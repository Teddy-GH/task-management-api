using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using task_management_system.Dto;
using task_management_system.Interfaces;
using task_management_system.Models;

namespace task_management_system.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signInManager;

        public object Int { get; private set; }

        public AuthController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        [HttpGet("users")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = _userManager.Users.ToList();

            var userDtos = users.Select(u => new UserResponseDto
            {
                Id = Guid.Parse(u.Id),
                UserName = u.UserName,
                Email = u.Email,
                Role = u.Role,
                CreatedAt = u.CreatedAt
            }).ToList();

            return Ok(userDtos);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new User
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");

                    if (roleResult.Succeeded)
                    {
                        return Ok("User  Created");
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);

                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);

            }
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = await _userManager.FindByNameAsync(loginDto.Username);

                if (user == null)
                    return Unauthorized("Invalid username or password");

                var signInResponse = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                if (!signInResponse.Succeeded)
                    return Unauthorized("Incorrect username or password");

       

                return Ok(new LoginResponseDto
                {
                    Id = user.Id.ToString(),  
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = await _tokenService.CreateToken(user),
                    CreatedAt = user.CreatedAt,  
                    Role = user.Role.ToString()
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

    }
}



