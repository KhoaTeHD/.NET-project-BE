using System.Data.SqlTypes;

namespace Services.CartItemAPI.Models.Dto
{
    public class CartItemDto
    {
        public int Item_Id { get; set; }
        public int Cus_Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
