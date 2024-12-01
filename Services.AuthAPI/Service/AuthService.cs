using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.AuthAPI.Data;
using Services.AuthAPI.Models;
using Services.AuthAPI.Models.Dto;
using Services.AuthAPI.Service.IService;

namespace Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == email.ToLower());
            if (user != null)
            {
                if(!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    //Create role if it does not exist
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<string> ChangePassword(string userId, string oldPassword, string newPassword)
        {
            // Find the user by ID
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return "User not found";
            }

            // Check if the old password is correct
            var isOldPasswordValid = await _userManager.CheckPasswordAsync(user, oldPassword);
            if (!isOldPasswordValid)
            {
                return "Old password is incorrect";
            }

            // Change the password
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (result.Succeeded)
            {
                return "Password changed successfully";
            }

            return result.Errors.FirstOrDefault()?.Description ?? "Error changing password";
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.Email.ToLower());

            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (user == null || !isValid)
            {
                return new LoginResponseDto
                {
                    User = null,
                    Token = ""
                };
            }

            //if user was found, Generate JWT Token
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            UserDto userDto = new()
            {
                ID = user.Id,
                Name = user.Name,
                AvatarUrl = user.AvatarUrl,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate,
                Gender = user.Gender,
                Status = user.Status,
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };
            return loginResponseDto;
        }

        public async Task<string> Register(RegisterationRequestDto registerationRequestDto)
        {
            ApplicationUser user = new()
            {
                Name = registerationRequestDto.Name,
                Email = registerationRequestDto.Email,
                NormalizedEmail = registerationRequestDto.Email.ToUpper(),
                UserName = registerationRequestDto.Email,
                PhoneNumber = registerationRequestDto.PhoneNumber,
                Gender = registerationRequestDto.Gender,
                BirthDate = registerationRequestDto.BirthDate,
                Status = registerationRequestDto.Status,
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registerationRequestDto.Password);
                if (result.Succeeded)
                {
                    return "";
                } 
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<LoginResponseDto> UpdateUser(string id, UserDto updateUserDto)
        {
            // Tìm người dùng cần cập nhật
            var user = _context.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return new LoginResponseDto
                {
                    User = null,
                    Token = ""
                };
            }

            // Cập nhật thông tin người dùng từ DTO
            if (updateUserDto.Name != user.Name)
            {
                user.Name = updateUserDto.Name;
            }

            if (updateUserDto.Email != user.Email)
            {
                user.Email = updateUserDto.Email;
                user.NormalizedEmail = updateUserDto.Email.ToUpper();
                user.UserName = updateUserDto.Email;
                user.NormalizedUserName = updateUserDto.Email.ToUpper();
            }

            if (updateUserDto.AvatarUrl != user.AvatarUrl)
            {
                user.AvatarUrl = updateUserDto.AvatarUrl;
            }

            if (updateUserDto.PhoneNumber != user.PhoneNumber)
            {
                user.PhoneNumber = updateUserDto.PhoneNumber;
            }

            if (updateUserDto.BirthDate != user.BirthDate)
            {
                user.BirthDate = updateUserDto.BirthDate;
            }

            if (updateUserDto.Gender != user.Gender)
            {
                user.Gender = updateUserDto.Gender;
            }

            if (updateUserDto.Status != user.Status)
            {
                user.Status = updateUserDto.Status;
            }

            // Lưu thay đổi
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            //if user was found, Generate JWT Token
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            UserDto userDto = new()
            {
                ID = user.Id,
                Name = user.Name,
                AvatarUrl = user.AvatarUrl,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate,
                Gender = user.Gender,
                Status = user.Status,
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };
            return loginResponseDto;
        }
    }
}
