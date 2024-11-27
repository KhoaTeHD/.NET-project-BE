namespace Services.OrderAPI.Models.Dto
{
    public class ProductVariationDto
    {
        public int Id { get; set; }
        public int Pro_Id { get; set; }
        public int Col_Id { get; set; }
        public int Siz_Id { get; set; }
        public decimal Price { get; set; }
        public decimal ImportPrice { get; set; }
        public string Pic { get; set; }
        public int Quantity { get; set; }
        public string Desc { get; set; }
        public int Discount { get; set; }
        public Boolean Status { get; set; }
        public ColorDto? Color { get; set; }
        public SizeDto? Size { get; set; }
        public ProductDto? Product { get; set; }
    }
}
