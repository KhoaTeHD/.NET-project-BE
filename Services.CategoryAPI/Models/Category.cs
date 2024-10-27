using System.ComponentModel.DataAnnotations;

namespace Services.CategoryAPI.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public Boolean Status { get; set; }
    }
}
