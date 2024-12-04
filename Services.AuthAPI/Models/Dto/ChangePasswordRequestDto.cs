namespace Services.AuthAPI.Models.Dto
{
    public class ChangePasswordRequestDto
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
