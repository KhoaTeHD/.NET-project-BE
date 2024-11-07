using System.ComponentModel.DataAnnotations;

namespace Services.NationAPI.Models
{
    public class Nation
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Boolean Status { get; set; }
    }
}
