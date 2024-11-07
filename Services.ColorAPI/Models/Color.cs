using System.ComponentModel.DataAnnotations;

namespace Services.ColorAPI.Models
{
    public class Color
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Boolean Status { get; set; }
    }
}
