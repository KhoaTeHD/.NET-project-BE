using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.ProductAPI.Models.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        public int Cat_Id { get; set; }
        public int Nat_Id { get; set; }
        public int Bra_Id { get; set; }
        public int Sup_Id { get; set; }
        public string Name { get; set; }
        public Boolean Status { get; set; }
        public ICollection<ProductVariationDto>? ProductVariations { get; set; }
        public BrandDto? Brand { get; set; }
        public CategoryDto? Category { get; set; }
        public NationDto? Nation { get; set; }
        public SupplierDto? Supplier { get; set; }
    }
}
