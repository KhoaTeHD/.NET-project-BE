namespace Services.OrderAPI.Models.Dto
{
    public class BrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean Status { get; set; }

        public ProductVariationDto ProductVariation { get; set; }
    }
}
