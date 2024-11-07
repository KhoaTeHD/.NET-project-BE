using System.ComponentModel.DataAnnotations;

namespace Services.SizeAPI.Models
{
    public class Size
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Desc { get; set; }
        [Required]
        public Boolean Status { get; set; }
    }
}
