using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        [AllowNull]
        public string? AvatarUrl { get; set; }

        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public Boolean Status { get; set; }
    }
}
