using Services.CartItemAPI.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Services.CartItemAPI.Models
{
    public class CartItem
    {
        // Xóa thuộc tính [Key] ở đây vì chúng ta sẽ định nghĩa khóa chính phức hợp
        public int Item_Id { get; set; }
        public string Cus_Id { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [ForeignKey("Item_Id")]
        public required ProductVariationDto ProductVariation { get; set; }

        [ForeignKey("Cus_Id")]
        public required UserDto User { get; set; }
    }
}
