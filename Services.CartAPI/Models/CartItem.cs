using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace Services.CartItemAPI.Models
{
    public class CartItem
    {
        [Key]
        public int Item_Id { get; set; }
        [Required]
        public int Cus_Id { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
