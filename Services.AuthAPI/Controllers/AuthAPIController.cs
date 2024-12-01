using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.AuthAPI.Data;
using Service.AuthAPI.Models.Dto;
using Services.AuthAPI.Models;
using Services.AuthAPI.Models.Dto;
using Services.AuthAPI.Service.IService;
using System.Security.Claims;

namespace Services.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        public readonly IAuthService _authService;
        protected ResponseDto _response;
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public AuthAPIController(IAuthService authService, AppDbContext dbContext, IMapper mapper)
        {
            _authService = authService;
            _response = new();
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationRequestDto model)
        {
            var errorMessages = await _authService.Register(model);
            if(!string.IsNullOrEmpty(errorMessages))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessages;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);
            if(loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Invalid login request";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegisterationRequestDto model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                _response.IsSuccess = false;
                _response.Message = "Error encountered";
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        // GET: api/user
        [HttpGet]
        public async Task<IActionResult> GetListUser()
        {
            try
            {
                var users = await _dbContext.Users.ToListAsync();
                var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
                return Ok(userDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/user/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN,CUSTOMER")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var userDto = _mapper.Map<UserDto>(user);
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/user/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("checkUnique")]
        public async Task<IActionResult> CheckUnique(string field, string value)
        {
            bool isUnique = field.ToLower() switch
            {
                "email" => !await _dbContext.Users.AnyAsync(u => u.Email == value),
                "phonenumber" => !await _dbContext.Users.AnyAsync(u => u.PhoneNumber == value),
                _ => throw new ArgumentException("Invalid field")
            };
            return Ok(isUnique);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN,CUSTOMER")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDto updateUserDto)
        {
            var loginResponse = await _authService.UpdateUser(id, updateUserDto);
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Failed to update user!";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpGet("checkDuplicateForUpdate")]
        public async Task<IActionResult> CheckDuplicateForUpdate(string userId, string field, string value)
        {
            if (string.IsNullOrEmpty(field) || string.IsNullOrEmpty(value))
            {
                return BadRequest("Field and value are required.");
            }

            bool isDuplicate = field.ToLower() switch
            {
                "email" => await _dbContext.Users.AnyAsync(u => u.Email == value && u.Id != userId),
                "phonenumber" => await _dbContext.Users.AnyAsync(u => u.PhoneNumber == value && u.Id != userId),
                _ => throw new ArgumentException("Invalid field")
            };

            return Ok(isDuplicate);
        }

        // POST: api/auth/change-password
        [HttpPost("change-password")]
        [Authorize(Roles = "ADMIN,CUSTOMER")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto changePasswordRequestDto)
        {
            if (changePasswordRequestDto == null || string.IsNullOrEmpty(changePasswordRequestDto.OldPassword) || string.IsNullOrEmpty(changePasswordRequestDto.NewPassword))
            {
                return BadRequest("Old and new password must be provided.");
            }

            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Assuming user ID is stored as a claim
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            var result = await _authService.ChangePassword(userId, changePasswordRequestDto.OldPassword, changePasswordRequestDto.NewPassword);
            if (result == "Password changed successfully")
            {
                return Ok(new { Message = result });
            }

            return BadRequest(new { Message = result });
        }

    }
}
