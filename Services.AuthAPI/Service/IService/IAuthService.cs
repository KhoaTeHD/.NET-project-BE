using Services.AuthAPI.Models.Dto;

namespace Services.AuthAPI.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegisterationRequestDto registerationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);

        Task<LoginResponseDto> UpdateUser(string id, UserDto updateUserDto);

        Task<string> ChangePassword(string userId, string oldPassword, string newPassword);
    }
}
