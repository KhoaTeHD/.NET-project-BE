namespace Services.AuthAPI.Models.Dto
{
    public class RegisterationRequestDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public string Gender { get; set; }
        public string Password { get; set; }

        public Boolean Status { get; set; }

        public string? Role { get; set; }
    }
}
