﻿using System.Diagnostics.CodeAnalysis;

namespace Services.AuthAPI.Models.Dto
{
    public class UserDto
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string? AvatarUrl { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public string Gender { get; set; }
        public Boolean Status { get; set; }
    }
}
