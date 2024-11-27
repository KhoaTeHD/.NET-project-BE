
namespace Services.ProductAPI.Models.Dto
{
    public class UpdateProductDto
    {
        public int Id { get; set; }
        public int Cat_Id { get; set; }
        public int Nat_Id { get; set; }
        public int Bra_Id { get; set; }
        public int Sup_Id { get; set; }
        public string Name { get; set; }
        public Boolean Status { get; set; }
        public ICollection<ProductVariationDto>? ProductVariations { get; internal set; }
    }
}
